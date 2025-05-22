import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationAnalysisRoutingModule } from './location-analysis-routing.module';
import { CitiesComponent } from './components/cities/cities.component';
import { NeighborhoodsComponent } from './components/neighborhoods/neighborhoods.component';
import { CityService } from './services/city.service';
import { NeighborhoodService } from './services/neighborhood.service';
import { ErrorSummaryModule } from '../../shared/directives/error-summary/error-summary.module';
import { ConfirmModule } from '../../shared/modals/confirm/confirm.module';
import { ReactiveFormsModule } from '@angular/forms';
import { InputMaskModule } from '../../shared/directives/input-mask/input-mask.module';
import { LookupService } from '../../shared/services/lookup.service';
import { AddEditCityComponent } from './modals/add-edit-city/add-edit-city.component';
import { AddEditNeighborhoodComponent } from './modals/add-edit-neighborhood/add-edit-neighborhoods.component';
import { TooltipModule } from '../../shared/directives/tooltip/tooltip.module';
import { PopoverModule } from '../../shared/directives/popover/popover.module';
import { ItemCloneService } from 'src/app/shared/services/item-clone.service';
import { HarvestCityDataComponent } from './modals/harvest-city-data/harvest-city-data.component';


@NgModule({
    declarations: [
        CitiesComponent,
        NeighborhoodsComponent,
        AddEditCityComponent,
        AddEditNeighborhoodComponent,
        HarvestCityDataComponent
    ],
    imports: [
        CommonModule,
        TooltipModule,
        ErrorSummaryModule,
        ConfirmModule,
        ReactiveFormsModule,
        InputMaskModule,
        PopoverModule,
        LocationAnalysisRoutingModule
    ],
    providers: [
        CityService,
        NeighborhoodService,
        LookupService,
        ItemCloneService
    ]
})
export class LocationAnalysisModule { }
