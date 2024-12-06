export interface Team {
    teamId: number;
    teamBadge: string;
    teamName: string;
    played: number;
    wins: number;
    loses: number;
    draws: number;
    points: number;
    goalsF: number;
    goalsA: number;
    goalsDiff: number;
  }
  
  export interface ApiResponse {
    teams: Team[];
  }