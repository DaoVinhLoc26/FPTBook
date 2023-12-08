using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class TblCategory
{
    public int CatId { get; set; }

    public string CatName { get; set; } = null!;

    public string? CatDetails { get; set; }

    public virtual ICollection<TblBook> TblBooks { get; set; } = new List<TblBook>();
}
