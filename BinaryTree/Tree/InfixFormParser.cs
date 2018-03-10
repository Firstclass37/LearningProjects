using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Tree
{
    internal sealed class InfixFormParser<T> where T : IComparable<T>
    {
        public BinaryTree<T> Parse(string treeString)
        {
            var tree = new BinaryTree<T>();
            var root = Parse<T>(treeString);
            tree.SetRoot(root);
            return tree;
        }

        private TreeNode<T> Parse<T>(string treeString) where T : IComparable<T>
        {
            var corrected = Correct(treeString.Trim());
            var parts = ParseParts(corrected);
            if (parts.Length != 3 || parts[1].Contains("("))
                throw new ArgumentException($"Invalid tree part {treeString}");
            var node = new TreeNode<T>(JsonConvert.DeserializeObject<T>(parts[1]));
            var leftChild = parts[0].Contains("(")
                ? Parse<T>(parts[0])
                : CreateNode<T>(parts[0]);
            var rightChild = parts[2].Contains("(")
                ? Parse<T>(parts[2])
                : CreateNode<T>(parts[2]);
            node.AddChild(leftChild);
            node.AddChild(rightChild);
            return node;
        }

        private string Correct(string treeString)
        {
            var corrected = treeString;
            if (treeString.StartsWith("("))
                corrected = treeString.Remove(0, 1);
            if (treeString.EndsWith(")"))
                corrected = corrected.Remove(corrected.Length -1, 1);
            return corrected;
        }

        private string[] ParseParts(string nodeString)
        {
            var parts = new List<string>();
            var index = 0;
            while (index < nodeString.Length)
            {
                parts.Add(ParsePart(nodeString, ref index));
            }
            return parts.ToArray();
        }

        private string ParsePart(string nodeString, ref int startIndex)
        {
            var stringBuilder = new StringBuilder();
            var openBrackets = 0;
            var closeBrackets = 0;
            while (startIndex < nodeString.Length)
            {
                var curChar = nodeString[startIndex];
                startIndex++;
                if (curChar == '(')
                    openBrackets++;
                if (curChar == ')')
                    closeBrackets++;
                if (curChar == ',' && closeBrackets == openBrackets)
                    break;
                stringBuilder.Append(curChar);
            }
            return stringBuilder.ToString();
        }

        private TreeNode<T> CreateNode<T>(string value) where T: IComparable<T>
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return new TreeNode<T>(JsonConvert.DeserializeObject<T>(value));
        }
    }
}
