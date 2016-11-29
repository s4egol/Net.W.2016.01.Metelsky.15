using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IVisitor<T>
    {
        SquareMatrix<T> Visit(SquareMatrix<T> squareMatrix1, SquareMatrix<T> squareMatrix2);
        SquareMatrix<T> Visit(SquareMatrix<T> squareMatrix1, SymmetricMatrix<T> symmetricMatrix2);
        SquareMatrix<T> Visit(SquareMatrix<T> squareMatrix1, DiagonalMatrix<T> diagonalMatrix2);
        SymmetricMatrix<T> Visit(SymmetricMatrix<T> symmetricMatrix1, SymmetricMatrix<T> symmetricMatrix2);
        SquareMatrix<T> Visit(SymmetricMatrix<T> symmetricMatrix1, SquareMatrix<T> symmetricMatrix2);
        SymmetricMatrix<T> Visit(SymmetricMatrix<T> symmetricMatrix1, DiagonalMatrix<T> symmetricMatrix2);
        DiagonalMatrix<T> Visit(DiagonalMatrix<T> diagonalMatrix1, DiagonalMatrix<T> diagonalMatrix2);
        SquareMatrix<T> Visit(DiagonalMatrix<T> diagonalMatrix1, SquareMatrix<T> diagonalMatrix2);
        SquareMatrix<T> Visit(DiagonalMatrix<T> diagonalMatrix1, SymmetricMatrix<T> diagonalMatrix2);
    }

    public class ComputeSumMatrixVisitor<T> : IVisitor<T>
    {
        public SquareMatrix<T> Visit(SquareMatrix<T> squareMatrix1, SquareMatrix<T> squareMatrix2)
        {
            T[] obtainedArray = SumMatrix(squareMatrix1, squareMatrix2);
            return new SquareMatrix<T>(obtainedArray);
        }

        public SquareMatrix<T> Visit(SquareMatrix<T> squareMatrix1, SymmetricMatrix<T> symmetricMatrix2)
        {
            T[] obtainedArray = SumMatrix(squareMatrix1, symmetricMatrix2);
            return new SquareMatrix<T>(obtainedArray);
        }

        public SquareMatrix<T> Visit(SquareMatrix<T> squareMatrix1, DiagonalMatrix<T> diagonalMatrix2)
        {
            T[] obtainedArray = SumMatrix(squareMatrix1, diagonalMatrix2);
            return new SquareMatrix<T>(obtainedArray);
        }

        public SymmetricMatrix<T> Visit(SymmetricMatrix<T> symmetricMatrix1, SymmetricMatrix<T> symmetricMatrix2)
        {
            T[] obtainedArray = SumMatrix(symmetricMatrix1, symmetricMatrix1);
            return new SymmetricMatrix<T>(obtainedArray);
        }

        public SquareMatrix<T> Visit(SymmetricMatrix<T> symmetricMatrix1, SquareMatrix<T> squareMatrix2)
        {
            T[] obtainedArray = SumMatrix(symmetricMatrix1, squareMatrix2);
            return new SquareMatrix<T>(obtainedArray);
        }

        public SymmetricMatrix<T> Visit(SymmetricMatrix<T> symmetricMatrix1, DiagonalMatrix<T> diagonalMatrix2)
        {
            T[] obtainedArray = SumMatrix(symmetricMatrix1, diagonalMatrix2);
            return new SymmetricMatrix<T>(obtainedArray);
        }

        public DiagonalMatrix<T> Visit(DiagonalMatrix<T> diagonalMatrix1, DiagonalMatrix<T> diagonalMatrix2)
        {
            T[] obtainedArray = SumMatrix(diagonalMatrix1, diagonalMatrix2);
            return new DiagonalMatrix<T>(obtainedArray);
        }

        public SquareMatrix<T> Visit(DiagonalMatrix<T> diagonalMatrix1, SquareMatrix<T> squareMatrix2)
        {
            T[] obtainedArray = SumMatrix(diagonalMatrix1, squareMatrix2);
            return new SquareMatrix<T>(obtainedArray);
        }

        public SquareMatrix<T> Visit(DiagonalMatrix<T> diagonalMatrix1, SymmetricMatrix<T> symmetricMatrix2)
        {
            T[] obtainedArray = SumMatrix(diagonalMatrix1, symmetricMatrix2);
            return new SquareMatrix<T>(obtainedArray);
        }      

        private T[] SumMatrix(AbstractMatrix<T> matrix1, AbstractMatrix<T> matrix2)
        {
            T[] rArray = new T[matrix1.Size*matrix1.Size];

            int index = 0;
            for (int i = 0; i < matrix1.Size; i++)
            {
                for (int j = 0; j < matrix1.Size; j++)
                {
                    rArray[index] = checked(new Param<T>(matrix1[i, j]) + new Param<T>(matrix2[i, j]));
                    index++;
                }
            }

            return rArray;
        } 
    }

    class Param<T>
    {
        private static readonly Func<T, T, T> addMethod;
        public T parameter;
        static Param()
        {
            try
            {
                ParameterExpression left = Expression.Parameter(typeof(T), "left");
                ParameterExpression right = Expression.Parameter(typeof(T), "right");
                addMethod = Expression.Lambda<Func<T, T, T>>(Expression.Add(left, right), left, right).Compile();
            }
            catch (InvalidOperationException exp)
            {
                throw new InvalidOperationException("Operation isn't supported by this type of", exp);
            }
        }

        public Param(T value)
        {
            parameter = value;
        }

        public static T operator +(Param<T> leftOperand, Param<T> rightOperand)
        {
            try
            {
                return addMethod(leftOperand.parameter, rightOperand.parameter);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
