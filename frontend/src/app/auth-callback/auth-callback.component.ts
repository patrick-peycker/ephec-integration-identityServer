import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css']
})

export class AuthCallbackComponent implements OnInit {
 
  error: boolean = false;

  constructor(private authentificationService: AuthentificationService, private router: Router) { }

  ngOnInit(): void {
    this.authentificationService.completeAuthentication()
      .then(content => {
        const pathRedirect = localStorage.getItem('path-redirect');
        if (pathRedirect != null) {
          localStorage.removeItem('path-redirect');
          this.router.navigate([pathRedirect]);
        } else {
          this.router.navigate(['/home']);
        }
      })
      .catch(
        error => { this.error = true;}
      );
  }

}
