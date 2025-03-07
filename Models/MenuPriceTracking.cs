using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class MenuPriceTracking
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public decimal Price { get; set; }

    public byte[]? Date { get; set; }

    public virtual Menu Menu { get; set; } = null!;
}
