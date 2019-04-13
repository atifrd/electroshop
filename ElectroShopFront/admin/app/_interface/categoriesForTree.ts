 
 export interface categoriesForTree
 { id :number;
   name :string;
    parentId:number;
     subCategories: Array<categoriesForTree>;
     isSelect:boolean;
     toggle:any;
     //SubCategories1: categoriesForTree[] 
 }