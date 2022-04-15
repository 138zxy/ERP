function HR_KQ_PaiBanIndexInit(options) {
    /**
    * 获取本周、本季度、本月、上月的开始日期、结束日期
    */
    var now = new Date(); //当前日期  
    var nowDayOfWeek = now.getDay(); //今天本周的第几天 
    var nowDay = now.getDate(); //当前日 
    var nowMonth = now.getMonth(); //当前月  
    var nowYear = now.getYear(); //当前年 
    nowYear += (nowYear < 2000) ? 1900 : 0; //

    var nextWeekDate2 = new Date(); //月日期
    nextWeekDate2.setDate(nextWeekDate2.getDate() + 7); 
    var nowDayOfWeek2 = nextWeekDate2.getDay(); //今天下周周的第几天 
    var nowDay2 = nextWeekDate2.getDate(); //当前日 
    var nowMonth2 = nextWeekDate2.getMonth(); //当前月  
    var nowYear2 = nextWeekDate2.getYear(); //当前年 
    nowYear2 += (nowYear2 < 2000) ? 1900 : 0; //


    var nextMonth = nowMonth;
    var nextYear = nowYear; 
    if (nextMonth == 11) {
        nextMonth = 0;
        nextYear = nextYear + 1;
    }
    else {
        nextMonth = nextMonth + 1;
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
    function getMonthDays(myYear,myMonth) {
        var monthStartDate = new Date(myYear, myMonth, 1);
        var monthEndDate = new Date(myYear, myMonth + 1, 1);
        var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
        return days;
    }
 
    //获得本周的开始日期 
    function getWeekStartDate() {
        var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
        return formatDate(weekStartDate);
    }

    //获得本周的结束日期 
    function getWeekEndDate() {
        var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
        return formatDate(weekEndDate);
    }

    //获得下周的开始日期 
    function getNextWeekStartDate() {
        var weekStartDate = new Date(nowYear2, nowMonth2, nowDay2 - nowDayOfWeek2);
        return formatDate(weekStartDate);
    }

    //获得下周的结束日期 
    function getNextWeekEndDate() {
        var weekEndDate = new Date(nowYear2, nowMonth2, nowDay2 + (6 - nowDayOfWeek2));
        return formatDate(weekEndDate);
    }

    //获得本月的开始日期 
    function getMonthStartDate() {
        var monthStartDate = new Date(nowYear, nowMonth, 1);
        return formatDate(monthStartDate);
    }

    //获得本月的结束日期 
    function getMonthEndDate() {
        var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowYear,nowMonth));
        return formatDate(monthEndDate);
    }

    //获得下月月开始时间
    function getNextMonthStartDate() {
        var nextMonthStartDate = new Date(nextYear, nextMonth, 1);
        return formatDate(nextMonthStartDate);
    }

    //获得下月结束时间
    function getNextMonthEndDate() {
        var nextMonthEndDate = new Date(nextYear, nextMonth, getMonthDays(nextYear, nextMonth));
        return formatDate(nextMonthEndDate);
    }
    var Thisweekstart = getWeekStartDate();
    var Thisweekend = getWeekEndDate();
    var Nextweekstart = getNextWeekStartDate();
    var Nextweekend = getNextWeekEndDate();

    var ThisMonthstart = getMonthStartDate();
    var ThisMonthend = getMonthEndDate();
    var NextMonthstart = getNextMonthStartDate();
    var NextMonthend = getNextMonthEndDate();

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
		, sortByField: 'PersonID,WorkDate'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , sortOrder: 'ASC'
        , storeCondition: "(WorkDate>='" + Thisweekstart + "' and WorkDate<='" + Thisweekend + "')"
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '员工', name: 'PersonID', index: 'PersonID', width: 100, hidden: true }
            , { label: '班次', name: 'ArrangeID', index: 'ArrangeID', width: 100, hidden: true }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentID', index: 'HR_Base_Personnel.DepartmentID', width: 100, hidden: true }
            , { label: '工号', name: 'HR_Base_Personnel.Code', index: 'HR_Base_Personnel.Code', width: 80 }
            , { label: '员工', name: 'Name', index: 'Name', width: 80 }
            , { label: '部门', name: 'HR_Base_Personnel.DepartmentName', index: 'HR_Base_Personnel.DepartmentName', width: 80 }
            , { label: '工作日', name: 'WorkDate', index: 'WorkDate', width: 100, formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
            , { label: '星期', name: 'WeekShow', index: 'WeekShow', width: 100 } 
            , { label: '班次', name: 'ArrangeName', index: 'ArrangeName', width: 100 }
            , { label: '上班时间', name: 'WordStartTime', index: 'WordStartTime', width: 120, formatter: 'datetime' }
            , { label: '下班时间', name: 'WordEndTime', index: 'WordEndTime', width: 120, formatter: 'datetime' }

            , { label: '有效开始签到时间', name: 'UpStartTime', index: 'UpStartTime', width: 120, formatter: 'datetime' }
            , { label: '截止结束签退时间', name: 'DwonEndTime', index: 'DwonEndTime', width: 120, formatter: 'datetime' }

            , { label: '自动签到', name: 'AutoUp', index: 'AutoUp', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '自动签退', name: 'AutoDown', index: 'AutoDown', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '是否跨天', name: 'IsInnerDay', index: 'IsInnerDay', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }
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
                    width: 600,
                    height: 400,
                    postCallBack: function (response) {
                            myJqGrid.refreshGrid("1=1");
                        }
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn, 
                    width: 600,
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
            },
            BathPaiBan: function (btn) {
                //批量排班 
                $("#MyFormBathPaiBan").dialog("open"); 
            }, 
            ThisWeek: function (btn) {
                var str = "(WorkDate>='" + Thisweekstart + "' and WorkDate<='" + Thisweekend + "')";
                myJqGrid.refreshGrid(str);
            }, 
              NextWeek: function (btn) {
                  var str = "(WorkDate>='" + Nextweekstart + "' and WorkDate<='" + Nextweekend + "')";
                myJqGrid.refreshGrid(str);
            }, 
              ThisMonth: function (btn) {
                  var str = "(WorkDate>='" + ThisMonthstart + "' and WorkDate<='" + ThisMonthend + "')";
                myJqGrid.refreshGrid(str);
            }, 
              NextMonth: function (btn) {
                  var str = "(WorkDate>='" + NextMonthstart + "' and WorkDate<='" + NextMonthend + "')";
                myJqGrid.refreshGrid(str);
            },
            All: function (btn) { 
                myJqGrid.refreshGrid("1=1");
            }
        }
    });
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细', 
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            width: 600,
            height: 400,
            afterFormLoaded: function () {
                var RecordTo = myJqGrid.getSelectedRecord();
                $('input[name="PersonIDText"]').val(RecordTo.Name);
            } 
        });
    });
    $("#ArrangeID").change(function () { 
        GetWork();
    });
    $("#WorkDate").change(function () {
        var weekDay = ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
        var dateStr = $("#WorkDate").val();
        var myDate = new Date(Date.parse(dateStr.replace(/-/g, "/")));
        var WeekShow = weekDay[myDate.getDay()];
        $("#WeekShow").val(WeekShow);
        GetWork();
    });

    function GetWork() { 
        var dateStr = $("#WorkDate").val();
        var ArrangeID = $("#ArrangeID").val();
        if (dateStr != "" && ArrangeID > 0) {
            var requestURL = options.GetArrangeUrl;
            var postParams = { id: ArrangeID, workdate: dateStr };
            var rData;
            ajaxRequest(requestURL, postParams, false, function (response) {
                if (response.Result) {
                    $("#WordStartTime").val(response.Data.WordStartTime);
                    $("#WordEndTime").val(response.Data.WordEndTime);
                    $("#UpStartTime").val(response.Data.UpStartTime);
                    $("#DwonEndTime").val(response.Data.DwonEndTime);
                    $("#Remark").val(response.Data.Remark); 
                    if (response.Data.AutoUp) {
                        $('#AutoUp').attr('checked', true);
                    }
                    if (response.Data.AutoDown) {
                        $('#AutoDown').attr('checked', true);
                    }
                    if (response.Data.IsInnerDay) {
                        $('#IsInnerDay').attr('checked', true);
                    }
                }
            });
        }

    }
    //*-----------------------------------批量排班------------------------------------------      *//


    var myJqGrid2 = new MyGrid({
          renderTo: 'MyJqGrid2'
        , autoWidth: true
        , buttons: buttons1 
		, storeURL: options.PersonUrl
		, sortByField: 'ID'
		, primaryKey: 'ID'
		, setGridPageSize: 30
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
                myJqGrid2.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid2.refreshGrid();
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
            if (node2.length > 0) {
                var str = '';
                str = getAllChildrenNodes(treeNode, str);
                str = str + ',' + treeNode.id; // 加上被选择节点自己
                var ids = str.substring(1, str.length); // 去掉最前面的逗号
                var idsArray = ids.split(','); // 得到所有节点ID 的数组
                var length = idsArray.length; // 得到节点总数量
                myJqGrid2.refreshGrid("DepartmentID IN(" + idsArray + ")"); 
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
        //细节控制   
        $("input[name='Monday']").attr("checked", true);
        $("input[name='Tuesday']").attr("checked", true);
        $("input[name='Wednesday']").attr("checked", true);
        $("input[name='Thursday']").attr("checked", true);
        $("input[name='Friday']").attr("checked", true);
        $("#StartWeek").attr("disabled", true);
        $("#AlternateDay").attr("disabled", true); 
        $("#StartPai").val(NextMonthstart);
        $("#EndPai").val(NextMonthend); 

        $("#IsCancel").change(function () {
            var re = $("#IsCancel").is(':checked')
            if (re) { 
                console.log("ArraggeShowID");
                $("#ArraggeShowID").attr("disabled", true);
            }
            else {
                $("#ArraggeShowID").attr("disabled", false);
            }

        });
        $("#IsWeekAlternate").change(function () {
            var re = $("#IsWeekAlternate").is(':checked')
            if (re) {
                console.log("re:" + re);
                $("input[name='IsDayAlternate']").attr("checked", false);
                $("#StartWeek").attr("disabled", false);
                $("#AlternateDay").attr("disabled", true); 
            }
            else {
                $("#StartWeek").attr("disabled", true);
            } 
        });
        $("#IsDayAlternate").change(function () {
            var re = $("#IsDayAlternate").is(':checked')
            if (re) {
                $("input[name='IsWeekAlternate']").attr("checked", false);
                $("#AlternateDay").attr("disabled", false);
                $("#StartWeek").attr("disabled", true);
            }
            else {
                $("#AlternateDay").attr("disabled", true);
            }
        });
      //细节控制结束-----------

        $("#MyFormBathPaiBan").dialog({
            modal: true,
            autoOpen: false,
            width: 600,
            Height: 800,
            title: '批量排班',
            buttons: {
                '立即排班': function () {
                    var IsSelectDepart = $("#IsSelectDepart").is(':checked');
                    //勾选的部门节点
                    var treeObj = $.fn.zTree.getZTreeObj("Department2");
                    var nodes = treeObj.getCheckedNodes(true);
                    var departmentid = [];
                    for (var i = 0; i < nodes.length; i++) {
                        departmentid.push(nodes[i].id);
                    }
                    var Pesonid = myJqGrid2.getSelectedKeys();
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
                    var IsCancel = $("#IsCancel").is(':checked');
                    var ArraggeShowID = $("#ArraggeShowID").val();
                    var StartPai = $("#StartPai").val();
                    var EndPai = $("#EndPai").val();
                    if (!IsCancel) {
                        if (ArraggeShowID <= 0) {
                            showMessage('提示', "请选择班次!");
                            return;
                        }
                        if (StartPai == "" || EndPai == "") {
                            showMessage('提示', "请选择起止时间!");
                            return;
                        }
                    }

                    var Monday = $("#Monday").is(':checked');
                    var Tuesday = $("#Tuesday").is(':checked');
                    var Wednesday = $("#Wednesday").is(':checked');
                    var Thursday = $("#Thursday").is(':checked');
                    var Friday = $("#Friday").is(':checked');
                    var Saturday = $("#Saturday").is(':checked');
                    var Sunday = $("#Sunday").is(':checked');

                    var IsWeekAlternate = $("#IsWeekAlternate").is(':checked');
                    var StartWeek = $("#StartWeek").val();
                    if (IsWeekAlternate && !IsCancel) {
                        if (StartWeek == "") {
                            showMessage('提示', "请选择间隔交替开始星期!");
                            return;
                        }
                    }
                    var IsDayAlternate = $("#IsDayAlternate").is(':checked');
                    var AlternateDay = $("#AlternateDay").val();
                    if (IsDayAlternate && !IsCancel) {
                        if (AlternateDay <= 0) {
                            showMessage('提示', "请输入隔天数交替的天数!");
                            return;
                        }
                    }
                    var requestURL = options.BathPaiBanUrl;
                    ajaxRequest(requestURL, {
                        IsSelectDepart: IsSelectDepart,
                        departmentid: departmentid,
                        Pesonid: Pesonid,
                        IsCancel: IsCancel,
                        ArraggeShowID: ArraggeShowID,
                        StartPai: StartPai,
                        EndPai: EndPai,
                        Monday: Monday,
                        Tuesday: Tuesday,
                        Wednesday: Wednesday,
                        Thursday: Thursday,
                        Friday: Friday,
                        Saturday: Saturday,
                        Sunday: Sunday,
                        IsWeekAlternate: IsWeekAlternate,
                        StartWeek: StartWeek,
                        IsDayAlternate: IsDayAlternate,
                        AlternateDay: AlternateDay 

                    },
				false,
				function (response) {
				    if (!!response.Result) {
				        showMessage('提示', '排班成功！');
				      
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
        //设置日期列为日期范围搜索
        myJqGrid.setRangeSearch("WorkDate");
}