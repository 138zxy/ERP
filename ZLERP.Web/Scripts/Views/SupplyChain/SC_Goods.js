function SC_GoodsIndexInit(opts) {
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
            url: opts.scTreeUrl,
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
            var str = '';
            str = getAllChildrenNodes(treeNode, str);
            str = str + ',' + treeNode.id; // 加上被选择节点自己
            var ids = str.substring(1, str.length); // 去掉最前面的逗号
            var idsArray = ids.split(','); // 得到所有节点ID 的数组
            var length = idsArray.length; // 得到节点总数量
            console.log(idsArray);
            myJqGrid.refreshGrid("TypeNo IN(" + idsArray + ")");
            typeno = treeNode.id;typename = treeNode.name;
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
    var typeno = "", typename = "";var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 400,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        storeCondition: "IsOff=0",
        sortOrder: 'ASC',
        editSaveUrl: "/SC_Goods.mvc/Update",
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
        {
            label: 'TypeNo', name: 'TypeNo', index: 'TypeNo', width: 80, hidden: true
        },
        {
            label: '编码', name: 'GoodsCode', index: 'GoodsCode', width: 60
        },
		{
		    label: '品名', name: 'GoodsName', index: 'GoodsName', width: 100
		},
        {
            label: '拼音简码',
            name: 'BrevityCode',
            index: 'BrevityCode',
            width: 80
        },
		{
		    label: '条码', name: 'GoodsNo', index: 'GoodsNo', width: 100
		},
		{
		    label: '规格',
		    name: 'Spec',
		    index: 'Spec',
		    width: 100
		},
		{
		    label: '最小计量单位', name: 'Unit', index: 'Unit', width: 60
		},
		{
		    label: '预警下限', name: 'MinWarning', index: 'MinWarning', width: 60, editable: true, formatter: "number"
		},
        {
            label: '预警上限', name: 'MaxWarning', index: 'MaxWarning', width: 60, editable: true, formatter: "number"
        },
		{
		    label: '品牌',
		    name: 'Brand',
		    index: 'Brand',
		    width: 80
		},
		{
		    label: '分类',
		    name: 'TypeName',
		    index: 'TypeName',
		    width: 60
		},
		{
		    label: '最近购买价',
		    name: 'Price',
		    index: 'Price',
		    width: 80,
		    formatter: 'currency'
		},
		{
		    label: '参考价1',
		    name: 'Price2',
		    index: 'Price2',
		    width: 80,
		    formatter: 'currency'
		},
		{
		    label: '参考价2',
		    name: 'Price3',
		    index: 'Price3',
		    width: 80,
		    formatter: 'currency'
		},
		{
		    label: '库存总金额',
		    name: 'LibMoney',
		    index: 'LibMoney',
		    width: 100
		},
		{
		    label: '商家编码',
		    name: 'SellerNo',
		    index: 'SellerNo',
		    width: 100
		},
		 { label: "商品文件", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false }
		,
		{
		    label: '是否停用',
		    name: 'IsOff',
		    index: 'IsOff',
		    width: 80,
		    formatter: booleanFmt,
		    unformat: booleanUnFmt,
		    stype: "select",
		    searchoptions: {
		        value: booleanSearchValues()
		    }
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		}
        ,
		{
		    label: '启用辅助单位', name: 'IsAuxiliaryUnit', index: 'IsAuxiliaryUnit', formatter: booleanFmt, unformat: booleanUnFmt, width: 80
		}
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
                if (typeno == "" || typename == "") {
                    showMessage('提示', '请先从左方树状图中选择商品分类！');
                    return;
                }
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    width: 600,
                    height: 420,
                    afterFormLoaded: function () {
                        $("#Attachments").empty();
                        goodsUnitJqGrid.refreshGrid("1=2");
                        $("#formula-tabs").tabs({ disabled: [1] });
                        myJqGrid.setFormFieldValue('TypeNo', typeno);
                        myJqGrid.setFormFieldValue('TypeNo_text', typename);
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(response.Data);
                        }
                    }
                });
            },
            handleEdit: function (btn) {
                var selectrecord = myJqGrid.getSelectedRecord();
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    width: 600,
                    height: 420,
                    afterFormLoaded: function () {
                        //附件
                        var attDiv = $("#Attachments");
                        attDiv.empty();
                        attDiv.append(selectrecord["Attachments"]);
                        $("a[att-id]").show();
                        goodsUnitJqGrid.refreshGrid("GoodsID='" + selectrecord.ID + "'");
                        if (selectrecord.IsAuxiliaryUnit == "true") {
                            $("#formula-tabs").tabs("enable", 1);
                        }
                        else {
                            $("#formula-tabs").tabs({ disabled: [1] });

                        }
                    }, postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(selectrecord.ID);
                        }
                    }
                });
            },
            handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            handleAddType: function (btn) {
                $("#ParentType").val('');
                $("#OrderNo").val('');
                var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
                var node = treeObj.getSelectedNodes();
                if (node.length > 0) {
                    var nodeid = node[0].id;
                    var nodeName = node[0].name;
                    $("#ParentType").val(nodeName);
                    $("#MyGoodsTypeFormDiv").dialog("open");
                } else {
                    showMessage('提示', '请选择父类进行添加！');
                    return;
                }
            },
            handleEditType: function (btn) {
                var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
                var node = treeObj.getSelectedNodes();
                if (node.length > 0) {
                    nodeid = node[0].id;
                    nodeName = node[0].name;
                    if (nodeName == "商品分类") {
                        showMessage('提示', '不能修改删除跟节点！');
                        return;
                    }
                    $("#TypeNameEdit").val(nodeName);
                    $("#MyGoodsTypeEditFormDiv").dialog("open");
                } else {
                    showMessage('提示', '请选择节点进行修改！');
                    return;
                }
            },
            handleDeleteType: function (btn) {
                var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
                var node = treeObj.getSelectedNodes(); //点击节点后 获取节点数据
                if (node.length > 0) {
                    nodeid = node[0].id;
                    nodeName = node[0].name;
                    if (nodeName == "商品分类") {
                        showMessage('提示', '不能修改删除跟节点！');
                        return;
                    }
                    showConfirm("确认信息", "您确定删除此项分类:" + nodeName + "？",
					function () {
					    var requestURL = opts.scDeleteType;
					    ajaxRequest(requestURL, {
					        id: nodeid
					    },
						false,
						function (response) {
						    if (!!response.Result) {
						        showMessage('提示', '删除成功！');
						        var jobtree = $.fn.zTree.init($('#GoodgType'), treeSettings);
						        jobtree.expandAll(true);
						    } else {
						        showMessage('提示', response.Message);
						    }
						});
					});
                } else {
                    showMessage('提示', '请选择一个类进行删除！');
                    return;
                }
            },
            LoadExcel: function (btn) {
                myJqGrid.showWindow({
                    loadFrom: 'MyFormDiv1',
                    btn: btn,
                    width: 500,
                    height: 200,
                    afterFormLoaded: function () {

                    }, buttons: {
                        "立即导入": function () {
                            console.log("导入开始");
                            attachmentUpload1();
                            $(this).dialog("close");
                        },
                        "关闭": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            },
            handleIsOff: function () {
                myJqGrid.refreshGrid("IsOff = 1");
            },
            handleIsOn: function () {
                myJqGrid.refreshGrid("IsOff = 0");
            },
            handleAll: function () {
                myJqGrid.refreshGrid("1 = 1");
            }

        }
    });

    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            myJqGrid.getJqGrid().setCell(cl, "LibMoney", '', { color: 'red' }, ''); 
        } 
    });

    $("#MyGoodsTypeFormDiv").dialog({
        modal: true,
        autoOpen: false,
        width: 400,
        maxHeight: 400,
        buttons: {
            '确认': function () {
                var TypeName = $("#TypeName").val();
                var OrderNo = $("#OrderNo").val();
                if (!TypeName || TypeName == '') {
                    showMessage('提示', '请填写子类名称！');
                    return;
                }
                var nodeid = 0;
                var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
                var node = treeObj.getSelectedNodes();
                if (node.length > 0) {
                    nodeid = node[0].id;
                }
                if (nodeid == 0) {
                    showMessage('提示', '请选择节点！');
                    return;
                }
                var requestURL = opts.scAddType;
                ajaxRequest(requestURL, {
                    id: nodeid,
                    typeName: TypeName,
                    orderNo: OrderNo,
                    flag:1
                },
				false,
				function (response) {
				    if (!!response.Result) {
				        showMessage('提示', '保存成功！');
				        var jobtree = $.fn.zTree.init($('#GoodgType'), treeSettings);
				        jobtree.expandAll(true);
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

    $("#MyGoodsTypeEditFormDiv").dialog({
        modal: true,
        autoOpen: false,
        width: 400,
        maxHeight: 400,
        buttons: {
            '确认': function () {
                var TypeName = $("#TypeNameEdit").val();
                var OrderNo = $("#OrderNoEdit").val();
                if (!TypeName || TypeName == '') {
                    showMessage('提示', '请填写分类名称！');
                    return;
                }
                var nodeid = 0;
                var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
                var node = treeObj.getSelectedNodes();
                if (node.length > 0) {
                    nodeid = node[0].id;
                }
                if (nodeid == 0) {
                    showMessage('提示', '请选择节点！');
                    return;
                }
                console.log("请选择节点：" + nodeid);
                var requestURL = opts.scEditType;
                ajaxRequest(requestURL, { 
                    id: nodeid,
                    typeName: TypeName,
                    orderNo: OrderNo,
                    flag:1
                },
				false,
				function (response) {
				    if (!!response.Result) {
				        showMessage('提示', '保存成功！');
				        var jobtree = $.fn.zTree.init($('#GoodgType'), treeSettings);
				        jobtree.expandAll(true);
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

    var goodsUnitJqGrid = new MyGrid({
        renderTo: 'SC_GoodsUnitGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight / 3
		    , storeURL: '/SC_GoodsUnit.mvc/Find'
		    , sortByField: 'Rate'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '商品编码', name: 'GoodsID', index: 'GoodsID', width: 70 }
                , { label: '描述', name: 'UnitDesc', index: 'UnitDesc', width: 60 }
                , { label: '单位', name: 'Unit', index: 'Unit', width: 60 }
                , { label: '比率', name: 'Rate', index: 'Rate', width: 60 }
                , { label: '备注', name: 'Meno', index: 'Meno', width: 80 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    goodsUnitJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    goodsUnitJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();
                    goodsUnitJqGrid.handleAdd({
                        loadFrom: 'GoodsUnitFormDiv',
                        btn: btn,
                        width: 300,
                        height: 280,
                        prefix: "SC_GoodsUnit",
                        afterFormLoaded: function () {
                            goodsUnitJqGrid.getFormField("GoodsID").val(Record.ID);
                            $("#baseunit").html(Record.Unit);
                        }
                    });
                },
                handleEdit: function (btn) {
                    var Record = myJqGrid.getSelectedRecord();
                    var RecordD = goodsUnitJqGrid.getSelectedRecord();
                    if (RecordD.UnitDesc == "最小计量单位") {
                        showMessage('提示', "最小计量单位不可修改");
                        return;
                    }
                    goodsUnitJqGrid.handleEdit({
                        loadFrom: 'GoodsUnitFormDiv',
                        btn: btn,
                        width: 300,
                        height: 280,
                        prefix: "SC_GoodsUnit",
                        afterFormLoaded: function () {
                            goodsUnitJqGrid.getFormField("ID").val(RecordD.ID);
                            goodsUnitJqGrid.getFormField("GoodsID").val(Record.ID);
                            goodsUnitJqGrid.getFormField("UnitDesc").val(RecordD.UnitDesc);
                            goodsUnitJqGrid.getFormField("Unit").val(RecordD.Unit);
                            goodsUnitJqGrid.getFormField("Rate").val(RecordD.Rate);
                            goodsUnitJqGrid.getFormField("Meno").val(RecordD.Meno);
                            //
                            $("#baseunit").html(Record.Unit);
                        }, beforeSubmit: function () {
                            return true;
                        }
                    });
                }
                , handleDelete: function (btn) {
                    goodsUnitJqGrid.deleteRecord({
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
                loadFrom: 'MyFormDiv',
                title: '查看详细', 
                width: 600,
                height: 420,
                buttons: {
                    "关闭": function () {
                        $(this).dialog('close');
                    }
                },
                afterFormLoaded: function () {
                    $("#tb_IsBath").hide();
                    $("input[name='TypeNo_text']").val(TypeName);
                }
            });
        });
        $("#IsAuxiliaryUnit").change(function () {
            var ischecked = document.getElementById("IsAuxiliaryUnit").checked;
            console.log("Unit = " + $("select[name=Unit]").eq(1).val());
            if (ischecked) {
                if (!$("select[name=Unit]").eq(1).val()) {
                    showMessage('提示', "请先选择最小计量单位!");
                    return;
                }

                $("#formula-tabs").tabs("enable", 1); //启用第二个选项卡
            }
            else {
                $("#formula-tabs").tabs({ disabled: [1] }); //禁用第二个选项卡
            }
        });
   $("#GoodsName").change(function () {
            var str = $("#GoodsName").val();
            ajaxRequest(opts.LP_TOPY_SHORTUrl, {
                str: str
            },
		false,
		function (response) {
		    if (response.Result) {
		        $("#BrevityCode").val(response.Data);

		    } else {
		        showMessage('提示', response.Message);
		    }
		});
        });
    //上传附件
    function attachmentUpload(objectId) {
            var fileElement = $("input[type=file]");
            if (fileElement.val() == "") return;

            $.ajaxFileUpload
            ({
                url: opts.uploadUrl + "?objectType=SC_Goods&objectId=" + objectId,
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
        //上传附件
        function attachmentUpload1() {
             
            var fileElement = $("#uploadFile1"); 
            if (fileElement.val() == "") return;
            
            $.ajaxFileUpload({
                url: opts.uploadUrl,
                secureuri: false,
                fileElementId: "uploadFile1",
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