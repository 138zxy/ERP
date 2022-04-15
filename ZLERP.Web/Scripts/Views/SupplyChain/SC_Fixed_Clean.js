function sc_fixed_cleanIndexInit(storeUrl) {
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
            , dialogHeight: 520
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '流水号', name: 'CleanNo', index: 'CleanNo',width:100 }
                , { label: '资产编号', name: 'FixedID', index: 'FixedID', width: 50 }
                , { label: '资产代码', name: 'Fcode', index: 'Fcode', width: 60 }
                , { label: '资产名称', name: 'Fname', index: 'Fname', width: 60 }
                , { label: '清理日期', name: 'CleanDate', index: 'CleanDate', formatter: 'date', width: 80 }
                , { label: '清理方式', name: 'CleanType', index: 'CleanType', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '申报人', name: 'ApplicantMan', index: 'ApplicantMan', width: 60 }
                , { label: '批准人', name: 'ApproveMan', index: 'ApproveMan', width: 60 }
                , { label: '资产规格型号', name: 'Spec', index: 'Spec', width: 100 }
                , { label: '资产配置', name: 'Configure', index: 'Configure' }
                , { label: '保管员', name: 'Storeman', index: 'Storeman', width: 80 }
                , { label: '存放位置', name: 'Position', index: 'Position', width: 80 }
                , { label: '资产类型', name: 'Ftype', index: 'Ftype', width: 60 }
                , { label: '使用部门', name: 'DepartMent', index: 'DepartMent', width: 60 }
                , { label: '条形码', name: 'BarCode', index: 'BarCode', width: 70 }
                , { label: '拼音简码', name: 'BrevityCode', index: 'BrevityCode', width: 60 }
                , { label: '转售金额', name: 'ResalePrice', index: 'ResalePrice', width: 60 }
                , { label: '资产价格', name: 'FixedPirce', index: 'FixedPirce', width: 60 }
                , { label: '资产增加日期', name: 'AddDate', index: 'AddDate', formatter: 'date', width: 90 }
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
                    ajaxRequest('/SC_Fixed_Clean.mvc/GenerateOrderNo', {},
				        false,
				        function (response) {
				            if (response.Result) {
				                console.log(response.Data);
				                myJqGrid.handleAdd({
				                    loadFrom: 'MyFormDiv',
				                    btn: btn,
				                    afterFormLoaded: function () {
				                        FixedChange();
				                        $("#CleanNo").val(response.Data);
				                    },
				                    postCallBack: function (response) {

				                    }
				                });
				            }
				        });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            FixedChange();
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
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细',
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