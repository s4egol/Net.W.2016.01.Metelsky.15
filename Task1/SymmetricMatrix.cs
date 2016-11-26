using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SymmetricMatrix<T> : SquareMatrix<T>
    {
        public SymmetricMatrix(IEnumerable<T> collection) : base(collection)
        {

        }

        public override T this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size)
                    throw new ArgumentOutOfRangeException();

                if (i.CompareTo(j) <= 0)
                    return array[i * Size + j];
                else
                    return array[j * Size + i];
            }
            set
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size)
                    throw new ArgumentOutOfRangeException();

                if (i.CompareTo(j) <= 0)
                    array[i * Size + j] = value;
                else
                    array[j * Size + i] = value;

                OnElementChanged(new Data<T>(i, j));
            }
        }
    }
}
