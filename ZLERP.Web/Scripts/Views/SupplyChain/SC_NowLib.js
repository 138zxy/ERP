function SC_NowLibIndexInit(opts) {
    $('#GoodgType').height(gGridHeight + 80);
    var nodeid = 0;
    var LibId = $("input[type='radio']:checked").val();
    var libconditon = "( SC_Goods.IsOff=0)";
    if (LibId == 0) {
        var libconditon = "( SC_Goods.IsOff=0 )"; //and Quantity!=0
    }
    else {
        var libconditon = "(LibID=" + LibId + " and SC_Goods.IsOff=0 )"; //and Quantity!=0
    }
    var nodeName = ""; 
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid',
        autoWidth: true,
        buttons: buttons0,
        height: gGridHeight,
        storeURL: opts.storeUrl,
        storeCondition:libconditon,
        sortByField: 'SC_Goods.GoodsName',
        dialogWidth: 480,
        dialogHeight: 400,
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
            label: '仓库',
            name: 'SC_Lib.LibName',
            index: 'SC_Lib.LibName',
            width: 80
        },
        {
        	label: '商品编码',
        	name: 'SC_Goods.GoodsCode',
        	index: 'SC_Goods.GoodsCode',
        	width: 80
        },
		{
		    label: '品名',
		    name: 'SC_Goods.GoodsName',
		    index: 'SC_Goods.GoodsName',
		    width: 80
		},
        
		{
		    label: '规格',
		    name: 'SC_Goods.Spec',
		    index: 'SC_Goods.Spec',
		    width: 80
		},  
		{
		    label: '分类',
		    name: 'SC_Goods.SC_GoodsType.TypeName',
		    index: 'SC_Goods.SC_GoodsType.TypeName',
		    width: 80
		},
		{
		    label: '库存均价',
		    name: 'PirceUnit',
		    index: 'PirceUnit',
		    width: 60,
            formatter: 'currency'
		},
        {
            label: '数量',
            name: 'Quantity',
            index: 'Quantity',
            width: 60
        },
        {
            label: '单位',
            name: 'SC_Goods.Unit',
            index: 'SC_Goods.Unit',
            width: 40
        },
        {
            label: '辅助数量',
            name: 'AuxiliaryUnit',
            index: 'AuxiliaryUnit',
            width: 80
        },
     
        {
		    label: '库存总金额',
		    name: 'LibMoney',
		    index: 'LibMoney',
		    width: 80,
		    formatter: 'currency'
		}, 
		{
		    label: '最后入库异动日期',
		    name: 'InDate',
		    index: 'InDate',
		    formatter: 'datetime',
		    width: 120
		},
        {
            label: '最后出库异动日期',
            name: 'OutDate',
            index: 'OutDate',
            formatter: 'datetime',
            width: 120
        },
         {
            label: '低预警',
            name: 'Warning',
            index: 'Warning',
            width: 80
        },
        {
            label: '高预警',
            name: 'UpWarning',
            index: 'UpWarning',
            width: 80
        }],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            } 
        }
    });
    window.Search = function (LibId) {
//        console.log("选择仓库查询");
        if (LibId == 0) {
            libconditon = "(SC_Goods.IsOff=0)";
        }
        else {
            libconditon = "(LibID=" + LibId + "and SC_Goods.IsOff=0)";
        }
        myJqGrid.reloadGrid(libconditon);
    }


    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            myJqGrid.getJqGrid().setCell(cl, "Warning", '', { color: 'red' }, '');
            myJqGrid.getJqGrid().setCell(cl, "UpWarning", '', { color: 'red' }, ''); 
        }
    })

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
               treeObj.expandNode(nodes[i], true, true, true);
           }
       }
   } 
 
    var streeNode;
    function zTreeOnCheck(event, treeId, treeNode, clickFlag) {
        streeNode = treeNode;
        var condition = GetSearch();
        myJqGrid.refreshGrid(condition);
    }

    //绑定所有radio的事件
    $("input:radio").change(function () {
        var condition = GetSearch();
        myJqGrid.refreshGrid(condition);
    });

    var Search = function () {
        var condition = GetSearch();
        myJqGrid.refreshGrid(condition);
    }

    function GetSearch() {
        //拼接查询字符串中的仓库条件
        var checkedLibID = $("input:radio:checked").val();

        var condition;
        if (checkedLibID == "0") {
            condition = "(SC_Goods.IsOff=0)"  //所有仓库
        } else {
            condition = "(LibID =" + checkedLibID + "and SC_Goods.IsOff=0)";
        }
        
        //拼接查询字符串中的商品类别
        var treeObj = $.fn.zTree.getZTreeObj("GoodgType");
        var node = treeObj.getSelectedNodes();
        if (node.length > 0) {
            var str = '';
            str = getAllChildrenNodes(streeNode, str);
            str = str + ',' + streeNode.id; // 加上被选择节点自己
            var ids = str.substring(1, str.length); // 去掉最前面的逗号
            var idsArray = ids.split(','); // 得到所有节点ID 的数组
            var length = idsArray.length; // 得到节点总数量
//            console.log(idsArray);
            condition += " and SC_Goods.TypeNo IN(" + idsArray + ")";
        }

        return condition;
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
