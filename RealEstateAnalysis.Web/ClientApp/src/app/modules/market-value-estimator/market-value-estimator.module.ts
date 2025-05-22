import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorSummaryModule } from 'src/app/shared/directives/error-summary/error-summary.module';
import { ConfirmModule } from 'src/app/shared/modals/confirm/confirm.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RentalPropertiesModule } from '../rental-properties/rental-properties.module';
import { TooltipModule } from 'src/app/shared/directives/tooltip/tooltip.module';
import { InputMaskModule } from 'src/app/shared/directives/input-mask/input-mask.module';
import { LookupService } from 'src/app/shared/services/lookup.service';
import { MarketValueEstimatorRoutingModule } from './market-value-estimator-routing.module';
import { MarketValueEstimatorComponent } from './components/market-value-estimator.component';


@NgModule({
    declarations: [
        MarketValueEstimatorComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MarketValueEstimatorRoutingModule,
        ErrorSummaryModule,
        ConfirmModule,
        RentalPropertiesModule,
        TooltipModule,
        InputMaskModule
    ],
    providers: [
        LookupService
    ]
})
export class MarketValueEstimatorModule { }
