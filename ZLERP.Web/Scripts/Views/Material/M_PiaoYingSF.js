function M_YingSFIndexInit(options) {
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
        , storeCondition: "(supplykind in ('Su1','Su5') and IsUsed =1)"
        //, storeCondition: "(1=1 and IsUsed =1)"
		, initArray: [
              { label: '厂商编号', name: 'ID', index: 'ID', width: 200 }
            , { label: '供货厂商', name: 'SupplyName', index: 'SupplyName', width: 250 }
            , { label: '地址', name: 'SupplyAddr', index: 'SupplyAddr', width: 200 }
            , { label: '联系人', name: 'LinkMan', index: 'LinkMan', width: 80 }
            , { label: '联系电话', name: 'LinkTel', index: 'LinkTel', width: 80 }

            , { label: '期初应开票', name: 'PiaoPaidIn', index: 'PiaoPaidIn', width: 80, formatter: 'currency' }
            , { label: '期初已开票', name: 'PiaoPaidOut', index: 'PiaoPaidOut', width: 80, formatter: 'currency' }
            , { label: '期初未开票额', name: 'PiaoPaidOwing', index: 'PiaoPaidOwing', width: 80, formatter: 'currency' }
            , { label: 'PiaoPaidFavourable', name: 'PiaoPaidFavourable', index: 'PiaoPaidFavourable', hidden: true, width: 80 }
            , { label: '应开票总额', name: 'PiaoPayMoney', index: 'PiaoPayMoney', width: 80, align: 'right', formatter: 'currency' } 

         
 
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
                    showMessage('提示', "请选择需要的结算单明细!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                var Record1 = myJqGridTo.getSelectedRecords();
                var PayAll = 0;
                for (var i = 0; i < Record1.length; i++) {
                    PayAll = PayAll + parseFloat(Record1[i].PiaoPayOwing);
                }
                var Record1keys = myJqGridTo.getSelectedKeys();
                myJqGridToPay.refreshGrid("StockPactID='" + id + "' and ID IN(" + Record1keys + ")");
                $("#M_BaleBalance_StockPactID").val(id);
                $("#M_BaleBalance_SupplyInfo_SupplyName").val(Record.SupplyName);
                $("#M_BaleBalance_SupplyInfo_PayMoney").val(PayAll);
                $("#M_PiaoYingSFPayForm").dialog("open");

                //给发票类型、发票号，已开票金额等赋值
                var PayAll = 0;
                var PayPal = 0;
                var overPay = 0;
                for (var i = 0; i < Record1.length; i++) {
                    PayAll = PayAll + parseFloat(Record1[i].PiaoPayMoney2);
                    PayPal = PayPal + parseFloat(Record1[i].PiaoPayFavourable2);
                    overPay = overPay + parseFloat(Record1[i].PiaoPayOwing);
                }
                var overPay1 = overPay - PayAll - PayPal;
                $("#M_BaleBalance_PiaoPayMoney").val(PayAll.toFixed(2));
                $("#M_BaleBalance_PiaoPayFavourable").val(PayPal.toFixed(2));
                $("#M_BaleBalance_PiaoPayOwing").val(overPay1.toFixed(2));

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
                    loadFrom: 'M_PiaoYingSFPayIniForm',
                    btn: btn,
                    prefix: "M_PiaoYingSFrec",
                    afterFormLoaded: function () {
                        $("#SupplyInfo_SupplyName").val(Record.SupplyName);
                        $("#SupplyInfo_PiaoPayMoney").val(Record.PiaoPayMoney);
                        $("#SupplyInfo_PiaoPaidIn").val(Record.PiaoPaidIn);
                        $("#SupplyInfo_PiaoPaidOut").val(Record.PiaoPaidOut);
                        $("#SupplyInfo_PiaoPaidFavourable").val(Record.PiaoPaidFavourable);
                        $("#SupplyInfo_PiaoPaidOwing").val(Record.PiaoPaidOwing);
                        $("#M_PiaoYingSFrec_FinanceMoney").val(Record.PiaoPaidOwing);
                        $("#M_PiaoYingSFrec_UnitID").val(Record.ID);
                    }
                });
            } 
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'PiaomyJqGridDetial',
        autoWidth: true,
        buttons: buttons1,
        title: "未收票完成的结算单",
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
            , { label: '供货厂商', name: 'SupplyInfo.SupplyName', index: 'SupplyInfo.SupplyName', width: 150 }
            , { label: '材料', name: 'StuffInfo.StuffName', index: 'StuffInfo.StuffName', width: 100 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '结算方式', name: 'BaleType', index: 'BaleType', width: 60 }
            , { label: '确认数量', name: 'AllOkCount', index: 'AllOkCount', width: 60 }
            , { label: '结算金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 60, align: 'right', formatter: 'currency' }

            , { label: '已开票额', name: 'PiaoPayMoney', index: 'PiaoPayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '免开票额', name: 'PiaoPayFavourable', index: 'PiaoPayFavourable', width: 80, search: false, align: 'right', formatter: 'currency' }
            , { label: '未开票额', name: 'PiaoPayOwing', index: 'PiaoPayOwing', width: 80, search: false, align: 'right', formatter: 'currency' }
            , { label: '本次开票额', name: 'PiaoPayMoney2', index: 'PiaoPayMoney2', width: 80, editable: true, search: false, hidden: true }
            , { label: '免开票额', name: 'PiaoPayFavourable2', index: 'PiaoPayFavourable2', width: 80, editable: true, search: false, hidden: true }
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
        renderTo: 'PiaomyJqGridPayDetial',
        //autoWidth: true,
        buttons: buttons1,
        height: 200,
        //Width: 800,
        storeURL: options.DelstoreUrl,
        sortByField: 'ID',
        //dialogWidth: 600,
        //dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: false,
        multiselect: false,
        editSaveUrl: options.UpdateUrl,
        initArray: [
              { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 100 }
            , { label: '供货厂商', name: 'SupplyInfo.SupplyName', index: 'SupplyInfo.SupplyName', width: 150 }
            , { label: '材料', name: 'StuffInfo.StuffName', index: 'StuffInfo.StuffName', width: 80 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '确认数量', name: 'AllOkCount', index: 'AllOkCount', width: 80 }
            , { label: '结算金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 60, align: 'right', formatter: 'currency' }
            , { label: '未开票额', name: 'PiaoPayOwing', index: 'PiaoPayOwing', width: 40, search: false }
            , { label: '本次开票额', name: 'PiaoPayMoney2', index: 'PiaoPayMoney2', width: 80, editable: true, search: false }
            , { label: '免开票额', name: 'PiaoPayFavourable2', index: 'PiaoPayFavourable2', width: 60, editable: true }

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
	    var StuffID = record.StuffID;
	    myJqGridTo.refreshGrid("StockPactID='" + id + "' and PiaoPayOwing>0 and AuditStatus=1");
	});

    myJqGridToPay.addListeners("afterSaveCell", function () {
        var records = myJqGridToPay.getAllRecords();        
        var PayAll = 0.00;
        var PayPal = 0.00;
        var overPay = 0.00; console.log(records);
        for (var i = 0; i < records.length; i++) {
            console.log($(records[i].PiaoPayMoney2));
            if (!parseFloat(records[i].PiaoPayMoney2)) {
                console.log($(records[i].PiaoPayMoney2)[0].id);
                document.getElementById($(records[i].PiaoPayMoney2)[0].id).value
                console.log($(records[i].PiaoPayMoney2)[0].value);
                //PayAll = PayAll + parseFloat($(records[i].PiaoPayMoney2)[0].val());
            }
            else {
                PayAll = PayAll + parseFloat(records[i].PiaoPayMoney2);
            }
            
            
            PayPal = PayPal + parseFloat(records[i].PiaoPayFavourable2);
            overPay = overPay + parseFloat(records[i].PiaoPayOwing);
        }
        var overPay1 = overPay - PayAll - PayPal;
        $("#M_BaleBalance_PiaoPayMoney").val(PayAll.toFixed(2));
        $("#M_BaleBalance_PiaoPayFavourable").val(PayPal.toFixed(2));
        $("#M_BaleBalance_PiaoPayOwing").val(overPay1.toFixed(2));

    });
    $("#M_PiaoYingSFPayForm").dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        Height: 500,
        title: "开票操作界面",
        buttons: {
            '确认': function () {
                var recordssf = [];
                var records = myJqGridToPay.getAllRecords();
                for (var i = 0; i < records.length; i++) {
                    var PayAll = parseFloat(records[i].PiaoPayMoney2);
                    var PayPal = parseFloat(records[i].PiaoPayFavourable2);
                    var overPay = parseFloat(records[i].PiaoPayOwing);
                    if (overPay < (PayAll + PayPal)) {
                        showMessage('提示', '输入的金额有误，付款金额+优惠金额不能大于欠款金额！');
                        return;
                    }
                    var jj = {};
                    jj.ID = records[i].ID;
                    jj.PayMoney2 = records[i].PiaoPayMoney2;
                    jj.PayFavourable2 = records[i].PiaoPayFavourable2;
                    recordssf.push(jj);
                }
                var jsonStr = JSON.stringify(recordssf);
                var V_PerPay = {
                    StockPactID: $("#M_BaleBalance_StockPactID").val(),
                    PiaoNo: $("#M_BaleBalance_PiaoNo").val(),
                    PayType: $("#M_BaleBalance_PayType").val(),
                    InDate: $("#M_BaleBalance_BaleDate").val(),
                    Payer: $("#M_BaleBalance_Payer").val(),
                    Gatheringer: $("#M_BaleBalance_Gatheringer").val(),
                    Remark2: $("#M_BaleBalance_Remark2").val(),
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
				        attachmentUpload(response.Data);
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



    //上传附件
    function attachmentUpload(objectId) {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload({
            url: options.uploadUrl + "?objectType=M_PiaoYingSFrec&objectId=" + objectId,
            secureuri: false,
            fileElementId: "uploadFile",
            dataType: "json",
            beforeSend: function () {
                $("#loading").show();
            },
            complete: function () {
                $("#loading").hide();
            },
            success: function (data, status) {
                if (data.Result) {
                    showMessage("附件上传成功");
                    myJqGrid.reloadGrid();
                }
                else {
                    showError("附件上传失败", data.Message);
                }
            },
            error: function (data, status, e) {
                showError(e);
            }
        }
        );
        return false;

    }
}