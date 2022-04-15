function CarAccidentIndexInit(options) {
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
            , { label: '驾驶员', name: 'Driver', index: 'Driver', width: 80 }
            , { label: '事故日期', name: 'AccidentDate', index: 'AccidentDate', width: 100, formatter: 'date' }
            , { label: '现场交警', name: 'TrafficPolice', index: 'TrafficPolice', width: 80 }
            , { label: '损失金额', name: 'LossCost', index: 'LossCost', width: 80 }
            , { label: '理赔金额', name: 'SettleCost', index: 'SettleCost', width: 80 }
            , { label: '损坏程度', name: 'SettleDegree', index: 'SettleDegree', width: 80 }
            , { label: '保险单位', name: 'InsuranceUnit', index: 'InsuranceUnit', width: 80, hidden: true }
            , { label: '保险单位', name: 'CarDealings.Name', index: 'CarDealings.Name', width: 80 }
            , { label: '保单号', name: 'InsuranceNo', index: 'InsuranceNo', width: 80 }
            , { label: '对方车牌号', name: 'SideCarNo', index: 'SideCarNo', width: 80 }
            , { label: '对方姓名', name: 'SideName', index: 'SideName', width: 80 }
            , { label: '对方电话', name: 'SidePhone', index: 'SidePhone', width: 80 }
            , { label: '对方车型', name: 'SideCarType', index: 'SideCarType', width: 80 }
            , { label: '对方单位', name: 'SideUnit', index: 'SideUnit', width: 80 }
            , { label: '对方损坏程度', name: 'SideDegree', index: 'SideDegree', width: 80 }


            , { label: '事故描述', name: 'AccidentDec', index: 'AccidentDec', width: 200 }
            , { label: '人员损伤', name: 'PeopleDec', index: 'PeopleDec', width: 200 }
            , { label: '事故地点', name: 'KeepUnit', index: 'KeepUnit', width: 80, hidden: true }
            , { label: '保养单位', name: 'AccidentAdress', index: 'AccidentAdress', width: 80 } 
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
                    loadFrom: 'CarAccidentMyFormDiv',
                    btn: btn,
                    width: 700,
				    height: 500 
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'CarAccidentMyFormDiv',
                    btn: btn,
                    width: 700,
				    height: 500 
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    myJqGrid.addListeners('rowdblclick', function (id, record, selBool) {

        myJqGrid.handleEdit({
            title: '查看事故详细',
            loadFrom: "CarAccidentMyFormDiv", 
            width: 700,
			height: 500 ,
            buttons: {
                "关闭": function () {
                    $(this).dialog('close');
                }
            },
            afterFormLoaded: function () { 
            }
        });
    });
}