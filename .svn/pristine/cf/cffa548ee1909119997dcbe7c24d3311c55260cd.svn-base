function hr_pm_contractIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogHeight: 480
            , dialogWidth: 500
            , rowNumbers: true
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '人员编码', name: 'PersonID', index: 'PersonID', width: 80, hidden: true }
                , { label: '人员名称', name: 'PersonName', index: 'PersonName', width: 80 }
                , { label: '人员拼音码', name: 'PersonPym', index: 'PersonPym', width: 80 }
                , { label: '部门编码', name: 'DepartmentID', index: 'DepartmentID', width: 80, hidden: true }
                , { label: '部门名称', name: 'DepartmentName', index: 'DepartmentName', width: 80 }
                , { label: '合同编号', name: 'ContractNo', index: 'ContractNo', width: 80 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 120 }
                , { label: '合同类型', name: 'ContractType', index: 'ContractType', width: 80 }
                , { label: '有无期限', name: 'IsTerm', index: 'IsTerm', width: 80, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '合同期限(年)', name: 'ContractTermYear', index: 'ContractTermYear', width: 80 }
                , { label: '合同期限(月)', name: 'ContractTermMonth', index: 'ContractTermMonth', width: 80 }
                , { label: '签订日期', name: 'SignDate', index: 'SignDate', formatter: 'date', width: 80 }
                , { label: '试用期(月)', name: 'ProbationMonth', index: 'ProbationMonth', width: 80 }
                , { label: '试用期(天)', name: 'ProbationDay', index: 'ProbationDay', width: 80 }
                , { label: '试用开始日期', name: 'ProbationStart', index: 'ProbationStart', formatter: 'date', width: 80 }
                , { label: '试用结束日期', name: 'ProbationEnd', index: 'ProbationEnd', formatter: 'date', width: 80 }
                , { label: '试用工资', name: 'ProbationWage', index: 'ProbationWage', width: 80 }
                , { label: '生效日期', name: 'ValidDate', index: 'ValidDate', formatter: 'date', width: 80 }
                , { label: '到期日期', name: 'MatureDate', index: 'MatureDate', formatter: 'date', width: 80 }
                , { label: '转正工资', name: 'CorrectionWage', index: 'CorrectionWage', width: 80 }
                , { label: '状态', name: 'State', index: 'State', width: 80 }
                , { label: '删除标识', name: 'Delflag', index: 'Delflag', width: 80, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '备注', name: 'Meno', index: 'Meno', width: 80 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            PersonChange();
                            ProbationStartChange();
                            ProbationEndChange();

                            ValidDateChange();
                            MatureDateChange();
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            PersonChange();
                            ProbationStartChange();
                            ProbationEndChange();

                            ValidDateChange();
                            MatureDateChange();
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    //下拉选择人员的时候
    PersonChange = function () {
        var PersonIDField = $('input[name="PersonID"]'); //myJqGrid.getFormField("ID"); 
        PersonIDField.unbind('change');
        PersonIDField.bind('change',
	    function () {
	        var personId = PersonIDField.val();
	        var requestURL = "/HR_Base_Personnel.mvc/Get";
	        ajaxRequest(requestURL, {
	            id: personId
	        },
			false,
			function (response) {
				if (!!response.Result) {
				    var data = response.Data;
				    $("#PersonPym").val(data.PyCode);
				    $("#DepartmentName").val(data.DepartmentName);
				    $("#DepartmentID").val(data.DepartmentID);
				    
				} else {
				    showMessage('提示', response.Message);
				}
			});

	    });
    }

    //--------------------------------------------------试用期-开始---------------------------------------------------------
    //下拉选择试用开始时间的时候
    ProbationStartChange = function () {
        var ProbationStartField = $('input[name="ProbationStart"]');
        var ProbationEndField = $('input[name="ProbationEnd"]');
        if (ProbationStartField.val() == "" || ProbationEndField.val() == "") {
            return;
        }
        var oldStartTime = ProbationStartField.val(); //记录老时间
        ProbationStartField.unbind('change');
        ProbationStartField.bind('change',
	    function () {
	        var ProStartTime = ProbationStartField.val();
	        var ProEndTime = ProbationEndField.val();
	        if (ProStartTime >= ProEndTime) {
	            showError('提示', '试用开始时间不能小于试用结束时间！');
	            ProbationStartField.val(oldStartTime);
	            return;
	        }
	        calcMD(ProStartTime, ProEndTime);
	    });
    }
	//下拉选择试用结束时间的时候
	ProbationEndChange = function () {
	    var ProbationStartField = $('input[name="ProbationStart"]');
	    var ProbationEndField = $('input[name="ProbationEnd"]');
	    if (ProbationStartField.val() == "" || ProbationEndField.val() == "") {
	        return;
	    }
	    var oldEndTime = ProbationEndField.val(); //记录老时间

	    ProbationEndField.unbind('change');
	    ProbationEndField.bind('change',
	    function () {
	        var ProStartTime = ProbationStartField.val();
	        var ProEndTime = ProbationEndField.val();
	        if (ProStartTime >= ProEndTime) {
	            showError('提示', '试用开始时间不能小于试用结束时间！');
	            ProbationEndField.val(oldEndTime);
	            return;
	        }
	        calcMD(ProStartTime, ProEndTime);
	    });
	}
	//计算试用月、天
	function calcMD(ProStartTime, ProEndTime) {
	    
	    var modth, day;
	    var cday = datedifference(ProStartTime, ProEndTime);
	    modth = parseInt((cday * 1) / 30); //取整 相差多少月
	    day = cday * 1 - modth * 30; //相差多少天

	    $("#ProbationMonth").val(modth);
	    $("#ProbationDay").val(day);
	}
	//--------------------------------------------------试用期-结束--------------------------------------------------------

	//--------------------------------------------------合同期限-开始------------------------------------------------------

	ValidDateChange = function () {
	    var ValidDateField = $('input[name="ValidDate"]');
	    var MatureDateField = $('input[name="MatureDate"]');
	    if (ValidDateField.val() == "" || MatureDateField.val() == "") {
	        return;
	    }
	    var oldStartTime = ValidDateField.val(); //记录老时间
	    ValidDateField.unbind('change');
	    ValidDateField.bind('change',
	    function () {
	        var ProStartTime = ValidDateField.val();
	        var ProEndTime = MatureDateField.val();
	        if (ProStartTime >= ProEndTime) {
	            showError('提示', '生效时间不能小于到期时间！');
	            ValidDateField.val(oldStartTime);
	            return;
	        }
	        calcMD2(ProStartTime, ProEndTime);
	    });
	}
	//下拉选择试用结束时间的时候
	MatureDateChange = function () {
	    var ValidDateField = $('input[name="ValidDate"]');
	    var MatureDateField = $('input[name="MatureDate"]');
	    if (ValidDateField.val() == "" || MatureDateField.val() == "") {
	        return;
	    }
	    var oldEndTime = MatureDateField.val(); //记录老时间
	    MatureDateField.unbind('change');
	    MatureDateField.bind('change',
	    function () {
	        var ProStartTime = ValidDateField.val();
	        var ProEndTime = MatureDateField.val();
	        if (ProStartTime >= ProEndTime) {
	            showError('提示', '生效时间不能小于到期时间！');
	            MatureDateField.val(oldEndTime);
	            return;
	        }
	        calcMD2(ProStartTime, ProEndTime);
	    });
	}
	//计算合同期限月、天
	function calcMD2(ProStartTime, ProEndTime) {

	    var modth, year;
	    var cday = datedifference(ProStartTime, ProEndTime);
	    year = parseInt((cday * 1) / 365); //取整 相差多少年
	    modth = parseInt((cday * 1) / 30) - 12 * year; //相差多少月

	    $("#ContractTermYear").val(year);
	    $("#ContractTermMonth").val(modth);
	}
	//--------------------------------------------------合同期限-结束------------------------------------------------------
    
    //两个时间相差天数 兼容firefox chrome
    function datedifference(sDate1, sDate2) { 
        var dateSpan,
            tempDate,
            iDays;
        sDate1 = Date.parse(sDate1);
        sDate2 = Date.parse(sDate2);
        dateSpan = sDate2 - sDate1;
        dateSpan = Math.abs(dateSpan);
        iDays = Math.floor(dateSpan / (24 * 3600 * 1000));
        return iDays
    };
}