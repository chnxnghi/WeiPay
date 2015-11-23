<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Send.aspx.cs" Inherits="WeiPayWeb.Send" %>

<!DOCTYPE html">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>恒智天成注册码支付</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    </head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left:2%;color:#f00">请输入您的8位数字机器码：</div><br/>
        <div style="margin-left:2%;">输入机器码：</div><br/>
        <asp:TextBox ID="txtMCode" runat="server" style="width:96%;height:35px;margin-left:2%;" /><br /><br />
        <div style="margin-left:2%;">再次输入机器码：</div><br/>
        <asp:TextBox ID="txtSure" runat="server" style="width:96%;height:35px;margin-left:2%;" /><br /><br />
        <br/>
	    <div align="center">
            <asp:Button ID="BtnSave" runat="server" Text="下一步" style="width:210px; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;" OnClick="BtnSave_Click" />
	    </div>
        <br/><br/><br/>
    </form>
</body>
</html>
