import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { UserForRegistrationDTO } from '../authentication/models/userForRegistrationDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  baseUrl: string

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') _baseUrl: string
  ) {
    this.baseUrl = _baseUrl
  }

  public registerUser(body: UserForRegistrationDTO) {
    return this.http.post(this.baseUrl + "UserAccount/RegisterUser", body);
  }

}
