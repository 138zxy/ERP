function B_YingYFIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight * 0.7 - 100
		, storeURL: options.storeUrl
		, sortByField: 'BuildTime'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , storeCondition: "(AuditStatus=1)"
		, initArray: [
              { label: '合同编码', name: 'ID', index: 'ID', width: 80 }
            , { label: '合同号', name: 'ContractNo', index: 'ContractNo', width: 200 }
            , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 200 }
            , { label: '客户名称', name: 'CustName', index: 'CustName', width: 100 } 
            , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', width: 100 }
            , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 80 }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: '应付款', name: 'PayMoney', index: 'PayMoney', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: '预付金额', name: 'PrePay', index: 'PrePay', width: 80, align: 'right', formatter: 'currency' }
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handlePay: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID; 
                myJqGrid.handleEdit({
                    loadFrom: 'SC_YingYSPayForm',
                    btn: btn,
                    prefix: "SC_YingSFrec",
                    afterFormLoaded: function () {
                        $("#B_YingSFrec_UnitID").val(Record.ID);
                        $("#Contract_ContractName").val(Record.ContractName);
                        $("#Contract_PayMoney").val(Record.PayMoney);
                        $("#Contract_PrePay").val(Record.PrePay);
                    },
                    postCallBack: function (response) {
                        myJqGridTo.refreshGrid();
                    }
                });
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial',
        autoWidth: true,
        buttons: buttons1,
        title:"来往记录明细",
        height: gGridHeight * 0.3,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30, 
        showPageBar: true,
        initArray: [
              { label: '日期', name: 'FinanceDate', index: 'FinanceDate', formatter: 'date', width: 120 }
            , { label: '单据号', name: 'FinanceNo', index: 'FinanceNo', width: 100 }
            , { label: '记录类型', name: 'Source', index: 'Source', width: 80 }
            , { label: '付款方式', name: 'PayType', index: 'PayType', width: 80 }
            , { label: '预付额', name: 'FinanceMoney', index: 'FinanceMoney', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>', align: 'right', formatter: 'currency' }
           // , { label: '付预付款额', name: 'YFinanceMoney', index: 'YFinanceMoney', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '优惠额', name: 'PayFavourable', index: 'PayFavourable', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>', align: 'right', formatter: 'currency' }
            , { label: '操作人', name: 'Builder', index: 'Builder', width: 80 } 
		],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            }   
        }
    });
    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("UnitID='" + id+"'" );
	});
}