import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  teams: any[] = [];
  loading: boolean = true;
  error: string | null = null;

  //private apiUrl = 'https://localhost:7097/api/results/table';
  private apiUrl = 'http://localhost:3000/teams';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchLeagueTable();

    console.log(this.teams);
  }

  fetchLeagueTable(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (response) => {
        console.log(response);
        this.teams = response;
        this.loading = false;
      },
      error: (err) => {
        console.error('Błąd podczas pobierania danych z API:', err);
        this.error = 'Nie udało się załadować danych.';
        this.loading = false;
      }
    });
  }

}
