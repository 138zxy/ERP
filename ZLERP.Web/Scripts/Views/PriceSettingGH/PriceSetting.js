//原材料销售合同页面
function priceSettingIndexInit(options) {
    //销售合同网格
    var contractGrid = new MyGrid({
        renderTo: "ContractDiv"
            , title: "销售合同"
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: "buttonToolbar"
            , height: gGridHeight
            , dialogWidth: 700
            , dialogHeight: 350
            , storeURL: options.ContractStoreUrl
            , storeCondition: ""
            , sortByField: "ID"
            , sortOrder: 'DESC'
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
            , singleSelect: true
            , initArray: [
                  { label: '合同编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '客户编码', name: 'CustomerID', index: 'CustomerID', hidden: false }
                , { label: '客户名称', name: 'CustName', index: 'Customer.CustName', width: 200 }
                , { label: '合同号', name: 'ContractNo', index: 'ContractNo', hidden: false }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 180 }
                , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', hidden: false }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', hidden: false }
                , { label: '签订总方量', name: 'SignTotalCube', index: 'SignTotalCube', width: 100, hidden: false }
                , { label: '签订日期', name: 'SignDate', index: 'SignDate', formatter: 'date', hidden: false }
            ]
            , functions: {
                handleReload: function (btn) {
                    contractGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    contractGrid.refreshGrid("1 = 1");
                }
            }//functions
    });

    //合同明细网格
    var contractItemGrid = new MyGrid({
        renderTo: "ContractItemDiv"
            , title: "合同明细"
            , autoWidth: true
        //            , buttons: buttons0
        //            , buttonRenderTo: "buttonToolbar"
            , height: gGridHeight / 3 - 55
            , dialogWidth: 700
            , dialogHeight: 20
            , storeURL: options.ContractItemStoreUrl 
            , storeCondition: "1<>1"
            , sortByField: "ID"
            , sortOrder: 'ASC'
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
            , singleSelect: true
            , autoLoad: true
            , initArray: [
                  { label: '合同明细编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 50, align: 'center' }
            ]
            , functions: {
                handleReload: function (btn) {
                    contractItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    contractItemGrid.refreshGrid("1=1");
                }
            }
    });

    //合同明细价格列表
    var contractItemPriceGrid = new MyGrid({
        renderTo: "ContractItemPriceDiv"
        , title: "销售价格"
        , autoWidth: true
        //         , buttons: buttons0
        //         , buttonRenderTo: "buttonToolbar"
        , height: gGridHeight / 3 - 55
        , dialogWidth: 700
        , dialogHeight: 20
        , storeURL: options.ContractItemPriceStoreUrl
        , storeCondition: "1<>1"
        , sortByField: "ChangeTime"
        , sortOrder: 'DESC'
        , primaryKey: "ID"
        , setGridPageSize: 30
        , showPageBar: true
        , singleSelect: true
        , initArray: [
              { label: "合同明细价格编号", name: "ID", index: "ID", width: 65, hidden: true }
            , { label: "合同明细编号", name: "ContractItemsID", index: "ContractItemsID", align: "center", width: 100, hidden: true }
            , { label: "改价时间", name: "ChangeTime", index: "ChangeTime", formatter: "datetime", width: 80, align: "center" }
            , { label: "销售价格(元/方)", name: "UnPumpPrice", index: "UnPumpPrice"}
            , { label: "建立人", name: "Builder", index: "Builder" }
            , { label: "建立时间", name: "BuildTime", index: "BuildTime", formatter: "date", width: 80 }
            , { label: "修改人", name: "Modifier", index: "Modifier" }
            , { label: "修改时间", name: "ModifyTime", index: "ModifyTime", formatter: "date", width: 80 }
        ]
        , autoLoad: true
        , functions: {
            handleReload: function (btn) {
                contractItemPriceGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                contractItemPriceGrid.refreshGrid("1=1");
            }
        }
    });

    

    //合同工程列表
    var projectGrid = new MyGrid({
        renderTo: "ProjectDiv"
        , title: "合同工程"
        , autoWidth: true
        //         , buttons: buttons0
        //         , buttonRenderTo: "buttonToolbar"
        , height: gGridHeight / 3 - 55
        , dialogWidth: 700
        , dialogHeight: 20
        , storeURL: options.ProjectStoreUrl
        , storeCondition: "1<>1"
        , sortByField: "ID"
        , sortOrder: 'DESC'
        , primaryKey: "ID"
        , setGridPageSize: 30
        , showPageBar: true
        , singleSelect: true
        , initArray: [
              { label: "工程编号", name: "ID", index: "ID", width: 65 }
            , { label: "工程名称", name: "ProjectName", index: "ProjectName", width: 100 }
            , { label: "工程运距", name: "Distance", index: "Distance", width: 80, align: "center" }
            , { label: "建立人", name: "Builder", index: "Builder" }
            , { label: "建立时间", name: "BuildTime", index: "BuildTime", formatter: "date", width: 80 }
            , { label: "修改人", name: "Modifier", index: "Modifier" }
            , { label: "修改时间", name: "ModifyTime", index: "ModifyTime", formatter: "date", width: 80 }
        ]
        , autoLoad: true
        , functions: {
            handleReload: function (btn) {
                projectGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                projectGrid.refreshGrid("1=1");
            }
        }
    });


    //点击“销售合同”行， 刷新 销售明细网格， 刷新 工程网格
    contractGrid.addListeners("rowclick", function (id, record, selBool) {
        contractItemGrid.refreshGrid("ContractID='" + id + "'");
        contractItemPriceGrid.refreshGrid("1<>1");

        contractPumpGrid.refreshGrid("ContractID='" + id + "'");
        contractPumpPriceGrid.refreshGrid("1<>1");

        projectGrid.refreshGrid("ContractID='" + id + "'");
    });

    //选择“销售明细”行，刷新 材料价格网格
    contractItemGrid.addListeners("rowclick", function (id, record, selBool) {
        contractItemPriceGrid.refreshGrid("ContractItemsID='" + id + "'");
    });

    //“销售明细”网格，单元格(禁用行编辑）单击刷新 材料价格网格
    contractPumpGrid.addListeners("rowclick", function (id, record, selBool) {
        contractPumpPriceGrid.refreshGrid("ContractPumpID='" + id + "'");
    });


    //Ajax获取“销售材料”的信息, 用于绑定 “销售明细”网格中的编辑控件
    function getStuffInfoSelectStr() {
        var selectStr = "";
        $.ajax({
            url: options.stuffInfoFindInfo,
            async: false,
            dataType: "json",
            success: function (data) {
                if ($.isArray(data)) {
                    selectStr += $.map(data, function (stuffItem, i) {
                        return stuffItem.ID + ":" + stuffItem.StuffName + "(" + stuffItem.ID + ")";
                    }).join(";");
                }
            }
        });
        return selectStr;
    }

    //tool function
    //Ajax获取Dic中项次，拼接字符串
    //用于Grid中行编辑的下拉列表控件
    //MyGrid中有自定义的扩展 dicToolbarSearchValues
    //报废
    function getDicStringForGridEditSelect(dicParentID) {
        var optionsString = "";
        if (options.dicFindDicsListUrl && dicParentID) {
            $.ajax({
                url: options.dicFindDicsListUrl,
                async: false,
                data: { nodeid: dicParentID },
                dataType: "json",
                success: function (data) {
                    if ($.isArray(data)) {
                        optionsString += $.map(data, function (dicItem, i) {
                            return dicItem.ID + ":" + dicItem.DicName + "(" + dicItem.ID + ")";
                        }).join(";");
                    }
                }
            });
        }

        return optionsString;
    }



}


