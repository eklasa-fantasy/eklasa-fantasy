import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatchService } from './../match.service';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.scss']
})
export class MatchListComponent implements OnInit {
  @Output() switchView= new EventEmitter<string>();
  matches: any[] = []; // Tablica na dane z backendu

  constructor(private matchService: MatchService) {}

  ngOnInit(): void {
    this.fetchMatches();
  }

  fetchMatches(): void {
    this.matchService.getMatches().subscribe(
      (data) => {
        // Filtrujemy wartości, aby pominąć ID
        this.matches = data.map(match => ({
          match_time: match.match_time,
          match_hometeam_name: match.match_hometeam_name,
          match_awayteam_name: match.match_awayteam_name,
          match_date: match.match_date,
          match_round: match.match_round,
          team_home_badge: match.team_home_badge,
          team_away_badge: match.team_away_badge
        }));
      },
      (error) => {
        console.error('Błąd podczas pobierania danych:', error);
      }
    );
  }
}
