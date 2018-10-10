import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../dashboard.service';
import { DashbordProfile } from '../profile.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  homeDetails: DashbordProfile;

  constructor(private dashboardService: DashboardService) { }

  ngOnInit() {
    this.updateData();
  }

  updateData(){
    this.dashboardService.getHomeDetails()
    .subscribe((homeDetails: DashbordProfile) => {
      this.homeDetails = homeDetails;
    },
    error => {
      //this.notificationService.printErrorMessage(error);
    });    
  }

  onChanged($event){    
    const profile = {
      customerId: this.homeDetails.customerId,
    }

    profile[$event.name] = $event.value;

    this.dashboardService
      .updateProfile(profile)
      .subscribe(result => { }, error => {
        //this.notificationService.printErrorMessage(error);
      });

    this.updateData();
  }
}
