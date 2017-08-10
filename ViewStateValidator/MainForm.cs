using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ViewStateValidator
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnParseEventValidation_Click(object sender, EventArgs e)
		{
			string text = txtEventValidation.Text;

			if (String.IsNullOrEmpty(text))
			{
				return;
			}

			text = text.Replace("\n", String.Empty).Replace(" ", String.Empty);

			if (String.IsNullOrEmpty(text))
			{
				return;
			}
		}
	}
}
