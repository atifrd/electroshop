import { Component, OnInit, OnDestroy } from '@angular/core';
import { CategotyService } from '../../../services/categoty.service';
import { AlertifyService } from '../../../services/alertify.service';
import { property } from '../../../_interface/property';
import { NgForm } from '@angular/forms';
import { SharedTreeService } from '../../../services/shared-tree.service';
import { Subscription } from 'rxjs/internal/Subscription';


@Component({
  selector: 'app-category-property',
  templateUrl: './category-property.component.html',
  styleUrls: ['./category-property.component.scss']
})
export class CategoryPropertyComponent implements OnInit, OnDestroy {
  model: property = {
    id: 0,
    name: 'hkhkhk', controlType: '', description: 'ddd',
    isSearchable: true, oroginalName: 'org',
    required: false, showInDetails: false, valueType: ''
  };
  sampleSubscription: Subscription;
  categoryId: number;
  categoryname: string;

  tableData: property;
  /* pagination Info */
  pageSize = 10;
  pageNumber = 1;

  constructor(private _categotyService: CategotyService
    , private alertify: AlertifyService
    , private sharedTreeService: SharedTreeService
  ) { }
  AddProperty(formName: NgForm) {
    console.log(this.categoryId);

    this._categotyService.AddProperty(this.categoryId, this.model).subscribe(x => {
      this.alertify.success('title', 'value'); formName.reset(); this.loadData();
    }, error => { this.alertify.success('error', 'error val') });
  }

  ngOnInit() {
    this.sampleSubscription = this.sharedTreeService.telecast$.subscribe(message => {
      this.categoryname = message.name;
      this.categoryId = message.id;
    });

    this.loadData();
  }
  ngOnDestroy() {
    if (this.sampleSubscription)
      this.sampleSubscription.unsubscribe();
  }

  //#region grid
  loadData() {
    console.log('emit');
    this._categotyService.GetPropertiesByCategoryId(this.categoryId).subscribe(next => {
      this.tableData = next
    });
  }

  pageChanged(pN: number): void {
    this.pageNumber = pN;
  }

  abcd(item) {
    console.log(item);
  }
  //#endregion
}
