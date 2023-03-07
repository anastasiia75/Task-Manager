import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../models/user';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'access_token';
  isUserLoggedIn: boolean = false;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  currentUser = {};
  router: any;


  constructor(private http: HttpClient) { }
 

  public register(user: User): Observable<string> 
  {
    return this.http.post('https://localhost:7159/api/Auth/register(regPopup:register)', user, {
      responseType: 'text'});
  }

  public login(user: User): Observable<string> {

    return this.http.post('https://localhost:7159/api/Auth/login(loginPopup:login)', user, {
      responseType: 'text' });
  }

  public Logout()
  {
      localStorage.removeItem('token'); 

  }
  IsLoggedIn() {

    return localStorage.getItem('token') !== null ;   
  }
  GetToken(){
   return localStorage.getItem('token');
  }
  
  public getMe() : Observable<string>
  {
    return this.http.get('https://localhost:7159/api/Auth/', {responseType: 'text'});
  }
}
