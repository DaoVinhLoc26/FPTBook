using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class TblCustomer
{
    public string CustomerEmail { get; set; } = null!;

    public string CustomerPassword { get; set; } = null!;

    public string CustomerFullname { get; set; } = null!;

    public string? CustomerAddress { get; set; }

    public string? CustomerPhone { get; set; }
}
