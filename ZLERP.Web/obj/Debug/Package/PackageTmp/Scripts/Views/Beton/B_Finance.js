function B_FinanceIndexInit(options) {
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
        , singleSelect: false 
		, initArray: [
              { label: '编号', name: 'ID', index: 'ID', width: 30 }
            , { label: '申请编号', name: 'FinanceNo', index: 'FinanceNo', width: 120 }
            , { label: '申请类别', name: 'Modeltype', index: 'Modeltype', width: 120 }
            , { label: '核准状态', name: 'AuditStatus', index: 'AuditStatus', search: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, width: 60 }
            , { label: '收付款方', name: 'UnitID', index: 'UnitID', width: 80 }
            , { label: '收付款方名称', name: 'UnitName', index: 'UnitName', width: 80 }

            , { label: '付款方式', name: 'PayType', index: 'PayType', width: 100 }
            , { label: '付款日期', name: 'FinanceDate', index: 'FinanceDate', formatter: 'date', width: 80 }
            , { label: '付款人', name: 'Payer', index: 'Payer', width: 100 }
            , { label: '收款人', name: 'Gatheringer', index: 'Gatheringer', width: 100 }

            , { label: '付款总金额', name: 'FinanceMoney', index: 'FinanceMoney', width: 80, align: 'right', formatter: 'currency' }

            , { label: '优惠总金额', name: 'PayFavourable', index: 'PayFavourable', width: 80, formatter: 'currency' }

            , { label: '备注', name: 'Remark', index: 'Remark', width: 100 }
            , { label: '核准时间', name: 'AuditTime', index: 'AuditTime', formatter: 'date', search: false }
            
            , { label: '核准人', name: 'Auditor', index: 'Auditor', search: false }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }

		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            AuditExec: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                } 
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                showConfirm("确认信息", "请认真核对当前信息，一旦执行，不可撤回？",
				function () {
				    ajaxRequest(options.AuditExecUrl, { id: id },
				        false,
				        function (response) {
				            if (response.Result) {
				                showMessage('提示', response.Message + "，已生成收付款/收开发票明细!");
				                myJqGrid.reloadGrid();
				                return;
				            } else {
				                showMessage('提示', response.Message);
				                return;
				            }

				        })
				});

            },
				UnExec: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }

                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                showConfirm("确认信息", "你确定驳回当前申请？",
				function () {
				    ajaxRequest(options.UnExecUrl, { id: id },
				        false,
				        function (response) {
				            if (response.Result) {
				                myJqGrid.reloadGrid();
				                return;
				            } else {
				                showMessage('提示', response.Message);
				                return;
				            }

				        })
				});

            } 
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial', 
        autoWidth: true,
        buttons: buttons1,
        title:"本次申请的单据",
        height: gGridHeight * 0.3,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [

              { label: '编号', name: 'ID', index: 'ID', width: 50 }
            , { label: '日期', name: 'OrderDate', index: 'OrderDate', formatter: 'date', width: 80 }
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 120 }
            , { label: '收付款方', name: 'UnitName', index: 'UnitName', width: 150 }
            , { label: '收付款对象', name: 'NextName', index: 'NextName', width: 150 }
            , { label: '纳入月份', name: 'InMonth', index: 'InMonth', width: 100 }
            , { label: '结算总金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '付款/开票额', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '优惠额', name: 'PayFavourable', index: 'PayFavourable', width: 80, search: false, align: 'right', formatter: 'currency' } 
 
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
	    myJqGridTo.refreshGrid("FinanceID='" + id + "'");
	}); 
}