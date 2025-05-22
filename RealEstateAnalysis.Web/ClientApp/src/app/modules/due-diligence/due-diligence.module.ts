import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreOfferComponent } from './components/pre-offer/pre-offer.component';
import { PostAcceptanceComponent } from './components/post-acceptance/post-acceptance.component';
import { DueDiligenceRoutingModule } from './due-diligence-routing.module';
import { LeasesComponent } from './modals/pre-offer/leases/leases.component';
import { RentRollComponent } from './modals/pre-offer/rent-roll/rent-roll.component';
import { ServiceContractsComponent } from './modals/pre-offer/service-contracts/service-contracts.component';
import { InventoriesComponent } from './modals/pre-offer/inventories/inventories.component';
import { SellerDisclosureStatementComponent } from './modals/pre-offer/seller-disclosure-statement/seller-disclosure-statement.component';
import { MaintenanceRecordsComponent } from './modals/post-acceptance/maintenance-records/maintenance-records.component';
import { EnvironmentalReportsComponent } from './modals/post-acceptance/environmental-reports/environmental-reports.component';
import { SeismicReportComponent } from './modals/post-acceptance/seismic-report/seismic-report.component';
import { OtherReportsComponent } from './modals/pre-offer/other-reports/other-reports.component';
import { OperatingStatementsComponent } from './modals/pre-offer/operating-statements/operating-statements.component';
import { WalkThroughComponent } from './modals/post-acceptance/walk-through/walk-through.component';
import { PropertyConditionAssessmentReportComponent } from './modals/post-acceptance/property-condition-assessment-report/property-condition-assessment-report.component';
import { UtilitiesStatementsComponent } from './modals/pre-offer/utilities-statements/utilities-statements.component';
import { TenantInterviewsComponent } from './modals/post-acceptance/tenant-interviews/tenant-interviews.component';
import { PurchaseAndSaleAgreementComponent } from './components/purchase-and-sale-agreement/purchase-and-sale-agreement.component';
import { TitleInspectionComponent } from './modals/post-acceptance/title-inspection/title-inspection.component';
import { SellerTaxReturnComponent } from './modals/pre-offer/seller-tax-return/seller-tax-return.component';
import { HoaDocumentsComponent } from './modals/pre-offer/hoa-documents/hoa-documents.component';
import { AsBuiltDrawingsComponent } from './modals/pre-offer/as-built-drawings/as-built-drawings.component';
import { OperatingExpensesReportComponent } from './modals/pre-offer/operating-expenses-report/operating-expenses-report.component';
import { WarrantiesComponent } from './modals/pre-offer/warranties/warranties.component';
import { OperatingBudgetComponent } from './modals/pre-offer/operating-budget/operating-budget.component';
import { CertificateOfOccupancyComponent } from './modals/pre-offer/certificate-of-occupancy/certificate-of-occupancy.component';
import { BuildingMeasurementVerificationComponent } from './modals/post-acceptance/building-measurement-verification/building-measurement-verification.component';
import { SewerScopeInspectionComponent } from './modals/post-acceptance/sewer-scope-inspection/sewer-scope-inspection.component';
import { AsbestosInspectionComponent } from './modals/post-acceptance/asbestos-inspection/asbestos-inspection.component';
import { LeadBasedPaintInspectionComponent } from './modals/post-acceptance/lead-based-paint-inspection/lead-based-paint-inspection.component';
import { WoodDestroyingOrganismsInspectionComponent } from './modals/post-acceptance/wood-destroying-organisms-inspection/wood-destroying-organisms-inspection.component';
import { DueDiligenceResourcesAndCompaniesComponent } from './modals/post-acceptance/due-diligence-resources-and-companies/due-diligence-resources-and-companies.component';
import { OtherPhysicalInspectionItemsComponent } from './modals/post-acceptance/other-physical-inspection-items/other-physical-inspection-items.component';
import { PurposeOfEstimatingRepairCostComponent } from './modals/post-acceptance/purpose-of-estimating-repair-cost/purpose-of-estimating-repair-cost.component';
import { PropertyTaxAppealsComponent } from './modals/post-acceptance/property-tax-appeals/property-tax-appeals.component';
import { HeadingTowardsFinishLineComponent } from './modals/post-acceptance/heading-towards-finish-line/heading-towards-finish-line.component';
import { LenderRequiredPropertyAppraisalComponent } from './modals/post-acceptance/lender-required-property-appraisal/lender-required-property-appraisal.component';
import { ClosingCreditsComponent } from './modals/post-acceptance/closing-credits/closing-credits.component';
import { ClosingDocumentsComponent } from './modals/post-acceptance/closing-documents/closing-documents.component';
import { FinalWalkThroughComponent } from './modals/post-acceptance/final-walk-through/final-walk-through.component';
import { FollowUpComponent } from './modals/post-acceptance/follow-up/follow-up.component';
import { TenantBackgroundCheckReportsComponent } from './modals/pre-offer/tenant-background-check-reports/tenant-background-check-reports.component';
import { ImprovementsComponent } from './modals/post-acceptance/improvements/improvements.component';
import { WhyIsOwnerSellingComponent } from './modals/pre-offer/why-is-owner-selling/why-is-owner-selling.component';
import { BuildingAndFireCodeViolationsComponent } from './modals/pre-offer/building-and-fire-code-violations/building-and-fire-code-violations.component';
import { RecordsOfAllMaintenanceComponent } from './modals/pre-offer/records-of-all-maintenance/records-of-all-maintenance.component';
import { ContingenciesComponent } from './modals/purchase-and-sale-agreement/contingencies/contingencies.component';
import { PropertySurveyComponent } from './modals/post-acceptance/property-survey/property-survey.component';
import { EncroachmentComponent } from './modals/post-acceptance/encroachment/encroachment.component';
import { NonconformingUseComponent } from './modals/pre-offer/nonconforming-use/nonconforming-use.component';
import { RightOfFirstRefusalOrOptionComponent } from './modals/pre-offer/right-of-first-refusal-or-option/right-of-first-refusal-or-option.component';
import { CapitalExpendituresComponent } from './modals/pre-offer/capital-expenditures/capital-expenditures.component';
import { TenantEstoppelCertificatesComponent } from './modals/pre-offer/tenant-estoppel-certificates/tenant-estoppel-certificates.component';
import { PhysicalInspectionComponent } from './modals/post-acceptance/physical-inspection/physical-inspection.component';


