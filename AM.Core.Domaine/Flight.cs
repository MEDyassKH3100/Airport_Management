using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Domaine
{
    public class Flight
    {
        public string Comment { get; set; }
        public string Destination { get; set; }
        public string Departure { get; set; }
        public DateTime FlightDate { get; set; }

        public int FlightId { get; set; }

        public DateTime EffectiveArrival { get; set; }
        public int EstimateDuration { get; set; }

        //ou bien [ForeignKey("MyPlane")]
        public int? PlaneId { get; set; }

        [ForeignKey("PlaneId")]
        public virtual Plane? MyPlane { get; set; }
        public virtual IList<Passenger> Passengers { get; set; }

        // Propriu00e9tu00e9 de navigation pour les ru00e9servations
        public virtual ICollection<Reservation> Reservations { get; set; }

        public override string ToString()
        {
            return "FlightId: "
                + FlightId
                + ", Departure:"
                + Departure
                + ", Destination:"
                + Destination
                + ", FlightDate: "
                + FlightDate
                + ", EffectiveArrival: "
                + EffectiveArrival
                + ", EstimateDuration: "
                + EstimateDuration
                + ", Plane: "
                + MyPlane
                + ", Passengers: "
                + Passengers;
        }
    }
}
