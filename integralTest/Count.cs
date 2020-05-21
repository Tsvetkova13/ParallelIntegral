using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace integralTest
{
    public class Count : Iintegral
    {
        public double Rectangles(double a, double b, double h, CancellationToken token, IProgress<int> progress, Func<double, double> func)
		{//принимаем токен и progress

			double hh = h;
			double N = (b - a) / h;
			double x = a;
			double s = 0;
            double k = 0.5;

			a += h * k;

			for (int i = 0; i < N-1; i++)
			{
				token.ThrowIfCancellationRequested();//вызов исключения который прописаны в try catch

				x = a + h * i;
				s += func(x);

				if (i % 2000 == 0)//шаг заполнения progress bar
					progress.Report(i * 100 / (int)N);//заполнение progress bar
			}

			return s * hh;
		}
		public double ParRect(double a, double b, double h, CancellationToken token, IProgress<int> progress, Func<double, double> func)
		{//принимаем токен и progress
			double S = 0.0;
			int count = 0;

			if (h != 0.0)
			{
				int n = Convert.ToInt32((b - a) / h);
				double[] buf = new double[n];

				Parallel.For(1, n, new ParallelOptions() { CancellationToken = token }, i =>// Metod Parallel.For включает в себя уже отмену, поэтому его как то  
																							//принудительно останавливать не нужно
				{
					buf[i] = func(a + i * h);

					Interlocked.Increment(ref count);//Increment добавляет 1(cont++)//Interlocked для того чтобы переменные были доступны всем
					if (n % 2000 == 0)//шаг заполнения  progress bar
						progress.Report(count * 100 / n);//заполнение progress bar

				});

				S = h * (buf.AsParallel().Sum(X => X));
			}

			return S;
		}
		public double Simpson(double a, double b, double h, CancellationToken token, IProgress<int> progress, Func<double, double> func)
		{//принимаем токен и progress

			double hh = h;
			double S = 0;
			double N = (b - a) / hh;
			int n = 0;
			double x = a;
			double I = func(a) + func(b);

			while (n < N - 1)
			{
				token.ThrowIfCancellationRequested();//вызов исключения который прописаны в try catch

				x = x + hh;
				if (n % 2 == 0) I = I + 4 * func(x);
				else I = I + 2 * func(x);
				n++;

				if (n % 2000 == 0)//шаг заполнения progress bar
					progress.Report(n * 100 / (int)N);//заполнение progress bar
			}

			return S = I * (hh / 3);

		}
		public double ParSimpson(double a, double b, double h, CancellationToken token, IProgress<int> progress, Func<double, double> func)
		{//принимаем токен и progress

			double S = 0.0;
			int count = 0;

			if (h != 0.0)
			{
				int N = Convert.ToInt32((b - a) / h);

				double[] buf = new double[N];
				double[] x = new double[N];

				Parallel.For(0, N, new ParallelOptions() { CancellationToken = token }, i =>
				{
					x[i] = a + i * h;

					if (i % 2 == 0) { buf[i] = 4.0 * func(x[i]);}// Metod Parallel.For включает в себя уже отмену, поэтому его как то  
					//принудительно останавливать не нужно
					else { buf[i] = 2.0 * func(x[i]); }

					Interlocked.Increment(ref count);//Increment добавляет 1(cont++)//Interlocked для того чтобы переменные были доступны всем
					if (count % 2000 == 0)//шаг заполнения progress bar
						progress.Report(count * 100 / N);//заполнение progress bar
				});

				S = h / 3.0 * (func(a) + func(b) + buf.AsParallel().Sum(X => X));
			}

			return S;
		}
	}
}
