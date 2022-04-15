function CarMaintainIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight*0.8-50
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true } 
            , { label: '流水号', name: 'MaintainNo', index: 'MaintainNo', width: 120 }
            , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
            , { label: '车牌号', name: 'Car.CarNo', index: 'Car.CarNo', width: 80 }
              , { label: '是否完修', name: 'IsOver', index: 'IsOver', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '维修方式', name: 'RepairWay', index: 'RepairWay', width: 80 }
            , { label: '送修日期', name: 'RepairDate', index: 'RepairDate', width: 80, formatter: 'date' }
            , { label: '送修里程', name: 'RepairMilo', index: 'RepairMilo', width: 80 }
            , { label: '送修人员', name: 'GiveMan', index: 'GiveMan', width: 80 }
            , { label: '维修单位', name: 'RepairUnit', index: 'RepairUnit', width: 80, hidden: true }
            , { label: '维修单位', name: 'CarDealings.Name', index: 'CarDealings.Name', width: 80 }

            , { label: '故障描述', name: 'FaultDesc', index: 'FaultDesc', width: 200 }
            , { label: '预计取车日期', name: 'ForecastDate', index: 'ForecastDate', width: 80, formatter: 'date' }
            , { label: '取车人', name: 'GetCarMan', index: 'GetCarMan', width: 80 }
            , { label: '取车日期', name: 'GetCarDate', index: 'GetCarDate', width: 80, formatter: 'date' }

            , { label: '维修地点', name: 'RepairAdress', index: 'RepairAdress', width: 120 }  
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
				            loadFrom: 'CarMaintainMyFormDiv',
				            btn: btn,
				            width: 500,
				            height: 400,
				            afterFormLoaded: function () {
				                $("#CarMaintain_MaintainNo").val(response.Data);
                                $("#HiddenGetCarMan").hide();
				                $("#HiddenIsOver").hide();
				                $("#HiddenRepairPirce").hide();
				                $("#HiddenRepairDesc").hide();
				                $("#HiddenRepairMan").hide();
				             
				            }
				        });
				    }
				});

            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarMaintainMyFormDiv',
                    btn: btn,
                    width: 500,
                    height: 500,
                    prefix: "CarMaintain",
                    afterFormLoaded: function () {
                        $("#IsOver").attr('checked', true);
                        $("#HiddenIsOver").show();
                        $("#HiddenGetCarMan").show();
                        $("#HiddenRepairPirce").show();
                        $("#HiddenRepairDesc").show();
                        $("#HiddenRepairMan").show(); 
                  
                    }
                });
            },
            handleAll: function (btn) {
                ajaxRequest(options.GenerateOrderNoUrl, {},
				false,
				function (response) {
				    if (response.Result) {
				        myJqGrid.handleAdd({
				            loadFrom: 'CarMaintainMyFormDiv',
				            btn: btn,
				            width: 500,
				            height: 500,
				            afterFormLoaded: function () {
				                $("#MaintainNo").val(response.Data);
				                $("#HiddenIsOver").show();
                                $("#HiddenGetCarMan").show();
				                $("#HiddenRepairPirce").show();
				                $("#HiddenRepairDesc").show();
				                $("#HiddenRepairMan").show(); 
				               
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
        buttons: buttons2,
        height: gGridHeight * 0.3,
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



    var myJqGridTo2 = new MyGrid({
        renderTo: 'MyJqGridTo2',
        title: '维修项目明细(场内维修如果走的是出库流程,可以不填单价)',
        autoWidth: true,
        buttons: buttons1,
        height: 150,
        storeURL: options.GetMainDel,
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
		    label: '维修单号',
		    name: 'MaintainID',
		    index: 'MaintainID',
		    width: 100,
		    hidden: true
		},
	    {
	        label: '维修大类',
		    name: 'CarMaintainItem.ItemType',
		    index: 'CarMaintainItem.ItemType',
		    width: 100
		},
        
		{
		    label: '维修项目',
		    name: 'ItemID',
		    index: 'ItemID',
		    width: 100,
		    hidden: true
		},
        {
            label: '维修项目',
            name: 'CarMaintainItem.ItemName',
            index: 'CarMaintainItem.ItemName',
            width: 80
        },
		{
		    label: '材料数量',
		    name: 'MaterialNum',
		    index: 'MaterialNum',
		    width: 60
		},
		{
		    label: '材料单价',
		    name: 'MaterialPrice',
		    index: 'MaterialPrice',
		    width: 50
		},
        {
            label: '材料费',
            name: 'MaterialCost',
            index: 'MaterialCost',
            width: 100
        },
		{
		    label: '工时(H)',
		    name: 'ManHour',
		    index: 'ManHour',
		    width: 60
		},
		{
		    label: '工时费',
		    name: 'ManHourCost',
		    index: 'ManHourCost',
		    width: 80
		},
		{
		    label: '合计',
		    name: 'AllCost',
		    index: 'AllCost',
		    width: 80
		},
		{
		    label: '摘要',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		}]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                myJqGridTo2.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo2.refreshGrid();
            },
            handleAddDetial: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                myJqGrid.handleAdd({
                    loadFrom: 'CarMaintainDelForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#CarMaintainDel_MaintainID").val(id); 
                        ItemChange();
                    },
                    postCallBack: function (response) {
                        myJqGridTo2.refreshGrid();
                    }
                });
            },
            handleEditDetial: function (btn) {
                myJqGridTo2.handleEdit({
                    loadFrom: 'CarMaintainDelForm',
                    btn: btn,
                    prefix: "CarMaintainDel",
                    afterFormLoaded: function () {
                        ItemChange();
                    },
                    postCallBack: function (response) {
                        myJqGridTo2.refreshGrid();
                    }
                });
            },
            handleDeleteDetial: function (btn) {
                myJqGridTo2.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }

    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("SC_PiaoOut.CarMaintainID=" + id + " and SC_PiaoOut.Condition='已出库'");
	    myJqGridTo2.refreshGrid("MaintainID=" + id);
	});

 

    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'CarMaintainMyFormDiv',
            title: '查看维修详细',
            prefix: "CarMaintain",
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


    $("#CarMaintainDel_MaterialNum").change(function () {
        GetMoneyFun();
    });
    $("#CarMaintainDel_MaterialPrice").change(function () {
        GetMoneyFun();
    });

    $("#CarMaintainDel_ManHourCost").change(function () {
        GetMoneyFun();
    });
    function GetMoneyFun() {
        var UnitPrice = $("#CarMaintainDel_MaterialNum").val();
        var Quantity = $("#CarMaintainDel_MaterialPrice").val();
        var money = UnitPrice * Quantity; 
        var ManHourCost = $("#CarMaintainDel_ManHourCost").val();
        $("#CarMaintainDel_MaterialCost").val(money)
        var allcost = money + ManHourCost;
        $("#CarMaintainDel_AllCost").val(allcost) 
    }

    //下拉选择商品的时候
    ItemChange = function () {
        var ItemField = $("#CarMaintainDel_ItemID");
        ItemField.unbind('change');
        ItemField.bind('change',
		function () {
		    var ItemId = ItemField.val();
		    ajaxRequest(options.GetDelUrl, {
		        ID: ItemId
		    },
			false,
			function (response) {
			    if (!!response.Result) {
			        $("#CarMaintainDel_MaterialPrice").val(response.Data.ID); 
			    } else {
			        showMessage('提示', response.Message);
			    }
			});
		});
    }
    
}