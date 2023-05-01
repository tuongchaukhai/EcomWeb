using System;
using System.Collections.Generic;

namespace EcomWeb.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public double PurchasedPrice { get; set; }

    public int Quantity { get; set; }

    public double? Discount { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
