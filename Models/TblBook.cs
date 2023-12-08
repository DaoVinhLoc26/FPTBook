 using System;
using System.Collections.Generic;

namespace DeMoGCS10035.Models;

public partial class TblBook
{
    public string BookId { get; set; } = null!;

    public string BookTitle { get; set; } = null!;

    public int BookPrice { get; set; }

    public string? BookDetailes { get; set; }

    public int CatId { get; set; }

    public string OwnerId { get; set; } = null!;

    public int PublisherId { get; set; }

    public virtual TblCategory Cat { get; set; } = null!;

    public virtual TblStoreOwner Owner { get; set; } = null!;

    public virtual TblPublisher Publisher { get; set; } = null!;

    public virtual ICollection<TblBookAuthor> TblBookAuthors { get; set; } = new List<TblBookAuthor>();
}
