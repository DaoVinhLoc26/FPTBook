using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class User
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? TaxCode { get; set; }

    public string? Details { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
