import { Component, ElementRef, NgZone, Input, AfterViewInit } from '@angular/core';
import { ReadProperty } from '../dtos/reads/readProperty';
import { GoogleService } from '../../../shared/services/google.service';


@Component({
    selector: 'app-property-google-map',
    template: '<ng-content></ng-content>'
})
export class PropertyGoogleMapComponent implements AfterViewInit {
    @Input() public property: ReadProperty;

    constructor(private element: ElementRef, private zone: NgZone, private googleService: GoogleService) { }

    ngAfterViewInit() {
        const nearbyGroceryOrSupermarkets = this.property.nearbyGroceryOrSupermarkets || [];
        const nearbyStarbuckses = this.property.nearbyStarbuckses || [];
        const nearbyPawnShops = this.property.nearbyPawnShops || [];
        const nearbyCheckCashingPlaces = this.property.nearbyCheckCashingPlaces || [];

        this.zone.runOutsideAngular(async () => {
            const mapsLoader = this.googleService.getMapsLoader();
            const google = await mapsLoader.load();

            const zoom = 12;

            const map = new google.maps.Map(this.element.nativeElement, {
                center: { lat: this.property.address.latitude, lng: this.property.address.longitude },
                zoom: zoom
            });

            // tslint:disable-next-line: no-unused-expression
            new google.maps.Marker({
                position: { lat: this.property.address.latitude, lng: this.property.address.longitude },
                animation: google.maps.Animation.DROP,
                map: map
            });

            // tslint:disable-next-line: no-unused-expression
            new google.maps.Circle({
                radius: 8046.72, // 5 miles
                clickable: false,
                strokeColor: '#ff0000',
                strokeOpacity: 1.0,
                strokeWeight: 2,
                fillOpacity: 0,
                center: { lat: this.property.address.latitude, lng: this.property.address.longitude },
                map: map
            });

            // tslint:disable-next-line: no-unused-expression
            new google.maps.Circle({
                radius: 4828.03, // 3 miles
                clickable: false,
                strokeColor: '#009933',
                strokeOpacity: 1.0,
                strokeWeight: 2,
                fillOpacity: 0,
                center: { lat: this.property.address.latitude, lng: this.property.address.longitude },
                map: map
            });

            // tslint:disable-next-line: no-unused-expression
            new google.maps.Circle({
                radius: 1609.34, // 1 mile
                clickable: false,
                strokeColor: '#0033cc',
                strokeOpacity: 1.0,
                strokeWeight: 2,
                fillOpacity: 0,
                center: { lat: this.property.address.latitude, lng: this.property.address.longitude },
                map: map
            });

            if (nearbyGroceryOrSupermarkets.length > 0) {
                const nearbyGroceryOrSupermarketsIcon = {
                    path: 'M528.12 301.319l47.273-208C578.806 78.301 567.391 64 551.99 64H159.208l-9.166-44.81C147.758 8.021 137.93 0 126.529 0H24C10.745 0 0 10.745 0 24v16c0 13.255 10.745 24 24 24h69.883l70.248 343.435C147.325 417.1 136 435.222 136 456c0 30.928 25.072 56 56 56s56-25.072 56-56c0-15.674-6.447-29.835-16.824-40h209.647C430.447 426.165 424 440.326 424 456c0 30.928 25.072 56 56 56s56-25.072 56-56c0-22.172-12.888-41.332-31.579-50.405l5.517-24.276c3.413-15.018-8.002-29.319-23.403-29.319H218.117l-6.545-32h293.145c11.206 0 20.92-7.754 23.403-18.681z',
                    fillColor: '#15aabf',
                    fillOpacity: 1,
                    strokeWeight: 0,
                    scale: 0.04
                };

                nearbyGroceryOrSupermarkets.forEach(nearbyGroceryOrSupermarket => {
                    // tslint:disable-next-line: no-unused-expression
                    new google.maps.Marker({
                        position: { lat: nearbyGroceryOrSupermarket.latitude, lng: nearbyGroceryOrSupermarket.longitude },
                        animation: google.maps.Animation.DROP,
                        map: map,
                        icon: nearbyGroceryOrSupermarketsIcon,
                        title: nearbyGroceryOrSupermarket.name
                    });
                });
            }

            if (nearbyStarbuckses.length > 0) {
                const starbucksIcon = {
                    path: 'M192 384h192c53 0 96-43 96-96h32c70.6 0 128-57.4 128-128S582.6 32 512 32H120c-13.3 0-24 10.7-24 24v232c0 53 43 96 96 96zM512 96c35.3 0 64 28.7 64 64s-28.7 64-64 64h-32V96h32zm47.7 384H48.3c-47.6 0-61-64-36-64h583.3c25 0 11.8 64-35.9 64z',
                    fillColor: '#15aabf',
                    fillOpacity: 1,
                    strokeWeight: 0,
                    scale: 0.04
                };

                nearbyStarbuckses.forEach(x => {
                    // tslint:disable-next-line: no-unused-expression
                    new google.maps.Marker({
                        position: { lat: x.latitude, lng: x.longitude },
                        animation: google.maps.Animation.DROP,
                        map: map,
                        icon: starbucksIcon,
                        title: x.name
                    });
                });
            }

            if (nearbyPawnShops.length > 0) {
                const pawnShopIcon = {
                    path: 'M434.7 64h-85.9c-8 0-15.7 3-21.6 8.4l-98.3 90c-.1.1-.2.3-.3.4-16.6 15.6-16.3 40.5-2.1 56 12.7 13.9 39.4 17.6 56.1 2.7.1-.1.3-.1.4-.2l79.9-73.2c6.5-5.9 16.7-5.5 22.6 1 6 6.5 5.5 16.6-1 22.6l-26.1 23.9L504 313.8c2.9 2.4 5.5 5 7.9 7.7V128l-54.6-54.6c-5.9-6-14.1-9.4-22.6-9.4zM544 128.2v223.9c0 17.7 14.3 32 32 32h64V128.2h-96zm48 223.9c-8.8 0-16-7.2-16-16s7.2-16 16-16 16 7.2 16 16-7.2 16-16 16zM0 384h64c17.7 0 32-14.3 32-32V128.2H0V384zm48-63.9c8.8 0 16 7.2 16 16s-7.2 16-16 16-16-7.2-16-16c0-8.9 7.2-16 16-16zm435.9 18.6L334.6 217.5l-30 27.5c-29.7 27.1-75.2 24.5-101.7-4.4-26.9-29.4-24.8-74.9 4.4-101.7L289.1 64h-83.8c-8.5 0-16.6 3.4-22.6 9.4L128 128v223.9h18.3l90.5 81.9c27.4 22.3 67.7 18.1 90-9.3l.2-.2 17.9 15.5c15.9 13 39.4 10.5 52.3-5.4l31.4-38.6 5.4 4.4c13.7 11.1 33.9 9.1 45-4.7l9.5-11.7c11.2-13.8 9.1-33.9-4.6-45.1z',
                    fillColor: '#ff0066',
                    fillOpacity: 1,
                    strokeWeight: 0,
                    scale: 0.04
                };

                nearbyPawnShops.forEach(x => {
                    // tslint:disable-next-line: no-unused-expression
                    new google.maps.Marker({
                        position: { lat: x.latitude, lng: x.longitude },
                        animation: google.maps.Animation.DROP,
                        map: map,
                        icon: pawnShopIcon,
                        title: x.name
                    });
                });
            }

            if (nearbyCheckCashingPlaces.length > 0) {
                const checkCashingIcon = {
                    path: 'M608 32H32C14.33 32 0 46.33 0 64v384c0 17.67 14.33 32 32 32h576c17.67 0 32-14.33 32-32V64c0-17.67-14.33-32-32-32zM176 327.88V344c0 4.42-3.58 8-8 8h-16c-4.42 0-8-3.58-8-8v-16.29c-11.29-.58-22.27-4.52-31.37-11.35-3.9-2.93-4.1-8.77-.57-12.14l11.75-11.21c2.77-2.64 6.89-2.76 10.13-.73 3.87 2.42 8.26 3.72 12.82 3.72h28.11c6.5 0 11.8-5.92 11.8-13.19 0-5.95-3.61-11.19-8.77-12.73l-45-13.5c-18.59-5.58-31.58-23.42-31.58-43.39 0-24.52 19.05-44.44 42.67-45.07V152c0-4.42 3.58-8 8-8h16c4.42 0 8 3.58 8 8v16.29c11.29.58 22.27 4.51 31.37 11.35 3.9 2.93 4.1 8.77.57 12.14l-11.75 11.21c-2.77 2.64-6.89 2.76-10.13.73-3.87-2.43-8.26-3.72-12.82-3.72h-28.11c-6.5 0-11.8 5.92-11.8 13.19 0 5.95 3.61 11.19 8.77 12.73l45 13.5c18.59 5.58 31.58 23.42 31.58 43.39 0 24.53-19.05 44.44-42.67 45.07zM416 312c0 4.42-3.58 8-8 8H296c-4.42 0-8-3.58-8-8v-16c0-4.42 3.58-8 8-8h112c4.42 0 8 3.58 8 8v16zm160 0c0 4.42-3.58 8-8 8h-80c-4.42 0-8-3.58-8-8v-16c0-4.42 3.58-8 8-8h80c4.42 0 8 3.58 8 8v16zm0-96c0 4.42-3.58 8-8 8H296c-4.42 0-8-3.58-8-8v-16c0-4.42 3.58-8 8-8h272c4.42 0 8 3.58 8 8v16z',
                    fillColor: '#ff0066',
                    fillOpacity: 1,
                    strokeWeight: 0,
                    scale: 0.04
                };

                nearbyCheckCashingPlaces.forEach(x => {
                    // tslint:disable-next-line: no-unused-expression
                    new google.maps.Marker({
                        position: { lat: x.latitude, lng: x.longitude },
                        animation: google.maps.Animation.DROP,
                        map: map,
                        icon: checkCashingIcon,
                        title: x.name
                    });
                });
            }
        });
    }
}
