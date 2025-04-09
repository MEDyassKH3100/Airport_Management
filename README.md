# Airport_Management
Application de gestion des activités d'un aéroport implémentant les principes de la POO, LINQ, et Entity Framework Core.
## 📦 Structure du Projet
📦 AirportManagement/

├── 📁 AM.Core.Domain/ # Modèles (Passenger, Flight, Plane...)

├── 📁 AM.Core.Services/ # Services métier (FlightService, IFlightService)

├── 📁 AM.Data/ # Persistance (EF Core, DbContext, Configurations)

└── 📁 AM.ULConsole/ # Interface Console (Program.cs)
## 🛠 Technologies
- .NET 6
- Entity Framework Core 6.0.9
- SQL Server (LocalDB)
- LINQ

## 🚀 Fonctionnalités
1. **Partie 1** :  
   - Modélisation des classes (héritage, polymorphisme).  
   - Tests de méthodes (`CheckProfile`, `GetPassengerType`).  

2. **Partie 2** :  
   - Requêtes LINQ (`GetFlightDates`, `GetThreeOlderTravellers`).  

3. **Parties 3/4/5** :  
   - Configurations EF Core (annotations, Fluent API).  
   - Stratégies d'héritage (TPH, TPT).  
   - Lazy loading et migrations.  

## 📌 Exemple de Code
```csharp
// Exemple de requête LINQ (Partie 2)
public IList<Flight> GetFlights(string filterType, string filterValue) {
    return Flights.Where(f => f.GetType().GetProperty(filterType).GetValue(f).ToString() == filterValue).ToList();
}
