function CarDealingsIndexInit(options) {
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
            , { label: '单位名称', name: 'Name', index: 'Name', width: 80 }
            , { label: '拼音简码', name: 'BrevityCode', index: 'BrevityCode', width: 80 }
            , { label: '类别', name: 'DealingsType', index: 'DealingsType', width: 80 }
            , { label: '联系人', name: 'Linker', index: 'Linker', width: 80 }
            , { label: '手机', name: 'LinkPhone', index: 'LinkPhone', width: 80 }
            , { label: '电话', name: 'LinkPhone2', index: 'LinkPhone2', width: 80 }
            , { label: '邮箱', name: 'Mail', index: 'Mail', width: 80 }
            , { label: '地址', name: 'Adress', index: 'Adress', width: 80 }
            , { label: '是否停业', name: 'IsOff', index: 'IsOff', width: 80,formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
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
                    loadFrom: 'CarDealingsMyFormDiv',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarDealingsMyFormDiv',
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

    $("#Name").change(function () {
        var str = $("#Name").val();
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