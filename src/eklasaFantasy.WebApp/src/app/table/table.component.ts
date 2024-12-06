import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiResponse, Team } from './team.model';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  teams: Team[] = [];
  loading: boolean = true;
  error: string | null = null;

  private apiUrl = 'https://localhost:7097/api/results/table';
  //private apiUrl = 'http://localhost:3000/teams';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchLeagueTable();
  }

  fetchLeagueTable(): void {
    this.http.get<ApiResponse>(this.apiUrl)
    .subscribe(
      (data) => {
        this.teams = data.teams;
        this.loading = false;
      },
      (err) => {
        console.error('Błąd podczas pobierania danych z API:', err);
        this.error = 'Nie udało się załadować danych.';
        this.loading = false;
      }
    );
  }

}
