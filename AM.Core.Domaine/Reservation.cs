using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Domaine
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public double Price { get; set; }

        public string Seat { get; set; }

        public bool VIP { get; set; }

        // Clés étrangères et navigation properties
        public string PassengerFK { get; set; }

        [ForeignKey("PassengerFK")]
        public virtual Passenger Passenger { get; set; }

        public int FlightFK { get; set; }

        [ForeignKey("FlightFK")]
        public virtual Flight Flight { get; set; }
    }
}
