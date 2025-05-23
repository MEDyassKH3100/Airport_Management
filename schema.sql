Build started...
Build succeeded.
CREATE TABLE [MyPlanes] (
    [PlaneId] int NOT NULL IDENTITY,
    [PlaneCapacity] int NOT NULL,
    [ManufactureDate] datetime2 NOT NULL,
    [MyPlaneType] int NOT NULL,
    CONSTRAINT [PK_MyPlanes] PRIMARY KEY ([PlaneId])
);
GO


CREATE TABLE [Passengers] (
    [PassportNumber] nvarchar(7) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    [Name] nvarchar(30) NOT NULL,
    [FullName_LastName] nvarchar(max) NOT NULL,
    [TelNumber] nvarchar(max) NOT NULL,
    [IsTraveller] int NOT NULL,
    [EmployementDate] datetime2 NULL,
    [Function] nvarchar(max) NULL,
    [Salary] float NULL,
    [HealthInformation] nvarchar(max) NULL,
    [Nationality] nvarchar(max) NULL,
    CONSTRAINT [PK_Passengers] PRIMARY KEY ([PassportNumber])
);
GO


CREATE TABLE [flights] (
    [FlightId] int NOT NULL IDENTITY,
    [Comment] nvarchar(max) NOT NULL,
    [Destination] nvarchar(max) NOT NULL,
    [Departure] nvarchar(max) NOT NULL,
    [FlightDate] datetime2 NOT NULL,
    [EffectiveArrival] datetime2 NOT NULL,
    [EstimateDuration] int NOT NULL,
    [PlaneId] int NULL,
    CONSTRAINT [PK_flights] PRIMARY KEY ([FlightId]),
    CONSTRAINT [FK_flights_MyPlanes_PlaneId] FOREIGN KEY ([PlaneId]) REFERENCES [MyPlanes] ([PlaneId]) ON DELETE SET NULL
);
GO


CREATE TABLE [FP] (
    [FlightsFlightId] int NOT NULL,
    [PassengersPassportNumber] nvarchar(7) NOT NULL,
    CONSTRAINT [PK_FP] PRIMARY KEY ([FlightsFlightId], [PassengersPassportNumber]),
    CONSTRAINT [FK_FP_Passengers_PassengersPassportNumber] FOREIGN KEY ([PassengersPassportNumber]) REFERENCES [Passengers] ([PassportNumber]) ON DELETE CASCADE,
    CONSTRAINT [FK_FP_flights_FlightsFlightId] FOREIGN KEY ([FlightsFlightId]) REFERENCES [flights] ([FlightId]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_flights_PlaneId] ON [flights] ([PlaneId]);
GO


CREATE INDEX [IX_FP_PassengersPassportNumber] ON [FP] ([PassengersPassportNumber]);
GO



