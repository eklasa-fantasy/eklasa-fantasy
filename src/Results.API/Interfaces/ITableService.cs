
using Results.API.Dtos;

namespace Results.API.Interfaces
{   
    public interface ITableService
    {
        Task<List<TableDto>> GetTable();
    }

}