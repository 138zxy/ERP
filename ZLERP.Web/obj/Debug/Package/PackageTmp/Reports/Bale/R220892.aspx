﻿<%@ Page Title="原材料进销存" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
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

             
            this.DropDownList2.DataSource = this.SqlDataSource3;
            this.DropDownList2.DataTextField = "ProductLineName";
            this.DropDownList2.DataValueField = "ProductLineID";
            this.DropDownList2.DataBind();
            ListItem li = new ListItem();
            li.Value = string.Empty;
            li.Text = string.Empty;
            this.DropDownList2.Items.Insert(0, li);

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

        //rpt.SetParameters(new ReportParameter("StuffID", this.DropDownList2.SelectedValue));

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
    生产线：<asp:DropDownList ID="DropDownList2" runat="server" Width="150px" >
    </asp:DropDownList>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="700px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"　 SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Bale\R220892.rdlc" DisplayName="原材料进销存">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_SiloInAndOut"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="DropDownList2" Name="ProductLineID" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="SELECT ProductLineID, ProductLineName FROM dbo.ProductLine WHERE IsUsed = 1"  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
    </asp:SqlDataSource>

</asp:Content>
