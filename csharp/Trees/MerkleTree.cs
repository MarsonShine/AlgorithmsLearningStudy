using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Trees
{
	public class MerkleTree
	{
		public class Node
		{
			public string Hash { get; set; } = "";
			public Node Left { get; set; }
			public Node Right { get; set; }
		}

		public Node Root { get; set; }

		public MerkleTree(string[] datas)
		{
			List<Node> leaves = [];
			for (int i = 0; i < datas.Length; i++)
			{
				leaves.Add(new Node { Hash = ComputeHash(datas[i]) });
			}
			Root = BuildMerkleTree(leaves);
		}

		private static Node BuildMerkleTree(List<Node> nodes)
		{
			if (nodes.Count == 1)
			{
				return nodes[0];
			}
			List<Node> parentNodes = [];
			for (int i = 0; i < nodes.Count; i += 2)
			{
				if (i + 1 < nodes.Count)
				{
					parentNodes.Add(new Node
					{
						Hash = ComputeHash(nodes[i].Hash + nodes[i + 1].Hash),
						Left = nodes[i],
						Right = nodes[i + 1]
					});
				}
				else
				{
					// 奇数个节点就复制最后一个节点进行计算
					parentNodes.Add(new Node { 
						Hash = ComputeHash(nodes[i].Hash + nodes[i].Hash),
						Left = nodes[i],
						Right = nodes[i]
					});
				}
			}
			return BuildMerkleTree(parentNodes);
		}

		private static string ComputeHash(string data)
		{
			var bytes = Encoding.UTF8.GetBytes(data);
			var hash = SHA256.HashData(bytes);
			StringBuilder builder = new();
			foreach (byte b in hash)
			{
				builder.Append(b.ToString("x2"));
			}
			return builder.ToString();
		}

		public bool VerifyData(string data)
		{
			string hash = ComputeHash(data);
			return VerifyHash(Root, hash);
		}

		public bool VerifyHash(Node node, string hash)
		{
			if (node == null)
			{
				return false;
			}
			if (node.Hash == hash)
			{
				return true;
			}
			return VerifyHash(node.Left,hash) || VerifyHash(node.Right,hash);
		}
	}
}
