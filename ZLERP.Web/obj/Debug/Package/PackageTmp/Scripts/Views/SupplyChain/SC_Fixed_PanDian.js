var CheckResult = 0;
var Autocondition=0;
function SC_Fixed_PanDianIndexInit(options) {
    CheckResult=options.IsInCheck;
	var myJqGrid = new MyGrid({
		renderTo: 'MyJqGrid',
		autoWidth: true,
		buttons: buttons0,
		height: gGridHeight,
		storeURL: options.storeUrl,
		sortByField: 'CheckResult',
		dialogWidth: 480,
		dialogHeight: 300,
		primaryKey: 'ID',
		setGridPageSize: 30,
		showPageBar: true,
		editSaveUrl: options.UpdateUrl,
		storeCondition: "AutoQuantity>0",
		initArray: [{
			label: 'ID',
			name: 'ID',
			index: 'ID',
			width: 80,
			hidden: true
		},
		{
			label: 'FixedID',
			name: 'FixedID',
			index: 'FixedID',
			width: 80,
			hidden: true
		},
		{
			label: '资产编号',
			name: 'Fcode',
			index: 'Fcode',
			width: 80
		},
		{
			label: '资产名称',
			name: 'Fname',
			index: 'Fname',
			width: 80
		},
		{
			label: '资产条形码',
			name: 'BarCode',
			index: 'BarCode',
			width: 80
		},
		{
			label: '资产拼音简码',
			name: 'BrevityCode',
			index: 'BrevityCode',
			width: 80
		},
		{
			label: '类型',
			name: 'Ftype',
			index: 'Ftype',
			width: 80
		},
		{
			label: '规格型号',
			name: 'Spec',
			index: 'Spec',
			width: 80
		},
		{
			label: '存放位置',
			name: 'Position',
			index: 'Position',
			width: 80
		},
		{
			label: '资产所属部门',
			name: 'DepartMent',
			index: 'DepartMent',
			width: 80
		},
		{
			label: '保管员',
			name: 'Storeman',
			index: 'Storeman',
			width: 80
		},
		{
			label: '状态',
			name: 'Condition',
			index: 'Condition',
			width: 80
		},
		{
			label: '电脑记录数量',
			name: 'AutoQuantity',
			index: 'AutoQuantity',
			width: 80
		},
		{
			label: '盘点数量',
			name: 'Quantity',
			index: 'Quantity',
			width: 80,
			editable: true,
		},
		{
			label: '盘点结果',
			name: 'CheckResult',
			index: 'CheckResult',
			width: 80
		}],
		autoLoad: true,
		functions: {
			handleReload: function(btn) {
				myJqGrid.reloadGrid();
			},
			handleRefresh: function(btn) {
				myJqGrid.refreshGrid();
			},
			handlePanKui: function(btn) {
				if (Autocondition==0) { 
                    Autocondition=1;
					myJqGrid.refreshGrid("1=1");
				} else {
                    Autocondition=0; 
					myJqGrid.refreshGrid("AutoQuantity>0");
				}
			},
			handlePandian: function(btn) { 
				if (CheckResult == 1) {
					showMessage('提示', "当前盘点未完成!");
					return;
				}
				showConfirm("确认信息", "你确定要【开始】进行【盘点】吗？",
				function() {
					ajaxRequest(options.PandianUrl, {},
					false,
					function(response) {
						if (response.Result) {
                            CheckResult=1;
							myJqGrid.refreshGrid("CheckResult!='盘亏'");
						}
					});
				})
			},
			handleCancel: function(btn) {
				if (CheckResult == 0) {
					showMessage('提示', "没有盘点数据可取消!");
					return;
				}
				showConfirm("确认信息", "你确定要【取消】当前的【盘点】操作吗？",
				function() {
					ajaxRequest(options.CancelUrl, {},
					false,
					function(response) {
						if (response.Result) {
                            CheckResult=0;
							myJqGrid.refreshGrid("1=1");
						}
					});
				})
			},
			handleOver: function(btn) {
				if (CheckResult== 0) {
					showMessage('提示', "没有盘点数据可完成!");
					return;
				}
				showConfirm("确认信息", "你确定要【完成并结束】当前的【盘点操作】吗？",
				function() {
					ajaxRequest(options.OverUrl, {},
					false,
					function(response) {
						if (response.Result) { 
                            CheckResult=0;
							myJqGrid.refreshGrid("1=1");
                            MyJqGrid2.refreshGrid("1=1");
                            $("#formula-tabs").tabs({ fx: {}, selected: 1 });
						}
                        else 
                        {
                            showMessage('提示', response.Message);
					        return;
                        }
					});
				})
			}
		}
	});

      var MyJqGrid2 = new MyGrid({
          renderTo: 'MyJqGrid2' 
        , autoWidth: true
        , buttons: buttons2
        , height: gGridHeight
		, storeURL: options.ResultUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true 
        , sortOrder: 'DESC'
		,initArray: [{
			label: 'ID',
			name: 'ID',
			index: 'ID',
			width: 80,
			hidden: true
		},
		{
			label: 'FixedID',
			name: 'FixedID',
			index: 'FixedID',
			width: 80,
			hidden: true
		},
        {
			label: '批号',
			name: 'CheckNo',
			index: 'CheckNo',
			width: 130
		},
		{
			label: '资产编号',
			name: 'Fcode',
			index: 'Fcode',
			width: 80
		},
		{
			label: '资产名称',
			name: 'Fname',
			index: 'Fname',
			width: 80
		},
		{
			label: '资产条形码',
			name: 'BarCode',
			index: 'BarCode',
			width: 80
		},
		{
			label: '资产拼音简码',
			name: 'BrevityCode',
			index: 'BrevityCode',
			width: 80
		},
		{
			label: '类型',
			name: 'Ftype',
			index: 'Ftype',
			width: 80
		},
		{
			label: '规格型号',
			name: 'Spec',
			index: 'Spec',
			width: 80
		},
		{
			label: '存放位置',
			name: 'Position',
			index: 'Position',
			width: 80
		},
		{
			label: '资产所属部门',
			name: 'DepartMent',
			index: 'DepartMent',
			width: 80
		},
        {
			label: '保管员',
			name: 'AddDate',
			index: 'AddDate',
            formatter: 'date',
			width: 80
		}, 
        {
			label: '价格(元)',
			name: 'FixedPirce',
			index: 'FixedPirce',
			width: 80
		}, 
        {
			label: '资产配置',
			name: 'Configure',
			index: 'Configure',
			width: 80
		}, 
		{
			label: '保管员',
			name: 'Storeman',
			index: 'Storeman',
			width: 80
		}, 
       {
			label: '开始盘点时间',
			name: 'CheckStartDate',
			index: 'CheckStartDate',
            formatter: 'datetime',
			width: 120
		}, 
       {
			label: '完成盘点时间',
			name: 'CheckEndDate',
			index: 'CheckEndDate',
            formatter: 'datetime',
			width: 120
		}, 
		{
			label: '电脑记录数量',
			name: 'AutoQuantity',
			index: 'AutoQuantity',
			width: 80
		},
		{
			label: '盘点数量',
			name: 'Quantity',
			index: 'Quantity',
			width: 80,
			editable: true,
		},
		{
			label: '盘点结果',
			name: 'CheckResult',
			index: 'CheckResult',
			width: 80
		}] 
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                MyJqGrid2.reloadGrid();
            },
            handleRefresh: function (btn) {
                MyJqGrid2.refreshGrid();
            } 
        }
    }); 

	myJqGrid.addListeners("afterSaveCell",
	function(rid, record, cellname, value) {
		if (cellname == "Quantity") {
			var Quantity = record.Quantity
			var AutoQuantity = record.AutoQuantity
			if (AutoQuantity >= 1) {
				if (AutoQuantity > Quantity) {
					myJqGrid.getJqGrid().setCell(record.ID, 'CheckResult', "盘亏");
				} else {
					myJqGrid.getJqGrid().setCell(record.ID, 'CheckResult', "正常");
				}
			} else {

				if (AutoQuantity < Quantity) {
					myJqGrid.getJqGrid().setCell(record.ID, 'CheckResult', "盘盈");
				} else {
					myJqGrid.getJqGrid().setCell(record.ID, 'CheckResult', "盘亏");
				}
			}
		}
	});
	myJqGrid.addListeners("gridComplete",
	function() {
		var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
		for (var i = 0; i < ids.length; i++) {
			var cl = ids[i];
			myJqGrid.getJqGrid().setCell(cl, "Quantity", '', {
				color: 'red'
			},
			'');
		}
	});
        myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细',
            width: 500,
            height: 450,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () { 
            }
        });
    });
     
}