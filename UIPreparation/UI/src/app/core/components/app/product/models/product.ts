import { Order } from "../../order/models/Order";
import { WareHouseProductMapping } from "../../wareHouseProductMapping/models/WareHouseProductMapping";

export class Product{
    id?:number; 
createdUserId?:number; 
createdDate?:(Date | any); 
lastUpdatedUserId?:number; 
lastUpdatedDate?:(Date | any); 
status:boolean; 
isDeleted:boolean; 
name?:string; 
colorId?:number; 
wareHouseProductMappings?:WareHouseProductMapping[]; 
orders?:Order[]; 

}