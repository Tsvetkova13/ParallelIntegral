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

			progress1.ProgressChanged += (sender, e) => { pgb1.Value = e; };
			progress2.ProgressChanged += (sender, e) => { pgb2.Value = e; };
			progress3.ProgressChanged += (sender, e) => { pgb3.Value = e; };
			progress4.ProgressChanged += (sender, e) => { pgb4.Value = e; };

			var res = 0.0;
			bool output = true;

			try
			{
				res = await Task<double>.Factory.StartNew(() => Rectangles(cts1.Token, progress1, time1));
			}
			catch (OperationCanceledException)
			{
				Trap_out.Text = "Отмена";
				output = false;
			}
			catch
			{
				Trap_out.Text = "Ошибка";
				output = false;
			}

			if (output)
			{
				Trap_out.Text = Convert.ToString(res);
				eTrap.Text = Convert.ToString(time1.Elapsed);
			}
		}

		private void Rectangles(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
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

				if ((AisNum) && (BisNum) && (HisNum) && (num1 <= num2) && (num3 > 0.0) && (num1 > 0.0))
				{
					DateTime t1 = DateTime.Now;

					ResRecS.Text = Convert.ToString(Math.Round(p.Rectangles(num1, num2, num3, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3));

					TimeSpan time = DateTime.Now - t1;
					RtimeS.Text = Convert.ToString(time.TotalSeconds) + " сек";

				}
			}
		}
		
		private void ParRect(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
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
					DateTime t1 = DateTime.Now;

					ResRecP.Text = Convert.ToString(Math.Round(r.Rectangles(num1, num2, num3, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3));

					TimeSpan time = DateTime.Now - t1;
					RtimeP.Text = Convert.ToString(time.TotalSeconds) + " сек";

				}
			}
		}

		private void Simpson(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
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
					DateTime t1 = DateTime.Now;

					ResSimS.Text = Convert.ToString(Math.Round(q.Simpson(num1, num2, num3, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3));

					TimeSpan time = DateTime.Now - t1;
					StimeS.Text = Convert.ToString(time.TotalSeconds) + " сек";

				}
			}
		}

		private void ParSimpson(CancellationToken token, IProgress<int> progress, Stopwatch time)
		{
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
					DateTime t1 = DateTime.Now;

					ResSimP.Text = Convert.ToString(Math.Round(s.Simpson(num1, num2, num3, x => 31.0 * x - Math.Log(5.0 * x) + 5.0), 3));

					TimeSpan time = DateTime.Now - t1;
					StimeP.Text = Convert.ToString(time.TotalSeconds) + " сек";

				}
			}
		}

	
		private void textBoxA_TextChanged(object sender, EventArgs e)
		{
			Rectangles();
			Simpson();
			ParRect();
			ParSimpson();
		}

		private void textBoxB_TextChanged(object sender, EventArgs e)
		{
			Rectangles();
			Simpson();
			ParRect();
			ParSimpson();
		}

		private void textBoxH_TextChanged(object sender, EventArgs e)
		{
			Rectangles();
			Simpson();
			ParRect();
			ParSimpson();
		}

		private void ResSimS_TextChanged(object sender, EventArgs e)
		{
			Simpson();
		}

		private void StimeS_TextChanged(object sender, EventArgs e)
		{
			//
		}

		private void ResRecS_TextChanged(object sender, EventArgs e)
		{
			Rectangles();
		}

		private void ResSimP_TextChanged(object sender, EventArgs e)
		{
			ParSimpson();
		}

		private void ResRecP_TextChanged(object sender, EventArgs e)
		{
			ParRect();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
