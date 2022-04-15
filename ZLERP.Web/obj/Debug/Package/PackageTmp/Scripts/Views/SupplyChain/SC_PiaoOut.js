function SC_PiaoOutIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'SC_PiaoOutGrid',
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
        storeCondition: "",
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '出库单号',
		    name: 'OutNo',
		    index: 'OutNo',
		    width: 120
		},
		{
		    label: '仓库ID',
		    name: 'LibID',
		    index: 'LibID',
		    width: 80,
		    hidden: true
		},
        {
            label: '仓库',
            name: 'SC_Lib.LibName',
            index: 'SC_Lib.LibName',
            width: 80
        },
		{
		    label: '出库方式',
		    name: 'OutType',
		    index: 'OutType',
		    width: 80
		},
		{
		    label: '日期',
		    name: 'OutDate',
		    index: 'OutDate',
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
		    label: '出库金额',
		    name: 'OutMoney',
		    index: 'OutMoney',
		    width: 80,
		    formatter: 'currency'
		},
		{
		    label: '状态',
		    name: 'Condition',
		    index: 'Condition',
		    width: 50
		},
        {
            label: '申领单号',
            name: 'SC_PiaoOutOrder.OrderNo',
            index: 'SC_PiaoOutOrder.OrderNo',
            width: 120
        },
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
		{
		    label: '单据号',
		    name: 'PiaoNo',
		    index: 'PiaoNo',
		    width: 100
		},
        {
            label: '资产维修单号',
            name: 'SC_Fixed_Maintain.MaintainNo',
            index: 'SC_Fixed_Maintain.MaintainNo',
            width: 120
        },
        {
            label: '车辆维修单号',
            name: 'CarMaintain.MaintainNo',
            index: 'CarMaintain.MaintainNo',
            width: 120
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
				            loadFrom: 'SC_PiaoOutForm',
				            btn: btn,
				            afterFormLoaded: function () {
				                UserChange();
				                $("#SC_PiaoOut_OutNo").val(response.Data);
				                $("#td_SC_PiaoOut_OutType").show();
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
                var Condition = Record.Condition;
                var OutType = Record.OutType;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能修改!");
                    return;
                }
                if (OutType == "申领出库") {
                    showMessage('提示', "申领出库的单据只能修改明细!");
                    return;
                }
                myJqGrid.handleEdit({
                    loadFrom: 'SC_PiaoOutForm',
                    btn: btn,
                    prefix: "SC_PiaoOut",
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
            ShowPurIn: function (btn) {
                myJqGrid.refreshGrid("OutType='申领出库'");
            },
            ShowPurOut: function (btn) {
                myJqGrid.refreshGrid("OutType='申领退回'");
            },
            ShowOther: function (btn) {
                myJqGrid.refreshGrid("OutType='其他出库'");
            },
            ShowCondtion: function (btn) {
                myJqGrid.refreshGrid("Condition='草稿'");
            },
            handlePurOut: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OutNo;
                var Condition = Record.Condition;
                var OutType = Record.OutType;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿下的单据才能审核出库!");
                    return;
                }
                showConfirm("确认信息", "此操作一旦点击不可撤回，将对库存，金额等产生影响，请确认？，出库单号：" + orderNo + "？",
				function () {
				    var requestURL = options.PurOutUrl;
				    ajaxRequest(requestURL, {
				        id: id
				    },
					false,
					function (response) {
					    if (response.Result) {
					        showMessage('提示', '操作成功！');
					        myJqGrid.refreshGrid();
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
                    var url = "/GridReport/DisplayReport.aspx?report=SC_PiaoOut&ID=" + key;
                    window.open(url, "_blank");
                },
                PrintDesign: function (btn) {
                    var url = "/GridReport/DesignReport.aspx?report=SC_PiaoOut";
                    window.open(url, "_blank");
                },
                PrintDirect: function (btn) {
                    var key = myJqGrid.getSelectedKey();
                    if (key <= 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var url = "/GridReport/PrintDirect.aspx?report=SC_PiaoOut&ID=" + key;
                    window.open(url, "_blank");
                }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'SC_ZhangOutGrid',
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
            label: '商品编码',
            name: 'SC_Goods.GoodsCode',
            index: 'SC_Goods.GoodsCode',
            width: 100
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
		    width: 60,
		    formatter: 'currency'
		},
		{
		    label: '金额',
		    name: 'OutMoney',
		    index: 'OutMoney',
		    width: 80,
		    formatter: 'currency'
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
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                console.log(id);
                var orderNo = Record.OrderNo;

                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能新增明细!");
                    return;
                }
                $("#SC_ZhangOut_OutNo").val(Record.ID);
                myJqGrid.handleAdd({
                    loadFrom: 'SC_ZhangOutForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            myJqGridTo.refreshGrid(); 
                        }
                    }
                });
            },
            handleEdit: function (btn) {
                var keysTo = myJqGridTo.getSelectedKeys();
                if (keysTo.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
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
                    loadFrom: 'SC_ZhangOutForm',
                    btn: btn,
                    prefix: "SC_ZhangOut",
                    afterFormLoaded: function () {

                        $('input[name="ID"]').val(GoodsID);

                        ajaxRequest(options.GetZhangUrl, {
                            id: GoodsID
                        },
			            false,
			            function (response) {
			                if (!!response.Result) {
			                    $("#SC_ZhangOut_Unit").find("option").remove();
			                    var length = response.Data.SC_GoodsUnits.length;
			                    for (var i = 0; i < length; i++) {
			                        if (!response.Data.IsAuxiliaryUnit) {
			                            if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                                break;
			                            }
			                        }
			                        $("#SC_ZhangOut_Unit").append(
                                    "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                                    + "</option>");
			                    }
			                    $("#SC_ZhangOut_Unit").val(Unit);
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
                var keysTo = myJqGridTo.getSelectedKeys();
                if (keysTo.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
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
                        myJqGridTo.refreshGrid();
                    }
                });
            }
        }
    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("OutNo=" + id);
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
 
	myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
	    myJqGrid.handleEdit({
	        loadFrom: 'SC_PiaoOutForm',
	        prefix: "SC_PiaoOut",
	        title: '查看详细', 
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
	GoodsChange = function () {
	    var GoodsIDField = myJqGrid.getFormField("ID");
	    GoodsIDField.unbind('change');
	    GoodsIDField.bind('change',
		function () {
		    var Goodsid = GoodsIDField.val();
		    $("#SC_ZhangOut_GoodsID").val(Goodsid);
		    $("#SC_ZhangOut_SC_Goods_GoodsName").val(Goodsid);
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
			                $("#SC_ZhangOut_Unit").find("option").remove();
			                var length = response.Data.SC_GoodsUnits.length;
			                for (var i = 0; i < length; i++) {
			                    if (!response.Data.IsAuxiliaryUnit) {
			                        if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                            break;
			                        }
			                    }
			                    $("#SC_ZhangOut_Unit").append(
                                "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                                + "</option>");

			                }
			                $("#SC_ZhangOut_UnitRate").val(1);
			                $("#SC_ZhangOut_PriceUnit").val(response.Data.LibPrice);
			                $("#SC_ZhangOut_SC_Goods_Spec").val(response.Data.Spec);
			                GetMoneyFun();

			            } else {
			                showMessage('提示', response.Message);
			            }
			        });
        }
            
    UserChange = function () {
        var UserIDField = myJqGrid.getFormField("SC_PiaoOut.UserID");
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
			            $("#SC_PiaoOut_Department_DepartmentName").val(response.Data.DepartmentName);
			            $("#SC_PiaoOut_DepartmentID").val(response.Data.ID);
			        } else {
			            showMessage('提示', response.Message);
			        }
			    });
		    });
    }
    $("#SC_ZhangOut_Quantity").change(function () {
        GetMoneyFun();
    });
    function GetMoneyFun() {
        var UnitPrice = $("#SC_ZhangOut_PriceUnit").val();
        var Quantity = $("#SC_ZhangOut_Quantity").val();
        var money = UnitPrice * Quantity;
        $("#SC_ZhangOut_OutMoney").val(money)
    }

    //单位对应
    $("#SC_ZhangOut_Unit").change(function () {
        var GoodsIDField = $('input[name="ID"]').eq(1);
        if (!isEmpty(GoodsIDField)) {
            var unit = $("#SC_ZhangOut_Unit").val();
            var requestURL = '/SC_PiaoIn.mvc/GetUnitRate';
            var postParams = { goodsId: GoodsIDField.val(), unit: unit };
            var rData;
            ajaxRequest(requestURL, postParams, false, function (response) {
                if (response) {

                    $("#SC_ZhangOut_PriceUnit").val(response.LibPrice);
                    $("#SC_ZhangOut_UnitRate").val(response.rate);
                    GetMoneyFun();
                }
            });
        }
    });
    //设置日期列为日期范围搜索
    myJqGrid.setRangeSearch("OutDate");
}