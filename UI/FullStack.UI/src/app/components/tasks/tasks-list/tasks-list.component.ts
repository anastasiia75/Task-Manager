import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Task } from 'src/app/models/task';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})

export class TasksListComponent implements OnInit {
  
  tasks: Task[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService,
    private router: Router){}
  ngOnInit(): void {
    
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id){
          this.userService.getAllTasks(id)
          .subscribe({
            next: (response) => {
              this.tasks = response;
            }
          })
        }
      }
    });
  }
}  
  

