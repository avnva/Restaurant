﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Restaurant.app.model;

namespace Restaurant.repository;

public class DishRepository
{
    private readonly RestaurantDbContext _context;

    public DishRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddDish(Dish dish)
    {
        _context.Dishes.Add(dish);
        _context.SaveChanges();
    }

    // READ
    public List<Dish> GetDishes()
    {
        return _context.Dishes.ToList();
    }

    public Dish GetDishById(int dishId)
    {
        return _context.Dishes.Find(dishId);
    }

    public int GetMaxDishId()
    {
        // Получаем максимальный ID из базы данных
        var maxId = _context.Dishes.Max(d => (int?)d.DishID) ?? 0;
        return maxId;
    }

    // UPDATE
    public void UpdateDish(Dish updatedDish)
    {
        var existingDish = _context.Dishes.Find(updatedDish.DishID);

        if (existingDish != null)
        {
            existingDish.GroupID = updatedDish.GroupID;
            existingDish.DishName = updatedDish.DishName;
            existingDish.DishCost = updatedDish.DishCost;
            existingDish.OutputWeight = updatedDish.OutputWeight;
            existingDish.CookingTechnology = updatedDish.CookingTechnology;
            existingDish.Photo = updatedDish.Photo;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteDish(int dishId)
    {
        var dishToDelete = _context.Dishes.Find(dishId);

        if (dishToDelete != null)
        {
            _context.Dishes.Remove(dishToDelete);
            _context.SaveChanges();
        }
    }
}
