

namespace Results.API.Dtos
{
    public class CardsDto
    {
       
        public string TimeReceived {get; set;} //bedzie musial zostac string z powodu kartek w doliczonym czasie (90+4)

        
        public string? HomeFault {get; set;}

        
        public string? AwayFault {get; set;}

        
        public string Card {get; set;}
    }
}