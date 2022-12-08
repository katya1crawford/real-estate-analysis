import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { PopoverModule } from 'src/app/shared/directives/popover/popover.module';
import { GoogleMapsModule } from 'src/app/shared/directives/maps/google-maps/google-maps.module';
import { ChartsjsModule } from 'src/app/shared/directives/chartsjs/chartsjs.module';
import { RentalPropertiesRoutingModule } from './rental-properties-routing.module';
import { RentalsComponent } from './components/rentals/rentals.component';
import { RentRollComponent } from './components/rent-roll/rent-roll.component';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { KpisComponent } from './components/property-details/partials/kpis/kpis.component';
import { GalleryComponent } from './components/property-details/partials/gallery/gallery.component';
import { PropertySummaryComponent } from './components/property-details/partials/property-summary/property-summary.component';
import { NotesComponent } from './components/property-details/partials/notes/notes.component';
import { IncomeStatementComponent } from './components/property-details/partials/income-statement/income-statement.component';
import { FinancialForecastComponent } from './components/property-details/partials/financial-forecast/financial-forecast.component';
import { EstimatedResaleValueComponent } from './components/property-details/partials/estimated-resale-value/estimated-resale-value.component';
import { ContractRentByMonthComponent } from './components/rent-roll/partials/contract-rent-by-month/contract-rent-by-month.component';
import { FloorPlansComponent } from './components/rent-roll/partials/floor-plans/floor-plans.component';
import { OccupancyComponent } from './components/rent-roll/partials/occupancy/occupancy.component';
import { LeaseCountByMonthComponent } from './components/rent-roll/partials/lease-count-by-month/lease-count-by-month.component';
import { RentRollDetailsComponent } from './components/rent-roll/partials/rent-roll-details/rent-roll-details.component';
import { RentsComponent } from './components/rent-roll/partials/rents/rents.component';
import { SummaryComponent } from './components/rent-roll/partials/summary/summary.component';
import { DeleteGalleryImageComponent } from './components/property-details/dialogs/delete-gallery-image/delete-gallery-image.component';
import { ZoomGalleryImageComponent } from './components/property-details/dialogs/zoom-gallery-image/zoom-gallery-image.component';
import { DeleteRentalPropertyComponent } from './dialogs/delete-rental-property/delete-rental-property.component';
import { AttachmentsComponent } from './dialogs/attachments/attachments.component';
import { AnnualIncomeStatementComponent } from './dialogs/annual-income-statement/annual-income-statement.component';
import { TotalCashNeededDetailsComponent } from './dialogs/annual-income-statement/total-cash-needed-details/total-cash-needed-details.component';
import { AddEditNewPropertyComponent } from './components/add-edit-property/add-edit-property.component';
import { SummaryMapComponent } from './components/property-details/partials/summary-map/summary-map.component';
import { RentRollUploadComponent } from './components/rent-roll/bottom-sheet-upload/rent-roll-upload/rent-roll-upload.component';
import { ErrorSummaryModule } from 'src/app/shared/error-summary/error-summary.module';



@NgModule({
  declarations: [
    RentalsComponent,
    RentRollComponent,
    PropertyDetailsComponent,
    KpisComponent,
    GalleryComponent,
    PropertySummaryComponent,
    NotesComponent,
    IncomeStatementComponent,
    FinancialForecastComponent,
    EstimatedResaleValueComponent,
    ContractRentByMonthComponent,
    FloorPlansComponent,
    OccupancyComponent,
    LeaseCountByMonthComponent,
    RentRollDetailsComponent,
    RentsComponent,
    SummaryComponent,
    DeleteGalleryImageComponent,
    ZoomGalleryImageComponent,
    DeleteRentalPropertyComponent,
    AttachmentsComponent,
    AnnualIncomeStatementComponent,
    TotalCashNeededDetailsComponent,
    SummaryMapComponent,
    AddEditNewPropertyComponent,
    RentRollUploadComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PopoverModule,
    GoogleMapsModule,
    ErrorSummaryModule,
    ChartsjsModule,
    RentalPropertiesRoutingModule
  ]
})
export class RentalPropertiesModule { }
