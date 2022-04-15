<%@ Page Title="车辆回厂信息表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
         if (!Page.IsPostBack)
         {
            //获取全局配置中的ChangeTime
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;

            this.ReportViewer1.LocalReport.Refresh();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        
        if (!string.IsNullOrEmpty(tbBeginDate.Text) && !string.IsNullOrEmpty(tbEndDate.Text) && string.Compare(tbBeginDate.Text, tbEndDate.Text) > 0)
        {
            lblMsg.Text = "日期范围错误";
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
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
             $('#<%=tbBeginDate.ClientID %>').datetimepicker();
             $('#<%=tbEndDate.ClientID %>').datetimepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    起始时间：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    只显示第一次打卡: <asp:CheckBox ID="chkOnlyFirst" runat="server" />
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="700px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"　 SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Car\R610229.rdlc" DisplayName="车辆回厂信息表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_carReturn"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="chkOnlyFirst" Name="OnlyFirst" PropertyName="Checked" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
