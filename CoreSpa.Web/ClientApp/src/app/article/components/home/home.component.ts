import { Component, OnInit } from '@angular/core';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import {
  GridDataResult,
  DataStateChangeEvent
} from '@progress/kendo-angular-grid';
import { ArticleService } from '../../article.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  ngOnInit() {
  }

  public articles: GridDataResult;
  public state: DataSourceRequestState = {
    skip: 0,
    take: 5
  };

  constructor(private dataService: ArticleService) {
    this.refresh();
  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.refresh();
  }

  private refresh(){
    this.dataService.fetch(this.state)
      .subscribe(r => {
        this.articles = r;
      }, error => console.log(error));
  }
}
