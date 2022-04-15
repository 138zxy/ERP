function SC_PriceChangeIndexInit(opts) {
    $('#GoodgType').height(gGridHeight + 80);
    var nodeid = 0;
    var nodeName = "";
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight * 0.7 - 100,
        storeURL: opts.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
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
            label: '商品编码',
            name: 'GoodsCode',
            index: 'GoodsCode',
            width: 100
        },
		{
		    label: '品名',
		    name: 'GoodsName',
		    index: 'GoodsName',
		    width: 100
		},
		{
		    label: '规格',
		    name: 'Spec',
		    index: 'Spec',
		    width: 100
		},
		{
		    label: '单位',
		    name: 'Unit',
		    index: 'Unit',
		    width: 100
		}, 
        {
		    label: '库存总数量',
		    name: 'AllNum',
		    index: 'AllNum',
		    width: 100
		},
		{
		    label: '进价',
		    name: 'LibPrice',
		    index: 'LibPrice',
		    width: 100,
		    formatter: 'currency'
		}, 
		{
		    label: '库存总金额',
		    name: 'LibMoney',
		    index: 'LibMoney',
		    width: 100,
		    formatter: 'currency'
		},
		
        {
		    label: '品牌',
		    name: 'Brand',
		    index: 'Brand',
		    width: 100
		},
		{
		    label: '条码',
		    name: 'GoodsNo',
		    index: 'GoodsNo',
		    width: 100
		}],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            PriceChange: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的商品!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID; 
                myJqGrid.handleEdit({
                    loadFrom: 'SC_PriceChangeForm',
                    btn: btn,
                    width: 500,
                    height: 200,
                    afterFormLoaded: function () {
                        $("#CurPrice").val(Record.LibPrice);
                        $("#GoodsID").val(id); 
                    },
                    postCallBack: function (response) {
                        myJqGridTo.refreshGrid("GoodsID=" + id);
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
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '当时数量',
		    name: 'CurNum',
		    index: 'CurNum',
		    width: 100
		},
		{
		    label: '调整金额',
		    name: 'CurMoney',
		    index: 'CurMoney',
		    width: 100,
		    formatter: 'currency'
		},
		 {
		     label: '调整前单价',
		     name: 'CurPrice',
		     index: 'CurPrice',
		     width: 100,
		     formatter: 'currency'
		 },
         {
             label: '调整后单价',
             name: 'AlferPrice',
             index: 'AlferPrice',
             width: 100,
             formatter: 'currency'
         },
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
        {
            label: '调价人',
            name: 'Builder',
            index: 'Builder',
            width: 80
        },
        {
            label: '调价时间',
            name: 'BuildTime',
            index: 'BuildTime',
            width: 200,
            formatter: "datetime",
            searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ["ge"] }
        }
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
	    console.log("进入格式判断");
	    myJqGridTo.refreshGrid("GoodsID=" + id);
	});
}