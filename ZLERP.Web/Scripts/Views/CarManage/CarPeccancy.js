function CarPeccancyIndexInit(options) {
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
            , { label: '驾驶员', name: 'Driver', index: 'Driver', width: 80 }
            , { label: '违章日期', name: 'PeccancyDate', index: 'PeccancyDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            , { label: '罚款金额', name: 'PeccancyCost', index: 'PeccancyCost', width: 80 }
            , { label: '扣分', name: 'PeccancyNum', index: 'PeccancyNum', width: 80 }
            , { label: '违章项目', name: 'PeccancyItem', index: 'PeccancyItem', width: 80}
            , { label: '执行单位', name: 'CarDealings.Name', index: 'CarDealings.Name', width: 80 }
            , { label: '执行单位', name: 'PeccancyUnit', index: 'PeccancyUnit', width: 80, hidden: true }
            , { label: '违章地址', name: 'PeccancyAdress', index: 'PeccancyAdress', width: 80 }
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
                    loadFrom: 'CarPeccancyMyFormDiv',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarPeccancyMyFormDiv',
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