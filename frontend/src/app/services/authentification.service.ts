import { UserManager, User, UserManagerSettings, WebStorageStateStore } from 'oidc-client';

export class AuthentificationService {

  constructor() {
    this.isAuth = false;
    this.userManager = new UserManager(this.getClientSettings());
    this.userManager.getUser()
      .then(
        content => {
          this.user = content;
        }
      )
      .catch(
        error => {
          alert('Authentication Service : ' + error)
        }
      );
  }

  private isAuth: boolean;
  private userManager: UserManager;
  private user: User = null;

  private getClientSettings(): UserManagerSettings {
    return {
      // Url IS
      authority: 'http://localhost:5000',
      // Client Id
      client_id: 'angular_spa',
      // Where to redirect to after login
      redirect_uri: 'http://localhost:4200/auth-callback',
      // Authorization Code Flow
      response_type: "code",
      // Scope
      scope: "openid profile api1",
      // Where to redirect to after logoutks
      post_logout_redirect_uri: 'http://localhost:4200/',
      // Where to save the token
      userStore: new WebStorageStateStore({ store: window.localStorage })
    }
  }

  getUser() {
    return this.user;
  }

  isAuthenticated() {
    return this.user !=null && !this.user.expired;
  }

  startAuthentication() {
    this.userManager.signinRedirect();
  }

  completeAuthentication() {
    return this.userManager.signinRedirectCallback().then(
      content => { this.user = content;}
    )
  }

  signOut() {
    return this.userManager.signoutRedirect();
  }
}