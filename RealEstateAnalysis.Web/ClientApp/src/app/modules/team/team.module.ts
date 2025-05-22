import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamRoutingModule } from './team-routing.module';
import { TeamComponent } from './components/team/team.component';
import { InsuranceBrokerComponent } from './modals/insurance-broker/insurance-broker.component';
import { PropertyManagementCompanyComponent } from './modals/property-management-company/property-management-company.component';
import { LenderComponent } from './modals/lender/lender.component';
import { CommercialRealEstateBrokerComponent } from './modals/commercial-real-estate-broker/commercial-real-estate-broker.component';
import { RealEstateCpaComponent } from './modals/real-estate-cpa/real-estate-cpa.component';
import { RealEstateAttorneyComponent } from './modals/real-estate-attorney/real-estate-attorney.component';
import { RealEstateInteriorDesignerComponent } from './modals/real-estate-interior-designer/real-estate-interior-designer.component';


@NgModule({
    declarations: [
        TeamComponent,
        InsuranceBrokerComponent,
        PropertyManagementCompanyComponent,
        LenderComponent,
        CommercialRealEstateBrokerComponent,
        RealEstateAttorneyComponent,
        RealEstateCpaComponent,
        RealEstateInteriorDesignerComponent
    ],
    imports: [
        CommonModule,
        TeamRoutingModule
    ]
})
export class TeamModule { }
