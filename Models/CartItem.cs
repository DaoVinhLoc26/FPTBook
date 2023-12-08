using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public string? BookId { get; set; }

    public int? CartId { get; set; }

    public int Quantity { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Cart? Cart { get; set; }
}
