function PrintReportIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'PrintReportGrid',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: options.storeUrl,
        editSaveUrl: options.editUrl,
        sortByField: 'ReportName',
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
		    label: '编号',
		    name: 'ReportNo',
		    index: 'ReportNo',
		    width: 120
		},
		{
		    label: '报表名称',
		    name: 'ReportName',
		    index: 'ReportName',
		    width: 80
		},
		{
		    label: '报表类型',
		    name: 'ReportType',
		    index: 'ReportType',
		    width: 80
		},
		{
		    label: '数据源类型',
		    name: 'SoucreType',
		    index: 'SoucreType',
		    width: 60
		},
		{
		    label: '数据源',
		    name: 'SqlData',
		    index: 'SqlData',
		    editable: true,
		    width: 500
		},
		{
		    label: '报表文件',
		    name: 'FilePath',
		    index: 'FilePath',
		    width: 150
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
        { label: '操作', name: 'act', index: 'act', width: 90, sortable: false, align: "center" }
        ],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'PrintReportForm',
                    btn: btn,
                    width: 600,
                    height: 400
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'PrintReportForm',
                    btn: btn,
                    width: 600,
                    height: 400
                });
            },
            handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            Copy: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据进行复制!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var requestURL = options.CopyUrl;
                ajaxRequest(requestURL, {
                    id: id
                },
					false,
					function (response) {
					    if (!!response.Result) {
					        showMessage('提示', '操作成功！');
					        myJqGrid.refreshGrid();
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
            },
            Upload: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据进行上传!");
                    return;
                }
                $("#uploadFilediv").dialog("open");
            }
        }
    });

    myJqGrid.addListeners("gridComplete", function () {
        var records = myJqGrid.getAllRecords();
        var rid, buildtime, report;
         
        for (var i = 0; i < records.length; i++) {
            rid = records[i].ID;
            report = records[i].ReportNo;
            be = "<input class='identityButton'  type='button' value='设计样式' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDesign(" + rid + ",'" + report + "');\"  />";
            myJqGrid.getJqGrid().jqGrid('setRowData', rid, { act: be });
        }
    });

    window.handleDesign = function (id, report) { 
        var url = "/GridReport/DesignReport.aspx?report=" + report + "";
        window.open(url, "_blank");
    }

    $("#uploadFilediv").dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        Height: 500,
        buttons: {
            '确认': function () {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
               attachmentUpload(keys[0])
                $(this).dialog('close');

            },
            '取消': function () {
                $(this).dialog('close');
            }
        },
        position: ["center", 100]
    });
    //上传附件
    function attachmentUpload(objectId) {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload({
            url: options.UploadUrl + "?id=" + objectId,
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
}