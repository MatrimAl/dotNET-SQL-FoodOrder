using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
