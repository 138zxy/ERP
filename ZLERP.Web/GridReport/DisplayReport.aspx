﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayReport.aspx.cs" Inherits="DisplayReport" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title><%=Request.QueryString["report"]%>.grf</title>
    <script src="../Scripts/GridReport/CreateControl.js" type="text/javascript"></script>
    <style type="text/css">
        html,body {
            margin:0;
            height:100%;
        }
    </style>
</head>
<body style="margin:0">
	<script type="text/javascript"> 
	    var Report = "<%=Request.QueryString["report"]%>",
	        dataURL = "<%=Request.QueryString["data"]%>";
            if (Report != "")
	        Report = "grf/" + Report + ".grf";
            dataURL = "data/DataCenter.ashx?data=" + dataURL;
            //将其它额外的参数也组合到 dataURL
            <%
            int Count = Context.Request.QueryString.Count;
            for (int i = 0; i < Count; ++i)
            {
                string Key = Context.Request.QueryString.GetKey(i);
                if (Key != "data")
                {
            %>
                    dataURL += "&<%=Key%>=" + encodeURIComponent("<%=Context.Request.QueryString[i]%>");
            <%
                }
            }
            %>
          
         CreateDisplayViewerEx("100%", "100%", Report, dataURL, true, "");
        
	</script>
</body>
</html> 
