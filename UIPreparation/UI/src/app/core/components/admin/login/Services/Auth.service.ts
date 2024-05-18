import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { environment } from 'environments/environment';
import { LoginUser } from '../model/login-user';
import { TokenModel } from '../model/token-model';
import { SharedService } from 'app/core/services/shared.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userName: string;
  isLoggin: boolean;
  decodedToken: any;
  userToken: string;
  jwtHelper: JwtHelperService = new JwtHelperService();
  claims: string[];
  userId: number;

  constructor(private httpClient: HttpClient, private storageService: LocalStorageService, 
              private router: Router, private alertifyService: AlertifyService, 
              private sharedService: SharedService) {
    this.setClaims();
  }

  login(loginUser: LoginUser) {
    let headers = new HttpHeaders().append("Content-Type", "application/json");

    this.httpClient.post<TokenModel>(`${environment.getApiUrl}/Auth/login`, loginUser, { headers: headers })
      .subscribe(data => {
        if (data.success) {
          this.storageService.setToken(data.data.token);
          this.storageService.setItem("refreshToken", data.data.refreshToken);
          this.claims = data.data.claims;

          const decode = this.jwtHelper.decodeToken(this.storageService.getToken());
          const propUserName = Object.keys(decode).find(x => x.endsWith("/name"));
          this.userName = decode[propUserName];
          const propUserId = Object.keys(decode).find(x => x.endsWith("/nameidentifier"));
          this.userId = Number(decode[propUserId]);
          this.sharedService.sendChangeUserNameEvent();

          this.router.navigateByUrl("/dashboard");
        } else {
          this.alertifyService.warning(data.message);
        }
      });
  }

  getUserName(): string {
    return this.userName;
  }

  setClaims() {
    if ((this.claims == undefined || this.claims.length == 0) && this.storageService.getToken() != null && this.loggedIn()) {
      this.httpClient.get<string[]>(`${environment.getApiUrl}/operation-claims/cache`).subscribe(data => {
        this.claims = data;
      });

      const token = this.storageService.getToken();
      const decode = this.jwtHelper.decodeToken(token);     
      const propUserName = Object.keys(decode).find(x => x.endsWith("/name"));
      this.userName = decode[propUserName];
      const propUserId = Object.keys(decode).find(x => x.endsWith("/nameidentifier"));
      this.userId = Number(decode[propUserId]);
    }
  }

  logOut() {
    this.storageService.removeToken();
    this.storageService.removeItem("lang");
    this.storageService.removeItem("refreshToken");
    this.claims = [];
  }

  loggedIn(): boolean {
    const isExpired = this.jwtHelper.isTokenExpired(this.storageService.getToken(), -120);
    return !isExpired;
  }

  getCurrentUserId(): number {
    return this.userId;
  }

  claimGuard(claim: string): boolean {
    if (!this.loggedIn()) {
      this.router.navigate(["/login"]);
      return false;
    }

    return this.claims.includes(claim);
  }
}
