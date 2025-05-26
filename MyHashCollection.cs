using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchLibrary;

namespace LAB12
{
    public class MyHashCollection<T> : IDictionary<string, T> where T : Watch, new()
    {
        private Dictionary<string, T> items = new Dictionary<string, T>();

        //конструктор с заданным количеством элементов
        public MyHashCollection(int length)
        {
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                T watch = GenerateRandomWatch(rnd);
                Add(watch.Brand, watch);
            }
        }

        //конструктор копирования
        public MyHashCollection(MyHashCollection<T> c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));

            foreach (var item in c)
            {
                //создаем глубокую копию элемента
                T clonedItem = (T)item.Value.Clone();
                Add(item.Key, clonedItem);
            }
        }

        //генератор часов
        private T GenerateRandomWatch(Random rnd)
        {
            T watch = new T(); //создаем экземпляр конкретного типа T
            watch.RandomInit(); //инициализируем случайными значениями
            return watch;
        }

        //реализация IDictionary<string, T>
        public T this[string key]
        {
            get => items[key];
            set => items[key] = value;
        }

        public ICollection<string> Keys => items.Keys;
        public ICollection<T> Values => items.Values;
        public int Count => items.Count;
        public bool IsReadOnly => false;
        public void Add(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Ключ не может быть пустым или null");

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!items.ContainsKey(key))
            {
                items.Add(key, value);
            }
        }
        public void Add(KeyValuePair<string, T> item) => items.Add(item.Key, item.Value);
        public void Clear() => items.Clear();
        public bool Contains(KeyValuePair<string, T> item) => items.ContainsKey(item.Key);
        public bool ContainsKey(string key) => items.ContainsKey(key);
        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex) => throw new NotImplementedException();
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator() => items.GetEnumerator();
        public bool Remove(string key) => items.Remove(key);
        public bool Remove(KeyValuePair<string, T> item) => items.Remove(item.Key);
        public bool TryGetValue(string key, out T value) => items.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
