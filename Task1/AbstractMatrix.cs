using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public sealed class Data<T> : EventArgs
    {
        public int Row { get; }
        public int Column { get; }
        public T OldValue { get; }
        public T NewValue { get; }

        public Data(int row, int column, T oldValue, T newValue)
        {
            Row = row;
            Column = column;
            OldValue = oldValue;
            NewValue = newValue;
        } 
    }

    public abstract class AbstractMatrix<T> : IEnumerable<T>
    {
        protected T[] array;
        private int size;

        public event EventHandler<Data<T>> ElementChanged = delegate { }; 

        public int Size {
            get
            {
                return size;
            }
            protected set
            {

            }
        }

        public AbstractMatrix(int size)
        {
            if (size <= 0)
                throw new ArgumentException();
        }

        public AbstractMatrix(T[] array)
        {
            int trimMatrix = (int)Math.Floor(Math.Sqrt(array.Length));
            if (trimMatrix == 0)
                throw new ArgumentException();

            Size = trimMatrix;
            array = new T[trimMatrix * trimMatrix];
        }

        protected abstract T GetElement(int row, int column);

        protected abstract void SetElement(T element, int row, int column);

        public T this[int i, int j]
        {
            get
            {
                ValidateIndexes(i, j);
                return GetElement(i, j);
            }
            set
            {
                ValidateIndexes(i, j);
                T oldValue = GetElement(i, j);
                SetElement(value, i, j);
                OnElementChanged(new Data<T>(i, j, oldValue, value));
            }
        }

        protected virtual void ValidateIndexes(int row, int column)
        {
            if (row < 0 || row >= Size)
                throw new ArgumentException();

            if (column < 0 || column >= Size)
                throw new ArgumentException();
        }

        protected virtual void OnElementChanged(Data<T> arg)
        {
            ElementChanged(this, arg); 
        }

        public AbstractMatrix<T> Accept(IVisitor<T> visitor, AbstractMatrix<T> matrix)
        {
            if (this.Size != matrix.Size)
                throw new ArgumentException();

            return visitor.Visit((dynamic)this, (dynamic)matrix); 
        } 

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    yield return this[i,j];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
