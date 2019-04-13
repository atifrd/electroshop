import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routing } from './pages_shop.routing';

//components
import { PagesComponent } from './pages.component';

import { SharedModule } from '../shared/shared.module';
import { LayoutModule } from '../shared/layout.module';

/* components */

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        LayoutModule,
        routing
    ],
    declarations: [
        PagesComponent
    ]
})
export class PagesShopModule { }
