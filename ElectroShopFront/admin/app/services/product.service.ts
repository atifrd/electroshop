import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import {product} from '../_interface/product';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseurl = environment.baseurl;
  private headers: HttpHeaders;
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }
  AddProduct(body:product)
  {
   return this.http.post(this.baseurl+'Product',body,{headers:this.headers})
   .pipe(map(x=>x as number));
  }
  GetProducts()
  {
    return this.http.get(this.baseurl+'Product',{headers:this.headers});
  }
  GetProductsByCategory(CategoryId)
  {
    return this.http.get(this.baseurl+'Product/GetProductsByCategory/'+CategoryId,{headers:this.headers});
  }
}
