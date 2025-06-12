using ProductApi.Models;

namespace ProductApi.DataStructures.Tree
{
    public class ProductTree
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

            if (product.CompareTo(root.Data) < 0)
                root.Left = Insert(root.Left, product);
            else
                root.Right = Insert(root.Right, product);

            return root;
        }

        public Product Search(string name)
        {
            return Search(Root, name);
        }

        private Product Search(TreeNode root, string name)
        {
            if (root == null) return null;

            if (string.Equals(root.Data.Name, name, StringComparison.OrdinalIgnoreCase))
                return root.Data;

            if (string.Compare(name, root.Data.Name, StringComparison.OrdinalIgnoreCase) < 0)
                return Search(root.Left, name);
            else
                return Search(root.Right, name);
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

        public void Clear()
        {
            Root = null;
        }
    }
}