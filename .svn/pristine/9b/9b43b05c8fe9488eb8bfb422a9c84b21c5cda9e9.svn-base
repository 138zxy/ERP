function M_TranYingSFIndexInit(options) {
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
        , storeCondition: "(supplykind in ('Su3','Su5') and IsUsed =1)"
		, initArray: [
              { label: '运输单位编号', name: 'ID', index: 'ID', width: 150 }
            , { label: '运输单位', name: 'SupplyName', index: 'SupplyName', width: 150 }

            , { label: '预付款', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: 'PaidIn', name: 'PaidIn', index: 'PaidIn', hidden: true, width: 80 }
            , { label: 'PaidOut', name: 'PaidOut', index: 'PaidOut', hidden: true, width: 80 }
            , { label: 'PaidOwing', name: 'PaidOwing', index: 'PaidOwing', hidden: true, width: 80 }
            , { label: 'PaidFavourable', name: 'PaidFavourable', index: 'PaidFavourable', hidden: true, width: 80 }
            , { label: '总应付款', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '预付金额', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: '地址', name: 'SupplyAddr', index: 'SupplyInfo.SupplyAddr', width: 200 }
            , { label: '联系人', name: 'LinkMan', index: 'SupplyInfo.LinkMan', width: 80 }
            , { label: '联系电话', name: 'LinkTel', index: 'SupplyInfo.LinkTel', width: 80 }
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
                var PayAll = 0;
                for (var i = 0; i < Record1.length; i++) {
                    PayAll = PayAll + parseFloat(Record1[i].PayOwing);
                }
                console.log(Record);
                var Record1keys = myJqGridTo.getSelectedKeys();
                myJqGridToPay.refreshGrid("TranID='" + id + "' and ID IN(" + Record1keys + ")");
                $("#M_TranBalance_TranID").val(id);
                console.log(id);
                $("#M_TranBalance_SupplyInfo_SupplyName").val(Record.SupplyName);
                console.log(Record.SupplyName);
                $("#M_TranBalance_SupplyInfo_PayMoney").val(PayAll);
                $("#M_TranYingSFPayForm").dialog("open");

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
                    loadFrom: 'M_TranYingSFPayIniForm',
                    btn: btn,
                    prefix: "M_TranYingSFrec",
                    afterFormLoaded: function () {
                        $("#SupplyInfo_SupplyName").val(Record.SupplyName);
                        $("#SupplyInfo_PayMoney").val(Record.PayMoney);
                        $("#SupplyInfo_PaidIn").val(Record.PaidIn);
                        $("#SupplyInfo_PaidOut").val(Record.PaidOut);
                        $("#SupplyInfo_PaidFavourable").val(Record.PaidFavourable);
                        $("#SupplyInfo_PaidOwing").val(Record.PaidOwing);
                        $("#M_TranYingSFrec_FinanceMoney").val(Record.PaidOwing);
                        $("#M_TranYingSFrec_UnitID").val(Record.ID);
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
                var url = "/GridReport/DisplayReport.aspx?report=M_TranYingSF&supplyid=" + key + "&key=" + keys;
                window.open(url, "_blank");
            },
            PrintDesign: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=M_TranYingSF";
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
                var url = "/GridReport/PrintDirect.aspx?report=M_TranYingSF&supplyid=" + key + "&key=" + keys;
                window.open(url, "_blank");
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'myJqGridDetial', 
        autoWidth: true,
        buttons: buttons1,
        title:"未付清的结算单",
        height: gGridHeight * 0.3,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,  
        initArray: [
              { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 80 }
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 100 }
            , { label: '运输单位', name: 'SupplyInfo.SupplyName', index: 'SupplyInfo.SupplyName', width: 150 }
            , { label: '材料', name: 'StuffInfo.StuffName', index: 'StuffInfo.StuffName', width: 100 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '结算方式', name: 'BaleType', index: 'BaleType', width: 60 }
            , { label: '确认数量(吨)', name: 'AllOkCount', index: 'AllOkCount', width: 60 }
            , { label: '结算金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 60, align: 'right', formatter: 'currency' }

            , { label: '付款额', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '欠款额', name: 'PayOwing', index: 'PayOwing', width: 80, search: false, align: 'right', formatter: 'currency' }
            , { label: '优惠额', name: 'PayFavourable', index: 'PayFavourable', width: 80, search: false, align: 'right', formatter: 'currency' }
            , { label: '结算人', name: 'BaleMan', index: 'BaleMan', width: 80 }
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
        renderTo: 'TranmyJqGridPayDetial',
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
              { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 80 }
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 100 }
            , { label: '运输单位', name: 'SupplyInfo.SupplyName', index: 'SupplyInfo.SupplyName', width: 150 }
            , { label: '材料', name: 'StuffInfo.StuffName', index: 'StuffInfo.StuffName', width: 100 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '结算方式', name: 'BaleType', index: 'BaleType', width: 60 }
            , { label: '确认数量(吨)', name: 'AllOkCount', index: 'AllOkCount', width: 60 }
            , { label: '结算金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 60, align: 'right', formatter: 'currency' }
            , { label: '欠款额', name: 'PayOwing', index: 'PayOwing', width: 40, search: false, align: 'right', formatter: 'currency' }
            , { label: '本次付款额', name: 'PayMoney2', index: 'PayMoney2', width: 40, editable: true, search: false, align: 'right', formatter: 'currency' }
            , { label: '优惠额', name: 'PayFavourable2', index: 'PayFavourable2', width: 40, editable: true, align: 'right', formatter: 'currency' }
            , { label: '结算人', name: 'BaleMan', index: 'BaleMan', width: 80 }
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
	    console.log(record);
	    var StuffID = record.StuffID;
	    myJqGridTo.refreshGrid("TranID='" + id + "' and PayOwing>0 and AuditStatus=1");
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
	    $("#M_TranBalance_PayMoney").val(PayAll);
	    $("#M_TranBalance_PayFavourable").val(PayPal);
	    $("#M_TranBalance_PayOwing").val(overPay1);

	});
	$("#M_TranYingSFPayForm").dialog({
	    modal: true,
	    autoOpen: false,
	    width: 900,
	    Height: 500,
        title:"付款操作界面",
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
	                StockPactID: $("#M_TranBalance_TranID").val(),
	                PayType: $("#M_TranBalance_PayType").val(),
	                InDate: $("#M_TranBalance_BaleDate").val(),
	                Payer: $("#M_TranBalance_Payer").val(),
	                Gatheringer: $("#M_TranBalance_Gatheringer").val(),
	                Remark2: $("#M_TranBalance_Remark2").val(),
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