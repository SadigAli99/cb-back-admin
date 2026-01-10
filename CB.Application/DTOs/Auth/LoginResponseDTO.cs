namespace CB.Application.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }
    }
}
