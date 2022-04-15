function HR_KQ_HolidayIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' 
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true  
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true  }
            , { label: '节假日名称', name: 'HolidayName', index: 'HolidayName', width: 100 }
            , { label: '开始时间', name: 'StartDate', index: 'StartDate', width: 120, formatter: 'date' }
            , { label: '结束时间', name: 'EndDate', index: 'EndDate', width: 120, formatter: 'date' }
            , { label: '假日类型', name: 'HolidayType', index: 'HolidayType', width: 120 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 } 
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
        }
    }); 
    
}