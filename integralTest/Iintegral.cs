using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integralTest
{
   public interface Iintegral
    {
        double Rectangles(double a, double b, double h, Func<double, double> func);
        double Simpson(double a, double b, double h, Func<double, double> func);
		double ParSimpson(double a, double b, double h, Func<double, double> func);
		double ParRect(double a, double b, double h, Func<double, double> func);
	}
}
