using ProductApi.Models;

namespace ProductApi.DataStructures.Tree
{
    public class ProductPriceTree
    {
        public TreeNode Root;

        public void Insert(Product product)
        {
            Root = Insert(Root, product);
        }

        private TreeNode Insert(TreeNode root, Product product)
        {
            if (root == null)
                return new TreeNode(product);

            if (product.Price < root.Data.Price)
                root.Left = Insert(root.Left, product);
            else
                root.Right = Insert(root.Right, product);

            return root;
        }

        public List<Product> ToList()
        {
            var result = new List<Product>();
            InOrderTraversal(Root, result);
            return result;
        }

        private void InOrderTraversal(TreeNode node, List<Product> result)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, result);
            result.Add(node.Data);
            InOrderTraversal(node.Right, result);
        }
    }
}