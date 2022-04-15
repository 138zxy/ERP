function SC_PiaoInOrderIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'SC_PiaoInOrderGrid',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight * 0.7 - 100,
        storeURL: options.storeUrl,
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
		    name: 'OrderNo',
		    index: 'OrderNo',
		    width: 130
		},
		{
		    label: '供应商ID',
		    name: 'SupplierID',
		    index: 'SupplierID',
		    width: 100,
		    hidden: true
		},
		{
		    label: '仓库ID',
		    name: 'LibID',
		    index: 'LibID',
		    width: 100,
		    hidden: true
		},
		{
		    label: '供应商',
		    name: 'SC_Supply.SupplierName',
		    index: 'SC_Supply.SupplierName',
		    width: 80
		},
		{
		    label: '日期',
		    name: 'PiaoDate',
		    index: 'PiaoDate',
		    formatter: 'date',
		    width: 80
		},
		{
		    label: '交货日期',
		    name: 'DeliveryDate',
		    index: 'DeliveryDate',
		    formatter: 'date',
		    width: 80
		},
		{
		    label: '经办人',
		    name: 'Operator',
		    index: 'Operator',
		    width: 80
		},
		{
		    label: '品种数',
		    name: 'VarietyNum',
		    index: 'VarietyNum',
		    width: 60
		},
		{
		    label: '订单金额',
		    name: 'OrderMoney',
		    index: 'OrderMoney',
		    width: 80,
		    formatter: 'currency'
		},
		{
		    label: '仓库',
		    name: 'SC_Lib.LibName',
		    index: 'SC_Lib.LibName',
		    width: 80
		},
		{
		    label: '状态',
		    name: 'Condition',
		    index: 'Condition',
		    width: 60
		},
		{
		    label: '单据号',
		    name: 'PiaoNo',
		    index: 'PiaoNo',
		    width: 100
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
		{
		    label: '审核人',
		    name: 'Auditor',
		    index: 'Auditor',
		    width: 80
		},
		{
		    label: '审核时间',
		    name: 'AuditTime',
		    index: 'AuditTime',
		    formatter: 'datetime',
		    width: 120
		}],
        autoLoad: true,
        functions: {
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
				        console.log(response.Data);
				        myJqGrid.handleAdd({
				            loadFrom: 'SC_PiaoInOrderForm',
				            btn: btn,
				            width: 500,
				            height: 300,
				            afterFormLoaded: function () {
				                $("#SC_PiaoInOrder_OrderNo").val(response.Data);
				            }
				        });
				    }
				});

            },
            handleEdit: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能修改!");
                    return;
                }
                myJqGrid.handleEdit({
                    loadFrom: 'SC_PiaoInOrderForm',
                    btn: btn,
                    prefix: "SC_PiaoInOrder"
                });
            },
            handleDelete: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能删除!");
                    return;
                }
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            handleAuditing: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();

                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能审核!");
                    return;
                }
                showConfirm("确认信息", "您确定要审核当前采购单:" + orderNo + "？",
				function () {
				    var requestURL = options.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 1,
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！');
					        myJqGrid.refreshGrid();
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				})
            },
            handleUnAuditing: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();

                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "已审核") {
                    showMessage('提示', "只有已审核状态下的单据才能反审!");
                    return;
                }
                showConfirm("确认信息", "您确定要反审当前采购单:" + orderNo + "？",
				function () {
				    var requestURL = options.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 2,
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！');
					        myJqGrid.refreshGrid();
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				})
            },
            handleOver: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();

                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "已审核") {
                    showMessage('提示', "只有已审核状态下的单据才能标记完成!");
                    return;
                }
                showConfirm("确认信息", "您确定要标记已完成当前采购单:" + orderNo + "？",
				function () {
				    var requestURL = options.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 3,
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！');
					        myJqGrid.refreshGrid();
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				})
            },
            handleUnOver: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "已完成") {
                    showMessage('提示', "只有已完成状态下的单据才能反完成!");
                    return;
                }
                showConfirm("确认信息", "您确定要反完成当前采购单:" + orderNo + "？",
				function () {
				    var requestURL = options.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 4,
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！');
					        myJqGrid.refreshGrid();
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				})
            },
            handlePurIn: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "已审核") {
                    showMessage('提示', "只有已审核下的单据才能操作入库!");
                    return;
                }
                showConfirm("确认信息", "我们将对未入库的采购数量产生入库单，请根据实际情况修改入库单数量及单价，并完成审核入库，采购单：" + orderNo + "？",
				function () {
				    var requestURL = options.PurInUrl;
				    ajaxRequest(requestURL, {
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功,请到入库单操作！');
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				})
            },
            print: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                // var url = "/GridReport/DisplayReport.aspx?report=SC_PiaoInOrder&data=GetPiaoInOrder&key=" + key;
                var url = "/GridReport/DisplayReport.aspx?report=SC_PiaoInOrder&ID=" + key;
                window.open(url, "_blank");
            },
            PrintDesign: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=SC_PiaoInOrder";
                window.open(url, "_blank");
            },
            PrintDirect: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/PrintDirect.aspx?report=SC_PiaoInOrder&ID=" + key;
                window.open(url, "_blank");
            }

        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'SC_ZhangInOrderGrid',
        autoWidth: true,
        buttons: buttons1,
        height: gGridHeight * 0.3,
        storeURL: options.ZhangstoreUrl,
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
		    name: 'OrderNo',
		    index: 'OrderNo',
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
        	label: '商品编码',
        	name: 'SC_Goods.GoodsCode',
        	index: 'SC_Goods.GoodsCode',
        	width: 100
        },
		{
		    label: '品名',
		    name: 'SC_Goods.GoodsName',
		    index: 'SC_Goods.GoodsName',
		    width: 100
		},
		{
		    label: '分类',
		    name: 'SC_Goods.SC_GoodsType.TypeName',
		    index: 'SC_Goods.SC_GoodsType.TypeName',
		    width: 100
		},
		{
		    label: '规格',
		    name: 'SC_Goods.Spec',
		    index: 'SC_Goods.Spec',
		    width: 100
		},
		{
		    label: '数量',
		    name: 'Quantity',
		    index: 'Quantity',
		    width: 100
		},
		{
		    label: '单位',
		    name: 'Unit',
		    index: 'Unit',
		    width: 100
		},
        {
            label: '辅助数量',
            name: 'AuxiliaryUnit',
            index: 'AuxiliaryUnit',
            width: 100
        }, 
		{
		    label: '单价',
		    name: 'UnitPrice',
		    index: 'UnitPrice',
		    width: 100,
		    formatter: 'currency'
		},
		{
		    label: '金额',
		    name: 'ZhangMoney',
		    index: 'ZhangMoney',
		    width: 100,
		    formatter: 'currency'
		},
		{
		    label: '已入数量',
		    name: 'InQuantity',
		    index: 'InQuantity',
		    width: 100
		}, 
		{
		    label: '未入数量',
		    name: 'UnQuantity',
		    index: 'UnQuantity',
		    width: 100
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
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            },
            handleAdd: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                
                var Record = myJqGrid.getSelectedRecord();
                 
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能新增明细!");
                    return;
                }
                $("#SC_ZhangInOrder_OrderNo").val(Record.ID);
                myJqGrid.handleAdd({
                    loadFrom: 'SC_ZhangInOrderForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                        myJqGridTo.refreshGrid();
                    }
                });
            },
            handleEdit: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                }
                var keysTo = myJqGridTo.getSelectedKeys();
                if (keysTo.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var RecordTo = myJqGridTo.getSelectedRecord();
                var GoodsID = RecordTo.GoodsID;
                var Unit = RecordTo.Unit;
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能修改明细!");
                    return;
                }
                myJqGridTo.handleEdit({
                    loadFrom: 'SC_ZhangInOrderForm',
                    btn: btn,
                    prefix: "SC_ZhangInOrder",
                    afterFormLoaded: function () { 
                        $('input[name="ID"]').val(GoodsID); 
                        ajaxRequest(options.GetZhangUrl, {
                            id: GoodsID
                        },
			            false,
			            function (response) {
			                if (!!response.Result) {
			                    $("#SC_ZhangInOrder_Unit").find("option").remove();
			                    var length = response.Data.SC_GoodsUnits.length;
			                    for (var i = 0; i < length; i++) {
			                        if (!response.Data.IsAuxiliaryUnit) {
			                            if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                                break;
			                            }
			                        }
			                        $("#SC_ZhangInOrder_Unit").append(
                                    "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                                    + "</option>");
			                    }
			                    
			                    $("#SC_ZhangInOrder_Unit").val(Unit);
			                } else {
			                    showMessage('提示', response.Message);
			                }
			            });
                        GoodsChange();
                    },
                    postCallBack: function (response) { 
                        myJqGridTo.refreshGrid();
                    }
                });
            },
            handleDelete: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var keysTo = myJqGridTo.getSelectedKeys();
                if (keysTo.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能删除明细!");
                    return;
                }
                myJqGridTo.deleteRecord({
                    deleteUrl: btn.data.Url,
                    postCallBack: function (response) {
                        myJqGrid.refreshGrid();
                    }
                });
            }
        }
    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("OrderNo=" + id);
	});
	myJqGrid.addListeners("gridComplete", function () {
	    var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
	    for (var i = 0; i < ids.length; i++) {
	        var cl = ids[i];
	        var record = myJqGrid.getRecordByKeyValue(ids[i]);
	        if (record.Condition == "草稿") {
	            myJqGrid.getJqGrid().setCell(cl, "Condition", '', { color: 'red' }, '');
	        }
	    }
	});
    //下拉选择商品的时候
    GoodsChange = function () {

        var GoodsIDField = myJqGrid.getFormField("ID");
        GoodsIDField.unbind('change');
        GoodsIDField.bind('change',
		function () {
		    var Goodsid = GoodsIDField.val();
		    $("#SC_ZhangInOrder_GoodsID").val(Goodsid);
		    $("#SC_ZhangInOrder_SC_Goods_GoodsName").val(Goodsid);
		    ChangeGood(Goodsid);
		});
    }


    function ChangeGood(Goodsid) {
        ajaxRequest(options.GetZhangUrl, {
            id: Goodsid
        },
			    false,
			    function (response) {
			        if (!!response.Result) {
			            $("#SC_ZhangInOrder_Unit").find("option").remove();
			            var length = response.Data.SC_GoodsUnits.length;
			            for (var i = 0; i < length; i++) {
			                if (!response.Data.IsAuxiliaryUnit) {
			                    if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                        break;
			                    }
			                }
			                $("#SC_ZhangInOrder_Unit").append(
                            "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                            + "</option>");

			            }

			            $("#SC_ZhangInOrder_UnitRate").val(1);
			            $("#SC_ZhangInOrder_UnitPrice").val(response.Data.Price);
			            $("#SC_ZhangInOrder_SC_Goods_Spec").val(response.Data.Spec);
			            GetMoneyFun();

			        } else {
			            showMessage('提示', response.Message);
			        }
			    });
    }

    $("#SC_ZhangInOrder_UnitPrice").change(function () {
        GetMoneyFun();
    });
    $("#SC_ZhangInOrder_Quantity").change(function () {
        GetMoneyFun();
    });

    function GetMoneyFun() {
        var UnitPrice = $("#SC_ZhangInOrder_UnitPrice").val();
        var Quantity = $("#SC_ZhangInOrder_Quantity").val();
        var money = UnitPrice * Quantity;
        $("#SC_ZhangInOrder_ZhangMoney").val(money)
    }

    //单位对应
    $("#SC_ZhangInOrder_Unit").change(function () {
        //var GoodsIDField = $('input[name="ID"]');
        var GoodsIDField = myJqGrid.getFormField("ID");
        if (!isEmpty(GoodsIDField)) {
            var unit = $("#SC_ZhangInOrder_Unit").val();
            var requestURL = '/SC_PiaoIn.mvc/GetUnitRate';
            var postParams = { goodsId: GoodsIDField.val(), unit: unit };
            var rData;
            ajaxRequest(requestURL, postParams, false, function (response) {
                if (response) {
                    $("#SC_ZhangInOrder_UnitPrice").val(response.price);
                    $("#SC_ZhangInOrder_UnitRate").val(response.rate);
                    GetMoneyFun();
                }
            });
        }
    });


    //设置日期列为日期范围搜索
    myJqGrid.setRangeSearch("PiaoDate");
     

}