import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WareHouseProductMapping } from '../models/WareHouseProductMapping';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class WareHouseProductMappingService {

  constructor(private httpClient: HttpClient) { }


  getWareHouseProductMappingList(): Observable<WareHouseProductMapping[]> {

    return this.httpClient.get<WareHouseProductMapping[]>(environment.getApiUrl + '/wareHouseProductMappings/getall')
  }

  getWareHouseProductMappingById(id: number): Observable<WareHouseProductMapping> {
    return this.httpClient.get<WareHouseProductMapping>(environment.getApiUrl + '/wareHouseProductMappings/getbyid?id='+id)
  }

  addWareHouseProductMapping(wareHouseProductMapping: WareHouseProductMapping): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/wareHouseProductMappings/', wareHouseProductMapping, { responseType: 'text' });
  }

  updateWareHouseProductMapping(wareHouseProductMapping: WareHouseProductMapping): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/wareHouseProductMappings/', wareHouseProductMapping, { responseType: 'text' });

  }

  deleteWareHouseProductMapping(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/wareHouseProductMappings/', { body: { id: id } });
  }


}