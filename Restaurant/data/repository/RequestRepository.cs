using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class RequestRepository
{
    private readonly RestaurantDbContext _context;

    public RequestRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddRequest(Request request)
    {
        _context.Requests.Add(request);
        _context.SaveChanges();
    }

    // READ
    public List<Request> GetRequests()
    {
        return _context.Requests
    .Include(w => w.Department)  // Загрузка связанной сущности Product
    .ToList();
    }

    public Request GetRequestById(int requestId)
    {
        return _context.Requests.Find(requestId);
    }
    public int GetMaxRequestId()
    {
        // Получаем максимальный ID из базы данных
        var maxId = _context.Requests.Max(d => (int?)d.RequestID) ?? 0;
        return maxId;
    }
    // UPDATE
    public void UpdateRequest(Request updatedRequest)
    {
        var existingRequest = _context.Requests.Find(updatedRequest);

        if (existingRequest != null)
        {
            existingRequest.DepartmentID = updatedRequest.DepartmentID;
            existingRequest.RequestDate = updatedRequest.RequestDate;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteRequest(int requestId)
    {
        var requestToDelete = _context.Requests.Find(requestId);

        if (requestToDelete != null)
        {
            _context.Requests.Remove(requestToDelete);
            _context.SaveChanges();
        }
    }
}
