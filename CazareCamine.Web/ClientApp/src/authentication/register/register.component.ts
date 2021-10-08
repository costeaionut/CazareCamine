import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';
import { UserForRegistrationDTO } from '../models/userForRegistrationDTO';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  areErrors: boolean;
  errorList: Array<string> = []

  baseUrl: string;
  public registerForm: FormGroup;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') _baseUrl: string,
    private authService: AuthenticationService
  ) {
    this.baseUrl = _baseUrl;
  }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('')
    });
    this.areErrors = false;
  }

  public validateControl(controlName: string) {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }
  public hasError (controlName: string, errorName: string) {
    return this.registerForm.controls[controlName].hasError(errorName)
  }

  registerUser(registerFormValues) {

    const formValues = registerFormValues

    const user: UserForRegistrationDTO = {
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      email: formValues.email,
      password: formValues.password,
      confirmPassword: formValues.confirm
    };

    this.authService.registerUser(user).subscribe(
      _ => {
        console.log("Successful registration");
      },
      error => {
        console.log(error.error.errors);
        this.areErrors = true;
        this.errorList = error.error.errors;
      }
    )


  }

}
