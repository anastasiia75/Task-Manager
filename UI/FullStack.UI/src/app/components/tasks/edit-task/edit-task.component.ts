import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from 'src/app/models/task';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit{
  task: Task = {
    id: '',
    name: '',
    description: ''
  };
  username = "";
  
  name!: string | null;
  constructor(private route: ActivatedRoute, private userService: UserService,
    private router: Router, private authService: AuthService){
      
    }
    
  ngOnInit(): void {
    
    this.authService.getMe().
        subscribe(data => {
         this.username = data;
       });

    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('taskid');
        this.name = params.get('id');
        if (this.name && id){
          this.userService.getTask(this.name,id)
          .subscribe({
            next: (response) => {
              this.task = response;
            }
          })
        }
      }
    });
  }

  updateTask()
  {
    
    this.userService.updateTask(this.task, this.name, this.task.id)
    .subscribe({
      next: (response) => {
        this.router.navigateByUrl('user-profile/'+ this.username+'/tasks');
      }
    });
  
  }
  deleteTask()
  {

    this.userService.deleteTask(this.task.id, this.name)
    .subscribe({
      next: (response) =>{
      this.router.navigateByUrl('user-profile/'+ this.username+'/tasks');
    }
    });
  }

}
