function lab_reporttypeconfigIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupField: 'LabReportTypeID'
            , advancedSearch: true
            , groupingView: { groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], groupSummary: [false], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID',hidden:true }
                , { label: '试验报告类型', name: 'LabReportTypeID', index: 'LabReportTypeID',width:200, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "LabTemplateType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("LabTemplateType")} }
		        , { label: '材料类型编码', name: 'StuffTypeID', index: 'StuffTypeID', hidden: true }
                , { label: '材料类型', name: 'StuffTypeName', index: 'StuffType.StuffTypeName' }
            ]
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
                        width: 350,
                        height: 200
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        width: 350,
                        height: 200
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