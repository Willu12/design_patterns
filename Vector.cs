using System;
using System.Collections;

namespace BTM
{
	public class Vector<T> : ICollection<T>
	{
		private int _length;

		private int _arrayLength;
		private T[] _array;

        public T this[int i]
        {
            get
            {
                if (i > Length || i < 0) throw new ArgumentOutOfRangeException();
                return _array[i];
            }
            set
            {
                if (i > Length || i < 0) throw new ArgumentOutOfRangeException();
                _array[i] = value;
            }
        }

		public Vector(int length)
		{
			Length = length;
			_arrayLength = 2 * length;
			_array = new T[_arrayLength];
		}

        public Vector()
        {
            Length = 0;
            _arrayLength = 1;
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

        List<T> toList()
        {
            List<T> list = new List<T>();
            for(int i =0; i<Length; i++)
            {
                list.Add(this[i]);
            }
            return list;
        }

        bool divideByHalf()
        {
            if (Length * 4 > _arrayLength) return false;
            List<T> list = this.toList();
            Vector<T> newVector = new Vector<T>(list);
            this._array = newVector._array;
            this.Length = newVector.Length;
            this._arrayLength = newVector._arrayLength;
            return true;
        }

       
        public bool popBack()
        {
            if (Length == 0) return false;

            _array[Length] = default(T);
            Length--;

            if(_arrayLength >= 4 * Length)
            {
                divideByHalf();
            }
            return true;
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

        public IEnumerator<T> GetEnumerator()
        {
            return CreateForwardIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public T currentItem()
        {
			return vector[i];
        }

        public void Dispose()
        {
            vector = null;
        }

        public void First()
        {
			i = 0;
        }

        public bool isDone()
        {
			return i >= vector.Length;
        }

        public bool MoveNext()
        {
            Next();
            return isDone();
        }

        public void Next()
        {
			i++;
        }

        public void Reset()
        {
            First();
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

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public T currentItem()
        {
            return vector[i];
        }

        public void Dispose()
        {
            vector = null;
        }

        public void First()
        {
            i = vector.Length -1;
        }

        public bool isDone()
        {
            return i < 0;
        }

        public bool MoveNext()
        {
            Next();
            return isDone();
        }

        public void Next()
        {
            i--;
        }

        public void Reset()
        {
            First();
        }
    }


}

