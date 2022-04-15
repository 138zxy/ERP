function CarStatusIndexInit(opts) {
    $('#CarsType').height(gGridHeight + 80);
    var nodeid = 0;
    var nodeName = "";
    var carid = "";
    function booleanIsBackSearchValues() {
        return { '': '', 1: '已回', 0: '途中' };
    }
    function booleanIsBackSelectValues() {
        return { 'true': '已回', 'false': '途中' };
    }
    var myJqGrid = new MyGrid({
        renderTo: 'MyShowDiv'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: opts.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 5
		, showPageBar: true
		, initArray: [
                  { label: '运输单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, frozen: true }
                , { label: '是否带回', name: 'IsBack', index: 'IsBack', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '途中', '1': '已回' }, stype: 'select', searchoptions: { value: booleanIsBackSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanIsBackSelectValues()} }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 50, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '客户名称', name: 'CustName', index: 'CustName', width: 120 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 120 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 120 }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 120, search: false }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 55, align: 'right', search: false, editable: true, formatter: "number", formatoptions: { defaulValue: "", decimalPlaces: 1} }
           
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            } 
        }
    });


    window.imgonclick = function (CarStatus, carID) {
        carid = carID;
        myJqGrid.refreshGrid("CarID='" + carid + "'");
        $("#MyShowDiv").dialog("open");
    
    }
    $("#MyShowDiv").dialog({
        modal: true,
        autoOpen: false,
        width: 300,
        Height: 300,
        title: "当前车辆最近的运输任务",
        buttons: {
            '确认': function () {
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
    var scLimitList = $.fn.zTree.init($('#CarsType'), treeSettings);
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



    var CarStatus = "100";
    var typeNo = "车辆类型";
    //选择树节点事件
    function zTreeOnCheck(event, treeId, treeNode, clickFlag) {
        var treeObj2 = $.fn.zTree.getZTreeObj("CarsType");
        var node2 = treeObj2.getSelectedNodes(); //点击节点后 获取节点数据
        if (node2.length > 0) {
            typeNo = treeNode.typeNo;
            ShowNodeCar(CarStatus, typeNo);
        }
    }

    $("input[name=radio]").click(function () {
        CarStatus = $(this).val();
        ShowNodeCar(CarStatus, typeNo);
    });

    function ShowNodeCar(CarStatus, typeNo) {

        //车辆状态
        var status = 'CarStatus_' + CarStatus;
        //车辆类型
        var type = 'CarTypes_' + typeNo;

        if (CarStatus == '100' && typeNo == '车辆类型') {
            $(".showall").show();
        }
        else if (CarStatus == '100' && typeNo != '车辆类型') {
            $(".showall").hide();
            $("." + type).show();
        }
        else if (CarStatus != '100' && typeNo == '车辆类型') {
            $(".showall").hide();
            $("." + status).show();
        }
        else {
            $(".showall").hide();
            $("." + type + "." + status).show();
             
        }
    }
  
}
