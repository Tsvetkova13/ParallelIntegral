﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace integralTest
{
	public partial class Form1 : Form
	{
		// описание токенов
		private CancellationTokenSource cts1, cts2, cts3, cts4;

		// описание переменных времени
		private Stopwatch time1, time2, time3, time4;




		public Form1()
		{
			InitializeComponent();
		}	




		// метод, в котором вычисления выполняются в отдельном потоке
		// с использованием await
		async void AsyncMethod()
		{
			// выдление памяти под переменные,
			// содержащие время выполнения для каждого метода
			time1 = new Stopwatch();
			time2 = new Stopwatch();
			time3 = new Stopwatch();
			time4 = new Stopwatch();

			// выдление памяти под переменные токенов для каждого метода
			cts1 = new CancellationTokenSource();
			cts2 = new CancellationTokenSource();
			cts3 = new CancellationTokenSource();
			cts4 = new CancellationTokenSource();

			Progress<int> progress1 = new Progress<int>();//обьявление progress bar
			Progress<int> progress2 = new Progress<int>();
			Progress<int> progress3 = new Progress<int>();
			Progress<int> progress4 = new Progress<int>();

			progress1.ProgressChanged += (sender, e) => { pgb1.Value = e; };//устанавливает значение progress bar
			progress2.ProgressChanged += (sender, e) => { pgb2.Value = e; };
			progress3.ProgressChanged += (sender, e) => { pgb3.Value = e; };
			progress4.ProgressChanged += (sender, e) => { pgb4.Value = e; };


			var res = 0.0;
			bool output = true;

			try // пробуем выполнить эту задачу
			{
				// вызов task, который расчитывает Rectangles метод.
				// В task передаем лямбда-выражение, которое запускает Rectangles метод
				// который в качестве параметров принимает токен, значение progressbar и
				// переменную времени

				res = await Task<double>.Factory.StartNew(() => Rectangles(cts1.Token, progress1, time1));
			}
			catch (OperationCanceledException) // если возникло исключение отмены
			{
				ResRecS.Text = "Отмена";
				output = false;
			}
			catch		// если возникла какая-либо ошибка
			{
				ResRecS.Text = "Ошибка";
				output = false;
			}

			if (output) // если output == true, выводим результаты
			{
				ResRecS.Text = Convert.ToString(res);				// результат вычислений
				RtimeS.Text = Convert.ToString(time1.Elapsed);		// время вычислений
			}

			output = true;

			// далее все выполняется аналогично, только в таски передаются
			// уже другие методы расчета

			try
			{
				res = await Task<double>.Factory.StartNew(() => Simpson(cts2.Token, progress2, time2));
			}
			catch (OperationCanceledException)
			{
				ResSimS.Text = "Отмена";
				output = false;
			}
			catch
			{
				ResSimS.Text = "Ошибка";
				output = false;
			}

			if (output)
			{
				ResSimS.Text = Convert.ToString(res);
				StimeS.Text = Convert.ToString(time2.Elapsed);
			}

			output = true;

			try
			{
				res = await Task<double>.Factory.StartNew(() => ParSimpson(cts3.Token, progress3, time3));
			}
			catch (OperationCanceledException)
			{
				ResSimP.Text = "Отмена";
				output = false;
			}
			catch
			{
				ResSimP.Text = "Ошибка";
				output = false;
			}

			if (output)
			{
				ResSimP.Text = Convert.ToString(res);
				StimeP.Text = Convert.ToString(time3.Elapsed);
			}

			output = true;

			try
			{
				res = await Task<double>.Factory.StartNew(() => ParRect(cts4.Token, progress4, time4));
			}
			catch (OperationCanceledException)
			{
				ResRecP.Text = "Отмена";
				output = false;
			}
			catch
			{
				ResRecP.Text = "Ошибка";
				output = false;
			}

			if (output)
			{
				ResRecP.Text = Convert.ToString(res);
				RtimeP.Text = Convert.ToString(time4.Elapsed);
			}
		}




		// функции расчета
		private double Rectangles(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
			double res = 0.0;

			if ((textBoxA.Text != "") && (textBoxB.Text != "") && (textBoxH.Text != ""))
			{
				Count p = new Count();
				double num1, num2, num3;

				string a = textBoxA.Text;
				string b = textBoxB.Text;
				string h = textBoxH.Text;

				bool AisNum = double.TryParse(a, out num1);
				bool BisNum = double.TryParse(b, out num2);
				bool HisNum = double.TryParse(h, out num3);

				if ((AisNum) && (BisNum) && (HisNum) && (num1 <= num2) && (num3 > 0.0) && (num1 > 0.0)) {

					time.Start();

					res = Math.Round(p.Rectangles(num1, num2, num3, token, progress, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3);

					time.Stop();
				}
			}

			return res;
		}
		private double ParRect(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
			double res = 0.0;

			if ((textBoxA.Text != "") && (textBoxB.Text != "") && (textBoxH.Text != ""))
			{
				Count r = new Count();
				double num1, num2, num3;

				string a = textBoxA.Text;
				string b = textBoxB.Text;
				string h = textBoxH.Text;

				bool AisNum = double.TryParse(a, out num1);
				bool BisNum = double.TryParse(b, out num2);
				bool HisNum = double.TryParse(h, out num3);

				if ((AisNum) && (BisNum) && (HisNum) && (num1 <= num2) && (num3 > 0.0) && (num1 > 0.0))
				{
					time.Start();

					res = Math.Round(r.Rectangles(num1, num2, num3, token, progress, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3);

					time.Stop();

				}
			}

			return res;
		}
		private double Simpson(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
			double res = 0.0;

			if ((textBoxA.Text != "") && (textBoxB.Text != "") && (textBoxH.Text != ""))
			{
				Count q = new Count();
				double num1, num2, num3;

				string a = textBoxA.Text;
				string b = textBoxB.Text;
				string h = textBoxH.Text;

				bool AisNum = double.TryParse(a, out num1);
				bool BisNum = double.TryParse(b, out num2);
				bool HisNum = double.TryParse(h, out num3);

				if ((AisNum) && (BisNum) && (HisNum) && (num1 <= num2) && (num3 > 0) && (num1 > 0.0))
				{
					time.Start();

					res = Math.Round(q.Simpson(num1, num2, num3, token, progress, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3);

					time.Stop();

				}
			}

			return res;
		}
		private double ParSimpson(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
			double res = 0.0;

			if ((textBoxA.Text != "") && (textBoxB.Text != "") && (textBoxH.Text != ""))
			{
				Count s = new Count();
				double num1, num2, num3;

				string a = textBoxA.Text;
				string b = textBoxB.Text;
				string h = textBoxH.Text;

				bool AisNum = double.TryParse(a, out num1);
				bool BisNum = double.TryParse(b, out num2);
				bool HisNum = double.TryParse(h, out num3);

				if ((AisNum) && (BisNum) && (HisNum) && (num1 <= num2) && (num3 > 0) && (num1 > 0.0))
				{
					time.Start();

					res = Math.Round(s.Simpson(num1, num2, num3, token, progress, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3);

					time.Stop();
				}
			}

			return res;
		}




		// при клике на кнопку отметы, срабатывает данный метод,
		// который отменяется все вычисления с помощью соответствующих токенов
		private void btnCancelled_Click(object sender, EventArgs e)
		{
			picture.Visible = true;

			if ((cts1 != null) && (cts2 != null) && (cts3 != null) && (cts4 != null))
			{
				// все токены отменяются последовательно
				cts1.Cancel();
				cts2.Cancel();
				cts3.Cancel();
				cts4.Cancel();
			}
		}




		// методы, запускающие функцию AsyncMethod() для расчета
		private void textBoxA_TextChanged(object sender, EventArgs e)
		{
			picture.Visible = false;

			AsyncMethod();
		}
		private void textBoxB_TextChanged(object sender, EventArgs e)
		{
			picture.Visible = false;

			AsyncMethod();
		}
		private void textBoxH_TextChanged(object sender, EventArgs e)
		{
			picture.Visible = false;

			AsyncMethod();
		}
	}
}
