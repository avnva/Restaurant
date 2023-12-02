using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class StatusRepository
{
    private readonly RestaurantDbContext _context;

    public StatusRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddStatus(Status status)
    {
        _context.Statuses.Add(status);
        _context.SaveChanges();
    }

    // READ
    public List<Status> GetStatuses()
    {
        return _context.Statuses.ToList();
    }

    public Status GetStatusById(int statusId)
    {
        return _context.Statuses.Find(statusId);
    }

    // UPDATE
    public void UpdateStatus(Status updatedStatus)
    {
        var existingStatus = _context.Statuses.Find(updatedStatus);

        if (existingStatus != null)
        {
            existingStatus.StatusName = updatedStatus.StatusName;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteStatus(int statusId)
    {
        var statusToDelete = _context.Statuses.Find(statusId);

        if (statusToDelete != null)
        {
            _context.Statuses.Remove(statusToDelete);
            _context.SaveChanges();
        }
    }
}
