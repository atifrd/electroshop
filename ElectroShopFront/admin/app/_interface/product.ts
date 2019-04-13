import { imageGallery } from './imageGallery';
import { ProductProperty } from './productProperty';

export interface product
{
    id:number;
    orginalName:string;
    title:string;
    isPreview:boolean;
    isNew:boolean;
    isEnabled:boolean;
    description:string;
    regDate:Date;
    sellerId:number;
    categoryId:number
    brandId:number;
    images:imageGallery[];
    productProperties:ProductProperty[];
}