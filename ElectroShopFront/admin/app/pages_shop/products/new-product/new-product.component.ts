import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { AlertifyService } from '../../../services/alertify.service';
import { product } from '../../../_interface/product';
import { CategotyService } from '../../../services/categoty.service';
import { categorylinear } from '../../../_interface/category-linear';
import { ProductPropertyComponent } from '../product-property/product-property.component';

//import { ProductProperty } from '../../../_interface/productProperty';
//import {NgForm} from '@angular/forms';
@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.scss'],
  
})
export class NewProductComponent implements OnInit {
  model: product;
  categorydrp: categorylinear;
  selectedcategory: categorylinear = { name: '', id: 0, parentId: 0 };
  @ViewChild("child") child :ProductPropertyComponent;
  constructor(private productService: ProductService,
    private _categotyService: CategotyService
    , private alertifyService: AlertifyService) {
   
  }

  ngOnInit() {
    this.filldrp();
    this.model =
      {
        brandId: 1,
        categoryId: 5,
        description: ""
        , id: 0
        , images: []
        , isEnabled: false
        , isNew: false
        , isPreview: false
        , orginalName: ""
        , productProperties: []//mishe hazf she?
        , regDate: new Date
        , sellerId: 1
        , title: ""
      }
  }
  filldrp() {
    this._categotyService.getCategories_drowpdown().subscribe(next => {
      this.categorydrp = next;

    })
  }

  onChangeObj(newObj) {
    console.log(newObj);
    this.selectedcategory.id = newObj;
  }
  addproduct() {
   // this.model.categoryId = this.selectedcategory.id;

    this.productService.AddProduct(this.model).subscribe(next => {
      //this.model.id = next;
      console.log('product in add product ' + JSON.stringify(this.model));
      this.alertifyService.success('', '');
      this.child.fillgrid(this.selectedcategory.id,next);
    }, error => { });
  }

}
