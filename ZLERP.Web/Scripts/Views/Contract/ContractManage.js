﻿//合同管理
function contractIndexInit(options) {
    var ContractGrid = new MyGrid({
        renderTo: "Contractid",
        title: "合同列表",
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight * 0.7 - 100,
        dialogWidth: 800,
        dialogHeight: 700,
        storeURL: options.ContractStoreUrl,
        storeCondition: "ContractStatus != '3'",
        sortByField: "ID",
        primaryKey: "ID",
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [{
            label: "合同编号",
            name: "ID",
            index: "ID",
            align: "center",
            width: 80
        },
		{
		    label: "合同名称",
		    name: "ContractName",
		    index: "ContractName"
		},
		{
		    label: "客户名称",
		    name: "CustName",
		    index: "Customer.CustName",
		    width: 200,
		    searchoptions: {
		        sopt: ["cn"]
		    }
		},
		{
		    label: "审核状态",
		    name: "AuditStatus",
		    index: "AuditStatus",
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: "AuditStatus"
		    },
		    stype: "select",
		    searchoptions: {
		        value: dicToolbarSearchValues("AuditStatus")
		    },
		    width: 100,
		    align: "center"
		},
		{
		    label: "已受限否",
		    name: "IsLimited",
		    index: "IsLimited",
		    align: "center",
		    width: 60,
		    formatter: booleanFmt,
		    unformat: booleanUnFmt,
		    stype: "select",
		    searchoptions: {
		        value: booleanSearchValues()
		    }
		},
		{
		    label: "合同状态",
		    name: "ContractStatus",
		    index: "ContractStatus",
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: "ContractStatus"
		    },
		    stype: "select",
		    searchoptions: {
		        value: dicToolbarSearchValues("ContractStatus")
		    },
		    width: 80
		},
		{
		    label: "业务类型",
		    name: "BusinessType",
		    index: "BusinessType",
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: "BusinessType"
		    },
		    stype: "select",
		    searchoptions: {
		        value: dicToolbarSearchValues("BusinessType")
		    },
		    width: 80
		}, 
		{
		    label: "客户编码",
		    name: "CustomerID",
		    index: "CustomerID",
		    hidden: true
		}

		,
		{
		    label: "合同号",
		    name: "ContractNo",
		    index: "ContractNo"
		},
		{
		    label: "合同样本",
		    name: "Attachments",
		    formatter: attachmentFmt,
		    sortable: false,
		    search: false
		},
		{
		    label: "建设单位",
		    name: "BuildUnit",
		    index: "BuildUnit"
		},
		{
		    label: "项目地址",
		    name: "ProjectAddr",
		    index: "ProjectAddr"
		},
		{
		    label: "签订总方量",
		    name: "SignTotalCube",
		    index: "SignTotalCube",
		    width: 80,
		    align: "right"
		},
		{
		    label: "签订日期",
		    name: "SignDate",
		    index: "SignDate",
		    formatter: "date",
		    width: 80,
		    align: "center",
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datepicker();
		        },
		        sopt: ["ge"]
		    }
		},
		{
		    label: "签订总金额",
		    name: "SignTotalMoney",
		    index: "SignTotalMoney",
		    width: 80,
		    align: "right"
		},
		{
		    label: "合同类型",
		    name: "ContractType",
		    index: "ContractType",
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: "ContractType"
		    },
		    stype: "select",
		    searchoptions: {
		        value: dicToolbarSearchValues("ContractType")
		    }
		},
		{
		    label: "计价方式",
		    name: "ValuationType",
		    index: "ValuationType",
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: "ValuationType"
		    },
		    stype: "select",
		    searchoptions: {
		        value: dicToolbarSearchValues("ValuationType")
		    }
		},
		{
		    label: "付款方式",
		    name: "PaymentType",
		    index: "PaymentType",
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: "PaymentType"
		    },
		    stype: "select",
		    searchoptions: {
		        value: dicToolbarSearchValues("PaymentType")
		    }
		} 
		,
		{
		    label: "审核人",
		    name: "Auditor",
		    index: "Auditor",
		    hidden: true
		},
		{
		    label: "审核时间",
		    name: "AuditTime",
		    index: "AuditTime",
		    formatter: "datetime",
		    hidden: true
		},
		{
		    label: "审核意见",
		    name: "AuditInfo",
		    index: "AuditInfo",
		    hidden: true
		},
		{
		    label: "业务员",
		    name: "Salesman",
		    index: "Salesman"
		},
		{
		    label: "甲方联系人",
		    name: "ALinkMan",
		    index: "ALinkMan"
		},
		{
		    label: "甲方联系电话",
		    name: "ALinkTel",
		    index: "ALinkTel"
		},
		{
		    label: "乙方联系人",
		    name: "BLinkMan",
		    index: "BLinkMan"
		},
		{
		    label: "乙方联系电话",
		    name: "BLinkTel",
		    index: "BLinkTel"
		},
		{
		    label: "合同开始时间",
		    name: "ContractST",
		    index: "ContractST",
		    formatter: "datetime",
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datetimepicker();
		        },
		        sopt: ["ge"]
		    }
		},
		{
		    label: "合同结束时间",
		    name: "ContractET",
		    index: "ContractET",
		    formatter: "datetime",
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datetimepicker();
		        },
		        sopt: ["ge"]
		    }
		},
		{
		    label: "回款联系人",
		    name: "PayBackMan",
		    index: "PayBackMan"
		},
		{
		    label: "回款联系电话",
		    name: "PayBackTel",
		    index: "PayBackTel"
		}, 
		{
		    label: "备注",
		    name: "Remark",
		    index: "Remark"
		},
        {
            label: "签约公司",
            name: "SignCompany",
            index: "SignCompany"
        },
        {
            label: "签约公司编号",
            name: "SignCompanyID",
            index: "SignCompanyID"
        }
        ],
        functions: {
            handleReload: function (btn) {
                ContractGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ContractGrid.refreshGrid("ContractStatus != '3'");
            },
            handleAdd: function (btn) {
                ContractGrid.handleAdd({
                    btn: btn,
                    loadFrom: 'ContractForm',
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(response.Data);
                        }
                    },
                    afterFormLoaded: function () {
                        $('#Attachments').empty();
                    }

                });
            },
            handleEdit: function (btn) {
                var data = ContractGrid.getSelectedRecord();
                var auditValue = data.AuditStatus;
                if (auditValue == 1) {
                    showMessage('提示', '已审核的数据不能修改');
                    return;
                }
                ContractGrid.handleEdit({
                    btn: btn,
                    loadFrom: 'ContractForm',
                    prefix: 'Contract',
                    afterFormLoaded: function () {
                        var attDiv = $('#Attachments');
                        attDiv.empty();
                        attDiv.append(data["Attachments"]);
                        $('a[att-id]').show();
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(data.ID);
                        }
                    }
                });
            },
            handleDelete: function (btn) {
                ContractGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            //添加合同明细
            handleAddContractItem: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectrecord = ContractGrid.getSelectedRecord();
                if (selectrecord.ContractStatus == 3) { //状态为3表示已完工，取自字典表
                    showMessage('提示', '已完工的合同不能再添加合同明细');
                    return;
                }
                ContractGrid.handleAdd({
                    title: '添加合同明细',
                    width: 400,
                    height: 250,
                    loadFrom: 'ContractItemForm',
                    btn: btn,
                    reloadGrid: false,
                    afterFormLoaded: function () {
                        ContractGrid.setFormFieldValue("ContractItem.ContractID", selectrecord.ID);
                        $("#ContractName_span").html(selectrecord.ContractName);
                    },
                    postCallBack: function (response) {
                        ContractItemGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                    }
                });
            },
            //添加特性
            handleAddIdentitySetting: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectrecord = ContractGrid.getSelectedRecord();
                if (selectrecord.ContractStatus == 3) { //状态为3表示已完工，取自字典表
                    showMessage('提示', '已完工的合同不能再添加特性');
                    return;
                }
                ContractGrid.showWindow({
                    title: '添加特性',
                    width: 600,
                    height: 500,
                    loadFrom: 'IdentitySetForm',
                    afterLoaded: function () {
                        $("input[name='IdentitySetting.ContractID']").val(selectrecord.ID);
                        identitySettingGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                        identitySettingGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/IdentitySetting.mvc/Delete" });
                        //下拉列表级联绑定
                        var identityTypelist = $("select[name='IdentitySetting.IdentityType']");
                        identityTypelist.unbind("change");
                        identityTypelist.bind("change", function () {
                            bindSelectData(
                                    $("select[name='IdentitySetting.IdentityName']"),
                                    "/Identity.mvc/ListData",
                                    { textField: "IdentityName", valueField: "IdentityName", condition: "IdentityType='" + $(this).val() + "'" }
                                );
                        });

                        var identityNamelist = $("select[name='IdentitySetting.IdentityName']");
                        identityNamelist.unbind("change");
                        identityNamelist.bind("change", function () {
                            var requestURL = "/Contract.mvc/getIdentityPrice";
                            var postParams = { identityName: $("select[name='IdentitySetting.IdentityName']").val(),
                                identityType: $("select[name='IdentitySetting.IdentityType']").val()
                            };
                            ajaxRequest(requestURL, postParams, false, function (response) {
                                $("input[name='IdentitySetting.IdentityPrice']").val(response.IdentityPrice);
                            });

                        });

                    }
                    , buttons: {
                            "关闭": function () {
                                $(this).dialog("close");
                            }
                    }
                });
            },
            //添加工程明细
            handleAddProject: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectrecord = ContractGrid.getSelectedRecord();
                if (selectrecord.ContractStatus == 3) { //状态为3表示已完工，取自字典表
                    showMessage('提示', '已完工的合同不能再设置工程明细');
                    return;
                }
                ContractGrid.handleAdd({
                    btn: btn,
                    width: 550,
                    height: 350,
                    loadFrom: '/Project.mvc/Index #projectForm form',
                    btn: btn,
                    reloadGrid: false,
                    afterFormLoaded: function () {
                        ContractGrid.setFormFieldValue("ContractID", selectrecord.ID);
                        ContractGrid.setFormFieldValue("ProjectAddr", selectrecord.ProjectAddr);
                        ContractGrid.setFormFieldValue("BuildUnit", selectrecord.BuildUnit);
                        ContractGrid.setFormFieldValue("Tel", selectrecord.BLinkTel);
                        $("#ContractName_project").html(selectrecord.ContractName);
                    },
                    postCallBack: function (response) {
                        ProjectGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                    }
                });
            },
            //合同审核
            handleAuditContract: function (btn) {
                var records = ContractGrid.getSelectedRecords();
                for (var i = 0; i < records.length; i++) {
                    var auditValue = records[i].AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '请选择未审核的合同！');
                        return;
                    }
                }

                var keys = ContractGrid.getSelectedKeys();
                if (keys.length > 1) {
                    var requestURL = options.handleBatchAuditUrl;
                    var postParams = {
                        id: keys
                    };
                    ajaxRequest(requestURL, postParams, true,
					function (response) {
					    ContractGrid.reloadGrid();
					});
                } else {
                    ContractGrid.handleEdit({
                        btn: btn,
                        width: 550,
                        height: 350,
                        prefix: 'Contract',
                        loadFrom: 'AuditForm',
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldDisabled('Contract.ContractName', true);
                            ContractGrid.setFormFieldDisabled('Contract.SignTotalCube', true);
                            ContractGrid.setFormFieldDisabled('Contract.SignTotalMoney', true);

                        }
                    });
                }
            },
            //取消审核
            handleUnAuditContract: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectedRecord = ContractGrid.getSelectedRecord();
                var auditValue = selectedRecord.AuditStatus;
                if (auditValue == 0) {
                    showMessage('提示', '该合同正处于未审核状态！');
                    return;
                } else {
                    //确认操作
                    showConfirm("确认信息", "您确定要<font color=green><b>取消审核</b></font>吗？",
					function () {
					    ajaxRequest(options.contractUnAuditUrl, {
					        contractID: selectedRecord.ID,
					        auditStatus: 0
					    },
						true,
						function () {
						    ContractGrid.refreshGrid();
						});
					    $(this).dialog("close");
					});
                }
            },
            //快速解除限制
            handleQuickUnfreeze: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectedRecord = ContractGrid.getSelectedRecord();
                if (selectedRecord.ContractStatus != 2) {
                    //如果合同为非进行中的，则需要确认
                    showConfirm("确认信息", "该合同的当前状态为<font color=red>" + dicNormalRenderer(selectedRecord.ContractStatus, 'ContractStatus') + "</font>，您确定要解除限制？",
					function () {
					    ajaxRequest(btn.data.Url, {
					        contractID: selectedRecord.ID,
					        contractStatus: 2

					    },
						true,
						function () {
						    ContractGrid.refreshGrid();
						});
					    $(this).dialog("close");
					});
                } else {
                    ajaxRequest(btn.data.Url, {
                        contractID: selectedRecord.ID,
                        contractStatus: selectedRecord.ContractStatus

                    },
					true,
					function (data) {
					    ContractGrid.refreshGrid();
					});
                }
            },
            //快速锁定（无条件锁定）
            handleQuickLock: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectedRecord = ContractGrid.getSelectedRecord();
                showLockReason();
                //弹出锁定理由框
                function showLockReason() {
                    ContractGrid.showWindow({
                        title: btn.data.FuncDesc,
                        width: 350,
                        height: 200,
                        loadFrom: 'lockReasonForm',
                        afterLoaded: function () {
                            if (!isEmpty(selectedRecord.Remark)) {
                                $("#lockReason").val(selectedRecord.Remark + "\n锁定理由：");
                            }
                            $("#ContractIDlock").val(selectedRecord.ID);
                        }

                    });
                    $(this).dialog("close");
                }
                function unshowLockReason() {
                    ajaxRequest(btn.data.Url, {
                        contractID: selectedRecord.ID

                    },
					true,
					function (data) {
					    ContractGrid.refreshGrid();
					});
                    $(this).dialog("close");
                }
            },
            //已完工合同
            handleCompleted: function () {
                ContractGrid.refreshGrid("ContractStatus = 3");
            },
            //未完工合同
            handleNotCompleted: function () {
                ContractGrid.refreshGrid("ContractStatus != 3 and IsLimited = 0");
            },
            //受限合同
            handleLimitBy: function () {
                ContractGrid.refreshGrid("IsLimited = 1");
            },
            handleSetComplete: function (btn) {
                setcmp(btn, ContractGrid, true);
            },
            handleImport: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectrecord = ContractGrid.getSelectedRecord();
                if (selectrecord.ContractStatus == 3) { //状态为3表示已完工，取自字典表
                    showMessage('提示', '已完工的合同不能再添加合同明细');
                    return;
                }
                ContractGrid.handleAdd({
                    title: '导入合同明细',
                    width: 400,
                    height: 300,
                    loadFrom: 'ImportForm',
                    btn: btn,
                    reloadGrid: false,
                    afterFormLoaded: function () {
                        ContractGrid.setFormFieldValue("contractID", selectrecord.ID);
                        $("#ContractNameForImportForm").html(selectrecord.ContractName);
                    },
                    postCallBack: function (response) {
                        ContractItemGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                    }
                });
            },
            handleEditContractStatus: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectrecord = ContractGrid.getSelectedRecord();

                ContractGrid.handleEdit({
                    btn: btn,
                    width: 300,
                    height: 200,
                    loadFrom: 'editCStausForm',
                    btn: btn,
                    reloadGrid: false,
                    afterFormLoaded: function () {
                        ContractGrid.setFormFieldValue("Contract.ID", selectrecord.ID);
                        ContractGrid.setFormFieldValue("ContractStatus", selectrecord.ContractStatus);

                    },
                    postCallBack: function (response) {
                        ContractGrid.refreshGrid("1=1"); //回调刷新
                    }
                });
            },
            handleBitUpdatePrice: function (btn) {
                $("#DivBitUpdatePrice").dialog("open");
            },
            //新增合同限制
            handleContractLimitAdd: function (btn) {
                if (!ContractGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectedContract = ContractGrid.getSelectedRecord();

                ContractGrid.handleAdd({
                    loadFrom: "contractLimitForm",
                    btn: btn,
                    width: 500,
                    height: 300,
                    afterFormLoaded: function () {
                        ContractGrid.setFormFieldValue("ContractLimit.ContractID", selectedContract.ID);

                        ContractGrid.getFormField("ContractLimit.LimitType").unbind("change").bind("change", function () {
                            if ($(this).val() == 'LimitTime') {
                                console.log("datetimepicker");
                                ContractGrid.getFormField("ContractLimit.WarnValue").datetimepicker();
                                ContractGrid.getFormField("ContractLimit.LimitValue").datetimepicker();
                            } else {
                                ContractGrid.getFormField("ContractLimit.WarnValue").datetimepicker("destroy");
                                ContractGrid.getFormField("ContractLimit.LimitValue").datetimepicker("destroy");
                            }
                        }).trigger("change");
                    },
                    postCallBack: function (response) {
                        contractLimitGrid.reloadGrid(); //回调刷新
                    }
                });
            }

        }
    });

    var ProjectGrid = new MyGrid({
        renderTo: 'ProjectGrid', 
        autoWidth: true,
        height: gGridHeight * 0.3,
        dialogWidth: 920,
        dialogHeight: 510,
        storeURL: options.ProjectStoreUrl,
        sortByField: 'ID',
        primaryKey: 'ID',
        setGridPageSize: 100,
        showPageBar: false,
        autoLoad: false,
        singleSelect: true,
        editSaveUrl: options.ProjectUpdateUrl,
        initArray: [{
            label: '工程编号',
            name: 'ID',
            index: 'ID',
            width: 80
        },
		{
		    label: '工程名称',
		    name: 'ProjectName',
		    index: 'ProjectName',
		    editable: true,
		    editrules: {
		        required: true
		    }
		},
		{
		    label: '项目地址',
		    name: 'ProjectAddr',
		    index: 'ProjectAddr',
		    editable: true
		},
		{
		    label: '工程运距',
		    name: 'Distance',
		    index: 'Distance',
		    editable: true,
		    width: 80
		},
		{
		    label: '建设单位',
		    name: 'BuildUnit',
		    index: 'BuildUnit',
		    editable: true
		},
		{
		    label: '施工单位',
		    name: 'ConstructUnit',
		    index: 'ConstructUnit',
		    editable: true
		},
		{
		    label: '工地联系人',
		    name: 'LinkMan',
		    index: 'LinkMan',
		    editable: true,
		    width: 80
		},
		{
		    label: '工地电话',
		    name: 'Tel',
		    index: 'Tel',
		    editable: true,
		    width: 80
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    editable: true
		},
		{
		    label: '运费模板ID',
		    name: 'CarTemplet',
		    index: 'CarTemplet',
		    width: 70,
		    align: 'center',
		    editable: true,
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: "CarTemplet"
		    },
		    stype: "select",
		    searchoptions: {
		        value: getFareTempletNoStr()
		    },
		    edittype: 'select',
		    editoptions: {
		        value: getFareTempletNoStr()
		    }
		},
		{
		    label: '运费模板名称',
		    name: 'B_FareTemplet.FareTempletName',
		    index: 'B_FareTemplet.FareTempletName',
		    width: 80
		},
		{
		    label: '操作',
		    name: 'act',
		    index: 'act',
		    width: 200,
		    sortable: false,
		    align: "center"
		}],
        functions: {
            handleReload: function (btn) {
                ProjectGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ProjectGrid.refreshGrid();
            }
        }
    });

    //特性设定
    var identitySettingGrid = new MyGrid({
        renderTo: 'identitySettingGrid'
            , title: '特性设置'
            , autoWidth: true
            , dialogWidth: 920
            , dialogHeight: 510
            , storeURL: '/IdentitySetting.mvc/Find'
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , setGridPageSize: 100
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: '/IdentitySetting.mvc/Update'
            , groupingView: { groupSummary: [false], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
            , groupField: 'IdentityName'
            , initArray: [
                { label: '特性设定编号', name: 'IdentitySettingID', index: 'IdentitySettingID', hidden: true }
                , { label: '操作', name: 'myac', width: 50, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
                , { label: ' 日期', name: 'SetDate', index: 'SetDate', editable: true, width: 130, formatter: 'datetime', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '特性类型', name: 'IdentityType', index: 'IdentityType', width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'IdenType'} }
                , { label: '详细特性', name: 'IdentityName', index: 'IdentityName', width: 100 }
                , { label: '特性价格', name: 'IdentityPrice', index: 'IdentityPrice', editable: true, width: 100 }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            ]
            , functions: {
                handleReload: function (btn) {
                    identitySettingGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    identitySettingGrid.refreshGrid();
                }
            }
        });

        //特性设定
        var identitySettingGridto = new MyGrid({
              renderTo: 'identitySettingGridto' 
            , autoWidth: true
            , dialogWidth: 920
            , dialogHeight: 510
            , storeURL: '/IdentitySetting.mvc/Find'
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , setGridPageSize: 100
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: '/IdentitySetting.mvc/Update'
            , groupingView: { groupSummary: [false], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
            , groupField: 'IdentityName'
            , initArray: [
                  { label: '特性设定编号', name: 'IdentitySettingID', index: 'IdentitySettingID', hidden: true } 
                , { label: '定价日期', name: 'SetDate', index: 'SetDate', editable: true, width: 130, formatter: 'datetime', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '特性类型', name: 'IdentityType', index: 'IdentityType', width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'IdenType'} }
                , { label: '详细特性', name: 'IdentityName', index: 'IdentityName', width: 100 }
                , { label: '特性价格', name: 'IdentityPrice', index: 'IdentityPrice', editable: true, formatter: 'currency', width: 100 }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            ]
            , functions: {
                handleReload: function (btn) {
                    identitySettingGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    identitySettingGrid.refreshGrid();
                }
            }
        });

    //合同泵车价格设定
    var ContractPumpGrid = new MyGrid({
        renderTo: 'ContractPump',
        autoWidth: true,
        storeURL: options.contractPumpStoreUrl,
        sortByField: 'ID',
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        autoLoad: false,
        singleSelect: true,
        initArray: [{
            label: ' 编号',
            name: 'ID',
            index: 'ID',
            hidden: true
        },
		{
		    label: ' 合同编号',
		    name: 'ContractID',
		    index: 'ContractID',
		    hidden: true
		},
		{
		    label: ' 日期',
		    name: 'SetDate',
		    index: 'SetDate',
		    width: 100,
		    formatter: 'date',
		    align: 'center',
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datepicker();
		        },
		        sopt: ['ge']
		    }
		},
		{
		    label: ' 泵车类型',
		    name: 'PumpType',
		    index: 'PumpType',
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: 'PumpType'
		    },
		    width: 120,
		    editable: true,
		    edittype: 'select',
		    editoptions: {
		        value: dicToolbarSearchValues('CastMode')
		    },
		    editrules: {
		        required: true
		    }
		},
		{
		    label: ' 泵车价格',
		    name: 'PumpPrice',
		    index: 'PumpPrice',
		    editable: true,
		    formatter: 'currency',
		    align: 'right',
		    width: 100
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                ContractPumpGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ContractPumpGrid.refreshGrid('1=1');
            }
        }
    });

    //合同明细
    var ContractItemGrid = new MyGrid({
        renderTo: 'ContractItemGrid',
        autoWidth: true,
        buttons: buttons1,
        height: gGridHeight * 0.3,
        storeURL: options.itemStoreUrl,
        sortByField: 'ConStrength',
        sortOrder: 'ASC',
        primaryKey: 'ID',
        setGridPageSize: 50,
        showPageBar: true,
        autoLoad: false,
        dialogWidth: 600,
        dialogHeight: 200,
        singleSelect: true,
        editSaveUrl: options.itemUpdateUrl,
        initArray: [{
            label: '合同明细编号',
            name: 'ID',
            index: 'ID',
            hidden: true
        },
		{
		    label: '合同编号',
		    name: 'ContractID',
		    index: 'ContractID',
		    hidden: true
		},
		{
		    label: '合同名称',
		    name: 'ContractName',
		    index: 'ContractName',
		    hidden: true
		},
		{
		    label: '砼强度',
		    name: 'ConStrength',
		    index: 'ConStrength',
		    width: 100,
		    align: 'center'
		},
        {
            label: '基准单价',
            name: 'TranPrice',
            index: 'TranPrice',
            width: 100,
            align: "right",
            formatter: 'currency'
        },
		{
		    label: '最新单价',
		    name: 'UnPumpPrice',
		    index: 'UnPumpPrice',
		    width: 100,
		    align: "right",
		    formatter: 'currency'
		},
       	{
		    label: '最新定价时间',
		    name: 'NewPriceDate',
		    index: 'NewPriceDate',
		    width: 150,
		    formatter: 'datetime',
		    
		}, 
		{
		    label: '额外费',
		    name: 'ExMoney',
		    index: 'ExMoney',
		    width: 50,
		    align: "right",
		    formatter: 'currency',
		    editable: true,
		    editrules: {
		        number: true
		    }
		},
		{
		    label: '建立时间',
		    name: 'BuildTime',
		    index: 'BuildTime',
		    width: 110,
		    formatter: 'datetime',
		    hidden: true
		},
		{
		    label: '操作',
		    name: 'act',
		    index: 'act',
		    width: 200,
		    sortable: false,
		    align: "center"
		}],
        functions: {
            handleReload: function (btn) {
                ContractItemGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ContractItemGrid.refreshGrid();
            }

        }
    });

    //价格设定
    var priceSettingGrid = new MyGrid({
        renderTo: 'priceSettingGrid',
        autoWidth: true,
        buttons: buttons2,
        storeURL: options.priceStoreUrl,
        sortByField: 'ID',
        primaryKey: 'ID',
        dialogWidth: 900,
        dialogHeight: 700,
        setGridPageSize: 30,
        showPageBar: true,
        altclass: 'identityButton',
        altRows: true,
        autoLoad: true,
        editSaveUrl: options.priceUpdateUrl,
        initArray: [{
            label: '特性设定编号',
            name: 'PriceSettingID',
            index: 'PriceSettingID',
            hidden: true
        },
		{
		    label: '合同明细编号',
		    name: 'ContractItemsID',
		    index: 'ContractItemsID',
		    hidden: true
		},
		{
		    label: '订价时间',
		    name: 'ChangeTime',
		    index: 'ChangeTime',
		    formatter: "datetime",
		    editable: false,
		    width: 150
		},
		{
		    label: '单价',
		    name: 'UnPumpPrice',
		    index: 'UnPumpPrice',
		    editable: false,
		    width: 80
		},
		{
		    label: '增加费',
		    name: 'PumpCost',
		    index: 'PumpCost',
		    hidden: true,
		    editable: false,
		    width: 80
		},
		{
		    label: '砂浆价格',
		    name: 'SlurryPrice',
		    index: 'SlurryPrice',
		    hidden: true,
		    editable: false,
		    width: 90
		},
		{
		    label: '操作',
		    name: 'myac',
		    width: 50,
		    fixed: true,
		    sortable: false,
		    resize: false,
		    formatter: 'actions',
		    formatoptions: {
		        keys: true,
		        editbutton: false
		    }
		},
		{
		    label: '创建人',
		    name: 'Builder',
		    index: 'Builder',
		    width: 80
		},
		{
		    label: '调价备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 80
		}],
        functions: {
            handleReload: function (btn) {
                priceSettingGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                priceSettingGrid.refreshGrid();
            },
            handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    ContractGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    ContractItemGrid.refreshGrid("ContractID='" + id + "'");
	    ProjectGrid.refreshGrid("ContractID='" + id + "'");
	    ContractPumpGrid.refreshGrid("ContractID='" + id + "'");
	    contractLimitGrid.refreshGrid("ContractID='" + id + "'"); 
	    identitySettingGridto.refreshGrid("ContractID='" + id + "'");
	});

    ContractGrid.addListeners('rowdblclick',
	function (id, record, selBool) {
	    ContractGrid.handleEdit({
	        loadFrom: 'ContractForm',
	        prefix: 'Contract',
	        title: '查看详细',
	        width: 800,
	        height: 400,
	        buttons: {
	            "关闭": function () {
	                $(this).dialog('close');
	            }
	        },
	        afterFormLoaded: function () { }
	    });
	});

    //合同置为完工状态操作
    function setcmp(b, grid, isCompleted) {
        if (!grid.isSelectedOnlyOne()) {
            showMessage('提示', '请选择一条记录进行操作！');
            return;
        }
        var selectedRecord = grid.getSelectedRecord();
        ajaxRequest(b.data.Url, {
            contractID: selectedRecord.ID
        },
		true,
		function () {
		    grid.refreshGrid();
		},
		null, b);
    }

    //grid行操作栏按钮（删除、价格变动）
    ContractItemGrid.addListeners("gridComplete",
	function () {
	    var records = ContractItemGrid.getAllRecords();
	    var rid, buildtime;
	    for (var i = 0; i < records.length; i++) {
	        rid = records[i].ID;
	        conStrength = records[i].ConStrength;
	        buildtime = records[i].BuildTime;
	        be = "<input class='identityButton'  type='button' value='价格变动' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handlePriceSet(" + rid + ",'" + conStrength + "','" + buildtime + "');\"  />";
	        fe = "<input class='identityButton'  type='button' value='调价记录' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleShowPrice(" + rid + ");\"  /> ";
	        ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteContractItem(" + rid + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
	        ContractItemGrid.getJqGrid().jqGrid('setRowData', rid, {
	            act: be + fe + ce
	        });
	    }
	});
    ProjectGrid.addListeners("gridComplete",
	function () {
	    var records = ProjectGrid.getAllRecords();
	    var rid;
	    for (var i = 0; i < records.length; i++) {
	        rid = records[i].ID;
	        ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteProject('" + rid + "');\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
	        ProjectGrid.getJqGrid().jqGrid('setRowData', rid, {
	            act: ce
	        });
	    }
	});
    //生成基础砼标记
    function getCarNoStr() {
        var requestURL = "/Contract.mvc/getConList";
        var postParams = {};
        var str = "";
        $.ajax({
            type: 'post',
            async: false,
            url: requestURL,
            data: postParams,
            success: function (data) {
                if (data != null) {
                    var jsonobj = eval(data);
                    var length = jsonobj.length;
                    str = ":" + ";";
                    for (var i = 0; i < length; i++) {
                        if (i != length - 1) {
                            str += jsonobj[i].ConStrengthCode + ":" + jsonobj[i].ConStrengthCode + ";";
                        } else {
                            str += jsonobj[i].ConStrengthCode + ":" + jsonobj[i].ConStrengthCode;
                        }
                    }
                }

            }
        });
        return str;
    }
    //生成运费模板
    function getFareTempletNoStr() {
        var requestURL = "/Contract.mvc/getB_FareTempletList";
        var postParams = {};
        var str = "";
        $.ajax({
            type: 'post',
            async: false,
            url: requestURL,
            data: postParams,
            success: function (data) {
                if (data != null) {
                    var jsonobj = eval(data);
                    var length = jsonobj.length;
                    str = ":" + ";";
                    for (var i = 0; i < length; i++) {
                        if (i != length - 1) {
                            str += jsonobj[i].ID + ":" + jsonobj[i].FareTempletName + ";";
                        } else {
                            str += jsonobj[i].ID + ":" + jsonobj[i].FareTempletName;
                        }
                    }
                }

            }
        });
        return str;
    }

    //弹出【价格变动】窗体
    window.handlePriceSet = function (id, ConStrength, buildtime) {
        ContractItemGrid.showWindow({
            title: '价格变动',
            width: 620,
            height: 400,
            loadFrom: 'PriceSetForm',
            afterLoaded: function () {
                //显示合同明细项目ID
                $("input[name='PriceSetting.ContractItemsID']").val(id);
                $("#DisplayContractItemsID1").html("<font color=red>" + id + "</font>");

                $("#DisplayConStrength1").html("<font color=red>" + ConStrength + "</font>");
                //显示合同明细项目建立时间
                $("input[name='ContractItem.BuildTime']").val(buildtime);
                $("#DisplayContractItemsBTime").html("<font color=red>" + buildtime + "</font>");

            },
            buttons: {
                "关闭": function () {
                    $(this).dialog("close");
                }
            }
        });
    };
    window.handleShowPrice = function (id) {
        priceSettingGrid.getJqGrid().jqGrid('setGridParam', {
            editurl: options.priceRowDeleteUrl
        });
        priceSettingGrid.refreshGrid("ContractItemsID='" + id + "'");
        $("#priceSetting").dialog("open");
    };
    //保存价格变动函数
    window.priceSave = function () {
        var _ContractItemsID = $("[name='PriceSetting.ContractItemsID']").val();
        var contractId = ContractGrid.getSelectedRecord().ID;
        var btime = $("[name='ContractItem.BuildTime']").val(); //合同明细项目建立时间
        var xchangedate = $("[name='PriceSetting.ChangeTime']").val(); //改价日期
        var xchangetime = $("[name='PriceSetting.ChangeTime']").val() + " 00:00:00"; //改价时间
//        var records = priceSettingGrid.getAllRecords(); //获取所有价格变动列表记录
//        for (var i = 0; i < records.length; i++) {
//            if (records[i].ChangeTime == xchangedate) {
//                showError('错误', '此改价时间已经存在！');
//                return;
//            }
//        }
        $.post(options.priceAddUrl, $("#PriceSetForms").serialize(),
		function (data) {
		    //
		    if (!data.Result) {
		        showError("出错啦！", data.Message);
		        return false;
		    } else {
		        showMessage("提示", data.Message);
		        $("#PriceSetForm").dialog("close");
		        $("#PriceSetForms")[0].reset();
		        //priceSettingGrid.refreshGrid("ContractItemsID='" + _ContractItemsID + "'");
		        ContractItemGrid.refreshGrid("ContractID='" + contractId + "'");
		    }

		});
    };
    //保存合同锁定理由
    window.lockSave = function () {
        ajaxRequest(options.saveLockUrl, {
            contractID: $("#ContractIDlock").val(),
            remark: $("#lockReason").val()
        },
		true,
		function (data) {
		    $("#lockReason").val("");
		    $("#lockReasonForm").dialog("close");
		    ContractGrid.refreshGrid();
		});
    };

    //保存特性设定函数
    window.identitySave = function () {
        var _ContractID = $("[name='IdentitySetting.ContractID']").val();
        $.post(
                    '/IdentitySetting.mvc/Add',
                    $("#IdentitySetForms").serialize(),
                    function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        $("#IdentitySetForms")[0].reset();
                        identitySettingGrid.refreshGrid("ContractID='" + _ContractID + "'");
                    }
                );
    };

    window.handleDeleteContractItem = function (id) {
        showConfirm("确认信息", "您确定删除此项合同明细？",
		function () {
		    $.post(options.itemDeleteUrl, {
		        ID: id
		    },
			function (data) {
			    if (!data.Result) {
			        showError("出错啦！", data.Message);
			        return false;
			    }
			    ContractItemGrid.reloadGrid();
			}

			);
		    $(this).dialog("close");
		});
    };
    window.handleDeleteProject = function (id) {

        console.log("确认信息" + id);
        showConfirm("确认信息", "您确定删除此项工程明细？",
		function () {
		    $.post(options.ProjectDeleteUrl, {
		        id: id
		    },
			function (data) {
			    if (!data.Result) {
			        showError("出错啦！", data.Message);
			        return false;
			    }
			    ProjectGrid.reloadGrid();
			}

			);
		    $(this).dialog("close");
		});
    };
    $("#priceSetting").dialog({
        modal: true,
        autoOpen: false,
        width: 500,
        Height: 500,
        title: "调价历史记录",
        buttons: {
            '关闭': function () {
                $(this).dialog('close');

            }
        },
        position: ["center", 100]
    });

    $('a[data-id]').live('click',
	function (e) {
	    if (e.preventDefault) e.preventDefault();
	    else e.returnValue = false;

	    var data = ContractGrid.getRecordByKeyValue($(this).attr('data-id'));

	    $('a[att-id]').hide();
	});
    $('a[att-id]').live('click',
	function (e) {
	    if (e.preventDefault) {
	        e.preventDefault();
	    } else {
	        e.returnValue = false;
	    }
	    var caller = $(this);
	    ajaxRequest(options.deleteAttachmentUrl, {
	        id: caller.attr('att-id')
	    },
		false,
		function (response) {
		    if (response.Result) {
		        caller.parent('li').remove();
		    }
		});
	});
    function attachmentUpload(objectId) {
        var fileElement = $('#uploadFile');
        if (fileElement.val() == "") return;
        $.ajaxFileUpload({
            url: options.uploadUrl + '?objectType=Contract&objectId=' + objectId,
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
                    ContractGrid.reloadGrid();
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

    $("#DivBitUpdatePrice").dialog({
        modal: true,
        autoOpen: false,
        title: "批量调价即在砼列表中的基准价格的基础上进行调整，批量生产调价信息",
        width: 800,
        Height: 600,
        buttons: {
            '确认': function () {
                var keys = ContractGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var BitUpdateType = $("#BitUpdatePrice_BitUpdateType").val();
                var BitUpdateCnt = $("#BitUpdatePrice_BitUpdateCnt").val();
                var BitUpdateDate = $("#BitUpdatePrice_BitUpdateDate").val();
                var type = "";
                if (BitUpdateType == 0) {
                    type = "按百分比";
                }
                if (BitUpdateType == 1) {
                    type = "按数额";
                }
                if (BitUpdateType == 2) {
                    type = "按基准强度";
                }
                showConfirm("确认信息", "您确定要批量调整勾选合同砼单价吗？合同总数目:" + keys.length + "，调整方式：" + type + "，调整浮动：" + BitUpdateCnt + "，调价时间：" + BitUpdateDate,
				function () {
				    var requestURL = options.BitUpdatePriceUrl;
				    ajaxRequest(requestURL, {
				        keys: keys,
				        BitUpdateType: BitUpdateType,
				        BitUpdateCnt: BitUpdateCnt,
				        BitUpdateDate: BitUpdateDate
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
                $(this).dialog('close');

            },
            '取消': function () {
                $(this).dialog('close');
            }
        },
        position: ["center", 100]
    });



    var contractLimitGrid = new MyGrid({
        renderTo: 'divContractLimit',
        autoWidth: true, 
        storeURL: options.contractLimitFindUrl,
        sortByField: 'ID',
        primaryKey: 'ID',
        dialogWidth: 500,
        dialogHeight: 170,
        setGridPageSize: 30,
        showPageBar: true,
        singleSelect: true,
        autoLoad: false,
        initArray: [{
            label: "限制编号",
            name: "ID",
            index: "ID",
            width: 60,
            align: "center",
            hidden: true
        }, {
            label: "合同编号",
            name: "ContractID",
            index: "ContractID",
            width: 80,
            align: "center"
        }, {
            label: "是否启用",
            name: "IsEnabled",
            index: "IsEnabled",
            align: "center",
            width: 60,
            formatter: booleanFmt,
            unformat: booleanUnFmt,
            stype: "select",
            searchoptions: {
                value: booleanSearchValues()
            }
        }, {
            label: "限制类型",
            name: "LimitType",
            index: "LimitType",
            formatter: dicCodeFmt,
            unformat: dicCodeUnFmt,
            formatoptions: {
                parentId: "ContractLimitType"
            },
            stype: "select",
            searchoptions: {
                value: dicToolbarSearchValues("ContractLimitType")
            },
            width: 100,
            align: "center"
        }, {
            label: "预警值",
            name: "WarnValue",
            index: "WarnValue",
            width: 100
        }, {
            label: "限制值",
            name: "LimitValue",
            index: "LimitValue",
            width: 100
        },
         {
             label: "是否达到预警",
             name: "IsWarn",
             index: "IsWarn",
             align: "center",
             width: 80,
             formatter: booleanFmt,
             unformat: booleanUnFmt,
             stype: "select",
             searchoptions: {
                 value: booleanSearchValues()
             }
         },
        {
            label: "预警监测信息",
            name: "CheckWarnMessage",
            index: "CheckWarnMessage",
            width: 200
        }, {
            label: "是否达到限制",
            name: "IsLimit",
            index: "IsLimit",
            align: "center",
            width: 80,
            formatter: booleanFmt,
            unformat: booleanUnFmt,
            stype: "select",
            searchoptions: {
                value: booleanSearchValues()
            }
        }, {
            label: "限制监测信息",
            name: "CheckLimitMessage",
            index: "CheckLimitMessage",
            width: 200
        }, {
            label: "当前值",
            name: "CurrentValue",
            index: "CurrentValue",
            width: 100
        }, {
            label: "限制之间关系",
            name: "Relation",
            index: "Relation",
            width: 100
        }, { label: '最后检测时间', name: 'ModifyTime', index: 'ModifyTime', width: 120, formatter: 'datetime' }, {
            label: '编辑',
            name: 'editContractLimit',
            index: 'editContractLimit',
            width: 90,
            sortable: false,
            align: "center",
            search: false
        }, {
            label: '删除',
            name: 'deleteContractLimit',
            index: 'deleteContractLimit',
            width: 90,
            sortable: false,
            align: "center",
            search: false
        }],
        functions: {
            handleReload: function (btn) {
                contractLimitGrid.reloadGrid(); //重新加载
            },
            handleRefresh: function (btn) {
                contractLimitGrid.refreshGrid(); //刷新
            }
        }
    });

    contractLimitGrid.addListeners("gridComplete", function () {
        var ids = contractLimitGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var deleteButton = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteContractLimit('" + ids[i] + "');\" class='ui-pg-div ui-inline-del' style='margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
            var editButton = "<input class='identityButton'  type='button' value='编辑' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleEditContractLimit(" + ids[i] + ");\"  /> ";
            contractLimitGrid.getJqGrid().jqGrid('setRowData', ids[i], {
                deleteContractLimit: deleteButton
            });
            contractLimitGrid.getJqGrid().jqGrid('setRowData', ids[i], {
                editContractLimit: editButton
            });
        }
    });


    window.handleDeleteContractLimit = function (contractLimitId) {
        contractLimitGrid.setSelection(contractLimitId);
        contractLimitGrid.deleteRecord({
            deleteUrl: options.contractLimitDeleteUrl
        });
    }

    window.handleEditContractLimit = function (contractLimitId) {
        contractLimitGrid.setSelection(contractLimitId);
        var selectedContractLimit = contractLimitGrid.getSelectedRecord();
        contractLimitGrid.handleEdit({
            postUrl: options.contractLimitEditUrl,
            loadFrom: "contractLimitForm",
            prefix: "ContractLimit",
            afterFormLoaded: function () {
                contractLimitGrid.getFormField("ContractLimit.LimitType").unbind("change").bind("change", function () {
                    if ($(this).val() == 'LimitTime') {
                        console.log("datetimepicker");
                        contractLimitGrid.getFormField("ContractLimit.WarnValue").datetimepicker();
                        contractLimitGrid.getFormField("ContractLimit.LimitValue").datetimepicker();
                    } else {
                        contractLimitGrid.getFormField("ContractLimit.WarnValue").datetimepicker("destroy");
                        contractLimitGrid.getFormField("ContractLimit.LimitValue").datetimepicker("destroy");
                    }
                }).trigger("change");
            },
            postCallBack: function (response) { }

        });

    }


}