function handlerecordIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            // , storeCondition: "stuffname!='水'"
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '筒仓编码', name: 'SiloID', index: 'SiloID', width: 100 }
                , { label: '筒仓名称', name: 'SiloName', index: 'SiloName', width: 100 }
                , { label: '材料编码', name: 'StuffID', index: 'StuffID', width: 100 }
                , { label: '材料名称', name: 'StuffName', index: 'StuffInfo.StuffName', width: 100 }
                , { label: '材料用量', name: 'StuffAmount', index: 'StuffAmount', width: 100 }
                , { label: '生产线名称', name: 'ProductLineName', index: 'ProductLineName', width: 100 }
                , { label: '操作员', name: 'Builder', index: 'Builder', width: 130 }
                , { label: '操作时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 100 }
                //, { label: '生产线编号', name: 'ProductLineID', index: 'ProductLineID' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    //myJqGrid.refreshGrid("1=1 and stuffname!='水'");
                    myJqGrid.refreshGrid("1=1");
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDivItems',
                        btn: btn,
                        width: 400,
                        height:200
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    $('#StuffID').bind('blur', function () {
        bindSelectData($('#SiloID'), opts.findSiloUrl, { condition: "stuffid='" + $(this).val() + "'" });

        var txt = $('#StuffID').find("option:selected").text();
        //alert(txt);
        
        $("input[name='StuffName']").val(txt);
    });

    $('#SiloID').bind('blur', function () {
        var txt = $('#SiloID').find("option:selected").text();
        //alert(txt);
        $("input[name='SiloName']").val(txt);
    });
}