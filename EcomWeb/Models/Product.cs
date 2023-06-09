﻿using System;
using System.Collections.Generic;

namespace EcomWeb.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ShortDesc { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public double? Discount { get; set; }

    public string? Thumb { get; set; }

    public string? Video { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? LastModified { get; set; }

    public bool BestSeller { get; set; }

    public bool HomeFlag { get; set; }

    public bool Active { get; set; }

    public string? Tags { get; set; }

    public string? Alias { get; set; }

    public string? MetaDesc { get; set; }

    public string? MetaKey { get; set; }

    public int UnitInStock { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
