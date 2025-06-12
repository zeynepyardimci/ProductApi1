using ProductApi.Models;

namespace ProductApi.DataStructures.PriorityQueue
{
    public class PriorityProductQueue
    {
        private List<Product> queue = new();

        public void Enqueue(Product product)
        {
            queue.Add(product);
            queue = queue.OrderByDescending(p => p.Price).ToList(); // Fiyata göre sıralama
            //queue.Sort(); // IComparable sayesinde isme göre sıralar
        }

        public Product Dequeue()
        {
            if (queue.Count == 0) return null;
            var item = queue[0];
            queue.RemoveAt(0);
            return item;
        }

        public List<Product> ToList()
        {
            return queue;
        }

        public bool IsEmpty()
        {
            return queue.Count == 0;
        }
    }
}
