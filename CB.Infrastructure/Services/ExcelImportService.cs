
using System.Globalization;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Core.Enums;
using CB.Infrastructure.Persistance;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly AppDbContext _context;
        public ExcelImportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ImportRatesAsync(Stream fileStream, DateTime date)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet(1);
            var currencyStartRow = workSheet
                        .RowsUsed()
                        .FirstOrDefault(x => x.Cell(1).GetValue<string>().Contains("Xarici valyutalar"))
                        ?.RowNumber() + 1;

            var metalStartRow = workSheet
                        .RowsUsed()
                        .FirstOrDefault(x => x.Cell(1).GetValue<string>().Contains("Bank metalları"))
                        ?.RowNumber() + 1;

            if (currencyStartRow is null || metalStartRow is null)
                throw new Exception("Excel formatı düzgün deyil");
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReadAndSaveRates(workSheet, currencyStartRow.Value, metalStartRow.Value - 2, ValuteType.EXTERNAL_VALUTE, date);
                var lastRow = workSheet.LastRowUsed()?.RowNumber();
                ReadAndSaveRates(workSheet, metalStartRow.Value, lastRow.Value - 5, ValuteType.METAL, date);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ImportYieldCurveAsync(Stream fileStream, DateTime date)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet(1);

            var usedRows = workSheet.RowsUsed().ToList();
            if (!usedRows.Any())
                throw new Exception("Excel-də istifadə olunan sətir yoxdur.");

            int startRow = 2;
            int endRow = usedRows.Last().RowNumber();

            try
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                ReadAndSaveCurves(workSheet, startRow, endRow, date);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ImportYieldDurationAsync(Stream fileStream, DateTime date)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet(1);
            int? lastRow = workSheet.LastRowUsed()?.RowNumber();
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReadAndSaveDurations(workSheet, 2, lastRow.Value, date);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ImportYieldParameterAsync(Stream fileStream, DateTime date)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet(1);
            int? lastRow = workSheet.LastRowUsed()?.RowNumber();
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReadAndSaveParameters(workSheet, 2, lastRow.Value, date);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ImportMarketDegreeAsync(Stream fileStream)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet(1);
            int? lastRow = workSheet.LastRowUsed()?.RowNumber();
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReadAndSaveMarketDegrees(workSheet, 191, lastRow.Value);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ImportMarketInformationAsync(Stream fileStream)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet(1);

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReadAndSaveMarketInformation(workSheet);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ImportIndexPeriodAsync(Stream fileStream)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet("Toplanan Faizli indekslər");

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReadAndSaveIndexPeriods(workSheet);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ImportIndexIncreasingAsync(Stream fileStream)
        {
            fileStream.Position = 0;
            using var workBook = new XLWorkbook(fileStream);
            var workSheet = workBook.Worksheet("Toplanan Faizli indekslər");

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReadAndSaveIndexIncreasing(workSheet);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        private void ReadAndSaveRates(IXLWorksheet sheet, int startRow, int endRow, ValuteType valuteType, DateTime date)
        {
            List<Language> languages = _context.Languages.Where(x => x.DeletedAt == null).ToList();
            for (int i = startRow; i <= endRow; i++)
            {
                string code = sheet.Cell(i, 2).GetValue<string>();
                if (string.IsNullOrWhiteSpace(code)) continue;

                string unit = sheet.Cell(i, 1).GetValue<string>();
                string title = sheet.Cell(i, 3).GetValue<string>();
                double index = sheet.Cell(i, 7).GetValue<double>();

                Valute? valute = _context.Valutes.FirstOrDefault(x => x.Code == code);

                if (valute is null)
                {
                    valute = new Valute
                    {
                        Unit = unit,
                        Code = code,
                        Type = valuteType,
                    };

                    valute.Translations.Clear();

                    foreach (Language language in languages)
                    {
                        valute.Translations.Add(new ValuteTranslation
                        {
                            LanguageId = language.Id,
                            Title = title,
                        });
                    }

                    _context.Valutes.Add(valute);
                    _context.SaveChanges();
                }

                var oldRates = _context.Rates.Where(x => x.Date.Date == date.Date && x.ValuteId == valute.Id);
                _context.Rates.RemoveRange(oldRates);
                _context.Rates.Add(new Rate
                {
                    Date = date,
                    Index = index,
                    ValuteId = valute.Id,
                });
            }
        }

        private void ReadAndSaveCurves(IXLWorksheet sheet, int startRow, int endRow, DateTime date)
        {
            var oldCurves = _context.YieldCurves.Where(x => x.Date.Date == date.Date);
            _context.YieldCurves.RemoveRange(oldCurves);

            for (int i = startRow; i <= endRow; i++) // <= olmalıdır
            {
                if (sheet.Cell(i, 2).IsEmpty() || sheet.Cell(i, 3).IsEmpty())
                    continue;

                int duration = sheet.Cell(i, 2).GetValue<int>();
                double index = sheet.Cell(i, 3).GetValue<double>();

                _context.YieldCurves.Add(new YieldCurve
                {
                    Date = date,
                    Duration = duration,
                    Index = index,
                });
            }
        }

        private void ReadAndSaveDurations(IXLWorksheet sheet, int startRow, int endRow, DateTime date)
        {
            List<Language> languages = _context.Languages.Where(x => x.DeletedAt == null).ToList();
            var oldDurations = _context.YieldDurations.Where(x => x.Date.Date == date.Date);
            _context.YieldDurations.RemoveRange(oldDurations);
            for (int i = startRow; i < endRow; i++)
            {
                string title = sheet.Cell(i, 2).GetValue<string>();
                Duration? duration = _context.Durations
                            .Include(x => x.Translations)
                            .ThenInclude(x => x.Language)
                            .FirstOrDefault(x => x.Translations.Any(t => t.LanguageId == 1 && t.Title == title));
                if (duration is null)
                {
                    duration = new Duration();
                    duration.Translations = new List<DurationTranslation>();
                    foreach (Language language in languages)
                    {
                        duration.Translations.Add(new DurationTranslation
                        {
                            LanguageId = language.Id,
                            Title = title
                        });
                    }

                    _context.Durations.Add(duration);
                    _context.SaveChanges();
                }
                double index = sheet.Cell(i, 3).GetValue<double>();
                _context.YieldDurations.Add(new YieldDuration
                {
                    Date = date,
                    DurationId = duration.Id,
                    Index = index,
                });
            }
        }

        private void ReadAndSaveParameters(IXLWorksheet sheet, int startRow, int endRow, DateTime date)
        {
            List<Language> languages = _context.Languages.Where(x => x.DeletedAt == null).ToList();
            var oldDurations = _context.YieldParameters.Where(x => x.Date.Date == date.Date);
            _context.YieldParameters.RemoveRange(oldDurations);
            for (int i = startRow; i < endRow; i++)
            {
                double betaZero = sheet.Cell(i, 1).GetValue<double>();
                double betaOne = sheet.Cell(i, 2).GetValue<double>();
                double betaTwo = sheet.Cell(i, 3).GetValue<double>();
                double lambda = sheet.Cell(i, 4).GetValue<double>();
                _context.YieldParameters.Add(new YieldParameter
                {
                    Date = date,
                    BetaZero = betaZero,
                    BetaOne = betaOne,
                    BetaTwo = betaTwo,
                    Lambda = lambda
                });
            }
        }

        private void ReadAndSaveMarketDegrees(IXLWorksheet sheet, int startRow, int endRow)
        {
            var items = new List<MarketPercentDegree>();

            for (int i = startRow; i <= endRow; i++)
            {
                var dateCell = sheet.Cell(i, 2);
                if (!TryGetCellValue<DateTime>(dateCell, out var rowDate))
                    continue; // tarix düzgün deyilsə, sətri keç

                // percentValue-ni string kimi oxuyuruq
                bool okVal = TryGetCellValue<double>(sheet.Cell(i, 3), out double percentValue);
                bool okVol = TryGetCellValue<double>(sheet.Cell(i, 4), out double percentVolume);
                bool okCnt = TryGetCellValue<int>(sheet.Cell(i, 5), out int dealCount);

                if (!okVal && !okVol && !okCnt)
                    continue;

                items.Add(new MarketPercentDegree
                {
                    Date = rowDate.Date,
                    PercentValue = $"{Math.Round(percentValue, 2)}%",  // 2 rəqəm dəqiqliklə
                    PercentVolume = percentVolume,
                    DealCount = dealCount
                });
            }

            if (items.Count == 0) return;

            var targetDates = items.Select(x => x.Date).Distinct().ToList();
            var oldDataQuery = _context.MarketPercentDegrees
                .Where(x => targetDates.Contains(x.Date));
            _context.MarketPercentDegrees.RemoveRange(oldDataQuery);

            _context.MarketPercentDegrees.AddRange(items);
        }

        private void ReadAndSaveMarketInformation(IXLWorksheet sheet)
        {
            // Excel-də sağ tərəfdəki məlumatlar H sütunundan başlayır
            // H2 = "AZIR indeksi ilə bağlı məlumatlar"
            // Sonrakı sətirlər: H3 → "Yenilənmə tarixi", I3 → "9/23/2025"
            int startRow = 3;
            int endRow = 13; // sən screenshotda 11 sətir idi

            // köhnə məlumatları silirik
            var oldData = _context.MarketInformations.ToList();
            _context.MarketInformations.RemoveRange(oldData);

            // dilləri çəkək (AZ, EN və s.)
            var languages = _context.Languages.Where(x => x.DeletedAt == null).ToList();

            for (int i = startRow; i <= endRow; i++)
            {
                var title = sheet.Cell(i, 8).GetString().Trim();   // H sütunu (8)
                var value = sheet.Cell(i, 9).GetString().Trim();   // I sütunu (9)

                if (string.IsNullOrWhiteSpace(title))
                    continue;

                var info = new MarketInformation
                {
                    Description = value
                };

                foreach (var lang in languages)
                {
                    info.Translations.Add(new MarketInformationTranslation
                    {
                        LanguageId = lang.Id,
                        Title = title // Əgər çoxdilli lazımdırsa, Title-ları ayrıca translate edəcəksən
                    });
                }

                _context.MarketInformations.Add(info);
            }
        }

        private void ReadAndSaveIndexPeriods(IXLWorksheet sheet)
        {
            int headerRow = 6; // başlıqlar
            int dataRow = 7;   // dəyərlər

            // Tarixi təhlükəsiz oxu
            var dateCell = sheet.Cell(dataRow, 2);
            DateTime date;
            if (dateCell.DataType == XLDataType.DateTime)
            {
                date = dateCell.GetDateTime().Date;
            }
            else if (DateTime.TryParse(dateCell.GetFormattedString(), out var parsedDate))
            {
                date = parsedDate.Date;
            }
            else if (double.TryParse(dateCell.GetFormattedString().Replace(",", "."),
                     NumberStyles.Any, CultureInfo.InvariantCulture, out var oa))
            {
                date = DateTime.FromOADate(oa).Date;
            }
            else
            {
                throw new Exception($"Tarixi oxumaq mümkün olmadı: {dateCell.GetFormattedString()}");
            }

            // dillər
            var languages = _context.Languages.Where(x => x.DeletedAt == null).ToList();

            for (int col = 3; col <= 5; col++)
            {
                string categoryTitle = sheet.Cell(headerRow, col).GetString().Trim();
                if (string.IsNullOrWhiteSpace(categoryTitle))
                    continue;

                // Value oxu (faiz formatını da nəzərə al)
                var cell = sheet.Cell(dataRow, col);
                string value;
                if (cell.DataType == XLDataType.Number && cell.Style.NumberFormat.Format.Contains("%"))
                {
                    value = (cell.GetDouble() * 100).ToString("0.##", CultureInfo.InvariantCulture) + "%";
                }
                else
                {
                    value = cell.GetFormattedString().Trim();
                }

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                // Mövcud category varmı?
                var category = _context.IndexPeriodCategories
                    .Include(x => x.Translations)
                    .FirstOrDefault(x => x.Translations.Any(t => t.Title == categoryTitle));

                if (category == null)
                {
                    category = new IndexPeriodCategory();
                    foreach (var lang in languages)
                    {
                        category.Translations.Add(new IndexPeriodCategoryTranslation
                        {
                            LanguageId = lang.Id,
                            Title = categoryTitle
                        });
                    }
                    _context.IndexPeriodCategories.Add(category);
                    _context.SaveChanges();
                }

                // Təkrar düşməsin: eyni tarix + eyni category varsa, skip
                bool alreadyExists = _context.IndexPeriods
                    .Any(x => x.Date == date && x.IndexPeriodCategoryId == category.Id);
                if (alreadyExists)
                    continue;

                // Dəyəri əlavə et
                var period = new IndexPeriod
                {
                    Date = date,
                    Description = value,
                    IndexPeriodCategoryId = category.Id
                };

                _context.Add(period);
            }
        }

        private static bool TryGetCellValue<T>(IXLCell cell, out T value)
        {
            value = default!;

            // Əgər tip stringdirsə → sadəcə string qaytar
            if (typeof(T) == typeof(string))
            {
                var s = cell.GetString()?.Trim();
                if (string.IsNullOrWhiteSpace(s))
                    return false;

                value = (T)(object)s;
                return true;
            }

            // Əgər birbaşa oxuna bilirsə
            if (cell.TryGetValue<T>(out var direct))
            {
                if (typeof(T) == typeof(double) && IsPercentFormat(cell))
                {
                    double dbl = Convert.ToDouble(direct);
                    value = (T)(object)(dbl * 100);
                }
                else
                {
                    value = direct;
                }
                return true;
            }

            var s2 = cell.GetString()?.Trim();
            if (string.IsNullOrWhiteSpace(s2) || s2 == "-" || s2 == "—")
                return false;

            s2 = s2.Replace("%", "")
                   .Replace("\u00A0", "")
                   .Replace(" ", "")
                   .Replace(",", ".");

            if (typeof(T) == typeof(double))
            {
                if (double.TryParse(s2, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedDouble))
                {
                    value = (T)(object)parsedDouble;
                    return true;
                }
            }
            else if (typeof(T) == typeof(int))
            {
                if (int.TryParse(s2, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedInt))
                {
                    value = (T)(object)parsedInt;
                    return true;
                }
                if (double.TryParse(s2, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedDouble))
                {
                    value = (T)(object)(int)Math.Round(parsedDouble, MidpointRounding.AwayFromZero);
                    return true;
                }
            }
            else if (typeof(T) == typeof(DateTime))
            {
                if (DateTime.TryParse(s2, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsedDate))
                {
                    value = (T)(object)parsedDate.Date;
                    return true;
                }
                if (double.TryParse(s2, NumberStyles.Any, CultureInfo.InvariantCulture, out var oa))
                {
                    try
                    {
                        value = (T)(object)DateTime.FromOADate(oa).Date;
                        return true;
                    }
                    catch { }
                }
            }

            return false;
        }

        private static bool IsPercentFormat(IXLCell cell)
        {
            var fmt = cell.Style.NumberFormat;
            return fmt.Format.Contains("%") || fmt.NumberFormatId == 9 || fmt.NumberFormatId == 10;
        }

        private void ReadAndSaveIndexIncreasing(IXLWorksheet sheet)
        {
            int startRow = 7; // məlumatların başlandığı sətr (H7 → Tarix, I7 → Dəyər)
            int colDate = 8;  // H sütunu
            int colValue = 9; // I sütunu

            int lastRow = sheet.LastRowUsed().RowNumber();

            for (int row = startRow; row <= lastRow; row++)
            {
                var dateCell = sheet.Cell(row, colDate);
                var valueCell = sheet.Cell(row, colValue);

                if (dateCell.IsEmpty() || valueCell.IsEmpty())
                    continue;

                // Tarixi təhlükəsiz oxu
                DateTime date;
                if (dateCell.DataType == XLDataType.DateTime)
                {
                    date = dateCell.GetDateTime().Date;
                }
                else if (DateTime.TryParse(dateCell.GetFormattedString(), out var parsedDate))
                {
                    date = parsedDate.Date;
                }
                else if (double.TryParse(dateCell.GetFormattedString().Replace(",", "."),
                         NumberStyles.Any, CultureInfo.InvariantCulture, out var oa))
                {
                    date = DateTime.FromOADate(oa).Date;
                }
                else
                {
                    continue; // tarixi oxumaq alınmadı
                }

                // Value oxu
                double value = valueCell.GetDouble();

                // Təkrar (eyni tarix) varsa, skip
                bool alreadyExists = _context.IndexIncreasings.Any(x => x.Date == date);
                if (alreadyExists)
                    continue;

                _context.IndexIncreasings.Add(new IndexIncreasing
                {
                    Date = date,
                    Value = value
                });
            }
        }
    }

}
