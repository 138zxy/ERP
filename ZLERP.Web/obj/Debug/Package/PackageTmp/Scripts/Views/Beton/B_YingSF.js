function B_YingSFIndexInit(options) {
    function DataTypeValues() {
        return { '': '', 0: '混凝土', 1: '湿拌', 2: '干混' };
    }
    //合同类型
    function DataTypeStateFmt(cellvalue, options, rowObject) {
        var style = "color:Blue;";
        var txt = "";
        if (cellvalue == 0) {
            txt = "混凝土";
        } else if (cellvalue == 1) {
            style = "color:Green;";
            txt = "湿拌";
        } else if (cellvalue == 2 || cellvalue == -1) {
            style = "color:Red;";
            txt = "干混";
        } else {
            style = "color:black;";
            txt = "您的合同状态有问题，请修复！";
        }
        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>';
    }
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight * 0.7 - 100
		, storeURL: options.storeUrl
		, sortByField: 'BuildTime'
        , sortOrder: 'DESC'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , singleSelect: false
        //, storeCondition: "(ContractStatus=2)"
		, initArray: [
              { label: '合同编号', name: 'ID', index: 'ID', width: 150 }
            //, { label: '数据状态', name: 'DataType', index: 'DataType', width: 80 }
            , { label: '数据来源', name: 'DataType', index: 'DataType', width: 150, formatter: DataTypeStateFmt, width: 50, searchoptions: { value: DataTypeValues() }, stype: 'select' }
            //, { label: '数据来源', name: 'DataType', index: 'DataType', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '混凝土', '1': '湿拌' }, stype: 'select', searchoptions: { value: booleanDataTypeSelectValues() }, editable: true, edittype: 'select', editoptions: { value: booleanDataTypeSelectValues() } }
            , { label: '合同号', name: 'ContractNo', index: 'ContractNo', width: 80 }
            , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 250 }
            , { label: '客户名称', name: 'CustName', index: 'CustName', width: 150 }
            , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', width: 100 }
            , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 80 }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', width: 80, align: 'right', formatter: 'currency', hidden: true }
            , { label: '期初应收款', name: 'PaidIn', index: 'PaidIn', width: 80, formatter: 'currency', hidden: true }
            , { label: '期初已收款', name: 'PaidOut', index: 'PaidOut', width: 80, formatter: 'currency', hidden: true }
            , { label: '期初未收款', name: 'PaidOwing', index: 'PaidOwing', width: 80, formatter: 'currency', hidden: true }

            , { label: 'PaidFavourable', name: 'PaidFavourable', index: 'PaidFavourable', hidden: true, width: 80 }
            , { label: '总应收砼款', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency', hidden: true }


		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handlePay: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                $("#B_Balance_ID").val(id);
                $("#B_Balance_Contract_ContractNo").val(Record.ContractNo);
                $("#B_Balance_Contract_ContractName").val(Record.ContractName);
                $("#B_Balance_Contract_PayMoney").val(Record.PayMoney);
                //$("#B_Balance_PayMoney").val(PayAll);
                $("#B_YingSFPayForm").dialog("open");

            },
            handlePayIni: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                console.log(Record);
                var id = Record.ID;
                myJqGrid.handleEdit({
                    loadFrom: 'B_YingSFPayIniForm',
                    btn: btn,
                    prefix: "B_YingSFrec",
                    afterFormLoaded: function () {
                        $("#Contract_ContractName").val(Record.ContractName);
                        $("#Contract_PayMoney").val(Record.PayMoney);
                        $("#Contract_PaidIn").val(Record.PaidIn);
                        $("#Contract_PaidOut").val(Record.PaidOut);
                        $("#Contract_PaidFavourable").val(Record.PaidFavourable);
                        $("#Contract_PaidOwing").val(Record.PaidOwing);
                        $("#B_YingSFrec_FinanceMoney").val(Record.PaidOwing);
                        $("#B_YingSFrec_UnitID").val(Record.ID);
                    }
                });
            },
            print: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的供应商!");
                    return;
                }
                var keys = myJqGridTo.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/DisplayReport.aspx?report=B_YingSF&supplyid=" + key + "&key=" + keys;
                window.open(url, "_blank");
            },
            PrintDesign: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=B_YingSF";
                window.open(url, "_blank");
            },
            PrintDirect: function (btn) {
                var key = myJqGrid.getSelectedKey();
                if (key <= 0) {
                    showMessage('提示', "请选择需要的供应商!");
                    return;
                }
                var keys = myJqGridTo.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var url = "/GridReport/PrintDirect.aspx?report=B_YingSF&supplyid=" + key + "&key=" + keys;
                window.open(url, "_blank");
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial',
        autoWidth: true,
        buttons: buttons1,
        height: gGridHeight * 0.3,
        title: "本次付款明细",
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [{
            label: '日期',
            name: 'FinanceDate',
            index: 'FinanceDate',
            formatter: 'date',
            width: 120
        },
		{
		    label: '合同号',
		    name: 'ContractAll.ContractNo',
		    index: 'ContractAll.ContractNo',
		    width: 80
		},
		{
		    label: '合同名称',
		    name: 'ContractAll.ContractName',
		    index: 'ContractAll.ContractName',
		    width: 150
		},
		{
		    label: '收支',
		    name: 'YingSF',
		    index: 'YingSF',
		    width: 80
		},
		{
		    label: '单据号',
		    name: 'FinanceNo',
		    index: 'FinanceNo',
		    width: 100
		},
		{
		    label: '金额',
		    name: 'FinanceMoney',
		    index: 'FinanceMoney',
		    width: 200,
		    search: false,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>',
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '优惠额',
		    name: 'PayFavourable',
		    index: 'PayFavourable',
		    width: 200,
		    search: false,
		    summaryType: 'sum',
		    summaryTpl: '合计: <font color=red>{0}</font>',
		    align: 'right',
		    formatter: 'currency'
		},
		{
		    label: '来源',
		    name: 'Source',
		    index: 'Source',
		    width: 80
		},
		{
		    label: '付款方式',
		    name: 'PayType',
		    index: 'PayType',
		    width: 80
		},
		{
		    label: '付款人',
		    name: 'Payer',
		    index: 'Payer',
		    width: 80
		},
		{
		    label: '收款人',
		    name: 'Gatheringer',
		    index: 'Gatheringer',
		    width: 80
		},
		{
		    label: '操作人',
		    name: 'Builder',
		    index: 'Builder',
		    width: 80
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    width: 200
		},
        { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime' }],
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
	    var ContractID = record.ContractID;
	    myJqGridTo.refreshGrid("UnitID='" + id + "' ");
	});

	
	$("#B_YingSFPayForm").dialog({
	    modal: true,
	    autoOpen: false,
	    width: 900,
	    Height: 500,
        title:"付款操作界面",
	    buttons: {
	        '确认': function () {
	            var Record = myJqGrid.getSelectedRecord();
	            console.log(Record);
	            var id = Record.ID;
	            var FinanceDate = $("#B_YingSFrec_FinanceDate").val();
	            var FinanceMoney = $("#B_YingSFrec_FinanceMoney").val();
	            var PayFavourable = $("#B_YingSFrec_PayFavourable").val();
	            var Payer = $("#B_YingSFrec_Payer").val();
	            var Gatheringer = $("#B_YingSFrec_Gatheringer").val();
	            var Remark = $("#B_YingSFrec_Remark").val();
	            var PayType = $("#B_YingSFrec_PayType").val();
	            if (PayType == "" || FinanceDate == "") {
	                showMessage('提示', '请输入日期以及付款方式！');
	                return;
	            }
	            var requestURL = options.PayUrl;
	            ajaxRequest(requestURL, { id: id, FinanceDate: FinanceDate, FinanceMoney: FinanceMoney, PayFavourable: PayFavourable, Payer: Payer, Gatheringer: Gatheringer, Remark: Remark, PayType: PayType },
				false,
				function (response) {
				    if (!!response.Result) {
				        showMessage('提示', '操作成功！');
				        myJqGrid.refreshGrid();
				        myJqGrid.setSelection(id);
				        myJqGridTo.refreshGrid("UnitID='" + id + "' ");
				    } else {
				        showMessage('提示', response.Message);
				    }
				});
	            $(this).dialog('close');

	        },
	        '取消': function () {
	            $(this).dialog('close');
	        }
	    },
	    position: ["center", 100]
	}); 
}


