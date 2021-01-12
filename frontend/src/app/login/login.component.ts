import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authSrv:AuthentificationService,private route:ActivatedRoute,private router:Router ) {     
  }
  ngOnInit(): void {
  }

  isAuth() {
    return this.authSrv.isAuthenticated();
  }

  onLogin() {
    this.authSrv.startAuthentication();
  }

  onLogout() {
    this.authSrv.signOut();
  }

}
