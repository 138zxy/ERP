<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocEdit.aspx.cs" Inherits="ZLERP.Web.DocEdit" %>
<%@ Import Namespace="System.Data.OleDb"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文档编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<meta http-equiv="X-UA-Compatible" content="IE=11;IE=9;IE=8;IE=7"/>--%>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
    <link href="../../Scripts/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"type="text/css" />

    <script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <link href="toastr/toastr.css" rel="stylesheet"type="text/css" />
    <script src="toastr/toastr.min.js" type="text/javascript"></script>
    <script type="text/javascript">
       
    </script>
</head>
<body style =" text-align :center ;" onbeforeunload="checkLeave()">
    <form  name="myform" action="#"  method="post">
    <%
        string url = this.Session["url"].ToString();
        string id = "", rid = "", docType = "", docTitle = "", fileurl = "", dataType = "", saveType = "";
        id = Request.QueryString["id"];//模板ID
        rid = Request.QueryString["rid"];//试验记录ID
        if (string.IsNullOrEmpty(id))//新建
        {
            docType = Request.Form["DocType"];
            //docType = "xls";
        }
        else
        {
            docTitle = Request.QueryString["docTitle"];
            docType = (Request.QueryString["docType"] == null || Request.QueryString["docType"].ToString()=="") ? "xls" : Request.QueryString["docType"];
            fileurl = Request.QueryString["fileurl"];
            dataType = Request.QueryString["dataType"];//打开文档 tempdate 模板 labreport 试验记录
            saveType = Request.QueryString["saveType"] == null ? "tempdate" : Request.QueryString["saveType"];//保存文档 tempdate 模板 labreport 试验记录
        }
        string editheight = Request.QueryString["height"] == null ? "750" : Request.QueryString["height"];
     %> 
     <script language ="javascript" type ="text/javascript" for="WebOffice1" event="NotifyCtrlReady">
        function checkLeave(){
        　　 event.returnValue="确定离开当前页面吗？";
             document.all.WebOffice1.Close();
        }
        WebOffice1_NotifyCtrlReady();       
     </script>
    <div class="container" class="text-left ">
            <div class="row" style="padding: 5px 0">
            </div>
            <div  class="row">
                <!--前缀-->
                <div class="form-group col-lg-3">
                    <div class="input-group">
                        <span class="input-group-addon">文档标题</span>
                        <input name="DocTitle" class="form-control" value= "" size="14" id="txtDocTitle" style="width:200px;"/>
                    </div>
                </div>
                <div class="form-group col-lg-2">
                    <div class="input-group">
                        <input type="button" class="btn btn-success" value="保存文档" onclick="return SaveToDb()"/> 
                    </div>
                </div>
                <div class="form-group col-lg-3" style="display:none;">
                    <div class="input-group">
                        <input id="DocContent" type="file" class="file" name ="DocFilePath" size="34"/>
                        
                    </div>
                </div>
               <%-- <div class="form-group col-lg-2" >
                    <div class="input-group">
                       <input type="button" class="btn btn-primary" value="打开本地文件" onclick="return docOpen()" id="Button1" style="width: 115px" />
                    </div>
                </div>--%>
                <div class="form-group col-lg-3">
                    <div class="input-group">
                        <input type ="button" class="btn btn-danger" value ="关闭窗体" onclick="quit()" />
                    </div>
                </div>
                
                <div class="form-group col-lg-3">
                    <div class="input-group">
                        <a href="../Content/Files/LabReport/在线编辑组件注册.zip" style="font-size:large;color:Red;">在线编辑组件下载</a>
                    </div>
                </div>

            </div>
            <%--<div class="row">
                <!--后缀-->
                <div class="form-group col-lg-3">
                    <div class="input-group">
                        <input class="form-control" type="text">
                        <span class="input-group-addon">查询</span>
                    </div>
                </div>
            </div>
            <div class="row">
                <!--下拉框-->
                <div class="form-group col-lg-3">
                    <div class="input-group">
                        <span class="input-group-addon">内容类型</span>
                        <select class="form-control">
                            <option>default</option>
                            <option>one</option>
                            <option>two</option>
                        </select>
                    </div>
                </div>
            </div>--%>
        </div>
   
    <div>

    <%--<script src="../../Scripts/WebOffice/LoadWebOffice.js" type="text/javascript"></script>--%>
    <%--加载WebOffice控件--%>
    <object  id="WebOffice1"  
            TYPE="application/x-itst-activex"  
            ALIGN="baseline" BORDER="0"  
            WIDTH="99%"HEIGHT='<%=editheight%>">'  
            clsid="{E77E049B-23FC-4DB8-B756-60529A35FAD5}" 
            codebase="WebOffice.cab#Version=7,0,1,8"  
            event_NotifyCtrlReady="NotifyCtrlReady"  
            event_NotifyToolBarClick="NotifyToolBarClick"  
            event_NotifyWordEvent="NotifyWordEvent">  
     </object> 
    </div>
    
    </form>
