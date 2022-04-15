
function SC_GongYiIndexInit(options) {

    function dicCodeFmt(cellvalue, options, rowObject) { 
        if (cellvalue == null) return ''; 
        if (typeof (cellvalue) != 'undefined' && !$.isEmptyObject(Libselect)) {
            var total = LibDic.length; 
            for (var i = 0; i < total; i++) {
                var dic = LibDic[i];
                if (dic.Value == cellvalue) {
                    return dic.Text;
                }
            }
        } 
        return "";
    }
    
    var myJqGrid = new MyGrid({
        renderTo: 'SC_GongYiGrid',
        autoWidth: true,
        buttons: buttons0,
        height: 200,
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
		    label: '加工名称',
		    name: 'GYName',
		    index: 'GYName',
		    width: 120
		},
	    { label: '是否停用', name: 'IsOff', index: 'IsOff', width: 80,formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} },
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		}],
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
                    loadFrom: 'SC_GongYiForm',
                    btn: btn 
                });
            },
            handleEdit: function (btn) { 
                myJqGrid.handleEdit({
                    loadFrom: 'SC_GongYiForm',
                    btn: btn,
                    prefix: "SC_GongYi"
                });
            },
            handleDelete: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                } 
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            ChangeInlib: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var GYName = Record.GYName;
               $("#ChangeInlibForm").dialog({ title: '加工：' + GYName }).dialog("open"); 
            } 
        }
    });
   
    var myJqGrid1 = new MyGrid({
        renderTo: 'SC_GongYiDetailGrid1',
        autoWidth: true,
        title: '加工前商品',
        buttons: buttons1,
        height: 200,
        storeURL: options.SC_GongYiDetailUrl,
        sortByField: 'ID',
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
		    label: '商品',
		    name: 'GoodsID',
		    index: 'GoodsID',
		    width: 120,
		    hidden: true
		}, 
        {
		    label: '类别',
		    name: 'FormSource',
		    index: 'FormSource',
		    width: 120,
		    hidden: true
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
            width: 80
        },
		{
		    label: '数量',
		    name: 'Quantity',
		    index: 'Quantity',
		    editable: true,
		    width: 80
		} ],
		autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGrid1.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid1.refreshGrid();
            },
            handleAddDetial: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                myJqGrid.handleAdd({
                    loadFrom: 'SC_GongYiDetialForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#SC_GongYiDetail_FormSource").val("原料");
                        $("#SC_GongYiDetail_GongYiID").val(id); 
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                        myJqGrid1.refreshGrid();
                    }
                });
            },
            handleEditDetial: function (btn) {
                myJqGrid1.handleEdit({
                    loadFrom: 'SC_GongYiDetialForm',
                    btn: btn,
                    prefix: "SC_GongYiDetial",
                    afterFormLoaded: function () {
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                        myJqGrid1.refreshGrid();
                    }
                });
            },
            handleDeleteDetial: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                myJqGrid1.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    var myJqGrid2 = new MyGrid({
        renderTo: 'SC_GongYiDetailGrid2',
        title: '加工后商品',
        autoWidth: true,
        buttons: buttons1,
        height: 200,
        storeURL: options.SC_GongYiDetailUrl,
        sortByField: 'ID',
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
		    label: '商品',
		    name: 'GoodsID',
		    index: 'GoodsID',
		    width: 120,
		    hidden: true
		}, 
       	{
		    label: '类别',
		    name: 'FormSource',
		    index: 'FormSource',
		    width: 120,
		    hidden: true
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
            width: 80
        },
		{
		    label: '数量',
		    name: 'Quantity',
		    index: 'Quantity',
		    editable: true,
		    width: 80
		}],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGrid2.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid2.refreshGrid();
            },
            handleAddDetial: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                } 
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                myJqGrid2.handleAdd({
                    loadFrom: 'SC_GongYiDetialForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#SC_GongYiDetail_FormSource").val("产品");
                        $("#SC_GongYiDetail_GongYiID").val(id); 
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                        myJqGrid2.refreshGrid();
                    }
                });
            },
            handleEditDetial: function (btn) {
                myJqGrid2.handleEdit({
                    loadFrom: 'SC_GongYiDetialForm',
                    btn: btn,
                    prefix: "SC_GongYiDetial",
                    afterFormLoaded: function () {
                        GoodsChange();
                    },
                    postCallBack: function (response) {
                        myJqGrid2.refreshGrid();
                    }
                });
            },
            handleDeleteDetial: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                myJqGrid2.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    var myJqGrid3 = new MyGrid({
        renderTo: 'showGrid1',
        autoWidth: true,
        buttons: buttons2,
        height: 200,
        storeURL: options.SearchChangeUrl,
        sortByField: 'ID',
        dialogWidth: 700,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        multiselect: false,
        editSaveUrl: options.UpdateUlr,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '商品',
		    name: 'GoodsID',
		    index: 'GoodsID',
		    width: 120,
		    hidden: true
		},
        	{
		    label: '总金额',
		    name: 'AllMoney',
		    index: 'AllMoney',
		    width: 120,
		    hidden: true
		},
        {
            label: '品名',
            name: 'SC_Goods.GoodsName',
            index: 'SC_Goods.GoodsName',
            width: 60
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
		    label: '数量',
		    name: 'Quantity',
		    index: 'Quantity',
		    width: 60
		},
		{
		    label: '库存单价',
		    name: 'SC_Goods.LibPrice',
		    index: 'SC_Goods.LibPrice',
		    width: 60
		},
       { label: ' 仓库', name: 'LibID', index: 'LibID', width: 120, formatter: dicCodeFmt, editable: true, edittype: 'select', editoptions: { value: Libselect }, editrules: { required: true} }
		 ],
        autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGrid3.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid3.refreshGrid();
            }
        }
    });

    var myJqGrid4 = new MyGrid({
        renderTo: 'showGrid2',
        autoWidth: true,
        buttons: buttons2,
        height: 200,
        storeURL: options.SearchChangeUrl,
        sortByField: 'ID',
        dialogWidth: 700,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        multiselect: false,
        editSaveUrl: options.UpdateUlr,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '商品',
		    name: 'GoodsID',
		    index: 'GoodsID',
		    width: 120,
		    hidden: true
		},
        {
            label: '品名',
            name: 'SC_Goods.GoodsName',
            index: 'SC_Goods.GoodsName',
            width: 60
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
		    label: '数量',
		    name: 'Quantity',
		    index: 'Quantity',
		    width: 60
		},
		{
		    label: '加工后单价',
		    name: 'Pirce',
		    index: 'Pirce', 
		    editable: true,
		    width: 60
		} ],
		autoLoad: false,
        functions: {
            handleReload: function (btn) {
                myJqGrid4.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid4.refreshGrid();
            }
        }
    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGrid1.refreshGrid("GongYiID=" + id +" and FormSource='原料'");
	    myJqGrid2.refreshGrid("GongYiID=" + id + " and FormSource='产品'");
	});
    //下拉选择商品的时候
	GoodsChange = function () {
	    var goodIDField = myJqGrid.getFormField("ID"); 
	    goodIDField.unbind('change');
	    goodIDField.bind('change',
		function () {
		    var goodsID = goodIDField.val(); 
		    ajaxRequest(options.GetZhangUrl, { 
		        ID: goodsID
		    },
			false,
			function (response) {
			    if (!!response.Result) {
			        $("#SC_GongYiDetail_GoodsID").val(response.Data.ID);
			        $("#SC_GongYiDetail_SC_Goods_Spec").val(response.Data.Spec);
			        $("#SC_GongYiDetail_SC_Goods_Unit").val(response.Data.Unit);
			        $("#SC_GongYiDetail_SC_Goods_Brand").val(response.Data.Brand);
			        $("#SC_GongYiDetail_SC_Goods_GoodsName").val(response.Data.GoodsName);    
			    } else {
			        showMessage('提示', response.Message);
			    }
			});
		});
	}

