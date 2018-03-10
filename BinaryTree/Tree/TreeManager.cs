using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Tree
{
    public sealed class TreeManager
    {
        public string Save<T>(BinaryTree<T> tree) where T: IComparable<T>
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var newFile = Path.Combine(assemblyPath, $"tree_{DateTime.Now:dd-MM-yyyy-hh-mm-ss}.txt");
            var treeString = tree.Show(i => JsonConvert.SerializeObject(i), ShowType.Infix);
            File.WriteAllText(newFile, treeString);
            return newFile;
        }

        public BinaryTree<T> Load<T>(string path) where T: IComparable<T>
        {
            var infinxParser = new InfixFormParser<T>();
            var treeString = File.ReadAllText(path);
            var tree = infinxParser.Parse(treeString);
            return tree;
        }
    }
}
