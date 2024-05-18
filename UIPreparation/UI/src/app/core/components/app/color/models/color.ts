import { Product } from "../../product/models/product";

export class Color{
    id?:number; 
createdUserId?:number; 
createdDate?:(Date | any); 
lastUpdatedUserId?:number; 
lastUpdatedDate?:(Date | any); 
status:boolean; 
isDeleted:boolean; 
name?:string; 
products?:Product[]; 

}