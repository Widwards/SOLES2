
using CSML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOLES2.Infrastructure
{
    public class LinEqSolver
    {
        private double[,] MM { get; set; }
        private double[] Vector { get; set; }
        private int Size { get; set; }

        public double[,] LUSafe { get; set; }

        public LinEqSolver()
        {

        }
        public LinEqSolver(double[,] mm, double[] vector, int size)
        {
            this.MM = mm;
            this.Vector = vector;
            this.Size = size;
        }

        public double[] Solve()
        {



            Matrix mm = new Matrix(MM);
            Matrix vector = new Matrix(Vector);
            Matrix solution = Matrix.Solve(mm, vector);
            Matrix luSafe = mm.LUSafe();

            Complex cmp = new Complex();
            var ret = new double[Size];

            for (int i = 0; i < Size; i++)
            {
                ret[i] = solution[i + 1].Re;
            }

            double[,] lu = new double[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    lu[i, j] = luSafe[i + 1, j + 1].Re;

                    
                }

            }


            LUSafe = lu;
            return ret;
        }
    }
}