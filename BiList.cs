using System;
using System.Collections;

namespace BTM
{
    public class BiList<T> : ICollection<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;

        public BiList(Node<T>? head, Node<T>? tail)
        {
            this.Head = head;
            this.Tail = tail;
        }

        public BiList(List<T> list) : this(null,null)
        {
            foreach(T el in list)
            {
                this.Add(el);
            }
        }

        public Node<T>? Head { get => _head; set => _head = value; }
        public Node<T>? Tail { get => _tail; set => _tail = value; }

        public void Add(T v)
        {
            Node<T> node = new Node<T>(null, null, v);
            if (Head == null || Tail == null)
            {
                Head = node;
                Tail = node;
                return;
            }

            node.Prev = Tail;
            Tail.Next = node;
            Tail = node;
        }

        public bool Delete(T v)
        {
            if (Head == null) return false;

            Node<T> p = Head;
            if (v.Equals(p.V))
            {
                if (Head.Next != null) Head.Next.Prev = null;
                Head = Head.Next;

            }
            while (p != null)
            {
                if (v.Equals(p.V))
                {
                    if (p.Prev != null)
                    {
                        p.Prev.Next = p.Next;
                    }
                    if (p.Next != null)
                    {
                        p.Next.Prev = p.Prev;
                    }
                    p.Next = null;
                    p.Prev = null;
                    return true;
                }
                p = p.Next;
            }
            return false;
        }

        public Iiterator<T> CreateForwardIterator()
        {
            return new ForwardBiListIterator<T>(this);
        }

       
        public Iiterator<T> CreateReverseIterator()
        {
            return new ReverseBiListIterator<T>(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ForwardBiListIterator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Node<T>
    {
        private Node<T>? _next;
        private Node<T>? _prev;
        private T _v;

        public Node<T>? Next { get => _next; set => _next = value; }
        public Node<T>? Prev { get => _prev; set => _prev = value; }
        public T V { get => _v; set => _v = value; }

        public Node(Node<T>? next, Node<T>? prev, T v)
        {
            this.Next = next;
            this.Prev = prev;
            this._v = v;
        }
    }

    public class ForwardBiListIterator<T> : Iiterator<T>
    {
        private BiList<T> _biList;
        private Node<T> _current;

        public ForwardBiListIterator(BiList<T> biList)
        {
            this._biList = biList;
            _current = biList.Head;
        }

        public T Current => currentItem();

        object IEnumerator.Current => _current;

        public T currentItem()
        {
            return _current.V;
        }

        public void Dispose()
        {
            _current = null;
        }

        public void First()
        {
            _current = _biList.Head;
        }

        public bool isDone()
        {
            return _current == null;
        }

        public bool MoveNext()
        {
            Next();
            return isDone();
        }

        public void Next()
        {
            _current = _current.Next;
        }

        public void Reset()
        {
            _current = _biList.Head;
        }
    }

    public class ReverseBiListIterator<T> : Iiterator<T>
    {
        private BiList<T> _biList;
        private Node<T> _current;

        public ReverseBiListIterator(BiList<T> biList)
        {
            this._biList = biList;
            _current = biList.Tail;
        }

        public T Current => currentItem();

        object IEnumerator.Current => _current;

        public T currentItem()
        {
            return _current.V;
        }

        public void Dispose()
        {
            _current = null;
        }

        public void First()
        {
            _current = _biList.Tail;
        }

        public bool isDone()
        {
            return _current == null;
        }

        public bool MoveNext()
        {
            Next();
            return isDone();
        }

        public void Next()
        {
            _current = _current.Prev;
        }

        public void Reset()
        {
            _current = _biList.Tail;
        }
    }
}
