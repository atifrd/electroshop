import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import { collapse } from '../../animation/collapse-animate';
import { categoriesForTree } from '../../../_interface/categoriesForTree';
import { SharedTreeService } from '../../../services/shared-tree.service';
@Component({
  selector: 'file-tree',
  templateUrl: './file-tree.component.html',
  styleUrls: ['./file-tree.component.scss'],
  animations: [collapse]
})
export class FileTreeComponent implements OnInit, OnDestroy {
  @Input() model: any;
  @Input() isChild: boolean;
  @Output() reFillGrid = new EventEmitter();
  constructor(private sharedTreeService: SharedTreeService) { }

  ngOnDestroy() {
    // this.sampleSubscription.unsubscribe();
  }
  ngOnInit() {
    this.model.forEach(element => {
      element.isSelect ? element.toggle = 'on' : element.toggle = 'init';
    });
  }

  private toggleItem(item: categoriesForTree) {
    item.toggle === 'on' ? item.toggle = 'off' : item.toggle = 'on';
    try {
      this.reFillGrid.emit(null);
      this.sharedTreeService.editMsg(item);
    }
    catch (e) {
      console.log('error' + e);
    }

  }

  private emitchildren()
  {
    this.reFillGrid.emit(null);
  }
}
