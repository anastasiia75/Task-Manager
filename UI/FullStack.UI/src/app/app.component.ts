import { Component,OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'FullStack.UI';
  name = '';
  constructor(private authService: AuthService) { 
    this.getme();
  }  

  SignIn ()
  { 
    
    return this.authService.IsLoggedIn(); 
  }
  LogOut()
  {
    this.authService.Logout();
  }
  getme()
  {
     this.authService.getMe().
     subscribe(data => {
      this.name = data;
      console.log(data); // This will log the actual data object to the console
    });
  }
  
}
