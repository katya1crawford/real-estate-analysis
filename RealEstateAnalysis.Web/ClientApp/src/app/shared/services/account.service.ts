import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WriteSignIn } from '../dtos/writes/writeSignIn';
import { WriteRegistration } from '../dtos/writes/writeRegistration';
import { WriteRequestPasswordReset } from '../dtos/writes/writeRequestPasswordReset';
import { WritePasswordReset } from '../dtos/writes/writePasswordReset';
import { ReadUser } from '../dtos/reads/readUser';
import { WriteUpdateUser } from '../dtos/writes/writeUpdateUser';
import { ReadAuthResponse } from '../dtos/reads/readAuthResponse';
import { WriteRefreshToken } from '../dtos/writes/writeRefreshToken';
import { WriteConfirmEmail } from '../dtos/writes/writeConfirmEmail';

@Injectable()
export class AccountService {
    constructor(private httpClient: HttpClient) { }

    public confirmEmail(apiModel: WriteConfirmEmail): Observable<Object> {
        return this.httpClient.post('/api/Account/ConfirmEmail', apiModel);
    }

    public signIn(apiModel: WriteSignIn): Observable<ReadAuthResponse> {
        return this.httpClient.post<ReadAuthResponse>('/api/Account/SignIn', apiModel);
    }

    public register(apiModel: WriteRegistration): Observable<Object> {
        return this.httpClient.post('/api/Account/Register', apiModel);
    }

    public requestPasswordReset(apiModel: WriteRequestPasswordReset): Observable<Object> {
        return this.httpClient.post('/api/Account/RequestPasswordReset', apiModel);
    }

    public resetPassword(apiModel: WritePasswordReset): Observable<Object> {
        return this.httpClient.post('/api/Account/ResetPassword', apiModel);
    }

    public getLoggedIntUser(): Observable<ReadUser> {
        return this.httpClient.get<ReadUser>('/api/Account/GetLoggedInUser');
    }

    public refreshToken(apiModel: WriteRefreshToken): Observable<ReadAuthResponse> {
        return this.httpClient.post<ReadAuthResponse>('/api/Account/RefreshToken', apiModel);
    }

    public updateUser(apiModel: WriteUpdateUser): Observable<ReadUser> {
        return this.httpClient.put<ReadUser>('/api/Account/UpdateUser', apiModel);
    }

    public deleteAccount(): Observable<Object> {
        return this.httpClient.delete('/api/Account/DeleteAccount');
    }
}
