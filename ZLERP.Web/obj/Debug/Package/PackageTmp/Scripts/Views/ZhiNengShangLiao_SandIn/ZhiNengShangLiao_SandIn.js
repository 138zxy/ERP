function supplyInfoInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , sortByField: ' ID DESC'
		    , sortByField: 'BuildTime'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 500
            , dialogHeight: 420
		    , showPageBar: true
		    , initArray: [
                  { label: '入库编号', name: 'ID', index: 'ID', width: 80, key: true }
                , { label: '筒仓编号', name: 'SiloID', index: 'SiloID' }
                , { label: '筒仓名称', name: 'Silo.SiloName', index: 'Silo.SiloName' }
                , { label: '材料编号', name: 'StuffID', index: 'StuffID'}
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', searchable: false }
                , { label: '入库数量(KG)', name: 'InNum', index: 'InNum', width: 80 }
                , { label: '料场称重编号', name: 'StockControlID', index: 'StockControlID' }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {//加载
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {//刷新
                    myJqGrid.refreshGrid('1=1');
                }
            }
    });
}