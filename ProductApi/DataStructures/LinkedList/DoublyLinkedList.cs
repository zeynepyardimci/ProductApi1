using System;
using System.Collections.Generic;
using ProductApi.Models; 

namespace ProductApi.DataStructures.LinkedList
{
    public class DoublyLinkedList
    {
        private class Node
        {
            public Product Data;
            public Node Next;
            public Node Prev;

            public Node(Product data)
            {
                Data = data;
                Next = null;
                Prev = null;
            }
        }

        private Node head;
        private Node tail;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
        }

        public void Add(Product product)
        {
            var newNode = new Node(product);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
        }

        public Product GetLast()
        {
            if (tail == null) return null;
            return tail.Data;
        }

        public List<Product> ToList()
        {
            var list = new List<Product>();
            var current = head;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }

        public DoublyLinkedList Reverse()
        {
            var reversedList = new DoublyLinkedList();
            var current = tail;
            while (current != null)
            {
                reversedList.Add(current.Data);
                current = current.Prev;
            }
            return reversedList;
        }
    }
}