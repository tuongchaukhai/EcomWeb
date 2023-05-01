using System;
using System.Collections.Generic;

namespace EcomWeb.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime? ShippingDate { get; set; }

    public bool Paid { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Note { get; set; }

    public int TransactStatusId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
