using ProductApi.Models;
using System.Collections.Generic;

namespace ProductApi.DataStructures.Stack
{
    public class ProductStack
    {
        private readonly Stack<Product> _products = new Stack<Product>();

        // Veritabanından ürünleri yükeleyen metod
        public void LoadFromDatabase(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                _products.Push(product);
            }
        }

        // Stack'e yeni ürün ekle
        public void Push(Product product)
        {
            _products.Push(product);
        }

        // Stack'in tepesindeki ürünü çıkar ve döndür
        public Product? Pop()
        {
            if (_products.Count == 0)
                return null;
            return _products.Pop();
        }

        // Stack'in tepesindeki ürünü görüntüle
        public Product? Peek()
        {
            return _products.Count > 0 ? _products.Peek() : null;
        }

        // Stack boş mu kontrolü
        public bool IsEmpty()
        {
            return _products.Count == 0;
        }

        // Stack'teki ürün sayısı
        public int Count => _products.Count;
    }
}

