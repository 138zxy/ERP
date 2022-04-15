﻿<%@ Page Title="销售统计明细" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //获取全局配置中的ChangeTime
            //获取全局配置中的ChangeTime 
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
  
   
            
        }
        this.ReportViewer1.LocalReport.DisplayName = string.Format("{0}({1}-{2})", this.Title, tbBeginDate.Text,tbEndDate.Text);
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));

        this.ReportViewer1.LocalReport.Refresh();
    }
    
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //$('#<%=tbBeginDate.ClientID %>').datetimepicker({ hour: 8 });
            //$('#<%=tbEndDate.ClientID %>').datetimepicker({ hour: 8 });

            var dates = $("#<%=tbBeginDate.ClientID %>, #<%=tbEndDate.ClientID %>").datetimepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                onSelect: function (selectedDate) {
                    var option = this.id == "<%=tbBeginDate.ClientID%>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">           
        </asp:ScriptManager>
     起始时间：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    合同名称：<asp:TextBox ID="tbContractName" runat="server"></asp:TextBox>
    客户名称：<asp:TextBox ID="tbCustName" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" runat="server"></asp:TextBox>
    产品类型：<asp:DropDownList ID="ddlProduct" runat="server" Width="150px">
                <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="混凝土" Value="0"></asp:ListItem>
                <asp:ListItem Text="湿拌" Value="1"></asp:ListItem>
              </asp:DropDownList>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Sales\R310427.rdlc"  DisplayName="销售统计明细">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_produce_sales_Detail"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                <asp:ControlParameter ControlID="tbBeginDate" Name="StartDateTime" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbEndDate" Name="EndDateTime" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbContractName" Name="ContractName" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbCustName" Name="CustName" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbConStrength" Name="ConStrength" PropertyName="Text" />
            <asp:ControlParameter ControlID="ddlProduct" Name="Product" PropertyName="Text" />
           </SelectParameters>
       </asp:SqlDataSource> 
 
</asp:Content>