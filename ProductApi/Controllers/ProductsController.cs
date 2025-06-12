using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.DataStructures.LinkedList;
using ProductApi.DataStructures.PriorityQueue;
using System.Linq;
using ProductApi.DataStructures.Tree;
using ProductApi.DataStructures.Stack;
using ProductApi.Services;


namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        private readonly ProductDataInitializer _data;
        private readonly ProductService _productService;

        public ProductsController(ProductDbContext context, ProductDataInitializer data, ProductService productService)
        {
            _context = context;
            _data = data;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
        }

        //Soru1: İsmi A ile başlayan ürünleri getir
        [HttpGet("startswith-a")]
        public IActionResult GetProductsStartingWit()
        {
            var products = _context.Products.ToList();
            var queue = new PriorityProductQueue();

            foreach (var product in products)
            {
                if (product.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase))
                {
                    queue.Enqueue(product);
                }
            }

            var result = queue.ToList();
            return Ok(result);
        }

        //Soru1.1: PriorityProductQueue sınıfını kullanarak, A ile başlayan ürünler arasından en pahalı olanı getir
        [HttpGet("most-expensive-a-priority")]
        public IActionResult GetMostExpensiveAWithPriorityQueue()
        {
            var products = _context.Products.ToList();
            var queue = new PriorityProductQueue();
            foreach (var product in products)
            {
                if (product.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase))
                {
                    queue.Enqueue(product); // En pahalıdan en ucuza doğru sıraya ekleniyor
                }
            }
            if (queue.IsEmpty())
                return NotFound("A harfiyle başlayan ürün bulunamadı.");
            var mostExpensive = queue.Dequeue(); // En pahalı ürün ilk sırada
            return Ok(mostExpensive);
        }

        //Soru2: veritabanındaki ürünleri bağlı listeye aktar ve 2. elemanı döndür
        [HttpGet("second-product")]
        public IActionResult GetSecondProduct()
        {
            var products = _context.Products.ToList();
            var list = new ProductLinkedList();
            foreach (var product in products)
            {
                list.Add(product);
            }
            var secondProduct = list.GetByIndex(1); // index 1 = 2. ürün
            if (secondProduct == null)
                return NotFound("2. ürün bulunamadı.");
            return Ok(secondProduct);
        }

        // Soru2.1: Ürünleri DoublyLinkedList'e aktar ve sondan 1. elemanı getir
        [HttpGet("doubly-last-product")]
        public IActionResult GetLastProductFromDoublyLinkedList()
        {
            var products = _context.Products.ToList();
            var doublyList = new DoublyLinkedList();

            foreach (var product in products)
            {
                doublyList.Add(product);
            }

            var lastProduct = doublyList.GetLast();

            if (lastProduct == null)
                return NotFound("Sondan 1. ürün bulunamadı.");

            return Ok(lastProduct);
        }

        // Soru2.2: DoublyLinkedList'i ters çevir ve sonucu döndür
        [HttpGet("doubly-reverse")]
        public IActionResult GetReversedDoublyLinkedList()
        {
            var products = _context.Products.ToList();
            var doublyList = new DoublyLinkedList();

            foreach (var product in products)
            {
                doublyList.Add(product);
            }

            var reversedList = doublyList.Reverse();

            return Ok(reversedList.ToList());
        }

        //Soru 3: Ürünleri isme göre ikili arama ağacına yerleştir ve "Avocado" ürününü bul.
        [HttpGet("search-avocado")]
        public IActionResult SearchAvocado()
        {
            var bst = new BinarySearchTree();

            // Örnek ürünler (Gerçek projede veritabanından çekebilirsin)
            bst.Insert(new Product("Muz"));
            bst.Insert(new Product("Elma"));
            bst.Insert(new Product("Avocado"));
            bst.Insert(new Product("Portakal"));

            var foundProduct = bst.Search("Avocado");

            if (foundProduct != null)
                return Ok($"Ürün bulundu: {foundProduct.Name}");
            else
                return NotFound("Ürün bulunamadı.");
        }

        // Soru 3.1: Ürünleri ikili arama ağacına yerleştir ve "istenen" ürününü bul
        [HttpGet("search/{name}")]
        public IActionResult SearchByName(string name)
        {
            var product = _data.ProductTree.Search(name);
            return product == null ? NotFound() : Ok(product);
        }

        // Soru 4: Ağaç yapısı kullanarak Ürünleri fiyatlarına göre sıralı bir şekilde listele
        [HttpGet("sorted-by-price")]
        public IActionResult GetSortedByPrice()
        {
            var priceTree = new ProductPriceTree();
            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                priceTree.Insert(product);
            }
            return Ok(priceTree.ToList());
        }

        // Soru 5: Ağaç yapısı kullanarak Ürünleri isimlerine göre sıralı bir şekilde listele
        [HttpGet("sorted-by-name")]
        public IActionResult GetSortedByName()
        {
            var nameTree = new ProductTree(); // isme göre sıralı tree zaten vardı
            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                nameTree.Insert(product);
            }
            var sortedList = nameTree.ToList();
            return Ok(sortedList);
        }

        // Soru 6: Stack kullanarak En son eklenen ürünü göster
        [HttpGet("last-added")]
        public IActionResult GetLastAddedProduct()
        {
            var lastProduct = _data.ProductStack.Peek();
            if (lastProduct == null)
                return NotFound("Henüz ürün eklenmedi.");
            return Ok(lastProduct);
        }

        // Soru 7: LinkedList kullanarak herhangi bir ürünü silme işlemi
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedProductName = _productService.DeleteProductById(id);

            if (deletedProductName != null)
                return Ok(new { message = $"Ürün: '{deletedProductName}' başarıyla silindi." });
            else
                return NotFound(new { message = $"Ürün: '{deletedProductName}' bulunamadı." });
        }

    }
}