import { Injectable } from '@angular/core';
import {RootComponent} from '../shared/roots/root.component';
import { GlobalService } from '../shared/services/global.service';
@Injectable({
  providedIn: 'root'
})
export class AlertifyService extends RootComponent  {

    constructor(public _globalService: GlobalService) {
        super(_globalService);}

  notification(type,title,value) {
    this.alertMessage(
      {
        type: type,
        title: 'Look here!'+title,
        value: 'This alert needs your attention.'+value
      }
    );
  }


  success(title,value) {
    this.alertMessage(
      {
        type: 'success',
        title: 'Look here!'+title,
        value: 'This alert needs your attention.'+value
      }
    );
  }
}