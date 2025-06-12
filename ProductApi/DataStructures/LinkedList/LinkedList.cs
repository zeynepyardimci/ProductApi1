using ProductApi.Models;

namespace ProductApi.DataStructures.LinkedList
{
    public class LinkedList
    {
        public Node Head;

        public void Add(Product p)
        {
            var newNode = new Node(p);
            if (Head == null)
            {
                Head = newNode;
                return;
            }

            Node temp = Head;
            while (temp.Next != null)
                temp = temp.Next;
            temp.Next = newNode;
        }

        public Product GetAt(int index)
        {
            int count = 0;
            Node temp = Head;
            while (temp != null)
            {
                if (count == index)
                    return temp.Data;
                temp = temp.Next;
                count++;
            }
            return null;
        }
    }
}
