function SC_PanDianLibIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'SC_PiaoInGrid',
        autoWidth: true,
        buttons: buttons0,
        height: 350,
        storeURL: options.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        storeCondition: "(InType='报损' or InType='报溢')",
        groupField: 'SC_Lib.LibName',
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '入库单号',
		    name: 'InNo',
		    index: 'InNo',
		    width: 120
		}, 
		{
		    label: '仓库ID',
		    name: 'LibID',
		    index: 'LibID',
		    width: 100,
		    hidden: true
		}, 
        {
            label: '仓库',
            name: 'SC_Lib.LibName',
            index: 'SC_Lib.LibName',
            width: 80
        },
		{
		    label: '入库方式',
		    name: 'InType',
		    index: 'InType',
		    width: 80
		},
		{
		    label: '日期',
		    name: 'InDate',
		    index: 'InDate',
		    formatter: 'date',
		    width: 80
		}, 
		{
		    label: '品种数',
		    name: 'VarietyNum',
		    index: 'VarietyNum',
		    width: 60
		},
		{
		    label: '入库金额',
		    name: 'InMoney',
		    index: 'InMoney',
		    width: 60
		},
		{
		    label: '状态',
		    name: 'Condition',
		    index: 'Condition',
		    width: 60
		}, 
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
		{
		    label: '审核人',
		    name: 'Auditor',
		    index: 'Auditor',
		    width: 80
		},
		{
		    label: '审核时间',
		    name: 'AuditTime',
		    index: 'AuditTime',
		    formatter: 'datetime',
		    width: 120
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
    var myJqGridTo = new MyGrid({
        renderTo: 'SC_ZhangInGrid',
        autoWidth: true,
        buttons: buttons1,
        height: 100,
        storeURL: options.ZhangstoreUrl,
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
		    label: '订单号',
		    name: 'InNo',
		    index: 'InNo',
		    width: 100,
		    hidden: true
		},
		{
		    label: '商品',
		    name: 'GoodsID',
		    index: 'GoodsID',
		    width: 100,
		    hidden: true
		},
        {
            label: '商品编码',
            name: 'SC_Goods.GoodsCode',
            index: 'SC_Goods.GoodsCode',
            width: 100
        },
		{
		    label: '品名',
		    name: 'SC_Goods.GoodsName',
		    index: 'SC_Goods.GoodsName',
		    width: 100
		},
		{
		    label: '分类',
		    name: 'SC_Goods.SC_GoodsType.TypeName',
		    index: 'SC_Goods.SC_GoodsType.TypeName',
		    width: 100
		},
		{
		    label: '规格',
		    name: 'SC_Goods.Spec',
		    index: 'SC_Goods.Spec',
		    width: 100
		},
		{
		    label: '数量',
		    name: 'Quantity',
		    index: 'Quantity',
		    width: 100
		},
		{
		    label: '单位',
		    name: 'SC_Goods.Unit',
		    index: 'SC_Goods.Unit',
		    width: 100
		},
		{
		    label: '单价',
		    name: 'PriceUnit',
		    index: 'PriceUnit',
		    width: 100
		},
		{
		    label: '金额',
		    name: 'InMoney',
		    index: 'InMoney',
		    width: 100
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
	    myJqGridTo.refreshGrid("InNo=" + id);
	}); 
}