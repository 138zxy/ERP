function SC_GoodsReportIndexInit(opts) {
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
        sortByField: 'Goodsid,ReportDate',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30000,
        showPageBar: false,
        sortOrder: 'ASC',
        initArray: [ 
		{
		    label: '日期',
		    name: 'ReportDate',
		    index: 'ReportDate',
		    formatter: 'date',
		    width: 80
		}, 
		{
		    label: '单号',
		    name: 'OrderNo',
		    index: 'OrderNo',
		    width: 120
		},
		{
		    label: '摘要',
		    name: 'OrderType',
		    index: 'OrderType',
		    width: 80
		},
        {
            label: '品名',
		    name: 'GoodsName',
		    index: 'GoodsName',
		    width: 80
		},
		{
		    label: '规格',
		    name: 'Spec',
		    index: 'Spec',
		    width: 80
		},
		{
		    label: '条码',
		    name: 'GoodsNo',
		    index: 'GoodsNo',
		    width: 80
		},
		{
		    label: '品牌',
		    name: 'Brand',
		    index: 'Brand',
		    width: 80
		},
		{
		    label: '入库数量',
		    name: 'InQuantity',
		    index: 'InQuantity',
		    width: 60
		},
		{
		    label: '入库单价',
		    name: 'InPriceUnit',
		    index: 'InPriceUnit',
		    width: 60
		},
		{
		    label: '入库金额',
		    name: 'InMoney',
		    index: 'InMoney',
		    width: 60
		},
		{
		    label: '出库数量',
		    name: 'OutQuantity',
		    index: 'OutQuantity',
		    width: 60
		},
		{
		    label: '出库单价',
		    name: 'OutPriceUnit',
		    index: 'OutPriceUnit',
		    width: 60
		},
		{
		    label: '出库金额',
		    name: 'OutMoney',
		    index: 'OutMoney',
		    width: 60
		},
		{
		    label: '剩余数量',
		    name: 'RemainderQuantity',
		    index: 'RemainderQuantity',
		    width: 60
		},
		{
		    label: '剩余金额',
		    name: 'RemainderMoney',
		    index: 'RemainderMoney',
		    width: 60
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
    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            myJqGrid.getJqGrid().setCell(cl, "RemainderQuantity", '', { color: 'red' }, ''); 
            myJqGrid.getJqGrid().setCell(cl, "RemainderMoney", '', { color: 'red' }, ''); 
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
        EndDate = addDate(EndDate, 1);
        if (BeginDate != "") {
            condition += " and ReportDate>'" + BeginDate+"'";
        }
        if (EndDate != "") {
            condition += " and ReportDate<='" + EndDate + "'";
        }
        return condition;
    }
    function addDate(date, days) {
        if (days == undefined || days == '') {
            days = 1;
        }
        var date = new Date(date);
        date.setDate(date.getDate() + days);
        var month = date.getMonth() + 1;
        var day = date.getDate();
        return date.getFullYear() + '-' + getFormatDate(month) + '-' + getFormatDate(day);
    }
    function getFormatDate(arg) {
        if (arg == undefined || arg == '') {
            return '';
        }

        var re = arg + '';
        if (re.length < 2) {
            re = '0' + re;
        }

        return re;
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