//采购计划
function stockPlanInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                  { label: '计划编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '任务单号', name: 'PlanNo', index: 'PlanNo', width: 110 }
                , { label: '开始日期', name: 'PlanDate', index: 'PlanDate', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '计划状态', name: 'ExecStatus', index: 'ExecStatus', width: 80 }

                , { label: '材料名称', name: 'StuffID', index: 'StuffID', hidden: true }
                , { label: '材料名称', name: 'StuffInfo.StuffName', index: 'StuffInfo.StuffName', width: 120 }

                , { label: '合同编号', name: 'StockPactID', index: 'StockPactID' }
                , { label: '合同名称', name: 'PactName', index: 'PactName' }
                , { label: '供货厂商', name: 'SupplyName', index: 'SupplyName' }
                , { label: '计划数量(T)', name: 'PlanAmount', index: 'PlanAmount', width: 80, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '质量单号', name: 'QualityNeed', index: 'QualityNeed', width: 80 }
                , { label: '产地', name: 'SourceAddr', index: 'SourceAddr', width: 80 }
                , { label: '发货单号', name: 'PurNo', index: 'PurNo', width: 80 }
                , { label: '联系人', name: 'Linker', index: 'Linker', width: 80 }
                , { label: '联系电话', name: 'LinkPhone', index: 'LinkPhone', width: 80 }
                , { label: '单据数量(T)', name: 'OrderNum', index: 'OrderNum', width: 80 }
                , { label: '运输商', name: 'TransID', index: 'TransID', width: 80 }
                , { label: '运输商', name: 'TransInfo.SupplyName', index: 'TransInfo.SupplyName', width: 120 }
                , { label: '运输车号', name: 'CarNo', index: 'CarNo', width: 80 }
                , { label: '运输方式', name: 'TransportMode', index: 'TransportMode', width: 80 }
                , { label: '联系人', name: 'CarLinker', index: 'CarLinker', width: 80 }
                , { label: '联系电话', name: 'CarLinkPhone', index: 'CarLinkPhone', width: 80 }
                , { label: '司机', name: 'CarDriver', index: 'CarDriver', width: 80 }
                , { label: '扣除率', name: 'OutRate', index: 'OutRate', width: 80 }
                , { label: '存放仓库', name: 'SiloID', index: 'SiloID', width: 80, hidden: true }
                , { label: '存放仓库', name: 'Silo.SiloName', index: 'Silo.SiloName', width: 80 }
                , { label: '每方容量', name: 'OneWeight', index: 'OneWeight', width: 80 }
                , { label: '下达人员', name: 'PlanMan', index: 'PlanMan', width: 80 }
                , { label: '登记人员', name: 'CheckMan', index: 'CheckMan', width: 80 }
                , { label: '登记日期', name: 'CheckDate', index: 'CheckDate', width: 80, formatter: 'date' }
                , { label: '计划数量(T)', name: 'PlanAmount', index: 'PlanAmount', width: 80 }
                , { label: '已入数量(T)', name: 'InNum', index: 'InNum', width: 80 }
                , { label: '未入数量(T)', name: 'UnNum', index: 'UnNum', width: 80 }
                , { label: '累计车数', name: 'CarNum', index: 'CarNum', width: 80 }
                , { label: '审核人', name: 'Auditor', index: 'Auditor' }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: booleanFmt, unformat: booleanUnFmt }
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
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    ajaxRequest(opts.GenerateOrderNoUrl, {},
				false,
				function (response) {
				    if (response.Result) {
				        myJqGrid.handleAdd({
				            loadFrom: 'MyFormDiv',
				            btn: btn,
				            width: 700,
				            height: 500,
				            afterFormLoaded: function () {
				                $("#PlanNo").val(response.Data);
				                StockChange()
				            }
				        });
				    }
				});
                },
                handleEdit: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();

                    var id = Record.ID;
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;
                    console.log(Record);
                    if (AuditStatus == 1) {
                        showMessage('提示', "只有草稿状态下的单据才能操作!");
                        return;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        width: 700,
                        height: 500,
                        afterFormLoaded: function () {

                        }
                    });
                },
                handleDelete: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();

                    var id = Record.ID;
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;
                    console.log(Record);
                    if (AuditStatus == 1) {
                        showMessage('提示', "只有草稿状态下的单据才能操作!");
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
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;
                    console.log(Record);
                    if (AuditStatus == 1) {
                        showMessage('提示', "只有草稿状态下的单据才能审核!");
                        return;
                    }
                    showConfirm("确认信息", "您确定要审核当前进料计划:" + PlanNo + "？",
				function () {
				    var requestURL = opts.ChangeConditionUrl;
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
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;
                    console.log(Record);
                    if (AuditStatus == 0) {
                        showMessage('提示', "只有草稿状态下的单据才能审核!");
                        return;
                    }
                    showConfirm("确认信息", "您确定要反审当前进料计划:" + PlanNo + "？",
				function () {
				    var requestURL = opts.ChangeConditionUrl;
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
                },
                ChangeCondition2: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var Record = myJqGrid.getSelectedRecord();

                    var id = Record.ID;
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;

                    showConfirm("确认信息", "您确定要更改当前计划单的状态码?单号:" + PlanNo + "？",
				function () {
				    var requestURL = opts.ChangeConditionUrl;
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
                ChangeCondition3: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var Record = myJqGrid.getSelectedRecord();

                    var id = Record.ID;
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;

                    showConfirm("确认信息", "您确定要更改当前计划单的状态码?单号:" + PlanNo + "？",
				function () {
				    var requestURL = opts.ChangeConditionUrl;
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
                ChangeCondition4: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var Record = myJqGrid.getSelectedRecord();

                    var id = Record.ID;
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;

                    showConfirm("确认信息", "您确定要更改当前计划单的状态码?单号:" + PlanNo + "？",
				function () {
				    var requestURL = opts.ChangeConditionUrl;
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
                ChangeCondition5: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var Record = myJqGrid.getSelectedRecord();

                    var id = Record.ID;
                    var PlanNo = Record.PlanNo;
                    var AuditStatus = Record.AuditStatus;

                    showConfirm("确认信息", "您确定要更改当前计划单的状态码?单号:" + PlanNo + "？",
				function () {
				    var requestURL = opts.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 5,
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
        myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
            myJqGrid.handleEdit({
                loadFrom: 'MyFormDiv',
                title: '查看详细',
                width: 700,
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
        StockChange = function () {
            var FixedField = myJqGrid.getFormField("StockPactID");
            FixedField.unbind('change');
            FixedField.bind('change',
		function () {
		    var id = FixedField.val();
		    ChangeStock(id);
		});
        }

        function ChangeStock(id) {
            ajaxRequest(opts.GetStockPactUrl, {
                id: id
            },
			false,
			function (response) {
			    if (!!response.Result) {
			        $("#PactName").val(response.Data.PactName);
			        $("#SupplyName").val(response.Data.SupplyName); 

			    } else {
			        showMessage('提示', response.Message);
			    }
			});
        }
}