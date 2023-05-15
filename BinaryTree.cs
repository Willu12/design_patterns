using BTM.Tree;
using BTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BTM.Tree
{
    public class BinaryTree<T> : ICollection<T>
    {
        private Node<T>? _root;
        static int rand = 0;

        public BinaryTree(Node<T> root)
        {
            Root = root;
        }

        public BinaryTree()
        {
            Root = null;
        }

        public BinaryTree(List<T> list)
        {
            foreach(T item in list)
            {
                this.Add(item);
            }
        }



        public Node<T> Root { get => _root; set => _root = value; }

        public void Add(T obj)
        {
            Node<T> p = Root;
            if(p == null)
            {
                Root = new Node<T>(null,null,null,obj);
                return;
            }
            while(p.Left != null && p.Right != null)
            {
                rand++;
                if (rand % 2 == 0) p = p.Left;
                else p = p.Right;
            }
            if (p.Left == null)
            {
                p.Left = new Node<T>(p, null,null,obj);
            }
            else if(p.Right == null)
            {
                p.Right = new Node<T>(p, null, null, obj);
            }
        }

        public bool Delete(Iiterator<T> iterator)
        {
            T item = iterator.currentItem();
            iterator.Next();

            if (Delete(item)) return true;

            return false;

        }

        public bool Delete(T obj)
        {
            if(Root == null) return false;

            TreeForwardIterator<T> iterator = new TreeForwardIterator<T>(this);

            while(iterator.isDone() == false)
            {
                if (iterator.currentItem() == null) return false;
                if(iterator.currentItem().Equals(obj))
                {
                    break;
                }
                iterator.Next();

            }

            if (iterator.isDone() == true) return false;

            Node<T> q = iterator.node;
            Node<T> p = iterator.node;
            if (p == null) return false;
            //find with empty children
            while (p.Left != null && p.Right != null)
            {
                if(p.Left == null)
                {
                    p = p.Right;
                }
                p = p.Left;
            }

            if (p.Parent.Left == p)
            {
                p.Parent.Left = null;
            }
            else if (p.Parent.Right == p)  p.Parent.Right = null;
            p.Right = q.Right;
            p.Left = q.Left;
            p.Parent = q.Parent;

            q.Parent = null;
            q.Left = null;
            q.Right = null;
            if (Root == q) Root = p;
           
            return true;
        }

        public Iiterator<T> CreateForwardIterator()
        {
            return new TreeForwardIterator<T>(this);
        }

        public Iiterator<T> CreateReverseIterator()
        {
            return new TreeReverseIterator<T>(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return CreateForwardIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Node<T>
    {
        private Node<T>? _parent;
        private Node<T>? _left;
        private Node<T>? _right;
        private T _v;

        public Node(Node<T>? parent, Node<T>? left, Node<T>? right, T v)
        {
            _parent = parent;
            _left = left;
            _right = right;
            _v = v;
        }

        public Node<T>? Parent { get => _parent; set => _parent = value; }
        public Node<T>? Left { get => _left; set => _left = value; }
        public Node<T>? Right { get => _right; set => _right = value; }
        public T V { get => _v; set => _v = value; }
    }

    public class TreeForwardIterator<T> : Iiterator<T>
    {
        private BinaryTree<T> _tree;
        public Node<T>? node;
        private Node<T>? prev;
        private Stack<Node<T>> _stack;

        public T Current => node.V;

        object IEnumerator.Current => node;

        public TreeForwardIterator(BinaryTree<T> tree)
        {
            _tree = tree;
            this.node = tree.Root;
            prev = null;
            _stack = new Stack<Node<T>>();

            PreOrder(tree.Root);
            if(isDone() == false)
            {
               // Next();
            }

        }

        void PreOrder(Node<T> node) 
        {
            if (node == null) return;
            _stack.Push(node);
            PreOrder(node.Left);
            PreOrder(node.Right);
        }

        public T currentItem()
        {
            if (node == null) return default(T);
            return node.V;
        }

        public void First()
        {
            node = _tree.Root;
        }

        public bool isDone()
        {
            if (_stack.Count == 0 && node == null) return true;
            return false;
        }

        public void Next()
        {
            if (_stack.Count == 0)
            {
                node = null;
                return;
            }
            node = _stack.Pop();
        }

        public bool MoveNext()
        {
            Next();
            return !isDone();
        }

        public void Reset()
        {
            _stack.Clear();
            this.node = _tree.Root;
            _stack = new Stack<Node<T>>();

            PreOrder(_tree.Root);
            if (isDone() == false)
            {
                Next();
            }
        }

        public void Dispose()
        {
            _stack.Clear();
            node = null;
        }
    }
    public class TreeReverseIterator<T> : Iiterator<T>
    {
        private BinaryTree<T> _tree;
        private Node<T>? node;
        private Stack<Node<T>> _stack;

        public T Current => node.V;

        object IEnumerator.Current => node;

        public TreeReverseIterator(BinaryTree<T> tree)
        {
            _tree = tree;
            this.node = tree.Root;
            _stack = new Stack<Node<T>>();

            PostOrder(tree.Root);
            if (isDone() == false)
            {
                Next();
            }
        }

        void PostOrder(Node<T> node)
        {
            if (node == null) return;
            PostOrder(node.Left);
        
            PostOrder(node.Right);
            _stack.Push(node);

        }

        public T currentItem()
        {
            return node.V;
        }

        public void First()
        {
            node = _tree.Root;
        }

        public bool isDone()
        {
            if (_stack.Count == 0 && node == null) return true;
            return false;
        }

        public void Next()
        {
            if (_stack.Count == 0)
            {
                node = null;
                return;
            }
            node = _stack.Pop();
        }

        public bool MoveNext()
        {
            Next();
            return !isDone();
        }

        public void Reset()
        {
            _stack.Clear();
            this.node = _tree.Root;
            _stack = new Stack<Node<T>>();

            PostOrder(_tree.Root);
            if (isDone() == false)
            {
                Next();
            }
        }

        public void Dispose()
        {
            _stack.Clear();
            node = null;
        }
    }
}

