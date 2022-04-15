function M_BaleBalanceIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight * 0.7 - 100
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true 
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 120 }
            , { label: '纳入月份', name: 'InMonth', index: 'InMonth', width: 60 }
            , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '单据日期', name: 'OrderDate', index: 'OrderDate', formatter: 'date', width: 60 }
            , { label: '确认数量(吨/方)', name: 'AllOkCount', index: 'AllOkCount', formatter: Kg2TFmt, unformat: T2KgFmt, width: 80 }
            , { label: '结算金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 60 }
            , { label: '供应商编码', name: 'StockPactID', index: 'StockPactID', width: 60, hidden: true }
            , { label: '供货厂商', name: 'SupplyInfo.SupplyName', index: 'SupplyInfo.SupplyName', width: 150 } 
            , { label: '材料编码', name: 'StuffID', index: 'StuffID', width: 60, hidden: true }
            , { label: '材料名称', name: 'StuffInfo.StuffName', index: 'StuffInfo.StuffName', width: 120 }
            , { label: '开始日期', name: 'StartDate', index: 'StartDate', width: 60, formatter: 'date' }
            , { label: '结束日期', name: 'EndDate', index: 'EndDate', width: 60, formatter: 'date' }
            , { label: '结算方式', name: 'BaleType', index: 'BaleType', width: 60 }
            , { label: '单一价格结算', name: 'IsOnePrice', index: 'IsOnePrice', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '单一价格', name: 'OnePrice', index: 'OnePrice', width: 60 }
            , { label: '合同价结算', name: 'IsStockPrice', index: 'IsStockPrice', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '累计数量(吨/方)', name: 'AllCount', index: 'AllCount', formatter: Kg2TFmt, unformat: T2KgFmt, width: 80 }
            , { label: '累计金额', name: 'AllMoney', index: 'AllMoney', width: 60 }

            , { label: '已付金额', name: 'PayMoney', index: 'PayMoney', width: 60 , align: 'right', formatter: 'currency' }
            , { label: '欠款金额', name: 'PayOwing', index: 'PayOwing', width: 60, align: 'right', formatter: 'currency' }
            , { label: '优惠金额', name: 'PayFavourable', index: 'PayFavourable', width: 60, align: 'right', formatter: 'currency' }

            , { label: '已开票金额', name: 'PiaoPayMoney', index: 'PiaoPayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '未开票金额', name: 'PiaoPayOwing', index: 'PiaoPayOwing', width: 80, align: 'right', formatter: 'currency' }
            , { label: '免开票金额', name: 'PiaoPayFavourable', index: 'PiaoPayFavourable', width: 80, align: 'right', formatter: 'currency' }

            , { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 60 }
            , { label: '结算人', name: 'BaleMan', index: 'BaleMan', width: 60 }
            , { label: '对方结算人', name: 'ThatBaleMan', index: 'ThatBaleMan', width: 60 }
            , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 60 }
            , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 60 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }
            , { label: '最后修改人', name: 'Modifier', index: 'Modifier', width: 80 }
            , { label: '最后修改时间', name: 'ModifyTime', index: 'ModifyTime', width: 120, formatter: 'datetime' }
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handleEdit: function (btn) {
                var data = myJqGrid.getSelectedRecord();
                if (data && data.AuditStatus == '1') {
                    showMessage('该结算单已审核，不允许再操作');
                    return;
                };
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    width: 700,
                    height: 500,
                    prefix: "M_BaleBalance",
                    afterFormLoaded: function () {

                    }
                });
            },
            handleDelete: function (btn) {
                var data = myJqGrid.getSelectedRecord();
                if (data && data.AuditStatus == '1') {
                    showMessage('该结算单已审核，不允许再操作');
                    return;
                };
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            ChangeCondition: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();

                var id = Record.ID;
                var BaleNo = Record.BaleNo;
                var AuditStatus = Record.AuditStatus;
                console.log(Record);
                if (AuditStatus == 1) {
                    showMessage('提示', "只有草稿状态下的单据才能审核!");
                    return;
                }
                showConfirm("确认信息", "您确定要审核当前结算单:" + BaleNo + "？",
				function () {
				    var requestURL = options.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 0,
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
            }
            , UnChangeCondition: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();

                var id = Record.ID;
                var BaleNo = Record.BaleNo;
                var AuditStatus = Record.AuditStatus;
                console.log(Record);
                if (AuditStatus == 0) {
                    showMessage('提示', "只有草稿状态下的单据才能审核!");
                    return;
                }
                showConfirm("确认信息", "您确定要反审当前结算单:" + BaleNo + "？",
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
            Compute: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();

                var id = Record.ID;
                var BaleNo = Record.BaleNo;
                var AuditStatus = Record.AuditStatus;
                console.log(Record);
                if (AuditStatus == 1) {
                    showMessage('提示', "只有草稿状态下的单据才能重新计算!");
                    return;
                }
                showConfirm("确认信息", "您确定要重新计算当前结算单:" + BaleNo + "？结算项目：累计数量，累计金额",
				function () {
				    var requestURL = options.ComputeUrl;
				    ajaxRequest(requestURL, {
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
            handleAdd: function (btn) { 
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord(); 
                var id = Record.ID;
                var BaleNo = Record.BaleNo;
                var AuditStatus = Record.AuditStatus;
                console.log(Record);
                if (AuditStatus == 1) {
                    showMessage('提示', "只有草稿状态下的单据才能添加!");
                    return;
                } 
                var Record = myJqGrid.getSelectedRecord();
                var StockPactID = Record.StockPactID;
                var StuffID = Record.StuffID;
                var ChangeDay = gSysConfig.ChangeDay;
                var inMonth = Record.InMonth;
                var EndDay = inMonth + '-' + ChangeDay;
                var StartDay = AddMonth(inMonth) + '-' + ChangeDay;
                condition = " AND InDate between '" + StartDay + " 00:00:00' and '" + EndDay + " 00:00:00'";
                myJqGridToAdd.refreshGrid(" StuffInID NOT IN (SELECT StuffInID FROM M_BaleBalanceDel) AND Lifecycle=3 AND SupplyID='" + StockPactID + "' and StuffID='" + StuffID + "'" + condition);
 
                $("#myJqGridAddDetial").dialog("open");
 ; 
            }
            , AddCompute: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();

                var id = Record.ID;
                var BaleNo = Record.BaleNo;
                var AuditStatus = Record.AuditStatus;
                if (AuditStatus == 1) {
                    showMessage('提示', "只有草稿状态下的单据才能添加!");
                    return;
                }
                showConfirm("确认信息", "您确定要添加当前结算单:" + BaleNo + "对应的所有单据？",
				function () {
				    var requestURL = options.AddAllBalanceUrl;
				    ajaxRequest(requestURL, {
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
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial',
        autoWidth: true,
        buttons: buttons1,
        title: "本结算单的进料单列表",
        height: gGridHeight * 0.3,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        editSaveUrl: options.UpdateUrl,
           rowList: [10, 20, 30, 50, 100, 200, 300, 400, 500] , 
        initArray: [
              {label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'BaleBalanceID', name: 'BaleBalanceID', index: 'BaleBalanceID', hidden: true }
            , { label: '进料单号', name: 'StuffInID', index: 'StuffInID', width: 100 }
            , { label: '原材料名称', name: 'StuffIn.StuffName', index: 'StuffIn.StuffName', width: 100 }
            , { label: '材料编码', name: 'SpecID', index: 'SpecID', hidden: true }
            , { label: '原料规格', name: 'SpecName', index: 'StuffIn.SpecName', width: 60 }
            , { label: '供货厂商', name: 'StuffIn.SupplyName', index: 'StuffIn.SupplyName', width: 150 }
            , { label: '入仓', name: 'StuffIn.SiloName', index: 'StuffIn.SiloName', width: 80 }
            , { label: '过磅时间', name: 'StuffIn.InDate', index: 'StuffIn.InDate', formatter: 'datetime', width: 120 }  
            , { label: '车号', name: 'StuffIn.CarNo', index: 'StuffIn.CarNo', width: 100 }
            , { label: '运输单位', name: 'StuffIn.TransportName', index: 'StuffIn.TransportName', width: 100 }
            , { label: '折算方量', name: 'StuffIn.Volume', index: 'StuffIn.Volume', width: 80 }
            , { label: '厂商数量(吨)', name: 'StuffIn.SupplyNum', index: 'StuffIn.SupplyNum', formatter: Kg2TFmt, unformat: T2KgFmt, width: 80 }
            , { label: '结算数量(吨)', name: 'StuffIn.FinalFootNum', index: 'StuffIn.FinalFootNum', formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right', width: 80 } 
            , { label: '单价(元/吨|方)', name: 'Price', index: 'Price', width: 80, align: 'right', formatter: 'currency' }
            , { label: '其他费用', name: 'OtherMoney', index: 'OtherMoney', editable: true, width: 60, align: 'right', formatter: 'currency' }
            , { label: '总价', name: 'AllMoney', index: 'AllMoney', width: 60, align: 'right', formatter: 'currency' }
            , { label: '备注', name: 'Remark', index: 'Remark', editable: true, search: false, width: 120, align: 'right' }
		],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            },
            handleDelete: function (btn) {
                myJqGridTo.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });


    var myJqGridToAdd = new MyGrid({
        renderTo: 'myJqGridAddDetial',
        autoWidth: true,
        buttons: buttons2,
        height: 500, 
        storeURL: options.StuffInUrl,
        sortByField: 'ID',
        dialogWidth: 500,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 100,
        showPageBar: true,
        multiselect: true, 
        initArray: [
              { label: '进料单号', name: 'ID', index: 'ID', width: 80 }
            , { label: '原材料名称', name: 'StuffName', index: 'StuffName', width: 100 }
            , { label: '材料编码', name: 'SpecID', index: 'SpecID', hidden: true }
            , { label: '原料规格', name: 'SpecName', index: 'StuffIn.SpecName', width: 60 }
            , { label: '供货厂商', name: 'SupplyName', index: 'SupplyName', width: 150 }
            , { label: '结算重量(吨)', name: 'FinalFootNum', index: 'FinalFootNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
            , { label: '入仓', name: 'SiloName', index: 'SiloName', width: 100 }
            , { label: '过磅时间', name: 'InDate', index: 'InDate', formatter: 'datetime', width: 120 }
            , { label: '出厂时间', name: 'OutDate', index: 'OutDate', formatter: 'datetime', width: 120 }
            , { label: '司磅员', name: 'Operator', index: 'Operator' }
      
            , { label: '运输单位', name: 'TransportName', index: 'TransportName', width: 150 }
            , { label: '车号', name: 'CarNo', index: 'CarNo', width: 100 }
           
           
		],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            }

        }
    });


    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("BaleBalanceID=" + id);
	});

	myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
	    myJqGrid.handleEdit({
	        loadFrom: 'MyFormDiv',
	        title: '查看详细',
	        width: 700,
	        height: 500,
	        prefix: "M_BaleBalance",
	        buttons: {
	            "关闭": function () {
	                $(this).dialog('close');
	            }
	        },
	        afterFormLoaded: function () {
	        }
	    });
	});


	$("#myJqGridAddDetial").dialog({
	    modal: true,
	    autoOpen: false,
	    title: "未结算的进料单[供应商和材料与选择的结算单一致]",
	    width: 800,
	    Height: 600,
	    buttons: {
	        '确认': function () {
	            var Record = myJqGrid.getSelectedRecord();
	            var ID = Record.ID;

	            var keys = myJqGridToAdd.getSelectedKeys();
	            if (keys.length == 0) {
	                showMessage('提示', "请选择需要的单据!");
	                return;
	            } 
	            var requestURL = options.AddStuffInUrl;
	            ajaxRequest(requestURL, {
                    id:ID,
	                keys: keys
	            },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！');
					        myJqGridTo.refreshGrid();
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



	function AddMonth(strmonth) {
	    var dateday = new Date(strmonth + '-26');
	    var day = new Date(dateday);
	    dateday.setMonth(dateday.getMonth() - 1);
	    var year = dateday.getFullYear();
	    var month = dateday.getMonth() + 1, month = month < 10 ? '0' + month : month;
	    return year + '-' + month;
	}
}