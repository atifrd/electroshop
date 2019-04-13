import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { categoriesForTree } from '../_interface/categoriesForTree';

@Injectable({
  providedIn: 'root'
})
export class SharedTreeService {

  defualtcat: categoriesForTree =
    {
      name: 'Root Category', id: 0, isSelect: false, parentId: 0, subCategories: null, toggle: false
    }
  private msgSource = new BehaviorSubject<categoriesForTree>(this.defualtcat);

  telecast$ = this.msgSource.asObservable();

  constructor() { }


  editMsg(newMsg: categoriesForTree) {//ba call in method meghdar avaz va da ekhtiare hame
    this.msgSource.next(newMsg);

  }

}
