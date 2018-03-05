using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Tree
{
    public sealed class TreeManager
    {
        public string Save(Object tree)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var newFile = Path.Combine(assemblyPath, $"tree_{DateTime.Now:dd-MM-yyyy-hh-mm-ss}.txt");
            var json = JsonConvert.SerializeObject(tree);
            File.WriteAllText(newFile, json);
            return newFile;
        }

        public BinaryTree<T> Load<T>(string path) where T: IComparable<T>
        {
            var tree = JsonConvert.DeserializeObject<BinaryTree<T>>(path);
            return tree;
        }
    }
}
