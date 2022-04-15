﻿function manageIndexInit(storeUrl, updateUrl, MultAuditUrl,CancelAuditUrl, tomorrowDate, todayDate) {
    var d = new Date();
    var tomorrow = tomorrowDate; //d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
    var condition = "PlanDate >= '" + todayDate + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59'";
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
          //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
          //, storeCondition: condition
          //, sortByField: 'NeedDate,BuildTime, ConstructUnit '
            , sortByField: 'ID'
            , sortOrder: 'DESC'
		    , primaryKey: 'ID'
            , dialogWidth: 500    //编辑Form显示时的 宽度和高度
            , dialogHeight: 1000
		    , setGridPageSize: 100
		    , showPageBar: true
            , singleSelect: false
            , groupField: 'PlanDate'
            , editSaveUrl: updateUrl
            , storeCondition: condition
            , groupingView: { groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], groupSummary: [true], showSummaryOnHide: true, minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                  { label: '工地计划编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', search: true, formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '计划日期', name: 'PlanDate', index: 'PlanDate', formatter: 'date', search: false, sortable: false, width: 80 }
                , { label: '到场时间', name: 'NeedDate', index: 'NeedDate', search: false, width: 60 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', search: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, width: 60 }
                , { label: '合同号', name: 'ContractNo', index: 'ContractNo', width: 100 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
                , { label: '客户名称', name: 'ContractGH.Customer.CustName', index: 'ContractGH.Customer.CustName', width: 100 }
                , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit' }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
                , { label: '交货地址', name: 'DeliveryAddress', index: 'DeliveryAddress' }
                , { label: '工地电话', name: 'Tel', index: 'Tel' }
                , { label: '强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
                , { label: '浇筑方式(干混)', name: 'CastMode', index: 'CastMode', width: 60 }
                , { label: '计划量', name: 'PlanCube', index: 'PlanCube', width: 60, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 60 }
                , { label: '剩余金额', name: 'Balance', index: 'Balance', width: 80 }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr' }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit' }
                , { label: '坍落度', name: 'Slump', index: 'Slump', width: 60, hidden: true }
                , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 100, hidden: true }
                , { label: '区间', name: 'RegionID', index: 'RegionID', width: 80 }
                , { label: '开盘安排', name: 'PiepLen', index: 'PiepLen', width: 50 }                
                , { label: '工地联系人', name: 'LinkMan', index: 'LinkMan' }
                , { label: '泵工', name: 'PumpMan', index: 'PumpMan', width: 60, hidden: true }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName' }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', search: false }
                , { label: '审核意见', name: 'AuditInfo', index: 'AuditInfo', search: false }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', search: false }
                , { label: '工地计划编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '交货地址', name: 'DeliveryAddress', index: 'DeliveryAddress', width: 100 }		    
            ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                //审核
                handleAudit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();

                    if (keys.length == 1) {
                        var record = myJqGrid.getSelectedRecord();
                        if (record && record.AuditStatus != '0') {
                            showMessage('该计划单已审核');
                            return;
                        }
                        myJqGrid.handleEdit({
                            loadFrom: 'MyFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                                myJqGrid.setFormFieldReadOnly('ConStrength', true);
                                myJqGrid.setFormFieldReadOnly('Slump', true);
                                myJqGrid.setFormFieldReadOnly('CastMode', true);
                                myJqGrid.setFormFieldReadOnly('NeedDate', true);
                                myJqGrid.setFormFieldReadOnly('ProjectName', true);
                            }
                            ,
                            height: 480
                        });
                    }
                    else if (keys.length > 1) {
                        showConfirm("确认信息", "确认要进行批量审核操作?", function (btn) {
                            ajaxRequest(MultAuditUrl, { ids: keys }, true, function () {
                                myJqGrid.reloadGrid();
                            });
                            $(this).dialog("close");
                        });
                    }
                    else {
                        showMessage('提示', '没有选择任何记录！');
                        return;
                    }
                },
                //批量审核
                handleMultAudit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length > 0) {
                        var requestURL = MultAuditUrl;
                        var postParams = { ids: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            myJqGrid.reloadGrid();
                        });
                    }
                    else {
                        showMessage('提示', '没有选择任何记录！');
                        return;
                    }
                },
                //取消审核
                handleCancelAudit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length > 0) {
                        var record = myJqGrid.getSelectedRecord();
                        if (record && record.AuditStatus == '0') {
                            showError('该计划单未审核，不需要取消！');
                            return;
                        }
                        showConfirm("确认信息", "确认要进行取消审核操作?", function (btn) {
                            var requestURL = CancelAuditUrl;
                            var postParams = { ids: keys };
                            ajaxRequest(requestURL, postParams, true, function (response) {
                                myJqGrid.reloadGrid();
                            });
                            $(this).dialog("close");
                        });
                    }
                    else {
                        showMessage('提示', '没有选择任何记录！');
                        return;
                    }
                }
                //删除记录
                , handleDelete: function (btn) {
                    var record = myJqGrid.getSelectedRecord()
                    if (record && record.AuditStatus == "1") {
                        showError("错误", "已审核记录不能删除！");
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //工地计划报表
                , handleReport: function (btn) {
                    window.open("/Reports/Produce/R310605.aspx");
                }
                , handleTomorrowPlan: function (btn) {
                    var d = new Date();
                    var tomorrow = tomorrowDate; //d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
                    var condition = "PlanDate >= '" + tomorrow + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59'";
                    myJqGrid.refreshGrid(condition);
                }
                , handleTodayPlan: function (btn) {
                    var d = new Date();
                    var today = d.format("Y-m-d");
                    var condition = "PlanDate >= '" + today + " 00:00:00' and PlanDate < '" + today + " 23:59:59'";
                    myJqGrid.refreshGrid(condition);
                }
                , handleAllPlan: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                }
            }
    });    
}