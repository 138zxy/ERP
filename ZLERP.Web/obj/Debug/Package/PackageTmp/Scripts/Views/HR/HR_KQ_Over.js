function HR_KQ_OverIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' 
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true  
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'PersonID', name: 'PersonID', index: 'PersonID', width: 80, hidden: true }
            , { label: '加班单号', name: 'OverNo', index: 'OverNo', width: 120 }   
            , { label: '工号', name: 'HR_Base_Personnel.Code', index: 'HR_Base_Personnel.Code', width: 80 }
            , { label: '员工', name: 'Name', index: 'Name', width: 80 }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentName', index: 'HR_Base_Personnel.DepartmentName', width: 80 }
            , { label: '加班类型', name: 'OverType', index: 'OverType', width: 80 }
            , { label: '开始日期', name: 'StartTime', index: 'StartTime', width: 120, formatter: 'datetime' }
            , { label: '结束日期', name: 'EndTime', index: 'EndTime', width: 120, formatter: 'datetime' }
            , { label: '状态', name: 'Condition', index: 'Condition', width: 80 }
            , { label: '加班/小时', name: 'DayLong', index: 'DayLong', width: 80 }
            , { label: '加班原因', name: 'Reson', index: 'Reson', width: 200 } 
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }  
            , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
            , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', width: 120, formatter: 'datetime' } 
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }
            , { label: "请假附件", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false }
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handleAdd: function (btn) {
                ajaxRequest(options.GenerateOrderNoUrl, { prefix: "O" },
				false,
				function (response) {
				    if (response.Result) { 
				        myJqGrid.handleAdd({
				            loadFrom: 'MyFormDiv',
				            btn: btn,
				            width: 800,
                            height: 400,
				            afterFormLoaded: function () {
				                $("#OverNo").val(response.Data);
				            },
				            postCallBack: function (response) {
				                if (response.Result) {
				                    attachmentUpload(response.Data);
				                }
				            }
				        });
				    }
				}); 
            },
        handleEdit: function (btn) {
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OverNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能修改!");
                    return;
                }
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    width: 800,
                    height: 400,
                    afterFormLoaded: function () { 
                        var RecordTo = myJqGrid.getSelectedRecord();
                        $('input[name="PersonIDText"]').val(RecordTo.Name); 
                        var attDiv = $("#Attachments");
                        attDiv.empty();
                        attDiv.append(RecordTo["Attachments"]);
                        $("a[att-id]").show(); 
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(Record.ID);
                        }
                    }
                });
            }
            , handleDelete: function (btn) {
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OverNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能删除!");
                    return;
                }
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            ToAuditing: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var orderNo = Record.OverNo;
                var Condition = Record.Condition;
                if (Condition != "草稿") {
                    showMessage('提示', "只有草稿状态下的单据才能审核!");
                    return;
                }
                showConfirm("确认信息", "您确定要审核当前加班单:" + orderNo + "？",
				function () {
				    var requestURL = options.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 1,
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
				})
            },
            ToUnAuditing: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
               
                var id = Record.ID;
                var orderNo = Record.OverNo;
                var Condition = Record.Condition;
                if (Condition != "已审核") {
                    showMessage('提示', "只有已审核状态下的单据才能反审!");
                    return;
                }
                showConfirm("确认信息", "您确定要反审当前加班单:" + orderNo + "？",
				function () {
				    var requestURL = options.ChangeConditionUrl;
				    ajaxRequest(requestURL, {
				        type: 2,
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
				})
            }
        }
    });

    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            var record = myJqGrid.getRecordByKeyValue(ids[i]);
            if (record.Condition == "草稿") {
                myJqGrid.getJqGrid().setCell(cl, "Condition", '', { color: 'red' }, '');
            }
        }
    });

    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv', 
            title: '查看详细',
            width: 800,
            height: 400,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
                var RecordTo = myJqGrid.getSelectedRecord();
                $('input[name="PersonIDText"]').val(RecordTo.Name); 
            }
        });
    });



    //上传附件
    function attachmentUpload(objectId) {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload({
            url: options.uploadUrl + "?objectType=HR_KQ_Over&objectId=" + objectId,
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