var myJqGrid;
function lab_recordIndexInit(storeUrl) {
    myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , advancedSearch: true
            , groupField: 'ReportType'
            , groupingView: { groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], groupSummary: [false], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: '记录id', name: 'ID', index: 'ID', hidden: true }
                , { label: '样品编号', name: 'ShowpeieNo', index: 'ShowpeieNo', width: 80 }
                , { label: '日期', name: 'Date', index: 'Date', formatter: 'datetime', width: 120 }
                , { label: "审核状态", name: "AuditStatus", index: "AuditStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "AuditStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("AuditStatus") }, width: 60, align: "center" }
                , { label: '运输车号', name: 'Carno', index: 'Carno', width: 80 }
                , { label: '材料大类id', name: 'FinalStuffTypeID', index: 'FinalStuffTypeID', hidden: true }
                , { label: '材料入库id', name: 'stuffinid', index: 'stuffinid', hidden: true }
                , { label: '材料id', name: 'Stuffid', index: 'Stuffid', hidden: true }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', width: 80 }
                , { label: '生产厂家id', name: 'Supplyid', index: 'Supplyid', hidden: true }
                , { label: '生产厂家', name: 'SupplyName', index: 'SupplyName', width: 100 }
                , { label: '单车车重(T)', name: 'Weight', index: 'Weight', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 70, hidden: true }
                , { label: '累计重量(T)', name: 'SumWeight', index: 'SumWeight', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 60, hidden: true }
                , { label: '罐编号', name: 'Siloid', index: 'Siloid', hidden: true }
                , { label: '筒仓', name: 'SiloName', index: 'SiloName', width: 70 }
                , { label: '收料人员', name: 'InMan', index: 'InMan', width: 70 }
                , { label: '报告URL', name: 'ReportUrl', index: 'ReportUrl', width: 300 }
                , { label: '文档类型', name: 'DocType', index: 'DocType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'DocType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('DocType')} }
                , { label: '报告类型', name: 'ReportType', index: 'ReportType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'LabTemplateType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('LabTemplateType')} }
                , { label: "审核人", name: "Auditor", index: "Auditor", width: 70 }
                , { label: "审核时间", name: "AuditTime", index: "AuditTime", formatter: "datetime", width: 120 }
                , { label: '发车时间', name: 'StartDate', index: 'StartDate', formatter: 'date', width: 120 }
                , { label: '到站时间', name: 'EndDate', index: 'EndDate', formatter: 'date', width: 120 }
                , { label: '铅封是否完好', name: 'IsWhole', index: 'IsUsed', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                , { label: '备注', name: 'Remark', index: 'Remark' }
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
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    var selectedRecord = myJqGrid.getSelectedRecord();
                    if (selectedRecord.AuditStatus == 1) {
                        showError('提示', '此记录已经审核！');
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    var selectedRecord = myJqGrid.getSelectedRecord();
                    if (selectedRecord.AuditStatus == 1) {
                        showError('提示', '此记录已经审核！');
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }, handleEditDoc: function (btn) {
                    var selectedRecord = myJqGrid.getSelectedRecord();
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showError('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    if (selectedRecord.AuditStatus == 1) {
                        showError('提示', '此记录已经审核不能编辑报告！');
                        return;
                    }
                    var doctitle = selectedRecord.ShowpeieNo + selectedRecord.StuffName;
                    var a = selectedRecord.ReportUrl;
                    if (a == "" || a == null || a == undefined) {
                        myJqGrid.showWindow({
                            title: "报告模板选择",
                            width: 300,
                            height: 150,
                            loadFrom: 'DocTemplateFormDiv',
                            afterLoaded: function () {
                                $("input[name='ReportType']").val('');
                                var labTemplateType = selectedRecord.ReportType;
                                getTemplateList(labTemplateType);
                            },
                            buttons: {
                                "确定": function () {
                                    var labTemplateID = $("#ReportType").val(); //$("input[name='LabTemplateID']").val();
                                    if (isEmpty(labTemplateID)) {
                                        showError('提示', "请选择模板!");
                                        return false;
                                    }

                                    //判断模板对应文件是否存在
                                    var requestUrl = '/Lab_Template.mvc/IsExistTemplateFile';
                                    var postParams = { templateid: labTemplateID };
                                    ajaxRequest(requestUrl, postParams, true, function (response) {
                                        if (response) {
                                            if (!response.Data) {
                                                return;
                                            }
                                        }
                                        else {
                                            return;
                                        }
                                        openEdit(labTemplateID, selectedRecord.ID, doctitle, selectedRecord.DocType, selectedRecord.ReportUrl, "tempdate", "labreport");

                                    });

                                    $(this).dialog("close");
                                }
                            }
                        })
                    }
                    else {
                        openEdit(selectedRecord.ID, selectedRecord.ID, doctitle, selectedRecord.DocType, selectedRecord.ReportUrl, "labreport", "labreport");
                    }

                }
                , handleDownload: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length <= 0) {
                        showError('提示', '未选择任何记录！');
                        return;
                    }
                    showConfirm("确认", "您已选择<font color=red>" + keys.length + "</font>条记录,确定要<font color=green><b>下载报告</b></font>吗？", function () {
                        ajaxRequest(
                        '/Lab_Record.mvc/DownFile',
                        {
                            ids: keys
                        },
                        true,
                        function (response) {
                            if (response.Result) {
                                window.location.target = "_blank";
                                var filename = '/Content/Files/LabReport/DownFile.zip';
                                window.open(filename, '_blank');
                            }
                        }
                    );
                    });

                }
                , FindCustRecord: function (btn) {
                    myJqGrid.showWindow({
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
                                condition = "Date between '" + BeginTime + "' and '" + EndTime + "'";
                                myJqGrid.refreshGrid(condition);
                                var checked = $("#sIsAutoClose")[0].checked;
                                if (checked) {
                                    $(this).dialog('close');
                                }
                            }
                        }
                    });
                },
                handleBatchAudit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    };
                    var records = myJqGrid.getSelectedRecords();
                    for (var i = 0; i < records.length; i++) {
                        var auditValue = records[i].AuditStatus;
                        if (auditValue == 1) {
                            showError('提示', '请选择未审核的合同！');
                            return;
                        }
                        var a = records[i].ReportUrl;
                        if (a == "" || a == null || a == undefined) {
                            showError("提示", "未编写报告！");
                            return;
                        }
                    }

                    showConfirm("确认审核", "您确定要<font color=red><b>审核操作</b></font>吗?", function () {
                        var requestUrl = btn.data.Url;
                        var postParams = { ids: keys, auditStatus: 1 };
                        ajaxRequest(requestUrl, postParams, true, function () {
                            myJqGrid.reloadGrid();
                        });
                        $(this).dialog("close");
                    });
                },
                handleCancelAudit: function (btn) {

                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showError('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = myJqGrid.getSelectedRecord();
                    var auditValue = selectedRecord.AuditStatus;
                    if (auditValue == 0) {
                        showError('提示', '该记录正处于未审核状态！');
                        return;
                    } else {
                        //确认操作
                        showConfirm("确认信息", "您确定要<font color=green><b>取消审核</b></font>吗？", function () {
                            ajaxRequest(
                                btn.data.Url,
                                {
                                    lrID: selectedRecord.ID,
                                    auditStatus: 0
                                },
                                true,
                                function () {
                                    myJqGrid.reloadGrid();
                                }
                            );
                            $(this).dialog("close");
                        });
                    }

                }

            }
    });
    //模板ID 试验记录ID 文档名 文档类型 文档URL 打开文档类型 保存文档类型
    function openEdit(tid, rid, doctitle, doctype, fileurl, openDataType, saveType) {
        var iHeight = (screen.availHeight - 20);
        var iWidth = (screen.availWidth - 5);
        var iTop = 0; var iLeft = 0; 
        var url = "/WebOffice/DocEdit.aspx?id=" + tid + "&rid=" + rid + "&docTitle=" + escape(doctitle) + "&docType=" + doctype + "&dataType=" + openDataType + "&fileurl=" + fileurl + "&saveType=" + saveType + "&height=" + (iHeight * 1 - 75);
        window.open(url, 'newwindow', 'height=' + iHeight + ', width=' + iWidth + ', ,top=' + iTop + ',left=' + iLeft + ', toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no,titlebar=no');
    }

    //加载记录对应的模板列表
    function getTemplateList(labTemplateType) {
        var conditionstr = " LabTemplateType='" + labTemplateType+"'";
        bindSelectData($("#ReportType"),
            '/Lab_Template.mvc/ListData',
            { textField: 'LabTemplateName',
              valueField: 'ID',
              condition: conditionstr
            });
    }

}