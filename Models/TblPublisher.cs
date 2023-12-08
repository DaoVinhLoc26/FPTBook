using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class TblPublisher
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public string? PublisherAddress { get; set; }

    public string? PublisherDetails { get; set; }

    public virtual ICollection<TblBook> TblBooks { get; set; } = new List<TblBook>();
}
