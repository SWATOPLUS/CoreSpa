import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { toODataString, DataSourceRequestState, toDataSourceRequestString, DataResult, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { map } from 'rxjs/operators/map';
import { tap } from 'rxjs/operators/tap';

export abstract class BaseGridFetcher extends BehaviorSubject<GridDataResult> {
  public loading: boolean;
  constructor(private http: HttpClient, private url: string) {
    super(null);
  }

  public fetch(state: DataSourceRequestState): Observable<DataResult> {
    const queryStr = `${toDataSourceRequestString(state)}`; // Serialize the state
    const hasGroups = state.group && state.group.length;

    return this.http
      .get(`${this.url}?${queryStr}`) // Send the state to the server
      .map(({ data, total/*, aggregateResults*/ }: GridDataResult) => // Process the response
        (<GridDataResult>{
          // If there are groups, convert them to a compatible format
          data: hasGroups ? translateDataSourceResultGroups(data) : data,
          total: total,
          // Convert the aggregates if such exist
          //aggregateResult: translateAggregateResults(aggregateResults)
        })
      )
  }
}

@Injectable()
export class ArticleService extends BaseGridFetcher {
  constructor(http: HttpClient) { super(http, '/api/Articles/ListArticle'); }
}
