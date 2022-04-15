function hr_pm_dimissionIndexInit(storeUrl) {
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
            , rowNumbers: true
		    , showPageBar: true
             , dialogHeight: 350
            , dialogWidth: 500
            , storeCondition: "Delflag=0"
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 70 }
                , { label: '员工编码', name: 'PersonID', index: 'PersonID', hidden: true }
                , { label: '单据号', name: 'DimissionNo', index: 'DimissionNo', hidden: false, width: 70 }
                , { label: '员工名称', name: 'PersonName', index: 'PersonName', width: 70 }
                , { label: '部门编码', name: 'DepartmentID', index: 'DepartmentID', hidden: true }
                , { label: '部门名称', name: 'DepartmentName', index: 'DepartmentName', width: 70 }
                , { label: '离职类型', name: 'DimissionType', index: 'DimissionType', width: 70 }
                
                , { label: '离职时间', name: 'DimissionDate', index: 'DimissionDate', formatter: 'date', width: 70 }
                , { label: '离职工资结算', name: 'WageAmount', index: 'WageAmount', width: 90 }
                , { label: '离职费用', name: 'Amount', index: 'Amount', width: 70 }
                 , { label: '状态', name: 'State', index: 'State', width: 80, formatter: StateFmt, unformat: StateUnFmt }
                , { label: '备注', name: 'Meno', index: 'Meno', width: 170 }
                , { label: '删除标识', name: 'Delflag', index: 'Delflag', width: 70, formatter: booleanFmt, unformat: booleanUnFmt, hidden: true }
               
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', width: 120, formatter: 'datetime' }
                , { label: '离职原因', name: 'DimissionReason', index: 'DimissionReason', width: 380 }
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
                    ajaxRequest('/HR_PM_Dimission.mvc/GenerateOrderNo', { prefix: 'HPD' },
				    false,
				    function (response) {
				        if (response.Result) {
				            myJqGrid.handleAdd({
				                loadFrom: 'MyFormDiv',
				                btn: btn,
				                afterFormLoaded: function () {
				                    $("#DimissionNo").val(response.Data);
				                    PersonChange();
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
                        }
                    });
                }
                , handleDelete: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();
                    var id = Record.ID;
                    var orderNo = Record.DimissionNo;
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
                    var orderNo = Record.DimissionNo;
                    var State = Record.State;
                    if (State != "草稿") {
                        showError('提示', "只有草稿状态下的单据才能审核!");
                        return;
                    }
                    showConfirm("确认信息", "您确定要审核当前离职单:<span style='color:red;'>" + orderNo + "</span>吗？",
				    function () {
				        var requestURL = '/HR_PM_Dimission.mvc/ChangeState';
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
                    var orderNo = Record.DimissionNo;
                    var State = Record.State;
                    if (State != "已审核") {
                        showError('提示', "只有已审核状态下的单据才能反审!");
                        return;
                    }
                    showConfirm("确认信息", "您确定要反审当前离职单:<span style='color:red;'>" + orderNo + "</span>吗？",
				    function () {
				        var requestURL = '/HR_PM_Dimission.mvc/ChangeState';
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
			        $("#PersonPym").val(data.PyCode);
			        $("#DepartmentName").val(data.DepartmentName);
			        $("#DepartmentID").val(data.DepartmentID);

			    } else {
			        showMessage('提示', response.Message);
			    }
		    });

	    });
    }
}