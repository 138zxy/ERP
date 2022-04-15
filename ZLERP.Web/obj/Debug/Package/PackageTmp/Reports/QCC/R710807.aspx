﻿<%@ Page Title="碱含量计算书" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $.datepicker.setDefaults({ dateFormat: 'yy-mm-dd' });
            $('#<%=tbBeginDate.ClientID %>').datepicker();
            $('#<%=tbEndDate.ClientID %>').datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    起始日期：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止日期：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\QCC\R710807.rdlc" DisplayName="强度评定台账">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_Assess"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="DateFrom" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="DateTo" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
