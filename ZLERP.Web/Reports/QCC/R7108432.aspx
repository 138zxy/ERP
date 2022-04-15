﻿<%@ Page Title="混凝土开盘鉴定记录" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
        }
        else
        {
            if (Request["uid"] != null)
            {
                LocalReport rpt = (LocalReport)this.ReportViewer1.LocalReport;
                this.tbID.Text = Request["uid"].ToString();
                rpt.SetParameters(new ReportParameter("OP_ID", this.tbID.Text));
                //设置EnterpriseName参数为全局配置中的EnterpriseName
                var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
                if (entCfg != null)
                {
                    rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
                }
                this.ReportViewer1.LocalReport.Refresh();
            }
        }
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbID.Text.Length > 0)
        {
            this.ReportViewer1.LocalReport.Refresh();
            LocalReport rpt = this.ReportViewer1.LocalReport;
            rpt.SetParameters(new ReportParameter("OP_ID", this.tbID.Text));
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (entCfg != null)
            {
                rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
            }
          
        }
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
        发货单号
        <asp:TextBox ID="tbID" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查询" 
            Width="57px" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\QCC\R7108432.rdlc"  DisplayName="混凝土开盘鉴定记录">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_tasksfirsttime"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="True"  >
           <SelectParameters> 
                 <asp:ControlParameter ControlID="tbID" Name="taskid" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>       
</asp:Content>