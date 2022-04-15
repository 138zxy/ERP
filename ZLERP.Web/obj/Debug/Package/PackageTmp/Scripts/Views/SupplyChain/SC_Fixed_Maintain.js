function SC_Fixed_MaintainIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: 330
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
            , { label: '流水号', name: 'MaintainNo', index: 'MaintainNo', width: 120 }
            , { label: '资产编号', name: 'Fcode', index: 'Fcode', width: 80 }
            , { label: '资产名称', name: 'Fname', index: 'Fname', width: 80 }
            , { label: '资产条形码', name: 'BarCode', index: 'BarCode', width: 80 }
            , { label: '资产拼音简码', name: 'BrevityCode', index: 'BrevityCode', width: 80 }
            , { label: '资产所属部门', name: 'DepartMent', index: 'DepartMent', width: 80 }
            , { label: '维修类型', name: 'RepairType', index: 'RepairType', width: 80 }
            , { label: '维修方式', name: 'RepairWay', index: 'RepairWay', width: 80 }
            , { label: '送修时间', name: 'RepairDate', index: 'RepairDate', width: 80, formatter: 'date' }
            , { label: '送修人员', name: 'GiveMan', index: 'GiveMan', width: 80 }
            , { label: '维修地点', name: 'RepairAdress', index: 'RepairAdress', width: 120 }
            , { label: '故障描述', name: 'FaultDesc', index: 'FaultDesc', width: 200 }
            , { label: '是否完修', name: 'IsOver', index: 'IsOver', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '完修时间', name: 'OverDate', index: 'OverDate', width: 80, formatter: 'date' }
            , { label: '维修人员', name: 'RepairMan', index: 'RepairMan', width: 80 }
            , { label: '维修费用(元)', name: 'RepairPirce', index: 'RepairPirce', width: 80 }
            , { label: '维修时长(h)', name: 'RepairTime', index: 'RepairTime', width: 80 }
            , { label: '修理描述', name: 'RepairDesc', index: 'RepairDesc', width: 200 }
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
				            height: 400,
				            afterFormLoaded: function () {
				                $("#MaintainNo").val(response.Data);
				                $("#HiddenIsOver").hide();
				                $("#HiddenRepairPirce").hide();
				                $("#HiddenRepairDesc").hide();
				                $("#HiddenRepairMan").hide(); 
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
                    height: 500,
                    afterFormLoaded: function () {
                        $("#IsOver").attr('checked', true);
                        $("#HiddenIsOver").show();
                        $("#HiddenRepairPirce").show();
                        $("#HiddenRepairDesc").show();
                        $("#HiddenRepairMan").show(); 
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
				            height: 500,
				            afterFormLoaded: function () {
				                $("#MaintainNo").val(response.Data);
				                $("#HiddenIsOver").show();
				                $("#HiddenRepairPirce").show();
				                $("#HiddenRepairDesc").show();
				                $("#HiddenRepairMan").show(); 
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
            },
            handleOut: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.MaintainNo;
                showConfirm("确认信息", "维修领料出库将会生成一条出库单，请在出库单中完成出库明细，维修单：" + orderNo + "？",
				function () {
				    var requestURL = btn.data.Url;
				    ajaxRequest(requestURL, { 
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！'); 
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				})
            },
            handleBack: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.MaintainNo;
                showConfirm("确认信息", "维修剩料退还将会生成一条出库单，请在出库单中完成出库明细，维修单：" + orderNo + "？",
				function () {
				    var requestURL = btn.data.Url;
				    ajaxRequest(requestURL, {
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！');
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				})
            } 
        }
    });

    var myJqGridTo = new MyGrid({
        renderTo: 'MyJqGridTo',
        title: '维修消耗明细',
        autoWidth: true,
        buttons: buttons1,
        height: 150,
        storeURL: options.GetOutDel,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '订单号',
		    name: 'OutNo',
		    index: 'OutNo',
		    width: 100,
		    hidden: true
		},
		{
		    label: '商品',
		    name: 'GoodsID',
		    index: 'GoodsID',
		    width: 100,
		    hidden: true
		},
        {
        	label: '出库单号',
        	name: 'SC_PiaoOut.OutNo',
        	index: 'SC_PiaoOut.OutNo',
        	width: 80
        },
		{
		    label: '品名',
		    name: 'SC_Goods.GoodsName',
		    index: 'SC_Goods.GoodsName',
		    width: 80
		},
		{
		    label: '分类',
		    name: 'SC_Goods.SC_GoodsType.TypeName',
		    index: 'SC_Goods.SC_GoodsType.TypeName',
		    width: 80
		},
		{
		    label: '规格',
		    name: 'SC_Goods.Spec',
		    index: 'SC_Goods.Spec',
		    width: 80
		},
		{
		    label: '数量',
		    name: 'Quantity',
		    index: 'Quantity',
		    width: 60
		},
		{
		    label: '单位',
		    name: 'Unit',
		    index: 'Unit',
		    width: 50
		},
        {
            label: '辅助数量',
            name: 'AuxiliaryUnit',
            index: 'AuxiliaryUnit',
            width: 100
        }, 
		{
		    label: '单价',
		    name: 'PriceUnit',
		    index: 'PriceUnit',
		    width: 60
		},
		{
		    label: '金额',
		    name: 'OutMoney',
		    index: 'OutMoney',
		    width: 80
		},  
		{
		    label: '批号',
		    name: 'PiNo',
		    index: 'PiNo',
		    width: 80
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
		{
		    label: '单位转换比率',
		    name: 'UnitRate',
		    index: 'UnitRate',
		    width: 80
		}] 
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            } 
        }
    });
    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("SC_PiaoOut.MaintainID=" + id + " and SC_PiaoOut.Condition='已出库'");
	}); 
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看维修详细',
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