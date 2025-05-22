import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadLookup } from '../dtos/reads/readLookup';

@Injectable()
export class LookupService {
    constructor(private httpClient: HttpClient) { }

    public getAllStates(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/States');
    }

    public getAllPropertyTypes(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/PropertyTypes');
    }

    public getAllPropertyStatuses(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/PropertyStatuses');
    }

    public getAllOperatingExpenseTypes(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/OperatingExpenseTypes');
    }

    public getAllInteriorRepairExpenseTypes(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/InteriorRepairExpenseTypes');
    }

    public getAllExteriorRepairExpenseTypes(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/ExteriorRepairExpenseTypes');
    }

    public getAllClosingCostTypes(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/ClosingCostTypes');
    }

    public getAllGeneralRepairExpenseTypes(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/Lookups/GeneralRepairExpenseTypes');
    }
}
