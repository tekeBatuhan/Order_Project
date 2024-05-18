import { Order } from "../../order/models/Order";

export class Customer{
    id?:number; 
createdUserId?:number; 
createdDate?:(Date | any); 
lastUpdatedUserId?:number; 
lastUpdatedDate?:(Date | any); 
status:boolean; 
isDeleted:boolean; 
name?:string; 
code?:string; 
address?:string; 
phoneNumber?:string; 
email?:string; 
orders?:Order[]; 

}