import{NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { FileUploadModule } from 'ng2-file-upload';
import {FormsModule} from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

import { NewProductComponent } from './new-product/new-product.component';
import {parentProductsComponent} from './parentProducts.component';
import { ImageProductComponent } from './image-product/image-product.component';
import { ProductPropertyComponent } from './product-property/product-property.component';
import {ListProductsComponent} from './list-products/list-products.component';
export const productsRoutes: Routes = [
    {
        path: '',component:parentProductsComponent ,children:
        [
            {path:'', redirectTo:'newProduct' ,pathMatch:'full'},
            {path:'newProduct', component: NewProductComponent}
            ,{path:'productList' ,component:ListProductsComponent}
            
    ]
    }
];
@NgModule(
    {
        imports:
        [
            CommonModule
            ,SharedModule
            ,RouterModule.forChild(productsRoutes)
            ,FileUploadModule
            ,FormsModule
            ,NgxPaginationModule
        ]
        ,declarations:
        [
            NewProductComponent
            ,parentProductsComponent
            , ImageProductComponent
            , ProductPropertyComponent
            ,ListProductsComponent
        ]
    }
)
export class productsModule
{

}