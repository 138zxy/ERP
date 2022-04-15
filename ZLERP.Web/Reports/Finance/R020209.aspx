<%@ Page Title="销售对账单" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            
        }
        else
        {            
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            
            this.ReportViewer1.LocalReport.Refresh();
        }
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;

        rpt.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text.Trim()));
        rpt.SetParameters(new ReportParameter("EndDate", tbEndDate.Text.Trim()));
    }


</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            var Date_Length = 10; //2013-01-01

            if ($('#<%=tbBeginDate.ClientID %>').val().length <= Date_Length) {
                $('#<%=tbBeginDate.ClientID %>').datepicker();
            }
            else {
                $('#<%=tbBeginDate.ClientID %>').datetimepicker();
            }

            if ($('#<%=tbEndDate.ClientID %>').val().length <= Date_Length) {
                $('#<%=tbEndDate.ClientID %>').datepicker();
            }
            else {
                $('#<%=tbEndDate.ClientID %>').datetimepicker();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    起始日期：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止日期：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    商品名称:  <asp:TextBox ID="tbGoodsName" runat="server"></asp:TextBox>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Finance\R020209.rdlc" DisplayName="供应链进出库汇总表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_SC_GoodsInANDOut"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="DateFrom" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="DateTo" PropertyName="Text" />
              <asp:ControlParameter ControlID="tbGoodsName" Name="GoodsName" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>