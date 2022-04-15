function sc_fixed_shiftIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 500
            , dialogHeight: 500
		    , initArray: [
                { label: '转移编号', name: 'ID', index: 'ID', width: 60, hidden: true }
                , { label: '流水号', name: 'ShiftNo', index: 'ShiftNo', width: 100 }
                , { label: '资产编号', name: 'FixedID', index: 'FixedID', width: 70 }
                , { label: '资产编码', name: 'Fcode', index: 'Fcode', width: 70 }
                , { label: '资产名称', name: 'Fname', index: 'Fname', width: 70 }
                , { label: '原使用部门', name: 'DepartMent', index: 'DepartMent', width: 90 }
                , { label: '原保管员', name: 'Storeman', index: 'Storeman', width: 80 }
                , { label: '原存放位置', name: 'Position', index: 'Position', width: 100 }
                , { label: '转移日期', name: 'ShiftDate', index: 'ShiftDate', formatter: 'date', width: 70 }
                , { label: '转移人', name: 'ShiftMan', index: 'ShiftMan', width: 70 }
                , { label: '新保管员', name: 'StoremanNew', index: 'StoremanNew', width: 70 }
                , { label: '新存放位置', name: 'PositionNew', index: 'PositionNew', width: 90 }
                , { label: '批准人', name: 'ApproveMan', index: 'ApproveMan', width: 70 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '资产条形码', name: 'BarCode', index: 'BarCode', width: 70 }
                , { label: '资产拼音简码', name: 'BrevityCode', index: 'BrevityCode', width: 110 }
                , { label: '新使用部门', name: 'DepartMentNew', index: 'DepartMentNew', width: 70 }
                , { label: '原分类号', name: 'Ftype', index: 'Ftype', width: 90,hidden:true }
                , { label: '原分类', name: 'FTypeName', index: 'SC_GoodsType.TypeName', width: 90 }
                , { label: '新分类号', name: 'FtypeNew', index: 'FtypeNew', width: 90, hidden: true }
                , { label: '新分类', name: 'FTypeNameNew', index: 'SC_GoodsTypeNew.TypeName', width: 90 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    ajaxRequest('/SC_Fixed_Shift.mvc/GenerateOrderNo', {},
				        false,
				        function (response) {
				            if (response.Result) {
				                console.log(response.Data);
				                
				                myJqGrid.handleAdd({
				                    loadFrom: 'MyFormDiv',
				                    btn: btn,
				                    afterFormLoaded: function () {
				                        FixedChange();
				                        $("#ShiftNo").val(response.Data);
				                        
				                    },
				                    postCallBack: function (response) {

				                    }
				                });
				            }
				        });
                },
				handleEdit: function (btn) {
				    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            FixedChange();
                            $("input[name='FtypeNew_text']").val(data.FTypeNameNew); 
                        },
                        postCallBack: function (response) {

                        }
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
            getdetial();
        });
        window.Show = function () {
            getdetial();
        }
        function getdetial() {
            myJqGrid.handleEdit({
                loadFrom: 'MyFormDiv',
                title: '查看固定资产转移信息',
                width: 500,
                height: 500,
                buttons: {
                    "关闭": function () {
                        $(this).dialog('close');
                    }
                },
                afterFormLoaded: function () {
                }
            });
        }

        //下拉选择商品的时候
        FixedChange = function () {
            var FixedIDField = $('input[name="FixedID"]'); //myJqGrid.getFormField("ID"); alert(GoodsIDField.val()); ID
            FixedIDField.unbind('change');
            FixedIDField.bind('change',
	        function () {
	            var fixedId = FixedIDField.val();
	            var requestURL = "/SC_Fixed.mvc/Get";
	            ajaxRequest(requestURL, {
	                id: fixedId
	            },
				    false,
				    function (response) {
				        if (!!response.Result) {
				            var data = response.Data;
				            $("#Fcode").val(data.Fcode);
				            $("#Fname").val(data.Fname);
				            $("#BrevityCode").val(data.BrevityCode);
				            $("#Ftype").val(data.Ftype);
				            $("#Spec").val(data.Spec);
				            $("#DepartMent").val(data.DepartMent);

				            var nadate = dataTimeFormat(data.AddDate, "Y-m-d")
				            $("#AddDate").val(nadate);

				            $("#FixedPirce").val(data.FixedPirce);
				            $("#Position").val(data.Position);
				            $("#Configure").val(data.Configure);
				            $("#Storeman").val(data.Storeman);
				            $("#BarCode").val(data.BarCode);
				        } else {
				            showMessage('提示', response.Message);
				        }
				    });

	        });
        } 
}