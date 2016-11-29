using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SquareMatrix<T> : AbstractMatrix<T>
    {
        public SquareMatrix(int size) : base(size)
        {
            Size = size;
            array = new T[size * size];
        }

        public SquareMatrix(T[] array) : base(array)
        {
            if (ReferenceEquals(array, null))
                throw new ArgumentNullException();

            int index = 0;
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    this.array[i * Size + j] = array[index];
                    index++;
                }
        }

        protected override T GetElement(int row, int column)
        {
            return array[row * Size + column];
        }

        protected override void SetElement(T element, int row, int column)
        {
            array[row * Size + column] = element;
        }
    }
}
