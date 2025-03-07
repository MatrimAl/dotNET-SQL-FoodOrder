using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Meal
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int CategoryId { get; set; }

    public string? MealImage { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<MealPriceTracking> MealPriceTrackings { get; set; } = new List<MealPriceTracking>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<OrderDetail> Orders { get; set; } = new List<OrderDetail>();
}
