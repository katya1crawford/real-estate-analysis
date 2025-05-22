import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadFile } from '../../../shared/dtos/reads/readFile';

@Injectable()
export class GalleryImageService {
    constructor(private httpClient: HttpClient) { }

    public delete(propertyId: number, galleryImageId: number): Observable<Object> {
        return this.httpClient.delete(`/api/RentalPropertyGalleryImage/${propertyId}/${galleryImageId}`);
    }

    public getAllLarge(propertyId: number): Observable<ReadFile[]> {
        return this.httpClient.get<ReadFile[]>(`/api/RentalPropertyGalleryImage/${propertyId}/GetAllLarge`);
    }

    public getAllSmall(propertyId: number): Observable<ReadFile[]> {
        return this.httpClient.get<ReadFile[]>(`/api/RentalPropertyGalleryImage/${propertyId}/GetAllSmall`);
    }

    public saveNew(propertyId: number, files: FileList): Observable<ReadFile[]> {
        const formData: FormData = new FormData();
        formData.append('propertyId', propertyId.toString());

        for (let i = 0; i < files.length; i++) {
            formData.append('files', files[i]);
        }

        return this.httpClient.post<ReadFile[]>(`/api/RentalPropertyGalleryImage`, formData);
    }
}
