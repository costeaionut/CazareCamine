import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { userModel } from '../models/userModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  baseUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') _baseUrl: string
  ) {
    this.baseUrl = _baseUrl;
  }

  ngOnInit(): void {
  }

  registerForm = new FormGroup({
    fNameField: new FormControl(''),
    lNameField: new FormControl(''),
    emailField: new FormControl(''),
    passwordField: new FormControl('')
  });

  sendFormValues() {
    var formValues = this.registerForm.value
    var userInfo = new userModel(formValues.emailField, formValues.passwordField, formValues.fNameField, formValues.lNameField)
    this.http.post(this.baseUrl + "UserAccount/RegisterUser", userInfo)
      .subscribe(result => {
        console.log(result)
      }, error => console.error(error));
  }

}
