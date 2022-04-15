function SC_FixedIndexInit(options) {
    $('#GoodgType').height(gGridHeight + 80);
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
            url: options.scTreeUrl,
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

    var scLimitList = $.fn.zTree.init($('#GoodgType'), treeSettings);
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
        var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
        var node = treeObj.getSelectedNodes(); //点击节点后 获取节点数据
        if (node.length > 0) {
             var str ='' ;
             str = getAllChildrenNodes(treeNode, str);
             str = str + ',' + treeNode.id; // 加上被选择节点自己
             var ids = str.substring(1, str.length); // 去掉最前面的逗号
             var idsArray = ids.split(','); // 得到所有节点ID 的数组
             var length = idsArray.length; // 得到节点总数量
             console.log(idsArray);
             myJqGrid.refreshGrid("Ftype IN(" + idsArray + ")");
     
        }
    }
    //递归，获取所有子节点
    function getAllChildrenNodes(treeNode,result){
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
        renderTo: 'SC_FixedGrid',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: options.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '编号',
		    name: 'Fcode',
		    index: 'Fcode',
		    width: 60
		},
		{
		    label: '条形码',
		    name: 'BarCode',
		    index: 'BarCode',
		    width: 80
		},
		{
		    label: '名称',
		    name: 'Fname',
		    index: 'Fname',
		    width: 80
		},
		{
		    label: '拼音简码',
		    name: 'BrevityCode',
		    index: 'BrevityCode',
		    width: 80
		},
		{
		    label: '类型',
		    name: 'Ftype',
		    index: 'Ftype',
		    width: 80,
		    hidden: true
		},
        {
            label: '类型',
            name: 'TypeName',
            index: 'TypeName',
            width: 80
        },
        {
            label: '规格型号',
            name: 'Spec',
            index: 'Spec',
            width: 80
        },
        {
            label: '价格',
            name: 'FixedPirce',
            index: 'FixedPirce',
            width: 80
        },
        {
            label: '制造商',
            name: 'Manufacturer',
            index: 'Manufacturer',
            width: 80
        },
        {
            label: '供应商',
            name: 'Supply',
            index: 'Supply',
            width: 100
        },
        {
            label: '国标编码',
            name: 'InterCode',
            index: 'InterCode',
            width: 80
        },
        {
            label: '出厂日期',
            name: 'ProductDate',
            index: 'ProductDate',
            formatter: 'date',
            width: 80
        },
        {
            label: '增加方式',
            name: 'AddType',
            index: 'AddType',
            width: 80
        },
        {
            label: '增加日期',
            name: 'AddDate',
            index: 'AddDate',
            formatter: 'date',
            width: 80
        },
        {
            label: '配置',
            name: 'Configure',
            index: 'Configure',
            width: 120
        },
        {
            label: '存放位置',
            name: 'Position',
            index: 'Position',
            width: 120
        },
        {
            label: '使用部门',
            name: 'DepartMent',
            index: 'DepartMent',
            width: 80
        },
        {
            label: '保管日期',
            name: 'StoreDate',
            index: 'StoreDate',
            formatter: 'date',
            width: 80
        },
        {
            label: '保管员',
            name: 'Storeman',
            index: 'Storeman',
            width: 80
        },
        {
            label: '净残值率(%)',
            name: 'NetSalvageRate',
            index: 'NetSalvageRate',
            width: 80
        },
        {
            label: '净残值',
            name: 'NetSalvage',
            index: 'NetSalvage',
            width: 80
        },
        {
            label: '折旧方法',
            name: 'Depreciation',
            index: 'Depreciation',
            width: 80
        },
        {
            label: '可用年限',
            name: 'UseYear',
            index: 'UseYear',
            width: 80
        },
        {
            label: '可用截止日期',
            name: 'UseEndDate',
            index: 'UseEndDate',
            formatter: 'date',
            width: 80
        },
        {
            label: '月折旧率',
            name: 'DepreciaMonthRate',
            index: 'DepreciaMonthRate',
            width: 80
        },
        {
            label: '月标准折旧',
            name: 'DepreciaMonthMoney',
            index: 'DepreciaMonthMoney',
            width: 80
        },
        {
            label: '状态',
            name: 'Condition',
            index: 'Condition',
            width: 60
        },
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 80
		},
        { label: "资产文件", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false }
		 ],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handleAdd: function (btn) {
                ajaxRequest(options.GenerateOrderNoUrl, {},
				false,
				function (response) {
				    if (response.Result) {
				        console.log(response.Data);
				        myJqGrid.handleAdd({
				            loadFrom: 'SC_FixedForm',
				            btn: btn,
				            width: 700,
				            height: 600,
				            afterFormLoaded: function () {
				                $("#Attachments").empty();
				                $("#tb_IsBath").show();
				                $("#SC_Fixed_Fcode").val(response.Data);
				                $("#SC_Fixed_UseYear").val("1");
				                $("#SC_Fixed_Condition").val("正常");
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
                var TypeName = Record.TypeName;
                myJqGrid.handleEdit({
                    loadFrom: 'SC_FixedForm',
                    btn: btn,
                    width: 700,
                    height: 600,
                    prefix: "SC_Fixed",
                    afterFormLoaded: function () {
                        //附件
                        var attDiv = $("#Attachments");
                        attDiv.empty();
                        attDiv.append(Record["Attachments"]);
                        $("a[att-id]").show();

                        $("#tb_IsBath").hide();
                        $("input[name='SC_Fixed.Ftype_text']").val(TypeName);
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(Record.ID);
                        }
                    }
                });
            },
            handleDelete: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                } 
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            handleCopy: function (btn) {
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var TypeName = Record.TypeName;
                var Ftype = Record.Ftype;
                ajaxRequest(options.GenerateOrderNoUrl, {},
				false,
				function (response) {
				    if (response.Result) {
				        myJqGrid.handleEdit({
				            loadFrom: 'SC_FixedForm',
				            btn: btn,
				            width: 700,
				            height: 600,
				            prefix: "SC_Fixed",
				            afterFormLoaded: function () {
				                $("#tb_IsBath").show();
				                $("#SC_Fixed_Fcode").val(response.Data);
				                $("input[name='SC_Fixed.Ftype_text']").val(TypeName);
				                $("input[name='SC_Fixed.Ftype']").val(Ftype);
				            }
				        });
				    }
				});
            },
            handleList: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                myJqGrid.handleEdit({
                    loadFrom: 'SC_Fixed_DelGrid',
                    title: '查看资产子表',
                    width: 800,
                    height: 600,
                    buttons: {
                        "关闭": function () {
                            $(this).dialog('close');
                        }
                    },
                    afterFormLoaded: function () {
                       myJqGridTo.refreshGrid("FixedID="+id); 
                       circulateGrid.refreshGrid("FixedID="+id); 
                       maintainGrid.refreshGrid("FixedID="+id); 
                       shiftGrid.refreshGrid("FixedID="+id); 
                       cleanGrid.refreshGrid("FixedID="+id); 
                       pandianGrid.refreshGrid("FixedID="+id); 
                    }
                });
              
            },
            handleCompute: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                showConfirm("确认信息", "计算将会重计【状态,可用截止时间,净残值,月折旧率,月标准折旧】,请确认？",
				function () {
				    ajaxRequest(options.ComputeUrl, { id: id },
					false,
					function (response) {
					    if (response.Result) {
					        showMessage('提示', "操作成功");
					        myJqGrid.refreshGrid();
					    }
					});
				})

            } 
        }
    });

    //固定资产项目明细
    var myJqGridTo = new MyGrid({
        renderTo: 'info1',
        autoWidth: true,
        buttons: buttons1,
        height:200,
        storeURL: options.ZhangstoreUrl,
        sortByField: 'ID',
        dialogWidth: 700,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        multiselect: false,
        autoLoad: false,
        editSaveUrl: options.UpdateUlr,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
            },
		    {
		        label: '资产',
		        name: 'FixedID',
		        index: 'FixedID',
		        width: 120,
		        hidden: true
		    }, 
            {
                label: '项目名称',
                name: 'EntryName',
                index: 'EntryName',
                width: 80,
                editable: true
            },
		    {
		        label: '项目说明',
		        name: 'EntryDec',
		        index: 'EntryDec',
                editable: true, 
		        width: 200
		    },
            {
                label: '备注',
                name: 'Remark',
                index: 'Remark',
                editable: true, 
                width: 200
            }],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            },
            handleAdddel: function (btn) {
               var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID; 
                var orderNo = Record.OrderNo;
                myJqGridTo.handleAdd({
				            loadFrom: 'SC_Fixed_DelForm',
				            btn: btn, 
				            afterFormLoaded: function () { 
				          $("#SC_Fixed_Del_FixedID").val(id);
				             
				           }
			     });
            },
            handleEditDel: function (btn) {
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var TypeName = Record.TypeName;
                myJqGridTo.handleEdit({
                    loadFrom: 'SC_Fixed_DelForm',
                    btn: btn, 
                    prefix: "SC_Fixed_Del",
                    afterFormLoaded: function () {
                        
                    }
                });
            },
            handleDeleteDel: function (btn) { 
                myJqGridTo.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            } 
        }
    });
 
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        var Record = myJqGrid.getSelectedRecord();
        var id = Record.ID;
        var TypeName = Record.TypeName;
        myJqGrid.handleEdit({
            loadFrom: 'SC_FixedForm',
            title: '查看资产详细',
            width: 700,
            height: 600,
            prefix: "SC_Fixed",
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
                $("#tb_IsBath").hide();
                $("input[name='SC_Fixed.Ftype_text']").val(TypeName);
            }
        }); 
    });
