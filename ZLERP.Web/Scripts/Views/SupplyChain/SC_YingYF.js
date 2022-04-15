function SC_YingYFIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight * 0.7 - 100
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: '名称', name: 'SupplierName', index: 'SupplierName', width: 80 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 80 }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', hidden: true, width: 80 }
            , { label: '应付款', name: 'PayMoney', index: 'PayMoney', hidden: true, width: 80 }
            , { label: '预付金额', name: 'PrePay', index: 'PrePay', width: 80, formatter: 'currency' }
            , { label: '地址', name: 'Adrress', index: 'Adrress', width: 120 }
            , { label: '联系人', name: 'Linker', index: 'Linker', width: 80 }
            , { label: '联系电话', name: 'LinkPhone', index: 'LinkPhone', width: 80 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
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
                        $("#SC_YingSFrec_UnitID").val(Record.ID); 
                        $("#SC_Supply_SupplierName").val(Record.SupplierName);
                        $("#SC_Supply_PayMoney").val(Record.PayMoney);
                        $("#SC_Supply_PrePay").val(Record.PrePay);
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
            , { label: '预付额', name: 'FinanceMoney', index: 'FinanceMoney', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '付预付款额', name: 'YFinanceMoney', index: 'YFinanceMoney', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '优惠额', name: 'PayFavourable', index: 'PayFavourable', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
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
	    myJqGridTo.refreshGrid("UnitID=" + id );
	});
}