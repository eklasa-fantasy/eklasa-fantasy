import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-not-logged-in',
  templateUrl: './not-logged-in.component.html',
  styleUrl: './not-logged-in.component.css'
})
export class NotLoggedInComponent {
  @Output() switchView= new EventEmitter<string>();

}
