using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class UnitOfMeasureRepository
{
    private readonly RestaurantDbContext _context;

    public UnitOfMeasureRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddUnitOfMeasure(UnitOfMeasure unitOfMeasure)
    {
        _context.UnitsOfMeasure.Add(unitOfMeasure);
        _context.SaveChanges();
    }

    // READ
    public List<UnitOfMeasure> GetUnitsOfMeasure()
    {
        return _context.UnitsOfMeasure.ToList();
    }

    public UnitOfMeasure GetUnitOfMeasureById(int unitOfMeasureId)
    {
        return _context.UnitsOfMeasure.Find(unitOfMeasureId);
    }

    // UPDATE
    public void UpdateUnitOfMeasure(UnitOfMeasure updatedUnitOfMeasure)
    {
        var existingUnitOfMeasure = _context.UnitsOfMeasure.Find(updatedUnitOfMeasure);

        if (existingUnitOfMeasure != null)
        {
            existingUnitOfMeasure.UnitOfMeasureName = updatedUnitOfMeasure.UnitOfMeasureName;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteUnitOfMeasure(int unitOfMeasureId)
    {
        var unitOfMeasureToDelete = _context.UnitsOfMeasure.Find(unitOfMeasureId);

        if (unitOfMeasureToDelete != null)
        {
            _context.UnitsOfMeasure.Remove(unitOfMeasureToDelete);
            _context.SaveChanges();
        }
    }
}
