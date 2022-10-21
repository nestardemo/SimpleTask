import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Coutry } from './coutry';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private ApiUrl = '/api/countries';

  constructor(private http: HttpClient) { }

  getAllCountries(): Observable<Coutry[]> {
    return this.http.get<Coutry[]>(this.ApiUrl);
  }

}
