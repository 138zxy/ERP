var projectMap = new abcMap();        //绘制地图

function limitRegionIndexInit(opts) {
    mapInit();
    var overlayCache = {};

    var LimitRegionGrid = new MyGrid({
        renderTo: 'LimitRegionGrid'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight / 2 - 80
        , storeURL: opts.LimitRegionStoreUrl
        , sortByField: 'ID'
        , primaryKey: 'ID'
        , setGridPageSize: 30
        , dialogWidth: 380
        , dialogHeight: 170
        , showPageBar: true
        , initArray: [
            { name: 'ID', index: 'ID', hidden: true }
            , { label: '名称', name: 'Name', index: 'Name', width: 300 }
            , { label: '名称', name: 'ProjectID', index: 'ProjectID', hidden: true }
            , { label: '是否为越界报警', name: 'IsOutAlarm', align: 'center', index: 'IsOutAlarm', width: 100, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '驶入报警', '1': '驶出报警' }, stype: 'select', searchoptions: { value: AlarmSearchValues() }, width: 60 }
            , { label: '是否显示', name: 'IsShow', index: 'IsShow', align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '隐藏', '1': '显示' }, stype: 'select', searchoptions: { value: ViewSearchValues() }, width: 60 }

        ]
        , autoLoad: false
        , functions: {
            handleReload: function (btn) {
                LimitRegionGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                LimitRegionGrid.refreshGrid('1=1');
            },
            handleEdit: function (btn) {
                LimitRegionGrid.handleEdit({
                    loadFrom: 'LimitRegionForm',
                    btn: btn
                });
            }
            , handleDelete: function (btn) {
                LimitRegionGrid.deleteRecord({
                    deleteUrl: opts.LimitRegionDeleteUrl
                });
            }
        }
    });

    var ProjectGrid = new MyGrid({
        renderTo: 'ProjectGrid'
        , autoWidth: true
        , buttons: buttons1
        , height: gGridHeight / 2
        , storeURL: '/Project.mvc/Find'
        , sortByField: 'IsShow'
        , primaryKey: 'ID'
        , setGridPageSize: 30
        , dialogWidth: 380
        , dialogHeight: 170
        , showPageBar: true
        , initArray: [
            { label: ' 工程编号', name: 'ID', index: 'ID', hidden: true }
            , { label: ' 项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 100 }
            , { label: ' 工程名称', name: 'ProjectName', index: 'ProjectName', width: 100 }
            , { label: ' 经度', name: 'Longitude', index: 'Longitude', hidden: true }
            , { label: ' 纬度', name: 'Latitude', index: 'Latitude', hidden: true }
            , { label: ' 是否显示', name: 'IsShow', index: 'IsShow', width: 50, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() } }
            , { label: ' 工程运距', name: 'Distance', width: 40, index: 'Distance' }
            , { label: ' 建设单位', name: 'BuildUnit', index: 'BuildUnit' }
            , { label: ' 施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
            , { label: '工地联系人', name: 'LinkMan', index: 'LinkMan' }
            , { label: ' 工地电话', name: 'Tel', index: 'Tel' }
            , { label: ' 合同编号', name: 'ContractID', width: 80, index: 'ContractID' }
            , { label: ' 合同名称', name: 'ContractName', index: 'ContractName' }
            , { label: ' 备注', name: 'Remark', index: 'Remark' }
            , { label: ' 工地范围', name: 'PlaceRange', index: 'PlaceRange', hidden: true }
        ]
        , autoLoad: true
        , functions: {
            handleReload: function (btn) {
                ProjectGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ProjectGrid.refreshGrid('1=1');
            },
            handleEnable: function (btn) {
                if (!ProjectGrid.isSelectedOnlyOne()) {
                    showMessage("提示", "请选择一条记录");
                    return false;
                }
                var keys = ProjectGrid.getSelectedKeys();
                ajaxRequest("/Project.mvc/MapEnable", { ids: keys }, true, function (data) {
                    ProjectGrid.reloadGrid();
                });
            },
            handleDisable: function (btn) {
                if (!ProjectGrid.isSelectedOnlyOne()) {
                    showMessage("提示", "请选择一条记录");
                    return false;
                }
                var keys = ProjectGrid.getSelectedKeys();
                ajaxRequest("/Project.mvc/MapDisable", { ids: keys }, true, function (data) {
                    ProjectGrid.reloadGrid();
                });
            }
        }
    });

    ProjectGrid.addListeners('rowclick', function (id, record, selBool) {
        LimitRegionGrid.refreshGrid("ProjectID='" + id + "'");
        var idPreffix = "prj"; var pid = id.substring(3);
        var lng = record.Longitude; var lat = record.Latitude;

        if (!isEmpty(lng) && !isEmpty(lat)) {//如果不为空
            var content = "<table class='tb_pj_img'><tr><td><img src='" + iconArr.yardIcon + "' /></td></tr><tr><td><span>" + record.ProjectName + "</span></td></tr></table>";
            var pjdata = { lng: lng, lat: lat, iurl: iconArr.yardIcon, clickEv: "", tipTxt: "", content: content, id: idPreffix + pid };
            projectMap.addPoints([pjdata], false);
            projectMap.map.panTo(new AMap.LngLat(lng, lat));
        }

        removeTempBound();
        clearAllBound();
        ajaxRequest('/LimitRegion.mvc/GetBoundInfoByPid', { id: id }, false, function (response) {
            if (response.records > 0) {
                ShowAllBoundHandle(response.rows);
            }
        });
    });

    //地图初始化
    function mapInit() {
        $("#projectMap").height($("#container").height());
        $("#projectMap").width($("#container").width() - $("#LimitRegionGrid").width());
        var opts = {
            render: "projectMap",
            cLng: companyinfo.lon,
            cLat: companyinfo.lat,
            level: 16
        };
        projectMap.init(opts);
        var fctopt = { lng: companyinfo.lon, lat: companyinfo.lat, iurl: iconArr.factIcon, clickEv: "", tipTxt: "", content: "", id: "factory_mark_s0" };
        projectMap.addPoints([fctopt], true);
        initMapContextMenu();

        //在地图中添加MouseTool插件
        projectMap.map.plugin(["AMap.MouseTool"], function () {
            mousetool = new AMap.MouseTool(projectMap.map);
            AMap.event.addListener(mousetool, "draw", completedDraw); 
        });

        //鼠标工具绘制完成之后
        function completedDraw(e) {
            var returnOverLayer = e.obj;
            var overLayerType = returnOverLayer.CLASS_NAME;
            if (isAllowCloseMouseTool) mousetool.close();
            switch (overLayerType) {
                //根据不同的覆盖物类型，做相应的处理    
                case "AMap.marker": //点标注
                case "AMap.Polygon": //多边形
                    var newPath = ArrayUnique(returnOverLayer.getPath());

                    var _mk = projectMap.map.getAllOverlays();
                    for (var i = 0; i < _mk.length; i++) {
                        if (_mk[i].extData == returnOverLayer.extData) {
                            _mk[i].setMap();
                            break;
                        }
                    }


                    if (newPath.length < 3) {
                        removeTempBound();
                        var options = {
                            label: "设置",
                            icons: {
                                primary: "ui-icon-wrench"
                            }
                        };
                        $("#setPen").button("option", options);
                        $("#show").button("enable");
                        $("#onekeyinout").button("enable");
                        $("#rule").button("enable");
                        showError("提醒：", "区域设置最少需要3点，请重新绘制");
                        return;
                    }
                    var polygon = new AMap.Polygon({
                        id: "curaddpoly",
                        path: newPath,
                        strokeColor: "#1791fc",
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        fillColor: "#1791fc",
                        fillOpacity: 0.35
                    });

                    //地图上添加多边形

                    polygon.extData = "curaddpoly";
                    polygon.setMap(projectMap.map);


                    overLayerObj = polygon;
                    projectMap.map.plugin(["AMap.PolyEditor"], function () {
                        polygonEditor = new AMap.PolyEditor(projectMap.map, overLayerObj);
                        polygonEditor.open();
                    });
                case "polyline": //折线
                case "circle": //圆
                default:

            }

        }

        //设置电子围栏
        $("#setPen").button({
            text: true,
            icons: {
                primary: "ui-icon-wrench"
            }
        }).click(function (btn) {
            var options;
            if ($.trim($(this).text()) == "设置") {
                options = {
                    label: "撤销",
                    icons: {
                        primary: "ui-icon-arrowreturnthick-1-w"
                    }
                };
                mousetool.polygon();
                $(this).button("option", options);
            }
            else {
                options = {
                    label: "设置",
                    icons: {
                        primary: "ui-icon-wrench"
                    }
                };
                if (overLayerObj == null) {

                    showError("错误", "区域尚未绘制完成，双击或右击结束绘制。");
                    return false;
                }
                removeTempBound();
                $(this).button("option", options);
            }
        });

        //保存电子围栏
        $("#savePen").button({
            text: true,
            icons: {
                primary: "ui-icon-disk"
            }
        }).click(function (btn) {
            //定义多边形的参数选项
            if (overLayerObj != null && overLayerObj.CLASS_NAME == "AMap.Polygon") {//多边形
                boundPoints = overLayerObj.getPath();
                if (boundPoints == null || boundPoints.length < 3) {
                    showError("错误！", "尚未设置限制区域"); return;
                }
                if (boundPoints.length >= 20) {
                    showError("错误", "限制区域的顶点数不能超过20个");
                    return;
                }
                $("#bounddiv").dialog({
                    title: "围栏设置（工地和搅拌站选一个填写）",
                    width: 520,
                    height: 330,
                    modal: true,
                    autoOpen: false,
                    close: function (event, ui) {
                        $('#bounddiv>form')[0].reset();

                        $(this).dialog("destroy");

                        $("#container").append($("#bounddiv"));


                    },
                    buttons: [{ text: '取消', click: function () { $(this).dialog("close"); } }, { text: '确定', click: SetBoundCmp }]
                });
                $("#bounddiv").dialog('open');
            } else {
                showMessage('提示', "尚未设置限制区域!"); return;
            }

        });
    }


    //围栏相关
    function clearAllBound() {
        $.each(overlayCache, function (index, overlay) {
            if (index.indexOf("bound") == 0) {
                if (overlay) {
                    overlay.hide();
                }
            }
        });
    }

    function removeTempBound() {
        if (polygonEditor != null) polygonEditor.close();
        var _mk = projectMap.map.getAllOverlays();
        for (var i = 0; i < _mk.length; i++) {
            if (_mk[i].extData == "curaddpoly") {
                _mk[i].setMap();
                break;
            }
        }
    }

    function AddBoundOverlay(id, data) {
        //如果存在对象，则刷新属性，设置显示
        var cacheKey = "bound" + id;
        var overlay = overlayCache[cacheKey];
        var dotstr = data["DotsStr"];
        if (isEmpty(dotstr) || dotstr == "") {
            if (overlay) {
                var _mk = projectMap.map.getAllOverlays();
                for (var i = 0; i < _mk.length; i++) {
                    if (_mk[i].extData == cacheKey) {
                        _mk[i].setMap();
                        break;
                    }
                }
            }
            showMessage("提示", data["Name"] + "没有区域数据");
            return;
        }
        if (overlay) {
            //需先增加，再更新
            overlay.show();
        }
        else {
            var dots = dotstr.split(';');
            var pots = new Array(); var x = 0;
            $.each(dots, function (index, dot) {
                if (dot != null && dot != "") {
                    var lonlat = dot.split(",");
                    if (lonlat.length == 2) {
                        pots.push(new AMap.LngLat(lonlat[0], lonlat[1]));
                    }
                }
            });
            if (pots.length >= 3) {
                //如果不存在，则创建新对象
                //可以建立两种刷子，表示两种报警
                var polygonOption = outpolygonOption;
                if (!data["IsOutAlarm"]) {
                    polygonOption = inpolygonOption;
                }

                polygonOption.id = cacheKey;
                polygonOption.path = pots;
                var newOverlay = new AMap.Polygon(polygonOption);

                newOverlay.extData = cacheKey;
                newOverlay.setMap(projectMap.map);


                overlayCache[cacheKey] = newOverlay;
            }
        }
    }

    function ShowAllBoundHandle(datas) {
        $.each(datas, function (index, data) {
            AddBoundOverlay(data["ID"], data);
        });
    }


    function AlarmSearchValues() {
        return { '': '', 1: '驶出报警', 0: '驶入报警' };
    }

    function ViewSearchValues() {
        return { '': '', 1: '显示', 0: '隐藏' };
    }
}


