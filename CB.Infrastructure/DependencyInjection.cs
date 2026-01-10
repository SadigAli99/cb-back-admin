using System.Text;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Application.Mappings;
using CB.Application.Validators.Translate;
using CB.Infrastructure.Auth;
using CB.Infrastructure.Persistance;
using CB.Infrastructure.Repositories;
using CB.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CB.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            #region General Configurations
            services.AddControllers();
            services.AddValidatorsFromAssemblyContaining<TranslateCreateValidator>();
            services.AddHttpContextAccessor();
            #endregion

            #region JWT Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync("{\"status\":\"error\",\"message\":\"Token etibarsız və ya göndərilməyib\"}");
                        }
                    };
                });

            services.AddAuthorization();
            #endregion

            #region Add Database Connection
            services.AddDbContext<AppDbContext>(options =>
            {
                // options.UseNpgsql(configuration.GetConnectionString("CBARConnection"));
                options.UseSqlServer(configuration.GetConnectionString("CBARConnection"));
            });

            #endregion

            #region Policy and Authorization
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var permissions = db.Permissions.Select(p => p.Name).ToList();

                services.AddAuthorization(options =>
                {
                    foreach (var permission in permissions)
                    {
                        if (!string.IsNullOrEmpty(permission))
                        {
                            options.AddPolicy(permission, policy =>
                                policy.Requirements.Add(new PermissionRequirement(permission)));
                        }
                    }
                });
            }

            #endregion

            #region Auto Mapper
            services.AddAutoMapper(typeof(MappingProfile));
            #endregion

            #region All Dependencies
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILogoService, LogoService>();
            services.AddScoped<ITranslateService, TranslateService>();
            services.AddScoped<ISocialService, SocialService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<IExcelImportService, ExcelImportService>();
            services.AddScoped<IPercentCorridorCategoryService, PercentCorridorCategoryService>();
            services.AddScoped<IPercentCorridorService, PercentCorridorService>();
            services.AddScoped<IMonetaryIndicatorCategoryService, MonetaryIndicatorCategoryService>();
            services.AddScoped<IMonetaryIndicatorService, MonetaryIndicatorService>();
            services.AddScoped<IBankSectorCategoryService, BankSectorCategoryService>();
            services.AddScoped<IBankSectorService, BankSectorService>();
            services.AddScoped<IBankNoteCategoryService, BankNoteCategoryService>();
            services.AddScoped<IBankNoteService, BankNoteService>();
            services.AddScoped<IInflationService, InflationService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IDigitalPortalCaptionService, DigitalPortalCaptionService>();
            services.AddScoped<IDigitalPortalService, DigitalPortalService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IMacroDocumentService, MacroDocumentService>();
            services.AddScoped<IOtherInfoService, OtherInfoService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IChronologyService, ChronologyService>();
            services.AddScoped<IMissionCaptionService, MissionCaptionService>();
            services.AddScoped<IMissionService, MissionService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IFormerChairmanService, FormerChairmanService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IDirectorContactService, DirectorContactService>();
            services.AddScoped<IDirectorDetailService, DirectorDetailService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IManagerContactService, ManagerContactService>();
            services.AddScoped<IManagerDetailService, ManagerDetailService>();
            services.AddScoped<IStructureCaptionService, StructureCaptionService>();
            services.AddScoped<IStatuteService, StatuteService>();
            services.AddScoped<IStatisticCaptionService, StatisticCaptionService>();
            services.AddScoped<IStatisticalBulletinService, StatisticalBulletinService>();
            services.AddScoped<IStatisticalReportService, StatisticalReportService>();
            services.AddScoped<IStatisticalReportFileService, StatisticalReportFileService>();
            services.AddScoped<IRevisionPolicyService, RevisionPolicyService>();
            services.AddScoped<IMethodologyService, MethodologyService>();
            services.AddScoped<ILegalActService, LegalActService>();
            services.AddScoped<IInsurerCaptionService, InsurerCaptionService>();
            services.AddScoped<IInsurerFileService, InsurerFileService>();
            services.AddScoped<IInsurerService, InsurerService>();
            services.AddScoped<IBankCaptionService, BankCaptionService>();
            services.AddScoped<IBankFileService, BankFileService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IOperatorBankCaptionService, OperatorBankCaptionService>();
            services.AddScoped<IOperatorBankService, OperatorBankService>();
            services.AddScoped<ICreditUnionCaptionService, CreditUnionCaptionService>();
            services.AddScoped<ICreditUnionService, CreditUnionService>();
            services.AddScoped<IPaymentAgentCaptionService, PaymentAgentCaptionService>();
            services.AddScoped<IPaymentAgentFileService, PaymentAgentFileService>();
            services.AddScoped<IRegistrationSecurityCaptionService, RegistrationSecurityCaptionService>();
            services.AddScoped<IRegistrationSecurityService, RegistrationSecurityService>();
            services.AddScoped<IShareholderCaptionService, ShareholderCaptionService>();
            services.AddScoped<IControlMeasureCategoryService, ControlMeasureCategoryService>();
            services.AddScoped<IControlMeasureService, ControlMeasureService>();
            services.AddScoped<IMonetaryPolicyCaptionService, MonetaryPolicyCaptionService>();
            services.AddScoped<IMonetaryPolicyDirectionService, MonetaryPolicyDirectionService>();
            services.AddScoped<IMonetaryPolicyDecisionService, MonetaryPolicyDecisionService>();
            services.AddScoped<IMonetaryPolicyReviewService, MonetaryPolicyReviewService>();
            services.AddScoped<IMonetaryPolicyVideoService, MonetaryPolicyVideoService>();
            services.AddScoped<IMonetaryPolicyGraphicService, MonetaryPolicyGraphicService>();
            services.AddScoped<IMonetaryPolicyInstrumentService, MonetaryPolicyInstrumentService>();
            services.AddScoped<IPosterService, PosterService>();
            services.AddScoped<IPolicyAnalysisService, PolicyAnalysisService>();
            services.AddScoped<IPolicyAnalysisFileService, PolicyAnalysisFileService>();
            services.AddScoped<IMediaCaptionService, MediaCaptionService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventContentService, EventContentService>();
            services.AddScoped<IEventImageService, EventImageService>();
            services.AddScoped<IEventVideoService, EventVideoService>();
            services.AddScoped<IFutureEventService, FutureEventService>();
            services.AddScoped<IMediaQueryService, MediaQueryService>();
            services.AddScoped<ICBAR105CaptionService, CBAR105CaptionService>();
            services.AddScoped<ICBAR105EventService, CBAR105EventService>();
            services.AddScoped<IPresidentDecreeService, PresidentDecreeService>();
            services.AddScoped<IDevelopmentArticleService, DevelopmentArticleService>();
            services.AddScoped<IAnniversaryStampService, AnniversaryStampService>();
            services.AddScoped<IAnniversaryCoinService, AnniversaryCoinService>();
            services.AddScoped<ITrainingJournalistService, TrainingJournalistService>();
            services.AddScoped<ICBAR100GalleryService, CBAR100GalleryService>();
            services.AddScoped<ICBAR100VideoService, CBAR100VideoService>();
            services.AddScoped<ICareerCaptionService, CareerCaptionService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<IVacancyDetailService, VacancyDetailService>();
            services.AddScoped<IInternshipCaptionService, InternshipCaptionService>();
            services.AddScoped<IInternshipDirectionService, InternshipDirectionService>();
            services.AddScoped<IInternshipProgramService, InternshipProgramService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<INakhchivanBulletinService, NakhchivanBulletinService>();
            services.AddScoped<INakhchivanPublicationService, NakhchivanPublicationService>();
            services.AddScoped<INakhchivanBlogService, NakhchivanBlogService>();
            services.AddScoped<INakhchivanEventService, NakhchivanEventService>();
            services.AddScoped<INakhchivanContactService, NakhchivanContactService>();
            services.AddScoped<IFinancialDevelopmentService, FinancialDevelopmentService>();
            services.AddScoped<ICustomerRightCaptionService, CustomerRightCaptionService>();
            services.AddScoped<ICitizenApplicationCategoryService, CitizenApplicationCategoryService>();
            services.AddScoped<ICitizenApplicationService, CitizenApplicationService>();
            services.AddScoped<IInfographicService, InfographicService>();
            services.AddScoped<IComplaintIndexCategoryService, ComplaintIndexCategoryService>();
            services.AddScoped<IComplaintIndexService, ComplaintIndexService>();
            services.AddScoped<ICustomerEventService, CustomerEventService>();
            services.AddScoped<ICustomerDocumentService, CustomerDocumentService>();
            services.AddScoped<IPaymentServiceService, PaymentServiceService>();
            services.AddScoped<IInformationBulletinService, InformationBulletinService>();
            services.AddScoped<IReviewApplicationService, ReviewApplicationService>();
            services.AddScoped<IReviewApplicationFileService, ReviewApplicationFileService>();
            services.AddScoped<IReviewApplicationLinkService, ReviewApplicationLinkService>();
            services.AddScoped<IReviewApplicationVideoService, ReviewApplicationVideoService>();
            services.AddScoped<IReceptionCitizenCategoryService, ReceptionCitizenCategoryService>();
            services.AddScoped<IReceptionCitizenService, ReceptionCitizenService>();
            services.AddScoped<IReceptionCitizenFileService, ReceptionCitizenFileService>();
            services.AddScoped<IReceptionCitizenLinkService, ReceptionCitizenLinkService>();
            services.AddScoped<IReceptionCitizenVideoService, ReceptionCitizenVideoService>();
            services.AddScoped<ICustomerContactService, CustomerContactService>();
            services.AddScoped<ICustomerFeedbackCaptionService, CustomerFeedbackCaptionService>();
            services.AddScoped<ICustomerFeedbackService, CustomerFeedbackService>();
            services.AddScoped<ICustomerProposalService, CustomerProposalService>();
            services.AddScoped<ITerritorialOfficeService, TerritorialOfficeService>();
            services.AddScoped<ITerritorialOfficeRegionService, TerritorialOfficeRegionService>();
            services.AddScoped<ITerritorialOfficeStatisticService, TerritorialOfficeStatisticService>();
            services.AddScoped<ICustomerDocumentFileService, CustomerDocumentFileService>();
            services.AddScoped<IFraudStatisticCaptionService, FraudStatisticCaptionService>();
            services.AddScoped<IFraudStatisticService, FraudStatisticService>();
            services.AddScoped<IExecutionStatusService, ExecutionStatusService>();
            services.AddScoped<IPaymentSystemCaptionService, PaymentSystemCaptionService>();
            services.AddScoped<IRealTimeSettlementSystemService, RealTimeSettlementSystemService>();
            services.AddScoped<ICorrespondentAccountService, CorrespondentAccountService>();
            services.AddScoped<ITechnicalDocumentService, TechnicalDocumentService>();
            services.AddScoped<ISoftwareService, SoftwareService>();
            services.AddScoped<IInstantPaymentSystemService, InstantPaymentSystemService>();
            services.AddScoped<IInstantPaymentPostService, InstantPaymentPostService>();
            services.AddScoped<IInstantPaymentOrganizationService, InstantPaymentOrganizationService>();
            services.AddScoped<IInstantPaymentFAQService, InstantPaymentFAQService>();
            services.AddScoped<IRegulationService, RegulationService>();
            services.AddScoped<ITariffService, TariffService>();
            services.AddScoped<IParticipantCategoryService, ParticipantCategoryService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IPaymentSystemStandartService, PaymentSystemStandartService>();
            services.AddScoped<IPaymentSystemStandartFileService, PaymentSystemStandartFileService>();
            services.AddScoped<IPaymentSystemStandartFAQService, PaymentSystemStandartFAQService>();
            services.AddScoped<IStateProgramCaptionService, StateProgramCaptionService>();
            services.AddScoped<IStateProgramCategoryService, StateProgramCategoryService>();
            services.AddScoped<IStateProgramService, StateProgramService>();
            services.AddScoped<IDigitalPaymentService, DigitalPaymentService>();
            services.AddScoped<INominationCaptionService, NominationCaptionService>();
            services.AddScoped<INominationCategoryService, NominationCategoryService>();
            services.AddScoped<INominationService, NominationService>();
            services.AddScoped<ILotteryService, LotteryService>();
            services.AddScoped<ILotteryFileService, LotteryFileService>();
            services.AddScoped<ILotteryVideoService, LotteryVideoService>();
            services.AddScoped<ILotteryFAQService, LotteryFAQService>();
            services.AddScoped<IPaymentSystemControlFileService, PaymentSystemControlFileService>();
            services.AddScoped<IPaymentSystemControlService, PaymentSystemControlService>();
            services.AddScoped<IPaymentSystemControlServiceService, PaymentSystemControlServiceService>();
            services.AddScoped<IDigitalPaymentReviewService, DigitalPaymentReviewService>();
            services.AddScoped<IRealTimeSettlementSystemCaptionService, RealTimeSettlementSystemCaptionService>();
            services.AddScoped<IRealTimeSettlementSystemFileService, RealTimeSettlementSystemFileService>();
            services.AddScoped<IClearingSettlementSystemCaptionService, ClearingSettlementSystemCaptionService>();
            services.AddScoped<IClearingSettlementSystemService, ClearingSettlementSystemService>();
            services.AddScoped<IClearingSettlementSystemFileService, ClearingSettlementSystemFileService>();
            services.AddScoped<IInterbankCardCenterService, InterbankCardCenterService>();
            services.AddScoped<IGovernmentPaymentPortalService, GovernmentPaymentPortalService>();
            services.AddScoped<IProcessingActivityService, ProcessingActivityService>();
            services.AddScoped<IDigitalPaymentInfograhicCategoryService, DigitalPaymentInfograhicCategoryService>();
            services.AddScoped<IDigitalPaymentInfograhicService, DigitalPaymentInfograhicService>();
            services.AddScoped<IDigitalPaymentInfograhicItemService, DigitalPaymentInfograhicItemService>();
            services.AddScoped<IFinancialStabilityCaptionService, FinancialStabilityCaptionService>();
            services.AddScoped<IFinancialStabilityReportCaptionService, FinancialStabilityReportCaptionService>();
            services.AddScoped<IFinancialStabilityReportService, FinancialStabilityReportService>();
            services.AddScoped<IMacroprudentialPolicyFrameworkCaptionService, MacroprudentialPolicyFrameworkCaptionService>();
            services.AddScoped<IMacroprudentialPolicyFrameworkService, MacroprudentialPolicyFrameworkService>();
            services.AddScoped<IFinancialInstitutionService, FinancialInstitutionService>();
            services.AddScoped<IFinancingActivityCaptionService, FinancingActivityCaptionService>();
            services.AddScoped<IFinancingActivityService, FinancingActivityService>();
            services.AddScoped<IRoadmapSustainableFinanceService, RoadmapSustainableFinanceService>();
            services.AddScoped<IGreenTaxonomyService, GreenTaxonomyService>();
            services.AddScoped<IRegulationControlService, RegulationControlService>();
            services.AddScoped<IDisclosureService, DisclosureService>();
            services.AddScoped<IInternationalCooperationInitiativeService, InternationalCooperationInitiativeService>();
            services.AddScoped<IFinancialEventService, FinancialEventService>();
            services.AddScoped<IInternationalCooperationCaptionService, InternationalCooperationCaptionService>();
            services.AddScoped<IInternationalFinancialOrganizationService, InternationalFinancialOrganizationService>();
            services.AddScoped<ICentralBankCooperationCaptionService, CentralBankCooperationCaptionService>();
            services.AddScoped<ICentralBankCooperationService, CentralBankCooperationService>();
            services.AddScoped<IInternationalEventService, InternationalEventService>();
            services.AddScoped<IMembershipInternationalOrganizationService, MembershipInternationalOrganizationService>();
            services.AddScoped<IMethodologyExplainService, MethodologyExplainService>();
            services.AddScoped<IOpenBankingService, OpenBankingService>();
            services.AddScoped<IOpenBankingFileService, OpenBankingFileService>();
            services.AddScoped<ICustomEditingModeService, CustomEditingModeService>();
            services.AddScoped<IEKYCService, EKYCService>();
            services.AddScoped<IVirtualActiveService, VirtualActiveService>();
            services.AddScoped<ICBDCService, CBDCService>();
            services.AddScoped<IFinancialLiteracyCaptionService, FinancialLiteracyCaptionService>();
            services.AddScoped<IFinancialLiteracyEventCaptionService, FinancialLiteracyEventCaptionService>();
            services.AddScoped<IFinancialLiteracyPortalCaptionService, FinancialLiteracyPortalCaptionService>();
            services.AddScoped<IVirtualEducationCaptionService, VirtualEducationCaptionService>();
            services.AddScoped<IFinancialSearchSystemCaptionService, FinancialSearchSystemCaptionService>();
            services.AddScoped<IFinancialLiteracyEventService, FinancialLiteracyEventService>();
            services.AddScoped<IFinancialLiteracyPortalService, FinancialLiteracyPortalService>();
            services.AddScoped<IVirtualEducationService, VirtualEducationService>();
            services.AddScoped<IFinancialSearchSystemService, FinancialSearchSystemService>();
            services.AddScoped<IEconometricModelService, EconometricModelService>();
            services.AddScoped<IEconometricModelFileService, EconometricModelFileService>();
            services.AddScoped<IStaffArticleCaptionService, StaffArticleCaptionService>();
            services.AddScoped<IStaffArticleService, StaffArticleService>();
            services.AddScoped<IStaffArticleFileService, StaffArticleFileService>();
            services.AddScoped<IAnnualReportService, AnnualReportService>();
            services.AddScoped<IFinancialFlowService, FinancialFlowService>();
            services.AddScoped<IMicrofinanceModelService, MicrofinanceModelService>();
            services.AddScoped<ICybersecurityStrategyService, CybersecurityStrategyService>();
            services.AddScoped<IPolicyConceptService, PolicyConceptService>();
            services.AddScoped<IRoadmapOverviewService, RoadmapOverviewService>();
            services.AddScoped<IMeasAboutService, MeasAboutService>();
            services.AddScoped<IMeasFileService, MeasFileService>();
            services.AddScoped<IIssuerTypeService, IssuerTypeService>();
            services.AddScoped<IInformationTypeService, InformationTypeService>();
            services.AddScoped<ISecurityTypeService, SecurityTypeService>();
            services.AddScoped<IMeasService, MeasService>();
            services.AddScoped<IMacroEconomyService, MacroEconomyService>();
            services.AddScoped<IMonetaryStatisticService, MonetaryStatisticService>();
            services.AddScoped<IExternalSectionService, ExternalSectionService>();
            services.AddScoped<IPaymentStatisticService, PaymentStatisticService>();
            services.AddScoped<IStatisticalReportCategoryService, StatisticalReportCategoryService>();
            services.AddScoped<IStatisticalReportSubCategoryService, StatisticalReportSubCategoryService>();
            services.AddScoped<INSDPService, NSDPService>();
            services.AddScoped<ICreditInstitutionCategoryService, CreditInstitutionCategoryService>();
            services.AddScoped<ICreditInstitutionSubCategoryService, CreditInstitutionSubCategoryService>();
            services.AddScoped<ICreditInstitutionService, CreditInstitutionService>();
            services.AddScoped<ICapitalMarketCategoryService, CapitalMarketCategoryService>();
            services.AddScoped<ICapitalMarketService, CapitalMarketService>();
            services.AddScoped<ICapitalMarketFileService, CapitalMarketFileService>();
            services.AddScoped<ILegalBasisService, LegalBasisService>();
            services.AddScoped<ICreditInstitutionLawService, CreditInstitutionLawService>();
            services.AddScoped<ICreditInstitutionPresidentActService, CreditInstitutionPresidentActService>();
            services.AddScoped<ICreditInstitutionMinisterActService, CreditInstitutionMinisterActService>();
            services.AddScoped<ICreditInstitutionRightService, CreditInstitutionRightService>();
            services.AddScoped<IPaymentLawService, PaymentLawService>();
            services.AddScoped<IPaymentRightService, PaymentRightService>();
            services.AddScoped<ICapitalMarketLawService, CapitalMarketLawService>();
            services.AddScoped<ICapitalMarketPresidentActService, CapitalMarketPresidentActService>();
            services.AddScoped<ICapitalMarketMinisterActService, CapitalMarketMinisterActService>();
            services.AddScoped<ICapitalMarketRightService, CapitalMarketRightService>();
            services.AddScoped<IInsurerLawService, InsurerLawService>();
            services.AddScoped<IInsurerPresidentActService, InsurerPresidentActService>();
            services.AddScoped<IInsurerMinisterActService, InsurerMinisterActService>();
            services.AddScoped<IInsurerRightService, InsurerRightService>();
            services.AddScoped<ICurrencyRegulationLawService, CurrencyRegulationLawService>();
            services.AddScoped<ICurrencyRegulationRightService, CurrencyRegulationRightService>();
            services.AddScoped<ICurrencyRegulationRightCaptionService, CurrencyRegulationRightCaptionService>();
            services.AddScoped<ILegalActStatisticService, LegalActStatisticService>();
            services.AddScoped<ILegalActMethodologyService, LegalActMethodologyService>();
            services.AddScoped<IOtherLawService, OtherLawService>();
            services.AddScoped<IOtherPresidentActService, OtherPresidentActService>();
            services.AddScoped<IOtherMinisterActService, OtherMinisterActService>();
            services.AddScoped<IOtherRightService, OtherRightService>();
            services.AddScoped<ICreditInstitutionRightCaptionService, CreditInstitutionRightCaptionService>();
            services.AddScoped<ICapitalMarketRightCaptionService, CapitalMarketRightCaptionService>();
            services.AddScoped<IInsurerRightCaptionService, InsurerRightCaptionService>();
            services.AddScoped<IOtherRightCaptionService, OtherRightCaptionService>();
            services.AddScoped<INonBankCaptionService, NonBankCaptionService>();
            services.AddScoped<INonBankService, NonBankService>();
            services.AddScoped<INonBankFileService, NonBankFileService>();
            services.AddScoped<IInformationIssuerCaptionService, InformationIssuerCaptionService>();
            services.AddScoped<IInformationIssuerService, InformationIssuerService>();
            services.AddScoped<IStockExchangeCaptionService, StockExchangeCaptionService>();
            services.AddScoped<IStockExchangeService, StockExchangeService>();
            services.AddScoped<IStockExchangeFileService, StockExchangeFileService>();
            services.AddScoped<IDepositorySystemService, DepositorySystemService>();
            services.AddScoped<IInvestmentCompanyCaptionService, InvestmentCompanyCaptionService>();
            services.AddScoped<IInvestmentCompanyService, InvestmentCompanyService>();
            services.AddScoped<IInvestmentCompanyFileService, InvestmentCompanyFileService>();
            services.AddScoped<IBankInvestmentCaptionService, BankInvestmentCaptionService>();
            services.AddScoped<IBankInvestmentService, BankInvestmentService>();
            services.AddScoped<IBankInvestmentFileService, BankInvestmentFileService>();
            services.AddScoped<IClearingHouseService, ClearingHouseService>();
            services.AddScoped<IInvestmentFundCaptionService, InvestmentFundCaptionService>();
            services.AddScoped<IInvestmentFundService, InvestmentFundService>();
            services.AddScoped<IInvestmentFundFileService, InvestmentFundFileService>();
            services.AddScoped<IQualificationCertificateCaptionService, QualificationCertificateCaptionService>();
            services.AddScoped<IQualificationCertificateService, QualificationCertificateService>();
            services.AddScoped<IInformationMemorandumCaptionService, InformationMemorandumCaptionService>();
            services.AddScoped<IInformationMemorandumService, InformationMemorandumService>();
            services.AddScoped<ILicensingProcessCaptionService, LicensingProcessCaptionService>();
            services.AddScoped<ILicensingProcessService, LicensingProcessService>();
            services.AddScoped<IInsuranceBrokerCaptionService, InsuranceBrokerCaptionService>();
            services.AddScoped<IInsuranceBrokerService, InsuranceBrokerService>();
            services.AddScoped<IForeignInsuranceBrokerCaptionService, ForeignInsuranceBrokerCaptionService>();
            services.AddScoped<IForeignInsuranceBrokerService, ForeignInsuranceBrokerService>();
            services.AddScoped<ILossAdjusterCaptionService, LossAdjusterCaptionService>();
            services.AddScoped<ILossAdjusterService, LossAdjusterService>();
            services.AddScoped<IActuaryCaptionService, ActuaryCaptionService>();
            services.AddScoped<IActuaryService, ActuaryService>();
            services.AddScoped<IAttestationService, AttestationService>();
            services.AddScoped<IAttestationFileService, AttestationFileService>();
            services.AddScoped<IElectronicMoneyInstitutionCaptionService, ElectronicMoneyInstitutionCaptionService>();
            services.AddScoped<IElectronicMoneyInstitutionService, ElectronicMoneyInstitutionService>();
            services.AddScoped<IElectronicMoneyInstitutionFileService, ElectronicMoneyInstitutionFileService>();
            services.AddScoped<IPaymentInstitutionCaptionService, PaymentInstitutionCaptionService>();
            services.AddScoped<IPaymentInstitutionService, PaymentInstitutionService>();
            services.AddScoped<IPaymentInstitutionFileService, PaymentInstitutionFileService>();
            services.AddScoped<IPaymentSystemOperationCaptionService, PaymentSystemOperationCaptionService>();
            services.AddScoped<IPaymentSystemOperationService, PaymentSystemOperationService>();
            services.AddScoped<IPaymentSystemOperationFileService, PaymentSystemOperationFileService>();
            services.AddScoped<IPostalCommunicationCaptionService, PostalCommunicationCaptionService>();
            services.AddScoped<IPostalCommunicationService, PostalCommunicationService>();
            services.AddScoped<ICurrencyExchangeCaptionService, CurrencyExchangeCaptionService>();
            services.AddScoped<ICurrencyExchangeService, CurrencyExchangeService>();
            services.AddScoped<IInsuranceStatisticCategoryService, InsuranceStatisticCategoryService>();
            services.AddScoped<IInsuranceStatisticSubCategoryService, InsuranceStatisticSubCategoryService>();
            services.AddScoped<IInsuranceStatisticService, InsuranceStatisticService>();
            services.AddScoped<IFaqCategoryService, FaqCategoryService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IFaqVideoService, FaqVideoService>();
            services.AddScoped<IInfographicDisclosureCategoryService, InfographicDisclosureCategoryService>();
            services.AddScoped<IInfographicDisclosureFrequencyService, InfographicDisclosureFrequencyService>();
            services.AddScoped<IInfographicDisclosureService, InfographicDisclosureService>();
            services.AddScoped<IInfographicDisclosureGraphicService, InfographicDisclosureGraphicService>();
            services.AddScoped<ICurrencyCaptionService, CurrencyCaptionService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IPageDetailService, PageDetailService>();
            services.AddScoped<INationalBankNoteCaptionService, NationalBankNoteCaptionService>();
            services.AddScoped<IPaperMoneyService, PaperMoneyService>();
            services.AddScoped<IMetalMoneyService, MetalMoneyService>();
            services.AddScoped<INationalBankNoteMoneySignService, NationalBankNoteMoneySignService>();
            services.AddScoped<IMoneySignHistoryService, MoneySignHistoryService>();
            services.AddScoped<IMoneySignHistoryFeatureService, MoneySignHistoryFeatureService>();
            services.AddScoped<IMoneySignCharacteristicService, MoneySignCharacteristicService>();
            services.AddScoped<IMoneySignCharacteristicImageService, MoneySignCharacteristicImageService>();
            services.AddScoped<IMoneySignProtectionCaptionService, MoneySignProtectionCaptionService>();
            services.AddScoped<IMoneySignProtectionService, MoneySignProtectionService>();
            services.AddScoped<IMoneySignProtectionElementService, MoneySignProtectionElementService>();
            services.AddScoped<ICoinMoneySignService, CoinMoneySignService>();
            services.AddScoped<ICoinMoneySignHistoryService, CoinMoneySignHistoryService>();
            services.AddScoped<ICoinMoneySignHistoryFeatureService, CoinMoneySignHistoryFeatureService>();
            services.AddScoped<ICoinMoneySignCharacteristicService, CoinMoneySignCharacteristicService>();
            services.AddScoped<ICoinMoneySignCharacteristicImageService, CoinMoneySignCharacteristicImageService>();
            services.AddScoped<IOutOfCirculationCaptionService, OutOfCirculationCaptionService>();
            services.AddScoped<IOutOfCirculationCategoryService, OutOfCirculationCategoryService>();
            services.AddScoped<IOutOfCoinMoneySignService, OutOfCoinMoneySignService>();
            services.AddScoped<IOutOfCoinMoneySignHistoryService, OutOfCoinMoneySignHistoryService>();
            services.AddScoped<IOutOfCoinMoneySignHistoryFeatureService, OutOfCoinMoneySignHistoryFeatureService>();
            services.AddScoped<IOutOfCoinMoneySignCharacteristicService, OutOfCoinMoneySignCharacteristicService>();
            services.AddScoped<IOutOfCoinMoneySignCharacteristicImageService, OutOfCoinMoneySignCharacteristicImageService>();
            services.AddScoped<IOutOfBankNoteMoneySignService, OutOfBankNoteMoneySignService>();
            services.AddScoped<IOutOfBankNoteMoneySignHistoryService, OutOfBankNoteMoneySignHistoryService>();
            services.AddScoped<IOutOfBankNoteMoneySignHistoryFeatureService, OutOfBankNoteMoneySignHistoryFeatureService>();
            services.AddScoped<IOutOfBankNoteMoneySignCharacteristicService, OutOfBankNoteMoneySignCharacteristicService>();
            services.AddScoped<IOutOfBankNoteMoneySignCharacteristicImageService, OutOfBankNoteMoneySignCharacteristicImageService>();
            services.AddScoped<ICurrencyHistoryService, CurrencyHistoryService>();
            services.AddScoped<ICurrencyHistoryPrevService, CurrencyHistoryPrevService>();
            services.AddScoped<ICurrencyHistoryPrevItemService, CurrencyHistoryPrevItemService>();
            services.AddScoped<ICurrencyHistoryPrevItemCharacteristicService, CurrencyHistoryPrevItemCharacteristicService>();
            services.AddScoped<ICurrencyHistoryPrevItemCharacteristicImageService, CurrencyHistoryPrevItemCharacteristicImageService>();
            services.AddScoped<ICurrencyHistoryNextService, CurrencyHistoryNextService>();
            services.AddScoped<ICurrencyHistoryNextItemService, CurrencyHistoryNextItemService>();
            #endregion

            return services;
        }
    }
}
