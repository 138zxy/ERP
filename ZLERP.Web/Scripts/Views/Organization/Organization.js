function organizationIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'organization-grid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight: 300
		    , setGridPageSize: -1
		    , showPageBar: false
            , singleSelect: true
            , treeGrid: true
            , treeGridModel: 'adjacency'
            , expandColumn: 'OrganizationName'
            , expandColClick: true
            , autoLoad: true
		    , initArray: [
                  { label: '组织编号', name: 'ID', index: 'ID', width:80, hidden: true }
                , { label: '组织名称', name: 'OrganizationName', index: 'OrganizationName' }
                , { label: '上级组织编号', name: '上级组织编号', index: 'ParentId', hidden: true }
                , { label: '是否叶子节点', name: 'IsLeaf', index: 'IsLeaf', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '层级', name: 'OrganizationLevel', index: 'OrganizationLevel', hidden: true }
                , { label: '上级组织', name: 'ParentOrganizationName', index: 'ParentOrganizationName' }
                , { label: ' 创建人', name: 'Builder', index: 'Builder', width: 50 }
                , { label: ' 创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 最后修改人', name: 'Modifier', index: 'Modifier', width: 70 }
                , { label: ' 最后修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
		    ]
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        btn: btn
                        , loadFrom: 'organizationForm'
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        btn: btn
                        , loadFrom: 'organizationForm'
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