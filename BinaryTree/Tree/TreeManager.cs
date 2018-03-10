using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Tree
{
    public sealed class TreeManager
    {
        public string Save<T>(BinaryTree<T> tree, ShowType type) where T: IComparable<T>
        {
            var formatter = new Formatter<T>(i => JsonConvert.SerializeObject(i));
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var newFile = Path.Combine(assemblyPath, $"tree_{DateTime.Now.ToFileTime()}.txt");

            var formatInfo = Formatter<T>.CreateInfo(type);
            var formatInfoString = JsonConvert.SerializeObject(formatInfo);
            var treeString = formatter.ToString(tree, formatInfo);

            File.WriteAllLines(newFile, new List<string> {formatInfoString, treeString});
            return newFile;
        }

        public BinaryTree<T> Load<T>(string path) where T: IComparable<T>
        {
            var treeInfo = File.ReadAllLines(path);
            var formatInfo = JsonConvert.DeserializeObject<FormatInfo>(treeInfo[0]);
            var treeString = treeInfo[1];

            var infinxParser = new InfixFormParser(formatInfo);

            var tree = infinxParser.Parse<T>(treeString);
            return tree;
        }
    }
}
