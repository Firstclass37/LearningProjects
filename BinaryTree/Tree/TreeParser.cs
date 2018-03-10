using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Tree
{
    internal sealed class TreeParser
    {
        private readonly FormatInfo _formatInfo;


        public TreeParser(FormatInfo formatInfo)
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
            if (parts.Length != 3 || parts[_formatInfo.RootPos].Contains(_formatInfo.RegionStart))
                throw new ArgumentException($"Invalid tree part {treeString}");
            var node = new TreeNode<T>(JsonConvert.DeserializeObject<T>(TakeValue(parts[_formatInfo.RootPos])));
            var leftChild = parts[_formatInfo.LeftChildPos].Contains(_formatInfo.RegionStart)
                ? ParseNode<T>(parts[_formatInfo.LeftChildPos])
                : CreateNode<T>(parts[_formatInfo.LeftChildPos]);
            var rightChild = parts[_formatInfo.RightChildPos].Contains(_formatInfo.RegionStart)
                ? ParseNode<T>(parts[_formatInfo.RightChildPos])
                : CreateNode<T>(parts[_formatInfo.RightChildPos]);
            node.AddChild(leftChild);
            node.AddChild(rightChild);
            return node;
        }

        private string Correct(string treeString)
        {
            var corrected = treeString;
            if (treeString.StartsWith(_formatInfo.RegionStart.ToString()))
                corrected = treeString.Remove(0, 1);
            if (treeString.EndsWith(_formatInfo.RegionEnd.ToString()))
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
            var regionStarts = 0;
            var regionEnds = 0;
            var isValueRegion = false;
            while (startIndex < nodeString.Length)
            {
                var curChar = nodeString[startIndex];
                startIndex++;
                if (curChar == _formatInfo.ValueStart)
                    isValueRegion = true;
                if (curChar == _formatInfo.ValueEnd)
                    isValueRegion = false;
                if (curChar == _formatInfo.RegionStart)
                    regionStarts++;
                if (curChar == _formatInfo.RegionEnd)
                    regionEnds++;
                if (!isValueRegion && curChar == _formatInfo.ValSeparator && regionEnds == regionStarts)
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
            var neededChars = sourceString.Trim().Where(v => v != _formatInfo.ValueStart &&
                                                      v != _formatInfo.ValueEnd)
                .ToArray();
            var targetValue = new string(neededChars);
            return targetValue;
        }

    }
}