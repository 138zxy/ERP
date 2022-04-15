<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignReport.aspx.cs" Inherits="ZLERP.Web.GridReport.DesignReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title><%=Request.QueryString["report"]%>.grf</title>
    <script src="../Scripts/GridReport/CreateControl.js" type="text/javascript"></script>
    	<script type="text/javascript">
    	    function OnSaveReport() {
//    	        alert("为了保护报表的正常演示，这里放弃了对报表设计的保存！");
    	        //ReportDesigner.DefaultAction = false;
    	    }
	</script>
    <style type="text/css">
        html,body {
            margin:0;
            height:100%;
        }
    </style>
</head>
<body style="margin:0">
	<script type="text/javascript">  
	        var Report = "<%=Request.QueryString["report"]%>"; 

            var ReportName=Report;
            var num=Math.random()*100;
            if (Report != "")
	        Report = "grf/" + Report + ".grf?"+num;
	         CreateDesignerEx("100%", "100%", Report, "data/DesignReportSave.ashx?report="+ReportName+"", "", 
            "<param name='OnSaveReport' value='OnSaveReport'>"); 

 
	</script>
</body>
</html>