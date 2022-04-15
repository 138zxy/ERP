<%@ Page Title="原料进货汇总表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();

            refresh();
            //this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");

            this.tbBeginDate.Text = DateTime.Now.ToString("yyyy-MM");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM");
        }
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        
        if (!string.IsNullOrEmpty(tbBeginDate.Text) && !string.IsNullOrEmpty(tbEndDate.Text) && string.Compare(tbBeginDate.Text, tbEndDate.Text) > 0)
        {
            lblMsg.Text = "月份范围错误";
            this.ReportViewer1.Visible = false;
            return;
        }
        else
        {
            lblMsg.Text = "";
            this.ReportViewer1.Visible = true;
        } 
    
        LocalReport rpt = this.ReportViewer1.LocalReport;
        rpt.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
        rpt.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }

    public void refresh()
    {
        string CurrentYear = Session["YearAccount"] == null ? "ZLERP" : Session["YearAccount"].ToString();

        this.SqlDataSource1.ConnectionString = this.SqlDataSource1.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.ProviderName = this.SqlDataSource1.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.DataBind();
    }
    protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
    {
        LocalReport rpt = (LocalReport)e.Report;
      
        rpt.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource"));

    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    起始月份：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止月份：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"　OnDrillthrough="ReportViewer1_Drillthrough">
        <LocalReport ReportPath="Reports\QCC\R710846.rdlc" DisplayName="配合比标准差异对比表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_FormulaDiff"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="beginMonth" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="endMonth" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
     
</asp:Content>
