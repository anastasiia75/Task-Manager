import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {
  name = "";
  constructor(private authService: AuthService){
    this.getme();
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
