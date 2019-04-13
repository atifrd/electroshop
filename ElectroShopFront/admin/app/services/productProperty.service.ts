import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ProductProperty} from '../_interface/productProperty';
import { map, catchError } from 'rxjs/operators';
import{Observable} from 'rxjs';
import { productPropertyMix } from '../_interface/productPropertyMix';
@Injectable({
  providedIn: 'root'
})
export class productPropertyService {
    private baseurl = environment.baseurl;
    private headers: HttpHeaders;
    constructor(private http: HttpClient) {
      this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    }
    GetProductProperties_toInsert(id: number,productId:number)
    {
        return this.http.get(this.baseurl+'ProductProperty/ProdProp_toInsert/'+id+'/'+productId,{headers:this.headers})
        .pipe(map(next =>next as productPropertyMix));
    }

    AddProductProperty(body:productPropertyMix)
    {
        var body2={};
        console.log('salam2');
     return this.http.post(this.baseurl+'ProductProperty/AddProProp',body,{headers:this.headers});
     //.pipe(catchError(x=> {console.log('service'+x);  return  Observable.throw(null);}));
    }
}