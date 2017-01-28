using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeilingFunction
{
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
