using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class TblAuthor
{
    public string AuthorId { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public string? AuthorAdress { get; set; }

    public string? AuthorEmail { get; set; }

    public virtual ICollection<TblBookAuthor> TblBookAuthors { get; set; } = new List<TblBookAuthor>();
}
