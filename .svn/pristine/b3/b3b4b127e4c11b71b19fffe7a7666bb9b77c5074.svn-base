//砼强度
function conStrengthIndexInit(storeUrl) {
    function DataTypeValues() {
        return { '': '', 0: '混凝土', 1: '湿拌', 2: '干混' };
    }
    function DataTypeStateFmt(cellvalue, options, rowObject) {
        var style = "color:Blue;";
        var txt = "";
        if (cellvalue == 0) {
            txt = "混凝土";
        } else if (cellvalue == 1) {
            style = "color:Green;";
            txt = "湿拌";
        } else if (cellvalue == 2 || cellvalue == -1) {
            style = "color:Red;";
            txt = "干混";
        } else {
            style = "color:black;";
            txt = "您的合同状态有问题，请修复！";
        }
        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>';
    }
    var ConStrengthGrid = new MyGrid({
        renderTo: 'ConStrengthid'

            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ConStrengthCode'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 400
            , dialogHeight: 500
		    , initArray: [
                  { label: ' 编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 强度代号', name: 'ConStrengthCode', index: 'ConStrengthCode', width: 80 }
                , { label: ' 等级（MPa）', name: 'Level', index: 'Level' }
                , { label: ' 基准价', name: 'BrandPrice', index: 'BrandPrice', align: 'right', width: 80 }
                , { label: ' 建议售价', name: 'AdvisePrice', index: 'AdvisePrice', align: 'right', width: 80 }
                , { label: ' 泵送加价', name: 'BumpAddPrice', index: 'BumpAddPrice', align: 'right', width: 80 }
                , { label: ' 换算率(T/m³)', name: 'Exchange', index: 'Exchange', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: ' 产品类型', name: 'IsSH', index: 'IsSH', formatter: DataTypeStateFmt, unformat: booleanUnFmt, width: 50, searchoptions: { value: DataTypeValues() }, stype: 'select' }
                , { label: ' 基础砼强度', name: 'IsBase', index: 'IsBase', formatter: booleanFmt, unformat: booleanUnFmt, width: 80, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: ' 定价砼强度', name: 'IsBaseCon', index: 'IsBaseCon', formatter: booleanFmt, unformat: booleanUnFmt, width: 80, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: ' NC编码', name: 'NCCode', index: 'NCCode', width: 50, align: 'center'}
                , { label: ' 序号', name: 'OrderNum', index: 'OrderNum', width: 50, align: 'center', hidden: true }

		    ]
            , functions: {
                handleReload: function (btn) {
                    ConStrengthGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ConStrengthGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    ConStrengthGrid.handleAdd({
                        loadFrom: 'ConStrengthForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    var data = ConStrengthGrid.getSelectedRecord();
                    ConStrengthGrid.handleEdit({
                        loadFrom: 'ConStrengthForm',
                        btn: btn,
                        afterFormLoaded: function () {
                             ConStrengthGrid.getFormField("Exchange").val(data.Exchange / 1000); 
                        }
                    });
                }
                , handleDelete: function (btn) {
                    ConStrengthGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
        ConStrengthGrid.getJqGrid().jqGrid('setGridWidth',$("#ConStrengthid").width());
    }