using System.ComponentModel.DataAnnotations;

namespace Results.API.Dtos
{
    public class ResultsFromToDateDtoRequest
    {
        [DataType(DataType.Date), DisplayFormat( DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true )]
        public DateTime DateFrom {get; set;}

        [DataType(DataType.Date), DisplayFormat( DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true )]
        public DateTime DateTo {get; set;}

    }
}