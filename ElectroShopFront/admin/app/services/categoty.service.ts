import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { categoriesForTree } from '../_interface/categoriesForTree';
import { map } from 'rxjs/operators';
import { property } from '../_interface/property';
import { categorylinear } from '../_interface/category-linear';

@Injectable({
  providedIn: 'root'
})
export class CategotyService {
  private baseurl = environment.baseurl;
  private headers: HttpHeaders;
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }
  getCategories() {
    return this.http.get(this.baseurl + 'category',
      { headers: this.headers }).pipe(map(x => x as categoriesForTree));

  }
  getCategories_drowpdown() {
    return this.http.get(this.baseurl + 'category/linear',
      { headers: this.headers }).pipe(map(x => x as categorylinear));

  }
  AddCategory(parentId, catName) {
    // var x = this.baseurl + 'Category/' + catName + '/and/' + parentId;
    // console.log(x);
    try {
      return this.http.post(this.baseurl + 'Category/' + catName + '/and/' + parentId, {}, { headers: this.headers });
    }
    catch (ex) { console.log(ex) }
  }


  //#region Property
  AddProperty(categoryId:number,property:property) {
  return this.http.post(this.baseurl+ 'Category/AddProperty/'+categoryId,property,{headers:this.headers});

  }
  GetPropertiesByCategoryId(categoryId:number)
  {
    return this.http.get(this.baseurl + 'category/GetPropertiesByCategory/'+categoryId,
      { headers: this.headers }).pipe(map(x => x as property));

  }
  //#endregion
}
