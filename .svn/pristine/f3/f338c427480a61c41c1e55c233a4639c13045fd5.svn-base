function M_YingSFrecIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight * 0.7 - 100,
        storeURL: options.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        groupField: 'SupplyName',
        groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        },
        initArray: [{
            label: '日期',
            name: 'FinanceDate',
            index: 'FinanceDate',
            formatter: 'date',
            width: 120
        },
		{
		    label: '合同编码',
		    name: 'UnitID',
		    index: 'UnitID',
		    width: 80
		},
		{
		    label: '供应商',
		    name: 'SupplyName',
		    index: 'SupplyName',
		    width: 150
		},
		{
		    label: '收支',
		    name: 'YingSF',
		    index: 'YingSF',
		    width: 80
		},
		{
		    label: '单据号',
		    name: 'FinanceNo',
		    index: 'FinanceNo',
		    width: 100
		},
		{
		    label: '金额',
		    name: 'FinanceMoney',
		    index: 'FinanceMoney',
		    width: 80,
		    search: false,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>',
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '优惠额',
		    name: 'PayFavourable',
		    index: 'PayFavourable',
		    width: 80,
		    search: false,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>',
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '来源',
		    name: 'Source',
		    index: 'Source',
		    width: 80
		},
		{
		    label: '付款方式',
		    name: 'PayType',
		    index: 'PayType',
		    width: 80
		},
		{
		    label: '付款人',
		    name: 'Payer',
		    index: 'Payer',
		    width: 80
		},
		{
		    label: '收款人',
		    name: 'Gatheringer',
		    index: 'Gatheringer',
		    width: 80
		},
		{
		    label: '操作人',
		    name: 'Builder',
		    index: 'Builder',
		    width: 80
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
        { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime'}],
        autoLoad: true,
        functions: {
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
        title: "本次付款明细",
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
		    label: '结算单号',
		    name: 'M_BaleBalance.BaleNo',
		    index: 'M_BaleBalance.BaleNo',
		    width: 120
		},
		{
		    label: '结算日期',
		    name: 'M_BaleBalance.BaleDate',
		    index: 'M_BaleBalance.BaleDate',
		    formatter: 'date',
		    width: 60
		},
		{
		    label: '供货厂商编码',
		    name: 'M_BaleBalance.StockPactID',
		    index: 'M_BaleBalance.StockPactID',
		    width: 60
		},
		{
		    label: '供货厂商',
		    name: 'M_BaleBalance.SupplyInfo.SupplyName',
		    index: 'M_BaleBalance.SupplyInfo.SupplyName',
		    width: 150
		},
		{
		    label: '材料名称',
		    name: 'M_BaleBalance.StuffInfo.StuffName',
		    index: 'M_BaleBalance.StuffInfo.StuffName',
		    width: 120
		},
		{
		    label: '应付金额',
		    name: 'M_BaleBalance.AllOkMoney',
		    index: 'M_BaleBalance.AllOkMoney',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '已付金额',
		    name: 'M_BaleBalance.PayMoney',
		    index: 'M_BaleBalance.PayMoney',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '已欠款额',
		    name: 'M_BaleBalance.PayOwing',
		    index: 'M_BaleBalance.PayOwing',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '已优惠额',
		    name: 'M_BaleBalance.PayFavourable',
		    index: 'M_BaleBalance.PayFavourable',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '本次付款',
		    name: 'PayMoney',
		    index: 'PayMoney',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '本次优惠',
		    name: 'PayFavourable',
		    index: 'PayFavourable',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
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

    var MyJqGrid5 = new MyGrid({
        renderTo: 'myJqGrid5',
        width: '100%',
        autoWidth: true,
        buttons: buttons2,
        height: gGridHeight,
        storeURL: options.FindrecordUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        groupField: 'FinanceType',
        groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        },
        initArray: [{
            label: '日期',
            name: 'FinanceDate',
            index: 'FinanceDate',
            formatter: 'datetime',
            width: 120
        },
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
		{
		    label: '资产分类',
		    name: 'FinanceType',
		    index: 'FinanceType',
		    width: 80
		},
		{
		    label: '消耗或支出',
		    name: 'IsInOrOut',
		    index: 'IsInOrOut',
		    width: 80
		},
		{
		    label: '金额',
		    name: 'FinanceMoney',
		    index: 'FinanceMoney',
		    width: 80,
		    search: false,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>',
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '余额',
		    name: 'Balance',
		    index: 'Balance',
		    width: 80,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '经办人',
		    name: 'Operater',
		    index: 'Operater',
		    width: 80
		},
		{
		    label: '经营性分类',
		    name: 'UseType',
		    index: 'UseType',
		    width: 80
		},
		{
		    label: '自动记录号',
		    name: 'ID',
		    index: 'ID',
		    width: 80
		}],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            }
        }
    });

    var myJqGrid2 = new MyGrid({
        renderTo: 'MyJqGrid2',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight * 0.7 - 100,
        storeURL: options.storeUrl2,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        groupField: 'SupplyName',
        groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        },
        initArray: [{
            label: '日期',
            name: 'FinanceDate',
            index: 'FinanceDate',
            formatter: 'date',
            width: 120
        },
       	{
       	    label: '发票号',
       	    name: 'PiaoNo',
       	    index: 'PiaoNo',
       	    width: 100
       	}, 
		{
		    label: '供应商名称',
		    name: 'SupplyName',
		    index: 'SupplyName',
		    width: 150
		},

		{
		    label: '单据号',
		    name: 'FinanceNo',
		    index: 'FinanceNo',
		    width: 100
		},
		{
		    label: '开票金额',
		    name: 'FinanceMoney',
		    index: 'FinanceMoney',
		    width: 120,
		    search: false,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>',
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '免开金额',
		    name: 'PayFavourable',
		    index: 'PayFavourable',
		    width: 120,
		    search: false,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>',
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '来源',
		    name: 'Source',
		    index: 'Source',
		    width: 80
		},
		{
		    label: '发票类型',
		    name: 'PayType',
		    index: 'PayType',
		    width: 80
		},
		{
		    label: '开票人',
		    name: 'Payer',
		    index: 'Payer',
		    width: 80
		},
		{
		    label: '收票人',
		    name: 'Gatheringer',
		    index: 'Gatheringer',
		    width: 80
		},
		{
		    label: '操作人',
		    name: 'Builder',
		    index: 'Builder',
		    width: 80
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
        { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' },
        { label: "发票文件", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false }
        ],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            }
        }

    });
    var myJqGridTo2 = new MyGrid({
        renderTo: 'myJqGridDetial2',
        autoWidth: true,
        buttons: buttons1,
        height: gGridHeight * 0.3,
        title: "本次收票明细",
        storeURL: options.DelstoreUrl2,
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
		    label: '结算单号',
		    name: 'M_BaleBalance.BaleNo',
		    index: 'M_BaleBalance.BaleNo',
		    width: 120
		},
		{
		    label: '结算日期',
		    name: 'M_BaleBalance.BaleDate',
		    index: 'M_BaleBalance.BaleDate',
		    formatter: 'date',
		    width: 60
		},
		{
		    label: '供货厂商编码',
		    name: 'M_BaleBalance.StockPactID',
		    index: 'M_BaleBalance.StockPactID',
		    width: 60
		},
		{
		    label: '供货厂商',
		    name: 'M_BaleBalance.SupplyInfo.SupplyName',
		    index: 'M_BaleBalance.SupplyInfo.SupplyName',
		    width: 150
		},
		{
		    label: '材料名称',
		    name: 'M_BaleBalance.StuffInfo.StuffName',
		    index: 'M_BaleBalance.StuffInfo.StuffName',
		    width: 120
		},
		{
		    label: '结算金额',
		    name: 'M_BaleBalance.AllOkMoney',
		    index: 'M_BaleBalance.AllOkMoney',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '已开票额',
		    name: 'M_BaleBalance.PiaoPayMoney',
		    index: 'M_BaleBalance.PiaoPayMoney',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '未开票额',
		    name: 'M_BaleBalance.PiaoPayOwing',
		    index: 'M_BaleBalance.PiaoPayOwing',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '免开票额',
		    name: 'M_BaleBalance.PiaoPayFavourable',
		    index: 'M_BaleBalance.PiaoPayFavourable',
		    width: 100,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '本次开票额',
		    name: 'PayMoney',
		    index: 'PayMoney',
		    width: 100,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '本次免开额',
		    name: 'PayFavourable',
		    index: 'PayFavourable',
		    width: 100,
		    align: 'right',
		    formatter: 'currency'
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
	myJqGrid2.addListeners('rowclick',
	function (id, record, selBool) {
	    console.log("进入格式判断");
	    myJqGridTo2.refreshGrid("FinanceNo=" + id);
	});
}