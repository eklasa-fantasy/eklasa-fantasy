import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatchService } from './../match.service';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.scss']
})
export class MatchListComponent implements OnInit {
  @Output() switchView= new EventEmitter<string>();
  matches: any[] = [];

  constructor(private matchService: MatchService) {}

  ngOnInit(): void {
    this.fetchMatches();
  }

  fetchMatches(): void {
    this.matchService.getMatches().subscribe(
      (data) => {
        this.matches = data.map(match => ({
          match_time: match.time,
          match_hometeam_name: match.homeTeamName,
          match_awayteam_name: match.awayTeamName,
          match_date: match.date,
          match_round: match.round,
          team_home_badge: match.homeTeamBadge,
          team_away_badge: match.awayTeamBadge
        }));
      },
      (error) => {
        console.error('Błąd podczas pobierania danych:', error);
      }
    );
  }
}
