import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { LocationAnalysisRoutingModule } from './location-analysis-routing.module';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { CdkVirtualScrollViewport } from "@angular/cdk/scrolling";
import { CitiesComponent } from './cities/cities.component';
import { NeighborhoodsComponent } from './neighborhoods/neighborhoods.component';
import { AddCityComponent } from './dialogs/add-city/add-city.component';
import { RemoveFromFavoritesComponent } from './dialogs/remove-from-favorites/remove-from-favorites.component';
import { DeleteCityComponent } from './dialogs/delete-city/delete-city.component';
import { DeleteNeighborhoodComponent } from './dialogs/delete-neighborhood/delete-neighborhood.component';
import { AddNeighborhoodComponent } from './dialogs/add-neighborhood/add-neighborhood.component';


@NgModule({
  declarations: [
    CitiesComponent,
    NeighborhoodsComponent,
    AddCityComponent,
    RemoveFromFavoritesComponent,
    DeleteCityComponent,
    DeleteNeighborhoodComponent,
    AddNeighborhoodComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    // CdkVirtualScrollViewport,
    ScrollingModule,
    LocationAnalysisRoutingModule
  ]
})
export class LocationAnalysisModule { }
