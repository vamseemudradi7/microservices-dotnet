import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../environment';
import { Claim } from '../models/claim';
import {
  MsalService,
} from '@azure/msal-angular';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private usersAPIURL: string = environment.usersAPIURL;

  public isAuthenticated: boolean = false;
  public claims: Claim[] = [];

  public isAdmin: boolean = false;
  public adminUserId: string = 'c9839fb6-faea-4413-bf90-49488c14fd43';
  public currentUserName: string = '';
  public currentUserId: string = '';

  constructor(private http: HttpClient) {
  }

  getClaim(claimName: string): Claim {
    return this.claims.find(temp => temp.claim == claimName) as Claim;
  }

  setClaims(claims: Claim[]) {
    this.claims = claims;
  }

  setIsAuthenticated(authService: MsalService) {
    this.isAuthenticated = authService.instance.getAllAccounts().length > 0;
    var subClaim = this.getClaim('sub');

    if (subClaim && this.isAuthenticated) {
      this.isAdmin = subClaim.value == this.adminUserId;
      this.currentUserId = subClaim.value;
      this.currentUserName = this.getClaim('given_name').value;
    }
    console.log(this.claims);
  }


  getAuthToken(): any {
    const token: any = localStorage.getItem('authToken');
    return token;
  }
}
