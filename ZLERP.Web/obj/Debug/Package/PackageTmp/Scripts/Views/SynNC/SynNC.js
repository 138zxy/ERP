function SynNCIndexInit(opts) {
    //入库状态（入库单）
    function NCTypeFmt(cellvalue, options, rowObject) {
        var style = "color:Green;";
        var txt = "";
        if (cellvalue == 0) {
            txt = "原材料入库单";
        } else if (cellvalue == 1) {
            style = "color:Green;";
            txt = "产成品入库单";
        } else if (cellvalue == 2 ) {
            style = "color:Green;";
            txt = "其它入库单";
        } else if (cellvalue == 3) {
            style = "color:Green;";
            txt = "销售出库单";
        } else if (cellvalue == 4) {
            style = "color:Green;";
            txt = "材料出库单";
        } else if (cellvalue == 5) {
            style = "color:Green;";
            txt = "其它出库单";
        } else {
            style = "color:black;";
            txt = "您的单据状态有问题，请修复！";
        }
        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>';
    }
    function NCTypeValues() {
        return { '': '', 0: '原材料入库单', 1: '产成品入库单', 2: '其它入库单', 3: '销售出库单', 4: '材料出库单', 5: '其它出库单' };
    }
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: " Status= -1 "
		    , sortByField: 'BuildTime '
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 515
            , dialogHeight: 334
            , initArray: [
//                      { label: 'ID', name: 'ID', index: 'ID' }
//                    ,  { label: 'synNC的自编号ID', name: 'SynNCID', index: 'SynNCID' }
//                    , { label: '单据编号', name: 'NO', index: 'NO' }
//                    , { label: '单据类型', name: 'Type', index: 'Type' }
//                    , { label: '参数报文信息', name: 'ParamsInfo', index: 'ParamsInfo' }
//                    , { label: 'NC接口返回的报文信息', name: 'ReturnInfo', index: 'ReturnInfo', hidden: true }
//                    , { label: '同步时间', name: 'SynDate', index: 'SynDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
//                    , { label: '返回编码', name: 'NcResultCode', index: 'NcResultCode' }
//                    , { label: '返回值', name: 'NcResultDescription', index: 'NcResultDescription' }
//                    , { label: '返回主体', name: 'NcContent', index: 'NcContent' }

                    { label: '自增长编号', name: 'Id', index: 'Id', hidden: true }
                    , { label: '单据类型', name: 'Type', index: 'Type', formatter: NCTypeFmt, searchoptions: { value: NCTypeValues() }, stype: 'select' }
                    , { label: '单据编号', name: 'NO', index: 'NO' }
                    , { label: '上传结果', name: 'Result', index: 'Result',width:400 }
                    , { label: 'True为正数、False为负数', name: 'IsPositive', index: 'IsPositive', hidden: true }
                    , { label: '同步状态 -1上传失败、0未上传、1已上传', name: 'Status', index: 'Status', hidden: true }
                    ,{ label: '创建时间', name: 'BuildTime', index: 'BuildTime',  formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']}}
                    ,{ label: '同步时间', name: 'SynDate', index: 'SynDate',  formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']}}
            
            ]
        //代码生成新增，修改，删除，刷新的JS代码
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid(' 1=1 ');
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