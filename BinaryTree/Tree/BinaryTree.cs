﻿using System;
using Newtonsoft.Json;

namespace Tree
{
    public sealed class BinaryTree<T> where T : IComparable<T>
    {
        private TreeNode<T> _root;

        public void Add(T value)
        {
            var newNode = new TreeNode<T>(value);
            if (_root == null)
                _root = newNode;
            else
                Insert(newNode);    
        }

        public void Add(T[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            foreach (var value in values)
                Add(value);
        }

        public bool Contains(T value)
        {
            var node = Find(value);
            return node != null;
        }

        public void Remove(T value)
        {
            var node = Find(value);
            if (node == null)
                return;
            if (!node.HasAnyChilds())
            {
                if (node == _root)
                    _root = null;
                else
                    node.Parent.RemoveChild(node);
            }
            else if (node.HasAllChilds())
            {
                var replacement = GetLastNode(node.RigthChild);
                replacement.Parent.RemoveChild(replacement);
                node.Value = replacement.Value;
            }
            else if (node.HasLeftChild())
            {
                node.Parent.RemoveChild(node);
                node.Parent.AddChild(node.LeftChild);
            }
            else if (node.HasRigthChild())
            {
                node.Parent.RemoveChild(node);
                node.Parent.AddChild(node.RigthChild);
            }
        }

        public string Show(Func<T, string> toString, ShowType type)
        {
            return Show(_root, toString, type);
        }

        internal void SetRoot(TreeNode<T> root)
        {
            _root = root;
        }

        private string Show(TreeNode<T> node, Func<T, string> toString , ShowType showType)
        {
            if (node == null)
                return " ";
            if (!node.HasAnyChilds())
                return node.Value.ToString();
            switch (showType)
            {
                case ShowType.Prefix:
                    return
                        $"({toString(node.Value)}, {Show(node.LeftChild, toString, showType)}, {Show(node.RigthChild, toString, showType)})";
                case ShowType.Infix:
                    return
                        $"({Show(node.LeftChild, toString, showType)}, {toString(node.Value)}, {Show(node.RigthChild, toString, showType)})";
                case ShowType.Postfix:
                    return
                        $"({Show(node.LeftChild,toString, showType)}, {Show(node.RigthChild,toString, showType)}), {toString(node.Value)}";
            }
            return "";
        }

        private TreeNode<T> Find(T value)
        {
            var currentNode = _root;
            do
            {
                if (currentNode == value)
                    return currentNode;
                currentNode = currentNode > value ? currentNode.LeftChild : currentNode.RigthChild;
            } while (currentNode != null);
            return null;
        }

        private TreeNode<T> GetLastNode(TreeNode<T> node)
        {
            var currentNode = node;
            while (currentNode.HasAnyChilds()) 
            {
               currentNode = node.LeftChild ?? node.RigthChild;
            } 
            return currentNode;
        }

        private void Insert(TreeNode<T> node)
        {
            var currentNode = _root;
            while (!currentNode.AddChild(node))
            {
                currentNode = currentNode > node ? currentNode.LeftChild : currentNode.RigthChild;
            }
        }
    }
}