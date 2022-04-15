function HR_GZ_TimingAnalysisIndexInit(opts) {
   
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' ,
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl,
        sortByField: 'Name',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'Name',
        setGridPageSize: 30,
        showPageBar: true, 
        initArray: [ 
		{
		    label: '项目编码',
		    name: 'Code',
		    index: 'Code',
		    width: 100
		}, 
		{
		    label: '项目名称',
		    name: 'Name',
		    index: 'Name',
		    width: 100
		},
		{
		    label: '总小时',
		    name: 'Quantity',
		    index: 'Quantity',
		    width: 100
		},
        {
            label: '金额',
		    name: 'AllMoney',
		    index: 'AllMoney',
		    width: 100
		},
		{
		    label: '占比(%)',
		    name: 'Proportion',
		    index: 'Proportion',
		    width: 100
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            } 
        }
        
    });
     window.Search = function () {
        var BeginDate = $("#BeginDate").val();
        var EndDate = $("#EndDate").val();
        var GoodsName = $("#GoodsName").val();
        if (BeginDate == "") {
            showMessage('提示', '请输入查询开始时间！');
            return;
        }
        if (EndDate == "") {
            showMessage('提示', '请输入查询结束时间！');
            return;
        } 
        var condition = GetSearch(); 
        myJqGrid.refreshGrid(condition);
    }

    function GetSearch() {
        var condition = "1=1"; 
        var BeginDate = $("#BeginDate").val();
        var EndDate = $("#EndDate").val();
        condition += "&" + BeginDate;
        condition += "&" + EndDate;
        return condition;
    }
 
    function SetSearchDate() {
        var date = new Date();
        var fdate = date.getFullYear() + '-' + (date.getMonth()) + '-' + date.getDate();
        var edate = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
        $("#BeginDate").val(fdate);
        $("#EndDate").val(edate); 
    }
    SetSearchDate();
 
}