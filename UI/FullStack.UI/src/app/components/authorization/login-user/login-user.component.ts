import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-user',
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.css']
})
export class LoginUserComponent implements OnInit
{
  showModal = false;
  user!: User;
  formData!: FormGroup;
  responsedata: any;
  username = "";
  constructor (private authService: AuthService,  private router : Router) {}
  
  ngOnInit() {
    this.user = { username: "", password:"" };
    this.formData = new FormGroup({
       username: new FormControl("", Validators.required),
       password: new FormControl("", Validators.required),
    });
    this.showModal = true;
 }

 onClickSubmit(data: any) {
  if (this.formData.valid){
    this.user.username = data.username;
    this.user.password = data.password;
    this.username = data.username;
    console.log("Login page: " + this.user.username);
    console.log("Login page: " + this.user.password);
    
    this.authService.login(this.user)
      .subscribe( (result: string) => 
        { 
          console.log("Is Login Success: " + result); 
          if(result!=null){
            this.showModal = false;
            this.responsedata=result;
            localStorage.setItem('token', this.responsedata);
            this.router.navigateByUrl('user-profile/' + this.user.username);
            
          }    
        }
      );
  }
    

  }

  onClose(){
    this.showModal = false;
    setTimeout(
      () => this.router.navigate(['']),
      100
    );
  }
}