@NgModule({
    declarations: [
        PreOfferComponent,
        PostAcceptanceComponent,
        LeasesComponent,
        RentRollComponent,
        ServiceContractsComponent,
        InventoriesComponent,
        SellerDisclosureStatementComponent,
        MaintenanceRecordsComponent,
        EnvironmentalReportsComponent,
        SeismicReportComponent,
        OtherReportsComponent,
        OperatingStatementsComponent,
        WalkThroughComponent,
        PropertyConditionAssessmentReportComponent,
        UtilitiesStatementsComponent,
        TenantInterviewsComponent,
        PurchaseAndSaleAgreementComponent,
        TitleInspectionComponent,
        SellerTaxReturnComponent,
        HoaDocumentsComponent,
        AsBuiltDrawingsComponent,
        OperatingExpensesReportComponent,
        WarrantiesComponent,
        OperatingBudgetComponent,
        CertificateOfOccupancyComponent,
        BuildingMeasurementVerificationComponent,
        SewerScopeInspectionComponent,
        AsbestosInspectionComponent,
        LeadBasedPaintInspectionComponent,
        WoodDestroyingOrganismsInspectionComponent,
        DueDiligenceResourcesAndCompaniesComponent,
        OtherPhysicalInspectionItemsComponent,
        PurposeOfEstimatingRepairCostComponent,
        PropertyTaxAppealsComponent,
        HeadingTowardsFinishLineComponent,
        LenderRequiredPropertyAppraisalComponent,
        ClosingCreditsComponent,
        ClosingDocumentsComponent,
        FinalWalkThroughComponent,
        FollowUpComponent,
        TenantBackgroundCheckReportsComponent,
        ImprovementsComponent,
        WhyIsOwnerSellingComponent,
        BuildingAndFireCodeViolationsComponent,
        RecordsOfAllMaintenanceComponent,
        ContingenciesComponent,
        PropertySurveyComponent,
        EncroachmentComponent,
        NonconformingUseComponent,
        RightOfFirstRefusalOrOptionComponent,
        CapitalExpendituresComponent,
        TenantEstoppelCertificatesComponent,
        PhysicalInspectionComponent
    ],
    imports: [
        CommonModule,
        DueDiligenceRoutingModule
    ]
})
export class DueDiligenceModule { }
