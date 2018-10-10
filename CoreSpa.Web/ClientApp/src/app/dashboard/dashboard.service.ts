import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';

import { Observable } from 'rxjs/Rx'; 

// Add the RxJS Observable operators we need in this app.
import '../rxjs-operators';
import { BaseService } from '../shared/services/base.service';
import { ConfigService } from '../shared/utils/config.service';
import { DashbordProfile } from './profile.interface';

@Injectable()

export class DashboardService extends BaseService {

  baseUrl: string = ''; 

  constructor(private http: Http, private configService: ConfigService) {
     super();
     this.baseUrl = configService.getApiURI();
  }

  private customerIdLsKey = "customerId";
  private customerId: number = 0;

  private saveCustomerId(customerId: number) {
    localStorage.setItem(this.customerIdLsKey, customerId.toString());
  }

  getCustomerId(): number {
    if(this.customerId == 0){
      const lsValue = localStorage.getItem(this.customerIdLsKey);
      
      if(lsValue){
        this.customerId = parseInt(lsValue);
      }
    }

    return this.customerId;
  }

  buildHeaders() : Headers{
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers.append('Authorization', `Bearer ${authToken}`);

    return headers;
  }

  getHomeDetails(): Observable<DashbordProfile> {
    var headers = this.buildHeaders();
  
    var request = this.http.get(this.baseUrl + "/dashboard/home", {headers})
      .map(response => response.json())
      .catch(this.handleError);

    request.subscribe(x=> this.saveCustomerId(x.customerId));

    return request;
  }

  updateProfile(profile){
    var headers = this.buildHeaders();
     
    return this.http.post(this.baseUrl + "/dashboard/profile", profile, {headers})
      .map(response => response.json())
      .catch(this.handleError);
  }
}
