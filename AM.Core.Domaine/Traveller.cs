using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Domaine
{
    public class Traveller : Passenger
    {
        public string HealthInformation { get; set; }
        public string Nationality { get; set; }

        public override string GetPassengerType()
        {
            return base.GetPassengerType()+"I am a Traveller";
        }

        public override string ToString()
        {
            return $"Nationality: {Nationality}, Health Information: {HealthInformation}";
        }

    }
}
