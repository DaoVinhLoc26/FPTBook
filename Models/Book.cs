using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class Book
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int Price { get; set; }

    public string? Detailes { get; set; }

    public string? Imagel1 { get; set; }

    public string? Imagel2 { get; set; }

    public string? Imagel3 { get; set; }

    public string? Imagel4 { get; set; }

    public string? Imagel5 { get; set; }

    public int CatId { get; set; }

    public string AuthorId { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public int PublisherId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Cat { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;
}
