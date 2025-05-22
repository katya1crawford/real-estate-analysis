import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReadFile } from '../../../shared/dtos/reads/readFile';

@Injectable()
export class PdfService {
    constructor(private httpClient: HttpClient) { }

    public getGetPropertySummaryPdf(propertyId: number): Observable<ReadFile> {
        return this.httpClient.get<ReadFile>(`/api/Pdf/GetPropertySummaryPdf/${propertyId}`);
    }
}
