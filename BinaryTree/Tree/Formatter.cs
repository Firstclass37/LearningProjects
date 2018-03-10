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


        public string ToString(BinaryTree<T> tree, ShowType showType)
        {
            var formatInfo = new FormatInfo(showType);
            return ToString(tree.Root, formatInfo);
        }

        public string ToString(BinaryTree<T> tree, FormatInfo info)
        {
            return ToString(tree.Root, info);
        }

        private string ToString(TreeNode<T> node, FormatInfo formatInfo)
        {
            if (node == null)
                return " ";
            if (!node.HasAnyChilds())
                return $"{formatInfo.ValueStart}{_toStringFunc(node.Value)}{formatInfo.ValueEnd}";
            var parametrs = new Object[5];
            parametrs[formatInfo.LeftChildPos] = ToString(node.LeftChild, formatInfo);
            parametrs[formatInfo.RightChildPos] = ToString(node.RigthChild, formatInfo);
            parametrs[formatInfo.RootPos] = $"{formatInfo.ValueStart}{_toStringFunc(node.Value)}{formatInfo.ValueEnd}";
            parametrs[3] = formatInfo.RegionStart;
            parametrs[4] = formatInfo.RegionEnd;
            return string.Format("{3}{0}, {1}, {2}{4}", parametrs);
        }
    }
}
