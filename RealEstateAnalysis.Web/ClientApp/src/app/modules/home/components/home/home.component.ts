import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from '../../../../app.config';
import { LargeScreenshotComponent } from '../../modals/large-screenshot/large-screenshot.component';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    styleUrls: ['./home.component.css'],
    templateUrl: './home.component.html'
})
export class HomeComponent {
    public appName = '';

    private screenshotOneLarge = '/assets/home/1.png';
    private screenshotTwoLarge = '/assets/home/2.png';
    private screenshotThreeLarge = '/assets/home/3.png';
    private screenshotFourLarge = '/assets/home/4.png';

    constructor(private titleService: Title,
        private modalService: ModalService) {
        titleService.setTitle(`${appConfig.businessName}: Home`);
        this.appName = appConfig.businessName;
    }

    public onScreenshotClick(screenshotId: number, title: string): void {
        const editModal = this.modalService.show(LargeScreenshotComponent, { sizeClass: 'modal-lg' });

        switch (screenshotId) {
            case 1:
                editModal.setModalProperties(title, this.screenshotOneLarge);
                break;
            case 2:
                editModal.setModalProperties(title, this.screenshotTwoLarge);
                break;
            case 3:
                editModal.setModalProperties(title, this.screenshotThreeLarge);
                break;
            case 4:
                editModal.setModalProperties(title, this.screenshotFourLarge);
                break;
        }
    }
}
