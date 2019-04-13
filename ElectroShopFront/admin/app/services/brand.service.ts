import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
@Injectable({
    providedIn: 'root'
  })
  export class brandService {
    private baseurl = environment.baseurl;
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
      this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    }

    GetBrands()
    {
        return this.http.get(this.baseurl+'Brand',{headers:this.headers});
    }
    AddBrand(name :string)
    {
        return this.http.post(this.baseurl+'Brand/'+name,{},{headers:this.headers});
    }
}