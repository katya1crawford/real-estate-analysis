import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from './modules/shared/shared.module';
import { PopoverModule } from './shared/directives/popover/popover.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountModule } from './modules/account/account.module';
import { HomeModule } from './modules/home/home.module';
import { RouterModule } from '@angular/router';
import { LocationAnalysisModule } from './modules/location-analysis/location-analysis.module';
import { DueDiligenceModule } from './modules/due-diligence/due-diligence.module';
import { MarketValueEstimatorModule } from './modules/market-value-estimator/market-value-estimator.module';
import { HttpClientModule } from '@angular/common/http';
import { RentalPropertiesModule } from './modules/rental-properties/rental-properties.module';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
    LocationAnalysisModule,
    DueDiligenceModule,
    SharedModule,
    PopoverModule,
    AccountModule,
    HomeModule,
    RentalPropertiesModule,
    MarketValueEstimatorModule,
    BrowserAnimationsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
