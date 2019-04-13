import { Routes, RouterModule } from '@angular/router';
import {PagesComponent} from './pages.component';
export const childRoutes: Routes = [
    
    {
        
        path: 'pages-shop',
        component: PagesComponent,
        children: [
            { path: '', redirectTo: 'index', pathMatch: 'full' },
            { path: 'index', loadChildren: './index/index-shop.module#IndexShopModule' },
            {path:'category',loadChildren:'./category/category.module#categoryModule'},
            {path:'product',loadChildren:'./products/products.module#productsModule'}
            // { path: 'editor', loadChildren: './editor/editor.module#EditorModule' },
            // { path: 'icon', loadChildren: './icon/icon.module#IconModule' },
            // { path: 'profile', loadChildren: './profile/profile.module#ProfileModule' },
            // { path: 'form', loadChildren: './form/form.module#FormModule' },
            // { path: 'charts', loadChildren: './charts/charts.module#ChartsModule' },
            // { path: 'ui', loadChildren: './ui/ui.module#UIModule' },
            // { path: 'table', loadChildren: './table/table.module#TableModule' },
            // { path: 'menu-levels', loadChildren: './menu-levels/menu-levels.module#MenuLevelsModule' },
       ]
    }

];



export const routing = RouterModule.forChild(childRoutes);