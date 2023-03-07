import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from 'src/app/models/task';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit{
  task: Task = {
    id: '00000000-0000-0000-0000-000000000000',
    name: '',
    description: '',
    dateOfCreation: new Date(),
    isDone: false,
    deadline: undefined
  };
  username = "";
  
  name!: string | null;
  constructor(private route: ActivatedRoute, private userService: UserService,
    private router: Router, private authService: AuthService){}
  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next: (params) => {
        this.name = params.get('id');  }
      });
    }
  createTask()
  {
    this.authService.getMe().
        subscribe(data => {
         this.username = data;
       });
    this.userService.createTask(this.task, this.name).subscribe({
      next: (task) => {
        this.router.navigateByUrl('user-profile/'+ this.username+'/tasks');
      }
    });
  }

}
