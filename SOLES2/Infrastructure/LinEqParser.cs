using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SOLES2.Infrastructure
{
    public class LinEqParser
    {
        private string _expression;

        public bool IsXForm { get; set; }
        public double[,] ResultMatrix { get; set; }
        public double[,] MM { get; set; }
        public double[] Vector { get; set; }
        public int ColN { get; set; }
        public int RowN { get; set; }
        public int SystemSize { get; set; }

        public LinEqParser(string expressionForParse)
        {
            this._expression = expressionForParse;
        }

        public void Initialize()
        {
            ResultMatrix = Parse(_expression);
            RowN = ResultMatrix.GetLength(0);
            ColN = ResultMatrix.GetLength(1);
            SystemSize = RowN;
            MM = GetMM(ResultMatrix, SystemSize);
            Vector = GetVector(ResultMatrix, SystemSize);
            int a = 5;
        }


        public double[,] Parse(string expression)
        {
            var result = NormalizeSystemString(expression);
            var ret = ParseMatrix(result);
            return ret;
            //return result;
        }

        private static double[,] ParseMatrix(string ps)
        {
            var s = ps;
            var rows = Regex.Split(s, "\r\n");
            var nums = rows[0].Split(' ');
            var matrix = new double[rows.Length, nums.Length];
            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    nums = rows[i].Split(' ');
                    for (int j = 0; j < nums.Length; j++) matrix[i, j] = double.Parse(nums[j]);
                }
            }
            catch (FormatException exc) { throw new FormatException("Wrong expression format!"); }
            return matrix;

        }

        private string NormalizeSystemString(string expression)
        {
            IsXForm = false;
            var result = expression;
            IsXForm = Regex.IsMatch(result, @"(x\d)");
            //replace all "-x1", "-x2"...
            result = Regex.Replace(result, @"(-\s*x\d)", " -1 ");
            //replace all "x1", "x2" on "x"
            result = Regex.Replace(result, @"(x\d)", "x");

            //replace all "-x", "-y", "-z"
            result = Regex.Replace(result, @"(-\s*x)|(-\s*y)|(-\s*z)|(-x)|(-y)|(-z)", " -1 ");
            //result = Regex.Replace(result, @"|(-\s*x\d)", "")

            //replace all "x", "y", "z" withoud digits
            result = Regex.Replace(result, @"(^\d*x)|(\+\s*x)|(^\d*y)|(\+\s*y)|(^\d*z)|(\+\s*z)" + @"|((^\d*x\d)|(\+\s*x\d))", " 1 ");
            

            //replace all "x", "y", "z" at the beginning of the line
            result = Regex.Replace(result, @"\r\nx|\r\n x|\r\n  x", "\r\n1 ");

            //replace all "x", "y", "z", "=", "+"
            result = Regex.Replace(result, @"x|y|z|=|\+", " ");

            //replace all spaces
            result = Regex.Replace(result, @" {2,}", " ");

            //space at the begining of the line
            result = Regex.Replace(result, @"^ ", "");
            result = Regex.Replace(result, @"\r\n ", "\r\n");

            return result;
        }

        public double[,] GetMM(double[,] fullSystemMatrix, int systemSize)
        {
            var matrix = fullSystemMatrix;
            var size = systemSize;
            var ret = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    ret[i, j] = matrix[i, j];
                }
            }
            return ret;
        }
        public double[] GetVector(double[,] fullSystemMatrix, int systemSize)
        {
            var matrix = fullSystemMatrix;
            var coln = fullSystemMatrix.GetLength(1);
            var size = systemSize;
            var ret = new double[size];

            for (var i = 0; i < size; i++)
            {
                ret[i] = matrix[i, coln - 1];
            }

            return ret;
        }
    }
}