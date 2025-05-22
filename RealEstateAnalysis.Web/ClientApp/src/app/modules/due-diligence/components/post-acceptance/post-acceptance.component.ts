import { Component } from '@angular/core';
import { MaintenanceRecordsComponent } from '../../modals/post-acceptance/maintenance-records/maintenance-records.component';
import { EnvironmentalReportsComponent } from '../../modals/post-acceptance/environmental-reports/environmental-reports.component';
import { SeismicReportComponent } from '../../modals/post-acceptance/seismic-report/seismic-report.component';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { PropertyConditionAssessmentReportComponent } from '../../modals/post-acceptance/property-condition-assessment-report/property-condition-assessment-report.component';
import { WalkThroughComponent } from '../../modals/post-acceptance/walk-through/walk-through.component';
import { TenantInterviewsComponent } from '../../modals/post-acceptance/tenant-interviews/tenant-interviews.component';
import { TitleInspectionComponent } from '../../modals/post-acceptance/title-inspection/title-inspection.component';
import { ModalOptions } from 'src/app/shared/dtos/modalOptions';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';
import { BuildingMeasurementVerificationComponent } from '../../modals/post-acceptance/building-measurement-verification/building-measurement-verification.component';
import { SewerScopeInspectionComponent } from '../../modals/post-acceptance/sewer-scope-inspection/sewer-scope-inspection.component';
import { AsbestosInspectionComponent } from '../../modals/post-acceptance/asbestos-inspection/asbestos-inspection.component';
import { LeadBasedPaintInspectionComponent } from '../../modals/post-acceptance/lead-based-paint-inspection/lead-based-paint-inspection.component';
import { WoodDestroyingOrganismsInspectionComponent } from '../../modals/post-acceptance/wood-destroying-organisms-inspection/wood-destroying-organisms-inspection.component';
import { DueDiligenceResourcesAndCompaniesComponent } from '../../modals/post-acceptance/due-diligence-resources-and-companies/due-diligence-resources-and-companies.component';
import { OtherPhysicalInspectionItemsComponent } from '../../modals/post-acceptance/other-physical-inspection-items/other-physical-inspection-items.component';
import { PurposeOfEstimatingRepairCostComponent } from '../../modals/post-acceptance/purpose-of-estimating-repair-cost/purpose-of-estimating-repair-cost.component';
import { HeadingTowardsFinishLineComponent } from '../../modals/post-acceptance/heading-towards-finish-line/heading-towards-finish-line.component';
import { LenderRequiredPropertyAppraisalComponent } from '../../modals/post-acceptance/lender-required-property-appraisal/lender-required-property-appraisal.component';
import { ClosingCreditsComponent } from '../../modals/post-acceptance/closing-credits/closing-credits.component';
import { ClosingDocumentsComponent } from '../../modals/post-acceptance/closing-documents/closing-documents.component';
import { FinalWalkThroughComponent } from '../../modals/post-acceptance/final-walk-through/final-walk-through.component';
import { PropertyTaxAppealsComponent } from '../../modals/post-acceptance/property-tax-appeals/property-tax-appeals.component';
import { FollowUpComponent } from '../../modals/post-acceptance/follow-up/follow-up.component';
import { ImprovementsComponent } from '../../modals/post-acceptance/improvements/improvements.component';
import { PropertySurveyComponent } from '../../modals/post-acceptance/property-survey/property-survey.component';
import { PhysicalInspectionComponent } from '../../modals/post-acceptance/physical-inspection/physical-inspection.component';

@Component({
    templateUrl: './post-acceptance.component.html',
    styleUrls: ['./post-acceptance.component.css']
})
export class PostAcceptanceComponent {
    constructor(titleService: Title,
        private modalService: ModalService
    ) {
        titleService.setTitle(`${appConfig.businessName}: Post-Acceptance: The PSA is Executed`);
    }

    private modalOptions = { sizeClass: 'modal-xl' } as ModalOptions;

    onMaintenanceRecordsClick() {
        this.modalService.show(MaintenanceRecordsComponent, this.modalOptions);
    }

    onEnvironmentalReportsClick() {
        this.modalService.show(EnvironmentalReportsComponent, this.modalOptions);
    }

    onPropertyConditionAssessmentReportClick() {
        this.modalService.show(PropertyConditionAssessmentReportComponent, this.modalOptions);
    }

    onSeismicReportClick() {
        this.modalService.show(SeismicReportComponent, this.modalOptions);
    }

    onWalkThroughClick() {
        this.modalService.show(WalkThroughComponent, this.modalOptions);
    }

    onTenantInterviewsClick() {
        this.modalService.show(TenantInterviewsComponent, this.modalOptions);
    }

    onTitleInspectionClick() {
        this.modalService.show(TitleInspectionComponent, this.modalOptions);
    }

    onBuildingMeasurementVerification() {
        this.modalService.show(BuildingMeasurementVerificationComponent, this.modalOptions);
    }

    onSewerScopeInspectionClick() {
        this.modalService.show(SewerScopeInspectionComponent, this.modalOptions);
    }

    onAsbestosInspectionClick() {
        this.modalService.show(AsbestosInspectionComponent, this.modalOptions);
    }

    onLeadBasedPaintInspectionClick() {
        this.modalService.show(LeadBasedPaintInspectionComponent, this.modalOptions);
    }

    onWoodDestroyingOrganismsInspectionClick() {
        this.modalService.show(WoodDestroyingOrganismsInspectionComponent, this.modalOptions);
    }

    onDueDiligenceResourcesAndCompaniesClick() {
        this.modalService.show(DueDiligenceResourcesAndCompaniesComponent, this.modalOptions);
    }

    onOtherPhysicalInspectionItemsClick() {
        this.modalService.show(OtherPhysicalInspectionItemsComponent, this.modalOptions);
    }

    onPurposeOfEstimatingRepairCostClick() {
        this.modalService.show(PurposeOfEstimatingRepairCostComponent, this.modalOptions);
    }

    onHeadingTowardsFinishLineClick() {
        this.modalService.show(HeadingTowardsFinishLineComponent, this.modalOptions);
    }

    onLenderRequiredPropertyAppraisalClick() {
        this.modalService.show(LenderRequiredPropertyAppraisalComponent, this.modalOptions);
    }

    onClosingCreditsClick() {
        this.modalService.show(ClosingCreditsComponent, this.modalOptions);
    }

    onClosingDocumentsClick() {
        this.modalService.show(ClosingDocumentsComponent, this.modalOptions);
    }

    onFinalWalkThroughClick() {
        this.modalService.show(FinalWalkThroughComponent, this.modalOptions);
    }

    onPropertyTaxAppealsClick() {
        this.modalService.show(PropertyTaxAppealsComponent, this.modalOptions);
    }

    onFollowUpClick() {
        this.modalService.show(FollowUpComponent, this.modalOptions);
    }

    onImprovementsClick() {
        this.modalService.show(ImprovementsComponent, this.modalOptions);
    }

    onPropertySurveyClick() {
        this.modalService.show(PropertySurveyComponent, this.modalOptions);
    }

    onPhysicalInspectionProcessClick() {
        this.modalService.show(PhysicalInspectionComponent, this.modalOptions);
    }
}
