import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReadFile } from '../../../shared/dtos/reads/readFile';

@Injectable()
export class FileService {
    constructor(private httpClient: HttpClient) { }

    public getFile(propertyId: number, fileId: number): Observable<ReadFile> {
        return this.httpClient.get<ReadFile>(`/api/RentalPropertyFiles/${propertyId}/${fileId}`);
    }

    public uploadFiles(propertyId: number, files: FileList): Observable<ReadFile[]> {
        const formData: FormData = new FormData();
        formData.append('propertyId', propertyId.toString());

        for (let i = 0; i < files.length; i++) {
            formData.append('files', files[i]);
        }

        return this.httpClient.post<ReadFile[]>(`/api/RentalPropertyFiles`, formData);
    }

    public deleteFile(propertyId: number, fileId: number): Observable<Object> {
        return this.httpClient.delete(`/api/RentalPropertyFiles/${propertyId}/${fileId}`);
    }
}
