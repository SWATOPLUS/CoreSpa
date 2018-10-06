import { Component } from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  status: boolean;
  subscription:Subscription;
  isExpanded = false;

  constructor(private userService:UserService) {
         
  }

  logout() {
    this.userService.logout();       
  }

 ngOnInit() {
   this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);
 }

  ngOnDestroy() {
   // prevent memory leak when component is destroyed
   this.subscription.unsubscribe();
 }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
