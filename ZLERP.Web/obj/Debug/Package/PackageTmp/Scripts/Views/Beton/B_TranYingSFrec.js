﻿function B_TranYingSFrecIndexInit(options) {
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
        groupField: 'B_CarFleet.FleetName',
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
		    label: '运输单位编码',
		    name: 'B_CarFleet.FleetCode',
		    index: 'B_CarFleet.FleetCode',
		    width: 80
		},
		{
		    label: '运输单位名称',
		    name: 'B_CarFleet.FleetName',
		    index: 'B_CarFleet.FleetName',
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
		    name: 'B_TranBalance.BaleNo',
		    index: 'B_TranBalance.BaleNo',
		    width: 120
		},
		{
		    label: '结算日期',
		    name: 'B_TranBalance.BaleDate',
		    index: 'B_TranBalance.BaleDate',
		    formatter: 'date',
		    width: 60
		}
        //  , { label: '运输单位编码', name: 'B_TranBalance.TranID', index: 'B_TranBalance.TranID', width: 60 }
		, {
		    label: '运输单位编码',
		    name: 'B_TranBalance.B_CarFleet.FleetCode',
		    index: 'B_TranBalance.B_CarFleet.FleetCode',
		    width: 60
		},
		{
		    label: '运输单位名称',
		    name: 'B_TranBalance.B_CarFleet.FleetName',
		    index: 'B_TranBalance.B_CarFleet.FleetName',
		    width: 150
		},
		{
		    label: '工程名称',
		    name: 'B_TranBalance.Project.ProjectName',
		    index: 'B_TranBalance.Project.ProjectName',
		    width: 120
		},
		{
		    label: '应付金额',
		    name: 'B_TranBalance.AllOkMoney',
		    index: 'B_TranBalance.AllOkMoney',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '已付金额',
		    name: 'B_TranBalance.PayMoney',
		    index: 'B_TranBalance.PayMoney',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '已欠款额',
		    name: 'B_TranBalance.PayOwing',
		    index: 'B_TranBalance.PayOwing',
		    width: 60,
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '已优惠额',
		    name: 'B_TranBalance.PayFavourable',
		    index: 'B_TranBalance.PayFavourable',
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

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    console.log("进入格式判断");
	    myJqGridTo.refreshGrid("FinanceNo=" + id);
	});
}