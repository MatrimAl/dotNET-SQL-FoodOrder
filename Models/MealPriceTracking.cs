using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class MealPriceTracking
{
    public int Id { get; set; }

    public int MealId { get; set; }

    public decimal Price { get; set; }

    public byte[]? Date { get; set; }

    public virtual Meal Meal { get; set; } = null!;
}
