var myJqGrid
function lab_conwprecordIndexInit(storeUrl) {
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
            , dialogWidth: 500
            , dialogHeight: 300
		    , showPageBar: true
		    , initArray: [
                { label: '试件编号', name: 'ID', index: 'ID', hidden: false, width: 80 }
                , { label: '取样日期', name: 'SamplingDate', index: 'SamplingDate', formatter: 'datetime', width: 120 }
                , { label: '强度等级', name: 'ConStrength', index: 'ConStrength', width: 80 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 280 }
                , { label: '构件部位', name: 'ConsPos', index: 'ConsPos', width: 160 }
                , { label: '取样车号', name: 'CarNo', index: 'CarNo', width: 70 }
                , { label: '生产线号', name: 'ProductLine', index: 'ProductLine', width: 70 }
                , { label: '坍落度(mm)', name: 'Slump', index: 'Slump', width: 80 }
                , { label: '扩展度(mm)', name: 'ExpansionDegree', index: 'ExpansionDegree', width: 80 }
                , { label: '保水性', name: 'WaterRetention', index: 'WaterRetention', width: 80 }
                , { label: '粘聚性', name: 'Cohesiveness', index: 'Cohesiveness', width: 80 }
                , { label: '合格判定', name: 'QualifiedJudgment', index: 'QualifiedJudgment', width: 120 }
                , { label: '取样人', name: 'SamplingMan', index: 'SamplingMan', width: 80 }
                , { label: '运输单号', name: 'ShipDocID', index: 'ShipDocID', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 120 }
                , { label: "创建人", name: "Builder", index: "Builder" }
                , { label: "创建时间", name: "BuildTime", index: "BuildTime", formatter: 'datetime', search: false }
                , { label: "修改人", name: "Modifier", index: "Modifier" }
                , { label: "修改时间", name: "ModifyTime", index: "ModifyTime", formatter: 'datetime', search: false }
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
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                
            }
    });
        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        var ckLab_RecordID;
        myJqGrid.addListeners("rowclick", function (id, record) {
            ckLab_RecordID = id;
            myJqGridItems.refreshGrid("Lab_ConWPRecordId='" + id + "'");
        });

        var myJqGridItems = new MyGrid({
            renderTo: 'MyJqGridR'
            //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: '/Lab_ConWPRecordItems.mvc/Find'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 700
            , dialogHeight: 360
		    , showPageBar: true
             , autoLoad: false
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '试件编号', name: 'Lab_ConWPRecordId', index: 'Lab_ConWPRecordId', width: 80 }
                , { label: '文档类型', name: 'DocType', index: 'DocType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'DocType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('DocType')} }
                , { label: '报告类型', name: 'ReportType', index: 'ReportType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'LabTemplateType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('LabTemplateType')} }
                , { label: '报告URL', name: 'ReportUrl', index: 'ReportUrl', width: 300 }
                , { label: "审核状态", name: "AuditStatus", index: "AuditStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "AuditStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("AuditStatus") }, width: 70, align: "center" }
                , { label: "审核人", name: "Auditor", index: "Auditor", width: 70 }
                , { label: "审核时间", name: "AuditTime", index: "AuditTime", formatter: "datetime", width: 120 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 70 }
                , { label: "创建人", name: "Builder", index: "Builder", width: 70 }
                , { label: "创建时间", name: "BuildTime", index: "BuildTime", formatter: 'datetime', search: false, width: 70 }
                , { label: "修改人", name: "Modifier", index: "Modifier", width: 70 }
                , { label: "修改时间", name: "ModifyTime", index: "ModifyTime", formatter: 'datetime', search: false, width: 70 }

		    ]
            , functions: {
                handleReload: function (btn) {
                    var selectrecord = myJqGrid.getSelectedRecord();
                    console.log(selectrecord.ID);
                    myJqGridItems.reloadGrid("Lab_ConWPRecordId='" + selectrecord.ID + "'");
                },
                handleRefresh: function (btn) {
                    var selectrecord = myJqGrid.getSelectedRecord();
                    console.log(selectrecord.ID);
                    myJqGridItems.reloadGrid("Lab_ConWPRecordId='" + selectrecord.ID + "'");
                },
                handleAdd: function (btn) {
                    var selectrecord = myJqGrid.getSelectedRecord();
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }

                    myJqGridItems.handleAdd({
                        loadFrom: 'MyFormDivItems',
                        btn: btn,
                        afterFormLoaded: function () {
                            console.log(selectrecord.ID);
                            $("#Lab_ConWPRecordItems_Lab_ConWPRecordId").val(selectrecord.ID);
                            $("#Lab_ConWPRecordItems_ReportType").val('');
                            var conditionstr = " ParentID='LabTemplateType' AND FIELD2=2 ";
                            bindSelectData($("#Lab_ConWPRecordItems_ReportType"),
                                '/Dic.mvc/ListData',
                                { textField: 'DicName',
                                    valueField: 'TreeCode',
                                    condition: conditionstr
                                });
                            }, beforeSubmit: function () {
                                var fieldvalue = $('#Lab_ConWPRecordItems_ReportType option:selected').val();
                                var records = myJqGridItems.getAllRecords();
                                var lists = records.filter(function (item, index) { return item.ReportType == fieldvalue });
                                if (lists.length > 0) {
                                    showError('错误', '该实验类型已经存在，请勿重复添加');
                                    return false;
                                }
                                return true;
                            }
                    });
                }
                ,handleEdit: function (btn) {
                    myJqGridItems.handleEdit({
                        loadFrom: 'MyFormDivR',
                        prefix: 'Lab_ConStrengthRecord',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    myJqGridItems.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleEditDoc: function (btn) {
                    var selectedRecord = myJqGridItems.getSelectedRecord();
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    if (selectedRecord.AuditStatus == 1) {
                        showError('提示', '此记录已经审核不能编辑报告！');
                        return;
                    }
                    var selectedReportType = "";
                    $("#MyJqGridR-datagrid").find("tr").each(function () {
                        if ($(this)[0].id == selectedRecord.ID) {
                            var tdArr = $(this).children();
                            selectedReportType = tdArr[4].title;
                            return false;
                        }
                    })
                    var doctitle = selectedRecord.Lab_ConWPRecordId + selectedReportType;
                    var a = selectedRecord.ReportUrl;
                    if (a == "" || a == null || a == undefined) {
                        myJqGridItems.showWindow({
                            title: "报告模板选择",
                            width: 300,
                            height: 150,
                            loadFrom: 'DocTemplateFormDiv',
                            afterLoaded: function () {
                                $("input[name='LabTemplateType']").val('');
                                var labTemplateType = selectedRecord.ReportType;
                                console.log(labTemplateType);
                                getTemplateList(labTemplateType);
                            },
                            buttons: {
                                "确定": function () {
                                    var labTemplateID = $("#LabTemplateType").val(); //$("input[name='LabTemplateID']").val();
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
                                                return ;
                                            }
                                        }
                                        else {
                                            return ;
                                        }
                                        openEdit(labTemplateID, selectedRecord.ID, doctitle, selectedRecord.DocType, selectedRecord.ReportUrl, "tempdate", "labreportCon");

                                    });

                                    $(this).dialog("close");
                                }
                            }
                        })
                    }
                    else {
                        openEdit(selectedRecord.ID, selectedRecord.ID, doctitle, selectedRecord.DocType, selectedRecord.ReportUrl, "labreportCon", "labreportCon");
                    }

                }
                , handleDownload: function (btn) {
                    var keys = myJqGridItems.getSelectedKeys();
                    if (keys.length <= 0) {
                        showError('提示', '未选择任何记录！');
                        return;
                    }
                    showConfirm("确认", "您已选择<font color=red>" + keys.length + "</font>条记录,确定要<font color=green><b>下载报告</b></font>吗？", function () {
                        ajaxRequest(
                        '/Lab_ConWPRecordItems.mvc/DownFile',
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
                ,handleBatchAudit: function (btn) {
                    var keys = myJqGridItems.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    };
                    var records = myJqGridItems.getSelectedRecords();
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
                }
                ,handleCancelAudit: function (btn) {

                    if (!myJqGridItems.isSelectedOnlyOne()) {
                        showError('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = myJqGridItems.getSelectedRecord();
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
            var conditionstr = " LabTemplateType='" + labTemplateType + "'";
            bindSelectData($("#LabTemplateType"),
            '/Lab_Template.mvc/ListData',
            { textField: 'LabTemplateName',
                valueField: 'ID',
                condition: conditionstr
            });
        }
}