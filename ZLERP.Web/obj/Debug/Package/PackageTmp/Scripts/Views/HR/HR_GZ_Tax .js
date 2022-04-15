function init(model)
{
    $("#HR_GZ_TaxSet_ID").val(model.ID);
    $("#HR_GZ_TaxSet_TaxMoney").val(model.TaxMoney);
} 
function HR_GZ_TaxIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' 
        , autoWidth: true
        , buttons: buttons0
        , height: 280
		, storeURL: options.itemUrl
        , editSaveUrl: options.itemUpdateUrl
		, sortByField: 'OrderNum'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
        , sortOrder: 'ASC' 
		, showPageBar: true  
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '级数', name: 'OrderNum', index: 'OrderNum', editable: true,  width: 80 } 
            , { label: '起始金额', name: 'Startmoney', index: 'Startmoney',editable: true,   width: 80 }
            , { label: '结算金额', name: 'Endmoney', index: 'Endmoney',editable: true,  width: 80 }
            , { label: '税率%', name: 'Rate', index: 'Rate', editable: true,   width: 120 } 

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
                    btn: btn,
                    prefix: "HR_GZ_Tax",
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    window.Save = function () {
        var ID = $("#HR_GZ_TaxSet_ID").val();
        var TaxMoney = $("#HR_GZ_TaxSet_TaxMoney").val(); 
        var requestURL = options.AddOrUpdateUrl;
        ajaxRequest(requestURL, {
            ID: ID,
            TaxMoney: TaxMoney 
        },
		false,
		function (response) {
			if (!!response.Result) {
               if(response.Data!=null)
                {
                    $("#HR_GZ_TaxSet_ID").val(response.Data); 
                }
				showMessage('提示', '保存成功！'); 
			} else {
				showMessage('提示', response.Message);
			}
		});
    }

}