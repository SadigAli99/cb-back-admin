
namespace CB.Application.Interfaces.Services
{
    public interface IExcelImportService
    {
        Task ImportRatesAsync(Stream fileStream, DateTime date);
        Task ImportYieldCurveAsync(Stream fileStream, DateTime date);
        Task ImportYieldDurationAsync(Stream fileStream, DateTime date);
        Task ImportYieldParameterAsync(Stream fileStream, DateTime date);
        Task ImportMarketDegreeAsync(Stream fileStream);
        Task ImportMarketInformationAsync(Stream fileStream);
        Task ImportIndexPeriodAsync(Stream fileStream);
        Task ImportIndexIncreasingAsync(Stream fileStream);
    }
}
