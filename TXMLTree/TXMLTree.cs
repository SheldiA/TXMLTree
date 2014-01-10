using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TXMLTree
{
    class TXMLTree
    {
        private string key;
        private string value;
        private int numberElements;
        public int NumberElements
        {
            get { return numberElements; }
        }
        private int numberInsertedTree;
        public int NumberInsertedTree
        {
            get { return numberInsertedTree; }
        }
        private List<TXMLTree> listChilds;
        private TXMLTree root;
        private TXMLTree parent;

        public TXMLTree(string key,string value)
        {
            this.key = key;
            this.value = value;
            numberInsertedTree = 0;
            numberElements = 1;
            listChilds = new List<TXMLTree>();
            parent = null;
            root = this;
        }

        public void Insert(string keyTo,string key,string value)
        {
            TXMLTree inNode = GetNode(this,keyTo);
            if (inNode != null || root == null)
            {                
                ++numberElements;
                if (root == null)
                {               
                    this.key = key;
                    this.value = value;
                    root = this;
                }
                else
                {
                    TXMLTree node = new TXMLTree(key, value);
                    if (inNode.listChilds.Count == 0 && inNode != this)
                        ++numberInsertedTree;
                    node.parent = inNode;
                    inNode.listChilds.Add(node);
                }
            }
        }
        
        public void RemoveAt(string key)
        {
            TXMLTree removeNode = GetNode(this,key);
            if (removeNode != null)
            {
                if (removeNode == root)
                    Clear();
                else
                {
                    if (removeNode.parent != null)
                    {
                        removeNode.parent.listChilds.RemoveAt(removeNode.parent.listChilds.IndexOf(removeNode));
                        if (removeNode.listChilds.Count > 0)
                            --numberInsertedTree;
                        foreach (TXMLTree Element in removeNode.listChilds)
                        {
                            Element.parent = removeNode.parent;
                            removeNode.parent.listChilds.Add(Element);

                        }
                        --numberElements;
                    }
                }
            }
        }

        public TXMLTree GetNode(TXMLTree from,string key)
        {
            TXMLTree result = null;

            if (from.key == key)
                result = from;
            else
                foreach (TXMLTree element in from.listChilds)
                {
                    result = GetNode(element, key);
                    if (result != null)
                        break;
                }

            return result;
        }

        public void Clear()
        {
            root = null;
            numberElements = 0;
            numberInsertedTree = 0;
            listChilds.Clear();
            value = "";
            key = "";
        }

        public string FindValue(string key)
        {
            string result = "";
            TXMLTree node = GetNode(this,key);
            if (node != null)
                result = node.value;
            return result;
        }

        public string FindPath(string path)
        {
            string result = "";

            string[] pathArray = path.Split('\\');
            TXMLTree node = root;
            bool isFind = false;
            if (pathArray.Length > 0 && node != null && pathArray[0] == node.key)
            {
                for (int i = 1; i < pathArray.Length; ++i)
                {
                    isFind = false;
                    foreach(TXMLTree element in node.listChilds)
                        if (element.key == pathArray[i])
                        {
                            isFind = true;
                            node = element;
                            break;
                        }
                    if (!isFind || node == null)
                        break;
                }
                if (isFind && node != null)
                    result = node.value;

            }
            return result;
        }
    }
}
