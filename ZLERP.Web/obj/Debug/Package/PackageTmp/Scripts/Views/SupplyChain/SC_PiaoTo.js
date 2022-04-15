function SC_PiaoToIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'SC_PiaoToGrid',
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
		    label: '调拨单号',
		    name: 'ChangeNo',
		    index: 'ChangeNo',
		    width: 120
		},
		{
		    label: '调出仓库',
		    name: 'OutLibID',
		    index: 'OutLibID',
		    width: 80,
		    hidden: true
		},
		{
		    label: '调入仓库',
		    name: 'InLibID',
		    index: 'InLibID',
		    width: 80,
		    hidden: true
		},
		{
		    label: '调出仓库',
		    name: 'SC_LibOut.LibName',
		    index: 'SC_LibOut.LibName',
		    width: 80
		},
		{
		    label: '调入仓库',
		    name: 'SC_LibIn.LibName',
		    index: 'SC_LibIn.LibName',
		    width: 80
		},
		{
		    label: '日期',
		    name: 'ChangeDate',
		    index: 'ChangeDate',
		    formatter: 'date',
		    width: 80
		},
		{
		    label: '品种数',
		    name: 'VarietyNum',
		    index: 'VarietyNum',
		    width: 60
		},
		{
		    label: '调库金额',
		    name: 'ChangeMoney',
		    index: 'ChangeMoney',
		    width: 80,
		    formatter: 'currency'
		},
		{
		    label: '状态',
		    name: 'Condition',
		    index: 'Condition',
		    width: 60
		},
		{
		    label: '制单人',
		    name: 'Builder',
		    index: 'Builder',
		    width: 80
		},
		{
		    label: '制单时间',
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
				            loadFrom: 'SC_PiaoToForm',
				            btn: btn,
				            afterFormLoaded: function () {
				                $("#SC_PiaoTo_ChangeNo").val(response.Data);
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
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能修改!");
                    return;
                }
                myJqGrid.handleEdit({
                    loadFrom: 'SC_PiaoToForm',
                    btn: btn,
                    prefix: "SC_PiaoTo"
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
            handlePurIn: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.ChangeNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿下的单据才能审核入库!");
                    return;
                }
                showConfirm("确认信息", "此操作一旦点击不可撤回，将对库存，金额等产生影响，请确认？，调拨单号：" + orderNo + "？",
				function () {
				    var requestURL = options.PurInUrl;
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
                    var url = "/GridReport/DisplayReport.aspx?report=SC_PiaoTo&ID=" + key;
                    window.open(url, "_blank");
                },
                PrintDesign: function (btn) {
                    var url = "/GridReport/DesignReport.aspx?report=SC_PiaoTo";
                    window.open(url, "_blank");
                },
                PrintDirect: function (btn) {
                    var key = myJqGrid.getSelectedKey();
                    if (key <= 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var url = "/GridReport/PrintDirect.aspx?report=SC_PiaoTo&ID=" + key;
                    window.open(url, "_blank");
                }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'SC_ZhangToGrid',
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
            hidden: true
        },
		{
		    label: '调拨单号',
		    name: 'ChangeNo',
		    index: 'ChangeNo',
		    hidden: true
		},
		{
		    label: '库存商品',
		    name: 'New_LibID',
		    index: 'New_LibID',
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
		    label: '品牌',
		    name: 'SC_Goods.Brand',
		    index: 'SC_Goods.Brand',
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
		    name: 'PriceUnit',
		    index: 'PriceUnit',
		    width: 100,
		    formatter: 'currency'
		},
		{
		    label: '金额',
		    name: 'ChangeMoney',
		    index: 'ChangeMoney',
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
                $("#SC_ZhangTo_ChangeNo").val(Record.ID);
                myJqGrid.handleAdd({
                    loadFrom: 'SC_ZhangToForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            myJqGrid.refreshGrid();
                            myJqGridTo.refreshGrid();
                        }
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
                    loadFrom: 'SC_ZhangToForm',
                    btn: btn,
                    prefix: "SC_ZhangTo",
                    afterFormLoaded: function () {

                        $('input[name="ID"]').val(GoodsID);

                        ajaxRequest(options.GetZhangUrl, {
                            id: GoodsID
                        },
						false,
						function (response) {
						    if (!!response.Result) {
						        $("#SC_ZhangTo_Unit").find("option").remove();
						        var length = response.Data.SC_GoodsUnits.length;
						        for (var i = 0; i < length; i++) {
						            if (!response.Data.IsAuxiliaryUnit) {
						                if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
						                    break;
						                }
						            }
						            $("#SC_ZhangTo_Unit").append("<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit + "</option>");
						        }
						        $("#SC_ZhangTo_Unit").val(Unit);
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
	    myJqGridTo.refreshGrid("ChangeNo=" + id);
	});
    myJqGrid.addListeners("gridComplete",
	function () {
	    var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
	    for (var i = 0; i < ids.length; i++) {
	        var cl = ids[i];
	        var record = myJqGrid.getRecordByKeyValue(ids[i]);
	        if (record.Condition == "草稿") {
	            myJqGrid.getJqGrid().setCell(cl, "Condition", '', {
	                color: 'red'
	            },
				'');
	        }
	    }
	});
    //下拉选择商品的时候
    GoodsChange = function () {
        var goodIDField = myJqGrid.getFormField("ID");
        goodIDField.unbind('change');
        goodIDField.bind('change',
		function () {
		    var Goodsid = goodIDField.val();
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
		        $("#SC_ZhangTo_Unit").find("option").remove();
		        var length = response.Data.SC_GoodsUnits.length;
		        for (var i = 0; i < length; i++) {
		            if (!response.Data.IsAuxiliaryUnit) {
		                if (response.Data.SC_GoodsUnits[i].UnitDesc != "最小计量单位") {
		                    break;
		                }
		            }
		            $("#SC_ZhangTo_Unit").append("<option value=" + response.Data.SC_GoodsUnits[i].Unit + ">" + response.Data.SC_GoodsUnits[i].Unit + "</option>");
		        }
		        $("#SC_ZhangTo_UnitRate").val(1);
		        $("#SC_ZhangTo_PriceUnit").val(response.Data.LibPrice);
		        $("#SC_ZhangTo_SC_Goods_Spec").val(response.Data.Spec);
		        $("#SC_ZhangTo_GoodsID").val(response.Data.ID);
		        $("#SC_ZhangTo_PriceUnit").val(response.Data.LibPrice);
		        $("#SC_ZhangTo_SC_Goods_Spec").val(response.Data.Spec);
		        $("#SC_ZhangTo_SC_Goods_Unit").val(response.Data.Unit);
		        $("#SC_ZhangTo_SC_Goods_Brand").val(response.Data.Brand);
		        $("#SC_ZhangTo_SC_Goods_GoodsName").val(response.Data.GoodsName);
		        GetMoneyFun();

		    } else {
		        showMessage('提示', response.Message);
		    }
		});
    }

    $("#SC_ZhangTo_Quantity").change(function () {
        GetMoneyFun();
    });

    function GetMoneyFun() {
        var UnitPrice = $("#SC_ZhangTo_PriceUnit").val();
        var Quantity = $("#SC_ZhangTo_Quantity").val();
        var money = UnitPrice * Quantity;
        $("#SC_ZhangTo_ChangeMoney").val(money)
    }
    //单位对应
    $("#SC_ZhangTo_Unit").change(function () {
        var GoodsIDField = $('input[name="ID"]');
        if (!isEmpty(GoodsIDField)) {
            var unit = $("#SC_ZhangTo_Unit").val();
            var requestURL = '/SC_PiaoIn.mvc/GetUnitRate';
            var postParams = {
                goodsId: GoodsIDField.val(),
                unit: unit
            };
            var rData;
            ajaxRequest(requestURL, postParams, false,
			function (response) {
			    if (response) { 
			        $("#SC_ZhangTo_PriceUnit").val(response.LibPrice);
			        $("#SC_ZhangTo_UnitRate").val(response.rate);
			        GetMoneyFun();
			    }
			});
        }
});
//设置日期列为日期范围搜索
myJqGrid.setRangeSearch("ChangeDate");
}