function SC_BaseIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'ItemsNo'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 60
		, showPageBar: true
        , groupField: 'ItemsType'
        , sortOrder: 'ASC'
        , groupingView: { groupColumnShow: [true], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['asc'], groupSummary: [false], groupColumnShow: true, showSummaryOnHide: true, groupCollapse: true, minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '类别', name: 'ItemsType', index: 'ItemsType', width: 100 }
            , { label: '名称', name: 'ItemsName', index: 'ItemsName', width: 80 }
            , { label: '序号', name: 'ItemsNo', index: 'ItemsNo', width: 80 }
            , { label: '是否系统项', name: 'IsSystem', index: 'IsSystem', width: 80, formatter: booleanFmt, unformat: booleanUnFmt }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            }
            ,handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDiv',
                    btn: btn
                });
            }
            ,handleEdit: function (btn) {
                var data = myJqGrid.getSelectedRecord();
                if (data.IsSystem == 'true') {
                    showError('提示', '系统项不能进行操作！');
                    return;
                }
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn
                });
            }
            , handleDelete: function (btn) {
                var data = myJqGrid.getSelectedRecord();
                if (data.IsSystem == 'true') {
                    showError('提示', '系统项不能进行操作！');
                    return;
                }
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
}