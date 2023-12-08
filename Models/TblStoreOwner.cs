using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class TblStoreOwner
{
    public string OwnerId { get; set; } = null!;

    public string OwnerName { get; set; } = null!;

    public string OwnerAddress { get; set; } = null!;

    public string? OwnerPhone { get; set; }

    public string? OwnerTaxCode { get; set; }

    public string? OwnerDetails { get; set; }

    public virtual ICollection<TblBook> TblBooks { get; set; } = new List<TblBook>();
}
