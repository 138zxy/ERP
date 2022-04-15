//合同余额
function balanceIndexInit(options) {

    //重置表单样式
    function resetClass(obj) {
        if (!$.isEmptyObject(obj)) {
            $.each(obj, function (i, n) {
                if (n == "removeClass") {
                    $("input[name='" + i + "']").removeClass("text requiredval");
                } else {
                    $("input[name='" + i + "']").addClass("text requiredval");
                }
            });
        }
    }


    //合同运输单明细
    var ContractItemGrid = new MyGrid({
        renderTo: 'ContractItemGrid'
            , title: '运输单明细'
            , autoWidth: true
            , buttons: buttons1
            , height: (gGridWithTitleHeight - 112) / 2
            , storeURL: options.itemStoreUrl
            , storeCondition: "IsEffective = '1' "
            , sortByField: 'ShipDocID'
            , sortOrder: 'DESC'
            , primaryKey: 'ShipDocID'
            , setGridPageSize: 50
            , showPageBar: true
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 200
            , singleSelect: true
            , initArray: [
                { label: '是否有效', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '无效', '1': '有效' }, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: 'CustomerID', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: 'TaskID', name: 'TaskID', index: 'TaskID', hidden: true }
                , { label: '方量', name: 'SignInCube', index: 'SignInCube', width: 50, align: 'center' }
                , { label: '单价', name: 'price', index: 'price', width: 50, align: 'center' }
                , { label: '金额', name: 'money', index: 'money', width: 50, align: 'center' }
                ,{ label: '运输单编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 50, align: 'center' }
                , { label: '单价', name: 'price', index: 'price', width: 30, align: "center" }
                , { label: '总价', name: 'sumCount', index: 'sumCount', width: 50, align: "center" }
                , { label: '出站时间', name: 'DeliveryTime', index: 'DeliveryTime', width: 120, formatter: 'datetime' }
                , { label: '到场时间', name: 'ArriveTime', index: 'ArriveTime', width: 120, formatter: 'datetime' }
            ]
            , functions: {
                handleReload: function (btn) {
                    ContractItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractItemGrid.refreshGrid();
                }

            }
    });

    var UserGrid = new MyGrid({
        renderTo: "Contractid"
            , title: "客户列表"
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: "buttonToolbar"
            , height: gGridWithTitleHeight
            , dialogWidth: 800
            , dialogHeight: 600
            , storeURL: options.UserStoreUrl
            , storeCondition: 'IsUse=1'
            , sortByField: "ID"
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
            , initArray: [
			    { label: '客户编码', name: 'ID', index: 'ID', width: 100 },
                { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt, width: 60, stype: 'select', searchoptions: { value: booleanSearchValues()} },
   		        { label: '客户名称', name: 'CustName', index: 'CustName', width: 200 },
   		        { label: '简称', name: 'ShortName', index: 'ShortName' },
                { label: '余额', name: 'AccountBalance', index: 'AccountBalance' },
   		        { label: '客户类型', name: 'CustType', index: 'CustType', stype: 'select', searchoptions: { value: dicToolbarSearchValues('CustType') }, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CustType' }, width: 80, align: 'center' },
   		        { label: '注册资金', name: 'RegFund', index: 'RegFund', width: 80 },
   		        { label: '企业印鉴', name: 'CachetPath', index: 'CachetPath' },
   		        { label: '营业地址', name: 'BusinessAddr', index: 'BusinessAddr' },
   		        { label: '发票地址', name: 'InvoiceAddr', index: 'InvoiceAddr' },
   		        { label: '营业电话', name: 'BusinessTel', index: 'BusinessTel' },
   		        { label: '营业传真', name: 'BusinessFax', index: 'BusinessFax' },
   		        { label: 'Email', name: 'Email', index: 'Email' },
   		        { label: '邮政编码', name: 'PostCode', index: 'PostCode', width: 80 },
   		        { label: '负责人性别', name: 'PrincipalSex', index: 'PrincipalSex', stype: 'select', searchoptions: { value: dicToolbarSearchValues('Gender') }, width: 80, align: 'center' },
   		        { label: '负责人', name: 'Principal', index: 'Principal', width: 80 },
   		        { label: '负责人电话', name: 'PrincipalTel', index: 'PrincipalTel' },
   		        { label: '联系人', name: 'LinkMan', index: 'LinkMan', width: 80 },
   		        { label: '联系人电话', name: 'LinkTel', index: 'LinkTel' },
   		        { label: '负责采购人', name: 'Buyer', index: 'Buyer', width: 80 },
   		        { label: '地点代码', name: 'AddrCode', index: 'AddrCode' },
   		        { label: '信用额度', name: 'CreditQuota', index: 'CreditQuota', width: 80 },
   		        { label: '是否审核', name: 'IsAudit', index: 'IsAudit', formatter: booleanFmt, unformat: booleanUnFmt, width: 60, stype: 'select', searchoptions: { value: booleanSearchValues()} },
   		        { label: '税务代码', name: 'TaxCode', index: 'TaxCode' },
   		        { label: '备注', name: 'Remark', index: 'Remark' }

		    ]
             , functions: {
                 handleReload: function (btn) {
                     UserGrid.reloadGrid();
                 },
                 handleRefresh: function (btn) {
                     UserGrid.refreshGrid("IsUse=1");
                 }
                 //垫资
                 ,handleAddDZ: function (btn) {
                        if (!UserGrid.isSelectedOnlyOne()) {
                                         showMessage('提示', '请选择一条记录进行操作！');
                                         return;
                        }
                        var selectrecord = UserGrid.getSelectedRecord();

                        UserGrid.handleAdd({
                                         btn: btn,
                                         width: 400,
                                         height: 280,
                                         loadFrom: "DZForm",
                                         reloadGrid: false,
                                         afterFormLoaded: function () {
                                             UserGrid.setFormFieldValue("ContractDZ.CustomerID", selectrecord.ID);
                                         }
                                         , postCallBack: function (response) {
                                             dzGrid.refreshGrid("CustomerID='" + selectrecord.ID + "'");
                                         }
                                     });
                       }
                 , handleAddAccountBalance: function (btn) {
                     if (!UserGrid.isSelectedOnlyOne()) {
                         showMessage("提示", "请选择一条记录进行操作！");
                         return;
                     }

                     var data = UserGrid.getSelectedRecord();
                     var id = data.ID;
                     var k = new Array();
                     k[0] = id;

                     UserGrid.handleAdd({
                         loadFrom: 'BalanceAddForm',
                         btn: btn,
                         width: 600,
                         height: 430,
                         beforeSubmit: function () {
                             balance1 = $('#Balance1', '#BalanceAddForm .mvcform').val();
                             Remark1 = $('#Remark1', '#BalanceAddForm .mvcform').val();
                            // alert(balance1 + "|" + Remark1);
                             k[1] = balance1;
                             k[2] = Remark1;
                             return true;
                         },
                         postData: { values: k }
                     });
                 }
             }
         });
             var dzGrid = new MyGrid({
                 renderTo: "dzGrid"
                     , title: "垫资记录"
                     , autoWidth: true
                     , buttons: buttons1
                     , height: (gGridWithTitleHeight - 112) / 4
                     , storeURL: "/ContractDZ.mvc/Find"
                     , sortByField: "ID"
                     , sortOrder: "ASC"
                     , primaryKey: "ID"
                     , setGridPageSize: 50
                     , showPageBar: false
                     , autoLoad: false
                     , dialogWidth: 600
                     , dialogHeight: 400
                     , singleSelect: true
                     , editSaveUrl: "/ContractDZ.mvc/Update"
                     , initArray: [
                         { label: "编号", name: "ID", index: "ID", hidden: true }
                          //, { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',formatoptions: { keys: true, editbutton: false } }
                         , { label: "客户编号", name: "CustomerID", index: "CustomerID", hidden: true }
                         , { label: "垫资时间", name: "BuildTime", index: "BuildTime",  formatter: "date", width: 80 }
                         , { label: "垫资金额", name: "DZMoney", index: "DZMoney",  width: 80, align: "center", summaryType: 'sum', summaryTpl: '{0}' }
                         , { label: "垫资人", name: "Builder", index: "Builder", width: 80, align: "center" }
                         , { label: "备注", name: "Remark", index: "Remark", width: 160, align: "center" }
                     ]
                     , functions: {
                         handleReload: function (btn) {
                             dzGrid.reloadGrid();
                         },
                         handleRefresh: function (btn) {
                             dzGrid.refreshGrid();
                         }

                     }
             });


         var ExtraPumpGrid = new MyGrid({
             renderTo: 'ExtraPumpGrid'
            , title: '余额流水'
            , autoWidth: true
            , buttons: buttons1
            , height: (gGridWithTitleHeight - 112) / 4
            , storeURL: '/Balance.mvc/Find'
            , sortByField: 'Builder'
            , sortOrder: 'DESC'
            , primaryKey: 'ID'
            , setGridPageSize: 50
            //, groupingView: { groupText: ['{0}'] }
            , groupField: 'Builder'
            , showPageBar: false
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 200
            , singleSelect: true
            , initArray: [
                { label: '流水编号', name: 'ID', index: 'ID', width: 50 }
                , { label: '金额', name: 'Balance', index: 'Balance', width: 50 }
                , { label: '说明', name: 'Remark', index: 'Remark' }
                , { label: "日期", name: "Builder", width: 80 }
                , { label: '时间', name: 'Modifier', index: 'Modifier', width: 80 }
                , { label: '操作员', name: 'User.TrueName', index: 'Value', width: 60 }
                , { label: "CustomerID", name: "CustomerID", width: 80, hidden: true }
               
            ]
            , functions: {
                handleReload: function (btn) {
                    ExtraPumpGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ExtraPumpGrid.refreshGrid();
                }

            }
        });


        dzGrid.addListeners("gridComplete", function () {
           dzGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ContractDZ.mvc/Delete" });
        });

         //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
         UserGrid.addListeners('rowclick', function (id, record, selBool) {
             ContractItemGrid.getJqGrid().setCaption("运输单明细：&nbsp;<font color='#2E6E9E'><strong>-></strong></font>&nbsp;" + record.CustName);
             ContractItemGrid.refreshGrid("CustomerID='" + id + "'");

             ExtraPumpGrid.refreshGrid("CustomerID='" + id + "'");
             dzGrid.refreshGrid("CustomerID='" + id + "'");
         });
}