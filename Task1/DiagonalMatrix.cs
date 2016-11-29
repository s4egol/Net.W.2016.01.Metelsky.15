using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class DiagonalMatrix<T> : AbstractMatrix<T>
    {
        public DiagonalMatrix(int size) : base(size)
        {
            Size = size;
            array = new T[size * size];
        }

        public DiagonalMatrix(T[] array) : base(array)
        {
            int index = 0;
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    if (i == j)
                        this.array[i * Size + j] = array[index];
                    index++;
                }
        }

        protected override T GetElement(int row, int column)
        {
            if (row == column)
                return array[row * Size + column];
            else
                return default(T);
        }

        protected override void SetElement(T element, int row, int column)
        {
            if (row == column)
                array[row * Size + column] = element;
            else
                throw new ArgumentException();
        }
    }
}
