import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WriteContactUs } from '../dtos/writes/writeContactUs';

@Injectable()
export class ContactUsService {
    constructor(private httpClient: HttpClient) { }

    public sendEmail(apiModel: WriteContactUs): Observable<Object> {
        return this.httpClient.post(`/api/ContactUs/`, apiModel);
    }
}
