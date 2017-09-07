using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator;

namespace ViewForm
{
	public partial class mainForm : Form
	{
		public mainForm()
		{
			InitializeComponent();
		}

		private void EnterText(object sender, EventArgs e)
		{
			if (txtView.Tag == "Answer")
				txtView.Text = "";
			txtView.Text += ((Control)sender).Text;
			txtView.Tag = "";
		}

		private void btnC_Click(object sender, EventArgs e)
		{
			txtView.Text = "";
		}

		private void btnEqual_Click(object sender, EventArgs e)
		{
			try
			{
				Polinomial p = new Polinomial(txtView.Text);
				txtView.Text = p.Answer(new string[0]);
				txtView.Tag = "Answer";
			}
			catch
			{
				txtView.Text = "Error: Invalid terms.";
				txtView.Tag = "Answer";
			}
		}
	}
}
