﻿function M_TranYingYFIndexInit(options) {
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
        , storeCondition: "(supplykind in ('Su3','Su5') and IsUsed =1)"
		, initArray: [
              { label: '运输公司编码', name: 'ID', index: 'ID',  width: 80 }
            , { label: '运输公司', name: 'SupplyName', index: 'SupplyName', width: 200 }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: '应付款', name: 'PayMoney', index: 'PayMoney', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: '预付金额', name: 'PrePay', index: 'PrePay', width: 80, align: 'right', formatter: 'currency' }
            , { label: '地址', name: 'SupplyAddr', index: 'SupplyAddr', width: 120 }
            , { label: '联系人', name: 'LinkMan', index: 'LinkMan', width: 80 }
            , { label: '联系电话', name: 'LinkTel', index: 'LinkTel', width: 80 }
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
                        $("#M_TranYingSFrec_UnitID").val(Record.ID);
                        $("#SupplyInfo_SupplyName").val(Record.SupplyName);
                        $("#SupplyInfo_PayMoney").val(Record.PayMoney);
                        $("#SupplyInfo_PrePay").val(Record.PrePay);
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
            , { label: '付预款支付', name: 'YFinanceMoney', index: 'YFinanceMoney', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>', align: 'right', formatter: 'currency' }
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