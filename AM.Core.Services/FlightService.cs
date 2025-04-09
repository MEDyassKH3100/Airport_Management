using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.Core.Domaine;

namespace AM.Core.Services
{
    public class FlightService : IFlightService
    {
        IList<Flight> flights { get; set; }

        IList<DateTime> GetFlightDates(string des)
        {
            IList<DateTime> listDAte = [];

            return (from f in flights
                    where f.Destination == des
                    select f.FlightDate).ToList();
            /*foreach (var flight in flights)
            {
                if(flight.Destination == des)
                {
                    listDAte.Add(flight.FlightDate);
                }
            }*/
            

        }

        public int GetWeeklyFlightNumber(DateTime date)
        {
             return (from f in flights where f.FlightDate <= date && date < date.AddDays(7) select f).Count();
            
           // return flights.Where(f=>f.FlightDate);
        }

       

    }
}
