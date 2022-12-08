import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WriteRefreshToken } from 'src/app/modules/rental-properties/dtos/writes/writeRefreshToken';
import { ReadAuthResponse } from '../dtos/reads/readAuthResponse';
import { ReadUser } from '../dtos/reads/readUser';
import { WriteConfirmEmail } from '../dtos/writes/WriteConfirmEmail';
import { WriteRegistration } from '../dtos/writes/writeRegistration';
import { WriteRequestPasswordReset } from '../dtos/writes/writeRequestPasswordReset';
import { WriteSignIn } from '../dtos/writes/writeSignIn';
import { WriteUpdateUser } from '../dtos/writes/writeUpdateUser';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }

  public register(apiModel: WriteRegistration): Observable<any> {
    return this.httpClient.post('/api/Account/Register', apiModel);
  }

  public confirmEmail(apiModel: WriteConfirmEmail): Observable<Object> {
    return this.httpClient.post('/api/Account/ConfirmEmail', apiModel);
  }


  public login(apiModel: WriteSignIn): Observable<any> {
    return this.httpClient.post('/api/Account/SignIn', apiModel);

  }

  public requestPasswordReset(apiModel: WriteRequestPasswordReset): Observable<Object> {
    return this.httpClient.post('/api/Account/RequestPasswordReset', apiModel);
  }

  public deleteAccount(): Observable<Object> {
    return this.httpClient.delete('/api/Account/DeleteAccount');
  }

  public refreshToken(apiModel: WriteRefreshToken): Observable<ReadAuthResponse> {
    return this.httpClient.post<ReadAuthResponse>('/api/Account/RefreshToken', apiModel);
  }

  public updateUser(apiModel: WriteUpdateUser): Observable<ReadUser> {
    return this.httpClient.put<ReadUser>('/api/Account/UpdateUser', apiModel);
  }
}
