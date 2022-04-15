//**** 轨迹回放 begin gjhf
var trackMark = null; 
//var line_brush = null; var eclipseBrush = null;var moveBrush = null;
var thePoints = {};

var spdArr = new Array(); var dotMarks = new Array();
var aryPts = new Array(); var mkidArr = new Array();
var linePts = new Array();
var isOk = false; var isRun = false; var isPause = false;var isLoad = false;
var timer1 = null; var counter = 0; var trackLine = undefined;
var trackIconIcon = null; var trackDotIcon = null;
var lineBrush = {};
function initTrack() {

   trackMark = new AMap.Marker({
        id: "track_mark_s0",
        position: new AMap.LngLat(106.42500579, 29.39185524), //换成重庆的
        icon: iconArr.trackIcon,
        offset: new AMap.Pixel(-16, -32)
    });
   
}
function GetPoints() {
    if (isLoad) return;
    if (isRun || isPause) {//播放状态和暂停状态下，不能加载数据
        showMessage( "提示", "播放状态和暂停状态下，不能加载数据");
    } else {
        ClearTrackLine();
        var selectedId = $("#carList").val();
        if (selectedId) {
            if (CheckDate("startTime", "endTime")) {
                if (GetDateDiff($("#startTime").val(), $("#endTime").val(), "day") <= 3000000) {
                    isOk = false; isLoad = true; $("#statusMsg").html("未加载");
                    DoAjax('/ABC.mvc/GetPoints', { carId: selectedId, startTime: $("#startTime").val(), endTime: $("#endTime").val(), tolerance: $("#tolerance").val() }, handlePoints);
                }
                else {
                    showError("错误", "查询时间段不能超过3天");
                }
            }
        } else {
            showError("错误", "请先选中要进行轨迹回放的车辆！"); 
        }
    }
}
function handlePoints(data) {
    isLoad = false;
    aryPts = new Array();
    if (data.Result === false) {
        showMessage("提示", data.Message);
    } else {
        thePoints = data;

        if (thePoints.length > 0) {
            $.each(thePoints, function (index, point) {
                linePts[index] = new AMap.LngLat(point.X, point.Y);

                //aryPts[index] = { lng: point.X, lat: point.Y };zjy 不能用数组
                aryPts[index] = new AMap.LngLat(point.X, point.Y);


                dotMarks[index] = new AMap.Marker({
                    id: "track_mark_" + index,
                    position: new AMap.LngLat(point.X, point.Y),
                    icon: iconArr.circleDot,
                    offset: new AMap.Pixel(-12, -11)
                });

                dotMarks[index].extData = "track_mark_" + index; //zjy


                mkidArr.push("track_mark_" + index);
                spdArr[index] = 0; var curSpd = parseFloat(point["Spd"]);
                if (curSpd != NaN) {
                    if (curSpd >= 70) {
                        spdArr[index] = 2;
                    } else if (curSpd >= 10 && curSpd < 70) {
                        spdArr[index] = 1;
                    }
                }

                //_abcMap.map.bind(dotMarks[index], "click", function (e) { zjy
                AMap.event.addListener(dotMarks[index], "click", function (e) {

                    _abcMap._openInfoWindow(dotMarks[index], eclpsInfoWindow(point));
                });
            });
            if (aryPts.length > 0) {
                //将开始 暂停 结束 置为可用
                $("#trace").slider("option", { max: aryPts.length, step: 1, value: 1 });
                //$("#playBtn").removeAttr("disabled");
                //$("#pauseBtn").attr("disabled", "false");
                $("#playBtn").button("enable");
                $(".jq_btn_track").button("enable");
                $("#statusMsg").html("加载成功");
                
                isOk = true;
                showMessage("提示", "加载数据成功，现在可以进行轨迹播放");
            }

        } else {
            $("#statusMsg").html("无数据");
            showError("错误", "未查询到数据，请调整时间或者精度阀值后重新加载数据");
        }
    }
}


function startTrack() {
    if (isOk) {
        if (!isRun) {
            if (timer1) window.clearInterval(timer1);
            if (!isPause) {//如果不是暂停状态，则需要清空地图
                counter = 0;
                ClearTrackLine();
            }
            
            isPause = false;
            $("#playBtn").button("disable");
            $("#pauseBtn").button("enable");
            $("#stopBtn").button("enable");


            //            _abcMap.map.addOverlays(trackMark); //加载覆盖物 zjy
            trackMark.extData = "track_mark_s0";
            trackMark.setMap(_abcMap.map);

           
            $("#trace").slider("option", { value: counter });
            
            timer1 = window.setInterval(drawLine, $("#thetimer").val());
            isRun = true;
        } else {
            showMessage("提示", "在播放状态下无法进行再次播放");
        }
    } else {
        showMessage( "提示", "数据未加载成功状态下无法进行播放");
    }
}
function pauseTrack() {
    if (isRun) {
        isPause = true; isRun = false;

        $("#pauseBtn").button("disable");
        $("#playBtn").button("enable");
        $("#stopBtn").button("enable");
        if (timer1) window.clearInterval(timer1);
    } else {
        showMessage( "提示", "在非播放状态下无法进行暂停");
    }
}
function stopTrack() {
    if (isRun || isPause) {
        if (timer1) window.clearInterval(timer1);
        isRun = false; isPause = false;
        counter = 0;
        ClearTrackLine();
        _abcMap.map.panTo(aryPts[0]);
        $("#stopBtn").button("disable");
        $("#playBtn").button("enable");
        $("#pauseBtn").button("disable");
    } else {
        showMessage("提示", "在非播放状态下进行停止无效");
    }
}
function ClearTrackLine() {
    //1.remove lines
    $.each(mkidArr, function (i, id) {
        //        _abcMap.removeMark(id); zjy
        var mk;
        var _mk = _abcMap.map.getAllOverlays();
        for (var _i = 0; _i < _mk.length; _i++) {
            if (_mk[_i].extData == id) {
                mk = _mk[_i];
                mk.setMap();
                break;
            }
        }
    });
    //2.remove trackMark
//    _abcMap.removeMark("track_mark_s0"); zjy
    var mk;
    var _mk = _abcMap.map.getAllOverlays();
    for (var _i = 0; _i < _mk.length; _i++) {
        if (_mk[_i].extData == 'track_mark_s0') {
            mk = _mk[_i];
            mk.setMap();
            break;
        }
    }
}

