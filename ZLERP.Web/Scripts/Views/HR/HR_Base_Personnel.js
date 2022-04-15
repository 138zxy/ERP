function hr_base_personnelIndexInit(opt) {
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
            url: opt.deptTreeUrl,
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
            console.log(idsArray);
            myJqGrid.refreshGrid("DepartmentID IN(" + idsArray + ")");
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
    var typeno = "", typename = "";
    var img;
    //自定义图片的格式，可以根据rowdata自定义
    function alarmFormatter(cellvalue, options, rowdata) {
        //console.log(rowdata);
        var url2;
        if (cellvalue == undefined || cellvalue == "" || cellvalue == null) {
            url2 = '/Content/erpimage/user.png';//为空默认
        }
        else {
            url2 = cellvalue; 
        }
        return '<a onclick="showPhotoDialog(' + rowdata.ID + ');" target="view_window" alt="' + url2 + '"><img src="' + url2 + '" id="img' + rowdata.ID + '" alt="' + url2 + '" style="width:30px;height:30px;"  /></a>';
    }
    function unalarmFormatter(cellvalue, options, cell) {
        return $('img', cell).attr('src');
        return $('a', cell).attr('href');
    }
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opt.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth:650
            , dialogHeight: 600
            , rowNumbers: true
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '照片', name: 'PhotoPath', index: 'PhotoPath', hidden: false, formatter: alarmFormatter, width: 40, unformat: unalarmFormatter }
                , { label: '员工编号', name: 'Code', index: 'Code', width: 70 }
                , { label: '姓名', name: 'Name', index: 'Name', width: 70 }
                , { label: '工号', name: 'JobNo', index: 'JobNo', width: 70 }
                , { label: '拼音码', name: 'PyCode', index: 'PyCode', width: 70 }
                , { label: '性别', name: 'Sex', index: 'Sex', width: 50 }
                , { label: '民族', name: 'Nation', index: 'Nation', width: 50 }
                , { label: '结婚状态', name: 'Marry', index: 'Marry', width: 60 }
                , { label: '生日', name: 'Birthday', index: 'Birthday', formatter: 'date', width: 70 }
                , { label: '入职时间', name: 'PostDate', index: 'PostDate', formatter: 'date', width: 70 }
                , { label: '学历', name: 'RecordSchool', index: 'RecordSchool', width: 70 }
                , { label: '毕业学校', name: 'School', index: 'School', width: 70 }
                , { label: '所学专业', name: 'Profession', index: 'Profession', width: 70 }
                , { label: '毕业时间', name: 'GraduateDate', index: 'GraduateDate', formatter: 'date', width: 70 }
                , { label: '社保状态', name: 'SocialState', index: 'SocialState', width: 70 }
                , { label: '现住地址', name: 'Address', index: 'Address', width: 70 }
                , { label: '身份证号', name: 'IDno', index: 'IDno', width: 120 }
                , { label: '身份证地址', name: 'IDAddress', index: 'IDAddress', width: 150 }
                , { label: '家庭电话', name: 'Telphone', index: 'Telphone', width: 70 }
                , { label: '手机', name: 'CellPhone', index: 'CellPhone', width: 70 }
                , { label: '紧急联系人', name: 'UrgentPerson', index: 'UrgentPerson', width: 70 }
                , { label: '职位编码', name: 'PositionType', index: 'PositionType', width: 70 }
                , { label: '岗位', name: 'Post', index: 'Post', width: 70 }
                , { label: '上岗性质', name: 'MountGuardProperty', index: 'MountGuardProperty', width: 70 }
                , { label: '部门编码', name: 'DepartmentID', index: 'DepartmentID', width: 70, hidden: true }
                , { label: '部门名称', name: 'DepartmentName', index: 'Department.DepartmentName', width: 70 }
                , { label: '备注', name: 'Meno', index: 'Meno', width: 70 }
                , { label: '人员状态', name: 'State', index: 'State', width: 70 }
                , { label: '用工形式', name: 'EmploymentForm', index: 'EmploymentForm', width: 70 }
                , { label: '电子邮件', name: 'Email', index: 'Email', width: 70 }
                , { label: '个人简历', name: 'Vita', index: 'Vita', width: 70 }
                , { label: '转正日期', name: 'CorrectionDate', index: 'CorrectionDate', formatter: 'date', width: 70 }
                , { label: '档案号', name: 'ArchivesNo', index: 'ArchivesNo', width: 70 }
                , { label: '银行卡号', name: 'BankNo', index: 'BankNo', width: 70 }
                , { label: '学历证号', name: 'SchoolRecordNo', index: 'SchoolRecordNo', width: 70 }
                , { label: '照片', name: 'PhotoPath', index: 'PhotoPath', width: 150 }
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
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldValue('DepartmentID', typeno);
                            myJqGrid.setFormFieldValue('DepartmentID_text', typename);
                        },
                        postCallBack: function (response) {
                            if (response.Result) {
                                
                            }
                        }
                    });
                },
                handleEdit: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("input[name='DepartmentID_text']").val(Record.DepartmentName);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }, LoadExcel: function (btn) {
                    myJqGrid.showWindow({
                        loadFrom: 'MyFormDiv1',
                        btn: btn,
                        width: 500,
                        height: 200,
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
                },
                print: function (btn) {
                    var key = myJqGrid.getSelectedKey();
                    if (key <= 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var url = "/GridReport/DisplayReport.aspx?report=HR_Base_Personnel&ID=" + key;
                    window.open(url, "_blank");
                },
                PrintDesign: function (btn) {
                    var url = "/GridReport/DesignReport.aspx?report=HR_Base_Personnel";
                    window.open(url, "_blank");
                },
                PrintDirect: function (btn) {
                    var key = myJqGrid.getSelectedKey();
                    if (key <= 0) {
                        showMessage('提示', "请选择需要的单据!");
                        return;
                    }
                    var url = "/GridReport/PrintDirect.aspx?report=HR_Base_Personnel&ID=" + key;
                    window.open(url, "_blank");
                }
            }
    });
    //浏览照片
    window.showPhotoDialog= function(id) {
        var path = $("#img" + id).attr('src');
        $("#Photo").attr("src", "");
        $("#Photo").attr("src", path);
        $("#Photo").attr("style", "width:100px;height:100px;");

        $('#divShowPhoto').dialog({
            width: 300,
            height: 300,
            modal: true,
            buttons: {
                '关闭': function () {
                    $(this).dialog('close');
                }
            }
        });
    }
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细', 
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
                var Record = myJqGrid.getSelectedRecord();
                $("input[name='DepartmentID_text']").val(Record.DepartmentName);
            }
        });
    });
    //上传附件
    function attachmentUpload() {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload({
            url: opt.uploadUrl,
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
                    myJqGrid.reloadGrid("1=1");
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