function SC_YingSFrecIndexInit(options) {
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
        , groupField: 'SC_Supply.SupplierName'
        ,groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        }
		, initArray: [
              { label: '日期', name: 'FinanceDate', index: 'FinanceDate', formatter: 'datetime', width: 120 }
            , { label: '供应商', name: 'SC_Supply.SupplierName', index: 'SC_Supply.SupplierName', width: 80 }
            , { label: '收支', name: 'YingSF', index: 'YingSF', width: 80 }
            , { label: '单据号', name: 'FinanceNo', index: 'FinanceNo', width: 100 }
            , { label: '金额', name: 'FinanceMoney', index: 'FinanceMoney', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>'}
            , { label: '优惠额', name: 'PayFavourable', index: 'PayFavourable', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>'}
            , { label: '来源', name: 'Source', index: 'Source', width: 80 }
            , { label: '付款方式', name: 'PayType', index: 'PayType', width: 80 }
            , { label: '付款人', name: 'Payer', index: 'Payer', width: 80 }
            , { label: '收款人', name: 'Gatheringer', index: 'Gatheringer', width: 80 }
            , { label: '操作人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
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
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '入库单号',
		    name: 'SC_PiaoIn.InNo',
		    index: 'SC_PiaoIn.InNo',
		    width: 100 
		}, 
		{
		    label: '品种数',
		    name: 'SC_PiaoIn.VarietyNum',
		    index: 'SC_PiaoIn.VarietyNum',
		    width: 60
		},
		 {
		     label: '应付金额',
		     name: 'SC_PiaoIn.InMoney',
		     index: 'SC_PiaoIn.InMoney',
		     width: 60, formatter: 'currency'
		 },
         {
             label: '已付金额',
             name: 'SC_PiaoIn.PayMoney',
             index: 'SC_PiaoIn.PayMoney',
             width: 60, formatter: 'currency'
         },
         {
             label: '已欠款额',
             name: 'SC_PiaoIn.PayOwing',
             index: 'SC_PiaoIn.PayOwing',
             width: 60, formatter: 'currency'
         },
         {
             label: '已优惠额',
             name: 'SC_PiaoIn.PayFavourable',
             index: 'SC_PiaoIn.PayFavourable',
             width: 60, formatter: 'currency'
         },
         {
             label: '品种数',
             name: 'SC_PiaoIn.VarietyNum',
             index: 'SC_PiaoIn.VarietyNum',
             width: 60
         },
         {
             label: '本次付款',
             name: 'PayMoney',
             index: 'PayMoney',
             width: 60
         },
         {
             label: '本次优惠',
             name: 'PayFavourable',
             index: 'PayFavourable',
             width: 60
         },
        {
            label: '付款方式',
            name: 'SC_PiaoIn.PayType',
            index: 'SC_PiaoIn.PayType',
            width: 60
        },
        {
            label: '入库方式',
            name: 'SC_PiaoIn.InType',
            index: 'SC_PiaoIn.InType',
            width: 60
        },
        {
            label: '审核人',
            name: 'SC_PiaoIn.Auditor',
            index: 'SC_PiaoIn.Auditor',
            width: 100
        },
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		}],
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
	    console.log("进入格式判断");
	    myJqGridTo.refreshGrid("FinanceNo=" + id);
	});
}