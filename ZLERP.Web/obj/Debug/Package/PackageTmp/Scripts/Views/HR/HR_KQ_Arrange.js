function HR_KQ_ArrangeIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '班次名称', name: 'ArrangeName', index: 'ArrangeName', width: 100 }
            , { label: '上班时间', name: 'WordStartTime', index: 'WordStartTime', width: 80 }
            , { label: '下班时间', name: 'WordEndTime', index: 'WordEndTime', width: 80 }
            , { label: '签到提前区间(h)', name: 'UpInterval', index: 'UpInterval', width: 100 }
            , { label: '签退延后区间(h)', name: 'DownInterval', index: 'DownInterval', width: 100 }
            , { label: '自动签到', name: 'AutoUp', index: 'AutoUp', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '自动签退', name: 'AutoDown', index: 'AutoDown', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '是否跨天', name: 'IsInnerDay', index: 'IsInnerDay', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
            , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
            , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }
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
                    btn: btn,
                    afterFormLoaded: function () {
                        WordStartTime();
                        WordEndTime();
                    }
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {
                        WordStartTime();
                        WordEndTime();
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
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细', 
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () { 
            }
        });
    });
    //处理上班时间-秒为00
    WordStartTime = function () {
        var WordStartTimeField = $('input[name="WordStartTime"]');
        WordStartTimeField.unbind('change');
        WordStartTimeField.bind('change',
	    function () {
	        //$('input[name="WordStartTime"]').val(WordStartTimeField.val().Format('d:m'));
	    });
    }
	//处理下班时间-秒为00
	WordEndTime = function () {
	    var WordEndTimeField = $('input[name="WordEndTime"]');
	    WordEndTimeField.unbind('change');
	    WordEndTimeField.bind('change',
	    function () {
	        //$('input[name="WordEndTime"]').val(WordEndTimeField.val().Format('d:m'));
	    });
	}
}