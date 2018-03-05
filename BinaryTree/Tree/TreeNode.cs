using System;

namespace Tree
{
    public sealed class TreeNode<T> where T : IComparable<T>
    {
        public TreeNode(T value)
        {
            Value = value;
        }
        
        public T Value { get; set; }

        public TreeNode<T> Parent { get; set; }

        public TreeNode<T> LeftChild { get; set; }

        public TreeNode<T> RigthChild { get; set; }

        public bool AddChild(TreeNode<T> node)
        {
            var added = false;
            if (this == node)
                added = true;
            else if (this > node && this.LeftChild == null)
            {
                LeftChild = node;
                LeftChild.Parent = this;
                added = true;
            }
            else if (this < node && this.RigthChild == null)
            {
                RigthChild = node;
                RigthChild.Parent = this;
                added = true;
            }
            return added;
        }

        public void RemoveChild(TreeNode<T> node)
        {
            if (LeftChild == node)
                LeftChild = null;
            else if (RigthChild == node)
                RigthChild = null;
        }

        public bool HasAllChilds()
        {
            return LeftChild != null && RigthChild != null;
        }

        public bool HasAnyChilds()
        {
            return LeftChild != null || RigthChild != null;
        }

        public bool HasLeftChild()
        {
            return LeftChild != null;
        }

        public bool HasRigthChild()
        {
            return RigthChild != null;
        }

        public static bool operator ==(TreeNode<T> first, TreeNode<T> second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
                return true;
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
                return false;
            return first.Value.CompareTo(second.Value) == 0;
        }

        public static bool operator ==(TreeNode<T> first, T value)
        {
            if (first == null)
                return false;
            return first.Value.CompareTo(value) == 0;
        }

        public static bool operator !=(TreeNode<T> first, TreeNode<T> second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
                return false;
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
                return true;
            return first.Value.CompareTo(second.Value) != 0;
        }

        public static bool operator !=(TreeNode<T> first, T value)
        {
            if (first == null)
                return false;
            return first.Value.CompareTo(value) != 0;
        }

        public static bool operator >(TreeNode<T> first, TreeNode<T> second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
                return false;
            return first.Value.CompareTo(second.Value) == 1;
        }

        public static bool operator >(TreeNode<T> first, T value)
        {
            if (first == null)
                return false;
            return first.Value.CompareTo(value) == 1;
        }

        public static bool operator <(TreeNode<T> first, TreeNode<T> second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
                return false;
            return first.Value.CompareTo(second.Value) == -1;
        }

        public static bool operator <(TreeNode<T> first, T value)
        {
            if (first == null)
                return false;
            return first.Value.CompareTo(value) == -1;
        }
    }
}