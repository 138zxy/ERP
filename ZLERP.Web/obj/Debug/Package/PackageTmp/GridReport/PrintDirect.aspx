<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDirect.aspx.cs" Inherits="ZLERP.Web.GridReport.PrintDirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title><%=Request.QueryString["report"]%>.grf</title>
    <script src="../Scripts/GridReport/CreateControl.js" type="text/javascript"></script>
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
                if ( Key != "data")
                //if (Key != "report" && Key != "data")
                {
            %>
                    dataURL += "&<%=Key%>=" + encodeURIComponent("<%=Context.Request.QueryString[i]%>");
            <%
                }
            }
            %>
        
        //打印
        function btnPrint_onclick() {
            CreateReport("ReportGrid");
            ReportGrid.LoadFromURL(Report);
            ReportGrid.LoadDataFromURL(dataURL);
            ReportGrid.Print(false); 
            this.close();          
        } 

	</script>
    <style type="text/css">
        html,body {
            margin:0;
            height:100%;
        }
    </style>
</head>
<body style="margin:0" >
<script type="text/javascript">
    btnPrint_onclick();
</script>
</body>
</html>
