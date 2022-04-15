function B_CarFleetIndexInit(options) {
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
        , groupField: 'FleetType'
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true  }
            , { label: '运输单位编号', name: 'FleetCode', index: 'FleetCode', width: 80 }
            , { label: '运输单位名称', name: 'FleetName', index: 'FleetName', width: 80 }
            , { label: '运输单位类别', name: 'FleetType', index: 'FleetType', width: 100 }
            , { label: '地址', name: 'Adrress', index: 'Adrress', width: 100 }
            , { label: '联系人', name: 'Linker', index: 'Linker', width: 80 }
            , { label: '联系电话', name: 'LinkPhone', index: 'LinkPhone', width: 80 }
            , { label: '期初应付额', name: 'PaidIn', index: 'PaidIn', width: 80 }
            , { label: '应付额', name: 'PayMoney', index: 'PayMoney', width: 80 }
            , { label: '账号', name: 'AccountNo', index: 'AccountNo', width: 80 }
            , { label: '税号', name: 'TaxNo', index: 'TaxNo', width: 80 }
            , { label: '期初预付额', name: 'PrepayInit', index: 'PrepayInit', width: 80 }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', width: 80 }
            , { label: '默认运费模板', name: 'B_FareTemplet.FareTempletName', index: 'B_FareTemplet.FareTempletName', width: 80 }
            , { label: '是否停运', name: 'IsStop', index: 'IsStop', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
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
            handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDivB_CarFleet',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDivB_CarFleet',
                    btn: btn
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            } 
        }
    });
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDivB_CarFleet', 
            title: '查看详细',
            width: 800,
            height: 400,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () { 
            }
        });
    });

}