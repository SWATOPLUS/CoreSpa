import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveInputComponent } from './approve-input.component';

describe('ApproveInputComponent', () => {
  let component: ApproveInputComponent;
  let fixture: ComponentFixture<ApproveInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
