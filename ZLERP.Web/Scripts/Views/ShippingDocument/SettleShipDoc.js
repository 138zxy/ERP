﻿function shipDocInit(opts) {
    function booleanIsBackSearchValues() {
        return { '': '', 1: '已回', 0: '途中' };
    }
    function booleanIsBackSelectValues() {
        return { 'true': '已回', 'false': '途中' };
    }
    function booleanIsAuditSearchValues() {
        return { '': '', 1: '已审', 0: '未审' };
    }
    function booleanIsOverTimeSearchValues() {
        return { '': '', 1: '超时', 0: '未超时' };
    }
    function booleanIsAuditSelectValues() {
        return { 'true': '已审', 'false': '未审' };
    }
    function booleanIsOverTimeSelectValues() {
        return { 'true': '超时', 'false': '未超时' };
    }
    function booleanIsOverTimeSearchValues() {
        return { '': '', 1: '超时', 0: '未超时' };
    }

    var shippDocGrid = new MyGrid({
        renderTo: 'shippDocGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ProduceDate'
            , storeCondition: " iseffective=1 and DataType=" + opts.dataType
            , sortOrder: 'DESC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 700
            , dialogHeight: 400
        //, singleSelect: true
            , columnReOrder: true
            , rowNumbers: true
            , rowList: [10, 20, 30, 50, 100, 200, 300, 400, 500, 1000]
            , advancedSearch: true
        //, multikey: 'shiftKey'
        //, multiselect: true
            , editSaveUrl: "/ShippingDocument.mvc/Update"
		    , initArray: [
                  { label: '运输单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, frozen: true }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 50, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '结算日期', name: 'SettleDate', index: 'SettleDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues() } }
                , { label: '是否二次审核', name: 'AuditStatus2', index: 'AuditStatus2', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues() } }
                , { label: '运费审核', name: 'y_IsAudit', index: 'y_IsAudit', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues()} }
                , { label: "车号", name: "CarID", index: "CarID", align: 'right', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getCarNoStr() }, edittype: 'select', editoptions: { value: getCarNoStr() } }
                , { label: '工程运距', name: 'Distance', index: 'Distance', align: 'right', width: 55, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2 } }
                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                //, { label: '补方方量', name: 'ShippingCube', index: 'ShippingCube', hidden: true, width: 60, align: 'right', formatter: BuCubeUnFmt, unformat: BuCubeUnFmt }
                , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 55, align: 'right', formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2 } }
                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2 } }
                , { label: '运费单价', name: 'TranPrice', index: 'TranPrice', width: 55, align: 'right', search: false }
                , { label: '运输费用', name: 'TranMoney', index: 'TranMoney', align: 'right', width: 80, search: false }
                , { label: '空载费', name: 'EmptyFee', index: 'EmptyFee', align: 'right', width: 80, search: false }
                , { label: '超时时长(h)', name: 'OverTimeCount', index: 'OverTimeCount', width: 75, align: 'right', search: false }
                , { label: '超时单价', name: 'OverTimePrice', index: 'OverTimePrice', width: 55, align: 'right', search: false }
                , { label: '超时费用', name: 'OverTimeFee', index: 'OverTimeFee', align: 'right', width: 80, search: false }
                , { label: '其他费用', name: 'OtherFee', index: 'OtherFee', align: 'right', width: 80, search: false }
                , { label: '运输超时费用', name: 'y_OuttimeMoney', index: 'y_OuttimeMoney', align: 'right', width: 80, search: false }
                , { label: '运输补量', name: 'y_ExtraMoney', index: 'y_ExtraMoney', align: 'right', width: 80, search: false }
                , { label: '运输其他费用', name: 'y_OtherMoney', index: 'y_OtherMoney', align: 'right', width: 80, search: false }
                , { label: '结算备注', name: 'RemarkJS', index: 'RemarkJS', width: 200, search: false }
                , { label: '运费备注', name: 'y_Remark', index: 'y_Remark', width: 200, search: false }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '客户编号', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'CustName' }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName' }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', search: false }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
                , { label: '砼标记', name: 'ProduceTask.BetonTag', index: 'ProduceTask.BetonTag', width: 60 }
                , { label: "浇筑方式", name: "CastMode", index: "CastMode", width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getDicCastModeStr() }, edittype: 'select', editoptions: { value: getDicCastModeStr()} }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
                , { label: "泵名称", name: "PumpName", index: "PumpName", width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getPumpStr() }, edittype: 'select', editoptions: { value: getPumpStr()} }
                , { label: '上车余料', name: 'RemainCube', index: 'RemainCube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '报废方量', name: 'ScrapCube', index: 'ScrapCube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '本车余料', name: 'TransferCube', index: 'TransferCube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', width: 55, align: 'right', search: false, hidden: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '过磅方量', name: 'Cube', index: 'Cube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '生产混凝土方量', name: 'BetonCount', index: 'BetonCount', width: 90, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '生产砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 80, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '生产总方量', name: 'TotalProduceCube', index: 'TotalProduceCube', width: 70, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '当班司机', name: 'Driver', index: 'Driver', search: true, width: 55, stype: "select", searchoptions: { value: getDriverStr() }, edittype: 'select', editoptions: { value: getDriverStr()} }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit', search: false }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', search: false, width: 55 }
                , { label: '委托单位', name: 'EntrustUnit', index: 'EntrustUnit', search: false, width: 55 }
                , { label: '前场工长', name: 'LinkMan', index: 'LinkMan', width: 55 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', width: 75 }
                , { label: '审核人', name: 'AuditMan', index: 'AuditMan', search: false, width: 50 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '是否超时', name: 'IsOverTime', index: 'IsOverTime', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '未超时', '1': '超时' }, stype: 'select', searchoptions: { value: booleanIsOverTimeSearchValues() }, edittype: 'select', editoptions: { value: booleanIsOverTimeSelectValues()} }
                , { label: '超时原因', name: 'OverTimeReason', index: 'OverTimeReason', width: 200, search: false, editable: true }
                , { label: '超时补助', name: 'OverTimeSubsidy', index: 'OverTimeSubsidy', width: 80, search: false, editable: true }
                , { label: '小票票号', name: 'TicketNO', index: 'TicketNO', width: 80, editable: true }
                , { label: '异常信息', name: 'Remark', index: 'Remark', width: 200, search: false, editable: true }
                , { label: '砼款已结算', name: 'IsAccount', index: 'IsAccount', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '运费已结算', name: 'IsTrans', index: 'IsTrans', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '泵送已结算', name: 'IsPonAccount', index: 'IsPonAccount', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '货款结算单', name: 'AccountBaleNo', index: 'AccountBaleNo', width: 100 }
                , { label: '运费结算单', name: 'AccountTranNo', index: 'AccountTranNo', width: 100 }
                , { label: '泵车结算单', name: 'AccountPonBaleNo', index: 'AccountPonBaleNo', width: 100 }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    shippDocGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    shippDocGrid.refreshGrid('1=1');
                },
                handleUpdate: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能修改');
                        return;
                    }
                    shippDocGrid.handleEdit({
                        loadFrom: 'CheckFee',
                        width: 480,
                        height: 210,
                        btn: btn,
                        beforeSubmit: function () {
                            if (shippDocGrid.getFormField('SignInCube').val() * 1 <= 0) {
                                showError('提示', '签收方量必须大于0！');
                                return;
                            }
                            return true;
                        },
                        afterFormLoaded: function () {
                            console.log(data.SettleDate);
                            $("#SettleDate").val(data.SettleDate);
                            shippDocGrid.setFormFieldReadOnly("ID", true);
                            var carField = shippDocGrid.getFormField('CarID');
                            carField.unbind("change");
                            carField.bind('change', function (s) {
                                var carid = $(this).val();
                                if (carid == "" || carid == null) {
                                    return;
                                }
                                var driverList = shippDocGrid.getFormField('Driver');
                                driverList.empty();
                                ajaxRequest(opts.getCarInfoUrl, { id: carid }, false, function (response) {
                                    var users = response.Users;
                                    if (!isEmpty(users)) {
                                        //设定司机
                                        for (var i = 0; i < users.length; i++) {
                                            var user = users[i];
                                            driverList.append("<option value=\"" + user.UserID + "\">" + user.TrueName + "</option>");
                                        }
                                        driverList.val(data['Driver']);
                                    }
                                });
                            });
                            carField.trigger('change');
                            //判断回厂时间是否为空，为空显示当前时间
                            var editArriveTime = $("#editArriveTime").val();
                            if ($.trim(editArriveTime).length == 0) {
                                var mydate = new Date();
                                var t = mydate.Format("yyyy-MM-dd hh:mm:ss");
                                $("#editArriveTime").val(t);
                            }
                        }
                    });
                },
                handleUpdateY: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.y_IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能修改');
                        return;
                    }
                    shippDocGrid.handleEdit({
                        loadFrom: 'YunfeiForm',
                        width: 480,
                        height: 210,
                        btn: btn,
                        afterFormLoaded: function () {
                            shippDocGrid.setFormFieldReadOnly("ID", true);
                            shippDocGrid.getFormField("OverTimeCount").bind('blur', function () {
                                OverTimeMoney();
                            });
                            shippDocGrid.getFormField("OverTimePrice").bind('blur', function () {
                                OverTimeMoney();
                            });
                        }
                    });
                },
                //审核票据
                handleAudit: function () {
                    var keys = shippDocGrid.getSelectedKeys();
                    if (keys.length > 1) {
                        var records = shippDocGrid.getSelectedRecords();
                        for (var i = 0; i < records.length; i++) {
                            var auditValue = records[i].IsAudit;
                            if (auditValue == 1 || auditValue == 'true') {
                                showMessage('提示', '请选择未审核的运输单！');
                                return;
                            }
                        }
                        var requestURL = opts.handleBatchAuditUrl;
                        var postParams = { id: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            shippDocGrid.reloadGrid();
                        });
                    }
                    else {
                        var selectedRecord = shippDocGrid.getSelectedRecord();
                        console.log(selectedRecord);
                        var confirmMessage = "您确定将此运输单设置为&nbsp;<font color=red><b>审核通过</b></font>&nbsp;状态吗？";

                        if (selectedRecord.IsAudit == "true" || selectedRecord.IsAudit == 1) {
                            confirmMessage = "您确定需要将此运输单设置为&nbsp;<font color=green><b>审核未通过</b></font>&nbsp;吗？";
                        }
                        var status = false;
                        if (selectedRecord.IsAudit == 0) {
                            status = false;
                        }
                        else if (selectedRecord.IsAudit == 1) {
                            status = true;
                        }
                        //确认操作
                        showConfirm("确认信息", confirmMessage, function () {
                            ajaxRequest(
                            opts.AuditUrl,
                            {
                                id: selectedRecord.ID,
                                status: status
                            },
                            true,
                            function (response) {
                                if (response.Result) {
                                    //根据ID获取行号
                                    //var rid = $("#shippDocGrid").jqGrid("getGridParam", "selrow");
                                    //shippDocGrid.jqGrid('setCell', rid, 'IsAudit', 1);
                                    for (var i = 0; i < keys.length; ++i) {
                                        if (selectedRecord.IsAudit == "true" || selectedRecord.IsAudit == 1) {
                                            shippDocGrid.getJqGrid().setCell(keys[i], 'IsAudit', 0);
                                        }
                                        else {
                                            shippDocGrid.getJqGrid().setCell(keys[i], 'IsAudit', 1);
                                        }
                                    }
                                }
                            }
                        );
                            $(this).dialog("close");
                        });
                    }
                }
                , handleAuditY: function () {
                    var keys = shippDocGrid.getSelectedKeys();
                    if (keys.length > 1) {
                        showMessage('提示', '请单选！');
                    }
                    else {
                        var selectedRecord = shippDocGrid.getSelectedRecord();
                        var confirmMessage = "您确定将此运输单设置为&nbsp;<font color=red><b>审核通过</b></font>&nbsp;状态吗？";
                        if (selectedRecord.y_IsAudit == "true" || selectedRecord.y_IsAudit == 1) {
                            confirmMessage = "您确定需要将此运输单设置为&nbsp;<font color=green><b>审核未通过</b></font>&nbsp;吗？";
                        }
                        //确认操作
                        showConfirm("确认信息", confirmMessage, function () {
                            ajaxRequest(
                            '/ShippingDocument.mvc/AuditY',
                            {
                                id: selectedRecord.ID
                            },
                            true,
                            function (response) {
                                if (response.Result) {
                                    for (var i = 0; i < keys.length; ++i) {
                                        if (selectedRecord.y_IsAudit == "true" || selectedRecord.y_IsAudit == 1) {
                                            shippDocGrid.getJqGrid().setCell(keys[i], 'y_IsAudit', 0);
                                        }
                                        else {
                                            shippDocGrid.getJqGrid().setCell(keys[i], 'y_IsAudit', 1);
                                        }
                                    }
                                }
                            }
                        );
                            $(this).dialog("close");
                        });
                    }
                }
                ,
                //取消审核
                handleUnAudit: function () {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    var confirmMessage = "您确定需要将此运输单设置为&nbsp;<font color=green><b>审核未通过</b></font>&nbsp;吗？";

                    if (selectedRecord.IsAudit == "false" || selectedRecord.IsAudit == 0) {
                        showMessage('提示', '请选择已审核的运输单！');
                        return;
                    }
                    var keys = shippDocGrid.getSelectedKeys();
                    //确认操作
                    showConfirm("确认信息", confirmMessage, function () {
                        ajaxRequest(
                        opts.AuditUrl,
                        {
                            id: selectedRecord.ID,
                            status: true
                        },
                        true,
                        function (response) {
                            if (response.Result) {
                                //根据ID获取行号
                                for (var i = 0; i < keys.length; ++i) {
                                    if (selectedRecord.IsAudit == "true" || selectedRecord.IsAudit == 1) {
                                        shippDocGrid.getJqGrid().setCell(keys[i], 'IsAudit', 0);
                                    }
                                    else {
                                        shippDocGrid.getJqGrid().setCell(keys[i], 'IsAudit', 1);
                                    }
                                }
                            }
                        }
                    );
                        $(this).dialog("close");
                    });
                }
                ,
                //批量审核          
                handleMultiAudit: function (btn) {

                    var keys = shippDocGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    }

                    var records = shippDocGrid.getSelectedRecords();
                    var signInCube = 0;
                    for (var i = 0; i < records.length; ++i) {
                        signInCube += isEmpty(records[i].SignInCube) ? 0 : parseFloat(records[i].SignInCube);
                    }
                    var xuCube = 0;
                    for (var i = 0; i < records.length; ++i) {
                        xuCube += isEmpty(records[i].XuCube) ? 0 : parseFloat(records[i].XuCube);
                    }

                    showConfirm("确认信息", "签收方量为：" + signInCube + "。其他方量2为：" + xuCube + "。<br />审核后将不允许删除、修改。是否继续审核？", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            opts.MultiAuditUrl,
                            {
                                ids: keys
                            },
                            true,
                            function (response) {
                                $(btn.currentTarget).button({ disabled: false });

                                if (response.Result) {
                                    for (var i = 0; i < keys.length; ++i) {
                                        shippDocGrid.getJqGrid().setCell(keys[i], 'IsAudit', 1);
                                    }
                                }
                            }
                        );

                        $(this).dialog("close");
                    });

                }
                //批量二次审核          
                , handleMultiAudit2: function (btn) {
                    var keys = shippDocGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    }
                    var records = shippDocGrid.getSelectedRecords();
                    for (var i = 0; i < records.length; i++) {
                        var record = records[i];
                        if (record && (record.AuditStatus2 == 1 || record.AuditStatus2 == 'true')) {
                            showMessage(record.ID + '该运输单单已二审，不允许再次二审');
                            return;
                        } if (record && (record.IsAudit == 0 || record.IsAudit == 'false')) {
                            showMessage(record.ID + '请选择已审核的运输单');
                            return;
                        }
                    }
                    showConfirm("确认信息", "审核后将不允许删除、修改。是否继续二次审核？", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            opts.MultiAuditUrl2,
                            {
                                ids: keys
                            },
                            true,
                            function (response) {
                                $(btn.currentTarget).button({ disabled: false });

                                if (response.Result) {
                                    for (var i = 0; i < keys.length; ++i) {
                                        shippDocGrid.getJqGrid().setCell(keys[i], 'AuditStatus2', 1);
                                    }
                                }
                            }
                        );

                        $(this).dialog("close");
                    });

                }
                , handleMultiAuditY: function (btn) {
                    var keys = shippDocGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    }

                    var records = shippDocGrid.getSelectedRecords();
                    var signInCube = 0;
                    for (var i = 0; i < records.length; ++i) {
                        signInCube += isEmpty(records[i].SignInCube) ? 0 : parseFloat(records[i].SignInCube);
                    }
                    var xuCube = 0;
                    for (var i = 0; i < records.length; ++i) {
                        xuCube += isEmpty(records[i].XuCube) ? 0 : parseFloat(records[i].XuCube);
                    }

                    showConfirm("确认信息", "签收方量为：" + signInCube + "。其他方量2为：" + xuCube + "。<br />审核后将不允许删除、修改。是否继续审核？", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            '/ShippingDocument.mvc/MultiAuditY',
                            {
                                ids: keys
                            },
                            true,
                            function (response) {
                                $(btn.currentTarget).button({ disabled: false });

                                if (response.Result) {
                                    for (var i = 0; i < keys.length; ++i) {
                                        shippDocGrid.getJqGrid().setCell(keys[i], 'y_IsAudit', 1);
                                    }
                                }
                            }
                        );

                        $(this).dialog("close");
                    });

                }
                , FindyunshuRecord: function (btn) {
                    condition = "ShipDocType='0'";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindqitaRecord: function (btn) {
                    condition = "ShipDocType!='0'";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindTodayRecord: function (btn) {

                    var d = new Date();
                    var today = d.format("Y-m-d");
                    condition = "ProduceDate between '" + today + " 00:00:00' and '" + today + " 23:59:59'";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindTomRecord: function (btn) {
                    var uom = GetDateBySpan(1);
                    condition = "ProduceDate between '" + uom + " 00:00:00' and '" + uom + " 23:59:59'";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindAllRecord: function (btn) {
                    condition = "1=1";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindCustRecord: function (btn) {
                    shippDocGrid.showWindow({
                        title: '选择时间范围',
                        width: 280,
                        height: 180,
                        resizable: false,
                        loadFrom: 'SelectTimeForm',
                        afterLoaded: function () {

                        },
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "确定": function () {
                                var BeginTime = $("#sBeginTime").val();
                                var EndTime = $("#sEndTime").val();
                                condition = "ProduceDate between '" + BeginTime + "' and '" + EndTime + "'";
                                shippDocGrid.refreshGrid(condition);
                                var checked = $("#sIsAutoClose")[0].checked;
                                if (checked) {
                                    $(this).dialog('close');
                                }
                            }
                        }
                    });
                }
                //显示-其他方量 列
                , hanldeShowHideCube: function (btn) {

                    shippDocGrid.getJqGrid().jqGrid('showCol', 'OtherCube'); //显示列
                    //shippDocGrid.getJqGrid().jqGrid('hideCol', 'XuCube'); ; //隐藏列
                },
                //自动生成运费结算单
                TranBalance: function (btn) {
                    var ids = shippDocGrid.getSelectedKeys();
                    if (ids.length > 1) {
                        showMessage('提示', '不能一次添加多条运输单，请到财务管理添加其他运输单！');
                        return;
                    }
                    if (ids.length > 0) {
                        showConfirm("确认信息", "我们会根据运输车队与工程产生不同的结算单，生成后,请到运费结算中完善信息？",
				function () {
				    ajaxRequest(opts.TranBalanceUrl, { ids: ids },
				        false,
				        function (response) {
				            if (response.Result) {
				                showMessage('提示', response.Message + "，请到运费结算模块查询，并完善信息!");
				                shippDocGrid.reloadGrid();
				                return;
				            } else {
				                showMessage('提示', response.Message);
				                return;
				            }

				        })
				});
                    }
                }
                ,
                //自动生成砼款结算单
                Balance: function (btn) {
                    var ids = shippDocGrid.getSelectedKeys();
                    if (ids.length > 1) {
                        showMessage('提示', '不能一次添加多条运输单，请到财务管理添加其他运输单！');
                        return;
                    }
                    if (ids.length > 0) {
                        showConfirm("确认信息", "我们会根据合同与工程产生不同的结算单，生成后,请到砼款结算中完善信息？",
				function () {
				    ajaxRequest(opts.BalanceUrl, { ids: ids },
				        false,
				        function (response) {
				            if (response.Result) {
				                showMessage('提示', response.Message + "，请到砼款结算模块查询，并完善信息!");
				                shippDocGrid.reloadGrid();
				                return;
				            } else {
				                showMessage('提示', response.Message);
				                return;
				            }

				        })
				});
                    }
                },
                //自动生成砼款结算单
                PotonBalance: function (btn) {
                    var ids = shippDocGrid.getSelectedKeys();
                    if (ids.length > 1) {
                        showMessage('提示', '不能一次添加多条运输单，请到财务管理添加其他运输单！');
                        return;
                    }
                    if (ids.length > 0) {
                        showConfirm("确认信息", "我们会根据合同与工程产生不同的泵送结算单，生成后,请到泵送款结算中完善信息？",
				function () {
				    ajaxRequest(opts.PotonBalanceUrl, { ids: ids },
				        false,
				        function (response) {
				            if (response.Result) {
				                showMessage('提示', response.Message + "，请到泵送款结算模块查询，并完善信息!");
				                shippDocGrid.reloadGrid();
				                return;
				            } else {
				                showMessage('提示', response.Message);
				                return;
				            }

				        })
				});
                    }
                },
                PeCarBalance: function (btn) {
                    var ids = shippDocGrid.getSelectedKeys();
                    if (ids.length > 1) {
                        showMessage('提示', '不能一次添加多条运输单，请到财务管理添加其他运输单！');
                        return;
                    }
                    if (ids.length > 0) {
                        showConfirm("确认信息", "我们会根据泵车单位与工程产生不同的泵车算单，生成后,请到泵车款结算中完善信息？",
				                function () {
				                    ajaxRequest(opts.PeCarBalanceUrl, { ids: ids },
				                        false,
				                        function (response) {
				                            if (response.Result) {
				                                showMessage('提示', "生成成功，请到泵车款结算模块查询，并完善信息!");
				                                shippDocGrid.reloadGrid();
				                                return;
				                            } else {
				                                showMessage('提示', response.Message);
				                                return;
				                            }

				                        })
				                });
                    }
                },
                QualityInspect: function (btn) {

                    if (shippDocGrid.isSelectedOnlyOne("请选择一条记录进行操作") == false) {
                        return;
                    }

                    var id = shippDocGrid.getSelectedKey();

                    ajaxRequest(btn.data.Url, { shipDocId: id },
				        false,
				        function (response) {
				            if (response.Result) {
				                showMessage('提示', response.Message);
				                shippDocGrid.reloadGrid();
				                return;
				            } else {
				                showMessage('提示', response.Message);
				                return;
				            }

				        })


                }
            }
    });
    if (opts.yunshu == 0) {
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'y_IsAudit');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'y_OuttimeMoney');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'y_ExtraMoney');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'y_OtherMoney');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'y_Remark');
    }
    if (opts.yunshu == 1) {
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'IsAudit');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'EmptyFee');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'OverTimeFee');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'OtherFee');
        shippDocGrid.getJqGrid().jqGrid('hideCol', 'RemarkJS');
    }
    //获取浇筑方式列表 
    //lzl add 2016-11-1
    function getDicCastModeStr() {
        //动态生成select内容
        var requestURL = "/Dic.mvc/FindDicsList";
        var postParams = { nodeid: "CastMode" };
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
                            str += jsonobj[i].DicName + ":" + jsonobj[i].DicName + ";";
                        } else {
                            str += jsonobj[i].DicName + ":" + jsonobj[i].DicName;
                        }
                    }
                }
                //return str;
            }
        });
        return str;
    }
    //运输车号列表
    function getCarNoStr() {
        var str = getStr("/ShippingDocument.mvc/getCarNoList");
        return str;
    }
    //获取泵名称列表
    function getPumpStr() {
        var str = getStr("/ShippingDocument.mvc/getPumpList");
        return str;
    }
    //获取司机列表
    function getDriverStr() {
        var str = getStr("/ShippingDocument.mvc/getDriverList");
        return str;
    }
    //动态生成下拉字符串
    function getStr(requestURL) {
        //动态生成select内容
        var requestURL = requestURL;
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
                            str += jsonobj[i].ID + ":" + jsonobj[i].ID + ";";
                        } else {
                            str += jsonobj[i].ID + ":" + jsonobj[i].ID;
                        }
                    }
                }
                //return str;
            }
        });
        return str;
    }

    function OverTimeMoney() {
        var Count = shippDocGrid.getFormField("OverTimeCount").val()*1;
        var Price = shippDocGrid.getFormField("OverTimePrice").val()*1;
        var Money = Count * Price;
        shippDocGrid.getFormField("y_OuttimeMoney").val(Money);

    }
    shippDocGrid.getJqGrid().jqGrid('setGridWidth', $("#shippDocGrid").width());
    shippDocGrid.addListeners("gridComplete", function () {
        var ids = shippDocGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var record = shippDocGrid.getRecordByKeyValue(ids[i]);
            if (record.XuCube != null && record.XuCube > 0 && record.IsAudit == 'true') {
                var $id = $(document.getElementById(ids[i]));
                $id.removeAttr("style");
                $id.removeClass("ui-widget-content");
                document.getElementById(ids[i]).style.backgroundColor = "#00BBFF"; //设置行背景颜色
            }
        }
        $(".cbox").shiftSelect();
    });

    shippDocGrid.addListeners('rowdblclick', function (id, record, selBool) {
        shippDocGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细',
            width: 900,
            height: 600,

            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
            }
        });
    });



    function CalcCube() {
        var TotalWeight = shippDocGrid.getFormField("TotalWeight").val();
        var CarWeight = shippDocGrid.getFormField("CarWeight").val();
        var Weight = TotalWeight - CarWeight;

        var Exchange = shippDocGrid.getFormField("Exchange").val();
        var Cube = 0;
        if (Exchange > 0) {
            Cube = parseFloat(Weight / Exchange).toFixed(2);
        }

        shippDocGrid.getFormField("Weight").val(Weight);
        shippDocGrid.getFormField("Cube").val(Cube);

    }

    //按shift键多选
    jQuery.fn.shiftSelect = function () {
        var checkboxes = this;
        var lastSelected;
        var executing = false;
        jQuery(this).click(function (event) {

            if (executing)
                return;

            if (!lastSelected) {
                lastSelected = this;
                return;
            }

            if (event.shiftKey) {
                var selIndex = checkboxes.index(this);
                var lastIndex = checkboxes.index(lastSelected);
                /*
                * if you find the "select/unselect" behavior unseemly,
                * remove this assignment and replace 'checkValue'
                * with 'true' below.
                */
                var checkValue = lastSelected.checked;
                if (selIndex == lastIndex) {
                    return true;
                }
                executing = true;
                var end = Math.max(selIndex, lastIndex);
                var start = Math.min(selIndex, lastIndex);
                for (i = start; i <= end; i++) {
                    if (checkboxes[i].checked != checkValue)
                        $(checkboxes[i]).click();
                }
                executing = false;
            }
            lastSelected = this;
        });
    }
    /**************************************时间格式化处理************************************/

    //创建时间格式化显示
    function crtTimeFtt(value) {
        if (value != null) {
            return moment(value).format("YYYY-MM-DD HH:mm:ss"); //直接调用公共JS里面的时间类处理的办法     
        }
        else {
            return null;
        }
    }
}
 