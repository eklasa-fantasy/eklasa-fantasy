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

        public TableService(IResultsService resultsService){
            _resultsService = resultsService;
        }


        public async Task<List<TableDto>> GetTable(){
            
        }
    }

 
}