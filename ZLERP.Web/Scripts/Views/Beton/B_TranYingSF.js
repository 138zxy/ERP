function B_TranYingSFIndexInit(options) {
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
        , storeCondition: "(IsStop=0)"
		, initArray: [
              { label: '车辆单位编号', name: 'ID', index: 'ID', hidden: true, width: 150 }
            , { label: '车辆单位编号', name: 'FleetCode', index: 'FleetCode', width: 80 }
            , { label: '车辆单位名称', name: 'FleetName', index: 'FleetName', width: 80 }
            , { label: '车辆单位类别', name: 'FleetType', index: 'FleetType', width: 100 }
            , { label: '地址', name: 'Adrress', index: 'Adrress', width: 100 }
            , { label: '预付款', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: '期初应付款', name: 'PaidIn', index: 'PaidIn', width: 80, formatter: 'currency' }
            , { label: '期初已付款', name: 'PaidOut', index: 'PaidOut', width: 80, formatter: 'currency' }
            , { label: '期初欠款', name: 'PaidOwing', index: 'PaidOwing', width: 80, formatter: 'currency' }
            , { label: 'PaidFavourable', name: 'PaidFavourable', index: 'PaidFavourable', hidden: true, width: 80 }
            , { label: '总应付款', name: 'PayMoney', index: 'PayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '预付金额', name: 'PrePay', index: 'PrePay', hidden: true, width: 80, align: 'right', formatter: 'currency' }
            , { label: '地址', name: 'Adrress', index: 'Adrress', width: 100 }
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
                var PayAll = 0;
                for (var i = 0; i < Record1.length; i++) {
                    PayAll = PayAll + parseFloat(Record1[i].PayOwing);
                }
                var Record1keys = myJqGridTo.getSelectedKeys();
                myJqGridToPay.refreshGrid("TranID='" + id + "' and ID IN(" + Record1keys + ")");
                $("#B_TranBalance_TranID").val(id); 
                $("#B_TranBalance_B_CarFleet_FleetCode").val(Record.FleetCode);
                $("#B_TranBalance_B_CarFleet_FleetName").val(Record.FleetName);
                $("#B_TranBalance_B_CarFleet_PayMoney").val(PayAll);
                $("#B_TranBalance_PayMoney").val(PayAll);
                $("#B_TranYingSFPayForm").dialog("open");

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
                    loadFrom: 'B_TranYingSFPayIniForm',
                    btn: btn,
                    prefix: "B_TranYingSFrec",
                    afterFormLoaded: function () {
                        $("#B_CarFleet_FleetName").val(Record.FleetName);
                        $("#B_CarFleet_PayMoney").val(Record.PayMoney);
                        $("#B_CarFleet_PaidIn").val(Record.PaidIn);
                        $("#B_CarFleet_PaidOut").val(Record.PaidOut);
                        $("#B_CarFleet_PaidFavourable").val(Record.PaidFavourable);
                        $("#B_CarFleet_PaidOwing").val(Record.PaidOwing);
                        $("#B_TranYingSFrec_FinanceMoney").val(Record.PaidOwing);
                        $("#B_TranYingSFrec_UnitID").val(Record.ID);
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
                var url = "/GridReport/DisplayReport.aspx?report=B_TranYingSF&supplyid=" + key + "&key=" + keys;
                window.open(url, "_blank");
            },
            PrintDesign: function (btn) {
                var url = "/GridReport/DesignReport.aspx?report=B_TranYingSF";
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
                var url = "/GridReport/PrintDirect.aspx?report=B_TranYingSF&supplyid=" + key + "&key=" + keys;
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
              { label: '结算类别', name: 'ModelType', index: 'ModelType', width: 100 }
            , { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 80 }
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 100 }
            , { label: '车辆单位', name: 'B_CarFleet.FleetName', index: 'B_CarFleet.FleetName', width: 150 }
            , { label: '工程名称', name: 'Project.ProjectName', index: 'Project.ProjectName', width: 120 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '结算模板', name: 'B_FareTemplet.FareTempletName', index: 'B_FareTemplet.FareTempletName', width: 60 }
            , { label: '系统结算', name: 'IsStockPrice', index: 'IsStockPrice', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '手动结算', name: 'IsOnePrice', index: 'IsOnePrice', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
            , { label: '手动方式', name: 'OnePriceType', index: 'OnePriceType', width: 60 }
            , { label: '确认数量', name: 'AllOkCount', index: 'AllOkCount', width: 60 }
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
              { label: '结算日期', name: 'BaleDate', index: 'BaleDate', formatter: 'date', width: 80 }
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 100 }
            , { label: '运输单位', name: 'B_CarFleet.FleetName', index: 'B_CarFleet.FleetName', width: 150 }
            , { label: '工程名称', name: 'Project.ProjectName', index: 'Project.ProjectName', width: 120 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
           
            , { label: '确认数量', name: 'AllOkCount', index: 'AllOkCount', width: 80 }
            , { label: '结算金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '欠款额', name: 'PayOwing', index: 'PayOwing', width: 80, search: false, align: 'right', formatter: 'currency' }
            , { label: '本次付款额', name: 'PayMoney2', index: 'PayMoney2', width: 80, editable: true, search: false, align: 'right', formatter: 'currency' }
            , { label: '优惠额', name: 'PayFavourable2', index: 'PayFavourable2', width: 80, editable: true, align: 'right', formatter: 'currency' }
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
	    $("#B_TranBalance_PayMoney").val(PayAll);
	    $("#B_TranBalance_PayFavourable").val(PayPal);
	    $("#B_TranBalance_PayOwing").val(overPay1);

	});
	$("#B_TranYingSFPayForm").dialog({
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
	                UnitID: $("#B_TranBalance_TranID").val(),
	                PayType: $("#B_TranBalance_PayType").val(),
	                InDate: $("#B_TranBalance_BaleDate").val(),
	                Payer: $("#B_TranBalance_Payer").val(),
	                Gatheringer: $("#B_TranBalance_Gatheringer").val(),
	                Remark2: $("#B_TranBalance_Remark2").val(),
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