using System.Security.Claims;
using CB.Application.DTOs.Auth;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Auth;
using CB.Core.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IValidator<LoginDTO> _validator;

        public AccountController(
            IAuthRepository authRepository,
            IValidator<LoginDTO> validator,
             ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _validator = validator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(login);
                if (!validationResult.IsValid)
                    return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });
                User? user = await _authRepository.AuthenticateAsync(login.Email, login.Password);
                if (user is null) return Unauthorized("Email və ya şifrə yanlışdır.");

                string accessToken = _tokenService.GenerateAccessToken(user);
                string refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMonths(1);
                bool updated = await _authRepository.UpdateAsync();
                if (!updated) return BadRequest("Refresh token yenilənmədi.");

                return Ok(new LoginResponseDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    AccessTokenExpires = DateTime.UtcNow.AddHours(3)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDTO request)
        {
            try
            {
                var user = await _authRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
                if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    return Unauthorized("Refresh token etibarsızdır.");

                string newAccessToken = _tokenService.GenerateAccessToken(user);
                string newRefreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMonths(1);

                await _authRepository.UpdateAsync();

                return Ok(new LoginResponseDTO
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    AccessTokenExpires = DateTime.UtcNow.AddMinutes(15)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("İstifadəçi tapılmadı.");

                var userId = int.Parse(userIdClaim);
                var result = await _authRepository.LogoutAsync(userId);

                if (!result) return BadRequest("Logout alınmadı.");

                return Ok("Uğurla çıxış edildi.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}
