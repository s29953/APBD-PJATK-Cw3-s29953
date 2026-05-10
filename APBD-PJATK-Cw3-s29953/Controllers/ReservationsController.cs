using Microsoft.AspNetCore.Mvc;
using APBD_PJATK_Cw3_s29953.Data;
using APBD_PJATK_Cw3_s29953.Models;

namespace APBD_PJATK_Cw3_s29953.Controllers;

public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetReservations(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var reservations = AppData.Reservations.AsQueryable();

        if (date.HasValue)
        {
            reservations = reservations.Where(r => r.Date == date.Value).AsQueryable();
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            reservations = reservations
                .Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .AsQueryable();
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(r => r.RoomId == roomId.Value).AsQueryable();
        }

        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public ActionResult<Reservation> GetReservation(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> CreateReservation(Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room == null)
        {
            return BadRequest(new
            {
                message = "Room does not exist"
            });
        }

        if (!room.IsActive)
        {
            return BadRequest(new
            {
                message = "Room is inactive"
            });
        }

        bool conflictExists = AppData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (conflictExists)
        {
            return Conflict(new
            {
                message = "Reservation time conflict"
            });
        }

        reservation.Id = AppData.Reservations.Max(r => r.Id) + 1;

        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation(int id, Reservation updatedReservation)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }

        var room = AppData.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

        if (room == null)
        {
            return BadRequest(new
            {
                message = "Room does not exist"
            });
        }

        bool conflictExists = AppData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date == updatedReservation.Date &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime);

        if (conflictExists)
        {
            return Conflict(new
            {
                message = "Reservation time conflict"
            });
        }

        reservation.RoomId = updatedReservation.RoomId;
        reservation.OrganizerName = updatedReservation.OrganizerName;
        reservation.Topic = updatedReservation.Topic;
        reservation.Date = updatedReservation.Date;
        reservation.StartTime = updatedReservation.StartTime;
        reservation.EndTime = updatedReservation.EndTime;
        reservation.Status = updatedReservation.Status;

        return Ok(reservation);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }

        AppData.Reservations.Remove(reservation);

        return NoContent();
    }
}