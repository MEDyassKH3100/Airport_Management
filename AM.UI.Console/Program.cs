// See [https://aka.ms/new-console-template](https://aka.ms/new-console-template) for more information
using System.Linq;
using AM.Core.Domaine;
using AM.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        // === TP1 - Tests des classes du domaine ===
        Console.WriteLine("\n=== TP1 - Tests des classes du domaine ===");

        // Test de la classe Plane
        Console.WriteLine("\nTest de la classe Plane :");
        Plane p = new();
        PlaneType pt = PlaneType.Boing;
        Plane p1 = new(pt, 15, DateTime.Now);
        Console.WriteLine(p1);

        // Test de la classe Passenger
        Console.WriteLine("\nTest de la classe Passenger :");
        Passenger passenger = new Passenger
        {
            PassportNumber = "ABC123",
            FullName = new FullName { FirstName = "John", LastName = "Doe" },
            EmailAddress = "john.doe@example.com",
            BirthDate = new DateTime(1990, 1, 1),
            TelNumber = "123456789",
        };
        Console.WriteLine(passenger);

        // Test de la classe Flight
        Console.WriteLine("\nTest de la classe Flight :");
        Flight testFlight = new Flight
        {
            FlightId = 1,
            Destination = "Paris",
            Departure = "Tunis",
            FlightDate = DateTime.Now,
            EffectiveArrival = DateTime.Now.AddHours(2),
            EstimateDuration = 120,
            MyPlane = p1,
        };
        Console.WriteLine(testFlight);

        // Test du polymorphisme avec Passenger, Staff et Traveller
        Console.WriteLine("\nTest du polymorphisme :");
        Staff staff = new Staff
        {
            PassportNumber = "STAFF123",
            FullName = new FullName { FirstName = "Jane", LastName = "Smith" },
            EmailAddress = "jane.smith@airline.com",
            BirthDate = new DateTime(1985, 5, 15),
            EmployementDate = DateTime.Now.AddYears(-5),
            Function = "Pilot",
            Salary = 5000,
        };

        Traveller traveller = new Traveller
        {
            PassportNumber = "TRAV456",
            FullName = new FullName { FirstName = "Bob", LastName = "Johnson" },
            EmailAddress = "bob.johnson@example.com",
            BirthDate = new DateTime(1995, 10, 20),
            HealthInformation = "No allergies",
            Nationality = "American",
        };

        Console.WriteLine("Staff:");
        Console.WriteLine(staff.GetPassengerType());
        Console.WriteLine("Traveller:");
        Console.WriteLine(traveller.GetPassengerType());

        // === TP2 - Tests des méthodes et extensions ===
        Console.WriteLine("\n=== TP2 - Tests des méthodes et extensions ===");

        // Test de la méthode CheckProfile
        Console.WriteLine("\nTest de la méthode CheckProfile :");
        Console.WriteLine(
            $"CheckProfile(\"John\", \"Doe\") : {passenger.CheckProfile("John", "Doe")}"
        );
        Console.WriteLine(
            $"CheckProfile(\"Jane\", \"Smith\") : {passenger.CheckProfile("Jane", "Smith")}"
        );
        Console.WriteLine(
            $"CheckProfile(\"John\", \"Doe\", \"john.doe@example.com\") : {passenger.CheckProfile("John", "Doe", "john.doe@example.com")}"
        );

        // Test de la méthode CalculateAge
        Console.WriteLine("\nTest de la méthode CalculateAge :");
        int age = passenger.Age;
        Console.WriteLine($"Age de {passenger.FullName.FirstName} : {age} ans");

        // === TP3 - Tests des méthodes LINQ ===
        Console.WriteLine("\n=== TP3 - Tests des méthodes LINQ ===");

        // Création d'une liste de vols pour les tests LINQ
        List<Flight> flightsList = new List<Flight>
        {
            new Flight
            {
                FlightId = 1,
                Destination = "Paris",
                Departure = "Tunis",
                FlightDate = new DateTime(2025, 4, 1, 10, 0, 0),
                EstimateDuration = 120,
            },
            new Flight
            {
                FlightId = 2,
                Destination = "Lyon",
                Departure = "Tunis",
                FlightDate = new DateTime(2025, 4, 2, 15, 30, 0),
                EstimateDuration = 105,
            },
            new Flight
            {
                FlightId = 3,
                Destination = "Paris",
                Departure = "Tunis",
                FlightDate = new DateTime(2025, 4, 3, 8, 45, 0),
                EstimateDuration = 130,
            },
            new Flight
            {
                FlightId = 4,
                Destination = "Rome",
                Departure = "Tunis",
                FlightDate = new DateTime(2025, 4, 4, 12, 0, 0),
                EstimateDuration = 90,
            },
            new Flight
            {
                FlightId = 5,
                Destination = "Madrid",
                Departure = "Tunis",
                FlightDate = new DateTime(2025, 4, 5, 17, 15, 0),
                EstimateDuration = 110,
            },
        };

        // Test des méthodes LINQ pour filtrer les vols par destination
        Console.WriteLine("\nTest des méthodes LINQ (vols vers Paris) :");
        var parisFlightDates = flightsList
            .Where(f => f.Destination == "Paris")
            .Select(f => f.FlightDate);
        foreach (var date in parisFlightDates)
        {
            Console.WriteLine($"Vol vers Paris le : {date:yyyy-MM-dd HH:mm}");
        }

        // Test de la méthode GetFlights avec LINQ
        Console.WriteLine("\nTest des méthodes LINQ (vols de durée estimée > 100 minutes) :");
        var longFlights = flightsList.Where(f => f.EstimateDuration > 100).ToList();
        foreach (var f in longFlights)
        {
            Console.WriteLine(
                $"Vol {f.FlightId} : {f.Departure} -> {f.Destination}, Durée: {f.EstimateDuration} minutes"
            );
        }

        // Test de tri des vols par durée
        Console.WriteLine("\nTest des méthodes LINQ (vols ordonnés par durée) :");
        var orderedFlights = flightsList.OrderBy(f => f.EstimateDuration).ToList();
        foreach (var f in orderedFlights)
        {
            Console.WriteLine(
                $"Vol {f.FlightId} : {f.Departure} -> {f.Destination}, Durée: {f.EstimateDuration} minutes"
            );
        }

        // === TP4 - Tests de la base de données ===
        Console.WriteLine("\n=== TP4 - Tests de la base de données ===");

        // Test de connexion à la base de données
        Console.WriteLine("\nTest de connexion à la base de données :");
        using (var context = new AmContext())
        {
            Console.WriteLine($"Base de données : {context.Database.ProviderName}");
            Console.WriteLine(
                $"État de la connexion : {(context.Database.CanConnect() ? "Connecté" : "Non connecté")}"
            );
        }

        // Test de récupération des entités
        Console.WriteLine("\nTest de récupération des entités :");
        using (var context = new AmContext())
        {
            Console.WriteLine($"Nombre de vols dans la base : {context.flights.Count()}");
            Console.WriteLine($"Nombre d'avions dans la base : {context.planes.Count()}");
            Console.WriteLine($"Nombre de passagers dans la base : {context.passengers.Count()}");
            Console.WriteLine(
                $"Nombre de réservations dans la base : {context.reservations.Count()}"
            );
        }

        // === TP5 - Tests des configurations ===
        Console.WriteLine("\n=== TP5 - Tests des configurations ===");

        // Test des configurations TPH (Table Per Hierarchy)
        Console.WriteLine("\nTest des configurations TPH :");
        using (var context = new AmContext())
        {
            var allPassengers = context.passengers.ToList();
            var staffCount = allPassengers.OfType<Staff>().Count();
            var travellerCount = allPassengers.OfType<Traveller>().Count();

            Console.WriteLine($"Nombre total de passagers : {allPassengers.Count}");
            Console.WriteLine($"Nombre de staff : {staffCount}");
            Console.WriteLine($"Nombre de voyageurs : {travellerCount}");
        }

        // Test des configurations de clés étrangères
        Console.WriteLine("\nTest des configurations de clés étrangères :");
        using (var context = new AmContext())
        {
            var reservations = context.reservations.Take(3).ToList();
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Réservation {reservation.Id} :");
                Console.WriteLine($"  - Siège : {reservation.Seat}");
                Console.WriteLine($"  - Prix : {reservation.Price} euros");
                Console.WriteLine($"  - VIP : {(reservation.VIP ? "Oui" : "Non")}");
                Console.WriteLine($"  - Passager FK : {reservation.PassengerFK}");
                Console.WriteLine($"  - Vol FK : {reservation.FlightFK}");
            }
        }

        // Test d'ajout d'un vol avec un avion
        Console.WriteLine("\nTest d'ajout d'un vol avec un avion :");
        using (var context = new AmContext())
        {
            // Créer un nouvel avion
            var plane6 = new Plane
            {
                Capacity = 150,
                ManufactureDate = DateTime.Now,
                MyPlaneType = PlaneType.Boing,
            };

            // Créer un nouveau vol avec cet avion
            var flight5 = new Flight
            {
                FlightDate = DateTime.Now,
                Destination = "Berlin",
                Departure = "Tunis",
                EffectiveArrival = DateTime.Now.AddHours(3),
                EstimateDuration = 180,
                MyPlane = plane6,
                Comment = "Vol ajouté pour test", // Ajout d'un commentaire pour éviter l'erreur NULL
            };

            // Ajouter le vol à la base de données
            context.flights.Add(flight5);
            context.SaveChanges();

            Console.WriteLine(
                $"Vol ajouté avec succès : ID={flight5.FlightId}, Destination={flight5.Destination}"
            );
        }

        // Test du Lazy Loading
        Console.WriteLine("\nTest du Lazy Loading :");
        using (var context = new AmContext())
        {
            // Récupérer le dernier vol ajouté sans Include() pour tester le LazyLoading
            var lazyFlight = context.flights.OrderByDescending(f => f.FlightId).FirstOrDefault();

            if (lazyFlight != null)
            {
                Console.WriteLine(
                    $"Vol récupéré : {lazyFlight.FlightId} - {lazyFlight.Destination}"
                );

                // Accéder à la propriété de navigation MyPlane pour tester le Lazy Loading
                if (lazyFlight.MyPlane != null)
                {
                    Console.WriteLine("Lazy Loading fonctionne correctement !");
                    Console.WriteLine(
                        $"Avion associé : {lazyFlight.MyPlane.MyPlaneType} (Capacité: {lazyFlight.MyPlane.Capacity})"
                    );
                }
                else
                {
                    Console.WriteLine("Lazy Loading ne fonctionne pas correctement.");
                }
            }
            else
            {
                Console.WriteLine("Aucun vol trouvé dans la base de données.");
            }
        }

        // Test du Eager Loading
        Console.WriteLine("\nTest du Eager Loading :");
        using (var context = new AmContext())
        {
            // Récupérer le dernier vol ajouté avec Include() pour tester l'Eager Loading
            var eagerFlight = context
                .flights.Include(f => f.MyPlane)
                .OrderByDescending(f => f.FlightId)
                .FirstOrDefault();

            if (eagerFlight != null)
            {
                Console.WriteLine(
                    $"Vol récupéré : {eagerFlight.FlightId} - {eagerFlight.Destination}"
                );

                // Accéder à la propriété de navigation MyPlane pour vérifier l'Eager Loading
                if (eagerFlight.MyPlane != null)
                {
                    Console.WriteLine("Eager Loading fonctionne correctement !");
                    Console.WriteLine(
                        $"Avion associé : {eagerFlight.MyPlane.MyPlaneType} (Capacité: {eagerFlight.MyPlane.Capacity})"
                    );
                }
                else
                {
                    Console.WriteLine("Eager Loading ne fonctionne pas correctement.");
                }
            }
            else
            {
                Console.WriteLine("Aucun vol trouvé dans la base de données.");
            }
        }

        // Test des méthodes supplémentaires
        Console.WriteLine("\nTest des méthodes supplémentaires :");

        // Test de la propriété Age
        Console.WriteLine($"Age calculé : {passenger.Age}");

        // Test de la méthode GetAge avec les bons paramètres
        int calculatedAge = 0;
        passenger.GetAge(passenger.BirthDate, calculatedAge);
        Console.WriteLine($"Age calculé par référence : {calculatedAge}");

        // Affichage du nom complet
        Console.WriteLine(
            $"Nom complet : {passenger.FullName.FirstName} {passenger.FullName.LastName}"
        );

        // Test des méthodes de projection LINQ
        var destinations = flightsList.Select(f => f.Destination).Distinct().ToList();
        Console.WriteLine("\nDestinations uniques :");
        foreach (var dest in destinations)
        {
            Console.WriteLine($" - {dest}");
        }

        // Test des méthodes d'agrégation LINQ
        var avgDuration = flightsList.Average(f => f.EstimateDuration);
        var maxDuration = flightsList.Max(f => f.EstimateDuration);
        var minDuration = flightsList.Min(f => f.EstimateDuration);
        Console.WriteLine($"\nDurée moyenne des vols : {avgDuration} minutes");
        Console.WriteLine($"Durée maximale des vols : {maxDuration} minutes");
        Console.WriteLine($"Durée minimale des vols : {minDuration} minutes");

        // Test des requêtes complexes
        Console.WriteLine("\nTest des requêtes complexes :");
        using (var context = new AmContext())
        {
            if (context.Database.CanConnect())
            {
                // Test des relations many-to-many
                var flightsWithPassengers = context
                    .flights.Include(f => f.Passengers)
                    .Where(f => f.Passengers.Any())
                    .Take(3)
                    .ToList();

                if (flightsWithPassengers.Any())
                {
                    Console.WriteLine("\nVols avec passagers :");
                    foreach (var flight in flightsWithPassengers)
                    {
                        Console.WriteLine(
                            $"Vol {flight.FlightId} vers {flight.Destination} avec {flight.Passengers.Count} passagers"
                        );
                    }
                }
                else
                {
                    Console.WriteLine("Aucun vol avec passagers trouvé.");
                }
            }
            else
            {
                Console.WriteLine(
                    "Impossible de se connecter à la base de données pour les tests complexes."
                );
            }
        }

        Console.WriteLine("\nTests terminés. Appuyez sur une touche pour quitter...");
        Console.ReadKey();
    }
}
