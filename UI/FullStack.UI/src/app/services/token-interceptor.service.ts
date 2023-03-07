import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs/internal/Observable";
import { tap } from "rxjs/operators";
import { AuthService } from "./auth.service";

@Injectable({
    providedIn: 'root'
  })
  export class TokenInterceptorService implements HttpInterceptor {
  
    constructor(private inject:Injector, private router: Router) {}
    intercept(
      req: HttpRequest<any>, 
      next: HttpHandler): Observable<HttpEvent<any>> {
      let authservice = this.inject.get(AuthService);
      const token = authservice.GetToken();
      if(token)
      {
        req = req.clone({
        setHeaders: { Authorization: `bearer ${token}`}
        });
      }
      
      return next.handle(req).pipe( tap(() => {},
      (err: any) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status !== 401) {
         return;
        }
        this.router.navigate(['login']);
      }
    }));

  }
  }