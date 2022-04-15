function hr_pm_transferIndexInit(storeUrl) {
    //状态列值处理 
    function StateFmt(cellvalue, options, rowObject) {
        if (cellvalue == '草稿') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = cellvalue;
            }
        }
        else {
            var style = "color:red;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = cellvalue;
            }
        }
        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }

    function StateUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }
    var myJqGrid = new MyGrid({
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
            , dialogHeight: 480
            , dialogWidth: 500
            , rowNumbers: true
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '人员编码', name: 'PersonID', index: 'PersonID', width: 80, hidden: true }
                , { label: '单据号', name: 'TransferNo', index: 'TransferNo', hidden: false, width: 70 }
                , { label: '人员名称', name: 'PersonName', index: 'PersonName', width: 80 }
                , { label: '状态', name: 'State', index: 'State', width: 80, formatter: StateFmt, unformat: StateUnFmt }
                , { label: '部门调动', name: 'IsDept', index: 'IsDept', formatter: booleanFmt, unformat: booleanUnFmt, width: 50 }
                , { label: '原部门编码', name: 'OldDepartmentID', index: 'OldDepartmentID', width: 80 }
                , { label: '原部门', name: 'OldDepartmentName', index: 'OldDepartmentName', width: 80 }
                , { label: '新部门ID', name: 'NewDepartmentID', index: 'NewDepartmentID', width: 80, hidden: true }
                , { label: '新部门', name: 'NewDepartmentName', index: 'NewDepartmentName', width: 80 }
                , { label: '职务调动', name: 'IsPosition', index: 'IsPosition', formatter: booleanFmt, unformat: booleanUnFmt, width: 50 }
                , { label: '原职务ID', name: 'OldPositionID', index: 'OldPositionID', width: 80, hidden: true }
                , { label: '原职务', name: 'OldPosition', index: 'OldPosition', width: 80 }
                , { label: '新职务ID', name: 'NewPositionID', index: 'NewPositionID', width: 80, hidden: true }
                , { label: '新职务', name: 'NewPosition', index: 'NewPosition', width: 80 }
                , { label: '调动岗位', name: 'IsPost', index: 'IsPost', formatter: booleanFmt, unformat: booleanUnFmt, width: 50 }
                , { label: '原岗位编码', name: 'OldPostID', index: 'OldPostID', width: 80, hidden: true }
                , { label: '新岗位名称', name: 'OldPostName', index: 'OldPostName', width: 80 }
                , { label: '新岗位编码', name: 'NewPostID', index: 'NewPostID', width: 80, hidden: true }
                , { label: '新岗位名称', name: 'NewPostName', index: 'NewPostName', width: 80 }
                , { label: '调动时间', name: 'TransferDate', index: 'TransferDate', formatter: 'date', width: 80 }
                , { label: '经办人', name: 'Operator', index: 'Operator', width: 80 }
                , { label: '原因', name: 'Reason', index: 'Reason', width: 80 }
                , { label: '删除标识', name: 'Delflag', index: 'Delflag', formatter: booleanFmt, unformat: booleanUnFmt, width: 80, hidden: true }
                
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', width: 120, formatter: 'datetime' }
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
                    ajaxRequest('/HR_PM_Transfer.mvc/GenerateOrderNo', { prefix: 'HPT' },
				    false,
				    function (response) {
				        if (response.Result) {
				            myJqGrid.handleAdd({
				                loadFrom: 'MyFormDiv',
				                btn: btn,
				                afterFormLoaded: function () {
				                    $("#TransferNo").val(response.Data);
				                    PersonChange();

				                }, beforeSubmit: function () {
				                    $("#NewDepartmentName").val($("input=[name='NewDepartmentID_text']").val());//部门名称
				                    return true;
				                },
				                postCallBack: function (response) {
				                    if (response.Result) {

				                    }
				                }
				            });
				        }
				    });
                },
                handleEdit: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();
                    var id = Record.ID;
                    var State = Record.State;
                    if (State == "已审核") {
                        showError('提示', "已审核单据不能修改!");
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            PersonChange();
                            myJqGrid.setFormFieldValue('NewDepartmentID', Record.NewDepartmentID);
                            myJqGrid.setFormFieldValue('NewDepartmentID_text', Record.NewDepartmentName);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();
                    var id = Record.ID;
                    var orderNo = Record.TransferNo;
                    var State = Record.State;
                    if (State == "已审核") {
                        showError('提示', "已审核单据不能删除!");
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                ChangeState: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();
                    var id = Record.ID;
                    var orderNo = Record.TransferNo;
                    var State = Record.State;
                    if (State != "草稿") {
                        showError('提示', "只有草稿状态下的单据才能审核!");
                        return;
                    }
                    showConfirm("确认信息", "您确定要审核当前调动单:<span style='color:red;'>" + orderNo + "</span>吗？",
				    function () {
				        var requestURL = '/HR_PM_Transfer.mvc/ChangeState';
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
					            showError('提示', response.Message);
					        }
					    })
				    })
                },
                UnChangeState: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();

                    var id = Record.ID;
                    var orderNo = Record.TransferNo;
                    var State = Record.State;
                    if (State != "已审核") {
                        showError('提示', "只有已审核状态下的单据才能反审!");
                        return;
                    }
                    showConfirm("确认信息", "您确定要反审当前离职单:<span style='color:red;'>" + orderNo + "</span>吗？",
				    function () {
				        var requestURL = '/HR_PM_Transfer.mvc/ChangeState';
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
					            showError('提示', response.Message);
					        }
					    })
				    })
                }
            }
    });
    //下拉选择人员的时候
    PersonChange = function () {
        var PersonIDField = $('input[name="PersonID"]'); //myJqGrid.getFormField("ID"); 
        PersonIDField.unbind('change');
        PersonIDField.bind('change',
	function () {
	    var personId = PersonIDField.val();
	    var requestURL = "/HR_Base_Personnel.mvc/Get";
	    ajaxRequest(requestURL, {
	        id: personId
	    },
		false,
		function (response) {
		    if (!!response.Result) {
		        var data = response.Data;
		        $("#PersonName").val(data.Name);
		        $("#OldDepartmentName").val(data.DepartmentName);
		        $("#OldDepartmentID").val(data.DepartmentID);

		        $("#OldPosition").val(data.PositionType);
		        $("#OldPositionID").val(data.PositionType);

		        $("#OldPostName").val(data.Post);
		        $("#OldPostID").val(data.Post);

		    } else {
		        showMessage('提示', response.Message);
		    }
		});

	});
    }
}