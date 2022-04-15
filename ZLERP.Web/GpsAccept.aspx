<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GpsAccept.aspx.cs" Inherits="ZLERP.Web.GpsAccept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GPS信息上传接收</title>
    <script src="Scripts/plugins/jQuery/jquery-2.2.3.min.js" type="text/javascript"></script>
    <link href="Scripts/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script type="text/javascript">
        var websocket;
        //测试环境
        var token1 = "g2gCZAAIaW5maW5pdHltAAAAlyL8pEPHAkJnhldMkSb2l3WvsMPP11qckV2V2D%2B%2BLZQAsEFjUOQnEqzpHm5A0H1oqZIXWfvr4ZX60mFfoqOoO%2FeSHRq4vk%2FynC1pIHRpJ1FloXz2FRqWEV%2FwsQuUC%2FS9n3c5PlFpxWceG1YJ4yCdK2OKvQSUVB6obq%2FGqzo5Ql8L1v7RvkEZfYwMQDZTwoqNyY9tlrl2WxQ%3D";
        var token2 = "g2gCZAAIaW5maW5pdHltAAAAkyL8pEPHAkJnhldMkSb2l3WvsMPP11qckV2V2D%2B%2BLZQAsEFjUOQnEqzpHm5A0H1oqZIXWfvr4Zn6uApe2q%2F7D9HCTUe170r3mCxod3M9JgVhpij8EkuXFV7wtAGXC%2FK7yCA%2FOl9rwGoaTlgI7iaTfm6L7FPFU0%2F9a%2F2a%2BGhgdnsP25%2Bk5EYfbowFQCxR2IWCypp32g%3D%3D";
        var uri = "ws://wechat.kfchain.com/ws_v1";
        var wsUri = uri + "?token=" + token1;

        //生产环境
        var token3 = "g2gCZAAIaW5maW5pdHltAAAAlyL8pEPHAkJnhldMkSb2l3WvsMPP11qckV2V2D%2B%2BLZQAsEFjUOQnEqzpHm5A0H1oqZIXWfvr4ZX60mFfq6OoO%2FeTShTk7EDyzSo5dXFpIFZm9yiiQBnERwLwsVHADv25mCNqbA09lGtLS1RS6nHHKm%2FYswWVBBr4aKuQ%2F2pvQlsH1v7RvkEZfYwMQDZTwoqNyY9tlrl2WxQ%3D";
        var wsUri1 = "ws://api.beidouapp.com/ws_v1?token=" + token3;

        var js = 0;
        var visitcount = 0;
        var successcount = 0;
        var failcount = 0;
        var stime;
        var etime;

        $(function () {
            initWebSocket();
            window.onload = function () {
                setTimeout(function () {
                    window.scrollTo(0, 1)
                }, 0);
            };
            function initWebSocket() {
                stime = moment(new Date()).format("YYYY-MM-DD HH:mm:ss"); //获取系统当前时间
                websocket = new WebSocket(wsUri1);
                websocket.onopen = function (evt) {
                    onOpen(evt);
                };
                websocket.onclose = function (evt) {
                    onClose(evt);
                };
                websocket.onmessage = function (evt) {
                    onMessage(evt);
                };
                websocket.onerror = function (evt) {
                    onError(evt);
                };
            }
            function onOpen(evt) {
                console.log("已打开连接！");
                $("#staus").html("<span style='color:green;'>已打开连接</span>");
                $("#startstop").val("暂停上传");
                $("#startstop").attr("class", "btn btn-danger");
            }

            function onClose(evt) {
                console.log("已失去连接！");
                $("#staus").html("<span style='color:red;'>已失去连接</span>");
                $("#startstop").val("启动上传");
                $("#startstop").attr("class", "btn btn-success");
                //            setTimeout(function () {
                //                initWebSocket();
                //            }, 3000);
            }
            function onError(evt) {

            }
            function doSend(message) {
                websocket.send(message);
            }
            //收取websocket消息
            function onMessage(evt) {
                //console.log(evt.type);
                //console.log(evt.data);
                var dataJSON = JSON.parse(evt.data);
                if (dataJSON.code == 0) {
                    //将消息添加到页面 
                    if (dataJSON.msg.msg_type != "jtt808_gps") {
                        return;
                    }
                    visitcount++;
                    $("#visitCount").html(visitcount);
                    var gpsinfo = "[" + JSON.stringify(dataJSON.msg) + "]";
                    $.ajax({
                        //要用post方式     
                        type: "Post",
                        //方法所在页面和方法名     
                        url: "GpsAccept.aspx/insertGPSInfo",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: "{gpsinfo:'" + gpsinfo + "'}",
                        success: function (data) {
                            //返回的数据用data.d获取内容     
                            if (data.d == true) {
                                createhtml(dataJSON, "<span style='color:green;'>成功</span>");

                                //成功接收数量
                                successcount++;
                                $("#successCount").html(successcount);

                                //上传频率
                                etime = moment(new Date()).format("YYYY-MM-DD HH:mm:ss");
                                var d1 = new Date(stime);
                                var d2 = new Date(etime);
                                var cz = parseInt(d2 - d1) / 1000;
                                $("#accepttime").html((successcount * 1 / cz * 1).toFixed(0) + '秒');

                                //超过10000条清空日志
                                js++;
                                if (js * 1 > 10000) {
                                    clearlog();
                                    js = 0;
                                }
                            }
                            else {
                                createhtml(dataJSON, "<span style='color:red;'>失败</span>");
                                //失败数量
                                failcount++;
                                $("#failCount").html(failcount);
                                console.log(gpsinfo);
                            }
                        },
                        error: function (err) {

                            //alert(err);
                            console.log(err);
                        }
                    });
                }
            }
            //创建表格HTML
            function createhtml(dataJSON, state) {
                var htmltext = "";
                //htmltext = htmltext + "<thead><tr><th>设备ID</th><th>原始经度</th><th>原始纬度</th><th>速度</th><th>方向(角度)</th><th>油量</th><th>总里程</th><th>卸料（正反转状态）</th><th>接收时间</th><th>ACC标志</th><th>消息类型</th></tr></thead><tbody>";                          
                htmltext = htmltext + "<tr>";
                htmltext = htmltext + "<td>" + dataJSON.msg.terminalid + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.originlongtidue + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.originlatitude + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.speed + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.direction + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.oil + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.distance + "</td>";
                var unload = '未知';
                switch (dataJSON.msg.unload) {
                    case 0:
                        unload = '停止';
                        break;
                    case 1:
                        unload = '正转';
                        break;
                    case 2:
                        unload = '反转';
                        break;
                    default:
                        break;
                }
                htmltext = htmltext + "<td>" + unload + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.receivetime + "</td>";
                var accflag = '未知';
                switch (dataJSON.msg.accflag) {
                    case 0:
                        accflag = '熄火';
                        break;
                    case 1:
                        accflag = '点火';
                        break;
                    default:
                        break;
                }
                htmltext = htmltext + "<td>" + accflag + "</td>";
                htmltext = htmltext + "<td>" + dataJSON.msg.msg_type + "</td>";
                htmltext = htmltext + "<td>" + state + "</td>";
                htmltext = htmltext + "</tr>";
                //htmltext = htmltext + "</tbody>";
                $("#acceptlist").append(htmltext);
            }

            $("#startstop").click(function () {
                var ss = $("#startstop").val();
                if (ss == "启动上传") {
                    initWebSocket();
                }
                else {
                    websocket.close();
                }
            });

            $("#clearlog").click(function () {
                clearlog();
            });
            function clearlog() {
                $("#acceptlist").html('');
                var htmltext = "<thead><tr><th>设备ID</th><th>原始经度</th><th>原始纬度</th><th>速度</th><th>方向(角度)</th><th>油量</th><th>总里程</th><th>卸料（正反转状态）</th><th>接收时间</th><th>ACC标志</th><th>消息类型</th><th>上传状态</th></tr></thead><tbody>";
                $("#acceptlist").html(htmltext);
            }

        });

        window.onbeforeunload = function () {
            //  这是用来设定一个时间, 时间到了, 就会执行一个指定的 method。
            setTimeout(onunloadcancel, 10);
            return "真的离开?";
        }
        window.onunloadcancel = function () {
            //alert("取消离开");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <h2>
            GPS实时上传接收-监控日志</h2>
        <p>
            监控状态：<span style="color: Red;" id="staus"></span> <span style="color: black;">总接收量:</span>
            <span style="color: black; blod" id="visitCount"><strong>0</strong></span> <span
                style="color: green;">接收成功:</span> <span style="color: green; blod" id="successCount">
                    <strong>0</strong></span> <span style="color: Red;">接收失败:</span> <span style="color: Red;
                        blod" id="failCount"><strong>0</strong></span> <span style="color: blue;">上传频率:</span>
            <span style="color: blue; blod" id="accepttime"><strong>0</strong></span>
            <input type="button" class="btn btn-success" data-toggle="button" id="startstop"
                value="启动上传" />
            <input type="button" class="btn btn-success" data-toggle="button" id="clearlog" value="清除日志" />
        </p>
        <table class="table" id="acceptlist">
            <thead>
                <tr>
                    <th>
                        设备ID
                    </th>
                    <th>
                        原始经度
                    </th>
                    <th>
                        原始纬度
                    </th>
                    <th>
                        速度
                    </th>
                    <th>
                        方向(角度)
                    </th>
                    <th>
                        油量
                    </th>
                    <th>
                        总里程
                    </th>
                    <th>
                        卸料（正反转状态）
                    </th>
                    <th>
                        接收时间
                    </th>
                    <th>
                        ACC标志
                    </th>
                    <th>
                        消息类型
                    </th>
                    <th>
                        上传状态
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
