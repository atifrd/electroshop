import { Component, OnInit,OnDestroy, Output,EventEmitter } from '@angular/core';
import { CategotyService } from '../../../services/categoty.service';
import { AlertifyService } from '../../../services/alertify.service';
import { SharedTreeService } from '../../../services/shared-tree.service';
import { Subscription } from 'rxjs/internal/Subscription';


@Component({
  selector: 'app-new-category',
  templateUrl: './new-category.component.html',
  styleUrls: ['./new-category.component.scss']
})
export class NewCategoryComponent implements OnInit,OnDestroy {
  sampleSubscription: Subscription;

  @Output() updateTreeInparent=new EventEmitter();
  constructor(private categorservice: CategotyService
    , private alertify: AlertifyService
    , private sharedTreeService: SharedTreeService
  ) { }


  addCategory(catnam: string) {
    var parentId;
    this.sampleSubscription = this.sharedTreeService.telecast$.subscribe(message => {
      parentId = message.id;
    });

    this.categorservice.AddCategory(parentId, catnam).subscribe(
      next => { 
        this.updateTreeInparent.emit();
        this.alertify.success('', '') }, error => { }
    );
    console.log('category added ' + parentId);
  }
  ngOnInit() {
  }
  ngOnDestroy() {
    if (this.sampleSubscription)
      this.sampleSubscription.unsubscribe();
  }
}
