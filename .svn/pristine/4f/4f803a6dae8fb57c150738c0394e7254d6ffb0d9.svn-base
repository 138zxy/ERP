function HR_GZ_SalariesIndexInit(options) {
    $('#Department').height(gGridHeight + 80);
    var CurrID=0; //记录当前的ID
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
             
            myJqGrid2.refreshGrid("SalariesID=" + CurrID+ " and HR_Base_Personnel.DepartmentID IN(" + idsArray + ")");
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
        renderTo: 'MyJqGrid1' 
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight 
		, storeURL: options.HR_GZ_SalariesUrl
		, sortByField: 'YearMonth'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true  
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '薪资名称', name: 'SalarName', index: 'SalarName', width: 120 }
            , { label: '对应年月', name: 'YearMonth', index: 'YearMonth', width: 80 }

            , { label: '总金额', name: 'AllMoney', index: 'AllMoney', width: 80, search: false}
            , { label: '是否封存', name: 'IsSeal', index: 'IsSeal', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '操作', name: 'act', index: 'act', width: 90, sortable: false, align: "center" }
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
                    loadFrom: 'HR_GZ_SalariesForm',
                    btn: btn,
                    width: 500,
                    height: 400 
                });
            },
        handleEdit: function (btn) { 
                myJqGrid.handleEdit({
                    loadFrom: 'HR_GZ_SalariesForm',
                    prefix: "HR_GZ_Salaries", 
                    btn: btn,
                    width: 500,
                    height: 400,
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
            ,
            ComputeSalar: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的计算的单据!");
                    return;
                }
                var id=keys[0];
                showConfirm("确认", "重计工资，将会删除当前月份的数据，重新统计。确认？",
				function () {
				    var requestURL = options.ComputeSalarUrl;
				    ajaxRequest(requestURL, {id:id}, false, function (response) {
				        if (response.Result) {
				            showMessage('提示', "计算成功!");
				            myJqGrid2.refreshGrid("SalariesID=" + id);
				            $("#formula-tabs").tabs({ fx: {}, selected: 1 });
				            return;
				        }
				        else {
				            showMessage('提示', "计算失败!");
				            return;
				        }
				    });
				})
            }
        }
    }); 
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        var keys = myJqGrid.getSelectedKeys();
        if (keys.length == 0) {
            showMessage('提示', "请选择需要的计算的单据!");
            return;
        }
        var id = keys[0];
        CurrID = id;
        myJqGrid2.refreshGrid("SalariesID=" + id);
        $("#formula-tabs").tabs({ fx: {}, selected: 1 });
    });
    
 
    myJqGrid.addListeners("gridComplete", function () {  
        var records = myJqGrid.getAllRecords();
        var rid, buildtime;
        for (var i = 0; i < records.length; i++) {
            rid = records[i].ID; 
            be = "<input class='identityButton'  type='button' value='查询详情' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleShow(" + rid + ");\"  />";
                myJqGrid.getJqGrid().jqGrid('setRowData', rid, { act: be });
        }
    });

    window.handleShow = function (id) { 
        CurrID=id;
        myJqGrid2.refreshGrid("SalariesID=" + id);
				    $("#formula-tabs").tabs({ fx: {}, selected: 1 });
    }

    var myJqGrid2 = new MyGrid({
        renderTo: 'MyJqGrid2'
        , autoWidth: true
        , buttons: buttons1
        , height: gGridHeight
		, storeURL: options.HR_GZ_SalariesItemUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , groupField: 'HR_Base_Personnel.DepartmentName'
        , groupingView: {
            groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'],
            groupOrder: ['desc'],
            groupSummary: [true],
            minusicon: 'ui-icon-circle-minus',
            plusicon: 'ui-icon-circle-plus'
        }
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'SalariesID', name: 'SalariesID', index: 'PersonID', width: 80, hidden: true }
            , { label: 'PersonID', name: 'PersonID', index: 'PersonID', width: 80, hidden: true }
            , { label: '对应年月', name: 'YearMonth', index: 'YearMonth', width: 80 }
            , { label: '工号', name: 'HR_Base_Personnel.Code', index: 'HR_Base_Personnel.Code', width: 80 }
            , { label: '员工', name: 'Name', index: 'Name', width: 80 }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentName', index: 'HR_Base_Personnel.DepartmentName', width: 80 }

            , { label: '基本工资', name: 'BasePay', index: 'BasePay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '岗位津贴', name: 'AllowancePay', index: 'AllowancePay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '级别工资', name: 'LevelPay', index: 'LevelPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '工龄工资', name: 'SeniorityPay', index: 'SeniorityPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '学历津贴', name: 'EducationalPay', index: 'EducationalPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '绩效奖金', name: 'PerformancePay', index: 'PerformancePay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '计件工资', name: 'PiecePay', index: 'PiecePay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '计时工资', name: 'TimingPay', index: 'TimingPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '计提工资', name: 'DeductPay', index: 'DeductPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '加班奖金', name: 'OverPay', index: 'OverPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '其他补贴', name: 'SubsidyPay', index: 'SubsidyPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '应发合计', name: 'AllPay', index: 'AllPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }


            , { label: '社保扣款', name: 'SocialPay', index: 'SocialPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '个人所得税', name: 'TaxPay', index: 'TaxPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '请假扣款', name: 'LeavePay', index: 'LeavePay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '迟到扣款', name: 'LatePay', index: 'LatePay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '旷工扣款', name: 'StayAwayPay', index: 'StayAwayPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '其他扣款', name: 'TakeOffPay', index: 'TakeOffPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '应扣合计', name: 'OutPay', index: 'OutPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '实际应发', name: 'ActionPay', index: 'ActionPay', width: 60, search: false, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }
            , { label: '最后修改人', name: 'Modifier', index: 'Modifier', width: 80 }
            , { label: '最后修改时间', name: 'ModifyTime', index: 'ModifyTime', width: 120, formatter: 'datetime' }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                myJqGrid2.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid2.refreshGrid("SalariesID=" + CurrID);
            },
            handleAdd: function (btn) {
                if (CurrID <= 0) {
                    showMessage('提示', "请选择一个工资项目进行新增!");
                    return;
                }
                $('#HR_GZ_SalariesItem_SalariesID').val(CurrID);
                var RecordTo = myJqGrid.getSelectedRecord(); 
                var flag = RecordTo.IsSeal === "false" ? false : true
                if (flag) {
                    showMessage('提示', "账目已经封存!");
                    return;
                }
                myJqGrid2.handleAdd({
                    loadFrom: 'HR_GZ_SalariesItemForm',
                    btn: btn,
                    width: 800,
                    height: 500
                });
            },
            handleEdit: function (btn) {
                if (CurrID <= 0) {
                    showMessage('提示', "请选择一个工资项目进行新增!");
                    return;
                }
                $('#HR_GZ_SalariesItem_SalariesID').val(CurrID);
                var RecordTo = myJqGrid.getSelectedRecord();
                var flag = RecordTo.IsSeal === "false" ? false : true
                if (flag) {
                    showMessage('提示', "账目已经封存!");
                    return;
                }
                myJqGrid2.handleEdit({
                    loadFrom: 'HR_GZ_SalariesItemForm',
                    prefix: "HR_GZ_SalariesItem",
                    btn: btn,
                    width: 800,
                    height: 500,
                    afterFormLoaded: function () {
                        var RecordTo = myJqGrid2.getSelectedRecord();
                        $('input[name="PersonIDText"]').val(RecordTo.Name);
                    }
                });
            }
            , handleDelete: function (btn) {
                if (CurrID <= 0) {
                    showMessage('提示', "请选择一个工资项目进行新增!");
                    return;
                }
                $('#HR_GZ_SalariesItem_SalariesID').val(CurrID);
                var RecordTo = myJqGrid.getSelectedRecord();
                var flag = RecordTo.IsSeal === "false" ? false : true
                if (flag) {
                    showMessage('提示', "账目已经封存!");
                    return;
                }
                myJqGrid2.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            Compute: function (btn) {
                var keys = myJqGrid2.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的计算的单据!");
                    return;
                }
                showConfirm("确认", "重计当前选中的工资，将会删除当前选中的数据，重新统计。确认？",
				function () {
				    var requestURL = options.ComputeUrl;
				    ajaxRequest(requestURL, { ids: keys }, false, function (response) {
				        if (response.Result) {
				            showMessage('提示', "计算成功!");
				            myJqGrid2.refreshGrid();
				            return;
				        }
				        else {
				            showMessage('提示', "计算失败!");
				            return;
				        }
				    });
				})
            }
        }
    });
    myJqGrid2.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid2.handleEdit({
            loadFrom: 'HR_GZ_SalariesItemForm',
            prefix: "HR_GZ_SalariesItem",
            title: '查看详细',
            width: 800,
            height: 500,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
                var RecordTo = myJqGrid2.getSelectedRecord();
                $('input[name="PersonIDText"]').val(RecordTo.Name);
            }
        });
    }); 
 
 
   
}