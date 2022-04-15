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
            this.tb_InMonth.Text = DateTime.Now.ToString("yyyy-MM");


            this.tb_CastMode.DataSource = this.SqlDataSource2;
            this.tb_CastMode.DataTextField = "DicName";
            this.tb_CastMode.DataValueField = "DicName";
            this.tb_CastMode.DataBind();


       
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
 
        subDataSource.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault()); 
        rpt.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource"));
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
 纳入月份：<asp:TextBox ID="tb_InMonth"  runat="server"></asp:TextBox>  
 浇筑方式： <asp:DropDownList ID="tb_CastMode" runat="server" Width="150px" >
    </asp:DropDownList>
 
 <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"  OnDrillthrough="ReportViewer1_Drillthrough">
        <LocalReport ReportPath="Reports\Finance\R020102.rdlc"  DisplayName="泵送费结算单">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_PoBon_stats"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
          <SelectParameters>
            <asp:ControlParameter ControlID="tb_InMonth" Name="InMonth" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tb_CastMode" Name="CastMode" PropertyName="Text" />   
          </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="subDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_PoBon_Detail"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    
    </asp:SqlDataSource>

      <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="select '' as  DicName  union   SELECT DicName FROM dbo.Dic WHERE  ParentID='CastMode' ORDER BY DicName "  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource>

   
</asp:Content>
