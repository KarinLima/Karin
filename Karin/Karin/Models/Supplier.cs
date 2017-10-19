using System.Collections.Generic;

namespace Karin.Models
{
    public class Supplier
    {
        public long SupplierId { get; set; }
        public string Name { get; set; }
        public object Nome { get; internal set; }

        public virtual ICollection<Product> Products { get; set;}
    }
}