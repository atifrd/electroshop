import { Component, OnInit, EventEmitter, Output, Input, DoCheck, ViewChild } from '@angular/core';
import { ProductProperty } from '../../../_interface/productProperty';
import { CategotyService } from '../../../services/categoty.service';
import { productPropertyMix } from '../../../_interface/productPropertyMix';
import { productPropertyService } from '../../../services/productProperty.service';


@Component({
  selector: 'app-product-property',
  templateUrl: './product-property.component.html',
  styleUrls: ['./product-property.component.scss']
})
export class ProductPropertyComponent implements OnInit {
//  @Input() productId: number;
  //@Input('categoryId') categoryIdParam: number;
  tableData: productPropertyMix;
  constructor(private _categotyService: CategotyService,
    private _productPropertyService: productPropertyService) {


  }
  // ngDoCheck() { console.log('ngDoCheck');  this.propertyArray="bache";
  //   this.propertyArraySend.next(this.propertyArray);
  // }
  ngOnInit() {
   // this.fillgrid();

  }
  fillgrid(catId,proId) {
    console.log(catId+'  '+ proId);
   // if (this.productId > 0) {
     // console.log('> '+this.productId);
      this._productPropertyService.GetProductProperties_toInsert(catId, proId).subscribe(next => {
        this.tableData = next;
      });
    }
  
  addproductProperties() {
    console.log('salam');
    this._productPropertyService.AddProductProperty(this.tableData).subscribe();
    console.log(this.tableData);
  }

  abcdd() {

    console.log('propertyValue: ' + JSON.stringify(this.tableData));
  }
}
