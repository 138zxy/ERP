function supplyInfoInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'BuildTime'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 500
            , dialogHeight: 420
		    , showPageBar: true 
		    , initArray: [
                { label: '厂商代号', name: 'ID', index: 'ID', width: 80, key: true }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                //, { label: '是否启用', name: 'IsUsed', index: 'IsUsed', align: 'center', width: 60, formatter: Checktypeformatter,unformat:ChecktypeUnformat, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'checkbox', editoptions: { value: "true:false"} }
                , { label: '简称', name: 'ShortName', index: 'ShortName' }
                , { label: '厂商类型', name: 'SupplyKind', index: 'SupplyKind', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'SuType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('SuType')} }
                , { label: '厂家全称', name: 'SupplyName', index: 'SupplyName' }
                , { label: '负责人', name: 'Principal', index: 'Principal', width: 80 } 
                , { label: '厂家地址', name: 'SupplyAddr', index: 'SupplyAddr' }
                , { label: '发票地址', name: 'InvoiceAddr', index: 'InvoiceAddr' }
                , { label: '开户银行', name: 'BankName', index: 'BankName' }
                , { label: '开户银行帐号', name: 'BankAccount', index: 'BankAccount' }
                , { label: '营业电话', name: 'BusinessTel', index: 'BusinessTel' }
                , { label: '营业传真', name: 'BusinessFax', index: 'BusinessFax' }
                , { label: '邮政编码', name: 'PostCode', index: 'PostCode' }
                , { label: '负责人电话', name: 'PrincipalTel', index: 'PrincipalTel' }
                , { label: '联络人', name: 'LinkMan', index: 'LinkMan' }
                , { label: '联络人手机', name: 'LinkTel', index: 'LinkTel' }
                , { label: '供货类型', name: 'SupplyType', index: 'SupplyType' }
                , { label: '信誉等级', name: 'CreditWorthiness', index: 'CreditWorthiness' }
                , { label: 'Email', name: 'Email', index: 'Email' }
                , { label: '含税否', name: 'IsTax', index: 'IsTax', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                , { label: '是否内转', name: 'IsNz', index: 'IsNz', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '期初应付额', name: 'PaidIn', index: 'PaidIn', width: 80, formatter: 'currency'}
                , { label: '期初已付额', name: 'PaidOut', index: 'PaidOut', width: 80, formatter: 'currency' }
                , { label: '期初欠款额', name: 'PaidOwing', index: 'PaidOwing', width: 80, formatter: 'currency' }
                , { label: '总应付额', name: 'PayMoney', index: 'PayMoney', width: 80, formatter: 'currency' }
                , { label: '期初预付额', name: 'PrepayInit', index: 'PrepayInit', width: 80, formatter: 'currency' }
                , { label: '预付款', name: 'PrePay', index: 'PrePay', width: 80, formatter: 'currency' }
                , { label: '期初应收票', name: 'PiaoPaidIn', index: 'PiaoPaidIn', width: 80, formatter: 'currency' }
                , { label: '期初已收票', name: 'PiaoPaidOut', index: 'PiaoPaidOut', width: 80, formatter: 'currency' }
	            , { label: '总应收票额', name: 'PiaoPayMoney', index: 'PiaoPayMoney', width: 80, formatter: 'currency' } 
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {//加载
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {//刷新
                    myJqGrid.refreshGrid('1=1');
                },
                handleIsUsed: function () {//已启用
                    myJqGrid.refreshGrid("IsUsed = 1");
                },
                handleNotUsed: function () {//已停用
                    myJqGrid.refreshGrid("IsUsed = 0");
                },
                handleAdd: function (btn) {//增加
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('SupplyInfo.ID', false);
                        }
                    });
                },
                handleEdit: function (btn) {//修改
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv', //加载窗体
                        btn: btn,
                        prefix: "SupplyInfo",
                        afterFormLoaded: function () {//窗体加载后
                            myJqGrid.setFormFieldDisabled('SupplyInfo.ID', true); //设置窗体字段不可用
                        }
                    });
                }
                , handleDelete: function (btn) {//删除
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url//删除路径
                    });

                }
                , handleSetUsed: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showError("提示", "请选择要启用的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.IsUsed=="true") {
                        showError("提示", "此记录已启用!");
                        return;
                    }
                    showConfirm("确认信息", "确认要启用选中的记录?", function (btn) {
                        ajaxRequest(
                            opts.updateUsedStatusUrl, //URL
                            {ids: keys, usedStatus: true }, //参数
                            true,
                            function (response) {
                                if (response.Result) {//执行成功
                                    myJqGrid.reloadGrid(); //重新加载
                                }
                            }
                        );

                        $(this).dialog('close'); //关闭对话框窗体
                    });
                }

                , handleSetUnused: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择要停用的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.IsUsed=="false") {
                        showError("提示", "此记录已停用!");
                        return;
                    }
                    showConfirm("确认信息", "确认要停用选中的记录?", function (btn) {
                        ajaxRequest(
                            opts.updateUsedStatusUrl,
                            { ids: keys, usedStatus: false },
                            true,
                            function (response) {
                                myJqGrid.reloadGrid();
                            }
                        );

                        $(this).dialog('close');
                    });
                }
            }
    });


    function Checktypeformatter(cellvalue, options, rowObject) {
        if (cellvalue == true || cellvalue == "true") {
            return "<input type='checkbox' checked='true'/>";            
        }
        else {
            return "<input type='checkbox'/>"; ;            
        }
    }
    function ChecktypeUnformat(cellvalue, options, rowObject) {
        if (cellvalue == true || cellvalue == "true") {
            return true;
        }
        else {
            return false;
        }
    }
 
   
    //自动生成厂家拼音码
    $("#ID").bind("change", function () {
        var val = myJqGrid.getFormField("ID").val();
        ajaxRequest(opts.getPinYin, { chn: val }, false, function (response) {
            if (response.Result) {
                myJqGrid.getFormField("ShortName").val(response.Data);
            }
        });
    });

   
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial',
        title: '运输公司车辆信息',
        autoWidth: true,
        buttons: buttons1,
        height: 100,
        storeURL: opts.DelstoreUrl,
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
                label: 'TransID',
                name: 'TransID',
                index: 'TransID',
                width: 80,
                hidden: true
            },
		    {
		        label: '车号',
		        name: 'CarNo',
		        index: 'CarNo',
		        width: 100
		    },
	      {
	          label: '司机',
	          name: 'Driver',
	          index: 'Driver',
	          width: 100
	      },
        {
            label: '最大重量(kg)',
            name: 'MaxWeight',
            index: 'MaxWeight',
            width: 100
        },
        {
            label: '空车重量(kg)',
            name: 'CarWeight',
            index: 'CarWeight',
            width: 100
        },
        {
            label: '材料类别',
            name: 'StuffType',
            index: 'StuffType',
            width: 100
        },
        {
            label: '是否停用',
            name: 'Condition',
            index: 'Condition',
            width: 80,
            formatter: booleanFmt,
            unformat: booleanUnFmt,
            stype: "select",
            searchoptions: {
                value: booleanSearchValues()
            }
        },
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
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
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID; 
                $("#M_Car_TransID").val(Record.ID);
                myJqGridTo.handleAdd({
                    loadFrom: 'MyFormDivDel',
                    btn: btn,
                    afterFormLoaded: function () {

                    }
                });
            },
            handleEdit: function (btn) {
                myJqGridTo.handleEdit({
                    loadFrom: 'MyFormDivDel',
                    btn: btn,
                    prefix: "M_Car"
                });
            }
            , handleDelete: function (btn) {
                myJqGridTo.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '查看详细',
            width: 800,
            height: 400,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
            }
        });
    });
    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    console.log(id);
	    myJqGridTo.refreshGrid("TransID='" + id+"'");
	});
}