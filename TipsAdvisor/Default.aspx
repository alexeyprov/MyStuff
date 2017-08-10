<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <a href="Default.aspx">preved</a>
    <form id="form1" runat="server">
    <div style="font-family: Calibri; font-size: x-large; font-weight: bold; color: #008000">
    <p>
    </div>
    
    <table class="style1">
		<tr>
			<td style="background-color: #00FFFF">
				<asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
			</td>
			<td>
				<asp:Button ID="btnGetTime" runat="server" Text="Get Time!" 
					onclick="btnGetTime_Click" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				&nbsp;</td>
		</tr>
	</table>
    
    </form>
</body>
</html>
