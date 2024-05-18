import { WareHouseProductMapping } from "../../wareHouseProductMapping/models/WareHouseProductMapping";

export class WareHouse{
    id?:number; 
createdUserId?:number; 
createdDate?:(Date | any); 
lastUpdatedUserId?:number; 
lastUpdatedDate?:(Date | any); 
status:boolean; 
isDeleted:boolean; 
name?:string; 
wareHouseProductMappings?:WareHouseProductMapping[]; 

}