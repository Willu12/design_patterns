using System;
namespace BTM
{
	public class Vector<T> : ICollection<T>
	{
		private int _length;

		private int _arrayLength;
		private T[] _array;

		public Vector(int length)
		{
			Length = length;
			_arrayLength = 2 * length;
			_array = new T[_arrayLength];
		}

        public Vector(List<T> list) : this(list.Count)
        {
            for(int i =0; i<list.Count; i++)
            {
                _array[i] = list[i];
            }
        }

        public int Length { get => _length; set => _length = value; }

		public T this[int key] { get => _array[key]; set => _array[key] = value;}
        public void Add(T obj)
        {
            if(_arrayLength == Length)
			{
				int missing_elements = 0;
				_arrayLength *= 2;
				T[] new_arr = new T[_arrayLength];

				for (int i = 0; i < Length; i++)
				{
					if (_array[i].Equals(default(T)))
					{
                       // new_arr[i] = _array[i];
						missing_elements++;
						continue;
                    }
					new_arr[i - missing_elements] = _array[i]; 
                }


                _array = new_arr;
			}

			_array[Length++] = obj;
			

        }

        public Iiterator<T> CreateForwardIterator()
        {
            return new ForwardVectorIterator<T>(this);
        }

        public Iiterator<T> CreateReverseIterator()
        {
            return new ReverseVectorIterator<T>(this);
        }

        public bool Delete(T obj)
        {
			if (Length == 0) return false;


			//pushback
			if (_array[_length -1].Equals(obj))
			{
				_array[_length - 1] = default(T);
				_length--;
				return true;
			}
			return false;
        }
    }


	public class ForwardVectorIterator<T> : Iiterator<T>
	{
		private Vector<T> vector;
		private int i;

        public ForwardVectorIterator(Vector<T> vector)
        {
            this.vector = vector;
            this.i = 0;
        }

        public T currentItem()
        {
			return vector[i];
        }

        public void First()
        {
			i = 0;
        }

        public bool isDone()
        {
			return i >= vector.Length;
        }

        public void Next()
        {
			i++;
        }
    }

    public class ReverseVectorIterator<T> : Iiterator<T>
    {
        private Vector<T> vector;
        private int i;

        public ReverseVectorIterator(Vector<T> vector)
        {
            this.vector = vector;
            this.i = vector.Length -1;
        }

        public T currentItem()
        {
            return vector[i];
        }

        public void First()
        {
            i = vector.Length -1;
        }

        public bool isDone()
        {
            return i < 0;
        }

        public void Next()
        {
            i--;
        }
    }


}

