using ProductApi.Data;
using ProductApi.DataStructures.LinkedList;
using ProductApi.DataStructures.Tree;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService
    {
        private readonly ProductDbContext _context;
        private readonly ProductLinkedList _productLinkedList;
        private readonly ProductTree _productTree;

        public ProductService(ProductDbContext context, ProductLinkedList productLinkedList)
        {
            _context = context;
            _productLinkedList = productLinkedList;
            _productTree = new ProductTree();
            InitializeTree();
        }

        public void InitializeTree()
        {
            _productTree.Clear();

            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                _productTree.Insert(product);
            }
        }

        public Product Search(string name)
        {
            return _productTree.Search(name);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            _productTree.Insert(product);
        }

        public string? DeleteProductById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                // Veritabanında ürün yok, silme işlemi başarısız
                return null;
            }

            // Veritabanından sil
            _context.Products.Remove(product);
            _context.SaveChanges();

            // Bellekteki linked list'ten sil
            _productLinkedList.RemoveById(id);

            return product.Name;
        }
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

    }
}
