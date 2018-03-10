using System;

namespace Tree
{
    public sealed class Formatter<T> where T: IComparable<T>
    {
        private readonly Func<T, string> _toStringFunc;

        public Formatter()
        {
            _toStringFunc = val => val.ToString();
        }

        public Formatter(Func<T, string> toStringFunc)
        {
            _toStringFunc = toStringFunc;
        }

        internal static FormatInfo CreateInfo(ShowType type)
        {
            if (type == ShowType.Infix)
                return new FormatInfo {RootPos = 1, LeftChildPos = 0, RightChildPos = 2};
            if (type == ShowType.Prefix)
                return new FormatInfo { RootPos = 0, LeftChildPos = 1, RightChildPos = 2 };
            if (type == ShowType.Postfix)
                return new FormatInfo { RootPos = 1, LeftChildPos = 2, RightChildPos = 0 };
            throw new ArgumentException($"Unknown type {nameof(type)} : {type}");
        }

        public string ToString(BinaryTree<T> tree, ShowType showType)
        {
            var formatInfo = CreateInfo(showType);
            return ToString(tree.Root, formatInfo);
        }

        private string ToString(TreeNode<T> node, FormatInfo formatInfo)
        {
            if (node == null)
                return " ";
            if (!node.HasAnyChilds())
                return node.Value.ToString();
            var parametrs = new Object[3];
            parametrs[formatInfo.LeftChildPos] = ToString(node.LeftChild, formatInfo);
            parametrs[formatInfo.RightChildPos] = ToString(node.RigthChild, formatInfo);
            parametrs[formatInfo.RootPos] = _toStringFunc(node.Value);
            return string.Format("({0}, {1}, {2})", parametrs);
        }
    }
}
