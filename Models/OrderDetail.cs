using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public byte[] OrderDate { get; set; } = null!;

    public int? OrderKuryeId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public virtual User? OrderKurye { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
