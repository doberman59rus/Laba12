using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchLibrary;

namespace LAB12
{
    public class MyTreeCollection<T> : IEnumerable<T> where T : Watch
    {
        private class TreeNode
        {
            public T Data;
            public TreeNode Left;
            public TreeNode Right;

            public TreeNode(T data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

        private TreeNode root;
        public int Count { get; private set; }

        //конструкторы
        public MyTreeCollection() { }

        public MyTreeCollection(int length)
        {
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                Watch watch = GenerateRandomWatch(rnd);
                if (watch is T compatibleWatch)
                {
                    Add(compatibleWatch);
                }
            }
        }

        public MyTreeCollection(MyTreeCollection<T> c)
        {
            if (c == null)
                throw new Exception();

            foreach (var item in c)
            {
                //создаем новый объект через клонирование
                T newItem = (T)item.Clone();
                Add(newItem);
            }
        }

        //основные методы дерева
        public void Add(T item)
        {
            root = AddRecursive(root, item);
            Count++;
        }

        private TreeNode AddRecursive(TreeNode node, T item)
        {
            if (node == null)
            {
                return new TreeNode(item);
            }

            //сравниваем по году выпуска
            if (item.YearOfManufacture < node.Data.YearOfManufacture)
            {
                node.Left = AddRecursive(node.Left, item);
            }
            else
            {
                node.Right = AddRecursive(node.Right, item);
            }

            return node;
        }

        public bool Remove(T item)
        {
            int initialCount = Count;
            root = RemoveRecursive(root, item);
            return Count < initialCount;
        }

        private TreeNode RemoveRecursive(TreeNode node, T item)
        {
            if (node == null) return null;

            if (item.YearOfManufacture < node.Data.YearOfManufacture)
            {
                node.Left = RemoveRecursive(node.Left, item);
            }
            else if (item.YearOfManufacture > node.Data.YearOfManufacture)
            {
                node.Right = RemoveRecursive(node.Right, item);
            }
            else
            {
                //узел с одним подузлом или без подузлов
                if (node.Left == null)
                {
                    Count--;
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    Count--;
                    return node.Left;
                }

                //узел с двумя подузлами
                node.Data = FindMin(node.Right).Data;
                node.Right = RemoveRecursive(node.Right, node.Data);
            }

            return node;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public T Find(T item)
        {
            TreeNode node = FindRecursive(root, item);
            return node != null ? node.Data : default(T);
        }

        private TreeNode FindRecursive(TreeNode node, T item)
        {
            if (node == null) return null;

            if (item.YearOfManufacture == node.Data.YearOfManufacture)
            {
                return node;
            }

            return item.YearOfManufacture < node.Data.YearOfManufacture
                ? FindRecursive(node.Left, item)
                : FindRecursive(node.Right, item);
        }

        //вспомогательные методы
        private TreeNode FindMin(TreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }

        //реализация IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal(root).GetEnumerator();
        }

        private IEnumerable<T> InOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                foreach (var item in InOrderTraversal(node.Left))
                    yield return item;

                yield return node.Data;

                foreach (var item in InOrderTraversal(node.Right))
                    yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        //генерация случайных часов
        private Watch GenerateRandomWatch(Random rnd)
        {
            int type = rnd.Next(0, 4);
            Watch watch;

            switch (type)
            {
                case 0:
                    watch = new Watch();
                    watch.RandomInit();
                    return watch;
                case 1:
                    watch = new AnalogWatch();
                    watch.RandomInit();
                    return watch;
                case 2:
                    watch = new ElectronicWatch();
                    watch.RandomInit();
                    return watch;
                case 3:
                    watch = new SmartWatch();
                    watch.RandomInit();
                    return watch;
                default:
                    return new Watch();
            }
        }
    }
}
