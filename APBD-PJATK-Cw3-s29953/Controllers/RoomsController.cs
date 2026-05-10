using Microsoft.AspNetCore.Mvc;
using APBD_PJATK_Cw3_s29953.Data;
using APBD_PJATK_Cw3_s29953.Models;

namespace APBD_PJATK_Cw3_s29953.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetRooms(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = AppData.Rooms.AsQueryable();

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value).AsQueryable();
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value).AsQueryable();
        }

        if (activeOnly == true)
        {
            rooms = rooms.Where(r => r.IsActive).AsQueryable();
        }

        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public ActionResult<Room> GetRoom(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetRoomsByBuilding(string buildingCode)
    {
        var rooms = AppData.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<Room> CreateRoom(Room room)
    {
        room.Id = AppData.Rooms.Max(r => r.Id) + 1;

        AppData.Rooms.Add(room);

        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, Room updatedRoom)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        room.Name = updatedRoom.Name;
        room.BuildingCode = updatedRoom.BuildingCode;
        room.Floor = updatedRoom.Floor;
        room.Capacity = updatedRoom.Capacity;
        room.HasProjector = updatedRoom.HasProjector;
        room.IsActive = updatedRoom.IsActive;

        return Ok(room);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        bool hasReservations = AppData.Reservations.Any(r => r.RoomId == id);

        if (hasReservations)
        {
            return Conflict(new
            {
                message = "Cannot delete room with existing reservations"
            });
        }

        AppData.Rooms.Remove(room);

        return NoContent();
    }
}