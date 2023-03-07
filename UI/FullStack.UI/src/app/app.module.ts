import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {CookieService} from 'ngx-cookie-service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegUserComponent } from './components/authorization/reg-user/reg-user.component';
import { LoginUserComponent } from './components/authorization/login-user/login-user.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { UserProfileComponent } from './components/user-profile/user-profile.component'; 
import { TokenInterceptorService } from './services/token-interceptor.service';
import { TasksListComponent } from './components/tasks/tasks-list/tasks-list.component';
import { EditTaskComponent } from './components/tasks/edit-task/edit-task.component';
import { CreateTaskComponent } from './components/tasks/create-task/create-task.component';


@NgModule({
  declarations: [
    AppComponent,
    RegUserComponent,
    LoginUserComponent,
    UserProfileComponent,
    TasksListComponent,
    EditTaskComponent,
    CreateTaskComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
    ],
  providers: [
    CookieService,
    {provide:HTTP_INTERCEPTORS,
      useClass:TokenInterceptorService,
      multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
