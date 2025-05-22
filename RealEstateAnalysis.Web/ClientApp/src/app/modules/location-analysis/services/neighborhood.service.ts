import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadNeighborhood } from '../dtos/reads/readNeighborhood';
import { WriteNeighborhood } from '../dtos/writes/writeNeighborhood';

@Injectable()
export class NeighborhoodService {
    constructor(private httpClient: HttpClient) { }

    public getAllNeighborhoods(): Observable<ReadNeighborhood[]> {
        return this.httpClient.get<ReadNeighborhood[]>('/api/LocationAnalysis/Neighborhoods');
    }

    public getNeighborhood(id: number): Observable<ReadNeighborhood> {
        return this.httpClient.get<ReadNeighborhood>(`/api/LocationAnalysis/Neighborhoods/${id}`);
    }

    public saveNewNeighborhood(apiModel: WriteNeighborhood): Observable<ReadNeighborhood> {
        return this.httpClient.post<ReadNeighborhood>('/api/LocationAnalysis/Neighborhoods', apiModel);
    }

    public updateNeighborhood(id: number, apiModel: WriteNeighborhood): Observable<ReadNeighborhood> {
        return this.httpClient.put<ReadNeighborhood>(`/api/LocationAnalysis/Neighborhoods/${id}`, apiModel);
    }

    public deleteNeighborhood(id: number): Observable<Object> {
        return this.httpClient.delete(`/api/LocationAnalysis/Neighborhoods/${id}`);
    }
}
