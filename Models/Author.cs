using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class Author
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Adress { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