//地图右击
function initMapContextMenu() {

    var content = $("<div></div>");

    var ct_ul = $("<ul class='mc_menu'></ul>");

    $("<li onclick='markMapProject()'> > 标定已有工地</li>").appendTo(ct_ul);

    ct_ul.appendTo(content);

    var contextMenuOption = {
        content: content.html()
        , isCustom: true
    };

    mapCMenu = new AMap.ContextMenu(contextMenuOption);

    AMap.event.addListener(projectMap.map, "rightclick", function (e) {

        if (isRightClick) {
            isRightClick = false;
            return;
        }
        projectMap.eventLngLat = { lng: e.lnglat.lng, lat: e.lnglat.lat };
        mapCMenu.open(projectMap.map, e.lnglat);
    });
}

function markMapProject() {
    var ptLng = 0; var ptLat = 0;
    if (!isEmpty(projectMap.eventLngLat.lng) && !isEmpty(projectMap.eventLngLat.lat)) {
        ptLat = projectMap.eventLngLat.lat;
        ptLng = projectMap.eventLngLat.lng;
    }

    if (ptLat > 0 && ptLng > 0) {
        $("#markProjectDiv").dialog({
            title: "标定工地",
            width: 520,
            height: 330,
            modal: true,
            closeOnEscape: false,
            autoOpen: false,
            close: function (event, ui) {
                $('#markProjectDiv>form')[0].reset();
                $(this).dialog("destroy");
                $("#container").append($("#markProjectDiv"));
            },
            buttons: [{ text: '取消', click: CloseProjectMarkFn }, { text: '保存', click: MarkProject }]
        });
        $("#markProjectDiv").dialog('open');
        $("#markProjectDiv [name='Longitude']").val(ptLng);
        $("#markProjectDiv [name='Latitude']").val(ptLat);
        $("#markProjectDiv [name='PlaceRange']").val(1);

        $("#markProjectDiv input[name='ID']").unbind('change');
        $("#markProjectDiv input[name='ID']").bind('change', function () {
            var item = $("input[name='PName']").data($(this).val() + "_cache");
            if (!isEmpty(item)) {
                var label = item.label;

                var str = "";
                var regx = new RegExp("<span.*?>.*?<\/span>", "img");
                var sregx = new RegExp("<span.*?>", "g");
                var eregx = new RegExp("<\/span>", "g");
                var result = label.match(regx).join("=").replace(sregx, "").replace(eregx, "").split("=");


                $("#markProjectDiv [name='ProjectID']").val(item.value);
                $("#markProjectDiv [name='ProjectName']").val(item.text);
                $("#markProjectDiv [name='ProjectAddr']").val(result[1]);
                $("#markProjectDiv [name='LinkMan']").val(result[2]);
                $("#markProjectDiv [name='Tel']").val(result[3]);
                $("#markProjectDiv [name='Remark']").val(result[4]);
            }
            else {
                $("#markProjectDiv [name='ProjectID']").val("");
                $("#markProjectDiv [name='ProjectName']").val("");
                $("#markProjectDiv [name='ProjectAddr']").val("");
                $("#markProjectDiv [name='LinkMan']").val("");
                $("#markProjectDiv [name='Tel']").val("");
                $("#markProjectDiv [name='Remark']").val("");
            }
        });

    } else {
        showMessage("提示", "未获取到定位信息，请重试！");
    }

    mapCMenu.close();
}
