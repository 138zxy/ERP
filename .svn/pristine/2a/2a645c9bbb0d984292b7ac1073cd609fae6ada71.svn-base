<%@ Page Title="原材料供应商对账单" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }
            this.tbSupplyID.DataSource = this.SqlDataSource2;
            this.tbSupplyID.DataTextField = "SupplyName";
            this.tbSupplyID.DataValueField = "SupplyID";
            this.tbSupplyID.DataBind();
            ListItem li = new ListItem();
            li.Value = string.Empty;
            li.Text = string.Empty;
            this.tbSupplyID.Items.Insert(0, li);
            
            //this.tbSupplyID.Text = Request["uname"].ToString();
            if (Request["uid"] != null)
            {
                //this.tbSupplyID.Text = Request["uname"].ToString();
                this.tbSupplyID.SelectedValue = Request["uid"].ToString();
            }
            
            tbBeginDate.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));
            this.ReportViewer1.LocalReport.DisplayName = DateTime.Today.ToString("yyyy年MM月dd日") + this.Title;
        }
        
        //this.ReportViewer1.LocalReport.Refresh();
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
    供货商：<asp:DropDownList ID="tbSupplyID" runat="server" Width="150px" >
    </asp:DropDownList>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt" PageCountMode="Actual">
        <LocalReport ReportPath="Reports\Finance\R020234.rdlc" DisplayName="原材料供应商对账单">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
<%--    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="600px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" AsyncRendering="False" PageCountMode="Actual">
        <LocalReport ReportPath="Reports\Finance\R020234.rdlc"  DisplayName="原材料供应商对账单">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
    </rsweb:ReportViewer> --%>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_SupplyAccount"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="StartDateTime" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDateTime" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbSupplyID" Name="SupplyID" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="select SupplyID,SupplyName  from SupplyInfo  where IsUsed=1  order by SupplyName "  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
    </asp:SqlDataSource>
</asp:Content>
