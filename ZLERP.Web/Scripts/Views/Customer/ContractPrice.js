function yincang(){
//function ContractIndexInit(opts) {
//    function DataTypeValues() {
//        return { '': '', 0: '混凝土', 1: '湿拌', 2: '干混' };
//    }
//    //合同类型
//    function DataTypeStateFmt(cellvalue, options, rowObject) {
//        var style = "color:Blue;";
//        var txt = "";
//        if (cellvalue == 0) {
//            txt = "混凝土";
//        } else if (cellvalue == 1) {
//            style = "color:Green;";
//            txt = "湿拌";
//        } else if (cellvalue == 2 || cellvalue == -1) {
//            style = "color:Red;";
//            txt = "干混";
//        } else {
//            style = "color:black;";
//            txt = "您的合同状态有问题，请修复！";
//        }
//        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>';
//    }
//    var myJqGrid = new MyGrid({
//        renderTo: 'MyJqGrid'
//        //, width: '100%'
//            , autoWidth: true
//            , buttons: buttons0
//            , height: gGridHeight
//		    , storeURL: opts.storeUrl
//		    , sortByField: 'ID'
//		    , primaryKey: 'ID'
//		    , setGridPageSize: 30
//		    , showPageBar: true
//            //, groupField: 'CustName'
//            , dialogWidth: 500
//            , dialogHeight: 300
//		    , initArray: [
//                { label: '合同编号', name: 'ID', index: 'ID', width: 150 }
//                , { label: '数据来源', name: 'DataType', index: 'DataType', width: 150, formatter: DataTypeStateFmt, width: 50, searchoptions: { value: DataTypeValues() }, stype: 'select' }
//                , { label: '合同号', name: 'ContractNo', index: 'ContractNo', width: 80 }
//                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 250 }
//                , { label: '客户名称', name: 'CustName', index: 'CustName', width: 150 }
//                , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', width: 100 }
//                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 80 }
//                , { label: '预付款', name: 'PrePay', index: 'PrePay', width: 80, align: 'right', formatter: 'currency', hidden: true }
//                , { label: '期初应收款', name: 'PaidIn', index: 'PaidIn', width: 80, formatter: 'currency', hidden: true }
//                , { label: '期初已收款', name: 'PaidOut', index: 'PaidOut', width: 80, formatter: 'currency', hidden: true }
//                , { label: '期初未收款', name: 'PaidOwing', index: 'PaidOwing', width: 80, formatter: 'currency', hidden: true }

//                , { label: 'PaidFavourable', name: 'PaidFavourable', index: 'PaidFavourable', hidden: true, width: 80 }
//                , { label: '总应收砼款', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency', hidden: true }
//		        ]
//		    , autoLoad: true
//            , functions: {
//                handleReload: function (btn) {
//                    myJqGrid.reloadGrid();
//                },
//                handleRefresh: function (btn) {
//                    myJqGrid.refreshGrid('1=1');
//                },
//                handleAdd: function (btn) {
//                    myJqGrid.handleAdd({
//                        loadFrom: 'MyFormDiv',
//                        btn: btn,
//                        afterFormLoaded: function () {
//                            myJqGrid.setFormFieldReadOnly('ID', false);
//                            myJqGrid.getFormField("Val").bind('blur', function () {
                           
//                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
//                                var Val = myJqGrid.getFormField("Val").val();
//                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
//                            });

//                            myJqGrid.getFormField("UniPrice").bind('blur', function () {
//                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
//                                var Val = myJqGrid.getFormField("Val").val();
//                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
//                            });
//                        }
//                    });
//                },
//                handleEdit: function (btn) {
//                    myJqGrid.handleEdit({
//                        loadFrom: 'MyFormDiv',
//                        btn: btn,
//                        afterFormLoaded: function () {
//                            myJqGrid.setFormFieldReadOnly('ID', true);
//                            myJqGrid.getFormField("Val").bind('blur', function () {
//                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
//                                var Val = myJqGrid.getFormField("Val").val();
//                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
//                            });

//                            myJqGrid.getFormField("UniPrice").bind('blur', function () {
//                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
//                                var Val = myJqGrid.getFormField("Val").val();
//                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
//                            });
//                        }
//                    });
//                }
//                , handleDelete: function (btn) {
//                    myJqGrid.deleteRecord({
//                        deleteUrl: btn.data.Url
//                    });
//                }
//            }
//    });
//    //合同泵车价格设定
//    var ContractPumpGrid = new MyGrid({
//        renderTo: 'ContractPump',
//        autoWidth: true,
//        storeURL: opts.contractPumpStoreUrl,
//        sortByField: 'ID',
//        primaryKey: 'ID',
//        setGridPageSize: 30,
//        showPageBar: false,
//        //autoLoad: false,
//        singleSelect: true,
//        initArray: [{
//            label: ' 编号', name: 'ID', index: 'ID', hidden: true  },
//		{   label: ' 合同编号',  name: 'ContractID', index: 'ContractID', hidden: true },
//		{   label: ' 日期', name: 'SetDate',  index: 'SetDate', width: 100, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem)    { $(elem).datepicker(); }, sopt: ['ge']  } },
//		{   label: ' 浇筑方式', name: 'PumpType', index: 'PumpType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PumpType' }, width: 120, editable: true, edittype: 'select', editoptions: { value: dicToolbarSearchValues('CastMode') }, editrules: { required: true } },
//		{
//		    label: ' 浇筑价格',
//		    name: 'PumpPrice',
//		    index: 'PumpPrice',
//		    editable: true,
//		    formatter: 'currency',
//		    align: 'right',
//		    width: 100
//		}],
//        autoLoad: false,
//        functions: {
//            handleReload: function (btn) {
//                ContractPumpGrid.reloadGrid();
//            },
//            handleRefresh: function (btn) {
//                ContractPumpGrid.refreshGrid('1=1');
//            }
//        }
//    });
//    //特性设定
//    var identitySettingGridto = new MyGrid({
//        renderTo: 'identitySettingGridto'
//        , autoWidth: true
//        //, dialogWidth: 920
//        //, dialogHeight: 510
//        , storeURL: '/IdentitySetting.mvc/Find'
//        , sortByField: 'ID'
//        , primaryKey: 'ID'
//        , setGridPageSize: 100
//        //, autoLoad: false
//        , singleSelect: true
//        , editSaveUrl: '/IdentitySetting.mvc/Update'
//        , groupingView: { groupSummary: [false], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
//        , groupField: 'IdentityName'
//        , initArray: [
//              { label: '特性设定编号', name: 'IdentitySettingID', index: 'IdentitySettingID', hidden: true }
//            , { label: '定价日期', name: 'SetDate', index: 'SetDate', editable: true, width: 130, formatter: 'datetime', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge'] } }
//            , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
//            , { label: '特性类型', name: 'IdentityType', index: 'IdentityType', width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'IdenType' } }
//            , { label: '详细特性', name: 'IdentityName', index: 'IdentityName', width: 100 }
//            , { label: '特性价格', name: 'IdentityPrice', index: 'IdentityPrice', editable: true, formatter: 'currency', width: 100 }
//            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
//        ]
//        , functions: {
//            handleReload: function (btn) {
//                identitySettingGrid.reloadGrid();
//            },
//            handleRefresh: function (btn) {
//                identitySettingGrid.refreshGrid();
//            }
//        }
//    });

//    myJqGrid.addListeners('rowclick',
//	function (id, record, selBool) {
//	    ContractPumpGrid.refreshGrid("ContractID='" + id + "'");
//	    identitySettingGridto.refreshGrid("ContractID='" + id + "'");
//	});
//}
}

