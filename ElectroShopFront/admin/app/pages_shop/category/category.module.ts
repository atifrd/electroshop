
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { CategoryTreeComponent } from './category-tree/category-tree.component';
import { NewCategoryComponent } from './new-category/new-category.component';
import { CategoryPropertyComponent } from './category-property/category-property.component';
import {FormsModule} from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
export const categoryRoutes: Routes = [
    {
        path: '', component: CategoryTreeComponent
    }
];

@NgModule(
    {
        imports: [
            RouterModule.forChild(categoryRoutes),
            CommonModule
            , SharedModule
           ,FormsModule,NgxPaginationModule

        ],
        declarations:
            [
                CategoryTreeComponent,
                NewCategoryComponent,
                CategoryPropertyComponent,

            ]

    })

export class categoryModule {

}