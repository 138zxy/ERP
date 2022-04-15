//油价设置
function customerIndexInit(opts) {
    var customersgrid = new MyGrid({
        renderTo: 'custid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , primaryKey: 'ID'
            , sortByField: 'ID'
            , showPageBar: true
            , setGridPageSize: 30
            , buttons: buttons0
            , dialogWidth: 350
            , dialogHeight: 200
            , storeURL: opts.storeUrl
            , storeCondition: '1=1'          
            , autoLoad: true
            , initArray: [
			    { label: '编码', name: 'ID', index: 'ID', width: 100 },
   		        { label: '生效日期', name: 'StartDate', index: 'StartDate', width: 200, formatter: 'date' },
                { label: '燃油类型', name: 'OilTypeName', index: 'OilTypeName'},
   		        { label: '区间油价(元/L)', name: 'OilPrice', index: 'OilPrice' }
   		        

		    ]
            , functions: {
                handleRefresh: function (btn) {
                    customersgrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    customersgrid.handleAdd({
                        loadFrom: 'custForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            
                            customersgrid.setFormFieldReadOnly('ID', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    customersgrid.handleEdit({
                        btn: btn,
                        loadFrom: 'custForm'
                    });
                },
                handleDelete: function (btn) {
                    customersgrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , reloadGrid: true

                    });
                },
                ShowAll: function (btn) {
                    customersgrid.refreshGrid('1=1');
                }
            }
    });

} 