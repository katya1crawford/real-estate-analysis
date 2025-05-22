import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReadCity } from '../dtos/reads/readCity';
import { Observable } from 'rxjs';
import { WriteCity } from '../dtos/writes/writeCity';

@Injectable()
export class CityService {
    constructor(private httpClient: HttpClient) { }

    public getAllCities(): Observable<ReadCity[]> {
        return this.httpClient.get<ReadCity[]>('/api/LocationAnalysis/Cities');
    }

    public getCity(id: number): Observable<ReadCity> {
        return this.httpClient.get<ReadCity>(`/api/LocationAnalysis/Cities/${id}`);
    }

    public saveNewCity(apiModel: WriteCity): Observable<ReadCity> {
        return this.httpClient.post<ReadCity>('/api/LocationAnalysis/Cities', apiModel);
    }

    public updateCity(id: number, apiModel: WriteCity): Observable<ReadCity> {
        return this.httpClient.put<ReadCity>(`/api/LocationAnalysis/Cities/${id}`, apiModel);
    }

    public deleteCity(id: number): Observable<Object> {
        return this.httpClient.delete(`/api/LocationAnalysis/Cities/${id}`);
    }

    public toggleIsFavorite(id: number): Observable<Object> {
        return this.httpClient.put(`/api/LocationAnalysis/Cities/ToggleIsFavorite/${id}`, null);
    }

    public harvestCityData(minimumPopulationCount: number): Observable<Object> {
        return this.httpClient.post(`/api/LocationAnalysis/Cities/HarvestCityData/${minimumPopulationCount}`, null);
    }
}
