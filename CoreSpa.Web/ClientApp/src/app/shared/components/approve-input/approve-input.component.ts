import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'approve-input',
  templateUrl: './approve-input.component.html',
  styleUrls: ['./approve-input.component.css']
})
export class ApproveInputComponent implements OnInit {

  isDirty: boolean;
  inputText: string;
  @Input() value: string;
  @Input() name: string;
  @Output() onChanged =  new EventEmitter();
  
  constructor() { }

  ngOnInit(){
    this.isDirty = false;
    this.inputText = this.value;
  }

  onTyped(){
    this.isDirty = this.inputText != this.value;
  }

  onSave(){
    this.value = this.inputText;
    this.onChanged.emit({
       name: this.name,
       value: this.value,
     });
    this.isDirty = false;
  }

  onCancel(){
    this.inputText = this.value;
    this.isDirty = false;
  }
}
