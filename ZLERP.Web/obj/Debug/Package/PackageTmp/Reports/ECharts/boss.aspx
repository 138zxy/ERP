<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script src="../../Scripts/echarts.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div id="main" style="height: 400px;">
    </div>
    <div id="Div1" style="height: 600px;">
        <div id="ProjectDiv" style="float: left;height: 600px; width: 50%;">
        </div>
        <div id="ProjectBJDiv" style="float: left;height: 300px; width: 50%;">
        </div>
        <div id="ProjectDetailDiv" style="float: left;height: 300px; width: 50%;">
        </div>
    </div>
    <script type="text/javascript">
        // 主表
        var myChart = echarts.init(document.getElementById('main'));
        // 主表初始化
        function initChart(data) {
            var option = {
                title: {
                    text: '方量统计总表',
                    subtext: '点击柱体查看明细'
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['方量']
                },
                dataZoom: {
                    show: true,
                    realtime: true,
                    start: 0,
                    end: 100
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                xAxis: [{
                    type: 'category',
                    data: data.ym
                }],
                yAxis: [{
                    type: 'value'
                }],
                series: [
                {
                    name: '方量',
                    type: 'bar',
                    data: data.huncount,
					itemStyle: {
					    normal: {
					            color: "#749f83",
								label: {
									show: true, //开启显示
									position: 'top', //在上方显示
									textStyle: { //数值样式
										color: 'black',
										fontSize: 16
									}
								}
							}
				    }
                }
                ]
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        }
        //刷新主表
        function updateChart() {
            $.ajax({
                type: 'post',
                url: '/ShippingDocument.mvc/GetYM',
                dataType: 'json',
                data: {},
                success: function (result) {
                    initChart(result);
                }
            });
        }

        // 工程表
        var projectChart = echarts.init(document.getElementById('ProjectDiv'));
        // 工程表初始化
        function initProjectChart(data,ym) {
            var option = {
                title: {
                    text: '客户统计月表' + ym,
                    subtext: '点击柱体查看明细'
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['方量']
                },
//                dataZoom: {
//                    show: true,
//                    realtime: true,
//                    start: 0,
//                    end: 100
//                },
                yAxis: [{
                    type: 'category',
                    data: data.project,
                    axisLabel: {
                        interval: 0,
                        formatter: function (value) {
                            debugger
                            var ret = ""; //拼接加\n返回的类目项  
                            var maxLength = 10; //每项显示文字个数  
                            var valLength = value.length; //X轴类目项的文字个数  
                            var rowN = Math.ceil(valLength / maxLength); //类目项需要换行的行数  
                            if (rowN > 1)//如果类目项的文字大于3,  
                            {
                                for (var i = 0; i < rowN; i++) {
                                    var temp = ""; //每次截取的字符串  
                                    var start = i * maxLength; //开始截取的位置  
                                    var end = start + maxLength; //结束截取的位置  
                                    //这里也可以加一个是否是最后一行的判断，但是不加也没有影响，那就不加吧  
                                    temp = value.substring(start, end) + "\n";
                                    ret += temp; //凭借最终的字符串  
                                }
                                return ret;
                            }
                            else {
                                return value;
                            }
                        }
                    }
                }],
                xAxis: [{
                    type: 'value'
                }],
                series: [{
                    name: '方量',
                    type: 'bar',
                    data: data.huncount,
                    itemStyle: {
                        normal: {
                            color: "#749f83",
                            label: {
                                show: true, //开启显示
                                position: 'right', //在上方显示
                                textStyle: { //数值样式
                                    color: 'black',
                                    fontSize: 16
                                }
                            }
                        }
                    }
                }]
            };

            // 使用刚指定的配置项和数据显示图表。
            projectChart.setOption(option);
        }
        //刷新工程表
        function updateProjectChart(ym) {
            $.ajax({
                type: 'post',
                url: '/ShippingDocument.mvc/GetProjectByYM',
                dataType: 'json',
                data: {ym:ym},
                success: function (result) {
                    initProjectChart(result,ym);
                }
            });
        }

        // 工程明细表
        var projectDetailChart = echarts.init(document.getElementById('ProjectDetailDiv'));
        // 工程明细表初始化
        function initProjectDetailChart(data, project) {
            var option = {
                title: {
                    text: project+'明细表'
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['方量']
                },
                dataZoom: {
                    show: true,
                    realtime: true,
                    start: 0,
                    end: 100
                },
                xAxis: [{
                    type: 'category',
                    data: data.time
                }],
                yAxis: [{
                    type: 'value'
                }],
                series: [
                {
                    name: '方量',
                    type: 'line',
                    data: data.huncount,
                    itemStyle: {
                        normal: {
                            label: {
                                show: true, //开启显示
                                position: 'top', //在上方显示
                                textStyle: { //数值样式
                                    color: 'black',
                                    fontSize: 16
                                }
                            }
                        }
                    }
                }
                ]
            };
            // 使用刚指定的配置项和数据显示图表。
            projectDetailChart.setOption(option);
        }
        //刷新主表
        function updateProjectDetailChart(project) {
            $.ajax({
                type: 'post',
                url: '/ShippingDocument.mvc/GetDetailByProject',
                dataType: 'json',
                data: { project: project },
                success: function (result) {
                    initProjectDetailChart(result, project);
                }
            });
        }

        // 工程比较表
        var projectBJChart = echarts.init(document.getElementById('ProjectBJDiv'));
        // 工程比较表初始化
        function initProjectBJChart(data, project) {
            var option = {
                title: {
                    text: project + '同比表'
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['去年方量','今年方量']
                },
                xAxis: [{
                    type: 'category',
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                }],
                yAxis: [{
                    type: 'value'
                }],
                series: [
                {
                    name: '去年方量',
                    type: 'bar',
                    data: data.lastyear,
                    itemStyle: {
                        normal: {
                            label: {
                                show: true, //开启显示
                                position: 'top', //在上方显示
                                textStyle: { //数值样式
                                    color: 'black',
                                    fontSize: 16
                                }
                            }
                        }
                    }
                },
                {
                    name: '今年方量',
                    type: 'bar',
                    data: data.nowyear,
                    itemStyle: {
                        normal: {
                            label: {
                                show: true, //开启显示
                                position: 'top', //在上方显示
                                textStyle: { //数值样式
                                    color: 'black',
                                    fontSize: 16
                                }
                            }
                        }
                    }
                }
                ]
            };
            // 使用刚指定的配置项和数据显示图表。
            projectBJChart.setOption(option);
        }
        //刷新主表
        function updateProjectBJChart(project) {
            $.ajax({
                type: 'post',
                url: '/ShippingDocument.mvc/GetProjectBJ',
                dataType: 'json',
                data: { project: project },
                success: function (result) {
                    initProjectBJChart(result, project);
                }
            });
        }

        updateChart();

        //点击也可改为阴影区域，参考https://www.cnblogs.com/zhenghengbin/p/7258378.html
        myChart.on('click', function (params) {
            updateProjectChart(params.name);
        });
        projectChart.on('click', function (params) {
            updateProjectDetailChart(params.name);
            updateProjectBJChart(params.name);
        });
    </script>
</asp:Content>
