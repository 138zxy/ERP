﻿function customerplanIndexInit(opts) {
    function bindIdentitySettings(container, contractId, customerPlanId) {
        var container = $(container);
        if (isEmpty(contractId)) return;
        ajaxRequest('/IdentitySetting.mvc/FindIdentitySettings1',
        { contractId: contractId, customerPlanId: customerPlanId },
        false,
        function (response) {
            container.empty();
            for (var i = 0; i < response.length; i++) {
                var item = response[i];
                var identDiv = $("<div>").appendTo(container)
                                    .attr("id", "Ident_" + i)
                                    .addClass('identbox ui-corner-all')
                                    .append('<div class="titlebar ui-widget-header ui-corner-all ui-helper-clearfix">' + item.IdentType.DicName + '</div>');
                identDiv.checkboxlist({ data: item.IdentItems });
            }
        });
    };

    function LiftFmt(cellvalue, options, rowObject) {
        var style = "color:green;";
        var txt = '正常';
        if (cellvalue == -1) {
            style = "color:red;";
            var txt = '作废';
        }

        if (cellvalue == 1) {
            style = "color:blue;";
            var txt = '已审核';
        }

        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }

    function LiftUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }

    var d = new Date();
    var tomorrow = opts.tomorrowDate; //  d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
    var today = d.format("Y-m-d");
    var condition = "PlanDate >= '" + today + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59' and lifecycle<>-1";
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
		,
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl,
        sortByField: ' PlanDate desc, ID',
        primaryKey: 'ID',
        setGridPageSize: 100,
        dialogWidth: 600,
        dialogHeight: 650,
        showPageBar: true,
        advancedSearch: true,
        storeCondition: condition + ' AND DataType=0 ',
        groupField: 'PlanDate',
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
            label: '客户名称',
            name: 'Contract.Customer.CustName',
            index: 'Contract.Customer.CustName'
        },
		
		{
		    label: '工程名称',
		    name: 'ProjectName',
		    index: 'ProjectName'
		},
		
		
		{
		    label: '施工部位',
		    name: 'ConsPos',
		    index: 'ConsPos',
		    width: 120
		},
		{
		    label: '砼强度',
		    name: 'ConStrength',
		    index: 'ConStrength',
		    width: 60
		}
        ,
		{
		    label: '坍落度',
		    name: 'Slump',
		    index: 'Slump',
		    width: 60
		},
		{
		    label: '浇筑方式',
		    name: 'CastMode',
		    index: 'CastMode',
		    width: 60
		},
                {
		    label: '工地电话',
		    name: 'Tel',
		    index: 'Tel',
                     width: 80
		},
		{
		    label: '计划方量',
		    name: 'PlanCube',
		    index: 'PlanCube',
		    width: 60,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>'
		},
		{
		    label: '计划日期',
		    name: 'PlanDate',
		    index: 'PlanDate',
		    //formatter: 'date',
		    //search: false,
		    //sortable: false,
		    formatter: 'date',
		    //formatoptions: { newformat: "ISO8601Long" }, 
            searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']},
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
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark'
		},
        { label: '其它要求', name: 'OtherDemand', index: 'OtherDemand' },
		{
		    label: '运距',
		    name: 'Distance',
		    index: 'Distance',
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
		    width: 60
		},
		{
		    label: '合同编号',
		    name: 'ContractID',
		    index: 'ContractID'
		},
                {
		    label: '合同名称',
		    name: 'ContractName',
		    index: 'ContractName', search: false
		},
		{
		    label: '任务单号',
		    name: 'TaskID',
		    index: 'TaskID'
		},
		
		{
		    label: '审核时间',
		    name: 'AuditTime',
		    index: 'AuditTime',
		    formatter: 'date',
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
		},
		{
		    label: '状态',
		    name: 'Lifecycle',
		    index: 'Lifecycle',
		    formatter: LiftFmt,
		    unformat: LiftUnFmt,
		    width: 60
		}, 
        { label: "级配",
		    name: "CarpGrade",
		    index: "CarpGrade",
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: { parentId: "JP" },
		    stype: "select",
		    searchoptions: { value: dicToolbarSearchValues("JP") },
		    width: 100,
		    align: "center"
		},
		{
		    label: '泵名称',
		    name: 'PumpName',
		    index: 'PumpName',
		    width: 100
		},
         {
		    label: '施工单位',
		    name: 'ConstructUnit',
		    index: 'ConstructUnit'
		},
           {
		    label: '供应单位',
		    name: 'SupplyUnit',
		    index: 'SupplyUnit'
		},
           {
            label: '项目编号',
            name: 'TaskNo',
            index: 'TaskNo'
        } ,
        {
		    label: '项目地址',
		    name: 'ProjectAddr',
		    index: 'ProjectAddr'
		},
        { label: '特性参数', name: 'IdentityValue', index: 'IdentityValue', width: 60 }
        ,
        {
            label: '子分部工程',
            name: 'ChildProject',
            index: 'ChildProject'
        },
        {
            label: "计划附件",
            name: "Attachments",
            formatter: attachmentFmt,
            sortable: false,
            search: false
        },
        {
            label: '异常信息',
            name: 'ExceptionInfo',
            index: 'ExceptionInfo',
            width: 150
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
                    closeDialog: false,
                    afterFormLoaded: function () {
                        myJqGrid.setFormFieldReadOnly('ContractName', false);
                        myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                        //                        if (gSysConfig.IsHandInputConStrength == "true") {
                        //                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                        //                        }
                        myJqGrid.setFormFieldReadOnly('Slump', true);
                        myJqGrid.setFormFieldDisabled('AuditStatus', true);
                        myJqGrid.setFormFieldReadOnly('CastMode', false);
                        myJqGrid.setFormFieldReadOnly('NeedDate', true);
                        // myJqGrid.setFormFieldReadOnly('ProjectName', true);
                        myJqGrid.setFormFieldReadOnly('ProjectName', false);
                        $("input[name='ContractName'] ~ button")[0].disabled = false;
                        document.getElementById("tr_AuditStatus").style.display = "none";
                        //$("input[name='ProjectName']")[0].nextSibling.disabled = false;
                        linkManChange();
                        contractChange();
                        projectChange();

                        $('#Attachments').empty();
                    },
                    //提交之前检查是否有添加附件
                    beforeSubmit: function () {
                        //如果全局配置没有设置“工地计划或者生产任务强制需要上传附件”
                        if (gSysConfig.MustCustomerPlanNeedAtt == "false") { return true; }

                        var fileElement = $('#uploadFile');
                        if (fileElement.val() == "") {
                            showMessage("提示", "新建工地计划请至少上传一个附件！");
                            return false;
                        }

                        return true;
                    },
                    postCallBack: function (response) {
                        myJqGrid.getFormField("ConsPos").val("");
                        //myJqGrid.getFormField("ConStrength").val("");

                        //保存特性
                        if (response.Result) {
                            var uid = response.Data;

                            var identities = [];
                            $('#rightdiv input:checked').each(function () {
                                identities.push($(this).val());
                            });

                            ajaxRequest('/CustomerPlan.mvc/SaveCustPlanIdentities', { custPlanId: uid, identities: identities }, false, function (response) {
                                if (!response.Result) {
                                    alert(response.Message);
                                };
                                // $("#MyFormDiv").dialog('close');
                                myJqGrid.refreshGrid();
                            });

                            //上传附件
                            attachmentUpload(response.Data);
                            $('#uploadFile').val("");
                        }
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
                        //myJqGrid.setFormFieldReadOnly('ContractName', true);
                        myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                        if (gSysConfig.IsHandInputConStrength == "true") {
                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                        }

                        //$("input[name='ProjectName']")[0].nextSibling.disabled = false;
                        myJqGrid.setFormFieldReadOnly('Slump', true);
                        myJqGrid.setFormFieldDisabled('AuditStatus', true);
                        myJqGrid.setFormFieldReadOnly('CastMode', false);
                        myJqGrid.setFormFieldReadOnly('NeedDate', true);
                        myJqGrid.setFormFieldReadOnly('ProjectName', false);
                        document.getElementById("tr_AuditStatus").style.display = "none";
                        //myJqGrid.setFormFieldReadOnly('ProjectName', true);
                        //$("input[name='ContractName'] ~ button")[0].disabled = true; //禁用AutoComplete控件
                        linkManChange();
                        contractChange();
                        projectChange();
                        var contractId = myJqGrid.getFormField("ContractID").val();
                        getCastMode(contractId); //加载合同对应的浇筑方式
                        getConStrength(contractId); //加载合同对应的砼强度列表

                        //特性
                        bindIdentitySettings('#rightdiv', contractId, record.ID);

                        //显示附件
                        var attDiv = $('#Attachments');
                        attDiv.empty();
                        attDiv.append(record["Attachments"]);
                        $('a[att-id]').show();
                    },
                    postCallBack: function (response) {
                        //保存特性
                        if (response.Result) {
                            var identities = [];
                            $('#rightdiv input:checked').each(function () {
                                identities.push($(this).val()); //选择的都压人数组.ar identities = []和int[] identities一样.
                            });

                            ajaxRequest('/CustomerPlan.mvc/SaveCustPlanIdentities', { custPlanId: record.ID, identities: identities }, false, function (response) {
                                if (!response.Result) {
                                    alert(response.Message);
                                };
                                $("#MyFormDiv").dialog('close');
                                myJqGrid.refreshGrid();
                            });

                            attachmentUpload(record.ID);
                        }
                    },
                    //提交之前检查是否有添加附件
                    beforeSubmit: function () {
                        //如果全局配置没有设置“工地计划或者生产任务强制需要上传附件”
                        if (gSysConfig.MustCustomerPlanNeedAtt == "false") { return true; }

                        //检查是否重新上传附件
                        var isAddAtt = true;
                        var fileElement = $('#uploadFile');
                        if (fileElement.val() == "") {
                            isAddAtt = false;
                        }
                        //检查编辑前是否已经有附件存在
                        var alreadyExitsAtt = true;
                        //console.log($("a[href]", "#MyFormDiv").length);
                        if ($("a[href]", "#MyFormDiv").length < 1) {
                            alreadyExitsAtt = false;
                        }
                        //console.log(isAddAtt || alreadyExitsAtt);
                        if (!(isAddAtt || alreadyExitsAtt)) {
                            showMessage("提示", "修改工地计划请至少上传一个附件！");
                        }

                        return isAddAtt || alreadyExitsAtt;
                    }
                });
            },
            handleAddCopy: function (btn) {
                var record = myJqGrid.getSelectedRecord();
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {
                        myJqGrid.setFormFieldValue("ID", '');
                        myJqGrid.setFormFieldValue("ContractName", record.ContractName);
                        myJqGrid.setFormFieldReadOnly('ContractName', false);
                        myJqGrid.setFormFieldReadOnly('SupplyUnit', false);

                        if (gSysConfig.IsHandInputConStrength == "true") {
                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                        }

                        //$("input[name='ProjectName']")[0].nextSibling.disabled = false;
                        myJqGrid.setFormFieldReadOnly('Slump', false);
                        myJqGrid.setFormFieldValue("AuditStatus", 0);
                        myJqGrid.setFormFieldDisabled('AuditStatus', true);
                        myJqGrid.setFormFieldReadOnly('CastMode', false);
                        myJqGrid.setFormFieldValue("NeedDate", '');
                        myJqGrid.setFormFieldReadOnly('NeedDate', false);
                        myJqGrid.setFormFieldReadOnly('ProjectName', false);
                        document.getElementById("tr_AuditStatus").style.display = "none";
                        //myJqGrid.setFormFieldReadOnly('ProjectName', true);
                        //$("input[name='ContractName'] ~ button")[0].disabled = true; //禁用AutoComplete控件
                        linkManChange();
                        contractChange();
                        projectChange();
                        var contractId = myJqGrid.getFormField("ContractID").val();
                        getCastMode(contractId); //加载合同对应的浇筑方式
                        getConStrength(contractId); //加载合同对应的砼强度列表

                        //特性
                        bindIdentitySettings('#rightdiv', contractId, record.ID);

                        //显示附件
                        $('#Attachments').empty();
                    },
                    postCallBack: function (response) {
                        //保存特性
                        if (response.Result) {
                            var identities = [];
                            $('#rightdiv input:checked').each(function () {
                                identities.push($(this).val()); //选择的都压人数组.ar identities = []和int[] identities一样.
                            });

                            ajaxRequest('/CustomerPlan.mvc/SaveCustPlanIdentities', { custPlanId: record.ID, identities: identities }, false, function (response) {
                                if (!response.Result) {
                                    alert(response.Message);
                                };
                                $("#MyFormDiv").dialog('close');
                                myJqGrid.refreshGrid();
                            });

                            attachmentUpload(record.ID);
                        }
                    },
                    //提交之前检查是否有添加附件
                    beforeSubmit: function () {
                        //如果全局配置没有设置“工地计划或者生产任务强制需要上传附件”
                        if (gSysConfig.MustCustomerPlanNeedAtt == "false") { return true; }

                        //检查是否重新上传附件
                        var isAddAtt = true;
                        var fileElement = $('#uploadFile');
                        if (fileElement.val() == "") {
                            isAddAtt = false;
                        }
                        //检查编辑前是否已经有附件存在
                        var alreadyExitsAtt = true;
                        //console.log($("a[href]", "#MyFormDiv").length);
                        if ($("a[href]", "#MyFormDiv").length < 1) {
                            alreadyExitsAtt = false;
                        }
                        //console.log(isAddAtt || alreadyExitsAtt);
                        if (!(isAddAtt || alreadyExitsAtt)) {
                            showMessage("提示", "修改工地计划请至少上传一个附件！");
                        }

                        return isAddAtt || alreadyExitsAtt;
                    }
                });
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
                            myJqGrid.setFormFieldDisabled('AuditStatus', false);
                            myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                            myJqGrid.setFormFieldReadOnly('Slump', true);
                            myJqGrid.setFormFieldReadOnly('CastMode', true);
                            myJqGrid.setFormFieldReadOnly('NeedDate', true);
                            myJqGrid.setFormFieldReadOnly('ProjectName', true);
                            //myJqGrid.setFormFieldDisabled('ProjectName', true);
                            //myJqGrid.setFormFieldDisabled('ConstructUnit', true);
                            document.getElementById("tr_AuditStatus").style.display = "inline";
                            //特性
                            bindIdentitySettings('#rightdiv', record.ContractID, record.ID);

                            //$("input[name='ProjectName']")[0].nextSibling.disabled = true;

                            //审核的选项删选
                            $($("#AuditStatus", "#MyFormDiv").children("option[value='2']")).css('display', 'none');
                            $($("#AuditStatus", "#MyFormDiv").children("option[value='3']")).css('display', 'none');
                            $($("#AuditStatus", "#MyFormDiv").children("option[value='4']")).css('display', 'none');

                            //选中
                            $("#AuditStatus", "#MyFormDiv").val('1');


                            //显示附件
                            var attDiv = $('#Attachments');
                            attDiv.empty();
                            attDiv.append(record["Attachments"]);
                        }
                    });
                }
                else if (keys.length > 1) {
                    showConfirm("确认信息", "确认要进行批量审核操作?", function (btn) {
                        ajaxRequest(opts.MultAuditUrl, { ids: keys }, true, function () {
                            myJqGrid.reloadGrid();
                        });
                        $(this).dialog("close");
                    });
                }
                else {
                    showMessage('提示', '没有选择任何记录！');
                    return;
                }
            },
            //批量审核
            handleMultAudit: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length > 0) {
                    var requestURL = opts.MultAuditUrl;
                    var postParams = { ids: keys };
                    ajaxRequest(requestURL, postParams, true, function (response) {
                        myJqGrid.reloadGrid();
                    });
                }
                else {
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
                    showConfirm("确认信息", "确认要进行取消审核操作?", function (btn) {
                        var requestURL = opts.CancelAuditUrl;
                        var postParams = { ids: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            myJqGrid.reloadGrid();
                        });
                        $(this).dialog("close");
                    });
                }
                else {
                    showMessage('提示', '没有选择任何记录！');
                    return;
                }
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
            //工地计划报表
            handleReport: function (btn) {
                window.open("/Reports/Produce/R310604.aspx");
            },
            handleTomorrowPlan: function (btn) {
                var d = new Date();
                var tomorrow = opts.tomorrowDate; //d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
                var condition = "PlanDate >= '" + tomorrow + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59'  and lifecycle<>-1 AND DataType=0 ";
                myJqGrid.refreshGrid(condition);
            },
            handleTodayPlan: function (btn) {
                var d = new Date();
                var today = d.format("Y-m-d");
                var condition = "PlanDate >= '" + today + " 00:00:00' and PlanDate < '" + today + " 23:59:59'  and lifecycle<>-1 AND DataType=0 ";
                myJqGrid.refreshGrid(condition);
            },
            handleAllPlan: function (btn) {
                myJqGrid.refreshGrid('1=1 AND DataType=0 ');
            },
            handleZf: function (btn) { //作废
                //确认操作
                showConfirm("确认信息", "确定要作废此记录吗？",
				function () {
				    var data = myJqGrid.getSelectedRecord();
				    var requestURL = '/CustomerPlan.mvc/Update';
				    var postParams = {
				        id: data.ID,
				        Lifecycle: -1
				    };
				    ajaxRequest(requestURL, postParams, true,
					function (response) {
					    myJqGrid.reloadGrid();
					});
				    $(this).dialog("close");
				});

            },
            handleUnZf: function (btn) { //取消作废
                showConfirm("确认信息", "确定要取消作废此记录吗？",
				function () {
				    var data = myJqGrid.getSelectedRecord();
				    var requestURL = '/CustomerPlan.mvc/Update';
				    var postParams = {
				        id: data.ID,
				        Lifecycle: 0
				    };
				    ajaxRequest(requestURL, postParams, true,
					function (response) {
					    myJqGrid.reloadGrid();
					});
				    $(this).dialog("close");
				});
            },
            handleSZf: function (btn) {
                var condition = "lifecycle=-1 AND DataType=0 "; //已作废
                myJqGrid.refreshGrid(condition);
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

    window.linkManChange = function () {
        var linkManField = myJqGrid.getFormField("LinkMan");
        linkManField.unbind('change');
        linkManField.bind('change',
		function (event) {
		    var linkMan = linkManField.val();
		    var selectedLinkMan = $('#LinkMan', '#' + myJqGrid.getFormId()).children(':selected').text();
		    var linkManId = $('#LinkMan', '#' + myJqGrid.getFormId()).val();
		    myJqGrid.setFormFieldValue('Tel', '');

		    if (linkManId && linkMan == selectedLinkMan) {
		        var requestURL = opts.getUserUrl;
		        var postParams = {
		            id: linkManId
		        };
		        var rData;
		        ajaxRequest(requestURL, postParams, false,
				function (response) {
				    if (response) {
				        rData = response.Data;
				        myJqGrid.getFormField("Tel").val(response.Data == null ? "" : rData.Tel);
				    }
				});
		    }
		});
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
		    var contractId = contractIdField.val();

		    console.log(111);
		    $('#rightdiv').empty();

		    if (!isEmpty(contractId)) {

		        bindIdentitySettings('#rightdiv', contractId);

		        //bindSelectData($('[name="ConStrength"]'),
		        getCastMode(contractId); //加载合同对应的浇筑方式
		        getConStrength(contractId); //加载合同对应的砼强度列表
		        bindSelectData($('#ProjectName'), '/Project.mvc/ListData1', {
		            textField: 'ProjectName',
		            valueField: 'ID',
		            condition: " isused=1 and  ContractID='" + contractId + "'"
		        },
				function (data) {
				    var count = data.length;
				    if (count > 0) {
				        $('#ProjectName').children().first().remove(); //施工单位清掉
				        //获取加
				        var projectid = data[0].Value;
				        ajaxRequest('/Project.mvc/Get', {
				            id: projectid
				        },
						false,
						function (response) {
						    if (response.Result) {
						        $('[name="ProjectName"]').val(response.Data.ProjectName);
						        $('[name="ProjectIDApp"]').val(response.Data.ID);
						        $('[name="ProjectAddr"]').val(response.Data.ProjectAddr);
						        $('[name="ConstructUnit"]').val(response.Data.ConstructUnit);
						        $('[name="LinkMan"]').val(response.Data.LinkMan);
						        $('[name="Tel"]').val(response.Data.Tel);
						        $('[name="Distance"]').val(response.Data.Distance);
						        $("#Contract_CustName").val(response.Data == null ? "" : response.Data.CustName);
						    }
						})
				    }
		            else {
		                ajaxRequest('/Contract.mvc/Get', {
		                    id: contractId
		                }, false, function (response) {
		                    if (response.Result) {
		                        $('[name="ConstructUnit"]').val(response.Data.CustName);
		                        $("#Contract_CustName").val(response.Data == null ? "" : response.Data.CustName);
		                        $('[name="ProjectAddr"]').val('');
		                    }
		                })
                    }
				}

				);

		    }
		});
    };

    projectChange = function () {
        var ProjectNameField = myJqGrid.getFormField("ProjectName");
        ProjectNameField.unbind('change');
        ProjectNameField.bind('change',
            function () {
                var projectid = $('#ProjectName').val();
                var str = $('input[name="ProjectName"]').val();//获取当前工程名称输入框的值
                var IsNewProjectName = true;
                $("#ProjectName option").each(function () { //遍历全部option
                    var txt = $(this).text(); //获取option的内容
                    if (txt == str) //如果不是“全部”
                    {
                        IsNewProjectName = false;
                    }
                });
                if (IsNewProjectName) {
//                    $('[name="ProjectIDApp"]').val('');
//                    $('[name="ProjectAddr"]').val('');
//                    $('[name="ConstructUnit"]').val('');
//                    $('[name="Tel"]').val('');
//                    $('[name="Distance"]').val('');
                } else { 
                    ajaxRequest('/Project.mvc/Get', {
                        id: projectid
                    },
						    false,
						    function (response) {
						        if (response.Result) {
						            $('[name="ProjectName"]').val(response.Data.ProjectName);
						            $('[name="ProjectIDApp"]').val(response.Data.ID);
						            $('[name="ProjectAddr"]').val(response.Data.ProjectAddr);
						            $('[name="ConstructUnit"]').val(response.Data.ConstructUnit);
						            $('[name="LinkMan"]').val(response.Data.LinkMan);
						            $('[name="Tel"]').val(response.Data.Tel);
						            $('[name="Distance"]').val(response.Data.Distance);
						        }
						    })
		        }
            })
    }
    //加载合同对应的砼强度列表
