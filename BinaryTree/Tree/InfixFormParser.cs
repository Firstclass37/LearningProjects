using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Tree
{
    internal sealed class InfixFormParser
    {
        private readonly FormatInfo _formatInfo;


        public InfixFormParser(FormatInfo formatInfo)
        {
            _formatInfo = formatInfo;
        }

        public BinaryTree<T> Parse<T>(string treeString) where T : IComparable<T>
        {
            var tree = new BinaryTree<T>();
            var root = ParseNode<T>(treeString);
            tree.SetRoot(root);
            return tree;
        }

        private TreeNode<T> ParseNode<T>(string treeString) where T : IComparable<T>
        {
            var corrected = Correct(treeString.Trim());
            var parts = ParseParts(corrected);
            if (parts.Length != 3 || parts[_formatInfo.RootPos].Contains("("))
                throw new ArgumentException($"Invalid tree part {treeString}");
            var node = new TreeNode<T>(JsonConvert.DeserializeObject<T>(TakeValue(parts[_formatInfo.RootPos])));
            var leftChild = parts[_formatInfo.LeftChildPos].Contains("(")
                ? ParseNode<T>(parts[_formatInfo.LeftChildPos])
                : CreateNode<T>(parts[_formatInfo.LeftChildPos]);
            var rightChild = parts[_formatInfo.RightChildPos].Contains("(")
                ? ParseNode<T>(parts[_formatInfo.RightChildPos])
                : CreateNode<T>(parts[_formatInfo.RightChildPos]);
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
            var isValueRegion = false;
            while (startIndex < nodeString.Length)
            {
                var curChar = nodeString[startIndex];
                startIndex++;
                if (curChar.ToString() == _formatInfo.ValueStart)
                    isValueRegion = true;
                if (curChar.ToString() == _formatInfo.ValueEnd)
                    isValueRegion = false;
                if (curChar == '(')
                    openBrackets++;
                if (curChar == ')')
                    closeBrackets++;
                if (!isValueRegion && curChar == ',' && closeBrackets == openBrackets)
                    break;
                stringBuilder.Append(curChar);
            }
            return stringBuilder.ToString();
        }

        private TreeNode<T> CreateNode<T>(string value) where T: IComparable<T>
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return new TreeNode<T>(JsonConvert.DeserializeObject<T>(TakeValue(value)));
        }

        private string TakeValue(string sourceString)
        {
            var neededChars = sourceString.Trim().Where(v => v.ToString() != _formatInfo.ValueStart &&
                                                      v.ToString() != _formatInfo.ValueEnd)
                .ToArray();
            var targetValue = new string(neededChars);
            return targetValue;
        }

    }
}