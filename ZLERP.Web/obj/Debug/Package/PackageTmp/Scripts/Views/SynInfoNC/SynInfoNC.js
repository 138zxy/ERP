function SynInfoNCIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 515
            , dialogHeight: 334
            , initArray: [
                      { label: 'ID', name: 'ID', index: 'ID' }
                    ,  { label: 'synNC的自编号ID', name: 'SynNCID', index: 'SynNCID' }
                    , { label: '单据编号', name: 'NO', index: 'NO' }
                    , { label: '单据类型', name: 'Type', index: 'Type' }
                    , { label: '参数报文信息', name: 'ParamsInfo', index: 'ParamsInfo' }
                    , { label: 'NC接口返回的报文信息', name: 'ReturnInfo', index: 'ReturnInfo', hidden: true }
                    , { label: '同步时间', name: 'SynDate', index: 'SynDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                    , { label: '返回编码', name: 'NcResultCode', index: 'NcResultCode' }
                    , { label: '返回值', name: 'NcResultDescription', index: 'NcResultDescription' }
                    , { label: '返回主体', name: 'NcContent', index: 'NcContent' }
            ]
        //代码生成新增，修改，删除，刷新的JS代码
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid(' SynNCID in ( select Id from SynNC where Status=-1) ');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {

                        }
                    });
                }
                , handleReUpload: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行上传");
                        return;
                    }
                    showConfirm("确认信息", "是否继续上传？", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            opts.ReUploadUrl,
                            {
                                ids: keys
                            },
                            true,
                            function (response) {
                                $(btn.currentTarget).button({ disabled: false });
                                if (response.Result) {
                                    myJqGrid.reloadGrid();
                                }
                            }
                        );

                        $(this).dialog("close");
                    });
                }
            }
    });

}