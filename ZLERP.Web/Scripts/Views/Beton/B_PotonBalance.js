function B_PotonBalanceIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'B_PotonBalanceMyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight * 0.7 - 100
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
        , storeCondition: "ModelType='泵送'"
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 120 }
            , { label: '纳入月份', name: 'InMonth', index: 'InMonth', width: 60 }
            , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '单据日期', name: 'OrderDate', index: 'OrderDate', formatter: 'date', width: 80 }
            , { label: '泵名称', name: 'CastMode', index: 'CastMode', width: 100 } 
         
            , { label: '结算总金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '合同号', name: 'ContractID', index: 'ContractID', width: 80, hidden: true }
            , { label: '合同名称', name: 'Contract.ContractName', index: 'Contract.ContractName', width: 150 }

            , { label: '工程编码', name: 'ProjectID', index: 'ProjectID', width: 80 }
            , { label: '工程名称', name: 'Project.ProjectName', index: 'Project.ProjectName', width: 120 }
            , { label: '开始日期', name: 'StartDate', index: 'StartDate', width: 80, formatter: 'date' }
            , { label: '结束日期', name: 'EndDate', index: 'EndDate', width: 80, formatter: 'date' }

            , { label: '合同结算', name: 'IsStockPrice', index: 'IsStockPrice', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '手动结算', name: 'IsOnePrice', index: 'IsOnePrice', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '手动设置价', name: 'OnePrice', index: 'OnePrice', width: 80 } 
            , { label: '累计泵送数量', name: 'AllPCount', index: 'AllPCount', width: 100 }
            , { label: '累计泵送金额', name: 'AllPMoney', index: 'AllPMoney', width: 100, align: 'right', formatter: 'currency' }
            , { label: '确认泵送数量', name: 'AllPOkCount', index: 'AllPOkCount', width: 100 }
            , { label: '确认泵送金额', name: 'AllPOkMoney', index: 'AllPOkMoney', width: 100, align: 'right', formatter: 'currency' }
            , { label: '其他费用', name: 'OtherMoney', index: 'OtherMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '已付金额', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '欠款金额', name: 'PayOwing', index: 'PayOwing', width: 80, align: 'right', formatter: 'currency' }
            , { label: '优惠金额', name: 'PayFavourable', index: 'PayFavourable', width: 80, align: 'right', formatter: 'currency' }
            , { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 80 }
            , { label: '结算人', name: 'BaleMan', index: 'BaleMan', width: 80 }
            , { label: '对方结算人', name: 'ThatBaleMan', index: 'ThatBaleMan', width: 80 }
            , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
            , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 80 }
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
                    loadFrom: 'B_PotonBalanceMyFormDiv',
                    btn: btn,
                    width: 900,
                    height: 600,
                    prefix: "B_Balance",
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
                showConfirm("确认信息", "您确定要重新计算当前结算单:" + BaleNo + "？结算项目：明细各项，累计数量，累计金额",
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
                var ContractID = Record.ContractID;
                var ProjectID = Record.ProjectID;
                var CastMode = Record.CastMode;
                var ChangeDay = gSysConfig.ChangeDay;
                var inMonth = Record.InMonth;
                var EndDay = inMonth + '-' + ChangeDay;
                var StartDay = AddMonth(inMonth) + '-' + ChangeDay;
                condition = " AND ProduceDate between '" + StartDay + " 00:00:00' and '" + EndDay + " 00:00:00'";
                myJqGridToAdd.refreshGrid("ID NOT IN (SELECT ShipDocID FROM B_BalanceDel WHERE ModelType='混凝土') AND ContractID ='" + ContractID + "'  and ProjectID='" + ProjectID + "' and CastMode='" + CastMode + "'and IsAudit=1 and IsEffective=1 " + condition);

                $("#B_PotonBalancemyJqGridBAddDetial").dialog("open");

            },
            AddCompute: function (btn) {
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
            },
            Print: function (btn) {
                console.log(1111);
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/DisplayReport.aspx?report=B_Balance&ID=" + key;
                window.open(url, "_blank");
            },
            PrintDesgin: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=B_Balance";
                window.open(url, "_blank");
            },
            PrintDirect: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/PrintDirect.aspx?report=B_Balance&ID=" + key;
                window.open(url, "_blank");
            }

        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'B_PotonBalancemyJqGridBDetial',
        autoWidth: true,
        buttons: buttons1,
        title: "本结算单的运输单列表",
        height: gGridHeight * 0.3,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        editSaveUrl: options.UpdateUrl,
        initArray: [
              {label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'BaleBalanceID', name: 'BaleBalanceID', index: 'BaleBalanceID', hidden: true }
            , { label: '运输单号', name: 'ShipDocID', index: 'ShipDocID', width: 100 }
            , { label: '泵名称', name: 'ShippingDocument.PumpName', index: 'ShippingDocument.PumpName', width: 80 }
            , { label: '砼强度', name: 'ShippingDocument.ConStrength', index: 'ShippingDocument.ConStrength', width: 100 }
            , { label: '浇筑方式', name: 'ShippingDocument.CastMode', index: 'ShippingDocument.CastMode', width: 80 } 
            , { label: '生产时间', name: 'ShippingDocument.ProduceDate', index: 'ShippingDocument.ProduceDate', formatter: 'datetime', width: 120 }
            , { label: '签收方量', name: 'ShippingDocument.SignInCube', index: 'ShippingDocument.SignInCube', width: 80 }
           
            , { label: '泵送单价(元/方)', name: 'PPrice', index: 'PPrice', search: false, width: 100, align: 'right', formatter: 'currency' }

            , { label: '泵送金额', name: 'AllPMoney', index: 'AllPMoney',  search: false, width: 100, align: 'right', formatter: 'currency' }

            , { label: '其他费用', name: 'OtherFee', index: 'OtherFee', editable: true, search: false, width: 80, align: 'right', formatter: 'currency' }
            , { label: '总费用', name: 'AllMoney', index: 'AllMoney', search: false, width: 80, align: 'right', formatter: 'currency' }
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
        renderTo: 'B_PotonBalancemyJqGridBAddDetial',
        autoWidth: true,
        buttons: buttons2,
        height: 500, 
        storeURL: options.ShopdocUrl,
        sortByField: 'ID',
        dialogWidth: 500,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 100,
        showPageBar: true,
        multiselect: true, 
        initArray: [
              { label: '运输单号', name: 'ID', index: 'ID', width: 100 }
            , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 80 }
            , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 80 }  
            , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 } 
            , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
            , { label: '时间', name: 'ProduceDate', index: 'ProduceDate', formatter: 'datetime', width: 120 }
       
            , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 60 }
            , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 60 } 
            , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 50, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
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
	        loadFrom: 'B_PotonBalanceMyFormDiv',
	        title: '查看详细',
	        width: 900,
	        height: 600,
	        prefix: "B_Balance",
	        buttons: {
	            "关闭": function () {
	                $(this).dialog('close');
	            }
	        },
	        afterFormLoaded: function () {
	        }
	    });
	});


	$("#B_PotonBalancemyJqGridBAddDetial").dialog({
	    modal: true,
	    autoOpen: false,
	    title: "未结算的运输单[合同与工程与选择的结算单一致]",
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
	            var requestURL = options.AddShopdocUrl;
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