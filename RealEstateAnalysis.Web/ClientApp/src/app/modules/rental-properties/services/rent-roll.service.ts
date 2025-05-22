import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadRentRollSummary } from '../dtos/reads/readRentRollSummary';

@Injectable()
export class RentRollService {
    constructor(private httpClient: HttpClient) { }

    public importRentRollCsv(propertyId: number, csvFile: File): Observable<ReadRentRollSummary> {
        const formData: FormData = new FormData();
        formData.append('propertyId', propertyId.toString());
        formData.append('rentRollCsv', csvFile);

        return this.httpClient.post<ReadRentRollSummary>(`/api/RentalPropertyRentRoll/ImportRentRollCsv`, formData);
    }

    public getSummary(propertyId: number): Observable<ReadRentRollSummary> {
        return this.httpClient.get<ReadRentRollSummary>(`/api/RentalPropertyRentRoll/Summary/${propertyId}`);
    }
}