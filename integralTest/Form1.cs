using System;
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
		private CancellationTokenSource cts1, cts2, cts3, cts4;
		private Stopwatch time1, time2, time3, time4;

		public Form1()
		{
			InitializeComponent();
		}

		async void AsyncMethod()
		{
			time1 = new Stopwatch();
			time2 = new Stopwatch();
			time3 = new Stopwatch();
			time4 = new Stopwatch();

			cts1 = new CancellationTokenSource();
			cts2 = new CancellationTokenSource();
			cts3 = new CancellationTokenSource();
			cts4 = new CancellationTokenSource();

			Progress<int> progress1 = new Progress<int>();
			Progress<int> progress2 = new Progress<int>();
			Progress<int> progress3 = new Progress<int>();
			Progress<int> progress4 = new Progress<int>();

			var res = 0.0;
			bool output = true;

			try
			{
				res = await Task<double>.Factory.StartNew(() => Rectangles(cts1.Token, progress1, time1));
			}
			catch (OperationCanceledException)
			{
				ResRecS.Text = "Отмена";
				output = false;
			}
			catch
			{
				ResRecS.Text = "Ошибка";
				output = false;
			}

			if (output)
			{
				ResRecS.Text = Convert.ToString(res);
				RtimeS.Text = Convert.ToString(time1.Elapsed);
			}

			output = true;

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

		private void btnCancelled_Click(object sender, EventArgs e)
		{
			picture.Visible = true;

			if ((cts1 != null) && (cts2 != null) && (cts3 != null) && (cts4 != null))
			{
				cts1.Cancel();
				cts2.Cancel();
				cts3.Cancel();
				cts4.Cancel();
			}
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

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
