function SalesmanIndexInit(options) {

    var UserGrid = new MyGrid({
        renderTo: "UserGrid"
            , title: "销售员列表"
            , autoWidth: true
            , buttons: buttons0
            , height: gGridWithTitleHeight / 2
            , dialogWidth: 800
            , dialogHeight: 600
            , storeURL: "/User.mvc/Find"
            , storeCondition: 'UserType=15'
            , sortByField: "ID"
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
            , initArray: [
			    { label: '登陆用户名', name: 'ID', index: 'ID', width: 100 }
                , { label: '真实姓名', name: 'TrueName', index: 'TrueName', width: 80 }
                , { label: 'Email', name: 'Email', index: 'Email' }
                , { label: '联系电话', name: 'Tel', index: 'Tel' }
                , { label: '手机', name: 'Mobile', index: 'Mobile' }
                , { label: '垫资额度', name: 'DZMoney', index: 'DZMoney', width: 50 }

		    ]
             , functions: {
                 handleReload: function (btn) {
                     UserGrid.reloadGrid("UserType=15");
                 },
                 handleRefresh: function (btn) {
                     UserGrid.refreshGrid("UserType=15");
                 }
                 //设置垫资额度
                 , handleAddDZ: function (btn) {
                     if (!UserGrid.isSelectedOnlyOne()) {
                         showMessage('提示', '请选择一条记录进行操作！');
                         return;
                     }
                     var selectrecord = UserGrid.getSelectedRecord();

                     UserGrid.handleAdd({
                         btn: btn,
                         width: 400,
                         height: 280,
                         loadFrom: "DZForm",
                         reloadGrid: false,
                         afterFormLoaded: function () {
                             UserGrid.setFormFieldValue("User.ID", selectrecord.ID);
                             UserGrid.setFormFieldValue("User.DZMoney", selectrecord.DZMoney);
                         }
                         , postCallBack: function (response) {
                             UserGrid.refreshGrid("UserType=15");
                         }
                     });
                 }
                 //增加未付
                 , handleAddWF: function (btn) {
                     if (!UserGrid.isSelectedOnlyOne()) {
                         showMessage('提示', '请选择一条记录进行操作！');
                         return;
                     }
                     var selectrecord = UserGrid.getSelectedRecord();

                     UserGrid.handleAdd({
                         btn: btn,
                         width: 400,
                         height: 280,
                         loadFrom: "WFForm",
                         reloadGrid: false,
                         afterFormLoaded: function () {
                             UserGrid.setFormFieldValue("SalesmanWF.UserID", selectrecord.ID);
                         }
                         , postCallBack: function (response) {
                             UserGrid.refreshGrid("UserType=15");
                         }
                     });
                 }

                 //增加扣款
                 , handleAddKK: function (btn) {
                     if (!UserGrid.isSelectedOnlyOne()) {
                         showMessage('提示', '请选择一条记录进行操作！');
                         return;
                     }
                     var selectrecord = UserGrid.getSelectedRecord();

                     UserGrid.handleAdd({
                         btn: btn,
                         width: 400,
                         height: 280,
                         loadFrom: "KKForm",
                         reloadGrid: false,
                         afterFormLoaded: function () {
                             UserGrid.setFormFieldValue("SalesmanKK.UserID", selectrecord.ID);
                         }
                         , postCallBack: function (response) {
                             UserGrid.refreshGrid("UserType=15");
                         }
                     });
                 }

                 //增加保险
                 , handleAddBX: function (btn) {
                     if (!UserGrid.isSelectedOnlyOne()) {
                         showMessage('提示', '请选择一条记录进行操作！');
                         return;
                     }
                     var selectrecord = UserGrid.getSelectedRecord();

                     UserGrid.handleAdd({
                         btn: btn,
                         width: 400,
                         height: 280,
                         loadFrom: "BXForm",
                         reloadGrid: false,
                         afterFormLoaded: function () {
                             UserGrid.setFormFieldValue("SalesmanBX.UserID", selectrecord.ID);
                         }
                         , postCallBack: function (response) {
                             UserGrid.refreshGrid("UserType=15");
                         }
                     });
                 }
             }
    });

    //未付
    var WFGrid = new MyGrid({
        renderTo: 'WFGrid'
            , title: "未付提成列表"
            , height: gGridWithTitleHeight / 2 - 100
            , autoWidth: true
            , storeURL: '/SalesmanWF.mvc/Find'
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, hidden: true }
                , { label: '序号', name: 'UserID', index: 'UserID', width: 50, hidden: true }
                 , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                     formatoptions: { keys: true, editbutton: false }
                 }
                , { label: '年份', name: 'WFYear', index: 'WFYear', align: 'center', width: 130 }
                , { label: '未付提成金额', name: 'WFMoney', index: 'WFMoney', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 80 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', align: 'center', formatter: 'datetime', width: 130 }
                , { label: '用户', name: 'Builder', index: 'Builder', width: 60, align: 'center' }
            ]
            , autoLoad: false
    });

    //垫资
    var DZGrid = new MyGrid({
        renderTo: 'DZGrid'
            , title: "垫资列表"
            , height: gGridWithTitleHeight / 3 - 50
            , autoWidth: true
            , storeURL: '/ContractDZ.mvc/Find'
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , groupingView: { groupSummary: [true], groupText: ['笔数({1})'] }
            , groupField: 'Builder'
            , emptyrecords: '无任何垫资记录'
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, sortable: false, hidden: true }
                , { label: '序号', name: 'CustomerID', index: 'CustomerID', width: 50, sortable: false, hidden: true }
                , { label: '客户', name: 'CustomerName', index: 'CustomerName', align: 'center', width: 300, sortable: false }
                , { label: '垫资金额', name: 'DZMoney', index: 'DZMoney', width: 80, sortable: false, summaryType: 'sum', summaryTpl: '{0}' }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 80, sortable: false }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', align: 'center', formatter: 'datetime', width: 130, sortable: false }
                , { label: '用户', name: 'Builder', index: 'Builder', width: 60, align: 'center', sortable: false }
            ]
            , autoLoad: false
    });

    //扣款
    var KKGrid = new MyGrid({
        renderTo: 'KKGrid'
            , title: "扣款列表"
            , height: gGridWithTitleHeight / 3 - 50
            , autoWidth: true
            , storeURL: '/SalesmanKK.mvc/Find'
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, hidden: true }
                , { label: '序号', name: 'UserID', index: 'UserID', width: 50, hidden: true }
                 , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                     formatoptions: { keys: true, editbutton: false }
                 }
                , { label: '扣款时间', name: 'KKTime', index: 'KKTime', align: 'center', formatter: 'date', width: 130 }
                , { label: '扣款金额', name: 'KKMoney', index: 'KKMoney', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 80 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', align: 'center', formatter: 'datetime', width: 130 }
                , { label: '用户', name: 'Builder', index: 'Builder', width: 60, align: 'center' }
            ]
            , autoLoad: false
    });

    //保险
    var BXGrid = new MyGrid({
        renderTo: 'BXGrid'
            , title: "保险列表"
            , height: gGridWithTitleHeight / 3 - 50
            , autoWidth: true
            , storeURL: '/SalesmanBX.mvc/Find'
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, sortable: false, hidden: true }
                , { label: '序号', name: 'UserID', index: 'UserID', width: 50, hidden: true }
                 , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                     formatoptions: { keys: true, editbutton: false }
                 }
                , { label: '交保时间', name: 'BXTime', index: 'BXTime', align: 'center', formatter: 'date', width: 130 }
                , { label: '保险金额', name: 'BXMoney', index: 'BXMoney', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 80 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', align: 'center', formatter: 'datetime', width: 130 }
                , { label: '用户', name: 'Builder', index: 'Builder', width: 60, align: 'center' }
            ]
            , autoLoad: false
    });

    KKGrid.addListeners("gridComplete", function () {
        KKGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/SalesmanKK.mvc/Delete" });
    });
    BXGrid.addListeners("gridComplete", function () {
        BXGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/SalesmanBX.mvc/Delete" });
    });
    WFGrid.addListeners("gridComplete", function () {
        WFGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/SalesmanWF.mvc/Delete" });
    });
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    UserGrid.addListeners('rowclick', function (id, record, selBool) {
        DZGrid.refreshGrid("Builder='" + id + "'");
        KKGrid.refreshGrid("UserID='" + id + "'");
        BXGrid.refreshGrid("UserID='" + id + "'");
        WFGrid.refreshGrid("UserID='" + id + "'");
    });
}