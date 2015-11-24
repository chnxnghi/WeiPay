<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notify.aspx.cs" Inherits="MyWxPay.MyNotify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>支付结果通知</title>
</head>
<body>
    <div style="margin-left: 2%;">您的注册码是<span style="color:#FF0000;font-size:30px;font-weight: bolder;"><%= message %></span>，请把此号码输入到您的电子书软件内，
如有问题，请联系恒智天成官方电话4006338987!</div>
    <div  style="margin-left: 2%;color:#FF0000;font-weight: bolder;">注意：请务必保存好您的注册码。</div>
</body>
</html>
