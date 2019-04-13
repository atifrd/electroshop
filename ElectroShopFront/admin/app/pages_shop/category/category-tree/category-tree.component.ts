import { Component, OnInit, OnDestroy,ViewChild } from '@angular/core';
import { CategotyService } from '../../../services/categoty.service';
import { categoriesForTree } from '../../../_interface/categoriesForTree';
import { AlertifyService } from '../../../services/alertify.service';
import { SharedTreeService } from '../../../services/shared-tree.service';
import { Subscription } from 'rxjs/internal/Subscription';
@Component({
  selector: 'app-category-tree',
  templateUrl: './category-tree.component.html',
  styleUrls: ['./category-tree.component.scss']
})
export class CategoryTreeComponent implements OnInit, OnDestroy {
  //fileData:Array<any>;
  x1: categoriesForTree;
  sampleSubscription: Subscription;
  categoryTitle: categoriesForTree;
  @ViewChild('catpr') child;
  constructor(
    private categotyService: CategotyService, private alertify: AlertifyService,
    private sharedTreeService: SharedTreeService) { }


  ngOnDestroy() {
    this.sampleSubscription.unsubscribe();
  }
  
  ngOnInit() {

    this.sampleSubscription = this.sharedTreeService.telecast$.subscribe(message => {
      this.categoryTitle = message;
    });
    this.fillTree();
    
  }
  fillTree()
  {
    
    this.categotyService.getCategories().subscribe(next => {
      this.x1 = next;
    });

  }
  updateTreebychild()
  {
    this.fillTree();
  }
  
}