function SC_Fixed_CirculateIndexInit(options) {
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
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'FixedID', name: 'FixedID', index: 'FixedID', width: 80, hidden: true }
            , { label: '流水号', name: 'CirculateNo', index: 'CirculateNo', width: 130 }
            , { label: '资产编号', name: 'Fcode', index: 'Fcode', width: 80 }
            , { label: '资产名称', name: 'Fname', index: 'Fname', width: 80 }
            , { label: '资产条形码', name: 'BarCode', index: 'BarCode', width: 80 }
            , { label: '资产拼音简码', name: 'BrevityCode', index: 'BrevityCode', width: 80 }
            , { label: '资产所属部门', name: 'DepartMent', index: 'DepartMent', width: 80 }
            , { label: '借出日期', name: 'BorrowDate', index: 'BorrowDate', width: 80, formatter: 'date' }
            , { label: '批准人', name: 'ApproveMan', index: 'ApproveMan', width: 80 }
            , { label: '借用部门', name: 'BorrowDepart', index: 'BorrowDepart', width: 80 }
            , { label: '借用人', name: 'BorrowMan', index: 'BorrowMan', width: 80 }
            , { label: '拟还日期', name: 'MayBackDate', index: 'MayBackDate', width: 80, formatter: 'date' }
            , { label: '借出备注', name: 'BorrowRemark', index: 'BorrowRemark', width: 80 }
            , { label: '是否归还', name: 'IsBack', index: 'IsBack', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '归还日期', name: 'BackDate', index: 'BackDate', width: 80, formatter: 'date' }
            , { label: '归还备注', name: 'BackRemark', index: 'BackRemark', width: 200 }
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
                ajaxRequest(options.GenerateOrderNoUrl, {},
				false,
				function (response) {
				    if (response.Result) {

				        myJqGrid.handleAdd({
				            loadFrom: 'MyFormDiv',
				            btn: btn,
				            width: 500,
				            height: 450,
				            afterFormLoaded: function () {
				                $("#CirculateNo").val(response.Data);
				                $("#HiddenIsBack").hide();
				                $("#HiddenBackRemark").hide();
				                FixedChange();
				            }
				        });
				    }
				});

            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    width: 500,
                    height: 450,
                    afterFormLoaded: function () { 
                        $("#HiddenIsBack").show();
                        $("#HiddenBackRemark").show();
                        FixedChange();
                    }
                });
            },
            handleAll: function (btn) {
                ajaxRequest(options.GenerateOrderNoUrl, {},
				false,
				function (response) {
				    if (response.Result) {

				        myJqGrid.handleAdd({
				            loadFrom: 'MyFormDiv',
				            btn: btn,
				            width: 500,
				            height: 450,
				            afterFormLoaded: function () {
				                $("#HiddenIsBack").show();
				                $("#HiddenBackRemark").show();
				                $("#CirculateNo").val(response.Data); 
				                FixedChange();
				            }
				        });
				    }
				}); 
            },  
            handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看借还详细',
            width: 500,
            height: 450,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () { 
            }
        });
    });
    FixedChange = function () {
        var FixedField = myJqGrid.getFormField("Fixed");
        FixedField.unbind('change');
        FixedField.bind('change',
		function () {
		    var id = FixedField.val(); 
		    ChangeFixed(id);
		});
    }

    function ChangeFixed(id) {
        ajaxRequest(options.GetFixedUrl, {
            id: id
            },
			false,
			function (response) {
			    if (!!response.Result) {
			        $("#BarCode").val(response.Data.BarCode);
			        $("#Fcode").val(response.Data.Fcode);
			        $("#Fname").val(response.Data.Fname);
			        $("#FixedID").val(id); 
			        $("#BrevityCode").val(response.Data.BrevityCode);
			        $("#DepartMent").val(response.Data.DepartMent);
			        
			    } else {
			        showMessage('提示', response.Message);
			    }
			});
    }
}