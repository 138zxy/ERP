function SC_NowLibIniIndexInit(opts) {
    $('#GoodgType').height(gGridHeight + 80);
    var nodeid = 0;
    var nodeName = "";
    var myJqGridIni = new MyGrid({
        renderTo: 'MyJqGrid', 
        autoWidth: true,
       // buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl,
        storeCondition: "IsOff=0",
        sortByField: 'ID',
        dialogWidth: 200,
        dialogHeight: 200,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        sortOrder: 'ASC',
        pageSize: 100,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
        {
            label: '商品编码',
            name: 'GoodsCode',
            index: 'GoodsCode',
            width: 100
        },
		{
		    label: '品名',
		    name: 'GoodsName',
		    index: 'GoodsName',
		    width: 80
		}, 
		{
		    label: '规格',
		    name: 'Spec',
		    index: 'Spec',
		    width: 100
		},
		{
		    label: '单位',
		    name: 'Unit',
		    index: 'Unit',
		    width: 40
		},  
		{
		    label: '分类',
		    name: 'TypeName',
		    index: 'TypeName',
		    width: 60
		},  
		{
		    label: '是否停用',
		    name: 'IsOff',
		    index: 'IsOff',
		    width: 40,
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
		}],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGridIni.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridIni.refreshGrid();
            } 
        }
    }); 
    window.showLib = function () { 
        $("#MyGoodsShowDiv").dialog("open"); 
    }

    $("#MyGoodsShowDiv").dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        Height: 500,
        title:"库存初始化（勾选商品并选择初始化仓库）",
        buttons: {
            '确认': function () {
                var keys = myJqGridIni.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var LibId = $("input[type='radio']:checked").val();
                if (LibId <= 0) {
                    showMessage('提示', "请选择初始化的仓库!");
                    return;
                } 
                var requestURL = opts.LibIn;
                ajaxRequest(requestURL, {
                    keys: keys,
                    libId: LibId
                },
				false,
				function (response) {
				    if (!!response.Result) {
				        showMessage('提示', '初始化数据成功，界面将跳转到入库管理！');
				        myJqGridIni.refreshGrid();
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

//    function zTreeOnCheck() {
//        var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
//        var node = treeObj.getSelectedNodes(); //点击节点后 获取节点数据
//        if (node.length > 0) {
//            var id = node[0].id;
//            var name = node[0].name
//            var typestring = node[0].typeNo;
//            myJqGridIni.refreshGrid("typestring like '" + typestring + "%'");

//        }
//    }

    //选择树节点事件
    function zTreeOnCheck(event, treeId, treeNode, clickFlag) {
        var treeObj2 = $.fn.zTree.getZTreeObj("GoodgType");
        var node2 = treeObj2.getSelectedNodes(); //点击节点后 获取节点数据
        if (node2.length > 0) {
            var str = '';
            str = getAllChildrenNodes(treeNode, str);
            str = str + ',' + treeNode.id; // 加上被选择节点自己
            var ids = str.substring(1, str.length); // 去掉最前面的逗号
            var idsArray = ids.split(','); // 得到所有节点ID 的数组
            var length = idsArray.length; // 得到节点总数量
            myJqGridIni.refreshGrid("TypeNo IN(" + idsArray + ")");
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
}