using Restaurant.app.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.data.repository;

public class DepartmentRepository
{
    private readonly RestaurantDbContext _context;

    public DepartmentRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddDepartment(Department department)
    {
        _context.Departments.Add(department);
        _context.SaveChanges();
    }

    // READ
    public List<Department> GetDepartments()
    {
        return _context.Departments.ToList();
    }

    public Department GetDepartmentsById(int departmentId)
    {
        return _context.Departments.Find(departmentId);
    }

    // UPDATE
    public void UpdateDepartment(Department updatedDepartment)
    {
        var existingDepartment = _context.Departments.Find(updatedDepartment);

        if (existingDepartment != null)
        {
            existingDepartment.DepartmentName = updatedDepartment.DepartmentName;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteDepartment(int departmentId)
    {
        var departmentToDelete = _context.Departments.Find(departmentId);

        if (departmentToDelete != null)
        {
            _context.Departments.Remove(departmentToDelete);
            _context.SaveChanges();
        }
    }
}
