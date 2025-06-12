using ProductApi.DataStructures.Tree;
using ProductApi.Models;
public class BinarySearchTree
{
    public TreeNode Root { get; private set; }

    public void Insert(Product product)
    {
        Root = InsertRecursive(Root, product);
    }

    private TreeNode InsertRecursive(TreeNode node, Product product)
    {
        if (node == null)
            return new TreeNode(product);

        int comparison = string.Compare(product.Name, node.Data.Name, StringComparison.OrdinalIgnoreCase);

        if (comparison < 0)
            node.Left = InsertRecursive(node.Left, product);
        else if (comparison > 0)
            node.Right = InsertRecursive(node.Right, product);

        return node;
    }

    public Product Search(string name)
    {
        return SearchRecursive(Root, name);
    }

    private Product SearchRecursive(TreeNode node, string name)
    {
        if (node == null)
            return null;

        int comparison = string.Compare(name, node.Data.Name, StringComparison.OrdinalIgnoreCase);

        if (comparison == 0)
            return node.Data;
        else if (comparison < 0)
            return SearchRecursive(node.Left, name);
        else
            return SearchRecursive(node.Right, name);
    }
}
