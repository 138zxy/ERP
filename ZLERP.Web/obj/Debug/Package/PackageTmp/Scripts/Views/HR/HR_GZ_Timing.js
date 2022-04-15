function HR_GZ_TimingIndexInit(options) {
    $('#Department').height(gGridHeight + 80);
    //-----------------------------树状开始-----------------------------------
    var treeSettings = {
        check: {
            enable: false
        },
        view: {
            selectedMulti: false
        },
        data: {
            simpleData: {
                enable: true
            },
            key: {
                title: 'title'
            }
        },
        async: {
            enable: true,
            url: options.deptTreeUrl,
            autoParam: ["id", "name", "level"]
        },
        callback: {
            onAsyncError: function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                handleServerError(XMLHttpRequest, textStatus, errorThrown);
            },
            onAsyncSuccess: zTreeOnAsyncSuccess,
            onClick: zTreeOnCheck
        }
    };

    var scLimitList = $.fn.zTree.init($('#Department'), treeSettings);
    scLimitList.expandAll(true);

    function zTreeOnAsyncSuccess(event, treeId, treeNode, msg) {
        var treeObj = $.fn.zTree.getZTreeObj(treeId);
        var nodes = treeObj.getNodes();
        if (nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                treeObj.expandNode(nodes[i], true, false, false);
            }
        }
    }
    //选择树节点事件
    function zTreeOnCheck(event, treeId, treeNode, clickFlag) {
        var treeObj = $.fn.zTree.getZTreeObj("Department");
        var node = treeObj.getSelectedNodes(); //点击节点后 获取节点数据
        if (node.length > 0) {
            var str = '';
            str = getAllChildrenNodes(treeNode, str);
            str = str + ',' + treeNode.id; // 加上被选择节点自己
            var ids = str.substring(1, str.length); // 去掉最前面的逗号
            var idsArray = ids.split(','); // 得到所有节点ID 的数组
            var length = idsArray.length; // 得到节点总数量

            myJqGrid.refreshGrid("HR_Base_Personnel.DepartmentID IN(" + idsArray + ")");
            typeno = treeNode.id; typename = treeNode.name;
        }
    }
    //递归，获取所有子节点
    function getAllChildrenNodes(treeNode, result) {
        if (treeNode.isParent) {
            var childrenNodes = treeNode.children;
            if (childrenNodes) {
                for (var i = 0; i < childrenNodes.length; i++) {
                    result += ',' + childrenNodes[i].id;
                    result = getAllChildrenNodes(childrenNodes[i], result);
                }
            }
        }
        return result;
    }
    //-----------------------------树状结束-----------------------------------
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
        , groupField: 'HR_GZ_TimingSet.Name'
        , groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        }
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'PersonID', name: 'PersonID', index: 'PersonID', width: 80, hidden: true }
            , { label: '项目ID', name: 'SetID', index: 'SetID', width: 80, hidden: true } 
            , { label: '工号', name: 'HR_Base_Personnel.Code', index: 'HR_Base_Personnel.Code', width: 80 }
            , { label: '员工', name: 'Name', index: 'Name', width: 80 }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentName', index: 'HR_Base_Personnel.DepartmentName', width: 80 }
            , { label: '项目名称', name: 'HR_GZ_TimingSet.Name', index: 'HR_GZ_TimingSet.Name', width: 80 }
            , { label: '计时单价', name: 'UnitPrice', index: 'UnitPrice', width: 80, search: false }
            , { label: '小时', name: 'Quantity', index: 'Quantity', width: 80, search: false }
            , { label: '总价', name: 'AllMoney', index: 'AllMoney', width: 80, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '工作日', name: 'WorkDate', index: 'WorkDate', width: 120, formatter: 'date' }  
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }   
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }
            , { label: "附件", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false }
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
                    btn: btn,
                    width: 500,
                    height: 400,
                    afterFormLoaded: function () { 
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(response.Data);
                        }
                    }
                });
            },
        handleEdit: function (btn) { 
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    width: 500,
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
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
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
            url: options.uploadUrl + "?objectType=HR_KQ_Leave&objectId=" + objectId,
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
    $("#SetID").change(function () {
        var id = $("#SetID").val();
        var requestURL = options.SetUrl;
        ajaxRequest(requestURL, { id: id }, false, function (response) {
            if (response.Result) {
                var Quantity = $("#UnitPrice").val(response.Data.UnitPrice);
            }
        });
    });
    $("#UnitPrice").change(function () {
        var UnitPrice = $("#UnitPrice").val();
        var Quantity = $("#Quantity").val();
        if (UnitPrice >= 0 && Quantity >= 0) {
            var all = UnitPrice * Quantity;
            $("#AllMoney").val(all); 
        }
    });
    $("#Quantity").change(function () { 
        var UnitPrice = $("#UnitPrice").val();
        var Quantity = $("#Quantity").val(); 
        var all = UnitPrice * Quantity;
        if (UnitPrice >= 0 && Quantity >= 0) {
            var all = UnitPrice * Quantity;
            $("#AllMoney").val(all); 
        }
    });
    //设置日期列为日期范围搜索
    myJqGrid.setRangeSearch("WorkDate"); 
}