function getConStrength(contractId) {
    var swhere = " IsBase=1";
    if (gSysConfig.IsALLConStrength != "true") {
        console.log(gSysConfig.IsALLConStrength);
        swhere += " AND IsSH=0  AND ConStrengthCode IN (SELECT ConStrength FROM ContractItems WHERE ContractID='" + contractId + "')";
        }
        bindSelectData($('#ConStrength'), '/CustomerPlan.mvc/ListDataStrengthInfo', {
            textField: 'ConStrengthCode',
            valueField: 'ConStrengthCode',
            //condition: " ContractID='" + contractId + "'"
            condition: swhere
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



    function attachmentUpload(objectId) {
        var fileElement = $('#uploadFile');
        if (fileElement.val() == "") return;
        $.ajaxFileUpload({
            url: opts.uploadUrl + '?objectType=CustomerPlan&objectId=' + objectId,
            secureuri: false,
            fileElementId: 'uploadFile',
            dataType: 'json',
            beforeSend: function () {
                $("#loading").show();
            },
            complete: function () {
                $("#loading").hide();
            },
            success: function (data, status) {
                if (data.Result) {
                    showMessage('附件上传成功');
                    myJqGrid.reloadGrid();
                } else {
                    showError('附件上传失败', data.Message);
                }
            },
            error: function (data, status, e) {
                showError(e);
            }
        });
        return false;

    }

    //rowdblclick 事件定义
    myJqGrid.addListeners('rowdblclick', function (id, record, rowIndex, cellIndex) {
        var record = myJqGrid.getSelectedRecord();
        myJqGrid.handleBrowse({
            loadFrom: 'MyFormDiv',
            afterFormLoaded: function () {
                myJqGrid.setFormFieldValue("ContractName", record.ContractName);
                //myJqGrid.setFormFieldReadOnly('ContractName', true);
                myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                if (gSysConfig.IsHandInputConStrength == "true") {
                    myJqGrid.setFormFieldReadOnly('ConStrength', true);
                }

                //$("input[name='ProjectName']")[0].nextSibling.disabled = false;
                myJqGrid.setFormFieldReadOnly('Slump', true);
                myJqGrid.setFormFieldDisabled('AuditStatus', true);
                myJqGrid.setFormFieldReadOnly('CastMode', false);
                myJqGrid.setFormFieldReadOnly('NeedDate', true);
                myJqGrid.setFormFieldReadOnly('ProjectName', false);
                document.getElementById("tr_AuditStatus").style.display = "none";
                //myJqGrid.setFormFieldReadOnly('ProjectName', true);
                //$("input[name='ContractName'] ~ button")[0].disabled = true; //禁用AutoComplete控件
                linkManChange();
                contractChange();
                projectChange();
                var contractId = myJqGrid.getFormField("ContractID").val();
                getCastMode(contractId); //加载合同对应的浇筑方式
                getConStrength(contractId); //加载合同对应的砼强度列表

                //特性
                bindIdentitySettings('#rightdiv', contractId, record.ID);

                //显示附件
                var attDiv = $('#Attachments');
                attDiv.empty();
                attDiv.append(record["Attachments"]);
                $('a[att-id]').show();
            }
        });
    });






}