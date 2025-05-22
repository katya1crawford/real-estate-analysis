import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { ContactUsModule } from './shared/modals/contact-us/contact-us.module';
import { AppComponent } from './components/app/app.component';
import { AuthorizationService } from './shared/services/authorization.service';
import { AuthGuardService } from './shared/services/auth-guard.service';
import { MessagingService } from './shared/services/messaging.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AccountModule } from './modules/account/account.module';
import { HomeModule } from './modules/home/home.module';
import { PrivacyModule } from './modules/privacy/privacy.module';
import { RentalPropertiesModule } from './modules/rental-properties/rental-properties.module';
import { TermsModule } from './modules/terms/terms.module';
import { ClassificationsModule } from './modules/classifications/classifications.module';
import { LocationAnalysisModule } from './modules/location-analysis/location-analysis.module';
import { GoogleService } from './shared/services/google.service';
import { DueDiligenceModule } from './modules/due-diligence/due-diligence.module';
import { ModalModule } from './shared/services/modal/modal.module';
import { TeamModule } from './modules/team/team.module';
import { MarketValueEstimatorModule } from './modules/market-value-estimator/market-value-estimator.module';
import { EntityStructureModule } from './modules/entity-structure/entity-structure.module';
import { CostSegregationModule } from './modules/cost-segregation/cost-segregation.module';
import { FindingGoodDealModule } from './modules/finding-good-deal/finding-good-deal.module';
import { BestRentalsModule } from './modules/best-rentals/best-rentals.module';
import { SubmittingYourOfferModule } from './modules/submitting-your-offer/submitting-your-offer.module';

@NgModule({ declarations: [
        AppComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        FormsModule,
        AccountModule,
        ContactUsModule,
        HomeModule,
        PrivacyModule,
        RentalPropertiesModule,
        MarketValueEstimatorModule,
        TermsModule,
        ClassificationsModule,
        LocationAnalysisModule,
        DueDiligenceModule,
        ModalModule,
        TeamModule,
        EntityStructureModule,
        CostSegregationModule,
        FindingGoodDealModule,
        BestRentalsModule,
        SubmittingYourOfferModule,
        AppRoutingModule], providers: [
        Title,
        AuthorizationService,
        AuthGuardService,
        MessagingService,
        GoogleService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
        provideHttpClient(withInterceptorsFromDi())
    ] })
export class AppModule { }
