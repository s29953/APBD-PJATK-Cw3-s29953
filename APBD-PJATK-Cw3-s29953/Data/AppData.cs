using APBD_PJATK_Cw3_s29953.Models;

namespace APBD_PJATK_Cw3_s29953.Data;

public static class AppData
{
    public static List<Room> Rooms { get; set; } = new()
    {
        new Room
        {
            Id = 1,
            Name = "Room A101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Room A102",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 15,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Lab B201",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Conference A301",
            BuildingCode = "A",
            Floor = 3,
            Capacity = 40,
            HasProjector = true,
            IsActive = false
        },
        new Room
        {
            Id = 5,
            Name = "Workshop B202",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 18,
            HasProjector = false,
            IsActive = true
        }
    };

    public static List<Reservation> Reservations { get; set; } = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Adam Kowalski",
            Topic = "REST API Workshop",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Jan Nowak",
            Topic = "ASP.NET Core Basics",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Anna Iksińska",
            Topic = "Docker Training",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(15, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 1,
            OrganizerName = "Tomasz Igrekowski",
            Topic = "HTTP Consultation",
            Date = new DateOnly(2026, 5, 13),
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(9, 30),
            Status = "cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 5,
            OrganizerName = "Pankracy Zetowski",
            Topic = "Frontend Workshop",
            Date = new DateOnly(2026, 5, 14),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(16, 0),
            Status = "confirmed"
        }
    };
}