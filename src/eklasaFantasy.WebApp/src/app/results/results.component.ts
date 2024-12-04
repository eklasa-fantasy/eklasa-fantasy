import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css']
})
export class ResultsComponent implements OnInit {
  @Output() switchView= new EventEmitter<string>();
  results: any[] = [];
  expanded: boolean[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchResults();
  }

  fetchResults(): void {
    this.http.get<any[]>('https://localhost:7097/api/results/all') 
      .subscribe(
        (data) => {
          this.results = data;
          this.expanded = new Array(data.length).fill(false);
        },
        (error) => {
          console.error('Błąd podczas pobierania wyników:', error);
        }
      );
  }

  toggleDetails(index: number): void {
    this.expanded[index] = !this.expanded[index];
  }
}
