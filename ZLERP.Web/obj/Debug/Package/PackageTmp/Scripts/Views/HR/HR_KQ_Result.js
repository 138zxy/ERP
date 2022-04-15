function HR_KQ_ResultIndexInit(options) {
    var myJqGridPerson;
    $('#Department').height(gGridHeight + 80);
    $('#Department2').height(200);
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

    //*-----------------------------------批量排班------------------------------------------      *//
    myJqGridPerson = new MyGrid({
        renderTo: 'MyJqGridPerson'
        , autoWidth: true
        , buttons: buttons2
        , height: 200
		, storeURL: options.PersonUrl
		, sortByField: 'ID'
        , storeCondition: condition
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
        , sortOrder: 'ASC'
		, showPageBar: true
		, initArray: [
                  { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '部门编码', name: 'DepartmentID', index: 'DepartmentID', width: 70, hidden: true }
                , { label: '姓名', name: 'Name', index: 'Name', width: 70 }
                , { label: '工号', name: 'JobNo', index: 'JobNo', width: 70 }
                , { label: '部门名称', name: 'DepartmentName', index: 'Department.DepartmentName', width: 70 }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                myJqGridPerson.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridPerson.refreshGrid();
            }
        }
    });
    //-----------------------------树状开始-----------------------------------
    var treeSettings2 = {
        check: {
            enable: true
        },
        view: {
            selectedMulti: true
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
            onAsyncSuccess: zTreeOnAsyncSuccess2,
            onClick: zTreeOnCheck2
        }
    };

    var scLimitList2 = $.fn.zTree.init($('#Department2'), treeSettings2);
    scLimitList2.expandAll(true);

    function zTreeOnAsyncSuccess2(event, treeId, treeNode, msg) {
        var treeObj2 = $.fn.zTree.getZTreeObj(treeId);
        var nodes2 = treeObj2.getNodes();
        if (nodes2.length > 0) {
            for (var i = 0; i < nodes2.length; i++) {
                treeObj2.expandNode(nodes2[i], true, false, false);
            }
        }
    }
    //选择树节点事件
    function zTreeOnCheck2(event, treeId, treeNode, clickFlag) {
        var treeObj2 = $.fn.zTree.getZTreeObj("Department2");
        var node2 = treeObj2.getSelectedNodes(); //点击节点后 获取节点数据
        console.log(node2);
        if (node2.length > 0) {
            var str = '';
            str = getAllChildrenNodes(treeNode, str);
            str = str + ',' + treeNode.id; // 加上被选择节点自己
            var ids = str.substring(1, str.length); // 去掉最前面的逗号
            var idsArray = ids.split(','); // 得到所有节点ID 的数组

            jQuery("#MyJqGridPerson-datagrid").refreshGrid("DepartmentID IN(" + idsArray + ")");
//            var condition = "DepartmentID IN(" + idsArray + ")";
//            var options = { url: '/HR_Base_Personnel.mvc/Find', datatype: "json", page: 1 };
//            if (!isEmpty(condition)) {
//                options = { url: '/HR_Base_Personnel.mvc/Find', datatype: "json", page: 1, postData: { condition: condition} };
//            }
//            $("#MyJqGridPerson-datagrid").jqGrid('setGridParam', options).trigger("reloadGrid");
        }
    }

    //-----------------------------树状结束-----------------------------------
     

    //获得本月的开始日期 
    var now = new Date(); //当前日期  
    var nowDayOfWeek = now.getDay(); //今天本周的第几天 
    var nowDay = now.getDate(); //当前日 
    var nowMonth = now.getMonth(); //当前月  
    var nowYear = now.getYear(); //当前年 
    nowYear += (nowYear < 2000) ? 1900 : 0; //

    var lastMonth = nowMonth;
    var lastYear = nowYear; 
    if (lastMonth == 0) {
        lastMonth = 11;
       lastYear = lastYear- 1;
    }
    else {
        lastMonth = lastMonth - 1;
    }

    //格式化日期：yyyy-MM-dd 
    function formatDate(date) {
        var myyear = date.getFullYear();
        var mymonth = date.getMonth() + 1;
        var myweekday = date.getDate();

        if (mymonth < 10) {
            mymonth = "0" + mymonth;
        }
        if (myweekday < 10) {
            myweekday = "0" + myweekday;
        }
        return (myyear + "-" + mymonth + "-" + myweekday);
    }

    //获得某月的天数 
    function getMonthDays(myYear, myMonth) {
        var monthStartDate = new Date(myYear, myMonth, 1);
        var monthEndDate = new Date(myYear, myMonth + 1, 1);
        var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
        return days;
    }
 

    function getMonthStartDate() {
        var monthStartDate = new Date(nowYear, nowMonth, 1);
        return formatDate(monthStartDate);
    }

    //获得本月的结束日期 
    function getMonthEndDate() {
        var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowYear, nowMonth));
        return formatDate(monthEndDate);
    }

    //获得上月月月开始时间
    function getlastMonthStartDate() {
        var lastMonthStartDate = new Date(lastYear,lastMonth, 1);
        return formatDate(lastMonthStartDate);
    }

    //获得下月结束时间
    function getlastMonthEndDate() {
        var lastMonthEndDate = new Date(lastYear, lastMonth, getMonthDays(lastYear, lastMonth));
        return formatDate(lastMonthEndDate);
    }

    var ThisMonthstart = getMonthStartDate();
    var ThisMonthend = getMonthEndDate();
    var lastMonthstart = getlastMonthStartDate();
    var lastMonthend = getlastMonthEndDate();


    var myJqGrid1 = new MyGrid({
        renderTo: 'MyJqGrid1'
        , autoWidth: true
        , buttons: buttons1
        , height: gGridHeight
		, storeURL: options.HR_KQ_ResultMainUrl
		, sortByField: 'YearMonth'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '考勤名称', name: 'KQName', index: 'KQName', width: 120 }
            , { label: '对应年月', name: 'YearMonth', index: 'YearMonth', width: 80 } 
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
                myJqGrid1.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid1.refreshGrid("1=1");
            },
            handleAdd: function (btn) {
                myJqGrid1.handleAdd({
                    loadFrom: 'HR_KQ_ResultMainForm',
                    btn: btn,
                    width: 500,
                    height: 400
                });
            },
            handleEdit: function (btn) {
                myJqGrid1.handleEdit({
                    loadFrom: 'HR_KQ_ResultMainForm',
                    prefix: "HR_KQ_ResultMain",
                    btn: btn,
                    width: 500,
                    height: 400,
                    afterFormLoaded: function () {
                    }
                });
            }
            , handleDelete: function (btn) {
                myJqGrid1.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
            ,
            ComputeSalar: function (btn) {
                var keys = myJqGrid1.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的计算的单据!");
                    return;
                }
                var id = keys[0];
                var RecordTo = myJqGrid1.getSelectedRecord();
                var YearMonth = RecordTo.YearMonth;
                showConfirm("确认", "重计工资，将会删除当前月份的数据，重新统计。确认？",
				function () {
				    var requestURL = options.ComputeSalarUrl;
				    ajaxRequest(requestURL, { id: id }, false, function (response) {
				        if (response.Result) {
				            showMessage('提示', "计算成功!");
				            myJqGrid.refreshGrid("YearMonth='" + YearMonth + "'");
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
    myJqGrid1.addListeners('rowdblclick', function (id, record, selBool) {
        var RecordTo = myJqGrid1.getSelectedRecord();
        var YearMonth = RecordTo.YearMonth;
        myJqGrid.refreshGrid("YearMonth='" + YearMonth + "'");
        $("#formula-tabs").tabs({ fx: {}, selected: 1 });
    });


    myJqGrid1.addListeners("gridComplete", function () {
        var records = myJqGrid1.getAllRecords();
        var rid, buildtime;
        for (var i = 0; i < records.length; i++) {
            rid = records[i].ID;
            be = "<input class='identityButton'  type='button' value='查询详情' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleShow(" + rid + ");\"  />";
            myJqGrid1.getJqGrid().jqGrid('setRowData', rid, { act: be });
        }
    });

    window.handleShow = function (id) {
        var RecordTo = myJqGrid1.getSelectedRecord();
        var YearMonth = RecordTo.YearMonth;
        myJqGrid.refreshGrid("YearMonth='" + YearMonth + "'");
        $("#formula-tabs").tabs({ fx: {}, selected: 1 });
    }



    var condition = "(WorkDate>='" + ThisMonthstart + "' and WorkDate<='" + ThisMonthend + "')"
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'PersonID,WorkDate'
        , storeCondition: condition
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30 
        , sortOrder: 'ASC'
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'PersonID', name: 'PersonID', index: 'PersonID', width: 80, hidden: true }
            , { label: '对应年月', name: 'YearMonth', index: 'YearMonth', width: 80 }
            , { label: '工号', name: 'HR_Base_Personnel.Code', index: 'HR_Base_Personnel.Code', width: 60 }
            , { label: '员工', name: 'HR_Base_Personnel.Name', index: 'HR_Base_Personnel.Name', width: 60 }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentName', index: 'HR_Base_Personnel.DepartmentName', width: 80 }
            , { label: '工作日', name: 'WorkDate', index: 'WorkDate', width: 70, formatter: 'date' }
            , { label: '星期', name: 'WeekShow', index: 'WeekShow', width: 60 }
            , { label: '迟到分钟', name: 'LateMinute', index: 'LateMinute', width: 60 }
            , { label: '早退分钟', name: 'LeaveMinute', index: 'LeaveMinute', width: 60 }
            , { label: '请假小时', name: 'OnleaveHour', index: 'OnleaveHour', width: 60 }
            , { label: '加班小时', name: 'OverHour', index: 'OverHour', width: 60 }
            , { label: '出差小时', name: 'TravelHour', index: 'TravelHour', width: 60 }
            , { label: '是否旷工', name: 'IsAway', index: 'IsAway', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '旷工小时', name: 'AwayHour', index: 'AwayHour', width: 60 }
            , { label: '是否异常', name: 'IsUnusual', index: 'IsUnusual', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '是否休息日加班', name: 'IsAllOver', index: 'IsAllOver', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
            , { label: '上班时间', name: 'WorkStartTime', index: 'WorkStartTime', width: 120, formatter: 'datetime' }
            , { label: '下班时间', name: 'WorkEndTime', index: 'WorkEndTime', width: 120, formatter: 'datetime' }
            , { label: '上班打卡时间', name: 'UpStartTime', index: 'UpStartTime', width: 120, formatter: 'datetime' }
            , { label: '下班打卡时间', name: 'DwonEndTime', index: 'DwonEndTime', width: 120, formatter: 'datetime' }
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            ComputeNearMonth: function (btn) {
                showConfirm("确认", "确认计算最近一个月的考勤结果？",
				function () {
				    var requestURL = options.ComputeNearMonthUrl;
				    ajaxRequest(requestURL, {}, false, function (response) {
				        if (response.Result) {
				            showMessage('提示', "计算成功!");
				            return;
				        }
				        else {
				            showMessage('提示', response.Message);
				            return;
				        }
				    });
				})
            },
            ComputeReslut: function (btn) {
                //批量排班 
                //$("#MyFormReslut").dialog("open"); 
                //细节控制    
                $("#MyFormReslut").dialog({
                    modal: true,
                    autoOpen: true,
                    width: 600,
                    Height: 800,
                    title: '考勤结果计算',
                    buttons: { '立即计算': function () {
                        var IsSelectDepart = $("#IsSelectDepart").is(':checked');
                        //勾选的部门节点
                        var treeObj = $.fn.zTree.getZTreeObj("Department2");
                        var nodes = treeObj.getCheckedNodes(true);
                        var departmentid = [];
                        for (var i = 0; i < nodes.length; i++) {
                            departmentid.push(nodes[i].id);
                        }
                        var Pesonid = myJqGridPerson.getSelectedKeys();
                        if (IsSelectDepart) {
                            if (departmentid.length == 0) {
                                showMessage('提示', "请勾选相关的部门!");
                                return;
                            }
                        }
                        else {
                            if (Pesonid.length == 0) {
                                showMessage('提示', "请选择需要的员工!");
                                return;
                            }
                        }

                        var StartR = $("#StartR").val();
                        var EndR = $("#EndR").val();
                        if (StartR == "" || EndR == "") {
                            showMessage('提示', "请选择起止时间!");
                            return;
                        }
                        var requestURL = options.ComputeReslutUrl;

                        ajaxRequest(requestURL, {
                            IsSelectDepart: IsSelectDepart,
                            departmentid: departmentid,
                            Pesonid: Pesonid,
                            startdate: StartR,
                            enddate: EndR
                        },
				        false,
				        function (response) {
				            if (!!response.Result) {
				                showMessage('提示', '执行成功！');
				                myJqGrid.refreshGrid("1=1");
				            } else {
				                showMessage('提示', response.Message);
				            }
				        });

                        $(this).dialog('close');
                    },
                        '取消': function () {
                            $(this).dialog('close');
                        }
                    },
                    position: ["center", 100]
                });   

            },
            ThisMonth: function (btn) { 
                  var str = "(WorkDate>='" + ThisMonthstart + "' and WorkDate<='" + ThisMonthend + "')";
                  myJqGrid.refreshGrid(str);
            },
             LastMonth: function (btn) {
                var str = "(WorkDate>='" + lastMonthstart + "' and WorkDate<='" + lastMonthend + "')";
                  myJqGrid.refreshGrid(str);
            },
             All: function (btn) {
                myJqGrid.refreshGrid("1=1");
            }  
        }
    });
    //设置日期列为日期范围搜索
    myJqGrid.setRangeSearch("WorkDate");
                
}