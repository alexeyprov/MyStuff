﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
		lblTime.Text = DateTime.Now.ToString();
    }
	protected void btnGetTime_Click(object sender, EventArgs e)
	{
		//lblTime.Text = DateTime.Now.ToString();
	}
}
