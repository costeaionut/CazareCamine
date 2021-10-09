import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CurrentUser } from '../../authentication/models/currentUser';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  user: CurrentUser;

  constructor(
    private authService: AuthenticationService,
    private router: Router
  )
  {
  }

  ngOnInit() {
    this.getCurrentUser();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut() {
    localStorage.removeItem("jwt");
    window.location.href = "/";
  }

  getCurrentUser() {
    this.authService.getCurrentUser()
      .subscribe(result => {
        this.user = result;
      }
    )
  }

}
