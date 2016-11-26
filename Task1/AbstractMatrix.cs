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
        public int Row { get; protected set; }
        public int Column { get; protected set; }

        public Data(int row, int column)
        {
            Row = row;
            Column = column;
        } 
    }

    public abstract class AbstractMatrix<T> : IEnumerable<T>
    {
        protected T[] array;

        public event EventHandler<Data<T>> ElementChanged = delegate { }; 

        public int Size { get; protected set; }

        public AbstractMatrix(IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null))
                throw new ArgumentNullException();

            int trimMatrix = (int)Math.Floor(Math.Sqrt(collection.Count()));
            if (trimMatrix == 0)
                throw new ArgumentException();
            Size = trimMatrix;
            array = new T[trimMatrix * trimMatrix];

            int index = 0;
            foreach (var value in collection)
            {
                array[index] = value;
                index++;
            }
        }

        public AbstractMatrix(int trimMatrix)
        {
            this.Size = trimMatrix*trimMatrix;
        } 

        public abstract T this[int i, int j] { get; set; }

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
            for (int i = 0; i < array.Length; i++)
                yield return array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
