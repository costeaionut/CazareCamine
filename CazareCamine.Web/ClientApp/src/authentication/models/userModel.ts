export class userModel {
  email: string;
  password: string;
  firstName: string;
  lastName: string;

  constructor(_email: string, _password: string, _firstName: string, _lastName: string) {
    this.email = _email;
    this.password = _password;
    this.lastName = _lastName;
    this.firstName = _firstName;
  }

}
