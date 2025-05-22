import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadProperty } from '../dtos/reads/readProperty';
import { WriteProperty } from '../dtos/writes/writeProperty';
import { ReadLookup } from '../../../shared/dtos/reads/readLookup';

@Injectable()
export class RentalPropertyService {
    constructor(private httpClient: HttpClient) { }

    public getByPropertyStatus(propertyStatus: PropertyStatusEnum): Observable<ReadProperty[]> {
        return this.httpClient.get<ReadProperty[]>(`/api/RentalProperties/propertyStatus/${propertyStatus}`);
    }

    public getAllGroupNames(): Observable<string[]> {
        return this.httpClient.get<string[]>('/api/RentalProperties/GetAllGroupNames');
    }

    public getAllSubjectPropertyLookups(): Observable<ReadLookup[]> {
        return this.httpClient.get<ReadLookup[]>('/api/RentalProperties/GetAllSubjectPropertyLookups');
    }

    public getByGroupName(groupName: string): Observable<ReadProperty[]> {
        return this.httpClient.get<ReadProperty[]>(`/api/RentalProperties/groupName/${groupName}`);
    }

    public getProperty(id: number, includeNearbyPlaces: boolean = false): Observable<ReadProperty> {
        return this.httpClient.get<ReadProperty>(`/api/RentalProperties/${id}/${includeNearbyPlaces}`);
    }

    public saveNewProperty(apiModel: WriteProperty): Observable<ReadProperty> {
        const formData: FormData = new FormData();
        this.buildFormData(formData, apiModel);
        return this.httpClient.post<ReadProperty>('/api/RentalProperties', formData);
    }

    public updateProperty(id: number, apiModel: WriteProperty): Observable<ReadProperty> {
        const formData: FormData = new FormData();
        this.buildFormData(formData, apiModel);
        return this.httpClient.put<ReadProperty>(`/api/RentalProperties/${id}`, formData);
    }

    public deleteProperty(propertyId: number): Observable<Object> {
        return this.httpClient.delete(`/api/RentalProperties/${propertyId}`);
    }

    public deleteThumbnailImage(propertyId: number): Observable<Object> {
        return this.httpClient.delete(`/api/RentalProperties/${propertyId}/DeleteThumbnailImage`);
    }

    private buildFormData(formData: FormData, data: any, parentKey: any | null = null) {
        if (data && typeof data === 'object' && !(data instanceof Date) && !(data instanceof File)) {
            Object.keys(data).forEach(key => {
                this.buildFormData(formData, data[key], parentKey ? `${parentKey}[${key}]` : key);
            });
        } else {
            const value = data == null ? '' : data;
            formData.append(parentKey, value);
        }
    }
}

export enum PropertyStatusEnum {
    Listing = 1,
    InReview = 2,
    Purchased = 3
}
