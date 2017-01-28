using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CeilingFunction.BST;

namespace CeilingFunction
{
    class CeilingFunction
    {
        static void Main(string[] args)
        {
            String input;
            String[] initializers = null;
            String[] treeData = null;
            BST[] trees = null;
            int[] matches = null;
            int count = 0;
            int match = 1;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else if(initializers == null)
                {
                    initializers = input.Split();
                    trees = new BST[int.Parse(initializers[0])];
                    treeData = new String[int.Parse(initializers[1])];
                    matches = new int[int.Parse(initializers[0])];
                }
                else
                {
                    //treeData = input.Split();
                    //BST tree = createBST(input.Split());
                    trees[count] = createBST(input.Split());
                    treeData = new String[int.Parse(initializers[1])];
                    count++;
                }
            }

            for(int i = 0; i < trees.Length; i++)
            {
                for(int j = i + 1; j < trees.Length; j++)
                {
                    if(SameTreeShape(trees[i].RootNode, trees[j].RootNode))
                    {
                        matches[i] += match;
                    }
                }
            }

            count = 0;

            foreach(int m in matches)
            {
                if(m == 0)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
            Console.WriteLine("\nDONE");
            Console.Read();

        }

        private static BST createBST(String[] data)
        {
            BST bst = new BST();

            foreach (String number in data)
            {
                bst.AddLeafNode(int.Parse(number));
            }

            return bst;
        }

        public static bool SameTreeShape(LeafNode x, LeafNode y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else
            {
                return (SameTreeShape(x.LeftNode, y.LeftNode) && SameTreeShape(x.RightNode, y.RightNode));
            }
        }
    }
}