//var myJqGridTo = new MyGrid({
//    renderTo: 'myJqGridDetial', 
//    autoWidth: true,
//    buttons: buttons1,
//    title:"未付清的结算单",
//    height: gGridHeight * 0.3,
//    storeURL: options.DelstoreUrl,
//    sortByField: 'ID',
//    dialogWidth: 480,
//    dialogHeight: 300,
//    primaryKey: 'ID',
//    setGridPageSize: 30,
//    showPageBar: true,
//    initArray: [
//          { label: '结算类别', name: 'ModelType', index: 'ModelType', width: 100 }
//        , { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 80 }
//        , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 120 }
//        , { label: '运输单位', name: 'Contract.ContractName', index: 'Contract.ContractName', width: 150 }
//        , { label: '工程名称', name: 'Project.ProjectName', index: 'Project.ProjectName', width: 120 }
//        , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
//        , { label: '纳入月份', name: 'InMonth', index: 'InMonth', width: 100 } 
//        , { label: '确认砼数量', name: 'AllBOkCount', index: 'AllBOkCount', width: 100 }
//        , { label: '结算砼金额', name: 'AllBOkMoney', index: 'AllBOkMoney', width: 100, align: 'right', formatter: 'currency' }
//        , { label: '确认运费数量', name: 'AllTOkCount', index: 'AllTOkCount', width: 80 }
//        , { label: '结算运费金额', name: 'AllTOkMoney', index: 'AllTOkMoney', width: 80, align: 'right', formatter: 'currency' }

//        , { label: '确认泵送数量', name: 'AllPOkCount', index: 'AllPOkCount', width: 80 }
//        , { label: '结算泵送金额', name: 'AllPOkMoney', index: 'AllPOkMoney', width: 80, align: 'right', formatter: 'currency' }

