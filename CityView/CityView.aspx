<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CityView.aspx.vb" Inherits="CityView.CityView"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<h1>City View</h1>
		<hr>
		<form method="post" runat="server">
			<TABLE cellPadding="8" border="0">
				<TR>
					<TD>City</TD>
					<TD>
						<asp:TextBox id="txtCity" runat="server" Width="100%"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>State</TD>
					<TD>
						<asp:DropDownList id="cmbState" runat="server" Width="100%">
							<asp:ListItem Value="AL">AL</asp:ListItem>
							<asp:ListItem Value="AK">AK</asp:ListItem>
							<asp:ListItem Value="AR">AR</asp:ListItem>
							<asp:ListItem Value="AZ">AZ</asp:ListItem>
							<asp:ListItem Value="CA">CA</asp:ListItem>
							<asp:ListItem Value="CO">CO</asp:ListItem>
						</asp:DropDownList></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD>
						<fieldset>
							<legend>
								Scale</legend>
							<asp:RadioButtonList id="btnScale" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
								<asp:ListItem Value="1 meter">1 meter</asp:ListItem>
								<asp:ListItem Value="2 meters">2 meters</asp:ListItem>
								<asp:ListItem Value="4 meters">4 meters</asp:ListItem>
								<asp:ListItem Value="8 meters" Selected="True">8 meters</asp:ListItem>
								<asp:ListItem Value="16 meters">16 meters</asp:ListItem>
								<asp:ListItem Value="32 meters">32 meters</asp:ListItem>
							</asp:RadioButtonList>
						</fieldset>
					</TD>
				</TR>
				<TR>
					<TD></TD>
					<TD>
						<asp:Button id="cmdShowImage" runat="server" Width="100%" Text="Show Image"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
		<hr>
		<asp:Image id="imgResult" runat="server"></asp:Image>
	</body>
</HTML>
