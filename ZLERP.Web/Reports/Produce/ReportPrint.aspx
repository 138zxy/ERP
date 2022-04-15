<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPrint.aspx.cs" Inherits="ZLERP.Web.Reports.Produce.ReportPrint" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .frbody{
            overflow:hidden !important;
        }
    </style>
    <script type="text/javascript">
        function GetRequest() {
            var url = location.search; //获取url中"?"符后的字串 
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
                }
            }
            return theRequest;
        };
        function hello() {
            var input = document.getElementsByName("print_browser")[0];
            var Request = new Object();
            Request = GetRequest();
            console.log(Request);
            var isclose = Request['isclose'];
            if (isclose=="true") {
                input.click();
                window.close();
            }
            //setTimeout("window.opener=null;window.close()", 1000);
        };
        //使用方法名字执行方法 setTimeout只执行一次
        var t1 = window.setTimeout(hello, 1000);
        var t2 = window.setTimeout("hello()", 1000);//使用字符串执行方法 
        window.clearTimeout(t1);//去掉定时器 
</script> 
</head>
<body>
    
    <form id="form1" runat="server">   
    <div style=" height:400px; margin-left:50px;" align="center">
        
    <cc1:WebReport ID="webReport" runat="server" OnStartReport="WebReport_StartReport" 
                        Width="100%" Height="600px" />

    </div>
    </form>   

</body>
</html>
