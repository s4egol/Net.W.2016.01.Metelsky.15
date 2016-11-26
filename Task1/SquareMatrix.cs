using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SquareMatrix<T> : AbstractMatrix<T>
    {

        public SquareMatrix(IEnumerable<T> collection) : base(collection)
        {

        }

        public override T this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size)
                    throw new ArgumentOutOfRangeException();

                return array[i*Size + j];
            }
            set
            {
                if (i < 0 || j < 0 || i >= Size || j >= Size)
                    throw new ArgumentOutOfRangeException();

                array[i*Size + j] = value;
                OnElementChanged(new Data<T>(i,j));
            }
        }
    }
}
