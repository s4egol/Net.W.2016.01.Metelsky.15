using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SymmetricMatrix<T> : AbstractMatrix<T>
    {
        public SymmetricMatrix(int size) : base(size)
        {
            Size = size;
            array = new T[size * size];
        }

        public SymmetricMatrix(T[] array) : base(array)
        {
            int index = 0;
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    if (i >= j)
                        this.array[i * Size + j] = array[index];
                    index++;
                }
        }

        protected override T GetElement(int row, int column)
        {
            if (row.CompareTo(column) <= 0)
                return array[row * Size + column];
            else 
                return array[column * Size + row];

        }

        protected override void SetElement(T element, int row, int column)
        {
            if (row.CompareTo(column) <= 0)
                array[row * Size + column] = element;
            else 
                array[column * Size + row] = element;

        }
    }
}
