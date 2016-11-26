using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(IEnumerable<T> collection) : base(collection)
        {

        }

        public override T this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size)
                    throw new ArgumentOutOfRangeException();

                if (i != j)
                    return default(T);
                else
                    return array[i];
            }
            set
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size || i != j)
                    throw new ArgumentOutOfRangeException();

                array[i] = value;
                OnElementChanged(new Data<T>(i,j));
            }
        }
    }
}
