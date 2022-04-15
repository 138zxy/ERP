function shippingDocInit(opts) {
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


    //报表参数
    var ShipDocProperties2 = {
        ID: "发货单号",
        TaskID: "任务号",
        ContractID: "合同编号",
        ContractName: "合同名称",
        CustomerID: "客户编号",
        CustName: "客户名称",
        CustMixpropID: "客户配比",
        ConsMixpropID: "施工配比",
        ProjectName: "工程名称",
        Remark: "备注",
        ProjectAddr: "工程地址",
        ConStrength: "砼强度",
        //此处添加相应的任务单中的字段，注意字段匹配
        BetonTag: "砼标记",
        OtherDemand: "其他要求",
        CastMode: "浇筑方式",
        ConsPos: "施工部位",
        CarpGrade: "级配",
        PumpName: "泵名称",
        ImpGrade: "抗渗等级",
        ImyGrade: "抗压等级",
        ImdGrade: "抗冻等级",
        CarpRadii: "骨料粒径",
        CementBreed: "水泥品种",
        RealSlump: "坍落度",
        BetonCount: "混凝土方量",
        SlurryCount: "砂浆方量",
        ParCube: "本车方量",
        XuCube: "其他方量2",
        SignInCube: "签收方量",
        RemainCube: "剩余方量",
        ProvidedCube: "累计方量",
        PlanCube: "计划方量",
        TransferCube: "运输方量",
        CarID: "车牌号",
        ProvidedTimes: "累计车数",
        DeliveryTime: "发车时间",
        Driver: "驾驶员",
        Surveyor: "质检员",
        Signer: "调度员",
        ForkLift: "上料员",
        Operator: "操作员",
        PlanClass: "计划班组",
        ProduceDate: "生产日期",
        ProductLineName: "搅拌站名称",
        SupplyUnit: "供货单位",
        ConstructUnit: "施工单位",
        FormulaName: "理论配比名称",
        Distance: "运距",
        LinkMan: "工地联系人",
        Tel: "工地电话"
    , PumpName1: "清洗泵名称1"
    , EnterpriseName: "企业名称"
    , Temp1: "备用1"
    , Temp2: "备用2"
    , Temp3: "备用3"
    , Temp4: "备用4"
    , Temp5: "备用5"
    , Temp6: "备用6"
    , Temp7: "备用7"
    , Temp8: "备用8"
    , Temp9: "备用9"
    , Temp10: "备用111"
    };

    var shippDocGrid = new MyGrid({
        renderTo: 'shippDocGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: " DataType=1  "
		    , sortByField: 'ProduceDate'
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
                  { label: '历史视频', name: 'HistoryVideo', index: 'HistoryVideo', width: 90, sortable: false, align: "center", search: false, hidden: false }
                , { label: '运输单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, frozen: true }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 50, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
        //, { label: '是否带回', name: 'IsBack', index: 'IsBack', width: 50, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '途中', '1': '已回' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否带回', name: 'IsBack', index: 'IsBack', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '途中', '1': '已回' }, stype: 'select', searchoptions: { value: booleanIsBackSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanIsBackSelectValues()} }
                , { label: '是否有效', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '无效', '1': '有效' }, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues()} }
                , { label: '是否超时', name: 'IsOverTime', index: 'IsOverTime', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '超市', '0': '未超时' }, stype: 'select', searchoptions: { value: booleanIsOverTimeSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsOverTimeSearchValues()} }
                
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '客户编号', name: 'CustomerID', index: 'CustomerID', hidden: true }

                , { label: '客户名称', name: 'CustName', index: 'CustName' }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName' }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID', hidden: true }
                , { label: '配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }

                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', search: false }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
                , { label: '砼标记', name: 'ProduceTask.BetonTag', index: 'ProduceTask.BetonTag', width: 60 }
                , { label: "浇筑方式", name: "CastMode", index: "CastMode", width: 60, editable: true, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getDicCastModeStr() }, edittype: 'select', editoptions: { value: getDicCastModeStr()} }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
                , { label: '级配', name: 'ProduceTask.CarpGrade', index: 'ProduceTask.CarpGrade', width: 80 }
                , { label: '泵工', name: 'PumpMan', index: 'PumpMan', width: 80 }
                , { label: "泵名称", name: "PumpName", index: "PumpName", width: 60, editable: true, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getPumpStr() }, edittype: 'select', editoptions: { value: getPumpStr()} }

                , { label: "车号", name: "CarID", index: "CarID", width: 60, editable: true, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getCarNoStr() }, edittype: 'select', editoptions: { value: getCarNoStr()} }
                , { label: '车牌', name: 'Car.CarNo', index: 'Car.CarNo', width: 60 }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 55, align: 'right', search: false }
                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 55, align: 'right', search: false }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }
                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 55, align: 'right', search: false }
                , { label: '抗渗等级', name: 'ImpGrade', index: 'ImpGrade', hidden: true }
                , { label: '抗压等级', name: 'ImyGrade', index: 'ImyGrade', hidden: true }
                , { label: '抗冻等级', name: 'ImdGrade', index: 'ImdGrade', hidden: true }
                , { label: '骨料粒径', name: 'CarpRadii', index: 'CarpRadii', hidden: true }
                , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed', hidden: true }
                , { label: '调度湿拌方量', name: 'SendBetonCount', index: 'SendBetonCount', width: 90, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2 }, hidden: true }
                , { label: '调度湿拌方量', name: 'SendSlurryCount', index: 'SendSlurryCount', width: 80, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2 } }
                , { label: '调度总方量', name: 'SendCube', index: 'SendCube', width: 70, align: 'right', editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 55, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '上车余料', name: 'RemainCube', index: 'RemainCube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 55, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 55, align: 'right', editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '报废方量', name: 'ScrapCube', index: 'ScrapCube', width: 55, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '本车余料', name: 'TransferCube', index: 'TransferCube', width: 55, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '其他方量2', name: 'XuCube', index: 'XuCube', width: 60, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', width: 55, align: 'right', search: false, hidden: true, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '过磅方量', name: 'Cube', index: 'Cube', width: 55, align: 'right', search: false, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '生产湿拌方量', name: 'BetonCount', index: 'BetonCount', width: 90, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '生产砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 80, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2 }, hidden: true }
                , { label: '生产总方量', name: 'TotalProduceCube', index: 'TotalProduceCube', width: 70, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2} }
                , { label: '毛重(T)', name: 'TotalWeight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '皮重(T)', name: 'CarWeight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '净重(T)', name: 'Weight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '换算率(T/m³)', name: 'Exchange', width: 80, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
        // , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 55, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
        // , { label: '运费', name: 'SumPrice', index: 'SumPrice' }
                , { label: '出厂时间', name: 'DeliveryTime', index: 'DeliveryTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '当班司机', name: 'Driver', index: 'Driver', search: true, editable: true, width: 55, stype: "select", searchoptions: { value: getDriverStr() }, edittype: 'select', editoptions: { value: getDriverStr()} }
                , { label: '是否质检', name: 'IsQualityInspected', index: 'IsQualityInspected', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', search: false, width: 55 }
                , { label: '调度员', name: 'Signer', index: 'Signer', search: false, width: 50 }
                , { label: '上料员', name: 'ForkLift', index: 'ForkLift', search: false, width: 50 }
                , { label: '操作员', name: 'Operator', index: 'Operator', search: false, width: 50 }

                , { label: '机组编号', name: 'ProductLineID', index: 'ProductLineID', width: 55 }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit', search: false }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', search: false, width: 55 }
                , { label: '委托单位', name: 'EntrustUnit', index: 'EntrustUnit', search: false, width: 55 }
                , { label: '现场验收人', name: 'Accepter', index: 'Accepter', search: false, width: 65 }
                , { label: '工程运距', name: 'Distance', index: 'Distance', width: 55, editable: true ,formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 2}}
                , { label: '前场工长', name: 'LinkMan', index: 'LinkMan', width: 55 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', width: 75 }
                , { label: '工程编号', name: 'ProjectID', index: 'ProjectID', search: false, width: 65 }
                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', search: false, width: 55 }

                , { label: '审核人', name: 'AuditMan', index: 'AuditMan', search: false, width: 50 }
                , { label: '公里数', name: 'CarKm', index: 'CarKm', width: 55, editable: true }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '超时原因', name: 'OverTimeReason', index: 'OverTimeReason', width: 200, search: false, editable: true }
                , { label: '超时补助', name: 'OverTimeSubsidy', index: 'OverTimeSubsidy', width: 80, search: false, editable: true }
                , { label: '小票票号', name: 'TicketNO', index: 'TicketNO', width: 80, editable: true }
                , { label: '砼款已结算', name: 'IsAccount', index: 'IsAccount', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '运费已结算', name: 'IsTrans', index: 'IsTrans', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '泵送已结算', name: 'IsPonAccount', index: 'IsPonAccount', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '货款结算单', name: 'AccountBaleNo', index: 'AccountBaleNo', width: 100 }
                , { label: '运费结算单', name: 'AccountTranNo', index: 'AccountTranNo', width: 100 }
                , { label: '泵车结算单', name: 'AccountPonBaleNo', index: 'AccountPonBaleNo', width: 100 }

                , { label: '其他要求', name: 'OtherDemand', index: 'OtherDemand', width: 200, search: false }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200, search: false, editable: true }
                , { label: '异常信息', name: 'ExceptionInfo', index: 'ExceptionInfo', width: 200, search: false }

                , { label: '车辆GPS设备编号', name: 'Car.TerminalID', index: 'Car.TerminalID', hidden: true }
                , { label: '视频设备编号', name: 'Car.VideoDeviceGuid', index: 'Car.VideoDeviceGuid', hidden: true }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {

                    shippDocGrid.reloadGrid();
                    
                },
                handleRefresh: function (btn) {
                    shippDocGrid.refreshGrid('1=1 AND DataType=1');
                },
                handleAdd: function (btn) {
                    shippDocGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            shippDocGrid.getFormField("BetonCount").bind('blur', function () {
                                var BetonCount = shippDocGrid.getFormField("BetonCount").val();
                                var SlurryCount = shippDocGrid.getFormField("SlurryCount").val();
                                shippDocGrid.getFormField("SendCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("ParCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("SignInCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                            });
                            shippDocGrid.getFormField("SlurryCount").bind('blur', function () {
                                var BetonCount = shippDocGrid.getFormField("BetonCount").val();
                                var SlurryCount = shippDocGrid.getFormField("SlurryCount").val();
                                shippDocGrid.getFormField("SendCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("ParCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("SignInCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                            });

                        }
                    });
                },
                //打印运输单
                handlePrintDoc: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录打印");
                        return false;
                    }
                    var docId = shippDocGrid.getSelectedKey();
                    if (docId <= 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var url = "/GridReport/PrintDirect.aspx?report=ShippingDocument&ID=" + docId;
                    window.open(url, "_blank");
                    //shippDocGrid.showWindow({
                    //    title: "打印模板选择",
                    //    width: 300,
                    //    height: 150,
                    //    loadFrom: 'TemplateForm',
                    //    //afterLoaded: function () {
                    //    //    var shipdocTemplateField = $("input[name='DicID']").val();
                    //    //}
                    //    buttons: {
                    //        "确定": function () {
                    //            var templateID = $("#TemplateID").val();
                    //            if (isEmpty(templateID)) {
                    //                showMessage("提示", "请选择发货单模板");
                    //                return false;
                    //            }
                    //            setShipDocTemplate(templateID);
                    //            printShippingDoc('preview', shippDocGrid.getSelectedKey());
                    //            $(this).dialog("close");
                    //        }
                    //    }
                    //})
                },
                //回单处理
                handleTakeBack: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能回单');
                        return;
                    }
                    shippDocGrid.handleEdit({
                        loadFrom: 'TakeBack',
                        btn: btn,
                        beforeSubmit: function () {
                            if (shippDocGrid.getFormField('SignInCube').val() * 1 < 0) {
                                showError('提示', '签收方量必须大于等于0！');
                                return;
                            }
                            return true;
                        },
                        afterFormLoaded: function () {
                            //$("#editDeliverTime").text(
                            //$.datepicker.formatTime('hh:mm:ss', "2007-09-12 22:34:00", { ampm: false })
                            //);

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

                            //shippDocGrid.setFormFieldDisabled("ConstructUnit", true);
                            //shippDocGrid.setFormFieldDisabled("ProjectName", true);
                            //shippDocGrid.setFormFieldDisabled("CustName", true);
                            //shippDocGrid.setFormFieldDisabled("ContractName", true);

                            //                            window.setTimeout(function () {
                            //                                shippDocGrid.setFormFieldChecked("IsBack", true);
                            //                                $("input[name='ArriveTime']").val(dataTimeFormat(new Date()));
                            //                            }, 500);

                            //判断回厂时间是否为空，为空显示当前时间
                            var editArriveTime = $("#editArriveTime").val();
                            if ($.trim(editArriveTime).length == 0) {
                                var mydate = new Date();
                                var t = mydate.Format("yyyy-MM-dd hh:mm:ss");
                                $("#editArriveTime").val(t);
                            }
                        }
                    });
                }
                //作废
                , handleGarbage: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能作废');
                        return;
                    }

                    var isEffective = data.IsEffective;
                    if (isEffective == 0 || isEffective == 'false') {
                        showMessage('提示', '已作废的发货单不能作废');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "发货单作废原因",
                        width: 350,
                        height: 200,
                        loadFrom: 'GarbageForm',
                        btn: btn,
                        postUrl: opts.garbageUrl,
                        postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {
                            $("#remark").val("");
                        }

                    });
                }
                //取消作废
                , handleCancelGarbage: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能取消作废');
                        return;
                    }

                    var isEffective = data.IsEffective;
                    if (isEffective == 1 || isEffective == 'true') {
                        showMessage('提示', '未作废的发货单不能取消作废');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "发货单取消作废原因",
                        width: 350,
                        height: 200,
                        loadFrom: 'GarbageForm',
                        btn: btn,
                        postUrl: opts.garbageUrl,
                        postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {
                            $("#remark").val("");
                        }
                    });
                }
                //删除
                , handleDelete: function (btn) {
                    var records = shippDocGrid.getSelectedRecords();
                    for (var i = 0; i < records.length; i++) {
                        var auditValue = records[i].IsAudit;
                        if (auditValue == 1 || auditValue == 'true') {
                            showMessage('提示', '已审核的发货单不能删除');
                            return;
                        }
                    }

                    shippDocGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //报表设计
                , handleDesign: function (btn) {
                    var url = "/GridReport/DesignReport.aspx?report=ShippingDocument";
                    window.open(url, "_blank");
                    //使用选中的发货单作设计数据
                    //var docId = shippDocGrid.getSelectedKey();
                    //if (!isEmpty(docId)) {
                    //    printShippingDoc('design', docId);
                    //}
                    //else {//未选中任务发货单则使用测试数据设计
                    //    shippingDocDesign();
                    //}
                },
                handleCheck: function (btn) {
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

                    var isEffective = data.IsEffective;
                    if (isEffective == 0 || isEffective == 'false') {
                        showMessage('提示', '已作废的发货单不能修改');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "出厂检测",
                        width: 350,
                        height: 200,
                        loadFrom: 'OutCheckForm',
                        btn: btn
                    });
                }
                //审核票据
                , handleAudit: function () {
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
                //查看生产登记修改历史记录
                , handleViewHis: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    showHis(btn, selectedRecord.ID);
                }
                //查看运输单修改历史记录
                , handleShipDocHis: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    showShipDocHis(btn, selectedRecord.ID);
                }
                , FindyunshuRecord: function (btn) {
                    condition = "ShipDocType='0' AND DataType=1 ";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindqitaRecord: function (btn) {
                    condition = "ShipDocType!='0' AND DataType=1";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindTodayRecord: function (btn) {

                    var d = new Date();
                    var today = d.format("Y-m-d");
                    condition = "ProduceDate between '" + today + " 00:00:00' and '" + today + " 23:59:59' AND DataType=1 ";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindTomRecord: function (btn) {
                    var uom = GetDateBySpan(1);
                    condition = "ProduceDate between '" + uom + " 00:00:00' and '" + uom + " 23:59:59' AND DataType=1 ";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindAllRecord: function (btn) {
                    condition = "1=1 AND DataType=1 ";
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
                                condition = "ProduceDate between '" + BeginTime + "' and '" + EndTime + "' AND DataType=1 ";
                                shippDocGrid.refreshGrid(condition);
                                var checked = $("#sIsAutoClose")[0].checked;
                                if (checked) {
                                    $(this).dialog('close');
                                }
                            }
                        }
                    });
                }
                , handleUpdateMetage: function (btn) {
                    shippDocGrid.handleEdit({
                        title: "出厂检测",
                        width: 550,
                        height: 200,
                        postUrl: opts.MetageUpdateUrl,
                        loadFrom: 'MetageUpdate',
                        btn: btn,
                        afterFormLoaded: function () {
                            shippDocGrid.getFormField("TotalWeight").bind('blur', function () {
                                CalcCube();
                            });
                            shippDocGrid.getFormField("CarWeight").bind('blur', function () {
                                CalcCube();
                            });

                            shippDocGrid.getFormField("Exchange").bind('blur', function () {
                                CalcCube();
                            });
                        }
                    });

                }
                //显示-其他方量 列
                , hanldeShowHideCube: function (btn) {

                    shippDocGrid.getJqGrid().jqGrid('showCol', 'OtherCube'); //显示列
                    //shippDocGrid.getJqGrid().jqGrid('hideCol', 'XuCube'); ; //隐藏列
                }
                //超时设置
                , hanldeOverTimeReason: function (btn) {

                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    shippDocGrid.handleEdit({
                        title: "超时设置",
                        width: 350,
                        height: 260,
                        loadFrom: 'OverTimeReasonForm',
                        btn: btn,
                        //postUrl: opts.OverTimeReasonUrl,
                        //postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {

                        }

                    });
                }
                ,
                //抽样
                handleSampling: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行抽样");
                        return;
                    }
                    var record = shippDocGrid.getSelectedRecord();

                    if (record && record.IsEffective == 'false') {
                        showError('该入库单已无效，不允许再抽样');
                        return;
                    }
                    var conditionstr = " ParentID='LabTemplateType' AND FIELD2=2 ";
                    $.post(
                            "/Dic.mvc/ListData",
                            {
                                textField: 'DicName',
                                valueField: 'TreeCode',
                                condition: conditionstr
                            },
                            function (res) {
                                if ($.isEmptyObject(res)) {
                                    showMessage("提示", res.Message);
                                    return false;
                                } else {
                                    var list = res;
                                    var executerDiv = document.getElementById("SamplingCheck");
                                    executerDiv.innerHTML = "";
                                    var ul = document.createElement("ul");
                                    for (var i = 0; i < list.length; i++) {
                                        var checkBox = document.createElement("input");
                                        checkBox.setAttribute("class", "requiredval");
                                        checkBox.setAttribute("type", "checkbox");
                                        checkBox.setAttribute("id", list[i].Value);
                                        checkBox.setAttribute("name", "ReportType");
                                        var li = document.createElement("li");
                                        li.appendChild(checkBox);
                                        li.appendChild(document.createTextNode(list[i].Text));
                                        ul.appendChild(li);
                                    }
                                    executerDiv.appendChild(ul);
                                    shippDocGrid.showWindow({
                                        btn: btn,
                                        title: '抽样选择',
                                        width: 450,
                                        height: 150,
                                        autoOpen: false,
                                        loadFrom: 'SamplingCheck',
                                        buttons: {
                                            "关闭": function () {
                                                $(this).dialog('close');
                                            }, "保存":
                                                    function () {
                                                        var checklist = document.getElementsByName("ReportType");
                                                        var types = "";
                                                        for (var i = 0; i < checklist.length; i++) {
                                                            if (checklist[i].checked == true) {
                                                                types = types + "," + checklist[i].id;
                                                            }
                                                        } $(this).dialog('close');
                                                        showConfirm("确认信息", "是否对该运输单进行抽样吗？", function (btn) {
                                                            $(btn.currentTarget).button({ disabled: true });
                                                            ajaxRequest(
                                                                    "/ShippingDocument.mvc/Sampling",
                                                                    {
                                                                        id: record.ID,
                                                                        type: types
                                                                    },
                                                                    true,
                                                                    function () {
                                                                        $(btn.currentTarget).button({ disabled: false });
                                                                        shippDocGrid.refreshGrid('1=1 AND DataType=1 ');
                                                                    }
                                                                );
                                                            $(this).dialog("close");
                                                        });
                                                    }
                                        }
                                    })

                                }
                            }
                        );

                },
                //扫描回单处理
                handleAutoBack: function (btn) {
                    //                    if (!shippDocGrid.isSelectedOnlyOne()) {
                    //                        showMessage("提示", "请选择一条记录进行操作");
                    //                        return false;
                    //                    }
                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能回单');
                        return;
                    }
                    shippDocGrid.handleEdit({
                        loadFrom: 'AutoBack',
                        btn: btn,
                        width: 680,
                        height: 440,
                        closeDialog: false,
                        beforeSubmit: function () {
                            if (shippDocGrid.getFormField('SignInCube').val() * 1 < 0) {
                                showError('提示', '签收方量必须大于等于0！');
                                return;
                            }
                        },
                        afterFormLoaded: function () {
                            ShippDocEnter();
                            var carField = shippDocGrid.getFormField('CarID');
                            //shippDocGrid.setFormFieldReadOnly("ID", true);
                            carField.unbind("change");
                            carField.bind('change', function (s) {
                                var carid = $(this).val();
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

                            //shippDocGrid.setFormFieldDisabled("ConstructUnit", true);
                            //shippDocGrid.setFormFieldDisabled("ProjectName", true);
                            //shippDocGrid.setFormFieldDisabled("CustName", true);
                            //shippDocGrid.setFormFieldDisabled("ContractName", true);

                            //                            window.setTimeout(function () {
                            //                                shippDocGrid.setFormFieldChecked("IsBack", true);
                            //                                $("input[name='ArriveTime']").val(dataTimeFormat(new Date()));
                            //                            }, 500);

                            //判断回厂时间是否为空，为空显示当前时间
                            var editArriveTime = $("#editArriveTime").val();
                            if ($.trim(editArriveTime).length == 0) {
                                var mydate = new Date();
                                var t = mydate.Format("yyyy-MM-dd hh:mm:ss");
                                $("#editArriveTime").val(t);
                            }

                        }
                        , postCallBack: function (response) {
                            shippDocGrid.getFormField("ShippDocID").val("");
                            shippDocGrid.getFormField("ShippDocID").focus();
                        }
                    });
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

                    if (shippDocGrid.isSelectedOnlyOne("请选择一条记录进行操作") == false)
                    {
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
                    str = ":"+";";
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

            //给ShipDocGrid中的每一行新增“历史视频”的按钮
            var historyVideoButton = "<input class='identityButton'  type='button' value='历史视频' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleHistoryVideo(" + ids[i] + ");\"  /> ";
            shippDocGrid.getJqGrid().jqGrid('setRowData', ids[i], { HistoryVideo: historyVideoButton });

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

    window.handleHistoryVideo = function (shipDocId) {
        var selectedShipDoc = shippDocGrid.getRecordByKeyValue(shipDocId);
//        console.log("selected Car.TerminalID = " + selectedShipDoc["Car.TerminalID"]);
//        console.log("selected DeliveryTime = " + selectedShipDoc["DeliveryTime"]);
//        console.log("selected ArriveTime = " + selectedShipDoc["ArriveTime"]);
//        console.log("gSysConfig.ShipDocHistoryVideoUrl = " + gSysConfig.ShipDocHistoryVideoUrl);

        if (gSysConfig.ShipDocHistoryVideoUrl == undefined || gSysConfig.ShipDocHistoryVideoUrl == "") {
            showError("未配置历史视频外部地址，请在【系统全局配置】->【GPS配置】中设置 运输单历史视频URL");
            return;
        }

        var historyVideoUrl = gSysConfig.ShipDocHistoryVideoUrl + "?deviceGuid=" + selectedShipDoc["Car.VideoDeviceGuid"] +
                                                                  "&beginTime=" + selectedShipDoc["DeliveryTime"];

        if (selectedShipDoc["ArriveTime"] !== "") {
            historyVideoUrl += "&endTime=" + selectedShipDoc["ArriveTime"];
        }

        window.open(historyVideoUrl, "_Blank");
    }

    //运输单修改历史
    var shipDocHisGrid = new MyGrid({
        renderTo: 'shipDocHisGrid'
            , width: 720
            , height: 240
            , storeURL: opts.findShipDocHisUrl
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , emptyrecords: '当前无任何修改'
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, sortable: false }
                , { label: '是否带回', name: 'IsBack', index: 'IsBack', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否有效', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '客户编号', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'CustName', searchoptions: { sopt: ['cn']} }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', hidden: true }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID', hidden: true }
                , { label: '配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', searchoptions: { sopt: ['cn']} }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr' }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 100 }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
                , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 80 }
                , { label: '泵工', name: 'PumpMan', index: 'PumpMan', width: 80 }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'right', searchoptions: { sopt: ['eq']} }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', search: false }
                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 60, align: 'right', search: false }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }
                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 50, align: 'right', search: false }
                , { label: '抗渗等级', name: 'ImpGrade', index: 'ImpGrade', hidden: true }
                , { label: '抗压等级', name: 'ImyGrade', index: 'ImyGrade', hidden: true }
                , { label: '抗冻等级', name: 'ImdGrade', index: 'ImdGrade', hidden: true }
                , { label: '骨料粒径', name: 'CarpRadii', index: 'CarpRadii', hidden: true }
                , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed', hidden: true }
                , { label: '湿拌方量', name: 'BetonCount', index: 'BetonCount', width: 60, align: 'right', search: false }
                , { label: '砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 60, align: 'right', search: false }
                , { label: '调度方量', name: 'SendCube', index: 'SendCube', width: 60, align: 'right' }
                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 60, align: 'right', search: false }
                , { label: '剩余方量', name: 'RemainCube', index: 'RemainCube', width: 60, align: 'right', search: false }
                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 60, align: 'right', search: false }
                , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 60, align: 'right' }
                , { label: '报废方量', name: 'ScrapCube', index: 'ScrapCube', width: 60, align: 'right', search: false }
                , { label: '转料方量', name: 'TransferCube', index: 'TransferCube', width: 60, align: 'right', search: false }
                , { label: '其他方量2', name: 'XuCube', index: 'XuCube', width: 80 }
                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', width: 60, align: 'right', search: false }
                , { label: '过磅方量', name: 'Cube', index: 'Cube', width: 60, align: 'right', search: false }
                , { label: '毛重(T)', name: 'TotalWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '皮重(T)', name: 'CarWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '净重(T)', name: 'Weight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '换算率(T/m³)', name: 'Exchange', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
                , { label: '出厂时间', name: 'DeliveryTime', index: 'DeliveryTime', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '当班司机', name: 'Driver', index: 'Driver', width: 60 }
                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', width: 60 }
                , { label: '发货员', name: 'Signer', index: 'Signer', width: 60 }
                , { label: '上料员', name: 'ForkLift', index: 'ForkLift', width: 60 }
                , { label: '操作员', name: 'Operator', index: 'Operator', width: 60 } 
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge', 'le', 'eq', 'gt', 'lt']} }
                , { label: '机组编号', name: 'ProductLineID', index: 'ProductLineID', width: 80 }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit', width: 100 }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
                , { label: '委托单位', name: 'EntrustUnit', index: 'EntrustUnit', width: 100 }
                , { label: '发货单公司', name: 'Accepter', index: 'Accepter' }
                , { label: '工程运距', name: 'Distance', index: 'Distance', width: 60 } 
                , { label: '前厂工长', name: 'LinkMan', index: 'LinkMan', width: 60 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', width: 80 }
                , { label: '工程编号', name: 'ProjectID', index: 'ProjectID', width: 80 }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', width: 60, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', width: 60, search: false }
               
                , { label: '入库上传状态', name: 'InStatus', index: 'InStatus', width: 80, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '出库上传状态', name: 'OutStatus', index: 'OutStatus', width: 80, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '异常信息', name: 'ExceptionInfo', index: 'ExceptionInfo', width: 100 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200, search: false }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 60, search: false }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 60, search: false }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', width: 120, search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            ]
            , autoLoad: false
    });

    //生产登记修改历史
    var dispatchHisGrid = new MyGrid({
        renderTo: 'dispatchHisGrid'
            , width: 720
            , height: 240
            , storeURL: opts.dispatchHisStoreUrl
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , emptyrecords: '当前无任何修改'
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, sortable: false }
                , { label: '操作', name: 'Act', index: 'Act', align: 'center', width: 60, sortable: false }
                , { label: '生产登记编号', name: 'DispatchID', index: 'DispatchID', width: 80, sortable: false }
                , { label: '任务单编号', name: 'TaskID', index: 'TaskID', width: 80, sortable: false }
                , { label: '生产线', name: 'ProductLineID', index: 'ProductLineID', align: 'center', width: 60 }
                , { label: '登记顺序', name: 'DispatchOrder', index: 'DispatchOrder', align: 'center', width: 60 }
                , { label: '启动时间', name: 'StartupTime', index: 'StartupTime', align: 'center', formatter: 'datetime', width: 130, sortable: false }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 60, align: 'center', sortable: false }
                , { label: '司机', name: 'Driver', index: 'Driver', width: 60, align: 'center', sortable: false }
                , { label: '执行时间', name: 'ExecTime', index: 'ExecTime', align: 'center', formatter: 'datetime', width: 130, sortable: false }
            ]
            , autoLoad: false
    });
    
    //查看运输单对应的生产登记历史记录
    function showHis(b, id) {
        var title = "运输单：&nbsp;<font color='#ff0000'>" + id + "</font>&nbsp;的历史记录";
        var refreshCon = "ShipDocID='" + id + "'";

        $("#dispatchHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
            close: function (event, ui) {
                $(this).dialog("destroy");
                dispatchHisGrid.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
            }
        });
        $('#dispatchHisWindow').dialog('open');
        dispatchHisGrid.refreshGrid(refreshCon);
    }

    //查看运输单对应的历史记录
    function showShipDocHis(b, id) {
        var title = "运输单：&nbsp;<font color='#ff0000'>" + id + "</font>&nbsp;的运输单历史记录";
        var refreshCon = "ShipDocID='" + id + "'";
        $("#shipDocHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
            close: function (event, ui) {
                $(this).dialog("destroy");
                shipDocHisGrid.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
            }
        });
        $('#shipDocHisWindow').dialog('open');
        shipDocHisGrid.refreshGrid(refreshCon);
    }

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
    //运输单回车
    function ShippDocEnter() {
        shippDocGrid.getFormField("ShippDocID").bind('keypress', function (event) {
            if (event.keyCode == 13) {
                var shippdocid = shippDocGrid.getFormField("ShippDocID").val();
                //动态生成select内容
                var requestURL = "/ShippingDocument.mvc/Get";
                var postParams = { ID: shippdocid };
                $.ajax({
                    type: 'post',
                    async: false,
                    url: requestURL,
                    data: postParams,
                    success: function (result) {
                        if (result.Result == true) {
                            var data = result.Data;
                            shippDocGrid.getFormField("ID").val(data.ID);
                            shippDocGrid.getFormField("ShippingCube").val(data.ShippingCube);
                            shippDocGrid.getFormField("SignInCube").val(data.SignInCube);
                            shippDocGrid.getFormField("ScrapCube").val(data.ScrapCube);
                            shippDocGrid.getFormField("TransferCube").val(data.TransferCube);
                            shippDocGrid.getFormField("BetonCount").val(data.BetonCount);
                            shippDocGrid.getFormField("SlurryCount").val(data.SlurryCount);

                            shippDocGrid.getFormField("ConstructUnit").val(data.ConstructUnit);
                            shippDocGrid.getFormField("DeliveryTime").val(crtTimeFtt(data.DeliveryTime));
                            shippDocGrid.getFormField("ProduceDate").val(crtTimeFtt(data.ProduceDate));
                            shippDocGrid.getFormField("ProjectName").val(data.ProjectName);
                            shippDocGrid.getFormField("ArriveTime").val(crtTimeFtt(data.ArriveTime));
                            shippDocGrid.getFormField("CastMode").val(data.CastMode);

                            shippDocGrid.getFormField("CarID").val(data.CarID);
                            shippDocGrid.getFormField("SendCube").val(data.SendCube);
                            shippDocGrid.getFormField("ProvidedTimes").val(data.ProvidedTimes);
                            shippDocGrid.getFormField("ProvidedCube").val(data.ProvidedCube);

                            shippDocGrid.getFormField("ParCube").val(data.ParCube);
                            shippDocGrid.getFormField("OtherCube").val(data.OtherCube);
                            shippDocGrid.getFormField("XuCube").val(data.XuCube);
                            shippDocGrid.getFormField("CarKm").val(data.CarKm);

                           
                            shippDocGrid.getFormField("LinkMan").val(data.LinkMan);
                            shippDocGrid.getFormField("OverTimeSubsidy").val(data.OverTimeSubsidy);
                            shippDocGrid.getFormField("TicketNO").val(data.TicketNO);
                            shippDocGrid.getFormField("PumpName").val(data.PumpName);

                            shippDocGrid.getFormField("Driver").val(data.Driver);
                            shippDocGrid.getFormField("Accepter").val(data.Accepter);
                            shippDocGrid.getFormField("ExceptionInfo").val(data.ExceptionInfo);

                            shippDocGrid.getFormField("ShippingCube").focus();
                            shippDocGrid.getFormField("ShippDocID").val('');
//                            var all = $("#time").html();
//                            var m = Number(all.substring(0, all.indexOf(":")));
//                            var s = Number(all.substring(all.indexOf(":") + 1, all.length + 1));
//                            var f = setInterval(function () {
//                                if (s < 10) {
//                                    //如果秒数少于10在前面加上0
//                                    $('#time').html(m + ':0' + s);
//                                } else {
//                                    $('#time').html(m + ':' + s);
//                                }
//                                s--;
//                                if (s < 0) {
//                                    //如果秒数少于0就变成59秒
//                                    if (m == 0) {
//                                        window.clearInterval(f);
//                                    }
//                                    m--;
//                                    s = 59;
//                                    shippDocGrid.getFormField("ID").val("");
//                                    shippDocGrid.getFormField("ID").focus();
//                                }
//                            }, 1000)
                        }
                        else {
                            shippDocGrid.getFormField("ShippDocID").val('');
                            showError('不存在此运输单信息！');
                            return;
                        }
                    }
                });
            }
        });
    }
}
 