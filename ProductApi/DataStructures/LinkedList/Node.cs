using ProductApi.Models;

namespace ProductApi.DataStructures.LinkedList
{
    public class Node
    {
        public Product Data { get; set; }
        public Node Next { get; set; }

        public Node(Product data)
        {
            Data = data;
        }
    }
}