//        , { label: '结算总金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 80, align: 'right', formatter: 'currency' }
//        , { label: '付款额', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency' }
//        , { label: '优惠额', name: 'PayFavourable', index: 'PayFavourable', width: 80, search: false, align: 'right', formatter: 'currency' } 
//        , { label: '欠款额', name: 'PayOwing', index: 'PayOwing', width: 80, search: false, align: 'right', formatter: 'currency' }
//        , { label: '结算人', name: 'BaleMan', index: 'BaleMan', width: 80 }
//	],
//    autoLoad: false,
//    functions: {
//        handleReload: function (btn) {
//            myJqGridTo.reloadGrid();
//        },
//        handleRefresh: function (btn) {
//            myJqGridTo.refreshGrid();
//        }   
//    }
//});
//$("#B_YingSFPayForm").dialog({
//    modal: true,
//    autoOpen: false,
//    width: 900,
//    Height: 500,
//    title: "付款操作界面",
//    buttons: {
//        '确认': function () {
//            var recordssf = [];
//            var records = myJqGridToPay.getAllRecords();
//            for (var i = 0; i < records.length; i++) {
//                var PayAll = parseFloat(records[i].PayMoney2);
//                var PayPal = parseFloat(records[i].PayFavourable2);
//                var overPay = parseFloat(records[i].PayOwing);
//                if (overPay < (PayAll + PayPal)) {
//                    showMessage('提示', '输入的金额有误，付款金额+优惠金额不能大于欠款金额！');
//                    return;
//                }
//                var jj = {};
//                jj.ID = records[i].ID;
//                jj.PayMoney2 = records[i].PayMoney2;
//                jj.PayFavourable2 = records[i].PayFavourable2;
//                recordssf.push(jj);
//            }
//            var jsonStr = JSON.stringify(recordssf);
//            var V_PerPay = {
//                UnitID: $("#B_Balance_ID").val(),
//                PayType: $("#B_Balance_PayType").val(),
//                InDate: $("#B_Balance_BaleDate").val(),
//                Payer: $("#B_Balance_Payer").val(),
//                Gatheringer: $("#B_Balance_Gatheringer").val(),
//                Remark2: $("#B_Balance_Remark2").val(),
//                Records: jsonStr
//            };

//            console.log(jsonStr);
//            if (V_PerPay.PayType == "" || V_PerPay.InDate == "") {
//                showMessage('提示', '请输入日期以及付款方式！');
//                return;
//            }
//            var requestURL = options.PayUrl;
//            ajaxRequest(requestURL, V_PerPay,
//            false,
//            function (response) {
//                if (!!response.Result) {
//                    showMessage('提示', '操作成功！');
//                    myJqGrid.refreshGrid();
//                } else {
//                    showMessage('提示', response.Message);
//                }
//            });
//            $(this).dialog('close');

//        },
//        '取消': function () {
//            $(this).dialog('close');
//        }
//    },
//    position: ["center", 100]
//});
//var myJqGridToPay = new MyGrid({
//    renderTo: 'myJqGridPayDetial',
//    autoWidth: true,
//    buttons: buttons1,
//    height: 200,
//    storeURL: options.DelstoreUrl,
//    sortByField: 'ID',
//    dialogWidth: 500,
//    dialogHeight: 300,
//    primaryKey: 'ID',
//    setGridPageSize: 30,
//    showPageBar: false,
//    multiselect: false,
//    editSaveUrl: options.UpdateUrl,
//    initArray: [
//          { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 80 }
//        , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 100 }
//        , { label: '合同名称', name: 'Contract.ContractName', index: 'Contract.ContractName', width: 150 }
//        , { label: '工程名称', name: 'Project.ProjectName', index: 'Project.ProjectName', width: 120 }
//        , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
//        , { label: '总金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 60 }
//        , { label: '欠款额', name: 'PayOwing', index: 'PayOwing', width: 40, search: false }
//        , { label: '优惠额', name: 'PayFavourable2', index: 'PayFavourable2', width: 80, editable: true }
//        , { label: '本次付款额', name: 'PayMoney2', index: 'PayMoney2', width: 80, editable: true, search: false }


//    ],
//    autoLoad: false,
//    functions: {
//        handleReload: function (btn) {
//            myJqGridTo.reloadGrid();
//        },
//        handleRefresh: function (btn) {
//            myJqGridTo.refreshGrid();
//        }

//    }
//});
//myJqGridToPay.addListeners("afterSaveCell", function () {
//    var records = myJqGridToPay.getAllRecords();
//    var PayAll = 0;
//    var PayPal = 0;
//    var overPay = 0;
//    for (var i = 0; i < records.length; i++) {
//        PayAll = PayAll + parseFloat(records[i].PayMoney2);
//        PayPal = PayPal + parseFloat(records[i].PayFavourable2);
//        overPay = overPay + parseFloat(records[i].PayOwing);
//    }
//    var overPay1 = overPay - PayAll - PayPal;
//    $("#B_Balance_PayMoney").val(PayAll);
//    $("#B_Balance_PayFavourable").val(PayPal);
//    $("#B_Balance_PayOwing").val(overPay1);

//});