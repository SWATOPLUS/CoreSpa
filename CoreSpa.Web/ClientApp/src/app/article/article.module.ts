import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/modules/shared.module';

import { AuthGuard } from '../auth.guard';
import { RootComponent } from './components/root/root.component';
import { HomeComponent } from './components/home/home.component';
import { ArticleComponent } from './components/article/article.component';
import { RouterModule } from '@angular/router';
import { GridModule } from '@progress/kendo-angular-grid';
import { ArticleService } from './article.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    GridModule,
    RouterModule.forChild([{
        path: 'articles',
        component: RootComponent, canActivate: [AuthGuard],

        children: [
          { path: '', component: HomeComponent },
          { path: 'article/:id', component: ArticleComponent }
        ]
    }])

  ],
  declarations: [
    RootComponent,
    HomeComponent,
    ArticleComponent
  ],
  exports: [],
  providers: [
    AuthGuard,
    ArticleService
  ]
})
export class ArticleModule { }
