function SC_PiaoOutOrderIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'SC_PiaoOutOrderGrid',
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
		    width: 120
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
		    name: 'UseDate',
		    index: 'UseDate',
		    formatter: 'date',
		    width: 80
		},
		{
		    label: '使用人',
		    name: 'UserID',
		    index: 'UserID',
		    width: 80
		},
		{
		    label: '使用部门',
		    name: 'Department.DepartmentName',
		    index: 'Department.DepartmentName',
		    width: 80
		},
		{
		    label: '品种数',
		    name: 'VarietyNum',
		    index: 'VarietyNum',
		    width: 60
		}, 
		{
		    label: '状态',
		    name: 'Condition',
		    index: 'Condition',
		    width: 60
		},
		{
		    label: '申请人',
		    name: 'Builder',
		    index: 'Builder',
		    width: 80
		},
		{
		    label: '申请时间',
		    name: 'BuildTime',
		    index: 'BuildTime',
		    formatter: 'datetime',
		    width: 120
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
		    width: 60
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
				            loadFrom: 'SC_PiaoOutOrderForm',
				            btn: btn,
				            afterFormLoaded: function () {
				                $("#SC_PiaoOutOrder_OrderNo").val(response.Data);
				                UserChange();
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
                    loadFrom: 'SC_PiaoOutOrderForm',
                    btn: btn,
                    prefix: "SC_PiaoOutOrder",
                    afterFormLoaded: function () {
                        UserChange();
                    }
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
            handleToFlow: function (btn, e) {
                var id = myJqGrid.getSelectedKey();
                if (id) {
                    ajaxRequest(
                            "/User.mvc/AddWorkFlow",
                            { id: id ,flowid:"ab76cb73-200f-4562-8be7-7058c7fc3e17", stepid:"6af6e593-752f-42d5-ab58-b8974927546a", stepname:"填表", title:"ERP物资采购申请"},
                            true,
                            function () {
                                myJqGrid.reloadGrid();
                            }
                        );
                }
                else {
                }
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
                showConfirm("确认信息", "您确定要审核当前申领单:" + orderNo + "？",
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
                    showMessage('提示', "只有已审核下的单据才能操作出库!");
                    return;
                }
                $("#MyGoodsShowDiv").dialog("open");
            },
            print: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/DisplayReport.aspx?report=SC_PiaoOutOrder&ID=" + key;
                window.open(url, "_blank");
            },
            PrintDesign: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=SC_PiaoOutOrder";
                window.open(url, "_blank");
            },
            PrintDirect: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/PrintDirect.aspx?report=SC_PiaoOutOrder&ID=" + key;
                window.open(url, "_blank");
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'SC_ZhangOutOrderGrid',
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
		    label: '已领数量',
		    name: 'InQuantity',
		    index: 'InQuantity',
		    width: 100
		},
		{
		    label: '未领数量',
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
                $("#SC_ZhangOutOrder_OrderNo").val(Record.ID);
                myJqGrid.handleAdd({
                    loadFrom: 'SC_ZhangOutOrderForm',
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
                    showMessage('提示', "请选择需要的单据!");
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
                    loadFrom: 'SC_ZhangOutOrderForm',
                    btn: btn,
                    prefix: "SC_ZhangOutOrder",
                    afterFormLoaded: function () {
                        $('input[name="ID"]').val(GoodsID);
                        ajaxRequest(options.GetZhangUrl, {
                            id: GoodsID
                        },
			            false,
			            function (response) {
			                if (!!response.Result) {
			                    $("#SC_ZhangOutOrder_Unit").find("option").remove();
			                    var length = response.Data.SC_GoodsUnits.length;
			                    for (var i = 0; i < length; i++) {
			                        if (!response.Data.IsAuxiliaryUnit) {
			                            if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                                break;
			                            }
			                        }
			                        $("#SC_ZhangOutOrder_Unit").append(
                                    "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                                    + "</option>");
			                    }
			                    $("#SC_ZhangOutOrder_SC_Goods_AllNum").val(response.Data.AllNum);
			                    $("#SC_ZhangOutOrder_Unit").val(Unit);
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
		    console.log("GoodsId = " + Goodsid);
		    $("#SC_ZhangOutOrder_GoodsID").val(Goodsid);
		    $("#SC_ZhangOutOrder_SC_Goods_GoodsName").val(Goodsid);
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
			                    $("#SC_ZhangOutOrder_Unit").find("option").remove();
			                    var length = response.Data.SC_GoodsUnits.length;
			                    for (var i = 0; i < length; i++) {
			                        if (!response.Data.IsAuxiliaryUnit) {
			                            if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                                break;
			                            }
			                        }
			                        $("#SC_ZhangOutOrder_Unit").append(
                                    "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                                    + "</option>");

			                    } 
			                    $("#SC_ZhangOutOrder_UnitRate").val(1); 
			                    $("#SC_ZhangOutOrder_SC_Goods_Spec").val(response.Data.Spec);
			                    $("#SC_ZhangOutOrder_SC_Goods_AllNum").val(response.Data.AllNum);
			                     

			                } else {
			                    showMessage('提示', response.Message);
			                }
			            });
        }


    UserChange = function () {
        var UserIDField = myJqGrid.getFormField("SC_PiaoOutOrder.UserID");
        console.log(UserIDField);
        UserIDField.unbind('change');
        UserIDField.bind('change',
		function () {
		    var userid = UserIDField.val();
		    console.log(userid);
		    ajaxRequest(options.GetDepartUrl, {
		        userid: userid
		    },
			false,
			function (response) {
			    if (!!response.Result) {
			        $("#SC_PiaoOutOrder_Department_DepartmentName").val(response.Data.DepartmentName);
			        $("#SC_PiaoOutOrder_DepartmentID").val(response.Data.ID); 
			    } else {
			        showMessage('提示', response.Message);
			    }
			});
		});
    }

    
    //单位对应
    $("#SC_ZhangOutOrder_Unit").change(function () {
        var GoodsIDField = $('input[name="ID"]').eq(1);
        if (!isEmpty(GoodsIDField)) {
            var unit = $("#SC_ZhangOutOrder_Unit").val();
            var requestURL = '/SC_PiaoIn.mvc/GetUnitRate';
            var postParams = { goodsId: GoodsIDField.val(), unit: unit };
            var rData;
            ajaxRequest(requestURL, postParams, false, function (response) {
                if (response) {
                    $("#SC_ZhangOutOrder_UnitRate").val(response.rate);
                }
            });
        }
    });
    $("#MyGoodsShowDiv").dialog({
        modal: true,
        autoOpen: false,
        width: 500,
        Height: 500,
        Title: "选择出库的仓库",
        buttons: {
            '确认': function () {
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OrderNo;
                var libid = $("input[type='radio']:checked").val();
                if (libid <= 0) {
                    showMessage('提示', "请选择出库的仓库!");
                    return;
                }
                ajaxRequest(options.IsCanOut, {
                    id: id,
                    libid: libid
                },
				false,
				function (response) {
				    
				    if (response.Result) {
				        var Message = "";
				        if (response.Data == 1) {
				            Message = "当前生成的商品只有部分有库存，只能生成部分出库明细，你确定继续操作操作？";
				        } else {
				            Message = "我们将会生成对应的出库单，你确定继续操作操作？ ";
				        }
				        showConfirm("确认信息", Message + "申请单号：" + orderNo,
						function () {
						    var requestURL = options.PurOutUrl;
						    ajaxRequest(requestURL, {
						        id: id,
						        libid: libid
						    },
							false,
							function (response) {
							    if (!!response.Result) {
							        showMessage('提示', '操作成功,请到出库单操作！');
							    } else {
							        showMessage('提示', response.Message);
							    }
							})
						})

				    } else {
				        showMessage('提示', response.Message);
				    }
				})

                $(this).dialog('close');

            },
            '取消': function () {
                $(this).dialog('close');
            }
        },
        position: ["center", 100]
    });
    //设置日期列为日期范围搜索
    myJqGrid.setRangeSearch("PiaoDate");
}