<%@ Page Title="租用车运费结算表" Language="C#"   MasterPageFile="~/Reports/ReportsBase.Master"
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
        }
        else
        { 
            //tbDate.Text = DateTime.Today.ToString("yyyyMM");
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月");
            this.ReportViewer1.LocalReport.Refresh();
            this.tbCar.DataSource = this.SqlDataSource2;
            this.tbCar.DataTextField = "CarID";
            this.tbCar.DataValueField = "CarID";
            this.tbCar.DataBind();

            tbBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd")+" 00:00:00";
            tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("startdate", tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("enddate", tbEndDate.Text));
        if (this.tbCar.Text == "")
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("CarID", " "));
        }
        else
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("CarID", this.tbCar.Text));
        }
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
        车号：<asp:DropDownList ID="tbCar" runat="server"> </asp:DropDownList>
   
    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="800px" runat="server" 
            Font-Names="Verdana"   AsyncRendering="False"  Font-Size="9pt" 
            InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Car\R610221.rdlc"  DisplayName="租用车运费结算表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
    </rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           SelectCommand="sp_rpt_RentCarFreight"  SelectCommandType="StoredProcedure" 
            CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <%--<asp:ControlParameter ControlID="tbDate" Name="Month" PropertyName="Text" /> --%>
                <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbCar" Name="CarID" PropertyName="Text" /> 

           </SelectParameters>
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
            SelectCommand="select '' CarID  UNION select CarID from Car where BelongTo='租用'">
       </asp:SqlDataSource>

</asp:Content>
