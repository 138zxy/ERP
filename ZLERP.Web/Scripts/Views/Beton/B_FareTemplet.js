function B_FareTempletIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'B_FareTemplet' 
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true 
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true  }
            , { label: '模板名称', name: 'FareTempletName', index: 'FareTempletName', width: 80 }  
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }  
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
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


    var myJqGrid1 = new MyGrid({
        renderTo: 'B_FareTempletDel1'
        , autoWidth: true
        , buttons: buttons1
        , height: gGridHeight/2
		, storeURL: options.B_FareTempletDel1Url
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
      
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'FareTempletID', name: 'FareTempletID', index: 'FareTempletID', width: 80, hidden: true }
            , { label: '车辆类型', name: 'FareType', index: 'FareType', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CarType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('CarType')} }
            , { label: '结算方式', name: 'BalaneType', index: 'BalaneType', width: 80 }
            , { label: '生效时间', name: 'EffectiveTime', index: 'EffectiveTime', formatter: 'datetime', width: 120}
            , { label: '单价', name: 'Price', index: 'Price', width: 80 }
            , { label: '运输补方临界值', name: 'CarBetonNum', index: 'CarBetonNum', width: 120 } 
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                myJqGrid1.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid1.refreshGrid();
            },
            handleAdd: function (btn) {

                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的运费模板!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                $("#B_FareTempletDel1_FareTempletID").val(Record.ID);
                myJqGrid1.handleAdd({
                    loadFrom: 'MyFormDiv1',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid1.handleEdit({
                    loadFrom: 'MyFormDiv1',
                    btn: btn,
                    prefix: "B_FareTempletDel1"
                });
            }
            , handleDelete: function (btn) {
                myJqGrid1.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });


    var myJqGrid2 = new MyGrid({
        renderTo: 'B_FareTempletDel2'
        , autoWidth: true
        , buttons: buttons2
        , height: gGridHeight / 2
		, storeURL: options.B_FareTempletDel2Url
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true

		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'FareTempletDelID', name: 'FareTempletDelID', index: 'FareTempletDelID', width: 80, hidden: true }
            , { label: '起止公里', name: 'UpKilometre', index: 'UpKilometre', width: 80 }
            , { label: '结束公里', name: 'DwonKilometre', index: 'DwonKilometre', width: 80 } 
            , { label: '单价', name: 'Price', index: 'Price', width: 80 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                myJqGrid2.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid2.refreshGrid();
            },
            handleAdd: function (btn) {

                var keys = myJqGrid1.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的运费模板!");
                    return;
                }
                var Record = myJqGrid1.getSelectedRecord();
                var id = Record.ID;
                $("#B_FareTempletDel2_FareTempletDelID").val(Record.ID);
                myJqGrid1.handleAdd({
                    loadFrom: 'MyFormDiv2',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid2.handleEdit({
                    loadFrom: 'MyFormDiv2',
                    btn: btn,
                    prefix: "B_FareTempletDel2"
                });
            }
            , handleDelete: function (btn) {
                myJqGrid2.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGrid1.refreshGrid("FareTempletID=" + id);
	});
	myJqGrid1.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGrid2.refreshGrid("FareTempletDelID=" + id);
	});



}