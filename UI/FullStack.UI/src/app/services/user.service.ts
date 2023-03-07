import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Task } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
    
    getAllTasks(username: string):Observable<Task[]>
    {
        return this.http.get<Task[]>('https://localhost:7159/api/Assignment/user-profile/'+ username);
    }

    getTask(username: string, id: string):Observable<Task>
    {
        return this.http.get<Task>('https://localhost:7159/api/Assignment/user-profile/'+ username + '/'+ id);
    }

    updateTask(task: Task, username: string | null, id: string): Observable<Task>
    {
      return this.http.put<Task>('https://localhost:7159/api/Assignment/user-profile/'+ username + '/update/'+ id, task);
    }
    createTask(task: Task, username: string|null): Observable<Task>
    {
      return this.http.post<Task>('https://localhost:7159/api/Assignment/user-profile/'+ username + '/create-task', task);
    }
    deleteTask(id: string, username: string|null)
    {
      return this.http.delete('https://localhost:7159/api/Assignment/user-profile/'+ username + '/' + id);
    }
}
