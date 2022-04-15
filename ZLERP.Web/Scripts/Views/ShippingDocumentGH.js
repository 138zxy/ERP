
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
    function booleanIsAuditSelectValues() {
        return { 'true': '已审', 'false': '未审' };
    }
    function booleanIsOverTimeSelectValues() {
        return { 'true': '超时', 'false': '未超时' };
    }
    function booleanIsOverTimeSearchValues() {
        return { '': '', 1: '超时', 0: '未超时' };
    }
    var img;
    //自定义图片的格式，可以根据rowdata自定义
    function picFormatter(cellvalue, options, rowdata) {
        if (cellvalue == undefined || cellvalue == "" || cellvalue == null) {
            return " ";
        }
        var url2 = cellvalue; //  "\\Content\\Files\\img\\Background.jpg"
        return '<a href="' + url2 + '" target="view_window" alt="' + url2 + '"><img src="' + url2 + '" id="img' + rowdata.Id + '" alt="' + url2 + '" style="width:50px;height:50px;"  /></a>';
    }
    function unpicFormatter(cellvalue, options, cell) {
        return $('img', cell).attr('src');
        return $('a', cell).attr('href');
    }
    var shippDocGrid = new MyGrid({
        renderTo: 'shippDocGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ProduceDate'
            , sortOrder: 'DESC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 700
            , dialogHeight: 400
            , storeCondition: '1=1'
        //, singleSelect: true
            , columnReOrder: true
            , rowNumbers: true
            , advancedSearch: true
            , rowList: [10, 20, 30, 50, 100, 200, 300, 400, 500, 1000]
        //, multikey: 'shiftKey'
        //, multiselect: true
            , editSaveUrl: "/ShippingDocumentGH.mvc/Update"
		    , initArray: [
                { label: '操作', name: 'act', index: 'act', width: 60, sortable: false, align: "center", hidden: true }
                , { label: '运输单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, frozen: true }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 80 }
                , { label: '客户名称', name: 'CustName', index: 'CustName' }
                , { label: '建设单位', name: 'CustName', index: 'CustName' }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', search: false, width: 100 }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', search: false, width: 60 }
                , { label: '交货地址', name: 'DeliveryAddress', index: 'DeliveryAddress', search: false, width: 60 }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 60, align: 'center' }
                , { label: '发货单号', name: 'ShippDocNo', index: 'ShippDocNo', width: 60, searchoptions: { sopt: ['cn'] }, frozen: true }

                , { label: '签收量', name: 'SignInCube', index: 'SignInCube', width: 55, align: 'right', editable: false }
                , { label: "车号", name: "CarID", index: "CarID", width: 40, editable: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getCarNoStr() }, edittype: 'select', editoptions: { value: getCarNoStr()} }
                , { label: '车牌', name: 'Car.CarNo', index: 'Car.CarNo', width: 60 }
                , { label: '强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 90 }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['in', 'ed', 'eq', 'gt', 'lt']} }
                , { label: "浇筑方式", name: "CastMode", index: "CastMode", width: 60, editable: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: getDicCastModeStr() }, edittype: 'select', editoptions: { value: getDicCastModeStr()} }
                , { label: '合同号', name: 'ContractNo', index: 'ContractNo', width: 90, searchoptions: { sopt: ['cn'] }, frozen: true }
                , { label: '单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 50, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
        //, { label: '是否带回', name: 'IsBack', index: 'IsBack', width: 50, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '途中', '1': '已回' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否有效', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '无效', '1': '有效' }, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 60, searchoptions: { sopt: ['cn']} }
                , { label: '客户编号', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', hidden: true }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID', hidden: true }
                , { label: '配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues()} }
                , { label: '审核人', name: 'AuditMan', index: 'AuditMan', search: false, width: 50 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                
        //, { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 60 }
                 , { label: '客户筒仓号', name: 'CustSiloNo', index: 'CustSiloNo', width: 90, searchoptions: { sopt: ['cn'] }, frozen: true }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 55, align: 'right', search: false }
                , { label: '已供量', name: 'ProvidedCube', index: 'ProvidedCube', width: 55, align: 'right', search: false }
                , { label: '计划量', name: 'PlanCube', index: 'PlanCube', width: 55, align: 'right', search: false }
                , { label: '当班司机', name: 'Driver', index: 'Driver', search: true, width: 55 }
                , { label: '前场工长', name: 'LinkMan', index: 'LinkMan', width: 55 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', width: 75 }
                , { label: '图片1', name: 'url1', index: 'url1', formatter: picFormatter, width: 60, unformat: unpicFormatter }
                , { label: '图片2', name: 'url2', index: 'url2', formatter: picFormatter, width: 60, unformat: unpicFormatter }
                , { label: '图片3', name: 'url3', index: 'url3', formatter: picFormatter, width: 60, unformat: unpicFormatter }

                , { label: '抗渗等级', name: 'ImpGrade', index: 'ImpGrade', hidden: true }
                , { label: '抗压等级', name: 'ImyGrade', index: 'ImyGrade', hidden: true }
                , { label: '抗冻等级', name: 'ImdGrade', index: 'ImdGrade', hidden: true }
                , { label: '骨料粒径', name: 'CarpRadii', index: 'CarpRadii', hidden: true }
                , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed', hidden: true }
                , { label: '真实坍落度', name: 'RealSlump', index: 'RealSlump', width: 80, search: false, hidden: true }
                , { label: '干混量', name: 'BetonCount', index: 'BetonCount', width: 60, align: 'right', search: false, editable: false }
                , { label: '砂浆量', name: 'SlurryCount', index: 'SlurryCount', width: 55, align: 'right', search: false, editable: false, hidden: true }
                , { label: '调度量', name: 'SendCube', index: 'SendCube', width: 55, align: 'right', editable: false }
                , { label: '出票量', name: 'ParCube', index: 'ParCube', width: 55, align: 'right', search: false, editable: false }
                , { label: '上车余料', name: 'RemainCube', index: 'RemainCube', width: 55, align: 'right', search: false }
                , { label: '运输量', name: 'ShippingCube', index: 'ShippingCube', width: 55, align: 'right', search: false, editable: false }
                , { label: '报废量', name: 'ScrapCube', index: 'ScrapCube', width: 55, align: 'right', search: false, editable: false }
                , { label: '本车余料', name: 'TransferCube', index: 'TransferCube', width: 55, align: 'right', search: false, editable: false }
                , { label: '其他量2', name: 'XuCube', index: 'XuCube', width: 60, align: 'right', search: false, editable: false }
                , { label: '补量', name: 'CompensationCube', index: 'CompensationCube', width: 60, align: 'right', search: false, editable: false }
                , { label: '其他量', name: 'OtherCube', index: 'OtherCube', width: 55, align: 'right', search: false, hidden: true, editable: false }
                , { label: '过磅量', name: 'Cube', index: 'Cube', width: 55, align: 'right', search: false }
                , { label: '毛重(T)', name: 'TotalWeight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '皮重(T)', name: 'CarWeight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '净重(T)', name: 'Weight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '过磅时间', name: 'GB_Date', index: 'GB_Date', width: 55, align: 'right', search: false }
                , { label: '换算率(T/m³)', name: 'Exchange', width: 80, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
        // , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 55, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
        // , { label: '运费', name: 'SumPrice', index: 'SumPrice' }
                , { label: '出厂时间', name: 'DeliveryTime', index: 'DeliveryTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
         
                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', search: false, width: 55 }
                , { label: '调度员', name: 'Signer', index: 'Signer', search: false, width: 50 }
                , { label: '班组', name: 'ForkLift', index: 'ForkLift', search: false, width: 50 }
                , { label: '操作员', name: 'Operator', index: 'Operator', search: false, width: 50 }
                , { label: '计划班组', name: 'PlanClass', index: 'PlanClass', search: false, width: 55 }
                , { label: '机组编号', name: 'ProductLineID', index: 'ProductLineID', width: 55 }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit', search: false }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', search: false, width: 55 }
                , { label: '委托单位', name: 'EntrustUnit', index: 'EntrustUnit', search: false, width: 55 }
                , { label: '现场验收人', name: 'Accepter', index: 'Accepter', search: false, width: 65 }
                , { label: '工程运距', name: 'Distance', index: 'Distance', width: 55 }
      

                , { label: '级配', name: 'ProduceTask.CarpGrade', index: 'ProduceTask.CarpGrade', width: 80 }
            
                , { label: '工程编号', name: 'ProjectID', index: 'ProjectID', search: false, width: 65 }
                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', search: false, width: 55 }
                , { label: '小票票号', name: 'TicketNO', index: 'TicketNO', search: false, width: 55, editable: false }
                , { label: '异常信息', name: 'ExceptionInfo', index: 'ExceptionInfo', width: 200, search: false }
                , { label: '是否超时', name: 'IsOverTime', index: 'IsOverTime', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '未超时', '1': '超时' }, stype: 'select', searchoptions: { value: booleanIsOverTimeSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsOverTimeSelectValues()} }
                , { label: '超时原因', name: 'OverTimeReason', index: 'OverTimeReason', width: 200, search: false, editable: false }
                , { label: '配比', name: 'FormulaName', index: 'FormulaName', width: 200, search: false }
                , { label: '配比标识', name: 'BetonTag', index: 'BetonTag', width: 120, search: false, editable: false }
                , { label: '是否带回', name: 'IsBack', index: 'IsBack', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '途中', '1': '已回' }, stype: 'select', searchoptions: { value: booleanIsBackSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsBackSelectValues()} }
                , { label: '是否抗压委托', name: 'IsCompComm', index: 'IsCompComm', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '未生成', '1': '已生成' }, stype: 'select', searchoptions: { value: booleanIsBackSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsBackSelectValues()} }
                , { label: '是否抗渗委托', name: 'IsImpComm', index: 'IsImpComm', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '未生成', '1': '已生成' }, stype: 'select', searchoptions: { value: booleanIsBackSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsBackSelectValues()} }
                 , { label: '是否上报余料', name: 'IsClout', index: 'IsClout', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '是', '0': '否' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues()} }
                , { label: '运费单价', name: 'CarFreight', index: 'CarFreight', width: 120, search: false, editable: false }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200, search: false, editable: false }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    shippDocGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    shippDocGrid.refreshGrid('1=1');
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
                    //打印条形码
                    //var data = shippDocGrid.getSelectedRecord();
                    //var auditValue = data.ID;
                    //var shipurl = "/Reports/Produce/ReportPrint.aspx?id=" + auditValue + "&type=ShippingDocumentGH&isclose=true";
                    //window.open(shipurl);

                    var docId = shippDocGrid.getSelectedKey();
                    if (docId <= 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var url = "/GridReport/PrintDirect.aspx?report=ShippingDocumentGH&ID=" + docId;
                    window.open(url, "_blank");
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
                        afterFormLoaded: function () {

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
                    var ShipDocType = data.ShipDocType;
                    var Weight = data.Weight;
                    if (Weight > 0 && ShipDocType == '成品仓') {
                        showMessage('提示', '已过磅的成品仓不能作废');
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
                        var Weight = records[i].Weight;
                        var ShipDocType = records[i].ShipDocType;
                        if (Weight > 0 && ShipDocType == '成品仓') {
                            showMessage('提示', '已过磅的成品仓不能删除');
                            return;
                        }
                    }

                    shippDocGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //报表设计
                , handleDesign: function (btn) {
                    var url = "/GridReport/DesignReport.aspx?report=ShippingDocumentGH";
                    window.open(url, "_blank");
                    //使用选中的发货单作设计数据
                    //var docId = shippDocGrid.getSelectedKey();
                    //if (!isEmpty(docId)) {
                    //    printShippingDocGH('design', docId);
                    //}
                    //else {//未选中任务发货单则使用测试数据设计
                    //    shippingDocDesign();
                    //}
                },
                //出厂检测
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

                        //确认操作
                        showConfirm("确认信息", confirmMessage, function () {
                            ajaxRequest(
                            opts.AuditUrl,
                            {
                                id: selectedRecord.ID,
                                status: selectedRecord.IsAudit == 0 ? false : true
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
                                    shippDocGrid.reloadGrid();
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
                    showConfirm("确认信息", "签收量为：" + signInCube + "。其他量2为：" + xuCube + "。<br />审核后将不允许删除、修改。是否继续审核？", function (btn) {
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
                //显示-其他量 列
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
                    showConfirm("确认信息", "是否对该运输单进行抽样吗？", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            "/ShippingDocumentGH.mvc/Sampling",
                            {
                                id: record.ID
                            },
                            true,
                            function () {
                                $(btn.currentTarget).button({ disabled: false });
                                shippDocGrid.refreshGrid('1=1');
                            }
                        );
                        $(this).dialog("close");
                    });
                },
                //打印抗压委托单
                handlePrintKywt: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录打印");
                        return false;
                    }
                    var record = shippDocGrid.getSelectedRecord();
                    if (record && record.IsCompComm == 'false') {
                        var shipdocid = record.ShipDocID;
                        ajaxRequest(
                            "/ShippingDocumentGH.mvc/CreateCompComm",
                            {
                                id: record.ID
                            },
                            true,
                            function () {
                                shippDocGrid.reloadGrid();
                            }
                        );
                    }
                    printKywtDoc('preview', shippDocGrid.getSelectedKey());
                },
                //打印抗渗委托单
                handlePrintKswt: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录打印");
                        return false;
                    }
                    var record = shippDocGrid.getSelectedRecord();
                    if (record && record.IsImpComm == 'false') {
                        ajaxRequest(
                            "/ShippingDocumentGH.mvc/CreateImpComm",
                            {
                                id: record.ID
                            },
                            true,
                            function () {
                                shippDocGrid.reloadGrid();
                            }
                        );
                    }
                    printKswtDoc('preview', shippDocGrid.getSelectedKey());
                    //printShippingDoc('print', shippDocGrid.getSelectedKey());
                } //历史视频
                , handleHisVideoReport: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showError("提示", "请选择一条记录进行操作！");
                        return;
                    }
                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    var TerminalID = selectedRecord.SN;
                    if (TerminalID == undefined || TerminalID.length <= 0) {
                        showError("提示", "SN号不能为空！");
                        return;
                    }
                    window.open("/Reports/HisVideo/RHisVideo.aspx?TerminalID=" + TerminalID);
                }
                , handleAlarm: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showError("提示", "请选择一条记录进行操作！");
                        return;
                    }
                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    var TerminalID = selectedRecord.CarID;
                    if (TerminalID == undefined || TerminalID.length <= 0) {
                        showError("提示", "车号不能为空！");
                        return;
                    }
                    //window.open.open("/Reports/HisVideo/RHisVideo.aspx?TerminalID=" + TerminalID);

                    openToUrl('/AlarmLog.mvc/Index?f=4208', '42', TerminalID);
                }
                //修改运距
                , handleEditDistance: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    shippDocGrid.handleEdit({
                        title: "修改运距",
                        width: 350,
                        height: 260,
                        loadFrom: 'EditDistanceForm',
                        btn: btn
                    });
                }
                //添加包装仓发货单
                , handlePackAdd: function (btn) {
                    shippDocGrid.handleAdd({
                        loadFrom: 'AddPackFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            $('#TotalWeight', '#AddPackFormDiv').removeClass('text requiredval');
                            $('#CarWeight', '#AddPackFormDiv').removeClass('text requiredval');
                            $('#Weight', '#AddPackFormDiv').removeClass('text requiredval');
                            var carNo = $('#CarID_In', '#AddPackFormDiv').val();
                            $('#CarID', '#AddPackFormDiv').val(carNo);
                            $('#IsEffective', '#AddPackFormDiv').val(true);
                        }
                    });
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
        var str = getStr("/ShippingDocumentGH.mvc/getCarNoList");
        return str;      
    }
 
    //获取司机列表
    function getDriverStr() {
        var str = getStr("/ShippingDocumentGH.mvc/getDriverList");
        return str;
    }
    //动态生成下拉字符串
    function getStr(requestURL, postParams) {
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
            rid = ids[i];
            var carid = record.CarID;
            be = "<input class='identityButton'  type='button' value='查看报警' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"AuditTZ(" + carid + ");\"  />";
            shippDocGrid.getJqGrid().jqGrid('setRowData', rid, { act: be });
        }
        $(".cbox").shiftSelect();
    });
    //弹出【】窗体
    window.AuditTZ = function (id) {
        TZValue = id;
        //$("a[rev='3103']").click();
        openToUrl('/AlarmLog.mvc/Index?f=4208', '42', id);
    };
    //跳转到目标页
    window.openToUrl = function (url, pid, tzvalue) {
        $(".active").removeClass();
        $("#menu a[rev=" + pid + "]").parent().addClass("active");
        //console.dir($("#menu a[rev=" + pid + "]"));
        $("#scollbox").empty();
        var pid = pid;
        TZValue = tzvalue; //alert(TZValue)
        if (pid in subMenuCache) {
            $("#scollbox").append(subMenuCache[pid]);
        }
        else {
            subMenuCache[pid] = '';
            $.each(subMenus, function (i, n) {
                if (n.PID == pid) {
                    if (n.IsL) {//alert(n.Url+'|||'+n.ID)
                        n.Url = appendFuncId(n.Url, n.ID);
                        var menuHtml = '<li><a onclick= openUrl(this,"' + n.Url + '") rev="' + n.ID + '" href="javascript:void(0)" ><span>' + n.Name + '</span></a></li>';
                        $("#scollbox").append(menuHtml);
                        subMenuCache[pid] += menuHtml;

                    }
                    else {
                        var menuHtml = '<li><a id="func_' + n.ID + '" onclick="openUrl(this,\'\')" class="fgmenu" href="javascript:void(0)" ><span><div style="margin-top:3px;float:right;"  class="ui-icon ui-icon-triangle-1-s" ></div>' + n.Name + '</span></a><div style="display:none;">' + renderSubMenu(n.ID) + '</div></li>';
                        $("#scollbox").append(menuHtml);
                        subMenuCache[pid] += menuHtml;
                    }
                }
            });
        }
        $('#scollbox').css('margin-left', '0px');
        $('a.fgmenu', '#scollbox').each(function () {
            $(this).fgmenu({
                content: $(this).next().html(),
                flyOut: true
            });
        });
        var func_id = url.substr(url.indexOf("f=") + 2);
        $("#menu_2 a[rev=" + func_id + "]").click();
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
                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'right', searchoptions: { sopt: ['eq']} }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', search: false }
                , { label: '已供量', name: 'ProvidedCube', index: 'ProvidedCube', width: 60, align: 'right', search: false }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }
                , { label: '计划量', name: 'PlanCube', index: 'PlanCube', width: 50, align: 'right', search: false }
                , { label: '抗渗等级', name: 'ImpGrade', index: 'ImpGrade', hidden: true }
                , { label: '抗压等级', name: 'ImyGrade', index: 'ImyGrade', hidden: true }
                , { label: '抗冻等级', name: 'ImdGrade', index: 'ImdGrade', hidden: true }
                , { label: '骨料粒径', name: 'CarpRadii', index: 'CarpRadii', hidden: true }
                , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed', hidden: true }
                , { label: '真实坍落度', name: 'RealSlump', index: 'RealSlump', width: 80, search: false, hidden: true }
                , { label: '混凝土量', name: 'BetonCount', index: 'BetonCount', width: 60, align: 'right', search: false }
                , { label: '砂浆量', name: 'SlurryCount', index: 'SlurryCount', width: 60, align: 'right', search: false }
                , { label: '调度量', name: 'SendCube', index: 'SendCube', width: 60, align: 'right' }
                , { label: '出票量', name: 'ParCube', index: 'ParCube', width: 60, align: 'right', search: false }
                , { label: '剩余量', name: 'RemainCube', index: 'RemainCube', width: 60, align: 'right', search: false }
                , { label: '运输量', name: 'ShippingCube', index: 'ShippingCube', width: 60, align: 'right', search: false }
                , { label: '签收量', name: 'SignInCube', index: 'SignInCube', width: 60, align: 'right' }
                , { label: '报废量', name: 'ScrapCube', index: 'ScrapCube', width: 60, align: 'right', search: false }
                , { label: '转料量', name: 'TransferCube', index: 'TransferCube', width: 60, align: 'right', search: false }
                , { label: '其他量2', name: 'XuCube', index: 'XuCube', width: 80 }
                , { label: '其他量', name: 'OtherCube', index: 'OtherCube', width: 60, align: 'right', search: false }
                , { label: '过磅量', name: 'Cube', index: 'Cube', width: 60, align: 'right', search: false }
                , { label: '毛重(T)', name: 'TotalWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '皮重(T)', name: 'CarWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '净重(T)', name: 'Weight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '换算率(T/m³)', name: 'Exchange', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
                , { label: '出厂时间', name: 'DeliveryTime', index: 'DeliveryTime', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '当班司机', name: 'Driver', index: 'Driver', width: 60, searchoptions: { sopt: ['eq']} }
                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', width: 60 }
                , { label: '发货员', name: 'Signer', index: 'Signer', width: 60 }
                , { label: '上料员', name: 'ForkLift', index: 'ForkLift', width: 60 }
                , { label: '操作员', name: 'Operator', index: 'Operator', width: 60 }
                , { label: '计划班组', name: 'PlanClass', index: 'PlanClass', width: 60 }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge', 'le', 'eq', 'gt', 'lt']} }
                , { label: '机组编号', name: 'ProductLineID', index: 'ProductLineID', width: 80 }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit', width: 100 }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
                , { label: '委托单位', name: 'EntrustUnit', index: 'EntrustUnit', width: 100 }
                , { label: '发货单公司', name: 'Accepter', index: 'Accepter' }
                , { label: '工程运距', name: 'Distance', index: 'Distance', width: 60 }
                , { label: '区间编号', name: 'RegionID', index: 'RegionID', width: 80, search: false }
                , { label: '前厂工长', name: 'LinkMan', index: 'LinkMan', width: 60 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', width: 80 }
                , { label: '工程编号', name: 'ProjectID', index: 'ProjectID', width: 80 }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', width: 60, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', width: 60, search: false }
                , { label: '小票票号', name: 'TicketNO', index: 'TicketNO', width: 80 }
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

    function ChangeCarType(value) {
        alert(value);
    }
    //新增包装仓-选择车辆类型事件
    $('input:radio[name="CarType"]').change(function () {
        var val = $(this).val();
        if (val == "outside") {
            document.getElementById("OutCarID").style.display = "inline";
            document.getElementById("InCarID").style.display = "none ";
            $('#CarID', '#AddPackFormDiv').removeClass('requiredval valid');
        } else if (val == "inside") {
            document.getElementById("OutCarID").style.display = "none ";
            document.getElementById("InCarID").style.display = "inline";
            $("#TotalWeight", '#AddPackFormDiv').val(0);
            $("#CarWeight", '#AddPackFormDiv').val(0);
            $("#Weight", '#AddPackFormDiv').val(0);
            $("#BetonCount", '#AddPackFormDiv').val(0);
            $("#SendCube", '#AddPackFormDiv').val(0);
            $("#ParCube", '#AddPackFormDiv').val(0);
            $("#SignInCube", '#AddPackFormDiv').val(0);
        }
    });

    $('select:[name="CarID_In"]', '#AddPackFormDiv').unbind("change");
    $('select:[name="CarID_In"]', '#AddPackFormDiv').bind('change', function (s) {
        var carid = $(this).val();
        return;
        if (isEmpty(carid)) {
            return;
        }
        $('#CarID', '#AddPackFormDiv').val(carid);
        ajaxRequest(opts.getCarInfoUrl, { id: carid }, false, function (response) {
            var MaxCube = response.MaxCube;
            if (!isEmpty(MaxCube)) {
                $("#BetonCount", '#AddPackFormDiv').val(MaxCube);
                $("#SendCube", '#AddPackFormDiv').val(MaxCube);
                $("#ParCube", '#AddPackFormDiv').val(MaxCube);
                $("#SignInCube", '#AddPackFormDiv').val(MaxCube);
            }
        });
    });

    $('input:text[name="Weight"]', '#AddPackFormDiv').unbind("change");
    $('input:text[name="Weight"]', '#AddPackFormDiv').bind('change', function (s) {
        var Weight = $(this).val();
        $("#BetonCount", '#AddPackFormDiv').val(Weight);
        $("#SendCube", '#AddPackFormDiv').val(Weight);
        $("#ParCube", '#AddPackFormDiv').val(Weight);
        $("#SignInCube", '#AddPackFormDiv').val(Weight);
    });

    $('input:text[name="TotalWeight"]', '#AddPackFormDiv').unbind("change");
    $('input:text[name="TotalWeight"]', '#AddPackFormDiv').bind('change', function (s) {
        var TotalWeight = $(this).val();
        var CarWeight = $('input:text[name="CarWeight"]', '#AddPackFormDiv').val();
        if (!isEmpty(TotalWeight) && !isEmpty(CarWeight)) {
            var Weight = TotalWeight - CarWeight;
            $("#Weight", '#AddPackFormDiv').val(Weight);
            $("#BetonCount", '#AddPackFormDiv').val(Weight);
            $("#SendCube", '#AddPackFormDiv').val(Weight);
            $("#ParCube", '#AddPackFormDiv').val(Weight);
            $("#SignInCube", '#AddPackFormDiv').val(Weight);
        }
    });
    $('input:text[name="CarWeight"]', '#AddPackFormDiv').unbind("change");
    $('input:text[name="CarWeight"]', '#AddPackFormDiv').bind('change', function (s) {
        var CarWeight = $(this).val();
        var TotalWeight = $('input:text[name="TotalWeight"]', '#AddPackFormDiv').val();
        if (!isEmpty(TotalWeight) && !isEmpty(CarWeight)) {
            var Weight = TotalWeight - CarWeight;
            $("#Weight", '#AddPackFormDiv').val(Weight);
            $("#BetonCount", '#AddPackFormDiv').val(Weight);
            $("#SendCube", '#AddPackFormDiv').val(Weight);
            $("#ParCube", '#AddPackFormDiv').val(Weight);
            $("#SignInCube", '#AddPackFormDiv').val(Weight);
        }
    });
    $('#CarID_Out', '#AddPackFormDiv').unbind("change");
    $('#CarID_Out', '#AddPackFormDiv').bind('change', function (s) {
        var CarID = $(this).val();
        $('#CarID', '#AddPackFormDiv').val(CarID);
    });
}
 