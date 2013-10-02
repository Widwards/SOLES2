using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOLES2.Infrastructure
{
    public class SolutionBuilder
    {
        public int SystemSize { get; set; }
        public bool IsXForm { get; set; }
        public double[] Vector { get; set; }

        public SolutionBuilder(double[] vector, bool isXForm)
        {
            this.Vector = vector;
            this.SystemSize = vector.Length;
            this.IsXForm = isXForm;
        }


        public string Build()
        {
            string ret="<div class=\"alert alert-dismissable alert-success\"><ul>";
            if (IsXForm)
            {
                for (int i = 1; i <= SystemSize; i++)
                {
                    ret += "<li>";
                    ret += "X" + i + " = ";
                    ret += Vector[i - 1];
                    ret += "</li>";
                }
            
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    ret += "<li>";
                    switch (i)
                    {
                        case 0:
                            ret += "X= " + Vector[0];
                            break;
                        case 1:
                            ret += "Y= " + Vector[1];
                            break;
                        case 2:
                            ret += "Z= " + Vector[2];
                            break;
                    }
                }
            }
            ret += "</ul></div>";
            return ret;

        }
    }
}