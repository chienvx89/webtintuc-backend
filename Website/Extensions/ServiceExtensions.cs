using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using NLog;
using System.Reflection.Metadata;
using Website.Application.Dtos;
using Website.Domain.Entities;
using Website.Infrastructure.IRepositories;
using Website.Infrastructure.Logs;
using Website.Infrastructure.Repositories;

namespace Website.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigDI(this IServiceCollection services)

        {

            #region DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IUnitOfWorkBase<>), typeof(UnitOfWorkBase<>));
            services.AddScoped(typeof(IRepository<,,>), typeof(RepositoryBase<,,>));
            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddSingleton<ILoggerManager, LoggerManager>();
            #endregion
            return services;
        }

        public static IServiceCollection ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(configAction: cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
                
                //cfg.CreateMap<string, string>().ConvertUsing<NullStringConverter>();
                //cfg.CreateMap<DateTime?, int?>().ConvertUsing(new DateTimeTypeConverter());
                //cfg.CreateMap<DateTime, int>().ConvertUsing(new DateTimeTypeConverter());
                //// Supplies
                //cfg.CreateMap<Supplies, SuppliesGetMap>();
                ////Finishedgoods
                //cfg.CreateMap<Finishedgoods, FinishedgoodsGetMap>();
                //// Product
                //cfg.CreateMap<Product, ProductGetMap>();
                //// Material
                //cfg.CreateMap<Material, MaterialGetMap>();
                //// ConvertUnit
                //cfg.CreateMap<Convertunit, ConvertUnitGetMap>();
                //// Partner
                //cfg.CreateMap<Partner, PartnerGetMap>();
                //// Location
                //cfg.CreateMap<Location, LocationGetMap>();
                //// PackageInfo
                //cfg.CreateMap<Packageinfo, PackageInfoGetmap>();
                //// PurchaseOrder
                //cfg.CreateMap<Purchaseorder, PurchaseOrderGetmap>();
                //// PurchaseOrder
                //cfg.CreateMap<Purchaseorderdetail, PurchaseOrderDetailGetMap>();
                ////Standardpacking
                //cfg.CreateMap<Standardpacking, StandardpackingGetMap>();
                ////Movementtype
                //cfg.CreateMap<Movementtype, MovementtypeGetMap>();
                ////Worker
                //cfg.CreateMap<WorkerRequest, Worker>();
                ////ApprovalFlowTemplate ReverseMap <-> two-way mapping
                //cfg.CreateMap<ApprovalFlowTemplate, ApprovalFlowTemplateDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowTemplate, CreateOrUpdateApprovalFlowTemplateDto>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<ApprovalFlowTemplateStep, ApprovalFlowTemplateStepDto>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<ApprovalFlowConfig, ApprovalFlowConfigDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowConfig, CreateOrUpdateApprovalFlowConfigDto>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<ApprovalFlowTemplate, SuggestionDto>();
                //cfg.CreateMap<ApprovalFlow, ApprovalFlowDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowStep, ApprovalFlowStepDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowStep, CreateApprovalFlowStepDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowComment, ApprovalFlowCommentDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowHistory, ApprovalFlowHistoryDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowComment, CreateOrUpdateApprovalCommentInput>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<Document, DocumentDto>().ReverseMap();
                //cfg.CreateMap<Document, CreateDocumentDto>().ReverseMap();
                //cfg.CreateMap<ApprovalFlowTemplateStep, ApprovalFlowStep>().ReverseMap();
                //cfg.CreateMap<ApprovalFlow, CreateApprovalFlowInput>().ReverseMap();
                //cfg.CreateMap<RequestCreateAsset, RequestCreateAssetDto>().ReverseMap();
                //cfg.CreateMap<RequestCreateAsset, CreateOrUpdateRequestCreateAssetInput>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<AssetStandard, CreateOrUpdateAssetStandardDto>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<AssetStandard, UpdateAssetDto>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<AssetStandard, AssetStandardDto>().ReverseMap();
                //cfg.CreateMap<RequestTransferAsset, CreateOrUpdateRequestTransferAssetInput>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<TransferAsset, CreateOrUpdateTransferAssetDto>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<Asset, UpdateAssetDto>(MemberList.Source).ReverseMap();
                ////cfg.CreateMap<Asset, UpdateAssetDto>().ReverseMap().IgnoreAllNonExisting();
                //cfg.CreateMap<Asset, AssetHistory>().ReverseMap();
                //cfg.CreateMap<TransferAssetConfirm, ConfirmTransferAssetInput>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<SegmentCostCenter, AddSegmentCostCenterInput>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<InventoryPlan, InventoryPlanDto>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<CreateOrUpdateInventoryPlanInput, InventoryPlan>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<InventoryPlanSessionInput, InventoryPlanSession>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<InventoryPlanSessionItemInput, InventoryPlanSessionItem>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<InventoryPlanSessionForEditDto, InventoryPlanSession>().ReverseMap();
                //cfg.CreateMap<InventoryAsset, InventoryAssetHistory>().ReverseMap();
                //cfg.CreateMap<AssetSource, AssetSourceDto>().ReverseMap();
                //cfg.CreateMap<CreateOrUpdateAssetSourceDto, AssetSource>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<UsersManual, UsersManualDto>().ReverseMap();
                //cfg.CreateMap<CreateOrUpdateUsersManualInput, UsersManual>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<SegmentFundCenter, Application.Dtos.SegmentFundCenter.SegmentFundCenterDto>().ReverseMap();
                //cfg.CreateMap<UpdateSegmentFundCenterInput, SegmentFundCenter>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<CreateUpdateInventoryUserConfigDto, InventoryUserConfig>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<UpdatePermissionDataInput, PermissionData>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<ApiLog, LogDto>().ReverseMap();
                //cfg.CreateMap<AssetReconcile, CreateOrUpdateAssetReconcileInput>().ReverseMap();
                //cfg.CreateMap<AssetReconcileAMS, AssetReconcileComapreJsonDto>().ReverseMap();
                //cfg.CreateMap<AssetReconcileSAP, AssetReconcileComapreJsonDto>().ReverseMap();
                //cfg.CreateMap<AssetReconcileAMSDto, AssetReconcileAMS>().ReverseMap();
                //cfg.CreateMap<AssetReconcileSAPDto, AssetReconcileSAP>().ReverseMap();
                //cfg.CreateMap<AssetReconcileDto, AssetReconcile>().ReverseMap();
                //cfg.CreateMap<InventoryAsset, CreateOrUpdateInventoryAssetInput>(MemberList.Source).ReverseMap();
                //cfg.CreateMap<ReportAssetNoValueDto, ReportAssetDto>(MemberList.Source).ReverseMap();
            }, AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
