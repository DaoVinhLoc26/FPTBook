using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Details { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
