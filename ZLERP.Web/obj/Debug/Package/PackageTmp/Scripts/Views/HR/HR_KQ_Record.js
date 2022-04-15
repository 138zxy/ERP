function HR_KQ_RecordIndexInit(options) {
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
            , { label: '工号', name: 'HR_Base_Personnel.Code', index: 'HR_Base_Personnel.Code', width: 80 }
            , { label: '员工', name: 'Name', index: 'Name', width: 80 }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentName', index: 'HR_Base_Personnel.DepartmentName', width: 80 }
            , { label: '自动签卡', name: 'AutoCheck', index: 'AutoCheck', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '签卡时间', name: 'CheckTime', index: 'CheckTime', width: 120, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
            , { label: '计算机名', name: 'ComputeName', index: 'ComputeName', width: 80 }
            , { label: '地址', name: 'IP', index: 'IP', width: 80 }
            , { label: 'MAC地址', name: 'MacAdress', index: 'MacAdress', width: 80 }
            , { label: '数据来源', name: 'DataSource', index: 'DataSource', width: 80 }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' } 
            , { label: '最后修改人', name: 'Modifier', index: 'Modifier', width: 80 }
            , { label: '最后修改时间', name: 'ModifyTime', index: 'ModifyTime', width: 120, formatter: 'datetime' }  
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
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDiv',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {
                        var RecordTo = myJqGrid.getSelectedRecord();
                        $('input[name="PersonIDText"]').val(RecordTo.Name);
                    }
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
            , ToExcel: function (btn) {
                myJqGrid.showWindow({
                    loadFrom: 'MyFormDiv1',
                    btn: btn,
                    afterFormLoaded: function () {

                    }, buttons: {
                        "立即导入": function () {
                            attachmentUpload();
                            $(this).dialog("close");
                        },
                        "关闭": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }
        }
    });

    //上传附件
    function attachmentUpload() {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload({
            url: options.uploadUrl,
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
                    myJqGrid.reloadGrid("DataSource ='Excel导入'");
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
    //设置日期列为日期范围搜索
    myJqGrid.setRangeSearch("CheckTime");

}