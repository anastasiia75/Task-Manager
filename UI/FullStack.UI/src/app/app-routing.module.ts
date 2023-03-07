import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginUserComponent } from './components/authorization/login-user/login-user.component';
import { RegUserComponent } from './components/authorization/reg-user/reg-user.component';
import { CreateTaskComponent } from './components/tasks/create-task/create-task.component';
import { EditTaskComponent } from './components/tasks/edit-task/edit-task.component';
import { TasksListComponent } from './components/tasks/tasks-list/tasks-list.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthGuard } from './services/auth.gard';

const routes: Routes = [
  { path: "", redirectTo: 'AppComponent' , pathMatch: 'full'},

  { path: "login", component: LoginUserComponent, outlet: 'loginPopup' },
  { path: "register", component: RegUserComponent, outlet: 'regPopup' },
  {path: "user-profile/:id",
  component: UserProfileComponent,
  canActivate: [AuthGuard]},
  
  {path: "user-profile/:id/tasks",
  component: TasksListComponent,
  canActivate: [AuthGuard]},
  {path: "user-profile/:id/tasks/edit/:taskid",
  component: EditTaskComponent,
  canActivate: [AuthGuard]},
  {path: "user-profile/:id/tasks/create-task",
  component: CreateTaskComponent,
  canActivate: [AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
