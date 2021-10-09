import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { CurrentUser } from '../authentication/models/currentUser';
import { UserForLoginDTO } from '../authentication/models/userForLoginDTO';
import { UserForRegistrationDTO } from '../authentication/models/userForRegistrationDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  baseUrl: string

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') _baseUrl: string,
  ) {
    this.baseUrl = _baseUrl
  }

  public registerUser(body: UserForRegistrationDTO) {
    return this.http.post(this.baseUrl + "UserAccount/RegisterUser", body);
  }

  public loginUser(body: UserForLoginDTO) {
    return this.http.post<any>(this.baseUrl + "UserAccount/LoginUser", body);
  }

  public getCurrentUser() {
    return this.http.get <CurrentUser>(this.baseUrl + "UserAccount/GetCurrentUser");
  }

}
