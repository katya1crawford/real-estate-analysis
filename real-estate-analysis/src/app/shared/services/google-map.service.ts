import { Injectable } from '@angular/core';
import { Loader } from '@googlemaps/js-api-loader';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GoogleMapService {

  constructor() { }

  private mapsLoader: Loader = null;

  public getMapsLoader(): Loader {
    this.mapsLoader = this.mapsLoader || new Loader({
      apiKey: environment.apiKey
    });
    return this.mapsLoader;
  }
}
