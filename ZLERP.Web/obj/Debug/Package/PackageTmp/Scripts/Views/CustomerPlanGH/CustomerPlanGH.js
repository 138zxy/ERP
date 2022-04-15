function customerplanIndexInit(opts) {
    var d = new Date();
    var today = opts.todayDate;
    var tomorrow = opts.tomorrowDate; //  d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
    var condition = "PlanDate >= '" + today + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59'";
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
		,
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl
        //		    , sortByField: 'NeedDate,BuildTime, ConstructUnit '
		,
        sortByField: 'ID',
        sortOrder: 'DESC',
        primaryKey: 'ID',
        setGridPageSize: 50,
        dialogWidth: 980,
        dialogHeight: 520,
        showPageBar: true,
        singleSelect: false,
        storeCondition: condition,
        groupField: 'PlanDate',
        advancedSearch: true,
        groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        },
        initArray: [{
            label: '工地计划编号',
            name: 'ID',
            index: 'ID',
            width: 100
        },
		{
		    label: '创建时间',
		    name: 'BuildTime',
		    index: 'BuildTime',
		    formatter: 'datetime',
		    search: true,
		    formatoptions: {
		        newformat: "ISO8601Long"
		    },
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datetimepicker();
		        },
		        sopt: ['ge']
		    }
		},
		{
		    label: '计划日期',
		    name: 'PlanDate',
		    index: 'PlanDate',
		    formatter: 'date',
		    search: false,
		    sortable: false,
		    width: 80
		},
		{
		    label: '到场时间',
		    name: 'NeedDate',
		    index: 'NeedDate',
		    search: false,
		    width: 60
		},
		{
		    label: '审核状态',
		    name: 'AuditStatus',
		    index: 'AuditStatus',
		    search: false,
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: 'AuditStatus'
		    },
		    width: 60
		},
		{
		    label: '合同号',
		    name: 'ContractNo',
		    index: 'ContractNo',
		    width: 100
		},
		{
		    label: '工程名称',
		    name: 'ProjectName',
		    index: 'ProjectName'
		},
		{
		    label: '客户名称',
		    name: 'ContractGH.CustName',
		    index: 'ContractGH.CustName',
		    width: 100
		},
		{
		    label: '建设单位',
		    name: 'BuildUnit',
		    index: 'BuildUnit'
		},
		{
		    label: '施工单位',
		    name: 'ConstructUnit',
		    index: 'ConstructUnit'
		},
		{
		    label: '交货地址',
		    name: 'DeliveryAddress',
		    index: 'DeliveryAddress'
		},
		{
		    label: '工地电话',
		    name: 'Tel',
		    index: 'Tel'
		},
		{
		    label: '强度',
		    name: 'ConStrength',
		    index: 'ConStrength',
		    width: 60
		},
		{
		    label: '浇筑方式(干混)',
		    name: 'CastMode',
		    index: 'CastMode',
		    width: 60
		},
		{
		    label: '计划量',
		    name: 'PlanCube',
		    index: 'PlanCube',
		    width: 60,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>'
		},
		{
		    label: '施工部位',
		    name: 'ConsPos',
		    index: 'ConsPos',
		    width: 60
		},
		{
		    label: '剩余金额',
		    name: 'Balance',
		    index: 'Balance',
		    width: 80
		},
		{
		    label: '项目地址',
		    name: 'ProjectAddr',
		    index: 'ProjectAddr'
		},
		{
		    label: '供应单位',
		    name: 'SupplyUnit',
		    index: 'SupplyUnit'
		},
		{
		    label: '坍落度',
		    name: 'Slump',
		    index: 'Slump',
		    width: 60
		},
		{
		    label: '泵名称',
		    name: 'PumpName',
		    index: 'PumpName',
		    width: 100,
		    hidden: true
		},
		{
		    label: '区间',
		    name: 'RegionID',
		    index: 'RegionID',
		    width: 80
		},
		{
		    label: '开盘安排',
		    name: 'PiepLen',
		    index: 'PiepLen',
		    width: 50
		},
		{
		    label: '工地联系人',
		    name: 'LinkMan',
		    index: 'LinkMan'
		},
		{
		    label: '泵工',
		    name: 'PumpMan',
		    index: 'PumpMan',
		    width: 60,
		    hidden: true
		},
		{
		    label: '合同编号',
		    name: 'ContractID',
		    index: 'ContractID',
		    width: 80
		},
		{
		    label: '合同名称',
		    name: 'ContractName',
		    index: 'ContractName'
		},
		{
		    label: '任务单号',
		    name: 'TaskID',
		    index: 'TaskID'
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark'
		},
		{
		    label: '审核时间',
		    name: 'AuditTime',
		    index: 'AuditTime',
		    formatter: 'datetime',
		    search: false
		},
		{
		    label: '审核意见',
		    name: 'AuditInfo',
		    index: 'AuditInfo',
		    search: false
		},
		{
		    label: '审核人',
		    name: 'Auditor',
		    index: 'Auditor',
		    search: false
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
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {
                        myJqGrid.setFormFieldReadOnly('ContractName', false);
                        myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                        if (gSysConfig.IsHandInputConStrengthGH == "true") {
                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                        }
                        myJqGrid.setFormFieldReadOnly('Slump', true);
                        myJqGrid.setFormFieldReadOnly('CastMode', true);
                        // myJqGrid.setFormFieldReadOnly('ConStrength', true);
                        //myJqGrid.setFormFieldReadOnly('NeedDate', true);
                        // myJqGrid.setFormFieldReadOnly('ProjectName', true);
//                        $("input[name='ContractName'] ~ button")[0].disabled = false;
                        linkManChange();
                        contractChange();
                        ProjectChange();

                        myJqGridHis.reloadGrid("1=2");
                    },
                    postCallBack: function (response) {
//                        myJqGrid.getFormField("ConsPos").val("");
//                        myJqGrid.getFormField("ConStrength")

//                        //置空输入框内容
//                        $("[name='ContractName']").val("");
//                        $("#ContractGH_CustName").val("");
//                        $("#BuildUnit").val("");
//                        $("[name='ConstructUnit']").val("");
//                        $("[name='ContractName']").val("");
//                        $("[name='ProjectAddr']").val("");
//                        $("[name='ProjectName']").val("");
//                        $("#LinkMan").val("");
//                        $("#Tel").val("");
//                        $("#ConsPos").val("");
//                        $("[name='ConStrength']").val("");
//                        $("[name='CastMode']").val("");
//                        $("[name='Slump']").val("");
//                        $("#DeliveryAddress").val("");
//                        $("[name='NeedDate']").val("");
//                        $("#PlanDate").val("");
//                        $("#PlanCube").val("");
//                        myJqGridHis.reloadGrid("1=2"); 
                     }
                });
            },
            handleEdit: function (btn) {
                var record = myJqGrid.getSelectedRecord();
                if (record && record.AuditStatus != '0') {
                    showError('该计划单已审核,不能修改');
                    return;
                }
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {
                        myJqGrid.setFormFieldValue("ContractName", record.ContractName);
//                        myJqGrid.setFormFieldValue("ProjectName", record.ProjectName);
                        //myJqGrid.setFormFieldReadOnly('ContractName', true);
                        //myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                        if (gSysConfig.IsHandInputConStrengthGH == "true") {
                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                        }
                        //myJqGrid.setFormFieldReadOnly('ConStrength', true);
                        //                            myJqGrid.setFormFieldReadOnly('Slump', true);
                        //                            myJqGrid.setFormFieldReadOnly('CastMode', true);
                        //                            myJqGrid.setFormFieldReadOnly('NeedDate', true);
                        //                            myJqGrid.setFormFieldReadOnly('ProjectName', true);
                        //$("input[name='ContractName'] ~ button")[0].disabled = true; //禁用AutoComplete控件
                        contractChange();
                        //myJqGrid.getFormField("ContractID").trigger("change");
                        linkManChange();

                        var contractId = myJqGrid.getFormField("ContractID").val();
                        getCastMode(contractId); //加载合同对应的浇筑方式
                        getConStrength(contractId); //加载合同对应的砼强度列表
                        getLinkMan(contractId); //加载合同对应前场工长列表
                    }
                });
            },
            handleDelete: function (btn) {
                var record = myJqGrid.getSelectedRecord();
                if (record && record.AuditStatus != '0') {
                    showError('该计划单已审核,不能删除');
                    return;
                }
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            handleTomorrowPlan: function (btn) {
                var d = new Date();
                var tomorrow = opts.tomorrowDate; //d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
                var condition = "PlanDate >= '" + tomorrow + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59'";
                myJqGrid.refreshGrid(condition);
            },
            handleTodayPlan: function (btn) {
                var d = new Date();
                var today = d.format("Y-m-d");
                var condition = "PlanDate >= '" + today + " 00:00:00' and PlanDate < '" + today + " 23:59:59'";
                myJqGrid.refreshGrid(condition);
            },
            handleAllPlan: function (btn) {
                myJqGrid.refreshGrid('1=1');
            },
            //审核
            handleAudit: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 1) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record.AuditStatus != '0') {
                        showMessage('该计划单已审核');
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                            myJqGrid.setFormFieldReadOnly('Slump', true);
                            myJqGrid.setFormFieldReadOnly('CastMode', true);
                            myJqGrid.setFormFieldReadOnly('NeedDate', true);
                            myJqGrid.setFormFieldReadOnly('ProjectName', true);
                        },
                        height: 480
                    });
                } else if (keys.length > 1) {
                    showConfirm("确认信息", "确认要进行批量审核操作?",
					function (btn) {
					    ajaxRequest(MultAuditUrl, {
					        ids: keys
					    },
						true,
						function () {
						    myJqGrid.reloadGrid();
						});
					    $(this).dialog("close");
					});
                } else {
                    showMessage('提示', '没有选择任何记录！');
                    return;
                }
            },
            //批量审核
            handleMultAudit: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length > 0) {
                    var requestURL = MultAuditUrl;
                    var postParams = {
                        ids: keys
                    };
                    ajaxRequest(requestURL, postParams, true,
					function (response) {
					    myJqGrid.reloadGrid();
					});
                } else {
                    showMessage('提示', '没有选择任何记录！');
                    return;
                }
            },
            //取消审核
            handleCancelAudit: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length > 0) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record.AuditStatus == '0') {
                        showError('该计划单未审核，不需要取消！');
                        return;
                    }
                    showConfirm("确认信息", "确认要进行取消审核操作?",
					function (btn) {
					    var requestURL = CancelAuditUrl;
					    var postParams = {
					        ids: keys
					    };
					    ajaxRequest(requestURL, postParams, true,
						function (response) {
						    myJqGrid.reloadGrid();
						});
					    $(this).dialog("close");
					});
                } else {
                    showMessage('提示', '没有选择任何记录！');
                    return;
                }
            }
            //复制计划单
			,
            handleCopyPlan: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showError('提示', '没有选择任何记录！');
                    return;
                }
                var record = myJqGrid.getSelectedRecord();
                if (record && record.AuditStatus != '0') {
                    showError('该计划单已审核,不能复制');
                    return;
                }
                myJqGrid.showWindow({
                    title: '复制计划单',
                    width: 200,
                    height: 380,
                    loadFrom: 'CopyPlanForm',
                    afterLoaded: function () {
                        ConStrengthGrid.refreshGrid("ContractID='" + record.ContractID + "' and AuditStatus=1 and ConStrength<>'" + record.ConStrength + "'");
                        $("#planid").html(record.ID);
                        //$("#ConsPos2").val("");
                    },
                    buttons: {
                        "关闭": function () {
                            $(this).dialog("close");
                        },
                        "确定": function () {
                            var ids = ConStrengthGrid.getSelectedKeys();
                            if (ids.length <= 0) {
                                showError('没有任何强度！');
                                $(this).dialog("close");
                                return false;
                            } else {
                                var consPos = $("#ConsPos2").val();
                                if (consPos == "") {
                                    showError('施工部位不能为空！');
                                    return false;
                                }
                                ajaxRequest("/CustomerPlanGH.mvc/CopyPlan", {
                                    planid: record.ID,
                                    ids: ids,
                                    conspos: consPos
                                },
								true,
								function (data) {
								    $("#CopyPlanForm").dialog("close");
								    myJqGrid.refreshGrid(condition);
								});
                            }

                        }
                    }
                });
                //------------------
            },
            //复制计划单(全部内容)
            handleCopyPlanAll: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showError('提示', '没有选择任何记录！');
                    return;
                }
                var record = myJqGrid.getSelectedRecord();
                if (record && record.AuditStatus != '0') {
                    showError('该计划单已审核,不能复制');
                    return;
                }
                showConfirm("确认信息", "确认要复制计划单?",
				function (btn) {
				    var requestURL = '/CustomerPlanGH.mvc/CopyPlanAll';
				    var postParams = {
				        planid: record.ID
				    };
				    ajaxRequest(requestURL, postParams, true,
					function (response) {
					    myJqGrid.refreshGrid(condition);
					});
				    $(this).dialog("close");
				});

            }
        }
    });
    //砼强度列表
    var ConStrengthGrid = new MyGrid({
        renderTo: 'ConStrengthGrid',
        autoWidth: true
        //, buttons: buttons0
		,
        height: 180,
        storeURL: '/ContractItemGH.mvc/FindConStrength',
        sortByField: 'ID',
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        singleSelect: false,
        rowNumbers: false,
        storeCondition: '1=1',
        initArray: [{
            label: ' 编号',
            name: 'ID',
            index: 'ID',
            hidden: true
        },
		{
		    label: ' 强度',
		    name: 'ConStrength',
		    index: 'ConStrength',
		    hidden: false
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                ConStrengthGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ConStrengthGrid.refreshGrid('1=1');
            },
            handleAdd: function (btn) {
                ConStrengthGrid.handleAdd({
                    loadFrom: 'ContractPumpForm',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                ConStrengthGrid.handleEdit({
                    loadFrom: 'ContractPumpForm',
                    btn: btn
                });
            },
            handleDelete: function (btn) {
                ConStrengthGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    function disableFormFields(disable) {
        myJqGrid.setFormFieldReadOnly('ProjectName', disable);
        myJqGrid.setFormFieldReadOnly('ProjectAddr', disable);
        myJqGrid.setFormFieldReadOnly('ConStrength', disable);
        myJqGrid.setFormFieldReadOnly('ConsPos', disable);
        myJqGrid.setFormFieldReadOnly('Slump', disable);
        myJqGrid.setFormFieldReadOnly('CastMode', disable);
        myJqGrid.setFormFieldReadOnly('Tel', disable);
        myJqGrid.setFormFieldReadOnly('LinkMan', disable);
    }
    //双击事件
    myJqGrid.addListeners('rowdblclick',
	function (id, record, selBool) {
	    var record = myJqGrid.getSelectedRecord();
	    if (record && record.AuditStatus != '0') {
	        showError('该计划单已审核,不能修改');
	        return;
	    }
	    myJqGrid.handleEdit({
	        loadFrom: 'MyFormDiv',
	        //getUrl: opts.storeUrl, //加载
	        title: '修改工地计划单',
	        postUrl: '/CustomerPlanGH.mvc/Update',
	        //提交
	        afterFormLoaded: function () {
	            contractChange();
	            linkManChange();
	            var contractId = myJqGrid.getFormField("ContractID").val();
	            getCastMode(contractId); //加载合同对应的浇筑方式
	            getConStrength(contractId); //加载合同对应的砼强度列表
	            getLinkMan(contractId); //加载合同对应前场工长列表
	        }
	    });
	});

    var target = $('#TaskID');
    target.append("<option value=''>新任务单</option>");
    if (typeof (tasks) != 'undefined' && tasks.length > 0) {
        var target = $('#TaskID');
        target.append("<option value=''>新任务单</option>");
        //每天的任务单都是新单子，因此不需要下拉获取旧数据
        /*
        for(var i = 0; i < tasks.length; i++) {
        var d = tasks[i];
        target.append("<option value='" + d.ID + "'>" + d.ProjectName +"-" +d.ConStrength+"-" + d.ConsPos +"-" + d.CastMode+ "</option>");
        }           
        target.bind('change', taskChanged);
        */
    }

    //根据前场工长获取电话
    window.linkManChange = function () {
        var linkManField = $('#LinkMan1'); //myJqGrid.getFormField("LinkMan1");
        linkManField.unbind('change');
        linkManField.bind('change',
		function (event) {
		    var linkMan = linkManField.val();
		    var selectedLinkMan = $('#LinkMan1', '#' + myJqGrid.getFormId()).children(':selected').text();
		    var linkManId = $('#LinkMan1', '#' + myJqGrid.getFormId()).val();
		    myJqGrid.setFormFieldValue('Tel', '');

		    if (linkManId && linkMan == selectedLinkMan) {
		        var requestURL = '/CustomerPlanGH.mvc/GetLinkManTel';
		        var postParams = {
		            contractid: myJqGrid.getFormField("ContractID").val(),
		            linkMan: linkManId
		        };
		        var rData;
		        ajaxRequest(requestURL, postParams, false,
				function (response) {
				    if (response.Result) {
				        rData = response.Data;
				        myJqGrid.getFormField("Tel").val(response.Data == null ? "" : rData);
				    }
				});
		    }
		});

        //        var linkManField = myJqGrid.getFormField("LinkMan");
        //        linkManField.unbind('change');
        //        linkManField.bind('change', function (event) {
        //            var linkMan = linkManField.val();
        //            var selectedLinkMan = $('#LinkMan', '#' + myJqGrid.getFormId()).children(':selected').text();
        //            var linkManId = $('#LinkMan', '#' + myJqGrid.getFormId()).val();
        //            myJqGrid.setFormFieldValue('Tel', '');
        //            if (linkManId && linkMan == selectedLinkMan) {
        //                var requestURL = opts.getUserUrl;
        //                var postParams = { id: linkManId };
        //                var rData;
        //                ajaxRequest(requestURL, postParams, false, function (response) {
        //                    if (response) {
        //                        rData = response.Data;
        //                        myJqGrid.getFormField("Tel").val(response.Data == null ? "" : rData.Tel);
        //                    }
        //                });
        //            }
        //        });
    };

    //自动设置施工单位为客户名称
    contractChange = function () {
        var contractIdField = myJqGrid.getFormField("ContractID");
        contractIdField.unbind('change');
        contractIdField.bind('change',
		function () {
		    //myJqGrid.setFormFieldValue('ProjectName', myJqGrid.getFormFieldValue("ContractName"));
		    //myJqGrid.getFormField('ProjectName').focus();
		    $('#ProjectName').empty();
		    myJqGrid.getFormField("ProjectName").val('');
		    myJqGrid.getFormField("ProjectAddr").val('');
		    var contractId = contractIdField.val();

		    if (!isEmpty(contractId)) {
		        //bindSelectData($('[name="ConStrength"]'),
		        getCastMode(contractId); //加载合同对应的浇筑方式
		        getConStrength(contractId); //加载合同对应的砼强度列表
		        getLinkMan(contractId); //加载合同对应前场工长列表
		        //加载工程列表
		        bindSelectData($('#ProjectName'), '/ProjectGH.mvc/ListData1', {
		            textField: 'ProjectName',
		            valueField: 'ID',
		            condition: "ContractID='" + contractId + "'"
		        },
				function (data) {
				    var count = data.length;
				    if (count > 0) {
				        $('#ProjectName').children().first().remove(); //施工单位清掉
				        //获取加
				        ajaxRequest('/ProjectGH.mvc/GetProjectName', {
				            id: contractId
				        },
						false,
						function (response) {
						    if (response) {
						        var name = response.Name;

						        $('[name="ProjectName"]').val(name);
						        if (response.ProjectId) {
						            var requestURL = '/ProjectGH.mvc/Get';
						            var postParams = {
						                id: response.ProjectId
						            };
						            var rData;
						            ajaxRequest(requestURL, postParams, false,
									function (response) {
									    if (response.Result) {
									        rData = response.Data;
									        myJqGrid.getFormField("ProjectAddr").val(response.Data == null ? "" : rData.ProjectAddr);
									        $("#BuildUnit").val(response.Data == null ? "" : rData.BuildUnit);
									        $("#ConstructUnit").val(response.Data == null ? "" : rData.ConstructUnit);
									        $("#LinkMan").val(response.Data == null ? "" : rData.LinkMan);
									        $("#Tel").val(response.Data == null ? "" : rData.Tel);
									    }
									});
						        }
						        //
						    }
						})
				    }
				}

				);
//mygrid.setFormFieldValue('ConstructUnit', mygrid.getFormFieldValue('ContractGH_CustName'));
		        var requestURL = "/ContractGH.mvc/Get";
		        var postParams = {
		            id: contractId
		        };
		        var rData;
		        ajaxRequest(requestURL, postParams, false,
				function (response) {
				    if (response) {
				        rData = response.Data;
				        //myJqGrid.getFormField("ConstructUnit").val(response.Data == null ? "" : rData.CustName);                      
				        //myJqGrid.getFormField("ProjectAddr").val(response.Data == null ? "" : rData.ProjectAddr);
				        $("#ContractNo").val(response.Data == null ? "" : rData.ContractNo); //合同号
				        $("#ContractGH_CustName").val(response.Data == null ? "" : rData.CustName); //合同名称
                        console.log("ContractGH_CustName"); 
                        console.log($("#ContractGH_CustName").val()); 
				        myJqGrid.getFormField("Tel").val(response.Data == null ? "" : rData.BLinkTel);
				        ////加载第一个工程名称
				        //$('#ProjectName').get(0).selectedIndex = 0;
				        //alert($('#ProjectName').get(0).selectedIndex);
				    }
				});

		        $('[name="LinkMan"]').val("");
		    }
		});
    };

    //加载合同对应的砼强度列表
    function getConStrength(contractId) {
        bindSelectData($('#ConStrength'), '/CustomerPlanGH.mvc/ListDataStrengthInfo', {
            textField: 'ConStrength',
            valueField: 'ConStrength',
            condition: " ContractID='" + contractId + "' and AuditStatus=1"
        });
    }

    //加载合同对应的浇筑方式
    function getCastMode(contractId) {
        bindSelectData($('[name="CastMode"]'), '/ContractPump.mvc/ListData', {
            textField: 'PumpType',
            valueField: 'PumpType',
            condition: " ContractID='" + contractId + "'"
        });
    }

    //加载合同对应的历史前场工长(必须是审核的计划单)
    function getLinkMan(contractId) {
        //bindSelectData($('[name="LinkMan"]'),
        bindSelectData($('#LinkMan1'), '/CustomerPlanGH.mvc/ListDataLinkMan', {
            textField: 'LinkMan',
            valueField: 'LinkMan',
            condition: " ContractID='" + contractId + "' AND LinkMan IS NOT null and  AuditStatus=1 and lifecycle<>-1"
        });
    }

    //根据工程显示对应信息
    window.ProjectChange = function () {
        var ProjectNameField = myJqGrid.getFormField("ProjectName");
        ProjectNameField.unbind('change');
        ProjectNameField.bind('change',
		function (event) {
		    var ProjectName = ProjectNameField.val();
		    var selectedProjectName = $('#ProjectName', '#' + myJqGrid.getFormId()).children(':selected').text();
		    var ProjectId = $('#ProjectName', '#' + myJqGrid.getFormId()).val();
		    myJqGrid.getFormField("ProjectAddr").val('');
		    //myJqGridHis.reloadGrid("ProjectName='" + selectedProjectName + "' and AuditStatus=1"); //加载工程对应的历史计划单
		    if (ProjectId) {
		        var requestURL = '/ProjectGH.mvc/Get';
		        var postParams = {
		            id: ProjectId
		        };
		        var rData;
		        ajaxRequest(requestURL, postParams, false,
				function (response) {
				    if (response.Result) {
				        rData = response.Data;
				        myJqGrid.getFormField("ProjectAddr").val(response.Data == null ? "" : rData.ProjectAddr);
				        $("#BuildUnit").val(response.Data == null ? "" : rData.BuildUnit);
				        $("#ConstructUnit").val(response.Data == null ? "" : rData.ConstructUnit);
				        $("#LinkMan").val(response.Data == null ? "" : rData.LinkMan);
				        $("#Tel").val(response.Data == null ? "" : rData.Tel);

				        myJqGridHis.reloadGrid("1=2");
				    }
				});
		    }
		});
    };

    var myJqGridHis = new MyGrid({
        renderTo: 'MyJqGridHis',
        width: 500
        //, autoWidth: 200
        //, buttons: buttons9
		,
        height: 350,
        storeURL: opts.storeUrl,
        sortByField: 'NeedDate, ConstructUnit ',
        primaryKey: 'ID',
        setGridPageSize: 50,
        dialogWidth: 710,
        dialogHeight: 500,
        showPageBar: true,
        singleSelect: true,
        storeCondition: 'AuditStatus=1'
        //, groupField: 'PlanDate'
        //, groupingView: { groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], groupSummary: [true], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		,
        initArray: [{
            label: '工地计划编号',
            name: 'ID',
            index: 'ID',
            width: 100,
            hidden: true
        },
		{
		    label: '审核状态',
		    name: 'AuditStatus',
		    index: 'AuditStatus',
		    search: false,
		    hidden: true,
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: 'AuditStatus'
		    },
		    width: 60
		},
		{
		    label: '合同号',
		    name: 'ContractNo',
		    index: 'ContractNo',
		    width: 100,
		    hidden: true
		},
		{
		    label: '操作',
		    name: 'act',
		    index: 'act',
		    width: 40,
		    sortable: false,
		    align: "center",
		    hidden: false
		},
		{
		    label: '工程名称',
		    name: 'ProjectName',
		    index: 'ProjectName'
		},
		{
		    label: '工地电话',
		    name: 'Tel',
		    index: 'Tel',
		    width: 60
		},
		{
		    label: '工地联系人',
		    name: 'LinkMan',
		    index: 'LinkMan',
		    width: 60
		},
		{
		    label: '合同编号',
		    name: 'ContractID',
		    index: 'ContractID',
		    width: 80,
		    hidden: true
		},
		{
		    label: '合同名称',
		    name: 'ContractName',
		    index: 'ContractName',
		    hidden: true
		},
		{
		    label: '建设单位',
		    name: 'BuildUnit',
		    index: 'BuildUnit'
		},
		{
		    label: '施工单位',
		    name: 'ConstructUnit',
		    index: 'ConstructUnit'
		},
		{
		    label: '项目地址',
		    name: 'ProjectAddr',
		    index: 'ProjectAddr'
		},
		{
		    label: '交货地址',
		    name: 'DeliveryAddress',
		    index: 'DeliveryAddress'
		},
		{
		    label: '供应单位',
		    name: 'SupplyUnit',
		    index: 'SupplyUnit',
		    hidden: true
		},
		{
		    label: '施工部位',
		    name: 'ConsPos',
		    index: 'ConsPos',
		    width: 60,
		    hidden: true
		},
		{
		    label: '强度',
		    name: 'ConStrength',
		    index: 'ConStrength',
		    width: 60,
		    hidden: true
		},
		{
		    label: '坍落度',
		    name: 'Slump',
		    index: 'Slump',
		    width: 60,
		    hidden: true
		},
		{
		    label: '浇筑方式(干混)',
		    name: 'CastMode',
		    index: 'CastMode',
		    width: 60,
		    hidden: true
		},
		{
		    label: '泵名称',
		    name: 'PumpName',
		    index: 'PumpName',
		    width: 100,
		    hidden: true
		},
		{
		    label: '计划量',
		    name: 'PlanCube',
		    index: 'PlanCube',
		    width: 60,
		    summaryType: 'sum',
		    hidden: true,
		    summaryTpl: '合计: <font color=red>{0}</font>'
		},
		{
		    label: '计划日期',
		    name: 'PlanDate',
		    index: 'PlanDate',
		    formatter: 'date',
		    search: false,
		    sortable: false,
		    width: 80
		},
		{
		    label: '到场时间',
		    name: 'NeedDate',
		    index: 'NeedDate',
		    search: false,
		    width: 60,
		    hidden: true
		},
		{
		    label: '区间',
		    name: 'RegionID',
		    index: 'RegionID',
		    width: 80,
		    hidden: true
		},
		{
		    label: '开盘安排',
		    name: 'PiepLen',
		    index: 'PiepLen',
		    width: 50,
		    hidden: true
		},
		{
		    label: '泵工',
		    name: 'PumpMan',
		    index: 'PumpMan',
		    width: 60,
		    hidden: true
		},
		{
		    label: '任务单号',
		    name: 'TaskID',
		    index: 'TaskID',
		    hidden: true
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    hidden: true
		},
		{
		    label: '审核时间',
		    name: 'AuditTime',
		    index: 'AuditTime',
		    formatter: 'date',
		    search: false,
		    hidden: true
		},
		{
		    label: '审核意见',
		    name: 'AuditInfo',
		    index: 'AuditInfo',
		    search: false,
		    hidden: true
		},
		{
		    label: '审核人',
		    name: 'Auditor',
		    index: 'Auditor',
		    search: false,
		    hidden: true
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridHis.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridHis.refreshGrid();
            },
            handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    closeDialog: false,
                    afterFormLoaded: function () { },
                    beforeSubmit: function () {

                    },
                    postCallBack: function (response) {

                    }
                });
            },
            handleEdit: function (btn) {
                var record = myJqGrid.getSelectedRecord();
                if (record && record.AuditStatus != '0') {
                    showError('该计划单已审核,不能修改');
                    return;
                }
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {

                    },
                    beforeSubmit: function () { }
                });
            },
            handleDelete: function (btn) {
                var record = myJqGrid.getSelectedRecord();
                if (record && record.AuditStatus != '0') {
                    showError('该计划单已审核,不能删除');
                    return;
                }
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    myJqGridHis.addListeners('rowdblclick',
	function (id, record, selBool) {
	    $("#ProjectAddr").val(record.ProjectAddr);
	    $("#DeliveryAddress").val(record.DeliveryAddress);
	    $("#LinkMan").val(record.LinkMan);
	    $("#Tel").val(record.Tel);

	});
    //加载工程对应的历史计划单
    window.SearchProHis = function () {
        //var ProjectId = $('#ProjectName', '#' + myJqGrid.getFormId()).val();
        var projectname = $("[name='ProjectName']").val();
        myJqGridHis.reloadGrid("ProjectName='" + projectname + "' and AuditStatus=1 and lifecycle<>-1");
    }
    //grid行操作栏按钮
    myJqGridHis.addListeners("gridComplete",
	function () {
	    var records = myJqGridHis.getAllRecords();
	    var rid, buildtime, status;
	    for (var i = 0; i < records.length; i++) {
	        rid = records[i].ID;
	        status = records[i].AuditStatus;
	        buildtime = records[i].BuildTime;
	        ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeletePlan('" + rid + "');\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='移除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
	        myJqGridHis.getJqGrid().jqGrid('setRowData', rid, {
	            act: ce
	        });
	    }
	});
    window.handleDeletePlan = function (id) {
        showConfirm("确认信息", "您确定移除此项历史记录吗？",
		function () {
		    $.post('/CustomerPlanGH.mvc/RemovePlan', {
		        planid: id
		    },
			function (data) {
			    if (!data.Result) {
			        showError("出错啦！", data.Message);
			        return false;
			    }
			    var projectname = $("[name='ProjectName']").val();
			    myJqGridHis.reloadGrid("ProjectName='" + projectname + "' and AuditStatus=1 and lifecycle<>-1");
			}

			);
		    $(this).dialog("close");
		});
    };
}