//    myJqGrid.addListeners('rowclick',
//	function (id, record, selBool) {
//	    myJqGridTo.refreshGrid("FixedID=" + id);
//	});
    myJqGrid.addListeners("gridComplete",
	function () {
	    var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
	    for (var i = 0; i < ids.length; i++) {
	        var cl = ids[i];
	        var record = myJqGrid.getRecordByKeyValue(ids[i]);
	        if (record.Condition != "正常") {
	            myJqGrid.getJqGrid().setCell(cl, "Condition", '', {
	                color: 'red'
	            },
				'');
	        }
	    }
	});
     
    $("#SC_Fixed_Fname").change(function () {
        var str = $("#SC_Fixed_Fname").val();
        ajaxRequest(options.LP_TOPY_SHORTUrl, {
            str: str
        },
		false,
		function (response) {
		    if (response.Result) {
		        $("#SC_Fixed_BrevityCode").val(response.Data); 

		    } else {
		        showMessage('提示', response.Message);
		    }
		});
    });
    $("#SC_Fixed_NetSalvageRate").change(function () {
        GetRate(); 
    });

    $("#SC_Fixed_UseYear").change(function () {
        GetRate();
    });
    //获取比率
    function GetRate() { 
        var price = $("#SC_Fixed_FixedPirce").val();
        var rate = $("#SC_Fixed_NetSalvageRate").val();
        var Lprice = price * rate / 100
        $("#SC_Fixed_NetSalvage").val(Lprice);
        var year = $("#SC_Fixed_UseYear").val();

        var Fixed_AddDate = $("#SC_Fixed_AddDate").val();
        var adddate = new Date(Fixed_AddDate)

        var y = adddate.getFullYear() + parseInt(year);
        var m = (adddate.getMonth() + 1) < 10 ? "0" + (adddate.getMonth() + 1) : (adddate.getMonth() + 1); //获取当前月份的日期，不足10补0
        var d = adddate.getDate() < 10 ? "0" + adddate.getDate() : adddate.getDate(); //获取当前几号，不足10补0
        var newdate = y + "-" + m + "-" + d; ;
        // newdate = new Date(newdate);
        console.log(newdate);

        $("#SC_Fixed_UseEndDate").val(newdate); 

        var DepreciaMonthRate = ((1 - rate / 100) / (year * 12)).toFixed(4);
        $("#SC_Fixed_DepreciaMonthRate").val(DepreciaMonthRate);
        var DepreciaMonthMoney = DepreciaMonthRate * price;
        $("#SC_Fixed_DepreciaMonthMoney").val(DepreciaMonthMoney);
    }

    //上传附件
    function attachmentUpload(objectId) {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload({
                url: options.uploadUrl + "?objectType=SC_Fixed&objectId=" + objectId,
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

    //借还记录
    var circulateGrid = new MyGrid({
        renderTo: 'info2'
        , autoWidth: true
        //, buttons: buttons0
        , height: 400
		, storeURL:'/SC_Fixed_Circulate.mvc/Find' 
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'FixedID', name: 'FixedID', index: 'FixedID', width: 80, hidden: true }
            , { label: '流水号', name: 'CirculateNo', index: 'CirculateNo', width: 130 }      
            , { label: '借出日期', name: 'BorrowDate', index: 'BorrowDate', width: 80, formatter: 'date' }
            , { label: '批准人', name: 'ApproveMan', index: 'ApproveMan', width: 80 }
            , { label: '借用部门', name: 'BorrowDepart', index: 'BorrowDepart', width: 80 }
            , { label: '借用人', name: 'BorrowMan', index: 'BorrowMan', width: 80 }
            , { label: '拟还日期', name: 'MayBackDate', index: 'MayBackDate', width: 80, formatter: 'date' }
            , { label: '借出备注', name: 'BorrowRemark', index: 'BorrowRemark', width: 80 }
            , { label: '是否归还', name: 'IsBack', index: 'IsBack', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '归还日期', name: 'BackDate', index: 'BackDate', width: 80, formatter: 'date' }
            , { label: '归还备注', name: 'BackRemark', index: 'BackRemark', width: 200 }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                circulateGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                circulateGrid.refreshGrid();
            }
        }
    });

    //维修记录
    var maintainGrid = new MyGrid({
        renderTo: 'info3'
        , autoWidth: true
        //, buttons: buttons0
        , height: 400
		, storeURL:'/SC_Fixed_Maintain.mvc/Find'
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'FixedID', name: 'FixedID', index: 'FixedID', width: 80, hidden: true }
            , { label: '流水号', name: 'MaintainNo', index: 'MaintainNo', width: 120 }
            , { label: '维修类型', name: 'RepairType', index: 'RepairType', width: 80 }
            , { label: '维修方式', name: 'RepairWay', index: 'RepairWay', width: 80 }
            , { label: '送修时间', name: 'RepairDate', index: 'RepairDate', width: 80, formatter: 'date' }
            , { label: '送修人员', name: 'GiveMan', index: 'GiveMan', width: 80 }
            , { label: '维修地点', name: 'RepairAdress', index: 'RepairAdress', width: 120 }
            , { label: '故障描述', name: 'FaultDesc', index: 'FaultDesc', width: 200 }
            , { label: '是否完修', name: 'IsOver', index: 'IsOver', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '完修时间', name: 'OverDate', index: 'OverDate', width: 80, formatter: 'date' }
            , { label: '维修人员', name: 'RepairMan', index: 'RepairMan', width: 80 }
            , { label: '维修费用(元)', name: 'RepairPirce', index: 'RepairPirce', width: 80 }
            , { label: '维修时长(h)', name: 'RepairTime', index: 'RepairTime', width: 80 }
            , { label: '修理描述', name: 'RepairDesc', index: 'RepairDesc', width: 200 }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                maintainGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                maintainGrid.refreshGrid();
            }
        }
    });

    //转移记录
    var shiftGrid = new MyGrid({
        renderTo: 'info4'
           //, width: '100%'
            , autoWidth: true
            //, buttons: buttons0
            , height: 400
		    , storeURL: '/SC_Fixed_Shift.mvc/Find'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 500
            , dialogHeight: 500
		    , initArray: [
                { label: '转移编号', name: 'ID', index: 'ID', width: 60, hidden: true }
                , { label: '流水号', name: 'ShiftNo', index: 'ShiftNo', width: 100 }
            
                , { label: '原使用部门', name: 'DepartMent', index: 'DepartMent', width: 90 }
                , { label: '原保管员', name: 'Storeman', index: 'Storeman', width: 80 }
                , { label: '原存放位置', name: 'Position', index: 'Position', width: 100 }
                , { label: '转移日期', name: 'ShiftDate', index: 'ShiftDate', formatter: 'date', width: 70 }
                , { label: '转移人', name: 'ShiftMan', index: 'ShiftMan', width: 70 }
                , { label: '新保管员', name: 'StoremanNew', index: 'StoremanNew', width: 70 }
                , { label: '新存放位置', name: 'PositionNew', index: 'PositionNew', width: 90 }
                , { label: '批准人', name: 'ApproveMan', index: 'ApproveMan', width: 70 }
                , { label: '备注', name: 'Remark', index: 'Remark' }             
                , { label: '新使用部门', name: 'DepartMentNew', index: 'DepartMentNew', width: 70 }
                , { label: '原分类号', name: 'Ftype', index: 'Ftype', width: 90, hidden: true }
                , { label: '原分类', name: 'FTypeName', index: 'SC_GoodsType.TypeName', width: 90 }
                , { label: '新分类号', name: 'FtypeNew', index: 'FtypeNew', width: 90, hidden: true }
                , { label: '新分类', name: 'FTypeNameNew', index: 'SC_GoodsTypeNew.TypeName', width: 90 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    shiftGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    shiftGrid.refreshGrid('1=1');
                }
            }
        });

   //清理记录
   var cleanGrid = new MyGrid({
            renderTo: 'info5'
            //, width: '100%'
            , autoWidth: true
            //, buttons: buttons0
            , height: 400
		    , storeURL:  '/SC_Fixed_Clean.mvc/Find'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 500
            , dialogHeight: 520
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '流水号', name: 'CleanNo', index: 'CleanNo', width: 100 }              
                , { label: '清理日期', name: 'CleanDate', index: 'CleanDate', formatter: 'date', width: 80 }
                , { label: '清理方式', name: 'CleanType', index: 'CleanType', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '申报人', name: 'ApplicantMan', index: 'ApplicantMan', width: 60 }
                , { label: '批准人', name: 'ApproveMan', index: 'ApproveMan', width: 60 }               
                , { label: '转售金额', name: 'ResalePrice', index: 'ResalePrice', width: 60 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    cleanGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    cleanGrid.refreshGrid('1=1');
                }
            }
        });

   //盘点结果记录
   var pandianGrid = new MyGrid({
          renderTo: 'info6' 
        , autoWidth: true
        //, buttons: buttons2
        , height: 400
		, storeURL: '/SC_Fixed_Result.mvc/Find'
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true 
        , sortOrder: 'DESC'
		,initArray: [{
			label: 'ID',
			name: 'ID',
			index: 'ID',
			width: 80,
			hidden: true
		},
		{
			label: 'FixedID',
			name: 'FixedID',
			index: 'FixedID',
			width: 80,
			hidden: true
		},
        {
			label: '批号',
			name: 'CheckNo',
			index: 'CheckNo',
			width: 130
		}, 
        {
			label: '开始盘点时间',
			name: 'CheckStartDate',
			index: 'CheckStartDate',
            formatter: 'datetime',
			width: 120
		}, 
        {
			label: '完成盘点时间',
			name: 'CheckEndDate',
			index: 'CheckEndDate',
            formatter: 'datetime',
			width: 120
		}, 
		{
			label: '电脑记录数量',
			name: 'AutoQuantity',
			index: 'AutoQuantity',
			width: 80
		},
		{
			label: '盘点数量',
			name: 'Quantity',
			index: 'Quantity',
			width: 80,
			editable: true,
		},
		{
			label: '盘点结果',
			name: 'CheckResult',
			index: 'CheckResult',
			width: 80
		}] 
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                pandianGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                pandianGrid.refreshGrid();
            } 
        }
    }); 

}