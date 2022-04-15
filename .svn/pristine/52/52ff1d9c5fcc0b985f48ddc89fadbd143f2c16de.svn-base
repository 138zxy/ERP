function CarMaintainItemIndexInit(options) {
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
            , { label: '编码', name: 'Code', index: 'Code', width: 80 }
            , { label: '项目大类', name: 'ItemType', index: 'ItemType', width: 80 }
            , { label: '维修项目', name: 'ItemName', index: 'ItemName', width: 80 }
            , { label: '拼音简码', name: 'BrevityCode', index: 'BrevityCode', width: 80 }
            , { label: '材料单价', name: 'MaterialPrice', index: 'MaterialPrice', width: 80 } 
            , { label: '单位', name: 'Unit', index: 'Unit', width: 80 } 
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
        }
    });

    $("#ItemName").change(function () {
        var str = $("#ItemName").val();
        ajaxRequest(options.LP_TOPY_SHORTUrl, {
            str: str
        },
		false,
		function (response) {
		    if (response.Result) {
		        $("#BrevityCode").val(response.Data);

		    } else {
		        showMessage('提示', response.Message);
		    }
		});
    });
    
}