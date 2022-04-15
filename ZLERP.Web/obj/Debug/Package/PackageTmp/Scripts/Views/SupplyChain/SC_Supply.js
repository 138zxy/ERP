function SC_SupplyIndexInit(options) {
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
        , storeCondition: 'IsOff = 0'
        , groupField: 'SupplierType'
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '名称', name: 'SupplierName', index: 'SupplierName', width: 80 }
            , { label: '供应商分类', name: 'SupplierType', index: 'SupplierType', width: 80 }
            , { label: '地址', name: 'Adrress', index: 'Adrress', width: 100 }
            , { label: '联系人', name: 'Linker', index: 'Linker', width: 80 }
            , { label: '联系电话', name: 'LinkPhone', index: 'LinkPhone', width: 80 }
            , { label: '最近采购日', name: 'NearPurDate', index: 'NearPurDate', formatter: 'date', width: 80}  
            , { label: '期初应付额', name: 'PaidIn', index: 'PaidIn', width: 80, formatter: 'currency' }
            , { label: '应付额', name: 'PayMoney', index: 'PayMoney', width: 80,formatter: 'currency'}
            , { label: '账号', name: 'AccountNo', index: 'AccountNo', width: 80 }
            , { label: '税号', name: 'TaxNo', index: 'TaxNo', width: 80 }
            , { label: '期初预付额', name: 'PrepayInit', index: 'PrepayInit', width: 80, formatter: 'currency' }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', width: 80, formatter: 'currency' }
            , { label: '付款方式', name: 'FinanceType', index: 'FinanceType', width: 80 }
            , { label: '联系电话2', name: 'LinkPhone2', index: 'LinkPhone2', width: 80 }
            , { label: '采购折扣', name: 'Discount', index: 'Discount', width: 80 }
            , { label: '是否停用', name: 'IsOff', index: 'IsOff', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
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
                    loadFrom: 'MyFormDiv',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
            ,
            handleOffSupply: function (btn) {
                myJqGrid.refreshGrid('IsOff = 1');
            }
            ,
            handleNotOffSuplly: function (btn) {
                myJqGrid.refreshGrid('IsOff = 0');
            }
        }
    });
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv', 
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