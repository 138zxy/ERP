<%@ Page Title="原材料货款结算单" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
           
        }
        else
        {
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            this.tb_Startdate.Text = DateTime.Now.ToString("yyyy-MM-01");
            this.tb_Enddate.Text = DateTime.Now.ToString("yyyy-MM-dd");  
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;

 
    }

    protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
    {
        LocalReport rpt = (LocalReport)e.Report;
 
      
    }

</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
      
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 开始时间：<asp:TextBox ID="tb_Startdate"  runat="server"></asp:TextBox>  
 结束时间：<asp:TextBox ID="tb_Enddate"  runat="server"></asp:TextBox> 
 商品名称：<asp:TextBox ID="tb_GoodsName"  runat="server"></asp:TextBox>   
 
 <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"  OnDrillthrough="ReportViewer1_Drillthrough">
        <LocalReport ReportPath="Reports\Finance\R020111.rdlc"  DisplayName="供应链出库明细">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Jxc_OutDetial"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
          <SelectParameters>
            <asp:ControlParameter ControlID="tb_Startdate" Name="Startdate" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tb_Enddate" Name="Enddate" PropertyName="Text" />   
            <asp:ControlParameter ControlID="tb_GoodsName" Name="GoodsName" PropertyName="Text" />   
          </SelectParameters>
       </asp:SqlDataSource>
   
</asp:Content>
