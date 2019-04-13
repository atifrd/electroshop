import { Component, OnInit, Input } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { imageGallery } from '../../../_interface/imageGallery';
import { environment } from '../../../../../admin/environments/environment';

const URL = 'https://evening-anchorage-3159.herokuapp.com/api/'; 
@Component({
  selector: 'app-image-product',
  templateUrl: './image-product.component.html',
  styleUrls: ['./image-product.component.scss']
})
export class ImageProductComponent implements OnInit {

  @Input() photos: imageGallery[];
  constructor() { }

  ngOnInit() {
    this.uploaderInitialize();
  }

  public uploader: FileUploader;
  public hasBaseDropZoneOver: boolean = false;
  public hasAnotherDropZoneOver: boolean = false;

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }
  uploaderInitialize() {
    this.uploader = new FileUploader({
      url: URL, maxFileSize: 10 * 1024 * 1024
      , autoUpload: false, allowedFileType: ['image'], isHTML5: true
    });
    this.uploader.onErrorItem=(item, response, status, headers)=>{
      console.log('error');
      let error = JSON.parse(response); //error server response
    }
    this.uploader.onSuccessItem = (item, response:any, status, headers) => {
      if (response) {
        console.log(response);
        console.log(JSON.parse(response));
        // const res:imageGallery=JSON.parse(response);

        // const photo:imageGallery ={
        //   title:res.title
        //   ,imageType:res.imageType,
        //   description:res.description
        //   ,path:res.path
        //   ,isActive:true
        //   ,link:res.link
        //   ,relatedProductId:4
        // };
        //this.photos.push(photo);
      }
    }
  
  }
}
