import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Province } from './province';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  private ApiUrl = '/api/provinces';

  constructor(private http: HttpClient) { }

  getProvinceByCountry(countryId: string): Observable<Province[]> {
    return this.http.get<Province[]>(this.ApiUrl + "?CountryId=" + countryId);
  }

}
