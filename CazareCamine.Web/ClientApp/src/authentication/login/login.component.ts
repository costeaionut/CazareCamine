import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForLoginDTO } from '../models/userForLoginDTO';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  invalidLogin: boolean;

  baseUrl: string;
  router: Router;
  
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') _baseUrl: string,
    _router: Router,
    private authService: AuthenticationService
  ) {
    this.baseUrl = _baseUrl
    this.router = _router
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(''),
      password: new FormControl(''),
    });
    this.invalidLogin = false;
  }

  loginUser(loginFormValues) {

    const user: UserForLoginDTO = {
      email: loginFormValues.email,
      password: loginFormValues.password,
    }

    this.authService.loginUser(user)
      .subscribe(
        result => {
          const accessToken = result.accessToken;
          const refreshToken = result.refreshToken;
          localStorage.setItem("jwt", accessToken);
          localStorage.setItem("refreshToken", refreshToken);
          this.invalidLogin = false;
          window.location.href = "/";
        }, err => {
          this.invalidLogin = true;
        }
      )
  }

}
