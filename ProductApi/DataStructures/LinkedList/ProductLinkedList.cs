using ProductApi.Models;

namespace ProductApi.DataStructures.LinkedList
{
    public class ProductLinkedList
    {
        private LinkedList<Product> _products;

        public ProductLinkedList()
        {
            _products = new LinkedList<Product>();
        }

        public void Add(Product product)
        {
            _products.AddLast(product);
        }

        public Product? GetByIndex(int index)
        {
            if (index < 0 || index >= _products.Count)
                return null;

            var current = _products.First;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex == index)
                    return current.Value;

                current = current.Next;
                currentIndex++;
            }

            return null;
        }

        public List<Product> ToList()
        {
            return _products.ToList();
        }

        public void RemoveById(int id)
        {
            var current = _products.First;

            while (current != null)
            {
                if (current.Value.Id == id)
                {
                    _products.Remove(current);
                    return;
                }
                current = current.Next;
            }
        }
    }
}
