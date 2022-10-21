import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './user';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userApiUrl = '/api/user';

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'accept': 'text/plain',
      'Content-Type': 'application/json'
    }),
    Body: ''
  };

  add(user: User) : Observable<string> {
    return this.http.post<string>(this.userApiUrl, user, this.httpOptions);

  }

  public findUserByLogin(account: string): Observable<User> {
    return this.http.get<User>(this.userApiUrl + "?Keywords=" + account);
  }
}
