function HR_GZ_SafeIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'HR_GZ_SafeGrid',
        autoWidth: true,
        buttons: buttons0,
        height: 150,
        storeURL: options.storeUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '账套名称',
		    name: 'SafeName',
		    index: 'SafeName',
		    width: 120
		}, 
		{
		    label: '应缴合计',
		    name: 'Summation',
		    index: 'Summation',
		    width: 80
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
        { label: '创建人', name: 'Builder', index: 'Builder', width: 80 },
        { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime'}],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'HR_GZ_SafeForm',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'HR_GZ_SafeForm',
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
    var myJqGridTo = new MyGrid({
        renderTo: 'HR_GZ_SafeSetGrid',
        autoWidth: true,
        buttons: buttons1,
        height: 300,
        storeURL: options.SafeSetUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            hidden: true
        },
		{
		    label: '社保账套',
		    name: 'SetID',
		    index: 'SetID',
		    hidden: true
		},  
		{
		    label: '社保项目',
		    name: 'ItemName',
		    index: 'ItemName',
		    width: 100
		},
		{
		    label: '个人缴纳',
		    name: 'PersonPay',
		    index: 'PersonPay',
		    width: 100
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 400
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            },
            handleAdd: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                $("#HR_GZ_SafeSet_SetID").val(Record.ID);
                myJqGrid.handleAdd({
                    loadFrom: 'HR_GZ_SafeSetForm',
                    btn: btn,
                    afterFormLoaded: function () { 
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                             myJqGridTo.refreshGrid(); 
                        }
                    }
                });
            },
            handleEdit: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                }
                var keysTo = myJqGridTo.getSelectedKeys();
                if (keysTo.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                } 
                myJqGridTo.handleEdit({
                    loadFrom: 'HR_GZ_SafeSetForm',
                    btn: btn,
                    prefix: "HR_GZ_SafeSet", 
                    postCallBack: function (response) {
                        myJqGridTo.refreshGrid();
                    }
                });
            },
            handleDelete: function (btn) {
                var keysTo = myJqGridTo.getSelectedKeys();
                if (keysTo.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                } 
                myJqGridTo.deleteRecord({
                    deleteUrl: btn.data.Url,
                    postCallBack: function (response) {
                        myJqGrid.refreshGrid();
                    }
                });
            }
        }
    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo.refreshGrid("SetID=" + id);
	});
    
}