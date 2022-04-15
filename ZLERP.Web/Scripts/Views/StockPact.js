﻿//原材料采购合同
function stockPactInit(opts) {
    var sid;
    function FootFromValues() {
        return { '': '', 0: '厂商数量', 1: '净重' };
    }
    function dicToolbarSearchValues2(pId) {
        var total = gDics[pId].length;
        var obj = { '': '' };
        for (var i = 0; i < total; i++) {
            var dic = gDics[pId][i];
            obj[dic.Field1] = dic.Field3;
        }
        return obj;
    }
    function booleanIsAuditSearchValues() {
        return { '': '', 1: '已审', 0: '未审' };
    }
    function booleanIsAuditSelectValues() {
        return { 'true': '已审', 'false': '未审' };
    }
    function dicCodeFmt2(cellvalue, options, rowObject) {
        if (cellvalue == null) return '';
        if (typeof (cellvalue) != 'undefined' && !$.isEmptyObject(gDics)) {
            var pId = options.colModel.formatoptions.parentId;
            if (gDics.hasOwnProperty(pId)) {
                var total = gDics[pId].length;
                for (var i = 0; i < total; i++) {
                    var dic = gDics[pId][i];
                    if (dic.Field1 == cellvalue || dic.ID == cellvalue) {//Dic表TreeCode和ID字段关联都可以
                        return '<span rel="' + cellvalue + '" class="' + pId + '_' + cellvalue + '">' + dic.Field3 + '</span>';
                    }

                }
            }
        }

        return cellvalue;
    }
    function dicCodeUnFmt2(cellvalue, options, cell) {
        var val = $('span', cell).attr('rel');
        if (typeof (val) != 'undefined' && val != "")
            return val;
        else
            return cellvalue;
    }
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: 350
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 830
            , dialogHeight: 400
		    , initArray: [
                  { label: '流水号', name: 'ID', index: 'ID', width: 80 }
                , { label: '供货厂家编号', name: 'SupplyID', index: 'SupplyID', editable: true, hidden: true }
                , { label: '供货厂家', name: 'SupplyName', index: 'SupplyInfo.SupplyName', width: 110 }
                , { label: '合同号', name: 'StockPactNo', index: 'StockPactNo' }
                , { label: '合同名称', name: 'PactName', index: 'PactName' }
                , { label: '合同状态', name: 'PactState', index: 'PactState' }
                , { label: '是否审核', name: 'AuditStatus', index: 'AuditStatus', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues() } }
                , { label: '签订时间', name: 'EstablishTime', index: 'EstablishTime', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '业务员', name: 'EstablishMan', index: 'EstablishMan', editable: true }
                , { label: '业务员电话', name: 'Phone', index: 'Phone' }
                , { label: '客服电话', name: 'CustomerPhone', index: 'CustomerPhone' }
                , { label: '原料编号1', name: 'StuffID', index: 'StuffID', editable: true, hidden: true }
                , { label: '原料编号2', name: 'StuffID1', index: 'StuffID1', editable: true, hidden: true }
                , { label: '原料编号3', name: 'StuffID2', index: 'StuffID2', editable: true, hidden: true }
                , { label: '原料编号4', name: 'StuffID3', index: 'StuffID3', editable: true, hidden: true }
                , { label: '原料编号5', name: 'StuffID4', index: 'StuffID5', editable: true, hidden: true }
                , { label: '原料编号1', name: 'SpecID', index: 'SpecID', editable: true, hidden: true }
                , { label: '原料编号2', name: 'SpecID1', index: 'SpecID1', editable: true, hidden: true }
                , { label: '原料编号3', name: 'SpecID2', index: 'SpecID2', editable: true, hidden: true }
                , { label: '原料编号4', name: 'SpecID3', index: 'SpecID3', editable: true, hidden: true }
                , { label: '原料编号5', name: 'SpecID4', index: 'SpecID4', editable: true, hidden: true }
                , { label: '磅差一', name: 'lowbangcha', index: 'lowbangcha', editable: true, hidden: true }
                , { label: '磅差二', name: 'highbangcha', index: 'highbangcha', editable: true, hidden: true }
                , { label: '期初欠款', name: 'baseMoney', index: 'baseMoney ', width: 110 }

                , { label: '单位', name: 'GageUnit', index: 'GageUnit', editable: true, hidden: true }
                , { label: '原料名称1', name: 'StuffName', index: 'StuffInfo.StuffName', width: 110 }
                , { label: '原料规格1', name: 'SpecName', index: 'StuffinfoSpec.SpecName', width: 60 }
                , { label: '基准单价1', name: 'StockPrice', index: 'StockPrice', width: 60 }
   		        , { label: '税率1', name: 'TaxRate', index: 'TaxRate', stype: 'select', searchoptions: { value: dicToolbarSearchValues2('TaxRate') }, formatter: dicCodeFmt2, unformat: dicCodeUnFmt2, formatoptions: { parentId: 'Bd_Taxrate' }, width: 80, align: 'center' }
                , { label: '原料名称2', name: 'StuffName1', index: 'StuffInfo1.StuffName', width: 110 }
                , { label: '原料规格2', name: 'SpecName1', index: 'StuffinfoSpec1.SpecName1', width: 60 }
                , { label: '基准单价2', name: 'StockPrice1', index: 'StockPrice1', width: 60 }
   		        , { label: '税率2', name: 'TaxRate1', index: 'TaxRate1', stype: 'select', searchoptions: { value: dicToolbarSearchValues2('TaxRate') }, formatter: dicCodeFmt2, unformat: dicCodeUnFmt2, formatoptions: { parentId: 'Bd_Taxrate' }, width: 80, align: 'center' }
                , { label: '原料名称3', name: 'StuffName2', index: 'StuffInfo2.StuffName', width: 110 }
                , { label: '原料规格3', name: 'SpecName2', index: 'StuffinfoSpec2.SpecName2', width: 60 }
                , { label: '基准单价3', name: 'StockPrice2', index: 'StockPrice2', width: 60 }
   		        , { label: '税率3', name: 'TaxRate2', index: 'TaxRate2', stype: 'select', searchoptions: { value: dicToolbarSearchValues2('TaxRate') }, formatter: dicCodeFmt2, unformat: dicCodeUnFmt2, formatoptions: { parentId: 'Bd_Taxrate' }, width: 80, align: 'center' }
                , { label: '原料名称4', name: 'StuffName3', index: 'StuffInfo3.StuffName', width: 110 }
                , { label: '原料规格4', name: 'SpecName3', index: 'StuffinfoSpec3.SpecName3', width: 60 }
                , { label: '基准单价4', name: 'StockPrice3', index: 'StockPrice3', width: 60 }
   		        , { label: '税率4', name: 'TaxRate3', index: 'TaxRate3', stype: 'select', searchoptions: { value: dicToolbarSearchValues2('TaxRate') }, formatter: dicCodeFmt2, unformat: dicCodeUnFmt2, formatoptions: { parentId: 'Bd_Taxrate' }, width: 80, align: 'center' }
                , { label: '原料名称5', name: 'StuffName4', index: 'StuffInfo4.StuffName', width: 110 }
                , { label: '原料规格5', name: 'SpecName4', index: 'StuffinfoSpec4.SpecName4', width: 60 }
                , { label: '基准单价5', name: 'StockPrice4', index: 'StockPrice4', width: 60 }
   		        , { label: '税率5', name: 'TaxRate4', index: 'TaxRate4', stype: 'select', searchoptions: { value: dicToolbarSearchValues2('TaxRate') }, formatter: dicCodeFmt2, unformat: dicCodeUnFmt2, formatoptions: { parentId: 'Bd_Taxrate' }, width: 80, align: 'center' }
                , { label: '数量(吨)', name: 'Amount', index: 'Amount', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100, hidden: true }
                , { label: '采购单价(元/吨)', name: 'StockPrice', index: 'StockPrice', width: 120, hidden: true, formatter: T2KgFmt, unformat: Kg2TFmt }
                , { label: '认磅差比例', name: 'BangchaRate', index: 'BangchaRate', editable: true, hidden: true }
                , { label: '结算数量来源', name: 'FootFrom', index: 'FootFrom', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '厂商数量', '1': '净重' }, stype: 'select', searchoptions: { value: FootFromValues() }, edittype: 'select', editoptions: { value: FootFromValues()} }
                , { label: '质量要求', name: 'QualityNeed', index: 'QualityNeed', editable: true }
                , { label: '录入时间', name: 'EstablishTime', index: 'EstablishTime', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }

                , { label: '报警百分比', name: 'WarmPercent', index: 'WarmPercent', hidden: true, editable: true }
                , { label: '称重依据', name: 'WeighBy', index: 'WeighBy', hidden: true, editable: true }
                , { label: '来源地', name: 'SourceAddr', index: 'SourceAddr', editable: true }
                , { label: '结算方式', name: 'FootMode', index: 'FootMode', hidden: true, editable: true }
                , { label: "价格文件", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false }
                , { label: '备注', name: 'Remark', index: 'Remark', editable: true }
                , { label: '暗扣率1', name: 'DarkRate', index: 'DarkRate', width: 80, hidden: true }
                , { label: '暗扣率2', name: 'DarkRate1', index: 'DarkRate1', width: 80, hidden: true }
                , { label: '暗扣率3', name: 'DarkRate2', index: 'DarkRate2', width: 80, hidden: true }
                , { label: '暗扣率4', name: 'DarkRate3', index: 'DarkRate3', width: 80, hidden: true }
                , { label: '暗扣率5', name: 'DarkRate4', index: 'DarkRate4', width: 80, hidden: true }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 130 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge'] } }
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
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(response.Data); //上传附件
                            }
                        },
                        afterFormLoaded: function () {
                            StuffIDChange();
                            StuffID1Change();
                            StuffID2Change();
                            StuffID3Change();
                            StuffID4Change();
                            $("#Attachments").empty();
                        }
                    });
                },
                handlePayMoney: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }

                    myJqGrid.handleAdd({
                        loadFrom: 'PayMoneyDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#StockPactChild_StockPactID").val(myJqGrid.getSelectedRecord().ID);
                        }
                    });
                },
                handleSuReport: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }

                    var selectedRecord = myJqGrid.getSelectedRecord();
                    url = "/Reports/Finance/R020234.aspx?uid=" + selectedRecord.SupplyID; //+ "&uname=" + selectedRecord.SupplyInfo.SupplyName;
                    window.open(url);
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data && data.AuditStatus == '1') {
                        showMessage('该单价记录已审核，不允许再修改');
                        return;
                    };
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: "StockPact",
                        afterFormLoaded: function () {
                            StuffIDChange();
                            StuffID1Change();
                            StuffID2Change();
                            StuffID3Change();
                            StuffID4Change();

                            //修改时 下拉框绑值必须这样绑定
                            bindSelectData($('#StockPact_SpecID'), opts.findSpecUrl, { condition: "StuffID = '" + data.StuffID + "'" }, function () { $("#StockPact_SpecID").val(data.SpecID); });
                            bindSelectData($('#StockPact_SpecID1'), opts.findSpecUrl, { condition: "StuffID = '" + data.StuffID1 + "'" }, function () { $("#StockPact_SpecID1").val(data.SpecID1); });
                            bindSelectData($('#StockPact_SpecID2'), opts.findSpecUrl, { condition: "StuffID = '" + data.StuffID2 + "'" }, function () { $("#StockPact_SpecID2").val(data.SpecID2); });
                            bindSelectData($('#StockPact_SpecID3'), opts.findSpecUrl, { condition: "StuffID = '" + data.StuffID3 + "'" }, function () { $("#StockPact_SpecID3").val(data.SpecID3); });
                            bindSelectData($('#StockPact_SpecID4'), opts.findSpecUrl, { condition: "StuffID = '" + data.StuffID4 + "'" }, function () { $("#StockPact_SpecID4").val(data.SpecID4); });
                            //显示附件
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
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //, handleAuditing: function (btn) {
                //    console.log();
                //    var data = myJqGrid.getSelectedRecord();
                //    if (data && data.AuditStatus == '1') {
                //        showMessage('该单价记录已审核，不允许再审核');
                //        return;
                //    };
                //    myJqGrid.handleEdit({
                //        loadFrom: 'AuditingFormDiv',
                //        btn: btn,
                //        width: 300,
                //        height: 250,
                //        prefix: "StockPact",
                //        afterFormLoaded: function () {
                //            myJqGrid.getFormField("Auditor").val(opts.currentUser);
                //        }
                //    });
                //}
                , handleAuditing: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行审核操作");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();

                    var confirmMessage = "您确定将此运输单设置为&nbsp;<font color=red><b>审核通过</b></font>&nbsp;状态吗？";

                    if (record.AuditStatus == "true" || record.AuditStatus == 1) {
                        confirmMessage = "您确定需要将此运输单设置为&nbsp;<font color=green><b>审核未通过</b></font>&nbsp;吗？";
                    }
                    var status = 0;
                    if (record.AuditStatus == 0) {
                        status = 1;
                    }
                    else if (record.AuditStatus == 1) {
                        status = 0;
                    }
                    //确认操作
                    showConfirm("确认信息", confirmMessage, function () {
                        ajaxRequest(
                        opts.AuditUrl,
                        {
                            id: record.ID,
                            status: status
                        },
                        true,
                        function (response) {
                            if (response.Result) {
                                myJqGrid.refreshGrid('1=1');
                                //根据ID获取行号
                                //var rid = $("#myJqGrid").jqGrid("getGridParam", "selrow");
                                //myJqGrid.jqGrid('setCell', rid, 'IsAudit', 1);
                                //for (var i = 0; i < keys.length; ++i) {
                                //    if (record.AuditStatus == "true" || record.AuditStatus == 1) {
                                //        myJqGrid.getJqGrid().setCell(keys[i], 'AuditStatus', 0);
                                //    }
                                //    else {
                                //        myJqGrid.getJqGrid().setCell(keys[i], 'AuditStatus', 1);
                                //    }
                                //    //$(btn.currentTarget).button({ disabled: false });
                                //    //myJqGrid.refreshGrid('1=1');
                                //}
                            }
                        }
                    );
                        $(this).dialog("close");
                    });
                },
                handleBitUpdatePrice: function (btn) {
                    $("#DivBitUpdatePrice").dialog("open");
                },

                UpdateBalanceRecordAndItems: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectRecord = myJqGrid.getSelectedRecord();

                    ajaxRequest(btn.data.Url, {
                        stockPactId: selectRecord.ID
                    },
					true,
					function (data) {
					    myJqGrid.refreshGrid();
					});
                }
            }
    });
    //上传附件
    function attachmentUpload(objectId) {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload
            ({
                url: opts.uploadUrl + "?objectType=StockPact&objectId=" + objectId,
                secureuri: false,
                fileElementId: "uploadFile",
                dataType: "json",
                beforeSend: function () {
                    $("#loading").show();
                },
                complete: function () {
                    $("#loading").hide();
                },
                success: function (data, status) {
                    if (data.Result) {
                        showMessage("附件上传成功");
                        myJqGrid.reloadGrid();
                    }
                    else {
                        showError("附件上传失败", data.Message);
                    }
                },
                error: function (data, status, e) {
                    showError(e);
                }
            }
        );
        return false;

    }

    //加载完成
    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        var currentDate = new Date(); //当前日期
        var m = currentDate.getMonth() + 2; //获取当前月份(0-11,0代表1月) 
        var syDate = currentDate.getFullYear() + "-" + m + "-" + currentDate.getDate(); //有效结束时间后一月日期
        for (var i = 0; i < ids.length; i++) {
            var record = myJqGrid.getRecordByKeyValue(ids[i]);
            var ValidTo = record.ValidTo; //有效结束时间

            if (ValidTo != null || ValidTo != "") {

                if (compareTime(syDate + " 00:00:00", ValidTo + " 00:00:00")) {
                    var $id = $(document.getElementById(ids[i]));
                    $id.removeAttr("style");
                    $id.removeClass("ui-widget-content");
                    document.getElementById(ids[i]).style.backgroundColor = "#FFAE4A";
                }
            }
        }
    });
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细',
            width: 800,
            height: 600, 
            prefix: "StockPact",
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
            }
        });
    });

    var payMoneyGrid = new MyGrid({
        renderTo: 'payMoneyGrid'
        , autoWidth: true
        , title: "付款信息"
        , height: 223
        , buttons: buttons2
        , storeURL: '/StockPactChild.mvc/Find'
        , sortByField: 'ID'
        , primaryKey: 'ID'
        , dialogWidth: 800
        , dialogHeight: 700
        , setGridPageSize: 30
        , showPageBar: true
        , altRows: true
        , autoLoad: false
        , initArray: [
            { label: '编号', name: 'ID', index: 'ID', hidden: true }
            , { label: '编号', name: 'StockPactID', index: 'StockPactID', hidden: true }
            , { label: '金额', name: 'PayMoney', index: 'PayMoney' }
            , { label: '其他金额', name: 'OtherMoney', index: 'OtherMoney' }
	        , { label: '付款时间', name: 'PayTime', index: 'PayTime', formatter: 'datetime', editable: true, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt'] } }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
        ]
        , functions: {
            handleReload: function (btn) {
                payMoneyGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                payMoneyGrid.refreshGrid();
            }
        }
    });

    var myJqGridTo = new MyGrid({
        renderTo: 'priceSettingGrid'
        , autoWidth: true
        , title: "材料调价信息"
        , height: 200
        , buttons: buttons1
        , storeURL: opts.priceStoreUrl
        , sortByField: 'ChangeTime'
        , primaryKey: 'ID'
        , dialogWidth: 800
        , dialogHeight: 700
        , setGridPageSize: 30
        , showPageBar: true 
        , altRows: true
        , autoLoad: false
        , editSaveUrl: opts.priceUpdateUrl
        , initArray: [
            { label: '编号', name: 'ID', index: 'ID', hidden: true }
            , { label: '价格编号', name: 'StockPactID', index: 'StockPactID', hidden: true }
            , { label: '材料编码', name: 'StuffID', index: 'StuffID'}
            , { label: '材料名称', name: 'StuffName', index: 'StuffName' }
            , { label: '材料编码', name: 'SpecID', index: 'SpecID', hidden: true }
            , { label: '原料规格', name: 'SpecName', index: 'SpecName', width: 60 }
            //, { label: '订价时间', name: 'ChangeTime', index: 'ChangeTime', formatter: "date", editable: true, width: 90 }
	    , { label: '订价时间', name: 'ChangeTime', index: 'ChangeTime', formatter: 'datetime', editable: true, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
            , { label: '单价', name: 'Price', index: 'Price', editable: true, width: 80 }
            , { label: '是否审核', name: 'AuditStatus', index: 'AuditStatus', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues() } }
            , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 130 }
            , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge'] } }
            , { label: '是否二次审核', name: 'AuditStatus2', index: 'AuditStatus2', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues() } }
            , { label: '二次审核人', name: 'Auditor2', index: 'Auditor2', width: 130 }
            , { label: '二次审核时间', name: 'AuditTime2', index: 'AuditTime2', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge'] } }
            , { label: '调价备注', name: 'Remark', index: 'Remark', width: 180 }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 80 }
        ]
        , functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            },
            handleAdd: function (btn) {
               
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                console.log(keys);
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                $("#StockPactPriceSet_StockPactID").val(Record.ID);
                myJqGridTo.handleAdd({
                    loadFrom: 'MyFormDivDel',
                    btn: btn, 
                    width: 300,
                    height: 300,
                    afterFormLoaded: function () {
                        PriceStuffIDChange();
                    }
                });
            },
            
            //批量审核          
            handleMultiAudit: function (btn) {
                var keys = myJqGridTo.getSelectedKeys();
                if (isEmpty(keys) || keys.length == 0) {
                    showMessage("提示", "请选择至少一条记录进行审核");
                    return;
                }
                var records = myJqGridTo.getSelectedRecords();
                for (var i = 0; i < records.length; i++) {
                    var record = records[i];
                    if (record && (record.AuditStatus == 1 || record.AuditStatus == 'true')) {
                        showMessage(record.ID + '请选择未审核的记录！');
                        return;
                    }
                }
                showConfirm("确认信息", "审核后将不允许删除、修改。是否继续审核？", function (btn) {
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
                                    myJqGridTo.getJqGrid().setCell(keys[i], 'AuditStatus', 1);
                                }
                                myJqGridTo.refreshGrid();
                            }
                        }
                    );

                    $(this).dialog("close");
                });
            },//批量审核  
            //批量取消审核          
            handleMultiAudit_N: function (btn) {
                var keys = myJqGridTo.getSelectedKeys();
                if (isEmpty(keys) || keys.length == 0) {
                    showMessage("提示", "请选择至少一条记录进行审核");
                    return;
                }
                var records = myJqGridTo.getSelectedRecords();
                for (var i = 0; i < records.length; i++) {
                    var record = records[i];
                    if (record && (record.AuditStatus == 0 || record.AuditStatus == 'false')) {
                        showMessage(record.ID + '请选择已审核的记录！');
                        return;
                    }
                    if (record && (record.AuditStatus2 == 1 || record.AuditStatus2 == 'true')) {
                        showMessage(record.ID + '请选择未进行二次审核的记录！');
                        return;
                    }
                }
                showConfirm("确认信息", "审核后将不允许删除、修改。是否继续取消审核？", function (btn) {
                    $(btn.currentTarget).button({ disabled: true });
                    ajaxRequest(
                        opts.MultiAudit_NUrl,
                        {
                            ids: keys
                        },
                        true,
                        function (response) {
                            $(btn.currentTarget).button({ disabled: false });
                            if (response.Result) {
                                for (var i = 0; i < keys.length; ++i) {
                                    myJqGridTo.getJqGrid().setCell(keys[i], 'AuditStatus', 0);
                                }
                                myJqGridTo.refreshGrid();
                            }
                        }
                    );

                    $(this).dialog("close");
                });
            },//批量取消审核  
            //批量二次审核          
            handleMultiAudit2: function (btn) {
                var keys = myJqGridTo.getSelectedKeys();
                if (isEmpty(keys) || keys.length == 0) {
                    showMessage("提示", "请选择至少一条记录进行二次审核");
                    return;
                }
                var records = myJqGridTo.getSelectedRecords();
                for (var i = 0; i < records.length; i++) {
                    var record = records[i];
                    if (record && (record.AuditStatus == 0 || record.AuditStatus == 'false')) {
                        showMessage(record.ID + '请选择已审核的记录！');
                        return;
                    }
                    if (record && (record.AuditStatus2 == 1 || record.AuditStatus2 == 'true')) {
                        showMessage(record.ID + '请选择未二次审核的记录！');
                        return;
                    }
                }
                showConfirm("确认信息", "审核后将不允许删除、修改。是否继续二次审核？", function (btn) {
                    $(btn.currentTarget).button({ disabled: true });
                    ajaxRequest(
                        opts.MultiAudit2Url,
                        {
                            ids: keys
                        },
                        true,
                        function (response) {
                            $(btn.currentTarget).button({ disabled: false });
                            if (response.Result) {
                                for (var i = 0; i < keys.length; ++i) {
                                    myJqGridTo.getJqGrid().setCell(keys[i], 'AuditStatus2', 1);
                                }
                                myJqGridTo.refreshGrid();
                            }
                        }
                    );

                    $(this).dialog("close");
                });
            },//批量二次审核  
            //批量二次取消审核          
            handleMultiAudit2_N: function (btn) {
                var keys = myJqGridTo.getSelectedKeys();
                if (isEmpty(keys) || keys.length == 0) {
                    showMessage("提示", "请选择至少一条记录进行取消二次审核");
                    return;
                }
                var records = myJqGridTo.getSelectedRecords();
                for (var i = 0; i < records.length; i++) {
                    var record = records[i];
                    if (record && (record.AuditStatus2 == 0 || record.AuditStatus2 == 'false')) {
                        showMessage(record.ID + '请选择已二次审核的记录！');
                        return;
                    }
                    if (record && (record.AuditStatus == 0 || record.AuditStatus == 'false')) {
                        showMessage(record.ID + '请选择已审核的记录！');
                        return;
                    }
                }
                showConfirm("确认信息", "审核后将不允许删除、修改。是否继续取消二次审核？", function (btn) {
                    $(btn.currentTarget).button({ disabled: true });
                    ajaxRequest(
                        opts.MultiAudit2_NUrl,
                        {
                            ids: keys
                        },
                        true,
                        function (response) {
                            $(btn.currentTarget).button({ disabled: false });
                            if (response.Result) {
                                for (var i = 0; i < keys.length; ++i) {
                                    myJqGridTo.getJqGrid().setCell(keys[i], 'AuditStatus2', 0);
                                }
                                myJqGridTo.refreshGrid();
                            }
                        }
                    );

                    $(this).dialog("close");
                });
            },//批量取消二次审核  
            handleEdit: function (btn) {
                var Record = myJqGridTo.getSelectedRecord(); 
                myJqGridTo.handleEdit({
                    loadFrom: 'MyFormDivDel',
                    btn: btn,
                    prefix: "StockPactPriceSet",
                    width: 300,
                    height: 300,
                    afterFormLoaded: function () { 
                        $('input[name="StuffName"]').val(Record.StuffName);
                    } 
                });
            }
                , handleDelete: function (btn) {
                    myJqGridTo.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
        }
    });
    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        myJqGridTo.refreshGrid("StockPactID='" + id + "'");
        payMoneyGrid.refreshGrid("StockPactID='" + id + "'");
    });



    $("#DivBitUpdatePrice").dialog({
        modal: true,
        autoOpen: false,
        title: "批量调价即在单价表中的基准价格的基础上进行调整，批量生产调价信息",
        width: 800,
        Height: 600,
        buttons: {
            '确认': function () {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var BitUpdateType = $("#BitStuffUpdatePrice_BitUpdateType").val();
                var BitStuffID = $("#BitStuffUpdatePrice_BitStuffID").val();
                var options1 = $("#BitStuffUpdatePrice_BitStuffID option:selected"); //获取当前选择项.
                var BitStuffName = options1.text(); 
             
                var BitUpdateCnt = $("#BitStuffUpdatePrice_BitUpdateCnt").val();
                var BitUpdateDate = $("#BitStuffUpdatePrice_BitUpdateDate").val();

                
                var type = "";
                if (BitUpdateType == 0) {
                    type = "按百分比";
                }
                if (BitUpdateType == 1) {
                    type = "按数额";
                }
                showConfirm("确认信息", "您确定要批量调整勾选的价格列表吗？价格总数目:" + keys.length + "，调整方式：" + type + "，调整材料：" + BitStuffName + ",调整浮动：" + BitUpdateCnt + "，调价时间：" + BitUpdateDate,
				function () {
				    var requestURL = opts.BitUpdatePriceUrl;
				    ajaxRequest(requestURL, {
				        keys: keys,
				        BitUpdateType: BitUpdateType,
				        BitStuffID: BitStuffID,
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

    $('#StockPact_FootFrom').bind('change', function () {
        $("#StockPact_SpecID").val(1000);
        console.log("121");
    });

    //选择材料关联相对应规格
    PriceStuffIDChange = function () {
        var StuffIDField = myJqGridTo.getFormField("StockPactPriceSet.StuffID");
        StuffIDField.unbind('change');
        StuffIDField.bind('change',
            function () {
                var _stuffid = $("input[name='StockPactPriceSet.StuffID']").val();
                bindSelectData($('#StockPactPriceSet_SpecID'), opts.findSpecUrl, { condition: "StuffID = '" + _stuffid + "'" });
            })
    }
    //选择材料关联相对应规格
    StuffIDChange = function () {
        var StuffIDField = myJqGrid.getFormField("StockPact.StuffID");
        StuffIDField.unbind('change');
        StuffIDField.bind('change',
            function () {
                var _stuffid = $("input[name='StockPact.StuffID']").val();
                bindSelectData($('#StockPact_SpecID'), opts.findSpecUrl, { condition: "StuffID = '" + _stuffid + "'" });
            })
    }
    //选择材料关联相对应规格
    StuffID1Change = function () {
        var StuffIDField = myJqGrid.getFormField("StockPact.StuffID1");
        StuffIDField.unbind('change');
        StuffIDField.bind('change',
            function () {
                var _stuffid = $("input[name='StockPact.StuffID1']").val();
                bindSelectData($('#StockPact_SpecID1'), opts.findSpecUrl, { condition: "StuffID = '" + _stuffid + "'" });
            })
    }
    //选择材料关联相对应规格
    StuffID2Change = function () {
        var StuffIDField = myJqGrid.getFormField("StockPact.StuffID2");
        StuffIDField.unbind('change');
        StuffIDField.bind('change',
            function () {
                var _stuffid = $("input[name='StockPact.StuffID2']").val();
                bindSelectData($('#StockPact_SpecID2'), opts.findSpecUrl, { condition: "StuffID = '" + _stuffid + "'" });
            })
    }
    //选择材料关联相对应规格
    StuffID3Change = function () {
        var StuffIDField = myJqGrid.getFormField("StockPact.StuffID3");
        StuffIDField.unbind('change');
        StuffIDField.bind('change',
            function () {
                var _stuffid = $("input[name='StockPact.StuffID3']").val();
                bindSelectData($('#StockPact_SpecID3'), opts.findSpecUrl, { condition: "StuffID = '" + _stuffid + "'" });
            })
    }
    //选择材料关联相对应规格
    StuffID4Change = function () {
        var StuffIDField = myJqGrid.getFormField("StockPact.StuffID4");
        StuffIDField.unbind('change');
        StuffIDField.bind('change',
            function () {
                var _stuffid = $("input[name='StockPact.StuffID4']").val();
                bindSelectData($('#StockPact_SpecID4'), opts.findSpecUrl, { condition: "StuffID = '" + _stuffid + "'" });
            })
    }
}
