function HR_GZ_SalariesSetIndexInit(options) {
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
        , editSaveUrl: options.editSaveUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'PersonID', name: 'PersonID', index: 'PersonID', width: 80, hidden: true }
            , { label: '项目ID', name: 'SetID', index: 'SetID', width: 80, hidden: true }
            , { label: '工号', name: 'HR_Base_Personnel.Code', index: 'HR_Base_Personnel.Code', width: 80 }
            , { label: '员工', name: 'Name', index: 'Name', width: 80 }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentName', index: 'HR_Base_Personnel.DepartmentName', width: 80 }

            , { label: '基本工资', name: 'BasePay', index: 'BasePay', editable: true, width: 80 }
            , { label: '岗位津贴', name: 'AllowancePay', index: 'AllowancePay', editable: true, width: 80 }
            , { label: '级别工资', name: 'LevelPay', index: 'LevelPay', editable: true, width: 80 }
            , { label: '工龄工资', name: 'SeniorityPay', index: 'SeniorityPay', editable: true, width: 80 }
            , { label: '学历津贴', name: 'EducationalPay', index: 'EducationalPay', editable: true, width: 80 }
            , { label: '计税工资', name: 'TaxPay', index: 'TaxPay', editable: true, width: 80 }

            , { label: '是否包括计件', name: 'IsPiece', index: 'IsPiece',  width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '是否包括计时', name: 'IsTiming', index: 'IsTiming', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '是否包括提成', name: 'IsDeduct', index: 'IsDeduct',   width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '是否按实际收入计税', name: 'IsTaxAction', index: 'IsTaxAction', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }  

            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
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
                myJqGrid.refreshGrid("1=1");
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

                    },
                    postCallBack: function (response) {
                        if (response.Result) {

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
     
 
}