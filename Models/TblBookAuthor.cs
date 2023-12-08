using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class TblBookAuthor
{
    public string BookId { get; set; } = null!;

    public string AuthorId { get; set; } = null!;

    public string? Details { get; set; }

    public virtual TblAuthor Author { get; set; } = null!;

    public virtual TblBook Book { get; set; } = null!;
}
