function sc_goodstypeIndexInit(storeUrl) {
    var name1 = "商品", name2 = "固定资产", name3 = "未知";
    //状态列值处理 
    function lockFmt(cellvalue, options, rowObject) {
        if (cellvalue == '1') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = name1;
            }
        } else if (cellvalue == '2') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = name2;
            }
        }     
        else {
            var txt = name3;
        }
        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }

    function lockUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }

    var myJqGrid = new MyGrid({
            renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'OrderNo'
            , sortorder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: -1
		    , showPageBar: false
            , singleSelect: true
            , treeGrid: true
            , treeGridModel: 'adjacency'
            , expandColumn: 'TypeName'
            , expandColClick: true
            , autoLoad: true
            , editSaveUrl: "/SC_GoodsType.mvc/Update"
            //, groupField: 'Flag'
            //, groupingView: { groupSummary: [true], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
            , initArray: [
                { label: '分类名称', name: 'TypeName', index: 'TypeName' }
                , { label: '编码', name: 'ID', index: 'ID', hidden: true }
                , { label: '父节点', name: 'ParentID', index: 'ParentID' }
                , { label: '排序号', name: 'OrderNo', index: 'OrderNo', editable: true }
                , { label: '标识分类', name: 'Flag', index: 'Flag', formatter: lockFmt, unformat: lockUnFmt }
                , { label: ' 是否叶子节点', name: 'IsLeaf', index: 'IsLeaf', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
		    ]
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('ID', false);//$("input[name='ParentID_text']").attr("readonly", "readonly");
                            $("input[name='ParentID_text']").attr("disabled", "disabled"); //将input元素设置为disabled
                            var pid = myJqGrid.getSelectedKey();
                            if (pid != null) {
                                var record = myJqGrid.getRecordByKeyValue(pid);
                                myJqGrid.setFormFieldValue('ParentID', pid);
                                myJqGrid.setFormFieldValue('ParentID_text', record.TypeName);

                                myJqGrid.setFormFieldValue('Flag', record.Flag);
                                $("#FlagName").attr("disabled", "disabled");
                                if (record.Flag == "1") {
                                    $("#FlagName").val(name1);
                                } else if (record.Flag == "2") {
                                    $("#FlagName").val(name2);
                                } else {
                                    $("#FlagName").val(name3);
                                }

                            }
                        }
                    });
                },
                handleEdit: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record.ParentID == "0") {
                        showError("提示", "此类不能修改！");
                        return false;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        //prefix: "csa",
                        afterFormLoaded: function () {
                            $("input[name='ParentID_text']").removeAttr("disabled"); //将input元素设置为disabled
                            var pid = myJqGrid.getSelectedKey();
                            if (pid != null) {
                                var record = myJqGrid.getSelectedRecord();
                                myJqGrid.setFormFieldValue('ParentID', record.ParentID);
                                myJqGrid.setFormFieldValue('ParentID_text', record.TypeName);

                                $("#FlagName").attr("disabled", "disabled");
                                if (record.Flag == "1") {
                                    $("#FlagName").val(name1);
                                } else if (record.Flag == "2") {
                                    $("#FlagName").val(name2);
                                } else {
                                    $("#FlagName").val(name3);
                                }
                            }
                        }
                    });
                }
                , handleDelete: function (btn) {

                    var record = myJqGrid.getSelectedRecord();
                    if (record.ParentID == "0") {
                        showError("提示", "此类不能删除！");
                        return false;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}