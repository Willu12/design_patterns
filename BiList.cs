using System;
namespace BTM
{
    public class BiList<T> : ICollection<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;

        public BiList(Node<T>? head, Node<T>? tail)
        {
            this._head = head;
            this._tail = tail;
        }
        public void Add(T v)
        {
            Node<T> node = new Node<T>(null, null, v);
            if (_head == null || _tail == null)
            {
                _head = node;
                _tail = node;
                return;
            }

            node.Prev = _tail;
            _tail.Next = node;
        }

        public bool Delete(T v)
        {
            if (_head == null) return false;

            Node<T> p = _head;
            if (v.Equals(p.V))
            {
                if(_head.Next != null)  _head.Next.Prev = null;
                _head = _head.Next;

            }
            while(p != null)
            {
                if(v.Equals(p.V))
                {
                    if(p.Prev != null)
                    {
                        p.Prev.Next = p.Next;
                    }
                    if(p.Next != null)
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
}

