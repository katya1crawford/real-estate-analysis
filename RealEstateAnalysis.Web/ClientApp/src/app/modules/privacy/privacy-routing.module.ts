import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PrivacyComponent } from './components/privacy/privacy.component';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'privacy', component: PrivacyComponent }
    ])],
    exports: [RouterModule]
})
export class PrivacyRoutingModule { }
