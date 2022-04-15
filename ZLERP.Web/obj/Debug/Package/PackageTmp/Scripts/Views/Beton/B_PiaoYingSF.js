function B_YingSFIndexInit(options) {
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
        , storeCondition: "(ContractStatus=2)"
		, initArray: [
              { label: '合同编号', name: 'ID', index: 'ID',   width: 150 }
            , { label: '合同号', name: 'ContractNo', index: 'ContractNo', width: 80 }
            , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 250 }
            , { label: '客户名称', name: 'CustName', index: 'CustName', width: 150 }
            , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', width: 100 }
            , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 80 }
            , { label: '期初应开票', name: 'PiaoPaidIn', index: 'PiaoPaidIn',  width: 80, formatter: 'currency' }
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
                    showMessage('提示', "请选择需要的结算明细!");
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
                myJqGridToPay.refreshGrid("ContractID='" + id + "' and ID IN(" + Record1keys + ")");
                $("#B_Balance_ID").val(id);
                $("#B_Balance_Contract_ContractNo").val(Record.ContractNo); 
                $("#B_Balance_Contract_ContractName").val(Record.ContractName);
                $("#B_Balance_Contract_SignTotalMoney").val(PayAll);
                $("#B_Balance_PiaoPayMoney").val(PayAll);
                $("#B_PiaoYingSFPayForm").dialog("open");

            },
            handlePayIni: function (btn) {
                console.log("我被点钟了");
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                console.log(Record);
                var id = Record.ID;
                myJqGrid.handleEdit({
                    loadFrom: 'B_PiaoYingSFPayIniForm',
                    btn: btn,
                    prefix: "B_PiaoYingSFrec",
                    afterFormLoaded: function () {
                        $("#Contract_ContractName").val(Record.ContractName);
                        $("#Contract_PiaoPayMoney").val(Record.PiaoPayMoney);
                        $("#Contract_PiaoPaidIn").val(Record.PiaoPaidIn);
                        $("#Contract_PiaoPaidOut").val(Record.PiaoPaidOut);
                        $("#Contract_PiaoPaidFavourable").val(Record.PiaoPaidFavourable);
                        $("#Contract_PiaoPaidOwing").val(Record.PiaoPaidOwing);
                        $("#B_PiaoYingSFrec_FinanceMoney").val(Record.PiaoPaidOwing);
                        $("#B_PiaoYingSFrec_UnitID").val(Record.ID);
                    }
                });
            }
        }
    });
    var myJqGridTo = new MyGrid({
        renderTo: 'PiaomyJqGridDetial', 
        autoWidth: true,
        buttons: buttons1,
        title:"未开票完成的结算单",
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
            , { label: '结算单号', name: 'BaleNo', index: 'BaleNo', width: 120 } 
            , { label: '工程名称', name: 'Project.ProjectName', index: 'Project.ProjectName', width: 120 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }
            , { label: '纳入月份', name: 'InMonth', index: 'InMonth', width: 100 }  
            , { label: '结算总金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '已开票额', name: 'PiaoPayMoney', index: 'PiaoPayMoney', width: 80, align: 'right', formatter: 'currency' }
            , { label: '免开票额', name: 'PiaoPayFavourable', index: 'PiaoPayFavourable', width: 80, search: false, align: 'right', formatter: 'currency' }
            , { label: '未开票额', name: 'PiaoPayOwing', index: 'PiaoPayOwing', width: 80, search: false, align: 'right', formatter: 'currency' }
 
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
            , { label: '合同名称', name: 'Contract.ContractName', index: 'Contract.ContractName', width: 150 }
            , { label: '工程名称', name: 'Project.ProjectName', index: 'Project.ProjectName', width: 120 }
            , { label: 'ID', name: 'ID', index: 'ID', hidden: true, width: 100 }  
            , { label: '总金额', name: 'AllOkMoney', index: 'AllOkMoney', width: 60 }
            , { label: '未开票额', name: 'PiaoPayOwing', index: 'PiaoPayOwing', width: 40, search: false }
            , { label: '免开票额', name: 'PiaoPayFavourable2', index: 'PiaoPayFavourable2', width: 80, editable: true }
            , { label: '本次开票额', name: 'PiaoPayMoney2', index: 'PiaoPayMoney2', width: 80, editable: true, search: false } 
           
         
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
	    var ContractID = record.ContractID;
	    myJqGridTo.refreshGrid("ContractID='" + id + "' and PiaoPayOwing>0 and AuditStatus=1");
	});

	myJqGridToPay.addListeners("afterSaveCell", function () {
	    var records = myJqGridToPay.getAllRecords();
	    var PayAll = 0;
	    var PayPal = 0;
	    var overPay = 0;
	    for (var i = 0; i < records.length; i++) {
	        PayAll = PayAll + parseFloat(records[i].PiaoPayMoney2);
	        PayPal = PayPal + parseFloat(records[i].PiaoPayFavourable2);
	        overPay = overPay + parseFloat(records[i].PiaoPayOwing);
	    }
	    var overPay1 = overPay - PayAll - PayPal;
	    $("#B_Balance_PiaoPayMoney").val(PayAll);
	    $("#B_Balance_PiaoPayFavourable").val(PayPal);
	    $("#B_Balance_PiaoPayOwing").val(overPay1);

	});
	$("#B_PiaoYingSFPayForm").dialog({
	    modal: true,
	    autoOpen: false,
	    width: 900,
	    Height: 500,
        title:"开票操作界面",
	    buttons: {
	        '确认': function () {
	            var recordssf = [];
	            var records = myJqGridToPay.getAllRecords();
	            for (var i = 0; i < records.length; i++) {
	                var PayAll = parseFloat(records[i].PiaoPayMoney2);
	                var PayPal = parseFloat(records[i].PiaoPayFavourable2);
	                var overPay = parseFloat(records[i].PiaoPayOwing);
	                
	                var jj = {};
	                jj.ID = records[i].ID;
	                jj.PayMoney2 = records[i].PiaoPayMoney2;
	                jj.PayFavourable2 = records[i].PiaoPayFavourable2;
	                recordssf.push(jj);
	            }
	            var jsonStr = JSON.stringify(recordssf);
	            var V_PerPay = {
	                UnitID: $("#B_Balance_ID").val(),
	                PiaoNo: $("#B_Balance_PiaoNo").val(),
	                PayType: $("#B_Balance_PayType").val(),
	                InDate: $("#B_Balance_BaleDate").val(),
	                Payer: $("#B_Balance_Payer").val(),
	                Gatheringer: $("#B_Balance_Gatheringer").val(),
	                Remark2: $("#B_Balance_Remark2").val(),
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
	        url: options.uploadUrl + "?objectType=B_PiaoYingSFrec&objectId=" + objectId,
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