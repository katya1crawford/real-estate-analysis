import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadFinancialForecast } from '../dtos/reads/readFinancialForecast';
import { ReadFinancialSummary } from '../dtos/reads/readFinancialSummary';
import { WriteFinancialSummary } from '../dtos/writes/writeFinancialSummary';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {

  constructor(private httpClient: HttpClient) { }

  public getFinancialSummary(apiModel: WriteFinancialSummary): Observable<ReadFinancialSummary> {
    return this.httpClient.post<ReadFinancialSummary>('/api/RentalPropertyCalculator/FinancialSummary', apiModel);
  }

  public getRentalPropertyLongTermFinancialForecasts(propertyId: number): Observable<ReadFinancialForecast[]> {
    return this.httpClient.get<ReadFinancialForecast[]>(`/api/RentalPropertyCalculator/LongTermFinancialForecasts/${propertyId}`);
  }
}
