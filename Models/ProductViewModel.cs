using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeMoGCS10035.Models
{
    public class ProductViewModel
    {
        public Book Book { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Publishers { get; set; }
    }
}
