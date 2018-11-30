using System;

namespace Trees {
    class Program {
        static void Main(string[] args) {
            // var node = new Node(33);
            // node.Left = new Node(16).Left = new Node(13).Right = new Node(15);
            // node.Left.Right.Left = new Node(17);
            // node.Left.Right.Right = new Node(25);
            // node.Left.Right.Right.Left = new Node(19);
            // node.Left.Right.Right.Right = new Node(21);

            // node.Right = new Node(50).Right = new Node(58).Right = new Node(66);
            // node.Right.Left = new Node(34);
            // node.Right.Right.Left = new Node(51).Right = new Node(55);

            var trees = new BinarySearchTree();
            trees.Insert(33);
            trees.Insert(16);
            trees.Insert(13);
            trees.Insert(18);
            trees.Insert(15);
            trees.Insert(17);
            trees.Insert(25);
            trees.Insert(19);
            trees.Insert(21);
            trees.Insert(50);
            trees.Insert(34);
            trees.Insert(58);
            trees.Insert(51);
            trees.Insert(55);
            trees.Insert(66);

            trees.Delete(18);
            Console.WriteLine("Hello World!");
        }
    }
}