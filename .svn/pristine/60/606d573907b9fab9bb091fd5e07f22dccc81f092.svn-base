function CarKeepIndexInit(options) {
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
              { label: 'ID', name: 'ID', index: 'ID', width: 80}
            , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
            , { label: '车牌号', name: 'Car.CarNo', index: 'Car.CarNo', width: 80 }
            , { label: '保养里程', name: 'KeepMileage', index: 'KeepMileage', width: 80 }
            , { label: '保养日期', name: 'KeepDate', index: 'KeepDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            , { label: '保养金额', name: 'KeepCount', index: 'KeepCount', width: 80 }
            , { label: '经办人', name: 'Opreater', index: 'Opreater', width: 80 }
            , { label: '保养单位', name: 'KeepUnit', index: 'KeepUnit', width: 80, hidden: true }
            , { label: '保养单位', name: 'CarDealings.Name', index: 'CarDealings.Name', width: 80 }
            , { label: '保养项目', name: 'KeepItem', index: 'KeepItem', width: 80 }
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
                    loadFrom: 'CarKeepMyFormDiv',
                    btn: btn,
                    width: 500,
                    height: 400
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarKeepMyFormDiv',
                    btn: btn,
                    width: 500,
                    height: 400
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