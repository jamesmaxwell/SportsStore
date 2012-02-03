using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        List<CartLine> cartLines = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine cartLine = cartLines.FirstOrDefault(c => c.Product.ProductID == product.ProductID);
            if (cartLine == null)
            {
                cartLines.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            cartLines.RemoveAll(c => c.Product.ProductID == product.ProductID);
        }

        public void Clear()
        {
            cartLines.Clear();
        }

        public decimal ComputeTotalValue()
        {
            return cartLines.Sum(c => c.Product.Price * c.Quantity);
        }

        public IEnumerable<CartLine> Lines
        {
            get { return cartLines; }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
