function CarInsuranceIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' 
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight * 0.7 - 100
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true 
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80}
            , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
            , { label: '车牌号', name: 'Car.CarNo', index: 'Car.CarNo', width: 80 }
            , { label: '保单类别', name: 'InsuranceType', index: 'InsuranceType', width: 80 }
            , { label: '保险单位', name: 'InsuranceUnit', index: 'InsuranceUnit', width: 80, hidden: true }
            , { label: '保险单位', name: 'CarDealings.Name', index: 'CarDealings.Name', width: 80 }
            , { label: '保单类别', name: 'InsuranceType', index: 'InsuranceType', width: 80 }
            , { label: '联系人', name: 'Linker', index: 'Linker', width: 80 }
            , { label: '联系电话', name: 'LinkPhone', index: 'LinkPhone', width: 80 }
            , { label: '投保人', name: 'InsuranceMan', index: 'InsuranceMan', width: 80 }
            , { label: '保单号', name: 'InsuranceNo', index: 'InsuranceNo', width: 80 }
            , { label: '是否交强险', name: 'IsMust', index: 'IsMust', width: 80, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }

            , { label: '车船费', name: 'OtherCost', index: 'OtherCost', width: 80 }
            , { label: '总费用', name: 'MoneyAll', index: 'MoneyAll', width: 80 } 
            , { label: '起始日期', name: 'StartDate', index: 'StartDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            , { label: '终止日期', name: 'EndDate', index: 'EndDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
           
            , { label: '经办人', name: 'Opreater', index: 'Opreater', width: 80 }
            , { label: '经办日期', name: 'OpreateDate', index: 'OpreateDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
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
            handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'CarInsuranceMyFormDiv',
                    btn: btn ,
                    width: 500,
				    height: 400
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarInsuranceMyFormDiv',
                    btn: btn,
                    width: 500,
				    height: 400,
                    prefix: "CarInsurance" 
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });




    var myJqGridTo2 = new MyGrid({
        renderTo: 'MyJqGridTo2',
        title: '保险明细',
        autoWidth: true,
        buttons: buttons1,
        height: gGridHeight * 0.3,
        storeURL: options.DelUrl,
        sortByField: 'ID',
        dialogWidth: 480,
        dialogHeight: 300,
        primaryKey: 'ID',
        setGridPageSize: 30,
        showPageBar: true,
        initArray: [{
            label: 'ID',
            name: 'ID',
            index: 'ID',
            width: 80,
            hidden: true
        },
		{
		    label: '保险单号',
		    name: 'CarInsuranceID',
		    index: 'CarInsuranceID',
		    width: 100,
		    hidden: true
		}, 
		{
		    label: '保险名称',
		    name: 'CarInsuranceName',
		    index: 'CarInsuranceName',
		    width: 100
		},
		{
		    label: '费用',
		    name: 'ItemMoney',
		    index: 'ItemMoney',
		    width: 100
		}]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                myJqGridTo2.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridTo2.refreshGrid();
            },
            handleAddDetial: function (btn) {
                var keys = myJqGrid.getSelectedKeys();
                if (keys.length == 0) {
                    showMessage('提示', "请选择需要的单据主体!");
                    return;
                }
                var Record = myJqGrid.getSelectedRecord();
                var id = Record.ID;
                myJqGrid.handleAdd({
                    loadFrom: 'CarInsuranceItemForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#CarInsuranceItem_CarInsuranceID").val(id);
                       
                    },
                    postCallBack: function (response) {
                        myJqGridTo2.refreshGrid();
                    }
                });
            },
            handleEditDetial: function (btn) {
                myJqGridTo2.handleEdit({
                    loadFrom: 'CarInsuranceItemForm',
                    btn: btn,
                    prefix: "CarInsuranceItem",
                    afterFormLoaded: function () {
                        
                    },
                    postCallBack: function (response) {
                        myJqGridTo2.refreshGrid();
                    }
                });
            },
            handleDeleteDetial: function (btn) {
                myJqGridTo2.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }

    });

    myJqGrid.addListeners('rowclick',
	function (id, record, selBool) {
	    myJqGridTo2.refreshGrid("CarInsuranceID=" + id);
	});

 
}