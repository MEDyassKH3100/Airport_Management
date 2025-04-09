using System.ComponentModel.DataAnnotations;

namespace AM.Core.Domaine
{
    public class Plane
    {
        [Range(0, int.MaxValue, ErrorMessage = "Capacity must be a positive number.")]
        public int Capacity { get; set; }

        public DateTime ManufactureDate { get; set; }

        public int PlaneId { get; set; }
        public PlaneType MyPlaneType { get; set; }

        public virtual IList<Flight> Flights { get; set; }

        public Plane() { }

        public Plane(PlaneType pt, int capacity, DateTime date)
        {
            this.Capacity = capacity;
            this.MyPlaneType = pt;
            this.ManufactureDate = date;
        }

        public override string ToString()
        {
            return $"PlaneId: {PlaneId}, Type: {MyPlaneType}, Capacity: {Capacity}, "
                + $"ManufactureDate: {ManufactureDate:yyyy-MM-dd}, Flights Count: {Flights?.Count ?? 0}";
        }
    }
}
