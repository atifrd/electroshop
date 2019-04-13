import { Component, OnInit } from '@angular/core';
import { product } from '../../../_interface/product';

@Component({
  selector: 'app-list-products',
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.scss']
})
export class ListProductsComponent implements OnInit {
  tableData:product
/* pagination Info */
pageSize = 10;
pageNumber = 1;

  constructor() { }

  ngOnInit() {
  }


  //#region grid
  loadData() {
   
  }

  pageChanged(pN: number): void {
    this.pageNumber = pN;
  }

  
  //#endregion
}



