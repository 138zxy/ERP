function SC_PiaoInIndexInit(options) {
 
    var myJqGrid = new MyGrid({
        renderTo: 'SC_PiaoInGrid',
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
        storeCondition: "(InType<>'报损' and InType<>'报溢')",
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80, 
            hidden: false
        },
		{
		    label: '入库单号',
		    name: 'InNo',
		    index: 'InNo',
		    width: 130
		},
		{
		    label: '供应商ID',
		    name: 'SupplierID',
		    index: 'SupplierID',
		    width: 80,
		    hidden: true
		},
        {
            label: '原欠款额',
            name: 'SC_Supply.PayMoney',
            index: 'SC_Supply.PayMoney',
            width: 80,
            hidden: true
        },
		{
		    label: '仓库ID',
		    name: 'LibID',
		    index: 'LibID',
		    width: 60,
		    hidden: true
		},
        {
            label: '供应商',
            name: 'SC_Supply.SupplierName',
            index: 'SC_Supply.SupplierName',
            width: 80
        },
        {
            label: '仓库',
            name: 'SC_Lib.LibName',
            index: 'SC_Lib.LibName',
            width: 60
        },
		{
		    label: '入库方式',
		    name: 'InType',
		    index: 'InType',
		    width: 80
		},
		{
		    label: '日期',
		    name: 'InDate',
		    index: 'InDate',
		    formatter: 'date',
		    width: 80
		},
		{
		    label: '采购员',
		    name: 'Purchase',
		    index: 'Purchase',
		    width: 60
		},
		{
		    label: '品种数',
		    name: 'VarietyNum',
		    index: 'VarietyNum',
		    width: 60
		},
		{
		    label: '入库金额',
		    name: 'InMoney',
		    index: 'InMoney',
		    width: 60,
		    formatter: 'currency'
		},
		{
		    label: '状态',
		    name: 'Condition',
		    index: 'Condition',
		    width: 40
		},
		{
		    label: '单据号',
		    name: 'PiaoNo',
		    index: 'PiaoNo',
		    width: 80
		},
        {
            label: '采购单号',
            name: 'SC_PiaoInOrder.OrderNo',
            index: 'SC_PiaoInOrder.OrderNo',
            width: 130
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
				            loadFrom: 'SC_PiaoInForm',
				            btn: btn,
				            width: 500,
				            height: 300,
				            afterFormLoaded: function () {
				                $("#SC_PiaoIn_InNo").val(response.Data);
				                $("#td_SC_PiaoIn_InType").show();
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
                var InType = Record.InType
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能修改!");
                    return;
                }
                if (InType == "库存初始化") {
                    showMessage('提示', "库存初始化的单据只能修改明细!");
                    return;
                }
                console.log(InType);
                myJqGrid.handleEdit({
                    loadFrom: 'SC_PiaoInForm',
                    btn: btn,
                    width: 500,
                    height: 300,
                    prefix: "SC_PiaoIn",
                    afterFormLoaded: function () {
                        $("#td_SC_PiaoIn_InType").hide();
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
                myJqGrid.refreshGrid("InType='采购入库'");
            },
            ShowPurOut: function (btn) {
                myJqGrid.refreshGrid("InType='采购退货'");
            },
            ShowOther: function (btn) {
                myJqGrid.refreshGrid("InType='其他入库'");
            },
            ShowInit: function (btn) {
                myJqGrid.refreshGrid("InType='库存初始化'");
            },
            ShowCondtion: function (btn) {
                myJqGrid.refreshGrid("Condition='草稿'");
            },
            handlePurIn: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.InNo;
                var Condition = Record.Condition;
                var InType = Record.InType;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿下的单据才能审核入库!");
                    return;
                }
                if (InType == '库存初始化') {
                    showConfirm("确认信息", "此操作一旦点击不可撤回，将对库存，金额等产生影响，请确认？，入库单号：" + orderNo + "？",
				function () {
				    var requestURL = options.PurInUrl;
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
                else {
                    myJqGrid.handleEdit({
                        loadFrom: 'SC_PiaoInPayForm',
                        btn: btn,
                        width: 500,
                        height: 300,
                        prefix: "SC_PiaoIn",
                        afterFormLoaded: function () {
                            $("#SC_PiaoIn_PayInID").val(id);
                            var money = $("#SC_PiaoIn_InMoney").val();
                            $("#SC_PiaoIn_PayMoney").val(money);
                            $("#SC_PiaoIn_PayFavourable").val(0);
                            $("#SC_PiaoIn_PayOwing").val(0); 
                        }
                    });
                }
                myJqGrid.reloadGrid();
            },
            print: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                } 
                var url = "/GridReport/DisplayReport.aspx?report=SC_PiaoIn&ID=" + key;
                window.open(url, "_blank");
            },
            PrintDesign: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=SC_PiaoIn";
                window.open(url, "_blank");
            },
            PrintDirect: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/PrintDirect.aspx?report=SC_PiaoIn&ID=" + key;
                window.open(url, "_blank");
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'SC_ZhangInGrid',
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
            hidden: false
        },
		{
		    label: '订单号',
		    name: 'InNo',
		    index: 'InNo',
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
		    label: '单价',
		    name: 'PriceUnit',
		    index: 'PriceUnit',
		    width: 100,
		    formatter: 'currency'
		},
        {
		    label: '辅助数量',
		    name: 'AuxiliaryUnit',
		    index: 'AuxiliaryUnit',
		    width: 100
		}, 
		{
		    label: '金额',
		    name: 'InMoney',
		    index: 'InMoney',
		    width: 100,
		    formatter: 'currency'
		},
		{
		    label: '批号',
		    name: 'PiNo',
		    index: 'PiNo',
		    width: 100
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		}
        ,
		{
		    label: '单位转换比率',
		    name: 'UnitRate',
		    index: 'UnitRate',
		    width: 80
		}
        ],
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
                console.log(id);
                var orderNo = Record.OrderNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能新增明细!");
                    return;
                }
                $("#SC_ZhangIn_InNo").val(Record.ID);
                myJqGrid.handleAdd({
                    loadFrom: 'SC_ZhangInForm',
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
                console.log(Record);
                myJqGridTo.handleEdit({
                    loadFrom: 'SC_ZhangInForm',
                    btn: btn,
                    prefix: "SC_ZhangIn",
                    afterFormLoaded: function () {
                        $('input[name="ID"]').val(GoodsID);                       
                        ajaxRequest(options.GetZhangUrl, {
                            id: GoodsID
                        },
			            false,
			            function (response) {
			                if (!!response.Result) {
			                    $("#SC_ZhangIn_Unit").find("option").remove();
			                    var length = response.Data.SC_GoodsUnits.length;
			                    for (var i = 0; i < length; i++) {
			                        if (!response.Data.IsAuxiliaryUnit) {
			                            if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                                break;
			                            }
			                        }
			                        $("#SC_ZhangIn_Unit").append(
                                    "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                                    + "</option>");
			                    } 
			                    $("#SC_ZhangIn_Unit").val(Unit);
			                } else {
			                    showMessage('提示', response.Message);
			                }
			            });
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                    }
                });
            },
            handleDelete: function (btn) {
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
                        // myJqGrid.refreshGrid();
                        myJqGridTo.refreshGrid();
                    }
                });
            }
        }
    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("InNo=" + id);
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
	        loadFrom: 'SC_PiaoInForm',
	        prefix: "SC_PiaoIn",
	        title: '查看详细',
	        width: 500,
	        height: 300,
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
	    var GoodsIDField =myJqGrid.getFormField("ID");// alert(GoodsIDField.val()); ID
	   // $('input[name="ID"]'); //
	    GoodsIDField.unbind('change');
	    GoodsIDField.bind('change',
		function () {
		    var Goodsid = GoodsIDField.val(); 
		    $("#SC_ZhangIn_GoodsID").val(Goodsid);
		    $("#SC_ZhangIn_SC_Goods_GoodsName").val(Goodsid);
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
			    $("#SC_ZhangIn_Unit").find("option").remove();
			    var length = response.Data.SC_GoodsUnits.length;
			    for (var i = 0; i < length; i++) {
			        if (!response.Data.IsAuxiliaryUnit) {
			            if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
			                break;
			            }
			        }
			        $("#SC_ZhangIn_Unit").append(
                    "<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit
                    + "</option>");

			    }
			    $("#SC_ZhangIn_UnitRate").val(1);
			    $("#SC_ZhangIn_PriceUnit").val(response.Data.Price);
			    $("#SC_ZhangIn_SC_Goods_Spec").val(response.Data.Spec);
			    GetMoneyFun();

			} else {
			    showMessage('提示', response.Message);
			}
		});
    }
            
    $("#SC_ZhangIn_PriceUnit").change(function () {
        GetMoneyFun();
    });
    $("#SC_ZhangIn_Quantity").change(function () {
        GetMoneyFun();
    });

    $("#SC_PiaoIn_PayMoney").change(function () {
        var inpay = $("#SC_PiaoIn_InMoney").val();
        var outpay = $("#SC_PiaoIn_PayMoney").val();
        var favpay = $("#SC_PiaoIn_PayFavourable").val();
        var money = inpay - outpay - favpay;
        $("#SC_PiaoIn_PayOwing").val(money)
    });
    $("#SC_PiaoIn_PayFavourable").change(function () {
        var inpay = $("#SC_PiaoIn_InMoney").val();
        var outpay = $("#SC_PiaoIn_PayMoney").val();
        var favpay = $("#SC_PiaoIn_PayFavourable").val();
        var money = inpay - outpay - favpay;
        $("#SC_PiaoIn_PayOwing").val(money)
    });

    function GetMoneyFun() {
        var UnitPrice = $("#SC_ZhangIn_PriceUnit").val();
        var Quantity = $("#SC_ZhangIn_Quantity").val();
        var money = UnitPrice * Quantity;
        $("#SC_ZhangIn_InMoney").val(money)
    }
    //单位对应
    $("#SC_ZhangIn_Unit").change(function () {
        var GoodsIDField = $('input[name="ID"]');  //alert($(GoodsIDField[1]).val());
        if (!isEmpty(GoodsIDField)) {
            var unit = $("#SC_ZhangIn_Unit").val();
            var requestURL = '/SC_PiaoIn.mvc/GetUnitRate';
            var postParams = { goodsId: $(GoodsIDField[1]).val(), unit: unit };
            var rData;
            ajaxRequest(requestURL, postParams, false, function (response) {
                if (response) {

                    $("#SC_ZhangIn_PriceUnit").val(response.price);
                    $("#SC_ZhangIn_UnitRate").val(response.rate);
                    GetMoneyFun();
                }
            });
        }
    });
     
    //设置日期列为日期范围搜索
    myJqGrid.setRangeSearch("InDate");
     
}