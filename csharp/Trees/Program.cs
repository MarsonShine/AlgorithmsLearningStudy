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

            var trees1 = new BinarySearchTree<int>();
            trees1.Add(33);
            trees1.Add(16);
            trees1.Add(13);
            trees1.Add(18);
            trees1.Add(15);
            trees1.Add(17);
            trees1.Add(25);
            trees1.Add(19);
            trees1.Add(21);
            trees1.Add(50);
            trees1.Add(34);
            trees1.Add(58);
            trees1.Add(51);
            trees1.Add(55);
            trees1.Add(66);

            trees1.Remove(13);
            Console.WriteLine("Hello World!");
        }
    }
}