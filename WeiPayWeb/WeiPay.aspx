<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiPay.aspx.cs" Inherits="WeiPayWeb.WeiPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>注册码支付确认</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script src="JS/lazyloadv3.js" type="text/javascript"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
</head>
    <script type="text/javascript">
        //调用微信JS api 支付
        function jsApiCall()
        {
            WeixinJSBridge.invoke(
            'getBrandWCPayRequest',
            <%=wxJsApiParam%>,//josn串
                    function (res)
                    {
                        if (res.err_msg == "get_brand_wcpay_request:ok")
                        {
                            //window.location.href = "<%= WeiPay.PayConfig.NotifyUrl + "?mcode=" + attach %>";
                            //WeixinJSBridge.log(res.err_msg);
                            alert(res.err_code + res.err_desc + res.err_msg);
                        }
                        
                    }
                    );
            
               }

               function callpay()
               {
                   if (typeof WeixinJSBridge == "undefined")
                   {
                       if (document.addEventListener)
                       {
                           document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                       }
                       else if (document.attachEvent)
                       {
                           document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                           document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                       }
                   }
                   else
                   {
                       jsApiCall();
                   }
               }
</script>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 2%; color: #f00">您的机器码是：<%= attach %></div>
        <br />
        <div>
            <asp:Label ID="Label1" runat="server" Style="color: #00CD00;"><b>恒智天成产品注册码：价格为<span style="color:#f00;font-size:50px">10元</span>钱</b></asp:Label><br />
        </div>
        <div align="center">
            <asp:Button ID="BtnSubmit" runat="server" Text="立即购买" Style="width: 210px; height: 50px; border-radius: 15px; background-color: #00CD00; border: 0px #FE6714 solid; cursor: pointer; color: white; font-size: 16px;" OnClientClick="callpay()" />
        </div>
    </form>
</body>
</html>

