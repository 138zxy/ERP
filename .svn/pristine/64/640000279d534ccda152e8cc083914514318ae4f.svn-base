var myJqGrid;
function lab_templateIndexInit(storeUrl) {
    
    myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            //, width: '100%'
            , title: '模板列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'LabTemplateType'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupField: "LabTemplateType"
            , advancedSearch: true
            , groupingView: { groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['asc'], groupSummary: [false], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: '模板编码', name: 'ID', index: 'ID', width: 80 }
                , { label: '模板名称', name: 'LabTemplateName', index: 'LabTemplateName', width: 120 }
                , { label: '试验类型', name: 'TemplateFlag', index: 'TemplateFlag', width: 70, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'LabType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('LabType')} }               
                , { label: '模板路径', name: 'LabTemplatePath', index: 'LabTemplatePath', width: 400 }
                , { label: '文档类型', name: 'DocType', index: 'DocType', width: 70, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'DocType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('DocType')} }
                , { label: '备注', name: 'Meno', index: 'Meno', width: 160 }
                , { label: '报告类型', name: 'LabTemplateType', index: 'LabTemplateType', hidden: true, width: 90, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'LabTemplateType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('LabTemplateType')} }
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
                        afterFormLoaded: function () {
                            bindLabTemplateTypeChange();
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            bindLabTemplateTypeChange();
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleEditDoc: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = myJqGrid.getSelectedRecord();
                    var iHeight = (screen.availHeight - 0);
                    var iWidth = (screen.availWidth - 0);
                    var iTop = 0;
                    var iLeft = 0;
                    window.open("/WebOffice/DocEdit.aspx?id=" + selectedRecord.ID + "&docTitle=" + selectedRecord.LabTemplateName + "&docType=" + selectedRecord.DocType + "&fileurl=" + selectedRecord.LabTemplatePath + "&dataType=tempdate", 'newwindow', 'height=' + iHeight + ', width=' + iWidth + ', ,top=' + iTop + ',left=' + iLeft + ', toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no,titlebar=no');
                }
                , handleDownload: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length <= 0) {
                        showError('提示', '未选择任何记录！');
                        return;
                    }
                    showConfirm("确认", "您已选择<font color=red>" + keys.length + "</font>条记录,确定要<font color=green><b>要下载模板</b></font>吗？", function () {
                        ajaxRequest(
                        '/Lab_Template.mvc/DownFile',
                        {
                            ids: keys
                        },
                        true,
                        function (response) {
                            if (response.Result) {
                                window.location.target = "_blank";
                                var filename = '/Content/Files/LabTemplate/DownFile.zip';
                                //window.location.href = filename;
                                window.open(filename, '_blank');
                            }
                        }
                    );
                    });
                }

            }
    });

    var myJqGridR = new MyGrid({
        renderTo: 'MyJqGridR'
        //, width: '100%'
            , title: '模板动态数据项配置'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: '/Lab_TemplateDataConfig.mvc/Find'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '模板编码', name: 'LabTemplateID', index: 'LabTemplateID', width: 70, hidden: true }
                , { label: '取值字段', name: 'Field', index: 'Field', width: 80 }
                , { label: '字段名称', name: 'FieldName', index: 'FieldName', width: 80 }
                , { label: '位置列', name: 'ExcelColumun', index: 'ExcelColumun', width: 40, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ExcelColumun' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('ExcelColumun')} }
                , { label: '位置行', name: 'ExcelRow', index: 'ExcelRow', width: 40, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ExcelRow' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('ExcelRow')} }
                , { label: '备注', name: 'Meno', index: 'Meno' }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myJqGridR.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGridR.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条模板记录进行操作！");
                        return;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    myJqGridR.handleAdd({
                        loadFrom: 'MyFormDivR',
                        btn: btn,
                        width: 400,
                        height: 350,
                        afterFormLoaded: function () {
                            getLabColumnList(data.TemplateFlag);
                            bindFieldChange();

                            myJqGridR.setFormFieldValue("LabTemplateID", data.ID);
                            myJqGridR.setFormFieldReadOnly("LabTemplateID", true);
                        }, beforeSubmit: function () {
                            $("#ExcelRow").val(myJqGridR.getFormField("Lab_TemplateDataConfig.ExcelRow").val());
                            $("#ExcelColumun").val(myJqGridR.getFormField("Lab_TemplateDataConfig.ExcelColumun").val());
                            return true;
                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGridR.getSelectedRecord();
                    myJqGridR.handleEdit({
                        loadFrom: 'MyFormDivR',
                        prefix: 'Lab_TemplateDataConfig',
                        btn: btn,
                        width: 400,
                        height: 350,
                        afterFormLoaded: function () {
                            var data0 = myJqGrid.getSelectedRecord();
                            getLabColumnList(data0.TemplateFlag); 
                            bindFieldChange();

                            myJqGridR.getFormField("ID").val(data.ID);
                            myJqGridR.getFormField("LabTemplateID").val(data.LabTemplateID);
                            myJqGridR.getFormField("Field").val(data.Field);
                            myJqGridR.getFormField("FieldName").val(data.FieldName);
                            myJqGridR.getFormField("Lab_TemplateDataConfig.ExcelRow").val(data.ExcelRow);
                            myJqGridR.getFormField("Lab_TemplateDataConfig.ExcelColumun").val(data.ExcelColumun);
                            myJqGridR.getFormField("Meno").val(data.Meno);


                            $("#ExcelRow").val(data.ExcelRow);
                            $("#ExcelColumun").val(data.ExcelColumun);
                        }, beforeSubmit: function () {
                            $("#ExcelRow").val(myJqGridR.getFormField("Lab_TemplateDataConfig.ExcelRow").val());
                            $("#ExcelColumun").val(myJqGridR.getFormField("Lab_TemplateDataConfig.ExcelColumun").val());
                            return true;
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGridR.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    //选择模板
    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        myJqGridR.getJqGrid().setCaption("模板动态数据项配置：" + record.LabTemplateName +"("+ record.ID+")");
        myJqGridR.refreshGrid("LabTemplateID='" + id + "'");
    });

    //选择试验类型事件
    function bindLabTemplateTypeChange() {
        myJqGrid.getFormField("LabTemplateType").unbind("change").bind("change", function () {
            var svalue = $('#LabTemplateType option:selected').val(); 
            if (svalue) {
            }
        });
    }

    //加载记录对应的模板列表
    function getLabColumnList(flag) {     
        bindSelectData($("#FieldName"),
            '/Lab_TemplateDataConfig.mvc/GetLabRecordListData',
            { flag: flag,
                textField: 'textField',
                valueField: 'valueField'
            });
    }
    //选择字段名称事件
    function bindFieldChange() {
        myJqGridR.getFormField("FieldName").unbind("change").bind("change", function () {
            var fieldvalue = $('#FieldName option:selected').val();
            if (fieldvalue) {
                $("#Field").val(fieldvalue);
            }
        });
    }

}
