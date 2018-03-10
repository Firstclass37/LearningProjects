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
            var formatter = new Formatter<T>(i => JsonConvert.SerializeObject(i));
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var newFile = Path.Combine(assemblyPath, $"tree_{DateTime.Now:dd-MM-yyyy-hh-mm-ss}.txt");
            var treeString = formatter.ToString(tree, ShowType.Infix);
            File.WriteAllText(newFile, treeString);
            return newFile;
        }

        public BinaryTree<T> Load<T>(string path) where T: IComparable<T>
        {
            var infinxParser = new InfixFormParser(new FormatInfo {RootPos = 1, LeftChildPos = 0, RightChildPos = 2});
            var treeString = File.ReadAllText(path);
            var tree = infinxParser.Parse<T>(treeString);
            return tree;
        }
    }
}
