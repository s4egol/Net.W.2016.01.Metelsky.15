using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task1;


namespace Task1.Tests
{
    [TestFixture]
    public class SquareMatrixTests
    {

        [Test]
        public void SumSquareMatrixAndSquareMatrix_ReturnedValue()
        {
            int[] array = new int[] {1,1,1,1,1,1,1,1,1};
            SquareMatrix<int> matrix1 = new SquareMatrix<int>(array);
            SquareMatrix<int> matrix2 = new SquareMatrix<int>(array);

            ComputeSumMatrixVisitor<int> visitor = new ComputeSumMatrixVisitor<int>();
            matrix1 = (SquareMatrix<int>)matrix1.Accept(visitor, matrix2);

            foreach (var value in matrix1)
            {
                Assert.AreEqual(value, 2);
            }
        }

        [Test]
        public void SumSymmetricMatrixAndSquareMatrix_ReturnedValue()
        {
            int[] array = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] array2 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            SymmetricMatrix<int> matrix1 = new SymmetricMatrix<int>(array);
            SquareMatrix<int> matrix2 = new SquareMatrix<int>(array);

            ComputeSumMatrixVisitor<int> visitor = new ComputeSumMatrixVisitor<int>();
            matrix2 = (SquareMatrix<int>)matrix1.Accept(visitor, matrix2);

            int[] result = new int[] {2,3,4,3,6,7,4,3,10};
            int index = 0;
            for (int i = 0; i < matrix2.Size; i++)
            {
                for (int j = 0; j < matrix2.Size; j++)
                {
                    Assert.AreEqual(result[index], matrix2[i,j]);
                }
            }                 
        }
    }
}