function ContractIndexInit(opts) {
    var initArray = [
            { label: '合同编号', name: 'ID', index: 'ID', width: 90, frozen: true }
                , { label: '合同号', name: 'ContractNo', index: 'ContractNo', width: 100, frozen: true }
                , { label: '客户名称', name: 'CustName', index: 'CustName', width: 150, frozen: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 150, frozen: true }
                , { label: '业务员', name: 'Salesman', index: 'Salesman', width: 60, frozen: true }
                , { label: '审核状态', name: 'Salesman', index: 'Salesman', width: 60, frozen: true }
                , { label: "运距", name: "Distance", index: "Distance", width: 60 }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 380 }
    ];
    var options = {
            autowidth: true
            , height: gGridWithTitleHeight
		    , url: opts.storeUrl
            , datatype: 'json'
            , mtype: 'POST'
            , jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                id: 'ID'
            }
		    , showPageBar: true
		    , sortable: false
            , sortname: 'ID'
            , sortorder: 'desc'
            , altRows: true
            , altclass: 'ui-priority-secondary'
		    , rowNum: 30
            , rowList: [10, 20, 30, 50, 100, 200]
            , autoLoad: true
            , pager: '#pgfrc1'
            , pagerpos: 'center'
            , recordpos: 'right'
            , multiselect: false
            , hidegrid: false
            , viewrecords: true
            , forceFit: false
            , shrinkToFit: false
            , colModel: initArray

    };
    //jQuery("#MyJqGrid").jqGrid(
    //    {
    //        autowidth: true
    //        , height: gGridWithTitleHeight
	//	    , url: opts.storeUrl
    //        , datatype: 'json'
    //        , mtype: 'POST'
    //        , jsonReader: {
    //            root: "rows",
    //            page: "page",
    //            total: "total",
    //            records: "records",
    //            repeatitems: false,
    //            id: 'ID'
    //        }
	//	    , showPageBar: true
	//	    , sortable: false
    //        , sortname: 'ID'
    //        , sortorder: 'desc'
    //        , altRows: true
    //        , altclass: 'ui-priority-secondary'
	//	    , rowNum: 30
    //        , rowList: [10, 20, 30, 50, 100, 200]
    //        , autoLoad: true
    //        , pager: '#pgfrc1'
    //        , pagerpos: 'center'
    //        , recordpos: 'right'
    //        , multiselect: false
    //        , hidegrid: false
    //        , viewrecords: true
    //        , forceFit: false
    //        , shrinkToFit: false
    //        , colModel: initArray
    //    });
    
    jQuery("#MyJqGrid").jqGrid('setFrozenColumns');
    //点击查询按钮
    jQuery("#idQuery").click(function () {
        //根据客户名称 获取查询后的数据并填充
        var custname = $("[name='CustName']").val();
        var data = { url: opts.storeUrl, datatype: "json", page: 1, postData: { condition: "CustName like '%" + custname + "%'" } };
        jQuery("#MyJqGrid").jqGrid('setGridParam', data).trigger("reloadGrid");
    });
    function getDynamicColAndCreateGrid(custname) {
        ajaxRequest(
                opts.storeUrl,
                { Datatype: "0" },
                false,
                function (response) {
                    var dynamicCol = new Array();
                    options.postData = { condition: "CustName like '%" + custname + "%'" };
                    //开始
                    ajaxRequest(
                            "/ConStrength.mvc/GetDynamicColByDataType",
                            { Datatype: "0" },
                            false,
                            function (response) {
                                var dynamicCol = new Array();
                                options.postData = { condition: "CustName like '%" + custname + "%'" };
                                console.log(options.postData);
                                $.each(response.ColumnData, function (i, n) {
                                    var cols = {
                                        label: n.label,
                                        name: n.name,
                                        index: n.index,
                                        width: isEmpty(n.width) ? 80 : n.width,
                                        align: n.align,
                                        editrules: { number: true },
                                        editable: n.editable,
                                        sortable: n.sortable
                                    };
                                    dynamicCol.push(cols);
                                });
                                var initArrayClone = initArray.slice(0);
                                initArrayCombin = $.merge(initArray, dynamicCol);
                                initArray = initArrayClone;
                                var plValue = '';

                                options.colModel = initArrayCombin;
                                jQuery("#MyJqGrid").jqGrid(options);
                                jQuery("#MyJqGrid").jqGrid('setFrozenColumns');
                                //hideOrShowCol();
                            }
                        );//结束
                    console.log(options.postData);
                    $.each(response.ColumnData, function (i, n) {
                        var cols = {
                            label: n.label,
                            name: n.name,
                            index: n.index,
                            width: isEmpty(n.width) ? 80 : n.width,
                            align: n.align,
                            editrules: { number: true },
                            editable: n.editable,
                            sortable: n.sortable
                        };
                        dynamicCol.push(cols);
                    });
                    var initArrayClone = initArray.slice(0);
                    initArrayCombin = $.merge(initArray, dynamicCol);
                    initArray = initArrayClone;
                    var plValue = '';
                    
                    options.colModel = initArrayCombin;
                    jQuery("#MyJqGrid").jqGrid(options);
                    jQuery("#MyJqGrid").jqGrid('setFrozenColumns');
                    //hideOrShowCol();
                }
            );
    }
    getDynamicColAndCreateGrid("杨博");
}

