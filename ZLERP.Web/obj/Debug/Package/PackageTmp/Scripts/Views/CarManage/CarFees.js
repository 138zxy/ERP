function CarFeesIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' 
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
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
            , { label: '费用名称', name: 'Name', index: 'Name', width: 80 } 
            , { label: '缴费日期', name: 'FeeDate', index: 'FeeDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            , { label: '缴费金额', name: 'FeeCount', index: 'FeeCount', width: 80 }
            , { label: '经办人', name: 'Opreater', index: 'Opreater', width: 80 }
            , { label: '缴费方式', name: 'FeeType', index: 'FeeType', width: 80 }
            , { label: '收费单位', name: 'SignUnit', index: 'SignUnit', width: 80, hidden: true }
            , { label: '收费单位', name: 'CarDealings.Name', index: 'CarDealings.Name', width: 80 }
            , { label: '下次缴费日期', name: 'NextFeeDate', index: 'NextFeeDate', width: 80, formatter: 'date' } 
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
                    loadFrom: 'CarFeesMyFormDiv',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarFeesMyFormDiv',
                    btn: btn
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            var cle = myJqGrid.getRecordByKeyValue(cl);

            var nextdate = cle.NextFeeDate;
            var nowdate = new Date();
            var datenow = nowdate.toISOString(); 
            if (datenow >= nextdate) {
                myJqGrid.getJqGrid().setCell(cl, "NextFeeDate", '', { color: 'red' }, '');
                var $id = $(document.getElementById(ids[i]));
                $id.removeAttr("style");
                $id.removeClass("ui-widget-content");
                document.getElementById(ids[i]).style.backgroundColor = "#FFEC8B";
            }
        }
    }) 
}