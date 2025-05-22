import { Component } from '@angular/core';


@Component({
    templateUrl: './large-screenshot.component.html'
})
export class LargeScreenshotComponent {
    public title: string;
    public screenshot: string;

    public setModalProperties(title: string, screenshot: string): void {
        this.title = title;
        this.screenshot = screenshot;
    }
}