$("#SC_ZhangTo_Quantity").change(function () {
    var UnitPrice = $("#SC_ZhangTo_PriceUnit").val();
        var Quantity = $("#SC_ZhangTo_Quantity").val();
        var money = UnitPrice * Quantity;
        $("#SC_ZhangTo_ChangeMoney").val(money)
    });

    $("#ChangeInlibForm").dialog({
        modal: true,
        autoOpen: false,
        width: 1000,
        Height: 500,
        buttons: {
            '确认': function () {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var id = keys[0];
                var lib = $("#LibName").val();
                var remark = $("#SC_PiaoChange_Remark").val();

                var model = {
                    id: id,
                    lib: lib,
                    remark: remark,
                    records3: [],
                    records4: []
                };
                //获取主体
                var records3 = myJqGrid3.getAllRecords();
                for (var i = 0; i < records3.length; i++) {
                    console.log(records3[i]);
                    var model3 = {};
                    model3.GoodsID = records3[i].GoodsID;
                    model3.Quantity = records3[i].Quantity;
                    model3.LibID = records3[i].LibID;
                    model.records3.push(model3);
                }
                var records4 = myJqGrid4.getAllRecords();
                for (var i = 0; i < records4.length; i++) {
                    var model4 = {};
                    model4.GoodsID = records4[i].GoodsID;
                    model4.Quantity = records4[i].Quantity;
                    model4.Pirce = records4[i].Pirce;
                    model.records4.push(model4);
                }
                if (lib == "") {
                    showMessage('提示', "请选择入库仓库");
                    return;
                }
                if (model.records3.length <= 0) {
                    showMessage('提示', "加工前的商品不能为空");
                    return;
                }
                if (model.records4.length <= 0) {
                    showMessage('提示', "加工加工后的商品不能为空");
                    return;
                }
                var jsonStr = JSON.stringify(model);
                console.log("model:");
                console.log(jsonStr);
                showConfirm("确认信息", "此操作一旦点击不可撤回，将对库存，金额等产生影响，请确认？",
				function () {
				    var requestURL = options.ChangeInlibUrl;
				    ajaxRequest(requestURL, {model:jsonStr},
				    false,
				    function (response) {
				        if (response.Result) {
				            showMessage('提示', '请到加工记录查询查看明细！');
				            $(this).dialog('close');
				        } else {
				            showMessage('提示', response.Message);
				        }
				    });
				   
				})
            },
            '取消': function () {
                $(this).dialog('close');
            }
        },
        position: ["center", 100]
    });
    window.Search = function () {
        var keys = myJqGrid.getSelectedKeys();
        if (keys.length == 0) {
            showMessage('提示', "请选择需要的单据!");
            return;
        }
        var id = keys[0];
        var CountNum = $("#CountNum").val();
        if (CountNum <= 0) {
            showMessage('提示', "请输入加工数量!");
            return;
        } 
        myJqGrid3.refreshGrid(id + ",原料," + CountNum);
        myJqGrid4.refreshGrid(id + ",产品," + CountNum);

    }

    var myJqGrid5 = new MyGrid({
        renderTo: 'myJqGrid5',
        autoWidth: true,
        buttons: buttons2,
        height: 200,
        storeURL: options.PiaoChangeUrl,
        sortByField: 'ID',
        height: gGridHeight,
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
		    label: '商品',
		    name: 'GoodsID',
		    index: 'GoodsID',
		    width: 120,
		    hidden: true
		},
        {
            label: '加工名称',
            name: 'ChangeName',
            index: 'ChangeName',
            width: 60
        },
        {
            label: '操作人',
            name: 'Operater',
            index: 'Operater',
            width: 60
          },
          {
            label: '操作时间',
            name: 'BuildTime',
            index: 'BuildTime',
            formatter: 'datetime',
            width: 120
        },
       
        {
            label: '品名(前)',
            name: 'GoodsName',
            index: 'GoodsName',
            width: 80, 
            search: false
        },
        {
            label: '规格(前)',
            name: 'Spec',
            index: 'Spec',
            width: 60,
            search: false
        },
        {
            label: '单位(前)',
            name: 'Unit',
            index: 'Unit',
            width: 60,
            search: false
        },
        {
            label: '数量(前)',
            name: 'V_Quantity',
            index: 'V_Quantity',
            width: 60,
            search: false
        },
         {
             label: '单价(前)',
             name: 'Price',
             index: 'Price',
             width: 60,
             search: false
         },
          {
              label: '仓库(前)',
              name: 'LibName',
              index: 'LibName',
              width: 60,
              search: false
          },

        {
            label: '品名(后)',
            name: 'GoodsName2',
            index: 'GoodsName2',
            width: 80,
            search: false
        },
        {
            label: '规格(后)',
            name: 'Spec2',
            index: 'Spec2',
            width: 60,
            search: false
        },
        {
            label: '单位(后)',
            name: 'Unit2',
            index: 'Unit2',
            width: 60,
            search: false
        },
        {
            label: '数量(后)',
            name: 'V_Quantity2',
            index: 'V_Quantity2',
            width: 60,
            search: false
        },
         {
             label: '单价(后)',
             name: 'Price2',
             index: 'Price2',
             width: 60,
             search: false
         },
          {
              label: '仓库(后)',
              name: 'LibName2',
              index: 'LibName2',
              width: 60,
              search: false
          },
        {
            label: '备注',
            name: 'Remark',
            index: 'Remark',
            width: 200
        }],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                myJqGrid5.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid5.refreshGrid();
            }
        }
    });

//    jQuery("#myJqGrid5").jqGrid('setGroupHeaders', {
//        useColSpanStyle: true,
//        groupHeaders: [
//    { startColumnName: 'GoodsName', numberOfColumns: 3, titleText: '<em>Price</em>' } 
// 
//    ]
//    });

     
}