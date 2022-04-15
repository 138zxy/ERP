function transpriceIndexInit(opts) {

    function booleanIsAuditSearchValues() {
        return { '': '', 1: '已审', 0: '未审' };
    }
    function booleanIsAuditSelectValues() {
        return { 'true': '已审', 'false': '未审' };
    }
        var transPriceGrid = new MyGrid({
            renderTo: 'TransPriceGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight/2
		    , storeURL: opts.transPriceStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 300
            , dialogHeight: 300
            , autoLoad: true 
            , toolbarRefresh:false
		    , initArray: [
                  { label: '编号', name: 'ID', index: 'ID', width: 50}
                , { name: 'TransportID', hidden: true }
                , { name: 'SupplyID', hidden: true }
                , { name: 'StuffID', hidden: true }
                , { label: '运输厂商', name: 'TransportName', index: 'TransportInfo.SupplyName' }
                , { label: '供应商', name: 'SupplyName', index: 'SupplyInfo.SupplyName' }
                , { label: '原料来源地', name: 'SourceName', index: 'SourceInfo.SupplyName' }
                , { label: '原材料', name: 'StuffName', index: 'StuffInfo.StuffName' }
                , { label: '默认单价', name: 'UnitPrice', index: 'UnitPrice', width:60, align:'right', formatter:'currency'}
                , { label: '计价方式', name: 'CalcType', index: 'CalcType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'TransPriceMethod' }, hidden: true }
                , { label: '备注', name: 'Remark', index: 'Remark' }
		    ] 
            , functions: { 
                handleRefresh: function (btn) {
                    transPriceGrid.reloadGrid();
                },
                handleAdd: function (btn) { 
                    transPriceGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                           
                        }
                    });
                },
                handleEdit: function (btn) {
                    var Record = transPriceGrid.getSelectedRecord(); 
                    transPriceGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: "TransPrice",
                        afterFormLoaded: function () {
                            $('input[name="TransportName"]').val(Record.TransportName);
                            $('input[name="SupplyName"]').val(Record.SupplyName);
                            $('input[name="StuffName"]').val(Record.StuffName);
                        } 
                    });
                }
                , handleDelete: function (btn) {
                    transPriceGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });

        var myJqGridTo = new MyGrid({
            renderTo: 'myJqGridDetial',
            title: '日期价格记录',
            autoWidth: true,
            buttons: buttons1,
            height: gGridHeight/2-100,
            storeURL: opts.DelstoreUrl,
            sortByField: 'ID',
            dialogWidth: 480,
            dialogHeight: 300,
            primaryKey: 'ID',
            setGridPageSize: 30,
            showPageBar: true,
            initArray: [{
                label: 'ID',
                name: 'ID',
                index: 'ID',
                width: 80,
                hidden: true
            },
            {
                label: 'TransPriceID',
                name: 'TransPriceID',
                index: 'TransPriceID',
                width: 80,
                hidden: true
            }
	        , { label: '日期', name: 'PriceDate', index: 'PriceDate', formatter: 'datetime', editable: true, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt'] } }
	        , { label: '默认单价', name: 'UnitPrice', index: 'UnitPrice', width: 60, align: 'right', formatter: 'currency' }
            , { label: '是否审核', name: 'AuditStatus', index: 'AuditStatus', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues() } }
            , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 130 }
            , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge'] } }
            , { label: '是否二次审核', name: 'AuditStatus2', index: 'AuditStatus2', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '1': '已审', '0': '未审' }, stype: 'select', searchoptions: { value: booleanIsAuditSearchValues() }, editable: false, edittype: 'select', editoptions: { value: booleanIsAuditSelectValues() } }
            , { label: '二次审核人', name: 'Auditor2', index: 'Auditor2', width: 130 }
            , { label: '二次审核时间', name: 'AuditTime2', index: 'AuditTime2', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge'] } }
            ,
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		}],
            autoLoad: false,
            functions: {
                handleReload: function (btn) {
                    myJqGridTo.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGridTo.refreshGrid();
                },
                handleAdd: function (btn) {
                    var keys = transPriceGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var Record = transPriceGrid.getSelectedRecord(); 
                    var id = Record.ID;
                    $("#M_TransPrice_TransPriceID").val(Record.ID);
                    myJqGridTo.handleAdd({
                        loadFrom: 'MyFormDivDel',
                        btn: btn,
                        afterFormLoaded: function () {
                           
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
                    myJqGridTo.handleEdit({
                        loadFrom: 'MyFormDivDel',
                        btn: btn,
                        prefix: "M_TransPrice"
                    });
                }
                , handleDelete: function (btn) {
                    myJqGridTo.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
        transPriceGrid.addListeners('rowclick',
	    function (id, record, selBool) { 
	        myJqGridTo.refreshGrid("TransPriceID=" + id);
	    });
}