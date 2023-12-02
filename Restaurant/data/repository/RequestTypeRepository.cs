using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class RequestTypeRepository
{
    private readonly RestaurantDbContext _context;

    public RequestTypeRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddRequestType(RequestType requestType)
    {
        _context.RequestTypes.Add(requestType);
        _context.SaveChanges();
    }

    // READ
    public List<RequestType> GetRequestTypes()
    {
        return _context.RequestTypes.ToList();
    }

    public RequestType GetRequestTypeById(int requestTypeId)
    {
        return _context.RequestTypes.Find(requestTypeId);
    }

    // UPDATE
    public void UpdateRequestType(RequestType updatedRequestType)
    {
        var existingRequestType = _context.RequestTypes.Find(updatedRequestType);

        if (existingRequestType != null)
        {
            existingRequestType.RequestTypesName = updatedRequestType.RequestTypesName;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteRequestType(int requestTypeId)
    {
        var requestTypeToDelete = _context.RequestTypes.Find(requestTypeId);

        if (requestTypeToDelete != null)
        {
            _context.RequestTypes.Remove(requestTypeToDelete);
            _context.SaveChanges();
        }
    }
}
