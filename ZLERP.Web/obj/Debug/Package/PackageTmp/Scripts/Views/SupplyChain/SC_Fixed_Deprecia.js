function SC_Fixed_DepreciaIndexInit(options) {

    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
        , storeCondition: "(Condition = '正常' OR Condition= '已借出' OR Condition = '维修中')"
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: 'FixedID', name: 'FixedID', index: 'FixedID', width: 80, hidden: true }
            , { label: '资产编号', name: 'Fcode', index: 'Fcode', width: 120 }
            , { label: '条形码', name: 'BarCode', index: 'BarCode', width: 80 }
            , { label: '资产名称', name: 'Fname', index: 'Fname', width: 80 }
            , { label: '资产拼音简码', name: 'BrevityCode', index: 'BrevityCode', width: 80 }
            , { label: '类型', name: 'Ftype', index: 'Ftype', width: 80 }
            , { label: '规格', name: 'Spec', index: 'Spec', width: 80 }
            , { label: '价格(元)', name: 'FixedPirce', index: 'FixedPirce', width: 80 }
            , { label: '增加日期', name: 'AddDate', index: 'AddDate', width: 80, formatter: 'date' } 
            , { label: '使用部门', name: 'DepartMent', index: 'DepartMent', width: 80 }
            , { label: '净残值率(%)', name: 'NetSalvageRate', index: 'NetSalvageRate', width: 80 }
            , { label: '净残值', name: 'NetSalvage', index: 'NetSalvage', width: 80 }
            , { label: '折旧方法', name: 'Depreciation', index: 'Depreciation', width: 80 }
            , { label: '可用年限', name: 'UseYear', index: 'UseYear', width: 80 }
            , { label: '可用截止日期', name: 'UseEndDate', index: 'UseEndDate', width: 80, formatter: 'date' }
            , { label: '月折旧率', name: 'DepreciaMonthRate', index: 'DepreciaMonthRate', width: 80 }
            , { label: '月标准折旧(元)', name: 'DepreciaMonthMoney', index: 'DepreciaMonthMoney', width: 90 }
            , { label: '状态', name: 'Condition', index: 'Condition', width: 80 }
            , { label: '清理日期', name: 'ClearDate', index: 'ClearDate', width: 80, formatter: 'date' }
            , { label: '清理转售金额', name: 'ClearMoney', index: 'ClearMoney', width: 80 }
            , { label: '分析截止月份', name: 'AnalysisMonth2', index: 'AnalysisMonth2', width: 90 }
            , { label: '实际截止月份', name: 'ActualMonth2', index: 'ActualMonth2', width: 90 }
            , { label: '本年有效折旧月数', name: 'DepreciaUseMonth', index: 'DepreciaUseMonth', width: 100 }
            , { label: '累计有效折旧月数', name: 'DepreciaAllMonth', index: 'DepreciaAllMonth', width: 100 }
            , { label: '累计有效折旧年数', name: 'DepreciaAllYear', index: 'DepreciaAllYear', width: 100 }

            , { label: '本月折旧额', name: 'DepreciaMonth', index: 'DepreciaMonth', width: 80 }
            , { label: '本年折旧额', name: 'DepreciaYear', index: 'DepreciaYear', width: 80 }
            , { label: '累计折旧额', name: 'DepreciaAll', index: 'DepreciaAll', width: 80 }
            , { label: '净值', name: 'NetWorth', index: 'NetWorth', width: 80 } 
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            } 
        }
    });

    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {
        getdetial();
    });


    window.Search = function () {
        var month = $("#SearchMonth").val();
        if (month == "") {
            showMessage('提示', '请输入分析月份！');
            return;
        }
        ajaxRequest(options.SerrchUrl, { month: month },
				false,
				function (response) {
				    if (response.Result) {
				        showMessage('提示', '分析成功！');
				        if ($("#IsClear").attr("checked")) {
				            myJqGrid.refreshGrid("1=1");
				        }
				        else {
				            myJqGrid.refreshGrid("(Condition = '正常' OR Condition= '已借出' OR Condition = '维修中')");
				        }
				    }
				});
    }


    window.Show = function () {
        getdetial();
    }
    function getdetial() {
        myJqGrid.handleEdit({
            loadFrom: 'MyFormDiv',
            title: '固定资产折旧分析信息',
            width: 700,
            height: 600,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () {
            }
        });
    }

    $("#IsClear").change(function () {
        if ($("#IsClear").attr("checked")) {
            myJqGrid.refreshGrid("1=1");
        }
        else {
            myJqGrid.refreshGrid("(Condition = '正常' OR Condition= '已借出' OR Condition = '维修中')");
        }
    });

    var date = new Date();
    var fdate = date.getFullYear() + '-' + (date.getMonth()+1);
    $("#SearchMonth").val(fdate);

}