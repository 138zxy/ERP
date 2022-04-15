function SC_YingSFIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight * 0.7 - 100
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 400
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , singleSelect: false
		, initArray: [
              { label: '名称', name: 'SupplierName', index: 'SupplierName', width: 80 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 80 }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, formatter: 'currency' } 
            , { label: 'PaidIn', name: 'PaidIn', index: 'PaidIn', hidden: true, width: 80 }  
            , { label: 'PaidOut', name: 'PaidOut', index: 'PaidOut', hidden: true, width: 80 }  
            , { label: 'PaidOwing', name: 'PaidOwing', index: 'PaidOwing', hidden: true, width: 80 } 
            , { label: 'PaidFavourable', name: 'PaidFavourable', index: 'PaidFavourable', hidden: true, width: 80 }
            , { label: '应付款', name: 'PayMoney', index: 'PayMoney', width: 80, formatter: 'currency' }
            , { label: '预付金额', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, formatter: 'currency' }
            , { label: '地址', name: 'Adrress', index: 'Adrress', width: 200 }
            , { label: '联系人', name: 'Linker', index: 'Linker', width: 80 }
            , { label: '联系电话', name: 'LinkPhone', index: 'LinkPhone', width: 80 }
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
            handlePay: function (btn) { 
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var keys1 = myJqGridTo.getSelectedKeys();
                if (keys1.length == 0) {
                    showMessage('提示', "请选择需要的入库明细!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID; 
                var Record1 = myJqGridTo.getSelectedRecords();
                var PayAll=0;
                 for (var i = 0; i < Record1.length; i++) { 
                      PayAll=PayAll+ parseFloat( Record1[i].PayOwing);
                  } 
               var Record1keys = myJqGridTo.getSelectedKeys();
                myJqGridToPay.refreshGrid("SupplierID=" + id+" and ID IN("+Record1keys+")");
                $("#SC_PiaoIn_SupplierID").val(id);
                $("#SC_PiaoIn_SC_Supply_SupplierName").val(Record.SupplierName); 
                $("#SC_PiaoIn_SC_Supply_PayMoney").val(PayAll); 
                $("#SC_YingSFPayForm").dialog("open"); 
 
            },
            handlePayIni: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord(); 
                var id = Record.ID; 
                myJqGrid.handleEdit({
                    loadFrom: 'SC_YingSFPayIniForm',
                    btn: btn,
                    prefix: "SC_YingSFrec",
                    afterFormLoaded: function () {
                            $("#SC_Supply_SupplierName").val(Record.SupplierName); 
                            $("#SC_Supply_PayMoney").val(Record.PayMoney); 
                            $("#SC_Supply_PaidIn").val(Record.PaidIn); 
                            $("#SC_Supply_PaidOut").val(Record.PaidOut); 
                            $("#SC_Supply_PaidFavourable").val(Record.PaidFavourable); 
                            $("#SC_Supply_PaidOwing").val(Record.PaidOwing); 
                            $("#SC_YingSFrec_FinanceMoney").val(Record.PaidOwing);   
                            $("#SC_YingSFrec_UnitID").val(Record.ID);   
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
                    var url = "/GridReport/DisplayReport.aspx?report=SC_YingSF&supplyid=" + key + "&key=" + keys;
                    window.open(url, "_blank");
                },
                PrintDesign: function (btn) {
                    var url = "/GridReport/DesignReport.aspx?report=SC_YingSF";
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
                    var url = "/GridReport/PrintDirect.aspx?report=SC_YingSF&supplyid=" + key + "&key=" + keys;
                    window.open(url, "_blank");
                } 
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial', 
        autoWidth: true,
        buttons: buttons1,
        height: gGridHeight * 0.3,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,  
        initArray: [
              { label: '日期', name: 'InDate', index: 'InDate', formatter: 'date', width: 120 }
            , { label: '入库单号', name: 'InNo', index: 'InNo', width: 100 }
            , { label: '供应商', name: 'SupplierID', index: 'SupplierID', hidden: true, width: 100 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '总金额', name: 'InMoney', index: 'InMoney', width: 80, formatter: 'currency' }
            , { label: '付款额', name: 'PayMoney', index: 'PayMoney', width: 80, formatter: 'currency' }
            , { label: '欠款额', name: 'PayOwing', index: 'PayOwing', width: 80, search: false, formatter: 'currency' }
            , { label: '优惠额', name: 'PayFavourable', index: 'PayFavourable', width: 80, search: false, formatter: 'currency' }
            , { label: '入库方式', name: 'InType', index: 'InType', width: 80 }
            , { label: '付款方式', name: 'PayType', index: 'PayType', width: 80 }
            , { label: '采购员', name: 'Purchase', index: 'Purchase', width: 80 } 
		],
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

    var myJqGridToPay = new MyGrid({
        renderTo: 'myJqGridPayDetial',
        autoWidth: true,
        buttons: buttons1,
        height: 200,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 500,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        multiselect: false, 
        editSaveUrl: options.UpdateUrl ,
        initArray: [
              { label: '日期', name: 'InDate', index: 'InDate', formatter: 'date', width: 80 }
            , { label: '入库单号', name: 'InNo', index: 'InNo', width: 100 }
            , { label: '供应商', name: 'SupplierID', index: 'SupplierID', hidden: true, width: 100 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '品种数', name: 'VarietyNum', index: 'VarietyNum', width: 30 }
            , { label: '总金额', name: 'InMoney', index: 'InMoney', width: 40, formatter: 'currency' }
            , { label: '欠款额', name: 'PayOwing', index: 'PayOwing', width: 40, search: false, formatter: 'currency' }
            , { label: '付款额', name: 'PayMoney2', index: 'PayMoney2', width: 40, editable: true, search: false, formatter: 'currency' }
            , { label: '优惠额', name: 'PayFavourable2', index: 'PayFavourable2', width: 40, editable: true, formatter: 'currency' } 
            , { label: '付款方式', name: 'PayType', index: 'PayType', width: 80 }
            , { label: '采购员', name: 'Purchase', index: 'Purchase', width: 80 }
		],
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
	    myJqGridTo.refreshGrid("SupplierID=" + id + " and PayOwing>0 "); 
	});

	myJqGridToPay.addListeners("afterSaveCell", function () {
	    var records = myJqGridToPay.getAllRecords();
	    var PayAll = 0;
	    var PayPal = 0;
	    var overPay = 0;
	    for (var i = 0; i < records.length; i++) {
	        PayAll = PayAll + parseFloat(records[i].PayMoney2);
	        PayPal = PayPal + parseFloat(records[i].PayFavourable2);
	        overPay = overPay + parseFloat(records[i].PayOwing);
	    }
	    var overPay1 = overPay - PayAll - PayPal;
	    $("#SC_PiaoIn_PayMoney").val(PayAll);
	    $("#SC_PiaoIn_PayFavourable").val(PayPal);
	    $("#SC_PiaoIn_PayOwing").val(overPay1);

	});
	$("#SC_YingSFPayForm").dialog({
	    modal: true,
	    autoOpen: false,
	    width: 700,
	    Height: 500,
	    buttons: {
	        '确认': function () {
	            var recordssf = [];
	            var records = myJqGridToPay.getAllRecords();
	            for (var i = 0; i < records.length; i++) {
	                var PayAll = parseFloat(records[i].PayMoney2);
	                var PayPal = parseFloat(records[i].PayFavourable2);
	                var overPay = parseFloat(records[i].PayOwing);
	                if (overPay < (PayAll + PayPal)) {
	                    showMessage('提示', '输入的金额有误，付款金额+优惠金额不能大于欠款金额！');
	                    return;
	                }
	                var jj = {};
	                jj.ID = records[i].ID;
	                jj.PayMoney2 = records[i].PayMoney2;
	                jj.PayFavourable2 = records[i].PayFavourable2;
	                recordssf.push(jj);
	            }
	            var jsonStr = JSON.stringify(recordssf);
	            var V_PerPay = {
	                SupplierID: $("#SC_PiaoIn_SupplierID").val(),
	                PayType: $("#SC_PiaoIn_PayType").val(),
	                InDate: $("#SC_PiaoIn_InDate").val(),
	                Payer: $("#SC_PiaoIn_Payer").val(),
	                Gatheringer: $("#SC_PiaoIn_Gatheringer").val(),
	                Remark2: $("#SC_PiaoIn_Remark2").val(),
	                Records: jsonStr
	            };

	            console.log(jsonStr);
	            if (V_PerPay.PayType == "" || V_PerPay.InDate == "") {
	                showMessage('提示', '请输入日期以及付款方式！');
	                return;
	            }
	            var requestURL = options.PayUrl;
	            ajaxRequest(requestURL, V_PerPay,
				false,
				function (response) {
				    if (!!response.Result) {
				        showMessage('提示', '操作成功！');
				        myJqGrid.refreshGrid();
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