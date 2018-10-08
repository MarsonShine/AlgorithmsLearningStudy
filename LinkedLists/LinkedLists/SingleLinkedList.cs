using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedLists
{
    /// <summary>
    /// 单链表的插入，删除，查询
    /// </summary>
    public class SingleLinkedList
    {
        private Node m_head = null;

        public Node FindByValue(int value)
        {
            Node p = m_head;
            while (p != null && p.Value != value)
            {
                p = p.Next;
            }
            return p;
        }

        public Node FindByIndex(int index)
        {
            Node p = m_head;
            int pos = 0;
            while (p != null && pos != index)
            {
                p = p.Next;
                ++pos;
            }
            return p;
        }

        public void AddFirst(int value)
        {
            Node head = new Node(value, null);
            InsertNodeToHead(head);
        }

        public void AddAfter(Node p, int value)
        {
            Node newNode = new Node(value, null);
            InsertAfter(p, newNode);
        }

        public void AddBefore(Node p, int value)
        {
            Node newNode = new Node(value, null);
            InsertBefore(p, newNode);
        }

        private void InsertBefore(Node p, Node newNode)
        {
            if (p == null) return;
            if (p == m_head)
            {
                InsertNodeToHead(newNode);
                return;
            }
            Node q = m_head;
            while (q != null && q.Next != p)
            {
                q = q.Next;
            }

            if (q == null) return;

            newNode.Next = p;
            q.Next = newNode;
        }

        private void InsertAfter(Node p, Node newNode)
        {
            if (p == null) return;
            newNode.Next = p.Next;
            p.Next = newNode;
        }

        private void InsertNodeToHead(Node newHead)
        {
            if (m_head == null)
            {
                m_head = newHead;
            }
            else
            {
                newHead.Next = m_head;
                m_head = newHead;
            }
        }

        public class Node
        {
            private int m_data;
            private Node m_next;

            public Node(int data, Node next)
            {
                m_data = data;
                m_next = next;
            }

            public int Value { get { return m_data; } set { m_data = value; } }
            public Node Next { get { return m_next; } set { m_next = value; } }
        }
    }
}
