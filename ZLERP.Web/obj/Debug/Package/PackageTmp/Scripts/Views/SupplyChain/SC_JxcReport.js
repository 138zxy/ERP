function SC_JxcReportIndexInit(opts) {
    $('#GoodgType').height(gGridHeight + 80);  
    var MyJqGrid1 = new MyGrid({
        renderTo: 'MyJqGrid1',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false, 
        multiselect: false,
        initArray: [
        {
            label: '月份',
            name: 'MonthLy',
            index: 'MonthLy',
            width: 80
        }, 
		{
		    label: '入库数量',
		    name: 'InQuantity',
		    index: 'InQuantity',
		    width: 80
		}, 
		{
		    label: '出库数量',
		    name: 'OutQuantity',
		    index: 'OutQuantity',
		    width: 80
		},
		{
		    label: '库存数量',
		    name: 'LibQuantity',
		    index: 'LibQuantity',
		    width: 80
		},
        {
            label: '入库金额',
            name: 'InMoney',
            index: 'InMoney',
            width: 80
		},
		{
		    label: '出库金额',
		    name: 'OutMoney',
		    index: 'OutMoney',
		    width: 80
		},
		{
		    label: '库存金额',
		    name: 'LibMoney',
		    index: 'LibMoney',
		    width: 80
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                MyJqGrid1.reloadGrid();
            },
            handleRefresh: function (btn) {
                MyJqGrid1.refreshGrid();
            } 
        }
    });


    var MyJqGrid2 = new MyGrid({
        renderTo: 'MyJqGrid2',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl2,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        multiselect: false,
        initArray: [
        {
            label: '分类',
            name: 'TypeName',
            index: 'TypeName',
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
		    label: '单位',
		    name: 'Unit',
		    index: 'Unit',
		    width: 60
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
		    label: '期初数量',
		    name: 'IniQuantity',
		    index: 'IniQuantity',
		    width: 80
		},
		{
		    label: '期初金额',
		    name: 'IniMoney',
		    index: 'IniMoney',
		    width: 80
		},
		{
		    label: '本期购进数量',
		    name: 'InQuantity',
		    index: 'InQuantity',
		    width: 100
		},
		{
		    label: '本期购进金额',
		    name: 'InMoney',
		    index: 'InMoney',
		    width: 80
		},
		{
		    label: '本期消耗数量',
		    name: 'OutQuantity',
		    index: 'OutQuantity',
		    width: 80
		},
		{
		    label: '本期消耗金额',
		    name: 'OutMoney',
		    index: 'OutMoney',
		    width: 80
		},
        {
            label: '本期拆分数量',
        	name: 'ChangeQuantity',
        	index: 'ChangeQuantity',
        	width: 80
        },
		{
		    label: '本期拆分金额',
		    name: 'ChangeMoney',
		    index: 'ChangeMoney',
		    width: 80
		},
		{
		    label: '本期库存数量',
		    name: 'LibQuantity',
		    index: 'LibQuantity',
		    width: 80
		},
		{
		    label: '本期库存金额',
		    name: 'LibMoney',
		    index: 'LibMoney',
		    width: 80
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                MyJqGrid2.reloadGrid();
            },
            handleRefresh: function (btn) {
                MyJqGrid2.refreshGrid();
            }
        }
    });

    var MyJqGrid3 = new MyGrid({
        renderTo: 'MyJqGrid3',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl3,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        multiselect: false,
        initArray: [
        {
            label: '品名',
            name: 'GoodsName',
            index: 'GoodsName',
            width: 100
        },
		{
		    label: '日期',
		    name: 'OrderDate',
		    index: 'OrderDate',
		    formatter: 'date',
		    width: 80
		},
		{
		    label: '原因',
		    name: 'Reason',
		    index: 'Reason',
		    width: 80
		},
		{
		    label: '供应商或部门',
		    name: 'UnitName',
		    index: 'UnitName',
		    width: 80
		},
        {
            label: '增减数量',
            name: 'OrderNum',
            index: 'OrderNum',
            width: 80
        },
		{
		    label: '当时价格',
		    name: 'Price',
		    index: 'Price',
		    width: 80
		},
		{
		    label: '库存结余',
		    name: 'LibQuantity',
		    index: 'LibQuantity',
		    width: 80
		},
		{
		    label: '操作人',
		    name: 'Operater',
		    index: 'Operater',
		    width: 80
		},
		{
		    label: '仓库',
		    name: 'LibName',
		    index: 'LibName',
		    width: 80
		}, 
        {
		    label: '时间',
		    name: 'OrderDate',
		    index: 'OrderDate',
		    formatter: 'datetime',
		    width: 120
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                MyJqGrid3.reloadGrid();
            },
            handleRefresh: function (btn) {
                MyJqGrid3.refreshGrid();
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
        var currentIndex = $("#formula-tabs").tabs().tabs("option", "selected");
        if (currentIndex == 0) {
            var BeginDate = $("#BeginMonth").val();
            var EndDate = $("#EndMonth").val();
            if (BeginDate == '' && EndDate == '') {
                showMessage('提示', '请输入月份！');
                return;
            }
        }
        if (currentIndex == 1) {
            var BeginDate = $("#BeginDate").val();
            var EndDate = $("#EndDate").val();
            if (BeginDate == '' && EndDate == '') {
                showMessage('提示', '请输入查询时间！');
                return;
            }
        }
        if (currentIndex == 0) {
            var condition = GetSearch1();
            MyJqGrid1.refreshGrid(condition);
        }
        if (currentIndex == 1) {
            var condition = GetSearch2();
            MyJqGrid2.refreshGrid(condition);
        }

    }
    window.Search1 = function () {
        var condition =  GetSearch1();
        MyJqGrid1.refreshGrid(condition);
    }

    window.Search2 = function () {
        var condition = GetSearch2(); 
        MyJqGrid2.refreshGrid(condition);
    }

    window.Search3 = function () {
        condition = ""; 
        var GoodsID = $("input[name='GoodsID']").val()
        var LibID = $("#LibName2").val();
        if (!GoodsID || GoodsID == '') {
            showMessage('提示', '请选择商品查询！');
            return;
        }
        condition += GoodsID;
        condition += "&" + LibID;
        MyJqGrid3.refreshGrid(condition);
    }

    function SetSearchDate() {
        var date = new Date();
        var fdateM = date.getFullYear() + '-' + (date.getMonth()-1) ;
        var edateM = date.getFullYear() + '-' + (date.getMonth() + 1) ;
        var fdate = date.getFullYear() + '-' + (date.getMonth()) + '-' + date.getDate();
        var edate = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
        $("#BeginMonth").val(fdateM);
        $("#EndMonth").val(edateM);
        $("#BeginDate").val(fdate);
        $("#EndDate").val(edate);
    }
    SetSearchDate();
    function GetSearch1() {
        var condition = "";
        var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
        var node = treeObj.getSelectedNodes();
        
        if (node.length > 0) {
            var id = node[0].id;
            var name = node[0].name
            var typestring = node[0].typeNo;
            condition = typestring;
        } 
        var BeginDate = $("#BeginMonth").val();
        var EndDate = $("#EndMonth").val();
        condition += "&" + BeginDate;
        condition += "&" + EndDate;
        return condition;
    }
    function GetSearch2() {
        var condition = "";
        var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
        var node = treeObj.getSelectedNodes();
        if (node.length > 0) {
            var id = node[0].id;
            var name = node[0].name
            var typestring = node[0].typeNo;
            condition = typestring;
        }
        var BeginDate = $("#BeginDate").val();
        var EndDate = $("#EndDate").val();
        var GoodsName = $("#GoodsName").val();
        var LibName = $("#LibName").val();
        condition += "&" + BeginDate;
        condition += "&" + EndDate;
        condition += "&" + GoodsName;
        condition += "&" + LibName;
        return condition;
    }
}