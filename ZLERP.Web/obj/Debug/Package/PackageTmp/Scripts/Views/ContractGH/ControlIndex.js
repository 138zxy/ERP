//合同控制管理
function contractIndexInit(options) {
    var flag = 0; //新增0 修改1
    //重置表单样式
    function resetClass(obj) {
        if (!$.isEmptyObject(obj)) {
            $.each(obj, function (i, n) {
                if (n == "removeClass") {
                    $("input[name='" + i + "']").removeClass("text requiredval");
                } else {
                    $("input[name='" + i + "']").addClass("text requiredval");
                }
            });
        }
    }

    //合同表单提交之前触发的事件
    function beforeContractFormSubmit() {
        resetClass({
            "ContractGH.ContractST": "removeClass",
            "ContractGH.ContractET": "removeClass",
            "ContractGH.MatCube": "removeClass",
            "ContractGH.PrepayCube": "removeClass"
        });
        var contractLimitTypeVal = ContractGrid.getFormFieldValue("ContractGH.ContractLimitType"); //合同限制类型
        var contractST = ContractGrid.getFormFieldValue("ContractGH.ContractST"); //合同开始时间
        var contractET = ContractGrid.getFormFieldValue("ContractGH.ContractET"); //合同结束时间
        var contractMatCube = ContractGrid.getFormFieldValue("ContractGH.MatCube"); //合同垫资量
        var contractPrepayCube = ContractGrid.getFormFieldValue("ContractGH.PrepayCube"); //合同预付量
        switch (contractLimitTypeVal) {
            case limitObj.limit0:
                break;
            case limitObj.limit1:
                if (!isEmpty(contractST) && !isEmpty(contractET)) {
                    if (Date.parse(contractST.replace(/\-/g, "\/")) > Date.parse(contractET.replace(/\-/g, "/"))) {
                        $("input[name='ContractGH.ContractET']").addClass("input-validation-error");
                        showError('验证错误', '合同结束时间必须大于合同开始时间');
                        return false;
                    }

                } else {
                    if (contractST == "" || isEmpty(contractST)) {
                        $("input[name='ContractGH.ContractST']").addClass("input-validation-error");
                        showError("验证错误", "请根据合同限制类型选择合同开始时间");
                        return false;
                    }
                    if (contractET == "" || isEmpty(contractET)) {
                        $("input[name='ContractGH.ContractET']").addClass("input-validation-error");
                        showError("验证错误", "请根据合同限制类型选择合同结束时间");
                        return false;
                    }
                }
                break;
            case limitObj.limit2:
                if (contractMatCube == "") {
                    $("input[name='ContractGH.MatCube']").addClass("input-validation-error");
                    showError("验证错误", "请根据合同限制类型设定垫资量");
                    return false;
                }
                break;
            case limitObj.limit3:
                if (contractPrepayCube == "") {
                    $("input[name='ContractGH.PrepayCube']").addClass("input-validation-error");
                    showError("验证错误", "请根据合同限制类型设定预付量");
                    return false;
                }
                break;
            default:
                break;

        }
        return true;
    }
    var ContractGrid = new MyGrid({
        renderTo: "Contractid"
            , title: "合同列表"
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: "buttonToolbar"
            , height: gGridWithTitleHeight * 0.7
        //            , dialogWidth: 800
        //            , dialogHeight: 520
            , storeURL: options.ContractStoreUrl
            , storeCondition: "ContractStatus != '3'"
            , sortByField: "ID"
            , sortOrder: 'Desc'
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
        //, singleSelect: true
            , initArray: [
                { label: "合同编号", name: "ID", index: "ID", width: 80 }
                , { label: "合同号", name: "ContractNo", index: "ContractNo", align: "center", width: 100 }
                , { label: "客户名称", name: "CustName", index: "Customer.CustName", width: 200, searchoptions: { sopt: ["cn"]} }
                , { label: "合同名称", name: "ContractName", index: "ContractName" }
                , { label: "审核状态", name: "AuditStatus", index: "AuditStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "AuditStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("AuditStatus") }, width: 100, align: "center" }
                , { label: "应收金额", name: "TotalComeMoney", index: "TotalComeMoney", width: 60, align: "right", hidden: false }
                , { label: "已收金额", name: "PayMoney", index: "PayMoney", width: 60, align: "right" }
                , { label: "未收余额", name: "Balance", index: "Balance", width: 60, align: "right", hidden: false }
                , { label: "已受限否", name: "IsLimited", index: "IsLimited", align: "center", width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
                , { label: "合同状态", name: "ContractStatus", index: "ContractStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ContractStatus") }, width: 80 }
                , { label: "业务类型", name: "BusinessType", index: "BusinessType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "BusinessType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("BusinessType") }, width: 80, hidden: true }
        //, { label: "合同限制类型", name: "ContractLimitType", index: "ContractLimitType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "CubeLimit" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("CubeLimit") }, width: 80 }
                , { label: "运距", name: "Distance",
                    index: "Distance",
                    width: 60
                },
		        {
		            label: "基准价",
		            name: "BasePrice",
		            index: "BasePrice",
		            width: 60
		        },
                { label: "结算金额", name: "TotalJSMoney", index: "TotalJSMoney", hidden: true }
                , { label: "签订总量", name: "SignTotalCube", index: "SignTotalCube", width: 80, align: "right" }
                , { label: "签订日期", name: "SignDate", index: "SignDate", formatter: "date", width: 80, align: "center", searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ["ge"]} }
                , { label: "签订总金额", name: "SignTotalMoney", index: "SignTotalMoney", width: 80, align: "right" }
                , { label: "客户编码", name: "CustomerID", index: "CustomerID", hidden: true }
                , { label: '结算量系数', name: 'JsCoefficient', index: 'JsCoefficient', width: 180, hidden: true }
                , { label: '签约量系数', name: 'QyCoefficient', index: 'QyCoefficient', width: 180, hidden: true }
                , { label: '回款提成系数', name: 'HkCoefficient', index: 'HkCoefficient', width: 180, hidden: true }
                , { label: '提成单价', name: 'DeductPerPrice', index: 'DeductPerPrice', width: 180, hidden: true }
                , { label: '垫资约定', name: 'DianziString', index: 'DianziString', width: 180, hidden: true }
                , { label: "合同样本", name: "Attachments", formatter: attachmentFmt, sortable: false, search: false }
                , { label: "建设单位", name: "BuildUnit", index: "BuildUnit" }
                , { label: "项目地址", name: "ProjectAddr", index: "ProjectAddr" }
                , { label: "合同类型", name: "ContractType", index: "ContractType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ContractType")} }
                , { label: "计价方式", name: "ValuationType", index: "ValuationType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ValuationType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ValuationType") }, hidden: true }
                , { label: "付款方式", name: "PaymentType", index: "PaymentType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "PaymentType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("PaymentType")} }
                , { label: "月结比例（%）", name: "yuejie", index: "yuejie", hidden: true }
                , { label: "审核人", name: "Auditor", index: "Auditor", hidden: true }
                , { label: "审核时间", name: "AuditTime", index: "AuditTime", formatter: "datetime", hidden: true }
                , { label: "审核意见", name: "AuditInfo", index: "AuditInfo", hidden: true }
                , { label: "业务员", name: "Salesman", index: "Salesman" }
                , { label: "甲方联系人", name: "ALinkMan", index: "ALinkMan", hidden: true }
                , { label: "甲方联系电话", name: "ALinkTel", index: "ALinkTel", hidden: true }
                , { label: "乙方联系人", name: "BLinkMan", index: "BLinkMan", hidden: true }
                , { label: "乙方联系电话", name: "BLinkTel", index: "BLinkTel", hidden: true }
                , { label: "合同开始时间", name: "ContractST", index: "ContractST", formatter: "datetime", searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ["ge"]} }
                , { label: "合同结束时间", name: "ContractET", index: "ContractET", formatter: "datetime", searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ["ge"]} }
                , { label: "结账日期", name: "FootDate", index: "FootDate", formatter: "date", searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ["ge"]} }
                , { label: "付款日期", name: "PaymentDate", index: "PaymentDate", formatter: "date", searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ["ge"] }, hidden: true }
                , { label: "回款联系人", name: "PayBackMan", index: "PayBackMan", hidden: true }
                , { label: "回款联系电话", name: "PayBackTel", index: "PayBackTel", hidden: true }
                , { label: "垫资量", name: "MatCube", index: "MatCube", hidden: true }
                , { label: "预付量", name: "PrepayCube", index: "PrepayCube", hidden: true }
                , { label: "国税", name: "NationalTax", index: "NationalTax", hidden: true }
                , { label: "是否开票", name: "IsInvoice", index: "IsInvoice", align: "center", width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues() }, hidden: true }
                , { label: "是否内部合同", name: "IsInCon", index: "IsInCon", align: "center", width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues() }, hidden: true }
                , { label: "施工单位", name: "ConstructUnit", index: "ConstructUnit" }
                , { label: "期初应收款", name: "InitReceivable", index: "InitReceivable", width: 60, hidden: true }
                , { label: '期初应收额', name: 'PaidIn', index: 'PaidIn', width: 80, formatter: 'currency' }
                , { label: '期初预付额', name: 'PrepayInit', index: 'PrepayInit', width: 80, formatter: 'currency' }
                , { label: '期初应开票', name: 'PiaoPaidIn', index: 'PiaoPaidIn', width: 80, formatter: 'currency' }
                , { label: "备注", name: "Remark", index: "Remark" }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 130 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '最后修改人', name: 'Modifier', index: 'Modifier', width: 130 }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            ]
            , functions: {
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
                        width: 900,
                        height: 500,
                        beforeSubmit: beforeContractFormSubmit,
                        closeDialog: true, //保存不关闭窗体 
                        closeOnEscape: true,
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(response.Data);
                            }
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
                        prefix: 'ContractGH',
                        beforeSubmit: beforeContractFormSubmit,
                        afterFormLoaded: function () {
                            var attDiv = $('#Attachments');
                            attDiv.empty();
                            attDiv.append(data["Attachments"]);
                            $('a[att-id]').show();

                            flag = 1;
                            $("#ContractID").val(data.ID);
                            ContractItemGrid2.refreshGrid("ContractID='" + data.ID + "'");
                            ProjectGrid2.refreshGrid("ContractID='" + data.ID + "'");
                            ContractPumpGrid.refreshGrid("ContractID='" + data.ID + "'");

                            $("#cancelContract").hide(); //隐藏取消操作按钮
                            $("#formula-tabs").tabs("select", 0);
                            $("#formula-tabs").tabs("enable", 1);
                            $("#formula-tabs").tabs("enable", 2);
                        },
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(data.ID);
                            }
                        }
                    });
                }
                , handleDelete: function (btn) {
                    var data = ContractGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showError('提示', '已审核的数据不能删除');
                        return;
                    }
                    ContractGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handleReport: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }

                    var selectedRecord = ContractGrid.getSelectedRecord();
                    window.open("/Reports/Sales/R311402.aspx?uid=" + selectedRecord.ID);
                },
                handleReport1: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }

                    var selectedRecord = ContractGrid.getSelectedRecord();
                    window.open("/Reports/Finance/R020231.aspx?uid=" + selectedRecord.ID);
                },
                handleBitUpdatePrice: function (btn) {
                    $("#DivBitUpdatePriceGH").dialog("open");
                },
                //添加合同明细
                handleAddContractItem: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();
                    if (selectrecord.ContractStatus == 3) {//状态为3表示已完工，取自字典表
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
                            ContractGrid.setFormFieldValue("ContractItemGH.ContractID", selectrecord.ID);
                            $("#ContractName_span").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            ContractItemGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
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
                    if (selectrecord.ContractStatus == 3) {//状态为3表示已完工，取自字典表
                        showMessage('提示', '已完工的合同不能再设置工程明细');
                        return;
                    }
                    ContractGrid.handleAdd({
                        btn: btn,
                        width: 550,
                        height: 350,
                        loadFrom: '/ProjectGH.mvc/Index #projectForm form',
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ContractID", selectrecord.ID);
                            ContractGrid.setFormFieldValue("Distance", selectrecord.Distance);
                            $("#ContractName_project").html(selectrecord.ContractName);
                            $("#ProjectName").val(selectrecord.ContractName);
                            $("#ConstructUnit").val(selectrecord.ConstructUnit);
                            $("#BuildUnit").val(selectrecord.BuildUnit);
                        }
                        , postCallBack: function (response) {
                            ProjectGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                        }
                    });
                },
                //添加工程明细
                handleUpdateProject: function (btn) {
                    var data = ContractGrid.getSelectedRecord();
                    var dataProject = ProjectGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能修改');
                        return;
                    }
                    ProjectGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'projectForm',
                        prefix: 'ContractGH',
                        beforeSubmit: beforeContractFormSubmit,
                        afterFormLoaded: function () {
                            var attDiv = $('#Attachments');
                            attDiv.empty();
                            attDiv.append(data["Attachments"]);
                            $('a[att-id]').show();

                            flag = 1;
                            $("#ContractID").val(data.ID);
                            ContractItemGrid2.refreshGrid("ContractID='" + data.ID + "'");
                            ProjectGrid2.refreshGrid("ContractID='" + data.ID + "'");
                            ContractPumpGrid.refreshGrid("ContractID='" + data.ID + "'");


                            ContractGrid.setFormFieldValue("ContractID", selectrecord.ID);
                            ContractGrid.setFormFieldValue("Distance", selectrecord.Distance);
                            $("#ContractName_project").html(selectrecord.ContractName);
                            $("#ProjectName").val(selectrecord.ContractName);
                            $("#ConstructUnit").val(selectrecord.ConstructUnit);
                            $("#BuildUnit").val(selectrecord.BuildUnit);
                        },
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(data.ID);
                            }
                        }
                    });
                },
                //额外泵送费设定
                //                handleAddExtraPump: function (btn) {
                //                    if (!ContractGrid.isSelectedOnlyOne()) {
                //                        showMessage('提示', '请选择一条记录进行操作！');
                //                        return;
                //                    }
                //                    var selectrecord = ContractGrid.getSelectedRecord();
                //                    ContractGrid.handleAdd({
                //                        btn: btn,
                //                        width: 550,
                //                        height: 350,
                //                        loadFrom: 'ExtraPumpForm',
                //                        btn: btn,
                //                        reloadGrid: false,
                //                        afterFormLoaded: function () {
                //                            ContractGrid.setFormFieldValue("ExtraPump.ContractID", selectrecord.ID);
                //                            $("#ContractName_ex").html(selectrecord.ContractName);
                //                        }
                //                        , postCallBack: function (response) {
                //                            ExtraPumpGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                //                        }
                //                    });
                //                },
                //其他加价设定
                handleOtherPrice: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = ContractGrid.getSelectedRecord();
                    ContractItemGrid.showWindow({
                        title: '其他加价设定',
                        width: 650,
                        height: 400,
                        loadFrom: 'ContractOtherpriceGridAndForm',
                        afterLoaded: function () {
                            $("input[name='ContractOtherPrice.ContractID']").val(selectedRecord.ID);
                            //$("#DisplayContractItemsID").html("<font color=red>" + id + "</font>");

                            contractOtherpriceGrid.getJqGrid().jqGrid('setGridParam', { editurl: options.otherPriceRowDeleteUrl });

                            //contractOtherpriceGrid.getJqGrid().jqGrid('setGridWidth', 450);
                            contractOtherpriceGrid.refreshGrid("ContractID='" + selectedRecord.ID + "'");

                            //计算方式改变时，对应的数量与百分比描述变化
                            var CalcTypelist = $("select[name='ContractOtherPrice.CalcType']");
                            CalcTypelist.unbind("change");
                            CalcTypelist.bind("change", function () {
                                if ($(this).val() == "百分比") {
                                    $("#amountorper label").text("百分比（%）");
                                } else {
                                    $("#amountorper label").text("数量");
                                }
                            });
                        }
                        , buttons: {
                            "关闭": function () {
                                $(this).dialog("close");
                            }
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
                        var postParams = { id: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            ContractGrid.reloadGrid();
                        });
                    }
                    else {
                        ContractGrid.handleEdit({
                            btn: btn,
                            width: 550,
                            height: 350,
                            prefix: 'ContractGH',
                            loadFrom: 'AuditForm',
                            afterFormLoaded: function () {
                                ContractGrid.setFormFieldDisabled('ContractGH.ContractName', true);
                                ContractGrid.setFormFieldDisabled('ContractGH.SignTotalCube', true);
                                ContractGrid.setFormFieldDisabled('ContractGH.SignTotalMoney', true);
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
                        showConfirm("确认信息", "您确定要<font color=green><b>取消审核</b></font>吗？", function () {
                            ajaxRequest(
                                options.contractUnAuditUrl,
                                {
                                    contractID: selectedRecord.ID,
                                    auditStatus: 0
                                },
                                true,
                                function () {
                                    ContractGrid.reloadGrid();
                                }
                            );
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
                        showConfirm("确认信息", "该合同的当前状态为<font color=red>" + dicNormalRenderer(selectedRecord.ContractStatus, 'ContractStatus') + "</font>，您确定要解除限制？", function () {
                            ajaxRequest(
                                btn.data.Url,
                                {
                                    contractID: selectedRecord.ID,
                                    contractStatus: 2, //强制更改为进行中
                                    contractLimitType: 'limit0'

                                },
                                true,
                                function () {
                                    ContractGrid.refreshGrid();
                                }
                            );
                            $(this).dialog("close");
                        });
                    } else {
                        ajaxRequest(
                                btn.data.Url,
                                {
                                    contractID: selectedRecord.ID,
                                    contractStatus: selectedRecord.ContractStatus,
                                    contractLimitType: 'limit0'

                                },
                                true,
                                function (data) {
                                    ContractGrid.refreshGrid();
                                }
                        );
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
                        ajaxRequest(
                                btn.data.Url,
                                {
                                    contractID: selectedRecord.ID

                                },
                                true,
                                function (data) {
                                    ContractGrid.refreshGrid();
                                }
                        );
                        $(this).dialog("close");
                    }
                    //去掉是否填写锁定理由确认 modify by xyl on 2012-4-25
                    //showPrompt("提示", "是否需要填写锁定理由？", showLockReason, unshowLockReason);

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
                    //modify by xyl on 2012-4-9 去除ContractLimitType不为不受限制这个条件
                    //ContractGrid.refreshGrid("ContractLimitType !='" + limitObj.limit0 + "' and IsLimited = 1");
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
                    if (selectrecord.ContractStatus == 3) {//状态为3表示已完工，取自字典表
                        showMessage('提示', '已完工的合同不能再添加合同明细');
                        return;
                    }
                    ContractGrid.handleAdd({
                        title: '导入合同明细',
                        width: 400,
                        height: 350,
                        loadFrom: 'ImportForm',
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("contractID", selectrecord.ID);
                            $("#ContractNameForImportForm").html(selectrecord.ContractName);
                        },
                        beforeSubmit: function () {
                            return true;
                        }
                        , postCallBack: function (response) {
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
                            ContractGrid.setFormFieldValue("ContractGH.ID", selectrecord.ID);
                            ContractGrid.setFormFieldValue("ContractStatus", selectrecord.ContractStatus);

                        }
                        , postCallBack: function (response) {
                            ContractGrid.refreshGrid("1=1"); //回调刷新
                        }
                    });
                },
                handleAddProduceTask: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条合同进行操作！');
                        return;
                    }
                    if (!ProjectGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条工程明细进行操作！');
                        return;
                    }
                    var contractrecord = ContractGrid.getSelectedRecord();
                    var projectrecord = ProjectGrid.getSelectedRecord();
                    if (contractrecord.AuditStatus != '1') {
                        showMessage('提示', '合同未审核或审核不通过！');
                        return;
                    }
                    ProjectGrid.handleAdd({
                        btn: btn,
                        loadFrom: 'ProduceTaskForm',
                        afterFormLoaded: function () {
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.CustName", contractrecord.CustName);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.ContractName", contractrecord.ContractName);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.ContractID", contractrecord.ID);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.ConstructUnit", contractrecord.CustName);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.ProjectName", projectrecord.ProjectName);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.ProjectID", projectrecord.ID);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.ProjectAddr", projectrecord.ProjectAddr);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.BuildUnit", projectrecord.BuildUnit);
                            if (!isEmpty(projectrecord.ConstructUnit))
                                ProjectGrid.setFormFieldValue("ProduceTaskGH.ConstructUnit", projectrecord.ConstructUnit);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.LinkMan", projectrecord.LinkMan);
                            ProjectGrid.setFormFieldValue("ProduceTaskGH.Tel", projectrecord.Tel);
                            bindSelectData(ProjectGrid.getFormField("ProduceTaskGH.ContractItemsID"),
                                options.listContractItemUrl,
                                { textField: 'ConStrength',
                                    valueField: 'ID',
                                    orderBy: 'ConStrength',
                                    ascending: true,
                                    condition: "ContractID='" + contractrecord.ID + "'"
                                }
                            );
                            //合同明细Change事件
                            var contractItemsField = ProjectGrid.getFormField("ProduceTaskGH.ContractItemsID");
                            contractItemsField.unbind('change');
                            contractItemsField.bind('change', function (event) {
                                var cid = contractItemsField.val();
                                bindIdentitySettings('#ProduceTaskFormRightDiv', cid);
                            });
                            //施工部位类型Change事件
                            var consPosTypeField = ProjectGrid.getFormField("ProduceTaskGH.ConsPosType");
                            consPosTypeField.unbind('change');
                            consPosTypeField.bind('change', function (event) {
                                ProjectGrid.getFormField("ProduceTaskGH.ConsPos").val(consPosTypeField.val());
                            });
                            //前场工长Change事件
                            var linkManField = ProjectGrid.getFormField("ProduceTaskGH.LinkMan");
                            linkManField.unbind('change');
                            linkManField.bind('change', function (event) {
                                var linkMan = linkManField.val();
                                if (!isEmpty(linkMan)) {
                                    var requestURL = "User.mvc/Get";
                                    var postParams = { id: linkMan };
                                    var rData;
                                    ajaxRequest(requestURL, postParams, false, function (response) {
                                        if (response) {
                                            rData = response.Data;
                                            ProjectGrid.getFormField("ProduceTaskGH.Tel").val(response.Data == null ? "" : rData.Tel);
                                        }
                                    });
                                }
                            });
                        },
                        closeDialog: false,
                        postCallBack: function (response) {
                            //保存特性
                            if (response.Result) {
                                var uid = response.Data;
                                var postUrl = options.saveTaskIdentitiesUrl;

                                var identities = [];
                                $('#ProduceTaskFormRightDiv input:checked').each(function () {
                                    identities.push($(this).val());
                                });

                                ajaxRequest(postUrl, { taskId: uid, identities: identities }, false, function (response) {
                                    $("#ProduceTaskForm").dialog('close');
                                });
                            }
                        }
                    });
                }
                ,
                handleContractDetail: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();
                    ContractGrid.handleEdit({
                        btn: btn,
                        width: 700,
                        height: 500,
                        loadFrom: 'ContractForm',
                        prefix: 'ContractGH',
                        btn: btn,
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }
                        },
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("Contract.ID", selectrecord.ID);
                            ContractGrid.setFormFieldValue("ContractStatus", selectrecord.ContractStatus);

                            var a = document.getElementsByName("input");
                            for (var t, i = 0, j = a.length; i < j; i++) {
                                t = a[i];
                                //对t进行条件判定(一定条件下...)
                                //continue;
                                t.disabled = "disabled";
                            }
                        }
                        , postCallBack: function (response) {
                            ContractGrid.refreshGrid("1=1"); //回调刷新
                        }
                    });


                },
                handleRefreshShipDocPriceGH: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条合同记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();

                    ContractGrid.handleEdit({
                        btn: btn,
                        width: 500,
                        height: 200,
                        loadFrom: 'divShipDocRefreshPrice',
                        prefix: 'ShipDocRefreshPriceGH',
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ShipDocRefreshPriceGH.ContractID", selectrecord.ID);
                        }
                        , postCallBack: function (response) {
                            ContractGrid.refreshGrid("1=1"); //回调刷新
                        }
                    });

                },
                handleRefreshShipDocFreightGH: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条合同记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();

                    ContractGrid.handleEdit({
                        btn: btn,
                        width: 500,
                        height: 200,
                        loadFrom: 'divShipDocRefreshPrice',
                        prefix: 'ShipDocRefreshPriceGH',
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ShipDocRefreshPriceGH.ContractID", selectrecord.ID);
                        }
                        , postCallBack: function (response) {
                            ContractGrid.refreshGrid("1=1"); //回调刷新
                        }
                    });

                },
                UpdateBalanceRecordAndItems: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedContract = ContractGrid.getSelectedRecord();

                    ajaxRequest(btn.data.Url, {
                        contractID: selectedContract.ID
                    },
					true,
					function (data) {
					    ContractGrid.refreshGrid();
					});
                }







            }
    });
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    ContractGrid.addListeners('rowclick', function (id, record, selBool) {

        //AdvanceMoneyGrid.refreshGrid("ContractID='" + id + "'");
        //ExtraPumpGrid.refreshGrid("ContractID='" + id + "'");
        //ExtraPumpGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ExtraPump.mvc/Delete" });

        ContractItemGrid.getJqGrid().jqGrid(1, 'UnPumpPrice', '', 'not-editable-cell', '');
        ContractItemGrid.getJqGrid().jqGrid(0, 'UnPumpPrice', '', 'not-editable-cell', '');
        $("#ContractItemGrid-datagrid").jqGrid('setCell', 0, 'UnPumpPrice', '', 'not-editable-cell');
        //document.getElementById("ContractItemGrid-datagrid").readOnly = true;
        //$("#ContractItemGrid-datagrid  input").attr("readOnly", "true");

        ContractItemGrid.getJqGrid().setCaption("合同明细：" + record.ContractNo);
        ContractItemGrid.refreshGrid("ContractID='" + id + "'");

        ProjectGrid.getJqGrid().setCaption("合同工程明细：" + record.ContractNo);
        ProjectGrid.refreshGrid("ContractID='" + id + "'");

        ContractPumpGrid2.getJqGrid().setCaption("合同泵送价：" + record.ContractNo);
        ContractPumpGrid2.refreshGrid("ContractID='" + id + "'");

    });


    $("#DivBitUpdatePriceGH").dialog({
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




    //双击事件
    ContractGrid.addListeners('rowdblclick', function (id, record, selBool) {
        if (!ContractGrid.isSelectedOnlyOne()) {
            showMessage('提示', '请选择一条记录进行操作！');
            return;
        }
        var selectrecord = ContractGrid.getSelectedRecord();
        ContractGrid.handleEdit({
            //btn: btn,
            width: 720,
            height: 600,
            loadFrom: 'ContractForm',
            prefix: 'ContractGH',
            title: '查看合同详细',
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            reloadGrid: false,
            afterFormLoaded: function () {

            }
            , postCallBack: function (response) {
                ContractGrid.refreshGrid("1=1"); //回调刷新
            }
        });
    });
    var contractLimitType = $("select[name='ContractGH.ContractLimitType']");
    contractLimitType.unbind("change");
    contractLimitType.bind("change", function () {
        switch ($(this).val()) {
            case limitObj.limit0:
                //resetClass(["ContractGH.ContractST", "ContractGH.ContractET", "ContractGH.MatCube", "ContractGH.PrepayCube"]);
                resetClass({
                    "ContractGH.ContractST": "removeClass",
                    "ContractGH.ContractET": "removeClass",
                    "ContractGH.MatCube": "removeClass",
                    "ContractGH.PrepayCube": "removeClass"
                });
                break;
            case limitObj.limit1:
                resetClass({
                    "ContractGH.ContractST": "addClass",
                    "ContractGH.ContractET": "addClass",
                    "ContractGH.MatCube": "removeClass",
                    "ContractGH.PrepayCube": "removeClass"
                });
                break;
            case limitObj.limit2:
                resetClass({
                    "ContractGH.ContractST": "removeClass",
                    "ContractGH.ContractET": "removeClass",
                    "ContractGH.MatCube": "addClass",
                    "ContractGH.PrepayCube": "removeClass"
                });
                break;
            case limitObj.limit3:
                resetClass({
                    "ContractGH.ContractST": "removeClass",
                    "ContractGH.ContractET": "removeClass",
                    "ContractGH.MatCube": "removeClass",
                    "ContractGH.PrepayCube": "addClass"
                });
                break;
            default:
                break;

        }
    });

    ContractGrid.getJqGrid().jqGrid('setGridWidth', $("#Contractid").width() - 6);
    //合同置为完工状态操作
    function setcmp(b, grid, isCompleted) {
        if (!grid.isSelectedOnlyOne()) {
            showMessage('提示', '请选择一条记录进行操作！');
            return;
        }
        var selectedRecord = grid.getSelectedRecord();
        ajaxRequest(
                b.data.Url,
                {
                    contractID: selectedRecord.ID
                },
                true,
                function () {
                    grid.refreshGrid();
                },
                null,
                b
            );
    }
    //合同明细
    var ContractItemGrid = new MyGrid({
        renderTo: 'ContractItemGrid'
            , title: '合同明细'
            , autoWidth: true
            , buttons: buttons6
            , height: gGridWithTitleHeight * 0.3
            , storeURL: options.itemStoreUrl
            , sortByField: 'ConStrength'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 50
            , showPageBar: true
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 200
            , singleSelect: true
            , editSaveUrl: options.itemUpdateUrl //编辑URL
            , initArray: [
                { label: '合同明细编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', hidden: true }
                , { label: '操作', name: 'act', index: 'act', width: 120, sortable: false, align: "center", hidden: false }
                , { label: ' 审核状态', name: 'AuditStatus', index: 'AuditStatus', width: 56, align: "center", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, hidden: true }
                , { label: '标号', name: 'ConStrength', index: 'ConStrength', width: 60, align: 'center' }
                , { label: '单价', name: 'UnPumpPrice', index: 'UnPumpPrice', width: 60, align: "right", formatter: 'number', editable: false, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '泵送费', name: 'PumpCost', hidden: true, index: 'PumpCost', width: 50, align: "right", formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '<font color=red>价格</font>', name: 'PumpPrice', width: 50, align: "right", formatter: 'number', index: 'PumpPrice', formatoptions: { thousandsSeparator: "," }, sortable: false, hidden: true }
                , { label: '额外费', name: 'ExMoney', index: 'ExMoney', width: 60, align: "right", formatter: 'number', editable: false, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '<font color=green>砂浆价格</font>', name: 'SlurryPrice', width: 50, index: 'SlurryPrice', formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: '运费', name: 'TranPrice', width: 50, index: 'TranPrice', formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: '基准标号', name: 'BaseConStrength', index: 'BaseConStrength', width: 70, align: 'center', editable: true, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "BaseConStrength" }, stype: "select", searchoptions: { value: getCarNoStr() }, edittype: 'select', editoptions: { value: getCarNoStr() }, hidden: true }
                , { label: '建立时间', name: 'BuildTime', index: 'BuildTime', width: 200, formatter: 'datetime' }
                , { label: '建立人', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 60, align: 'center' }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', width: 110, formatter: 'datetime' }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 60, align: 'center' }
            ]
            , functions: {
                handleReload: function (btn) {
                    ContractItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractItemGrid.refreshGrid();
                },
                handleEdit: function (btn) {
                    var selectrecord = ContractGrid.getSelectedRecord();
                    if (selectrecord.AuditStatus == 1) {
                        showMessage("提示", "明细所属合同已通过审核，不能修改合同明细!");
                        return;
                    }
                    ContractItemGrid.handleEdit({
                        loadFrom: 'ContractItemForm',
                        prefix: 'ContractItemGH',
                        btn: btn,
                        width: 400,
                        height: 250,
                        afterFormLoaded: function () {
                            $("#ContractName_span").html(selectrecord.ContractName);
                        }
                    });
                }
                , handleDeleteContractItem: function (btn) {
                    ContractItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }

            }
    });

    //        var ExtraPumpGrid = new MyGrid({
    //            renderTo: 'ExtraPumpGrid'
    //            , title: '额外泵送费收取规则'
    //            , autoWidth: true
    //            , buttons: buttons1
    //            , height: (gGridWithTitleHeight-112) / 4
    //            , storeURL: '/ExtraPump.mvc/Find'
    //            , sortByField: 'Text'
    //            , sortOrder: 'ASC'
    //            , primaryKey: 'ID'
    //            , setGridPageSize: 50
    //            , groupingView: { groupText: ['{0}'] }
    //            , groupField: 'Type'
    //            , showPageBar: false
    //            , autoLoad: false
    //            , dialogWidth: 600
    //            , dialogHeight: 200
    //            , singleSelect: true
    //            , editSaveUrl: '/ExtraPump.mvc/Update'
    //            , initArray: [
    //                { label: '编号', name: 'ID', index: 'ID', hidden: true }
    //                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
    //                , { label: '类型', name: 'Type', index: 'Type', hidden: true }
    //                , { label: '限度', name: 'Text', index: 'Text', width: 50, editable: true }
    //                , { label: '取值', name: 'Value', index: 'Value', width: 50, editable: true }
    //                , { label: "操作", name: "myac", width: 80, fixed: true, sortable: false, resize: false, formatter: "actions",
    //                    formatoptions: { keys: true, editbutton: false}
    //                }
    //            ]
    //            , functions: {
    //                handleReload: function (btn) {
    //                    ContractItemGrid.reloadGrid();
    //                },
    //                handleRefresh: function (btn) {
    //                    ContractItemGrid.refreshGrid();
    //                }

    //            }
    //        });

    //grid行操作栏按钮（删除、价格变动）
    ContractItemGrid.addListeners("gridComplete", function () {
        var records = ContractItemGrid.getAllRecords();
        var rid, buildtime, status, consPrice;
        for (var i = 0; i < records.length; i++) {
            rid = records[i].ID;
            status = records[i].AuditStatus;
            buildtime = records[i].BuildTime;
            consPrice = records[i].UnPumpPrice;
            be = "<input class='identityButton'  type='button' value='价格变动' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handlePriceSet(" + rid + ",'" + buildtime + "'," + consPrice + ");\"  />";
            de = "<div  onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleAudit('" + rid + "','" + status + "');\" style='float:left;margin-right:5px;display:none;' title='审核所选记录'><span class='ui-icon ui-icon-check'></span></div>";
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteContractItem(" + rid + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-right:15px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";

            ContractItemGrid.getJqGrid().jqGrid('setRowData', rid, { act: be + de + ce });
        }
    });

    //审核标号明细
    window.handleAudit = function (id, status) {

        if (status == "0") {
            showConfirm("确认", "您确定<font style='color:green;'><strong>审核</strong></font>此标号明细吗？", function () {
                $.post(
                        "/ContractItemGH.mvc/Audit"
                        , { ID: id }
                        , function (data) {
                            if (!data.Result) {
                                showError("出错啦！", data.Message);
                                return false;
                            }
                            ContractItemGrid.reloadGrid();
                        }

                    );
                $(this).dialog("close");
            });
        }
        else {
            showConfirm("确认", "您确定<font style='color:red;'><strong>取消审核</strong></font>此标号明细吗？", function () {
                $.post(
                        "/ContractItemGH.mvc/UnAudit"
                        , { consItemsID: id, auditStatus: 0 }
                        , function (data) {
                            if (!data.Result) {
                                showError("出错啦！", data.Message);
                                return false;
                            }
                            ContractItemGrid.reloadGrid();
                        }

                    );
                $(this).dialog("close");
            });
        }

    }
    ContractItemGrid.addListeners("afterSubmitCell", function () {
        ContractItemGrid.reloadGrid();

    });

    //运输车号列表
    function getCarNoStr() {
        //动态生成select内容
        var requestURL = "/ContractGH.mvc/getConList";
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
                //return str;
            }
        });
        return str;
    }

    //弹出【价格变动】窗体
    window.handlePriceSet = function (id, buildtime, consPrice) {

        ContractItemGrid.showWindow({
            title: '价格变动',
            width: 620,
            height: 400,
            loadFrom: 'PriceSetForm'
                        , afterLoaded: function () {

                            //显示合同明细项目ID
                            $("input[name='PriceSetting.ContractItemsID']").val(id);
                            $("#DisplayContractItemsID1").html("<font color=red>" + id + "</font>");
                            //显示合同明细项目建立时间
                            $("input[name='ContractItemGH.BuildTime']").val(buildtime);
                            $("#DisplayContractItemsBTime").html("<font color=red>" + buildtime + "</font>");

                            $("#baseConsPrice").val(consPrice);
                            priceSettingGrid.getJqGrid().jqGrid('setGridParam', { editurl: options.priceRowDeleteUrl });
                            priceSettingGrid.refreshGrid("ContractItemsID='" + id + "'");
                        }
                        , buttons: {
                            "关闭": function () {
                                $(this).dialog("close");
                            }
                        }
        });
    };

    window.handleIdentitySet = function (id) {

        ContractItemGrid.showWindow({
            title: '特性设定',
            width: 600,
            height: 450,
            loadFrom: 'IdentitySetForm'
                        , afterLoaded: function () {
                            $("input[name='IdentitySetting.ContractItemsID']").val(id);
                            $("#DisplayContractItemsID").html("<font color=red>" + id + "</font>");

                            identitySettingGrid.getJqGrid().jqGrid('setGridParam', { editurl: options.identityRowDeleteUrl });

                            identitySettingGrid.getJqGrid().jqGrid('setGridWidth', 450);
                            identitySettingGrid.refreshGrid("ContractItemsID='" + id + "'");

                            //下拉列表级联绑定
                            var identityTypelist = $("select[name='IdentitySetting.IdentityType']");
                            identityTypelist.unbind("change");
                            identityTypelist.bind("change", function () {
                                bindSelectData(
                                    $("select[name='IdentitySetting.IdentityName']"),
                                    options.listDataUrl,
                                    { textField: "IdentityName", valueField: "IdentityName", condition: "IdentityType='" + $(this).val() + "'" }
                                );
                            });


                            var identityNamelist = $("select[name='IdentitySetting.IdentityName']");
                            identityNamelist.unbind("change");
                            identityNamelist.bind("change", function () {
                                var requestURL = options.getIdentityPriceUrl;
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
    };

    //保存特性设定函数
    window.identitySave = function () {
        var _ContractItemsID = $("[name='IdentitySetting.ContractItemsID']").val();
        $.post(
                options.identityAddUrl,
                $("#IdentitySetForms").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    $("#IdentitySetForms")[0].reset();
                    identitySettingGrid.refreshGrid("ContractItemsID='" + _ContractItemsID + "'");
                }
            );
    };

    //改变浮动率计算新单价
    $("#freeRate").bind("change", function () {

        var bprice = $("#baseConsPrice").val();
        var feerate = $("#freeRate").val();
        if (bprice == "") {
            return;
        }
        if (feerate == "") {
            return;
        }
        var nprice = bprice * 1 * (1 + feerate / 100);
        $("[name='PriceSetting.UnPumpPrice']").val(nprice.toFixed(2));
    });


    //保存价格变动函数
    window.priceSave = function () {
        var _ContractItemsID = $("[name='PriceSetting.ContractItemsID']").val();
        var btime = $("[name='ContractItemGH.BuildTime']").val(); //合同明细项目建立时间
        var xchangedate = $("[name='PriceSetting.ChangeTime']").val(); //改价日期
        var xchangetime = $("[name='PriceSetting.ChangeTime']").val() + " 00:00:00"; //改价时间
        //        if (compareTime(btime, xchangetime)) {
        //            showError('错误', '改价时间不能大于合同明细项目建立时间！');
        //            return;
        //        }
        var records = priceSettingGrid.getAllRecords(); //获取所有价格变动列表记录
        for (var i = 0; i < records.length; i++) {
            if (records[i].ChangeTime == xchangedate) {
                showError('错误', '此改价时间已经存在！');
                return;
            }
        }
        $.post(
                options.priceAddUrl,
                $("#PriceSetForms").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    //$("#PriceSetForms")[0].reset();
                    $("[name='PriceSetting.ChangeTime']").val("");
                    $("#freeRate").val("0");
                    $("#PriceSetting_UnPumpPrice").val("");
                    priceSettingGrid.refreshGrid("ContractItemsID='" + _ContractItemsID + "'");
                }
            );
    };

    //保存合同锁定理由
    window.lockSave = function () {
        ajaxRequest(
                    options.saveLockUrl,
                    {
                        contractID: $("#ContractIDlock").val(),
                        remark: $("#lockReason").val()
                    },
                    true,
                    function (data) {
                        $("#lockReason").val("");
                        $("#lockReasonForm").dialog("close");
                        ContractGrid.refreshGrid();
                    }
            );
    };

    window.handleDeleteContractItem = function (id) {
        showConfirm("确认信息", "您确定删除此项合同明细？", function () {
            $.post(
                    options.itemDeleteUrl
                    , { ID: id }
                    , function (data) {
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

    ContractItemGrid.getJqGrid().jqGrid('setGridWidth', $("#ContractItemGrid").width() - 6);

    //特性设定
    var identitySettingGrid = new MyGrid({
        renderTo: 'identitySettingGrid'
            , autoWidth: true
            , buttons: buttons2
            , storeURL: options.identityStoreUrl
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
            , setGridPageSize: 30
            , showPageBar: true
            , altclass: 'identityButton'
            , altRows: true
            , autoLoad: false
            , editSaveUrl: options.identityUpdateUrl
            , initArray: [
                { label: '特性设定编号', name: 'IdentitySettingID', index: 'IdentitySettingID', hidden: true }
                , { label: '合同明细编号', name: 'ContractItemsID', index: 'ContractItemsID', hidden: true }
                , { label: '特性类型', name: 'IdentityType', index: 'IdentityType', width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'IdenType'} }
                , { label: '详细特性', name: 'IdentityName', index: 'IdentityName', width: 100 }
                , { label: '特性价格', name: 'IdentityPrice', index: 'IdentityPrice', editable: true, width: 100 }
                , { label: '操作', name: 'myac', width: 80, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
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

    //价格设定
    var priceSettingGrid = new MyGrid({
        renderTo: 'priceSettingGrid'
            , autoWidth: true
            , buttons: buttons2
            , storeURL: options.priceStoreUrl
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , dialogWidth: 900
            , dialogHeight: 700
            , setGridPageSize: 30
            , showPageBar: true
            , altclass: 'identityButton'
            , altRows: true
            , autoLoad: false
            , editSaveUrl: options.priceUpdateUrl
            , initArray: [
                { label: '特性设定编号', name: 'PriceSettingID', index: 'PriceSettingID', hidden: true }
                , { label: '合同明细编号', name: 'ContractItemsID', index: 'ContractItemsID', hidden: true }
                , { label: '订价时间', name: 'ChangeTime', index: 'ChangeTime', formatter: "date", editable: true, width: 90 }
                , { label: '单价', name: 'UnPumpPrice', index: 'UnPumpPrice', editable: true, width: 80 }
                , { label: '增加费', name: 'PumpCost', index: 'PumpCost', hidden: true, editable: true, width: 80 }
                , { label: '砂浆价格', name: 'SlurryPrice', index: 'SlurryPrice', hidden: true, editable: true, width: 90 }
                , { label: '操作', name: 'myac', width: 50, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 80 }
            ]
            , functions: {
                handleReload: function (btn) {
                    priceSettingGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    priceSettingGrid.refreshGrid();
                }
            }
    });


    //其他加价设定
    var contractOtherpriceGrid = new MyGrid({
        renderTo: 'contractOtherpriceGrid'
        //, autoWidth: true
            , width: 610
        //, buttons: buttons4
            , height: 150
            , storeURL: options.contractOtherPriceStoreUrl
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , showPageBar: true
            , editSaveUrl: options.otherPriceUpdateUrl
            , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 47 }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 88, align: 'center' }
                , { label: '加价项目', name: 'PriceType', index: 'PriceType', width: 110 }
                , { label: '计算方式', name: 'CalcType', index: 'CalcType', width: 70, editable: true, edittype: 'select', editoptions: { value: "百分比:百分比;公斤:公斤;固定:固定"} }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width: 50, formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '数量/百分比', name: 'Amount', index: 'Amount', width: 80, formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '全加', name: 'IsAll', index: 'IsAll', width: 50, sortable: false, formatter: 'checkbox', align: 'center', editable: true, edittype: 'checkbox', editoptions: { value: "true:false"} }
                , { label: '操作', name: 'myac', width: 50, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
            ]
            , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    contractOtherpriceGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    contractOtherpriceGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    contractOtherpriceGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    contractOtherpriceGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    contractOtherpriceGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //保存其他加价设定函数
    window.otherPriceSave = function () {
        var _ContractID = $("[name='ContractOtherPrice.ContractID']").val();
        $.post(
                options.otherPriceAddUrl,
                $("#otherPriceForms").serialize(),
                function (data) {
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    $("#otherPriceForms")[0].reset();
                    contractOtherpriceGrid.refreshGrid("ContractID='" + _ContractID + "'");
                }
            );
    };

    $('a[data-id]').live('click', function (e) {
        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;

        var data = ContractGrid.getRecordByKeyValue($(this).attr('data-id'));

        $('a[att-id]').hide();
    });
    $('a[att-id]').live('click', function (e) {
        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;
        var caller = $(this);

        ajaxRequest(options.deleteAttachmentUrl,
             { id: caller.attr('att-id') },
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

        $.ajaxFileUpload
                ({
                    url: options.uploadUrl + '?objectType=ContractGH&objectId=' + objectId,
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
                        }
                        else {
                            showError('附件上传失败', data.Message);
                        }
                    },
                    error: function (data, status, e) {
                        showError(e);
                    }
                }
        );
        return false;

    }

    var ProjectGrid = new MyGrid({
        renderTo: 'ProjectGrid'
            , title: '合同工程明细'
            , autoWidth: true
            , height: gGridWithTitleHeight * 0.3
            , dialogWidth: 920
            , dialogHeight: 510
            , storeURL: options.ProjectStoreUrl
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , setGridPageSize: 100
            , showPageBar: false
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: '/ProjectGH.mvc/Update'
            , initArray: [
                  { label: '工程编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', editable: true, editrules: { required: true} }
                , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', editable: true }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', editable: true }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', editable: true }
                , { label: '工程运距', name: 'Distance', index: 'Distance', editable: true, width: 80 }
                , { label: '工地联系人', name: 'LinkMan', index: 'LinkMan', editable: true, width: 80 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', editable: true, width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', editable: true }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName' }

		        , {
		            label: '操作',
		            name: 'act',
		            index: 'act',
		            width: 200,
		            sortable: false,
		            align: "center"
		        }
            ]
            , functions: {
                handleReload: function (btn) {
                    ProjectGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ProjectGrid.refreshGrid();
                }
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
    window.handleDeleteProject = function (id) {

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
    function bindIdentitySettings(container, contractItemId, taskId) {
        var container = $(container);
        container.empty();
        if (isEmpty(contractItemId)) return;
        ajaxRequest(options.findIdentitySettingsUrl,
            { contractItemId: contractItemId, taskId: taskId },
            false,
            function (response) {
                for (var i = 0; i < response.length; i++) {
                    var item = response[i];
                    var identDiv = $("<div>").appendTo(container)
                                        .attr("id", "Ident_" + i)
                                        .addClass('identbox ui-corner-all')
                                        .append('<div class="titlebar ui-widget-header ui-corner-all ui-helper-clearfix">' + item.IdentType.DicName + '</div>');
                    identDiv.checkboxlist({ data: item.IdentItems });
                }
            });
    }

    var ContractConstrenth = new MyGrid({
        renderTo: "ContractConstrenth"
            , title: "合同标号"
            , autoWidth: true
            , buttons: buttons1
            , height: 300
            , storeURL: options.itemStoreUrl
            , sortByField: "ConStrength"
            , sortOrder: "ASC"
            , primaryKey: "ID"
            , setGridPageSize: 50
            , showPageBar: true
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 400
            , singleSelect: true
            , editSaveUrl: options.itemUpdateUrl
            , initArray: [
                { label: "合同明细编号", name: "ID", index: "ID", hidden: true }
                , { label: "合同编号", name: "ContractID", index: "ContractID", hidden: true }
                , { label: "合同名称", name: "ContractName", index: "ContractName", hidden: true }
                , { label: "标号", name: "ConStrength", index: "ConStrength", width: 50, align: "center" }
                , { label: "非泵价格", name: "UnPumpPrice", index: "UnPumpPrice", width: 50, align: "right", formatter: "number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: "泵送费", name: "PumpCost", index: "PumpCost", width: 50, align: "right", formatter: "number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: "<font color=red>泵送价格</font>", name: "PumpPrice", width: 50, align: "right", formatter: "number", index: "PumpPrice", formatoptions: { thousandsSeparator: "," }, sortable: false, hidden: true }
                , { label: "<font color=green>砂浆价格</font>", name: "SlurryPrice", width: 50, index: "SlurryPrice", formatter: "number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: "操作", name: "act", index: "act", width: 90, sortable: false, align: "center", hidden: true }
            ]
            , functions: {
                handleReload: function (btn) {
                    ContractConstrenth.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractConstrenth.refreshGrid();
                }

            }
    })
    //合同泵车价格设定
    var ContractPumpGrid2 = new MyGrid({
        renderTo: 'ContractPumpGrid2'
        , autoWidth: true
        //, buttons: buttons9
        , height: (gGridWithTitleHeight - 200) / 3
		, storeURL: '/ContractPump.mvc/Find'
		, sortByField: 'ID'
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: false
        , singleSelect: true
        , rowNumbers: false
        , storeCondition: '1<>1'
		, initArray: [
            { label: ' 编号', name: 'ID', index: 'ID', hidden: true }
            , { label: ' 合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
            , { label: ' 日期', name: 'SetDate', index: 'SetDate', width: 100, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge'] }, editable: true }
        //, { label: ' 泵车', name: 'PumpType', index: 'PumpType', width: 90, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CastMode' }, editable: true, edittype: 'select', editoptions: { value: dicToolbarSearchValues('CastMode') }, editrules: { required: true} }
            , { label: ' 泵车价格', name: 'PumpPrice', index: 'PumpPrice', editable: true, formatter: 'number', editrules: { number: true }, align: 'right', width: 60 }
            , { label: '操作', name: 'delac', width: 80, fixed: true, sortable: false, resize: false, formatter: 'actions',
                formatoptions: { keys: true, editbutton: false }
            }
		]
		, autoLoad: false
        , editSaveUrl: '/ContractPump.mvc/Update'
        , functions: {
            handleReload: function (btn) {
                ContractPumpGrid2.reloadGrid();
            },
            handleRefresh: function (btn) {
                ContractPumpGrid2.refreshGrid('1=1');
            },
            handleAdd: function (btn) {
                ContractPumpGrid2.handleAdd({
                    loadFrom: 'ContractPumpForm',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                ContractPumpGrid2.handleEdit({
                    loadFrom: 'ContractPumpForm',
                    btn: btn
                });
            }
            , handleDelete: function (btn) {
                ContractPumpGrid2.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    ContractPumpGrid2.addListeners("cellclick", function (id, record) {
        var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
        $("#" + rowno + "_SetDate").datepicker();
    });
    //删除操作
    ContractPumpGrid2.addListeners("gridComplete", function () {
        ContractPumpGrid2.getJqGrid().jqGrid('setGridParam', { editurl: "/ContractPump.mvc/Delete" });
    });


    //合同明细2
    var ContractItemGrid2 = new MyGrid({
        renderTo: 'ContractItemGrid2'
            , title: '合同明细'
            , autoWidth: true
            , buttons: buttons4
            , height: gGridWithTitleHeight - 330
            , storeURL: options.itemStoreUrl
            , sortByField: 'ConStrength'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 50
            , showPageBar: false
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 200
            , singleSelect: true
            , editSaveUrl: options.itemUpdateUrl //编辑URL
            , initArray: [
                { label: '合同明细编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', hidden: true }
                , { label: '操作', name: 'act', index: 'act', width: 150, sortable: false, align: "center" }
                , { label: '标号', name: 'ConStrength', index: 'ConStrength', width: 80, align: 'center' }
                , { label: '单价', name: 'UnPumpPrice', index: 'UnPumpPrice', width: 80, align: "right", formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '泵送费', name: 'PumpCost', hidden: true, index: 'PumpCost', width: 50, align: "right", formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '<font color=red>价格</font>', name: 'PumpPrice', width: 50, align: "right", formatter: 'number', index: 'PumpPrice', formatoptions: { thousandsSeparator: "," }, sortable: false, hidden: true }
                , { label: '额外费', name: 'ExMoney', index: 'ExMoney', width: 80, align: "right", formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: '<font color=green>砂浆价格</font>', name: 'SlurryPrice', width: 50, index: 'SlurryPrice', formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: '运费', name: 'TranPrice', width: 50, index: 'TranPrice', formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: '基准标号', name: 'BaseConStrength', index: 'BaseConStrength', width: 70, align: 'center', editable: true, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "BaseConStrength" }, stype: "select", searchoptions: { value: getCarNoStr() }, edittype: 'select', editoptions: { value: getCarNoStr() }, hidden: true }
                , { label: '建立时间', name: 'BuildTime', index: 'BuildTime', width: 110, formatter: 'datetime', hidden: true }
            ]
            , functions: {
                handleReload: function (btn) {
                    ContractItemGrid2.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractItemGrid2.refreshGrid();
                },
                handleImport: function (btn) {

                    if (flag == 0) {
                        if (!$("#ContractGH_ID").val()) {
                            showError('提示', '请先保存合同基本信息！');
                            return;
                        }
                    }
                    if (flag == 1) {
                        if (!ContractGrid.isSelectedOnlyOne()) {
                            showError('提示', '请选择一条记录进行操作！');
                            return;
                        }
                    }

                    //                    var selectrecord = ContractGrid.getSelectedRecord();
                    //                    if (selectrecord.ContractStatus == 3) {//状态为3表示已完工，取自字典表
                    //                        showMessage('提示', '已完工的合同不能再添加合同明细');
                    //                        return;
                    //                    }
                    ContractItemGrid2.handleAdd({
                        title: '导入合同明细',
                        width: 400,
                        height: 350,
                        loadFrom: 'ImportForm',
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            if (flag == 0) {
                                var contractid = $("#ContractGH_ID").val();
                                $("#contractID").val(contractid);
                            }
                            if (flag == 1) {
                                var selectrecord = ContractGrid.getSelectedRecord();
                                $("#contractID").val(selectrecord.ID);

                            }
                            //ContractGrid.setFormFieldValue("contractID", contractid);
                            //$("#ContractNameForImportForm").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            var contractid;
                            if (flag == 0) {
                                contractid = $("#ContractGH_ID").val();
                            }
                            if (flag == 1) {
                                var selectrecord = ContractGrid.getSelectedRecord();
                                contractid = (selectrecord.ID);
                            }
                            ContractItemGrid.refreshGrid("ContractID='" + contractid + "'");
                            ContractItemGrid2.refreshGrid("ContractID='" + contractid + "'");

                            //判断是否都有输入行

                            $(".ui-dialog-titlebar-close").show(); //显示关闭按钮

                            var pumpGridIds = ContractPumpGrid.getAllRecords();
                            if (pumpGridIds.length * 1 > 0) {
                                $(".ui-dialog-titlebar-close").show(); //显示关闭按钮
                            }
                        }
                    });
                }
            }
    });
    //grid行操作栏按钮（删除、价格变动）
    ContractItemGrid2.addListeners("gridComplete", function () {
        var records = ContractItemGrid2.getAllRecords();
        var rid, buildtime, status, consPrice;
        for (var i = 0; i < records.length; i++) {
            rid = records[i].ID;
            status = records[i].AuditStatus;
            buildtime = records[i].BuildTime;
            consPrice = records[i].UnPumpPrice;
            be = "<input class='identityButton'  type='button' value='价格变动' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handlePriceSet(" + rid + ",'" + buildtime + "'," + consPrice + ");\"  />";
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteContractItem2(" + rid + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";

            ContractItemGrid2.getJqGrid().jqGrid('setRowData', rid, { act: de });
        }
    });
    window.handleDeleteContractItem2 = function (id) {
        showConfirm("确认信息", "您确定删除此项合同明细？", function () {
            $.post(
                    options.itemDeleteUrl
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        ContractItemGrid.reloadGrid();
                        ContractItemGrid2.reloadGrid();
                    }
                );
            $(this).dialog("close");
        });
    };

    //工程明细2
    var ProjectGrid2 = new MyGrid({
        renderTo: 'ProjectGrid2'
            , title: '合同工程明细'
            , autoWidth: false
            , buttons: buttons3
            , width: 600
            , height: gGridWithTitleHeight - 330
            , dialogWidth: 920
            , dialogHeight: 510
            , storeURL: options.ProjectStoreUrl
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , setGridPageSize: 100
            , showPageBar: false
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: '/ProjectGH.mvc/Update'
            , initArray: [
                { label: '工程编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', editable: true, editrules: { required: true} }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', editable: true }
                , { label: '工程运距', name: 'Distance', index: 'Distance', editable: true, width: 80 }
                , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', editable: true }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', editable: true }
                , { label: '工地联系人', name: 'LinkMan', index: 'LinkMan', editable: true, width: 80 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', editable: true, width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', editable: true }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName' }
            ]
            , functions: {
                handleReload: function (btn) {
                    ProjectGrid2.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ProjectGrid2.refreshGrid();
                },
                //添加工程明细
                handleAddProject: function (btn) {
                    if (flag == 0) {
                        if (!$("#ContractGH_ID").val()) {
                            showError('提示', '请先保存合同基本信息！');
                            return;
                        }
                    }
                    if (flag == 1) {
                        if (!ContractGrid.isSelectedOnlyOne()) {
                            showError('提示', '请选择一条记录进行操作！');
                            return;
                        }
                    }
                    ProjectGrid2.handleAdd({
                        btn: btn,
                        width: 550,
                        height: 350,
                        loadFrom: '/ProjectGH.mvc/Index #projectForm form',
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            if (flag == 0) {
                                var contractid = $("#ContractGH_ID").val();
                                $("#ContractID").val(contractid);
                                $("#ContractName_project").html(ContractGrid.getFormField("ContractGH.ContractName").val());
                                //$("#ProjectName").val($($("input[id=Contract_ContractName]")[2]).val()); //页面有2个id=Contract_ContractName的元素

                                ProjectGrid2.setFormFieldValue("ProjectName", ContractGrid.getFormField("ContractGH.ContractName").val());
                                ProjectGrid2.setFormFieldValue("ConstructUnit", ContractGrid.getFormField("ContractGH.ConstructUnit").val());
                                ProjectGrid2.setFormFieldValue("BuildUnit", ContractGrid.getFormField("ContractGH.BuildUnit").val());
                            }
                            if (flag == 1) {
                                var selectrecord = ContractGrid.getSelectedRecord();
                                $("#ContractID").val(selectrecord.ID);
                                $("#ContractName_project").html(selectrecord.ContractName);
                                $("#ProjectName").val(selectrecord.ContractName);
                                $("#ConstructUnit").val(selectrecord.ConstructUnit);
                                $("#BuildUnit").val(selectrecord.BuildUnit);
                            }

                        }
                        , postCallBack: function (response) {
                            var contractid;
                            if (flag == 0) {
                                contractid = $("#ContractGH_ID").val();
                            }
                            if (flag == 1) {
                                var selectrecord = ContractGrid.getSelectedRecord();
                                contractid = (selectrecord.ID);
                            }
                            ProjectGrid.refreshGrid("ContractID='" + contractid + "'");
                            ProjectGrid2.refreshGrid("ContractID='" + contractid + "'");
                        }
                    });
                }
            }
    });

    //合同泵车价格设定
    var ContractPumpGrid = new MyGrid({
        renderTo: 'ContractPumpGrid'
            , autoWidth: true
        //, buttons: buttons0
            , height: gGridWithTitleHeight - 150
		    , storeURL: '/ContractPump.mvc/Find'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            , storeCondition: '1<>1'
		    , initArray: [
                { label: ' 编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: ' 日期', name: 'SetDate', index: 'SetDate', width: 100, editable: true, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
        //, { label: ' 泵车类型', name: 'PumpType', index: 'PumpType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CastMode' }, width: 120, editable: true, edittype: 'select', editoptions: { value: dicToolbarSearchValues('CastMode') }, editrules: { required: true} }
                , { label: ' 泵车价格', name: 'PumpPrice', index: 'PumpPrice', editable: true, formatter: 'number', editrules: { number: true }, align: 'right', width: 100 }
                , { label: '操作', name: 'delac', width: 80, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
		    ]
		    , autoLoad: false
            , editSaveUrl: '/ContractPump.mvc/Update'
            , functions: {
                handleReload: function (btn) {
                    ContractPumpGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractPumpGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    ContractPumpGrid.handleAdd({
                        loadFrom: 'ContractPumpForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    ContractPumpGrid.handleEdit({
                        loadFrom: 'ContractPumpForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    ContractPumpGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //cellclick 事件定义，选择日期列单元格弹出日期选择控件 (lzl add 2016-07-26)
    ContractPumpGrid.addListeners("cellclick", function (id, record) {
        var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
        $("#" + rowno + "_SetDate").datepicker();
    });

    //导入泵车
    window.ImportPump = function () {
        var _ContractID = $("#ContractGH_ID").val();
        if (isEmpty(_ContractID)) {
            showError('提示', '请先保存合同基本信息！');
            return false;
        }
        ContractPumpGrid.showWindow({
            title: '导入泵车',
            width: 400,
            height: 300,
            loadFrom: 'ImportPumpForm',
            afterLoaded: function () {
                pumpTypeGrid.refreshGrid("ParentID='CastMode'");
            },
            buttons:
                { "关闭": function () {
                    $(this).dialog("close");
                }, "确定": function () {
                    var ids = pumpTypeGrid.getSelectedKeys();
                    if (ids.length <= 0) {
                        $(this).dialog("close");
                        return false;
                    } else {
                        ajaxRequest("/ContractPump.mvc/ImportEntitys", { ids: ids, contractID: _ContractID }, true, function (data) {
                            $("#ImportPumpForm").dialog("close");
                            ContractPumpGrid.refreshGrid("ContractID='" + _ContractID + "'");
                            //判断是否都有输入行
                            var contractItemGridIds2 = ContractItemGrid2.getAllRecords();

                            if (contractItemGridIds2.length * 1 > 0) {
                                $(".ui-dialog-titlebar-close").show(); //显示关闭按钮
                            }
                        });
                    }

                }
                }
        });
    };

    //保存泵车价格
    window.pumpPriceSave = function () {
        var _ContractID = $("#ContractID").val();
        if (isEmpty(_ContractID)) {
            showError('提示', '请先保存合同基本信息！');
            return false;
        }
        $.post(
                "/ContractPump.mvc/Add",
                $("#ContractPumpForms").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    //$("#ContractPumpForms")[0].reset();
                    $("#PumpType").val("");
                    $("#PumpPrice").val("");
                    ContractPumpGrid.refreshGrid("ContractID='" + _ContractID + "'");
                }
           );
    };
    //泵车类型
    var pumpTypeGrid = new MyGrid({
        renderTo: 'pumpTypeGrid'
            , autoWidth: true
        //, buttons: buttons0
        //, height: gGridHeight * 0.44
		    , storeURL: '/Dic.mvc/Find'
            , storeCondition: "ParentID='CastMode'"
		    , sortByField: 'ID'
		    , primaryKey: 'TreeCode'
		    , setGridPageSize: 30
		    , showPageBar: false
		    , initArray: [
                { label: ' 编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 泵车类型', name: 'DicName', index: 'DicName', sortable: false }
		    ]
		    , autoLoad: false
        //, show
            , functions: {
                handleReload: function (btn) {
                    ContractPumpGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractPumpGrid.refreshGrid('1=1');
                }
            }
    });
    ContractPumpGrid.addListeners("gridComplete", function () {
        ContractPumpGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ContractPump.mvc/Delete" });
    });

    //标号列表搜索
    window.SearchConStrength = function () {
        var conStrengthName = $.trim($("#ConStrengthName").val());
        var condition = "1=1";
        if (conStrengthName != '') {
            condition = "ConStrengthCode NOT LIKE 'C%'AND ConStrengthCode LIKE '%" + conStrengthName + "%'";
        }
        bindSelectData($('[name="conStrength"]'),
            '/ConStrength.mvc/getConStrengthList',
            { textField: 'ConStrengthCode',
                valueField: 'ConStrengthCode',
                condition: condition
            });

        //            $.ajax({
        //                type: "get",
        //                data: { ConStrengthCode: userName },
        //                url: "/ConStrength.mvc/getConStrengthList",
        //                success: function (data) {
        //                    alert(data);
        //                    $("#conStrength").empty();
        //                    var optionhtml = "";
        //                    if (data != null && data.length > 0) {
        //                        $.each(data, function (i, item) {
        //                            if (item.ConStrength < 0) {
        //                                optionhtml += ("<option value=\"" + item.UserId + "\">" + item.FullName + "</option>");
        //                            }
        //                        })
        //                        $('[name="conStrength"]').append(optionhtml);
        //                    }

        //                }
        //            })
    }

    //删除取消合同
    window.DelContract = function () {
        var _ContractID = $("#ContractGH_ID").val(); if (isEmpty(_ContractID)) {
            showError('提示', '你未保存过合同！');
            return false;
        }
        $.post(
                "/ContractGH.mvc/Delete",
                { id: _ContractID },
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    ContractGrid.reloadGrid();
                    $("#ContractForm").dialog("close"); //关闭对话框
                }
           );
    }
}

