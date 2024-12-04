using Results.API.Data;
using Results.API.Dtos;
using Results.API.Interfaces;
using Results.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Results.API.Services
{
    public class TableService : ITableService
    {
        private readonly IResultsService _resultsService;
        private readonly ResultsDbContext _context;

        public TableService(IResultsService resultsService, ResultsDbContext context)
        {
            _resultsService = resultsService;
            _context = context;
        }


        public async Task<List<TableDto>> CalculateTable()
        {

            var results = await _context.Results
                .ToListAsync();


            return null;

        }


        public List<TableTeamDto> CalculateTeamStats()
        {
            return null;
        }

    }


}