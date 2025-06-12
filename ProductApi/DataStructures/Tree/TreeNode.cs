using ProductApi.Models;

namespace ProductApi.DataStructures.Tree
{
    public class TreeNode
    {
        public Product Data;
        public TreeNode Left;
        public TreeNode Right;
        public TreeNode()
        {
            Data = null!;
            Left = null;
            Right = null;
        }

        public TreeNode(Product data)
        {
            Data = data;
        }
    }
}
