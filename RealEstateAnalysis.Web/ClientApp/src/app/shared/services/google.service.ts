import { Injectable } from '@angular/core';
import { Loader } from 'google-maps';
import { appConfig } from '../../app.config';

@Injectable()
export class GoogleService {
    private mapsLoader: Loader = null;

    public getMapsLoader(): Loader {
        this.mapsLoader = this.mapsLoader || new Loader(appConfig.googleMapsKey);
        return this.mapsLoader;
    }
}