function SetRoute() {
    var selectedId = $("#carList").val();
    if (selectedId) {
        if (CheckDate("startTime", "endTime")) {
            if (GetDateDiff($("#startTime").val(), $("#endTime").val(), "day") <= 3) {
                $("#setroutediv").dialog({
                    title: "工地间路线",
                    width: 520,
                    height: 330,
                    modal: true,
                    autoOpen: false,
                    close: function (event, ui) {
                        $('#setroutediv>form')[0].reset();

                        $(this).dialog("destroy");

                        $("#container").append($("#setroutediv"));


                    },
                    buttons: [{ text: '取消', click: function () { $(this).dialog("close"); } }, { text: '确定', click: SetRouteFunc}]
                });
                $("#setroutediv").dialog('open');
            }
            else {
                showError("错误", "查询时间段不能超过3天");
            }
        }
    } else {
        showError("错误", "请先选中车辆！");
    }
}

//phoenix
function SetRouteFunc() {
    var item_s = $("input[name='ProjectList_s']").data("autocomplete").selectedItem;
    var item_t = $("input[name='ProjectList_t']").data("autocomplete").selectedItem;
    var carid = $("#carList").val();
    var startTime = $("#startTime").val();
    var endTime = $("#endTime").val();
    var vs = "";
    var vt = "";
    if (item_s != null) {
        vs = item_s.value;
    }
    if (item_t != null) {
        vt = item_t.value;
    }
    var oJson = { sProjectID: vs, tProjectID: vt, carid: carid, startTime: startTime, endTime: endTime };

    ajaxRequest('/ABC.mvc/AddTZRoute', oJson, false, function (response) {
        if (response.Result) {
            showMessage('提示', response.Message);
        }
        else {
            showError("错误", response.Message);
        }
        $("#setroutediv").dialog('close');
    });
}
function addLine(i) {
    var color = "#32CD32";
    if (spdArr[i] == 1) {
        color = "#8B4513";
    } else if (spdArr[i] == 2) {
    color = "#DC143C";
    }
    _abcMap.addLine("polyline_" + i, color, [aryPts[i], aryPts[i + 1]]);
    mkidArr.push("polyline_" + i);
}

function drawLine() {
    if (dotMarks.length > 1) {
        addLine(counter);

        var lnglat = dotMarks[counter].getPosition();
        var bounds = _abcMap.map.getBounds();
        if (lnglat.lng <= bounds.southwest.lng || lnglat.lng >= bounds.northeast.lng || lnglat.lat <= bounds.southwest.lat || lnglat.lat >= bounds.northeast.lat) {
            _abcMap.map.panTo(lnglat);

        }
        trackMark.setPosition(aryPts[counter + 1]);



        //        _abcMap.map.addOverlays(dotMarks[counter]); //加载覆盖物 zjy
        dotMarks[counter].setMap(_abcMap.map);



        $("#trace").slider("value", $("#trace").slider("value") + 1);

        counter++;
    }
    else {
        $("#trace").slider("value", $("#trace").slider("value") + 1);
    }
    if (counter >= aryPts.length - 1) {
        if (timer1) window.clearInterval(timer1);


        //        _abcMap.map.addOverlays(dotMarks[counter]);zjy
        dotMarks[counter].setMap(_abcMap.map);


        isRun = false;
        $("#stopBtn").button("disable");
        $("#playBtn").button("enable");
        $("#pauseBtn").button("disable");
        alert("完成");
    }
    
   

}

function eclpsInfoWindow(data) {
    var content = "时间：<bluefont>" + data["Time"]
                    + "</bluefont><br />速度：<bluefont>" + data["Spd"]
                    + "km/h</bluefont><nbsp />Acc状态：<bluefont>" + data["Acc"]
                    + "</bluefont><nbsp />油位：<bluefont>" + data["Oil"]
                    + "L</bluefont>";
     
    return content;
}


function OpenHistoryVideo() {
    if (gSysConfig.ShipDocHistoryVideoUrl == undefined || gSysConfig.ShipDocHistoryVideoUrl == "") {
        showError("未配置历史视频外部地址，请在【系统全局配置】->【GPS配置】中设置 运输单历史视频URL");
        return;
    }
    console.log("VideoDeviceGuid =" + $("#VideoDeviceGuid").val());
    var historyVideoUrl = gSysConfig.ShipDocHistoryVideoUrl + "?deviceGuid=" + $("#VideoDeviceGuid").val() +
                                                              "&beginTime=" + $("#startTime").val() +
                                                              "&endTime=" + $("#endTime").val() ;

    window.open(historyVideoUrl, "_Blank");
}
