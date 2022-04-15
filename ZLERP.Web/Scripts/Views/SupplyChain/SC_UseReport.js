function SC_UseReportIndexInit(opts) {
    $('#GoodgType').height(gGridHeight + 80);
    var condition = "";
    var treeCondtion = "";
    var nodeid = 0;
    var nodeName = "";
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' ,
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [ 
		{
		    label: '品名',
		    name: 'GoodsName',
		    index: 'GoodsName',
		    width: 100
		}, 
		{
		    label: '规格',
		    name: 'Spec',
		    index: 'Spec',
		    width: 100
		},
		{
		    label: '单位',
		    name: 'Unit',
		    index: 'Unit',
		    width: 100
		},
        {
            label: '使用人',
            name: 'UserID',
            index: 'UserID',
            width: 100
        },
		{
		    label: '出库数量',
		    name: 'OutNum',
		    index: 'OutNum',
		    width: 100
		},
		{
		    label: '出库金额',
		    name: 'OutMoney',
		    index: 'OutMoney',
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

    var treeSettings = {
        check: {
            enable: false
        },
        view: {
            selectedMulti: false
        },
        data: {
            simpleData: {
                enable: true
            },
            key: {
                title: 'title'
            }
        },
        async: {
            enable: true,
            url: opts.scTreeUrl,
            autoParam: ["id", "name", "level"]
        },
        callback: {
            onAsyncError: function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                handleServerError(XMLHttpRequest, textStatus, errorThrown);
            },
            onAsyncSuccess: zTreeOnAsyncSuccess,
            onClick: zTreeOnCheck
        }
    };
    var scLimitList = $.fn.zTree.init($('#GoodgType'), treeSettings);
    scLimitList.expandAll(true);

    function zTreeOnAsyncSuccess(event, treeId, treeNode, msg) {
        var treeObj = $.fn.zTree.getZTreeObj(treeId);
        var nodes = treeObj.getNodes();
        if (nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                treeObj.expandNode(nodes[i], true, true, true);
            }
        }
    }
    function zTreeOnCheck() {
        var condition = GetSearch();
        myJqGrid.refreshGrid(condition);
    }

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
        var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
        var node = treeObj.getSelectedNodes();
        if (node.length > 0) {
            var id = node[0].id;
            var name = node[0].name
            var typestring = node[0].typeNo;
            condition += " and  typestring like '" + typestring + "%'";
        }
        var GoodsName = $("#GoodsName").val();
        if (GoodsName != "") {
            condition += " and GoodsName like '%" + GoodsName + "%'"
        } 
        var BeginDate = $("#BeginDate").val();
        var EndDate = $("#EndDate").val();
        condition += "&" + BeginDate;
        condition += "&" + EndDate;
        return condition;
    }

    //获得本月的开始日期 
    var now = new Date(); //当前日期  
    var nowDayOfWeek = now.getDay(); //今天本周的第几天 
    var nowDay = now.getDate(); //当前日 
    var nowMonth = now.getMonth(); //当前月  
    var nowYear = now.getYear(); //当前年 
    nowYear += (nowYear < 2000) ? 1900 : 0; //

    var lastMonth = nowMonth;
    var lastYear = nowYear;
    if (lastMonth == 0) {
        lastMonth = 11;
        lastYear = lastYear - 1;
    }
    else {
        lastMonth = lastMonth - 1;
    }

    //格式化日期：yyyy-MM-dd 
    function formatDate(date) {
        var myyear = date.getFullYear();
        var mymonth = date.getMonth() + 1;
        var myweekday = date.getDate();

        if (mymonth < 10) {
            mymonth = "0" + mymonth;
        }
        if (myweekday < 10) {
            myweekday = "0" + myweekday;
        }
        return (myyear + "-" + mymonth + "-" + myweekday);
    }
    //获得某月的天数 
    function getMonthDays(myYear, myMonth) {
        var monthStartDate = new Date(myYear, myMonth, 1);
        var monthEndDate = new Date(myYear, myMonth + 1, 1);
        var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
        return days;
    }

    function getMonthStartDate() {
        var monthStartDate = new Date(nowYear, nowMonth, 1);
        return formatDate(monthStartDate);
    }

    //获得本月的结束日期 
    function getMonthEndDate() {
        //  var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowYear, nowMonth));
        return formatDate(now);
    }

    var ThisMonthstart = getMonthStartDate();
    var ThisMonthend = getMonthEndDate();

    function SetSearchDate() {
        $("#BeginDate").val(ThisMonthstart);
        $("#EndDate").val(ThisMonthend);
    }  
    SetSearchDate();
}