<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notify.aspx.cs" Inherits="WeiPayWeb.Notify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>支付结果通知</title>
</head>
<body>
    <div style="margin-left: 2%; color: #f00">您的注册码是<span style="color:#00CD00;font-size:30px;font-weight: bolder;"><%= message %></span>，请妥善保存！</div>
    <div style="margin-left: 10px;color:#38810A;font-weight: bolder;">小提示：您即将收到的【微信支付凭证】中的【商户单号】，是以【机器码-注册码-付款日期】的格式储存，例如：12345678-26156030-20151212</div>
</body>
</html>
