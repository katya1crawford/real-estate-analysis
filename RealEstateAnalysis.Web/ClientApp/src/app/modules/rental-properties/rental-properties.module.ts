import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorSummaryModule } from '../../shared/directives/error-summary/error-summary.module';
import { ConfirmModule } from '../../shared/modals/confirm/confirm.module';
import { RentalPropertiesRoutingModule } from './rental-properties-routing.module';
import { LookupService } from '../../shared/services/lookup.service';
import { ItemCloneService } from '../../shared/services/item-clone.service';
import { RentalPropertiesComponent } from './components/rental-properties/rental-properties.component';
import { RentalPropertyService } from './services/rental-property.service';
import { FileService } from './services/file.service';
import { AddEditPropertyComponent } from './modals/add-edit-property/add-edit-property.component';
import { CalculatorService } from './services/calculator.service';
import { InputMaskModule } from '../../shared/directives/input-mask/input-mask.module';
import { AttachmentsComponent } from './modals/attachments/attachments.component';
import { IncomeStatementComponent as IncomeStatementModalComponent } from './modals/income-statement/income-statement.component';
import { GalleryImageZoomComponent } from './modals/gallery-image-zoom/gallery-image-zoom.component';
import { RentalPropertyDetailsComponent } from './components/rental-property-details/rental-property-details.component';
import { PdfService } from './services/pdf.service';
import { GalleryImageService } from './services/gallery-image.service';
import { PropertySummaryComponent } from './components/rental-property-details/partials/property-summary/property-summary.component';
import { NotesComponent } from './components/rental-property-details/partials/notes/notes.component';
import { KpisComponent } from './components/rental-property-details/partials/kpis/kpis.component';
import { IncomeStatementComponent } from './components/rental-property-details/partials/income-statement/income-statement.component';
import { FinancialForecastComponent } from './components/rental-property-details/partials/financial-forecast/financial-forecast.component';
import { GalleryComponent } from './components/rental-property-details/partials/gallery/gallery.component';
import { PropertyGoogleMapComponent } from './directives/property-google-map.component';
import { EstimatedResaleValueComponent } from './components/rental-property-details/partials/estimated-resale-value/estimated-resale-value.component';
import { HighchartsModule } from '../../shared/directives/highcharts/highcharts.module';
import { TooltipModule } from '../../shared/directives/tooltip/tooltip.module';
import { PopoverModule } from '../../shared/directives/popover/popover.module';
import { RentRollService } from './services/rent-roll.service';
import { RentRollComponent } from './components/rent-roll/rent-roll.component';
import { ImportRentRollComponent } from './modals/import-rent-roll/import-rent-roll.component';
import { ContractRentByMonthComponent } from './components/rent-roll/partials/contract-rent-by-month/contract-rent-by-month.component';
import { LeaseCountByMonthComponent } from './components/rent-roll/partials/lease-count-by-month/lease-count-by-month.component';
import { RentsComponent } from './components/rent-roll/partials/rents/rents.component';
import { OccupancyComponent } from './components/rent-roll/partials/occupancy/occupancy.component';
import { FloorPlansComponent } from './components/rent-roll/partials/floor-plans/floor-plans.component';
import { DatatablesModule } from 'src/app/shared/directives/datatables/datatables.module';
import { RentRollDetailsComponent } from './components/rent-roll/partials/rent-roll-details/rent-roll-details.component';;
import { SummaryComponent } from './components/rent-roll/partials/summary/summary.component'

@NgModule({
    imports: [
        CommonModule,
        RentalPropertiesRoutingModule,
        ReactiveFormsModule,
        ErrorSummaryModule,
        ConfirmModule,
        InputMaskModule,
        HighchartsModule,
        TooltipModule,
        PopoverModule,
        DatatablesModule
    ],
    declarations: [
        RentalPropertiesComponent,
        AddEditPropertyComponent,
        AttachmentsComponent,
        IncomeStatementComponent,
        IncomeStatementModalComponent,
        GalleryImageZoomComponent,
        RentalPropertyDetailsComponent,
        PropertySummaryComponent,
        NotesComponent,
        KpisComponent,
        IncomeStatementComponent,
        FinancialForecastComponent,
        GalleryComponent,
        PropertyGoogleMapComponent,
        EstimatedResaleValueComponent,
        RentRollComponent,
        ImportRentRollComponent,
        ContractRentByMonthComponent,
        LeaseCountByMonthComponent,
        RentsComponent,
        OccupancyComponent,
        FloorPlansComponent
        ,
        RentRollDetailsComponent,
        SummaryComponent
    ],
    providers: [
        RentalPropertyService,
        LookupService,
        FileService,
        ItemCloneService,
        CalculatorService,
        PdfService,
        GalleryImageService,
        RentRollService
    ]
})
export class RentalPropertiesModule { }
