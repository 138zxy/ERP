function CarCertificateIndexInit(options) {
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
            , { label: '所属公司', name: 'Car.CompanyName', index: 'Car.CompanyName', width: 200 } 
            , { label: '证照名称', name: 'Name', index: 'Name', width: 80 }
            , { label: '证照类型', name: 'CertificateType', index: 'CertificateType', width: 80 }
            , { label: '下次检验日期', name: 'NextDate', index: 'NextDate', width: 80, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            , { label: '证照费用', name: 'CertificateCost', index: 'CertificateCost', width: 80 }
            , { label: '生效日期', name: 'EffectDate', index: 'EffectDate', width: 80, formatter: 'date'}
            , { label: '证照号码', name: 'CertificateNo', index: 'CertificateNo', width: 80 }
            , { label: '签发单位', name: 'SignUnit', index: 'SignUnit', width: 80, hidden: true   }
            , { label: '签发单位', name: 'CarDealings.Name', index: 'CarDealings.Name', width: 80 }
            , { label: '经办日期', name: 'OprateDate', index: 'OprateDate', width: 80, formatter: 'date' }
            , { label: '经办人', name: 'Oprater', index: 'Oprater', width: 80 }
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
                    loadFrom: 'CarCertificateMyFormDiv',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarCertificateMyFormDiv',
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

            var nextdate = cle.NextDate;
            var nowdate = new Date();
            var datenow = nowdate.toISOString();

            if (datenow >= nextdate) {
                myJqGrid.getJqGrid().setCell(cl, "NextDate", '', { color: 'red' }, '');
                var $id = $(document.getElementById(ids[i]));
                $id.removeAttr("style");
                $id.removeClass("ui-widget-content");
                document.getElementById(ids[i]).style.backgroundColor = "#FFEC8B";
            }
        }
    }) 
}