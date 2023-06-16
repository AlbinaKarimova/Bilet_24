using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilet_24
{
    internal class Matrix
    {
        public int n;
        public int m;
        public double[,] matr;

        public int N
        {
            get => n;
            set => n = value;
        }

        public double[,] Matr
        {
            get => matr;
            set => matr = value;
        }
        
        public Matrix(List<double> elems, string _n)
        {
            this.n = Convert.ToInt32(_n);

            double[,] m = new double[this.n, this.n];
            int count = 0;
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    m[i, j] = elems[count];
                    count++;
                }
            }
            this.matr = m;
        }

        //Конструктор для матрицы с рандомными элементами
        public Matrix(int n = 0, int m = 0)
        {
            Random random = new();
            this.n = n;
            this.m = m;
            double[,] matr = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matr[i, j] = Math.Round(Convert.ToInt64(random.NextDouble() * 10000) / 100.0, 2);
                    //matr[i,j] = random.NextDouble(); // этот рандом выводит числа между 0 и 1
                }
            }
            this.matr = matr;
        }


        public double Deter(Matrix m_det, int n)
        {

            if (m_det.n == 1) return m_det.matr[0, 0];
            else if (m_det.n == 2) return m_det.matr[0, 0] * m_det.matr[1, 1] - m_det.matr[0, 1] * m_det.matr[1, 0];

            double det = 0;
            int k = 1;

            for (int i = 0; i < m_det.n; i++)
            {
                Matrix m_for_minor = Get_Minor_matr(m_det, i, 0);
                det += k * m_det.matr[i, 0] * Deter(m_for_minor, n - 1);
                k *= -1;
            }
            return det;
        }

        public Matrix Get_Minor_matr(Matrix A, int row, int col)
        {
            Matrix m_without_row_and_col = CreateMatrixWithoutRow(A, row);
            m_without_row_and_col = CreateMatrixWithoutColumn(m_without_row_and_col, col);
            Matrix m_res = Copy_matrix(m_without_row_and_col);
            return m_res;
        }

        // Копия квадратной матрицы
        public Matrix Copy_matrix(Matrix m)
        {
            Matrix matrix = new(m.n);
            for (int i = 0; i < m.n; i++)
            {
                for (int j = 0; j < m.n; j++)
                {
                    matrix.matr[i, j] = m.matr[i, j];
                }
            }
            return matrix;
        }

        // Удаление строки
        public Matrix CreateMatrixWithoutRow(Matrix A, int row)
        {
            if (row < 0 || row >= A.n)
            {
                throw new ArgumentException("invalid rows index");
            }

            else
            {
                Matrix m_res = new(A.n - 1, A.m);
                for (int i = 0; i < A.n; i++)
                {
                    for (int j = 0; j < A.m; j++)
                    {
                        if (i < row)
                        {
                            m_res.matr[i, j] = A.matr[i, j];
                        }
                        if (i > row)
                        {
                            m_res.matr[i - 1, j] = A.matr[i, j];
                        }

                    }
                }
                return m_res;
            }
        }

        // Удаление столбца
        public Matrix CreateMatrixWithoutColumn(Matrix A, int column)
        {
            if (column < 0 || column >= A.n)
            {
                throw new ArgumentException("invalid column index");
            }

            else
            {
                Matrix m_res = new(A.n, A.m - 1);
                for (int i = 0; i < A.n; i++)
                {
                    for (int j = 0; j < A.m; j++)
                    {
                        if (j < column)
                        {
                            m_res.matr[i, j] = A.matr[i, j];
                        }
                        if (j > column)
                        {
                            m_res.matr[i, j - 1] = A.matr[i, j];
                        }

                    }
                }
                return m_res;
            }
        }
    }
}
