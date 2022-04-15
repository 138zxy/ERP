function app_versionupdateIndexInit(storeUrl) {
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
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID',hidden:true }
                , { label: 'App编码', name: 'AppManageID', index: 'AppManageID' }
                , { label: '升级提示内容', name: 'UpdateContent', index: 'UpdateContent', width: 300 }
                , { label: '升级包URL', name: 'UpdateUrl', index: 'UpdateUrl', width: 300 }
                , { label: '更新版本号', name: 'UpdateVersion', index: 'UpdateVersion' }
                , { label: '上传时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime' }
                , { label: '备注', name: 'Meno', index: 'Meno' }
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
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
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