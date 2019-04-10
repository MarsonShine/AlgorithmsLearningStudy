using System;
using System.Collections.Generic;

namespace AlgorithmPractices.HashLinkeds {
    /// <summary>
    /// based on linked hashtable
    /// </summary>
    public class LRUBaseLinkedHashTable<TKey, TValue> {
        private Entry head;
        private int capacity;
        private readonly Dictionary<TKey, Entry> maps;
        public LRUBaseLinkedHashTable() : this(20) {

        }
        public LRUBaseLinkedHashTable(int capacity) {
            maps = new Dictionary<TKey, Entry>(capacity);
            this.capacity = capacity;
        }
        public void Add(TKey key, TValue value) {
            if (maps.ContainsKey(key)) {
                //remove exist node
                Entry exsited = maps[key];
                Unlink(exsited);
            }
            Entry newNode = new Entry(key, value);
            //before add map, remove last use item
            if (maps.Count >= capacity) {
                var node = RemoveTail();
                maps.Remove(node.key);
            }
            maps.TryAdd(key, newNode);
            InsertNodeToHead(newNode);
        }

        public TValue GetValue(TKey key) {
            if (!maps.ContainsKey(key)) return default;
            Entry node = maps[key];
            Unlink(node);
            InsertNodeToHead(node);
            return node.value;
        }

        public void Print() {
            Entry t = head;
            while (t != null) {
                Console.Write($"{t.value} ");
                t = t.next;
            }
            Console.WriteLine();
        }

        public int Capacity => capacity;

        private Entry RemoveTail() {
            Entry h = head;
            while (h != null) {
                h = h.next;
            }
            h.prev.next = null;
            h.prev = null;
            return h;
        }

        private void InsertNodeToHead(Entry node) {
            if (head == null) {
                head = node;
            } else {
                node.next = head;
                head.prev = node;
                head = node;
            }
        }

        private void Unlink(Entry node) {
            if (node.prev == null) {
                Entry p = head;
                head = head.next;
                p.next = null;
                return;
            }
            if (node.next == null) {
                node.prev.next = null;
                node.prev = null;
                return;
            }
            Entry prev = node.prev;
            Entry next = node.next;
            prev.next = next;
            next.prev = prev;
            node.prev = null;
            node.next = null;
        }

        private class Entry {
            public Entry prev;
            public Entry next;
            public TKey key;
            public TValue value;
            public Entry(TKey key, TValue value) {
                this.key = key;
                this.value = value;
            }
        }
    }
}