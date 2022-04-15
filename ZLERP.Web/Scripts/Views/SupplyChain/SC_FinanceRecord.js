function SC_FinanceRecordIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , groupField: 'FinanceType'
        ,groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        }
		, initArray: [ 
              { label: '日期', name: 'FinanceDate', index: 'FinanceDate', formatter: 'datetime', width: 120 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
            , { label: '资产分类', name: 'FinanceType', index: 'FinanceType', width: 80 }
            , { label: '消耗或支出', name: 'IsInOrOut', index: 'IsInOrOut', width: 80 }
            , { label: '金额', name: 'FinanceMoney', index: 'FinanceMoney', width: 80 , search: false,summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '余额', name: 'Balance', index: 'Balance', width: 80 }
            , { label: '经办人', name: 'Operater', index: 'Operater', width: 80 }
            , { label: '经营性分类', name: 'UseType', index: 'UseType', width: 80 } 
            , { label: '自动记录号', name: 'ID', index: 'ID', width: 80 }
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
    
}