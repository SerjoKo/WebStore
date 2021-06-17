using System.Collections.Generic;
using System.Linq;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantitie)> Items { get; set; }
        public int ItemsCount => Item?.Sum(Item => Item.Quantitie) ?? 0;
        public decimal TotalPrice => Item?.Sum(Item => Item.Product.Price * Item.Quantitie) ?? 0m;
    }
}
