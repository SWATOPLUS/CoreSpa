import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'approve-input',
  templateUrl: './approve-input.component.html',
  styleUrls: ['./approve-input.component.css']
})
export class ApproveInputComponent implements OnInit {

  isDirty: boolean;
  inputText: string;
  @Input() text: string;
  @Output() onChanged =  new EventEmitter();
  
  constructor() { }

  ngOnInit(){
    this.isDirty = false;
    this.inputText = this.text;
  }

  onTyped(){
    this.isDirty = this.inputText != this.text;

    console.log("typed " + this.isDirty);
  }

  onSave(){
    this.text = this.inputText;
    this.onChanged.emit(this.text);
    this.isDirty = false;
  }

  onCancel(){
    this.inputText = this.text;
    this.isDirty = false;
  }
}
