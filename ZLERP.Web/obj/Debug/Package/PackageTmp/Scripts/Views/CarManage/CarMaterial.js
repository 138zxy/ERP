function CarMaterialIndexInit(options) {
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
              { label: '编号', name: 'ID', index: 'ID', width: 80}
            , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
            , { label: '车牌号', name: 'Car.CarNo', index: 'Car.CarNo', width: 80 }

            , { label: '经办人', name: 'Oprater', index: 'Oprater', width: 80 }
            , { label: '产品名称', name: 'MaterialName', index: 'MaterialName', width: 80 }
            , { label: '购置日期', name: 'BuyDate', index: 'BuyDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            , { label: '规格型号', name: 'Spec', index: 'Spec', width: 80 }
            , { label: '单位', name: 'Unit', index: 'Unit', width: 80 }
            , { label: '数量', name: 'Quantity', index: 'Quantity', width: 80 }
            , { label: '单价', name: 'Price', index: 'Price', width: 80 }
            , { label: '金额', name: 'MoneyAll', index: 'MoneyAll', width: 80 }
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
                    loadFrom: 'CarMaterialMyFormDiv',
                    btn: btn,
                    width: 500,
				    height: 400
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarMaterialMyFormDiv',
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


    $("#Quantity").change(function () {
        GetMoneyFun();
    });
    $("#Price").change(function () {
        GetMoneyFun();
    });
    function GetMoneyFun() {
        var UnitPrice = $("#Quantity").val();
        var Quantity = $("#Price").val();
        var money = UnitPrice * Quantity;
        $("#MoneyAll").val(money)
    }
     
}