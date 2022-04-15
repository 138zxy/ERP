function SC_YingSFrecIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
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
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '仓库', name: 'SC_Lib.LibName', index: 'SC_Lib.LibName', width: 80 }
            , { label: '开始时间', name: 'BeginDate', index: 'BeginDate', formatter: 'datetime', width: 120 }
            , { label: '结束时间', name: 'EndDate', index: 'EndDate', formatter: 'datetime', width: 120 }
            , { label: '差异数量', name: 'DifferenceNum', index: 'DifferenceNum', width: 100 }
            , { label: '差异金额', name: 'DifferenceMoney', index: 'DifferenceMoney', width: 80, formatter: 'currency'
            }
            , { label: '是否完成', name: 'IsOff', index: 'IsOff', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
            , { label: '修正人', name: 'Auditor', index: 'Auditor', width: 80 }
            , { label: '修正时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 120 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 80 }
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
                    loadFrom: 'SC_PanDianForm',
                    btn: btn,
                    width: 400,
                    height: 200,
                    afterFormLoaded: function () {
                        $("#Remark").val("简易盘点");
                    },
                    postCallBack: function (response) {
//                        if (response.Result) {
//                            console.log("fucking response.Data = " + response.Data);
//                            myJqGrid.refreshGrid("ID=" + response.Data);
//                            myJqGridTo.refreshGrid("PanID=" + response.Data);
//                        }
                    }
                });
            },
            handleDelete: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var Condition = Record.IsOff;
                if (Condition == 1) {
                    showMessage('提示', "只有没有完成的单据才能删除!");
                    return;
                }
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            handlePanDianIn: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;

                showConfirm("确认信息", "此操作一旦点击不可撤回，将对库存，金额等产生影响，请确认？",
				function () {
				    var requestURL = options.PanDianInUrl;
				    ajaxRequest(requestURL, {
				        id: id
				    },
					false,
					function (response) {
					    if (!!response.Result) {
					        myJqGrid.refreshGrid();
					        showMessage('提示', '操作成功！');
					        
					    } else {
					        showMessage('提示', response.Message);
					    }
					})
				});
            },
            print: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/DisplayReport.aspx?report=SC_PanDian&ID=" + key;
                window.open(url, "_blank");
            },
            PrintDesign: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=SC_PanDian";
                window.open(url, "_blank");
            },
            PrintDirect: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/PrintDirect.aspx?report=SC_PanDian&ID=" + key;
                window.open(url, "_blank");
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial',
        autoWidth: true,
        buttons: buttons1,
        height: gGridHeight,
        storeURL: options.DelstoreUrl,
        sortByField: 'SC_Goods.GoodsName',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        editSaveUrl: options.UpdateUrl,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '分类',
		    name: 'SC_Goods.SC_GoodsType.TypeName',
		    index: 'SC_Goods.SC_GoodsType.TypeName',
		    width: 60
		},
        {
            label: '商品编码',
            name: 'SC_Goods.GoodsCode',
            index: 'SC_Goods.GoodsCode',
            width: 100
        },
		{
		    label: '品名',
		    name: 'SC_Goods.GoodsName',
		    index: 'SC_Goods.GoodsName',
		    width: 80
		},
		{
		     label: '规格',
		     name: 'SC_Goods.Spec',
		     index: 'SC_Goods.Spec',
		     width: 80
		 },
         {
             label: '单位',
             name: 'SC_Goods.Unit',
             index: 'SC_Goods.Unit',
             width: 40
         },
         {
             label: '品牌',
             name: 'SC_Goods.Brand',
             index: 'SC_Goods.Brand',
             width: 80
         }, 
         {
             label: '库存数量',
             name: 'LibNum',
             index: 'LibNum',
             width: 60
         },
         {
             label: '盘点数量',
             name: 'PanNum',
             index: 'PanNum',
             editable: true,
             width: 60
         },
         {
             label: '差异数量',
             name: 'DifferenceNum',
             index: 'DifferenceNum',
             width: 60
         },
         {
             label: '备注',
             name: 'Remark',
             index: 'Remark',
             editable: true,
             width: 100
         }],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGridTo.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo.refreshGrid();
            }   
        }
    });
    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) { 
	    myJqGridTo.refreshGrid("PanID=" + id);
	});
	myJqGridTo.addListeners("afterSaveCell", function (rid, record, cellname, value) {
	    if (cellname == "PanNum") {
	        var DifferenceNum = record.PanNum - record.LibNum
	       myJqGridTo.getJqGrid().setCell(record.ID, 'DifferenceNum', DifferenceNum);
	    } 
	});
	$("#IsCheckPandian").click(function () {
	    var Record = myJqGrid.getSelectedRecord();

	    var id = Record.ID;
	    console.log(id);
	    if ($(this).attr("checked")) {
	         console.log("已经点击了");
             myJqGridTo.refreshGrid("PanID=" + id + " and LibNum<> PanNum ");
         } else {
             console.log("1111已经点击了");
              myJqGridTo.refreshGrid("PanID=" + id);
         }
     });
}