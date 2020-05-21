using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace integralTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Rectangles()
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
		private void ParRect()
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

		private void Simpson()
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

		private void ParSimpson()
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
	}
}
