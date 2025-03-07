using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? MenuImage { get; set; }

    public virtual ICollection<MenuPriceTracking> MenuPriceTrackings { get; set; } = new List<MenuPriceTracking>();

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<OrderDetail> Orders { get; set; } = new List<OrderDetail>();
}
