using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeilingFunction
{
    class CeilingFunction
    {
        static void Main(string[] args)
        {
            String input;
            String[] initializers = null;
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
                    matches = new int[int.Parse(initializers[0])];
                }
                else
                {
                    trees[count] = createBST(input.Split());
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

        public class BST
        {
            private LeafNode rootNode;

            public BST()
            {
                rootNode = null;
            }

            public LeafNode RootNode
            {
                get { return rootNode; }
                set { rootNode = value; }
            }

            public void AddLeafNode(int nodeData)
            {
                LeafNode newNode = new LeafNode(nodeData);
                if (rootNode == null)
                {
                    rootNode = newNode;
                    return;
                }

                LeafNode currentNode = rootNode;
                LeafNode parentNode = null;
                while (true)
                {
                    parentNode = currentNode;
                    if (nodeData < currentNode.NodeData)
                    {
                        currentNode = currentNode.LeftNode;
                        if (currentNode == null)
                        {
                            parentNode.LeftNode = newNode;
                            return;
                        }
                    }
                    else
                    {
                        currentNode = currentNode.RightNode;
                        if (currentNode == null)
                        {
                            parentNode.RightNode = newNode;
                            return;
                        }
                    }
                }
            }
        }

        public class LeafNode
        {
            private int nodeData;
            private LeafNode leftNode;
            private LeafNode rightNode;

            public LeafNode(int data)
            {
                this.nodeData = data;
                leftNode = null;
                rightNode = null;
            }

            public int NodeData
            {
                get { return nodeData; }
            }

            public LeafNode LeftNode
            {
                get { return leftNode; }
                set { leftNode = value; }
            }

            public LeafNode RightNode
            {
                get { return rightNode; }
                set { rightNode = value; }
            }
        }
    }
}