</body>

<script language="javascript" type="text/javascript">
    function window_onunload() {
	   document.all.WebOffice1.Close();
    }

    function window_onunload() {
        alert("你浏览器");
        document.all.WebOffice1.Close();
    }
    // -----------------------------== 修订文档 ==------------------------------------ //
    function ProtectRevision() {
        document.all.WebOffice1.SetTrackRevisions(1)
    }

    // -----------------------------== 隐藏修订 ==------------------------------------ //
    function UnShowRevisions() {
        document.all.WebOffice1.ShowRevisions(0);
    }
    // --------------------------== 显示当前修订 ==---------------------------------- //
    function ShowRevisions() {
        document.all.WebOffice1.ShowRevisions(1);
    }
    // -------------------------== 接受当前所有修订 ------------------------------ //
    function AcceptAllRevisions() {
        document.all.WebOffice1.SetTrackRevisions(4);
    }
    // 打开本地文件
    function docOpen() {
        var flag = document.all.WebOffice1.LoadOriginalFile("open", "<%=docType%>");
        if (0 == flag) {
            alert("文件打开失败，请检查路径是否正确");
            return false;
        }
    }
    // 保存
    function SaveToDb() {
        var returnValue;
        if (myform.DocTitle.value == "") {
            alert("文档标题不能为空");
            myform.DocTitle.focus();
            return false;
        }
        else {
            //document.all.WebOffice1.OptionFlag = 0x0080;
            document.all.WebOffice1.HttpInit();
            //添加相应的Post元素
	        <%if(id != ""){          %>
	          document.all.WebOffice1.SetTrackRevisions(0);
	          document.all.WebOffice1.ShowRevisions(0);
	        <%}%>	
            document.all.WebOffice1.HttpAddPostString("DocID", "<%=id %>");
            document.all.WebOffice1.HttpAddPostString("DocTitle", encodeURI(myform.DocTitle.value));
            document.all.WebOffice1.HttpAddPostString("DocType", "<%=docType %>");
            document.all.WebOffice1.HttpAddPostString("fileurl", "<%=fileurl %>");
            document.all.WebOffice1.HttpAddPostString("DataType", "<%=dataType %>");
            document.all.WebOffice1.HttpAddPostString("saveType", "<%=saveType %>");
            document.all.WebOffice1.HttpAddPostString("rid", "<%=rid %>");

            //把当前文档添加到Post元素列表中，文件的标识符䶿DocContent
            document.all.WebOffice1.HttpAddPostCurrFile("DocContent", "");
            //HttpPost执行上传的动仿WebOffice支持Http的直接上传，在upload.aspx的页面中,解析Post过去的数据
            //拆分出Post元素和文件数据，可以有选择性的保存到数据库中，或保存在服务器的文件中
	        //HttpPost的返回值，根据upload.aspx中的设置，返回upload.aspx中Response.Write回来的数据
            returnValue = document.all.WebOffice1.HttpPost("<%=url %>/upload.aspx");
            if ("succeed" == returnValue) {
                //alert("文件上传成功");
                toastr.success('文档保存成功！');
                //调父窗体
                var myGrid = window.opener.myJqGrid;
                myGrid.refreshGrid('1=1');              
                
            } else {
                toastr.error('文档保存失败！');
            }
        }
    }
    //关闭
    function quit() {
        document.all.WebOffice1.Close();
        //window.location.href = "WebForm1.aspx";
        window.close();
    }      
    </script>
    <SCRIPT ID=clientEventHandlersJS LANGUAGE=javascript>
        //WebOffice加载页面
        //初始化控件 可以初始化加载文档等和设置weboffice的相关属性  
        function NotifyCtrlReady() {  
                 WebOffice1_NotifyCtrlReady();
        }  
        //监听工具栏的事件  
        function NotifyToolBarClick(id) {  
                    //eventinfo.innerText = "NotifyToolBarClick 事件发生，工具栏ID：" + id;  
        }  
        //监听word操作事件  
        function NotifyWordEvent(name) {  
                    //eventinfo.innerText = "NotifyWordEvent 事件发生,Word事件名称：" + name;  
        }  
        
        function WebOffice1_NotifyCtrlReady() {
            myform.DocTitle.value = "<%=docTitle %>";
            //document.all.WebOffice1.Close();
            //LoadOriginalFile接口装载文件,

            //如果是编辑已有文件，则文件路径传给LoadOriginalFile的第一个参数
            if (('<%=id %>' == null || '<%=id %>' == "" || '<%=fileurl %>' == null || '<%=fileurl %>' == '')&&'<%=saveType %>' == 'tempdate') {
                
                document.all.WebOffice1.LoadOriginalFile("", "<%=docType %>");
            }
            else {
                document.all.WebOffice1.LoadOriginalFile("<%=url %>/GetDoc.aspx?id=<%=id%>&dataType=<%=dataType%>&rid=<%=rid%>&saveType=<%=saveType%>", "<%=docType %>");
                //document.all.WebOffice1.LoadOriginalFile("<%=url %>/GetDoc.aspx?fileUrl=<%=fileurl%>", "<%=docType%>");
                document.all.WebOffice1.SetTrackRevisions(1); //开始修订文档
                document.all.WebOffice1.ShowRevisions(1); //显示修订
            }
            //屏蔽标准工具栏的前几个按钮
             document.all.WebOffice1.SetToolBarButton2("Standard",1,1);
             document.all.WebOffice1.SetToolBarButton2("Standard",2,1);
             document.all.WebOffice1.SetToolBarButton2("Standard",3,1);
             document.all.WebOffice1.SetToolBarButton2("Standard",6,1);   

//            //屏蔽文件菜单项
//            document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 1);
//            //屏蔽 保存快捷键(Ctrl+S) 
//            document.all.WebOffice1.SetKeyCtrl(595, -1, 0);
//            //屏蔽 打印快捷键(Ctrl+P) 
//            document.all.WebOffice1.SetKeyCtrl(592, -1, 0);

            <%if (docType   == "doc") {%>
                //屏蔽文件菜单项
                document.all.WebOffice1.SetToolBarButton2("Menu Bar",1,1);
                //屏蔽 保存快捷键(Ctrl+S) 
                document.all.WebOffice1.SetKeyCtrl(595,-1,0);
                //屏蔽 打印快捷键(Ctrl+P) 
                document.all.WebOffice1.SetKeyCtrl(592,-1,0);
            <%}else if(docType   == "xls") {%>
                //屏蔽文件菜单项
                document.all.WebOffice1.SetToolBarButton2("Worksheet Menu Bar",1,1);         
            <%} %>
        }
     </SCRIPT>

    <SCRIPT LANGUAGE=javascript FOR=WebOffice1 EVENT=NotifyCtrlReady>
      <!--
      //WebOffice1_NotifyCtrlReady() // 在装载完Weboffice(执行<object>...</object>)控件后自动执行WebOffice1_NotifyCtrlReady方法
      //NotifyCtrlReady();
      //-->
    </SCRIPT>

</html>