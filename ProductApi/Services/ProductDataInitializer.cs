using ProductApi.Data;
using ProductApi.DataStructures.LinkedList;
using ProductApi.DataStructures.Stack;
using ProductApi.DataStructures.Tree;

namespace ProductApi.Services
{
    public class ProductDataInitializer
    {
        private readonly ProductDbContext _context;

        public ProductStack ProductStack { get; private set; }
        public ProductTree ProductTree { get; private set; }
        private readonly ProductLinkedList _linkedList;

        public ProductDataInitializer(ProductDbContext context, ProductLinkedList linkedList)
        {
            _context = context;
            _linkedList = linkedList;
            ProductStack = new ProductStack();
            ProductTree = new ProductTree();

            LoadData();
        }

        private void LoadData()
        {
            var products = _context.Products.ToList();

            foreach (var product in products)
            {
                ProductStack.Push(product);
                ProductTree.Insert(product);
                _linkedList.Add(product);
            }
        }
    }
}
