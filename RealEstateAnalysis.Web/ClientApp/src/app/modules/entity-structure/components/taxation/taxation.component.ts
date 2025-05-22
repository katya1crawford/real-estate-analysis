import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    selector: 'app-taxation',
    templateUrl: './taxation.component.html'
})
export class TaxationComponent {
    constructor(titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Taxation`);
    }
}
