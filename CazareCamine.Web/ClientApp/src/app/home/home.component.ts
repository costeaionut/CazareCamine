import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { CurrentUser } from '../../authentication/models/currentUser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  user: CurrentUser;

  constructor(private http: HttpClient, private authService: AuthenticationService) {

  }

  ngOnInit() {
    this.getCurrentUser();
  }

  getCurrentUser() {
    this.authService.getCurrentUser()
      .subscribe(result => {
        this.user = result;
      }
    )
  }

}
