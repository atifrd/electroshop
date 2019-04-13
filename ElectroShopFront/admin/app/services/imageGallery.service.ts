import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { imageGallery } from '../_interface/imageGallery';
@Injectable({
    providedIn: 'root'
  })
  export class imageGallaryService {
    private baseurl = environment.baseurl;
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
      this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    }
    GetImages()
    {
        return this.http.get(this.baseurl+'ImageGallery',{headers:this.headers});
    }
    GetImagesByProduct(pid:number)
    {
      return this.http.get(this.baseurl+'ImageGallery/imgproduct/'+pid,{headers:this.headers});
    }
    AddImageToDb(body :imageGallery)
    {
        return this.http.post(this.baseurl+'ImageGallery',body,{headers:this.headers});
    }
}