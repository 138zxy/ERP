function StuffSaleIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , advancedSearch: true
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 515
            , dialogHeight: 334
            , initArray: [
                 { label: '销售编号', name: 'ID', index: 'ID' }
                , { label: '运输车号', name: 'CarID', index: 'CarID' }
                , { label: '车牌号码', name: 'CarNo', index: 'CarNo' }
                , { label: '原料名称', name: 'StuffID', index: 'StuffID', hidden: true }
                , { label: '原料名称', name: 'StuffName', index: 'StuffName' }
                , { label: '收货单位', name: 'SupplyID', index: 'SupplyID' }
        //, { label: '收货单位', name: 'SupplyName', index: 'SupplyName' }
                , { label: '供货单位', name: 'CompanyID', index: 'CompanyID', hidden: true }
                , { label: '供货单位', name: 'CompName', index: 'CompName' }
                , { label: '毛重(KG)', name: 'TotalWeight', index: 'TotalWeight' }
                , { label: '皮重(KG)', name: 'CarWeight', index: 'CarWeight' }
                , { label: '净重(KG)', name: 'Weight', index: 'Weight' }
                , { label: '称重人', name: 'WeightMan', index: 'WeightMan' }
                , { label: '地磅名称', name: 'WeightName', index: 'WeightName' }
                , { label: '到场时间', name: 'ArriveTime', index: 'ArriveTime', width: 130, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '出站时间', name: 'DeliveryTime', index: 'DeliveryTime', width: 130, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 130 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '最后修改人', name: 'Modifier', index: 'Modifier', width: 130 }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }

            ]
        //代码生成新增，修改，删除，刷新的JS代码
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
                            $("#ID").val('');
                            var ss = $("#ID").val();
                            console.log(ss);
                            myJqGrid.getFormField("TotalWeight").bind('blur', function () {
                                var TotalNum_T = myJqGrid.getFormField("TotalWeight").val();
                                var CarWeight_T = myJqGrid.getFormField("CarWeight").val();
                                myJqGrid.getFormField("Weight").val(TotalNum_T - CarWeight_T);
                            });
                            myJqGrid.getFormField("CarWeight").bind('blur', function () {
                                var TotalNum_T = myJqGrid.getFormField("TotalWeight").val();
                                var CarWeight_T = myJqGrid.getFormField("CarWeight").val();
                                myJqGrid.getFormField("Weight").val(TotalNum_T - CarWeight_T);
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            var data = myJqGrid.getSelectedRecord();
                            $("#StuffID").val(data.StuffID);
                            myJqGrid.getFormField("TotalWeight").bind('blur', function () {
                                var TotalNum_T = myJqGrid.getFormField("TotalWeight").val();
                                var CarWeight_T = myJqGrid.getFormField("CarWeight").val();
                                myJqGrid.getFormField("Weight").val(TotalNum_T - CarWeight_T);
                            });
                            myJqGrid.getFormField("CarWeight").bind('blur', function () {
                                var TotalNum_T = myJqGrid.getFormField("TotalWeight").val();
                                var CarWeight_T = myJqGrid.getFormField("CarWeight").val();
                                myJqGrid.getFormField("Weight").val(TotalNum_T - CarWeight_T);
                            });
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}