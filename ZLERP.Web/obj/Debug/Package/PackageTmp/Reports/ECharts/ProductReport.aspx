﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script src="../../Scripts/echarts.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div id="main" style="height: 365px;width: 100%;">
        <div id="ProductDay" style="float: left;height: 365px; width: 50%;">
        </div>
        <div id="ProductYear" style="float: left;height: 365px; width: 50%;">
        </div>
    </div>
    <hr>
    <div id="Div1" style="height: 365px;width: 100%;">
        <div id="ProductConstr" style="float: left;height: 365px; width: 50%;">
        </div>
        <div id="ProductLine" style="float: left;height: 365px; width: 50%;">
        </div>
    </div>
    <script type="text/javascript">
        // 主表
        var myChart = echarts.init(document.getElementById('ProductDay'));
        // 主表初始化
        function initChart(data) {
            var alldata = [];
            alldata.push({ "value": data.Cube[0] - data.Cube[1], "name": '未生产方量' });
            alldata.push({"value":data.Cube[1],"name":'已生产方量'});
            var option = {
                    title: {
                        text: '搅拌站当日生产方量统计图表',
                        subtext: '',
                        left: 'center'
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: '{a} <br/>{b} : {c} ({d}%)'
                    },
                    legend: {
                        orient: 'vertical',
                        left: 'left',
                        data: ['已生产方量', '未生产方量']
                    },
                    series: [
                        {
                            name: '访问来源',
                            type: 'pie',
                            radius: '55%',
                            center: ['50%', '60%'],
                            label: { normal: { show: true, formatter: "{b} : {c} ({d}%)" } },
                            data: alldata,
                            emphasis: {
                                itemStyle: {
                                    shadowBlur: 10,
                                    shadowOffsetX: 0,
                                    shadowColor: 'rgba(0, 0, 0, 0.5)'
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
                url: '/Report.mvc/GetPlanCube',
                dataType: 'json',
                data: {},
                success: function (result) {
                    initChart(result);
                }
            });
        }
        updateChart();

        // 主表
        var myChart2 = echarts.init(document.getElementById('ProductYear'));
        // 主表初始化
        function initChart2(data) {
            var alldata = [];
            alldata.push({ "value": data.Cube[0], "name": '本月生产方量' });
            alldata.push({ "value": data.Cube[1], "name": '其他月份生产方量' });
            var option = {
                title: {
                    text: '搅拌站年度生产方量统计图表',
                    subtext: '',
                    left: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{a} <br/>{b} : {c} ({d}%)'
                },
                legend: {
                    orient: 'vertical',
                    left: 'left',
                    data: ['本月生产方量', '其他月份生产方量']
                },
                series: [
                    {
                        name: '访问来源',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '60%'],
                        label: { normal: { show: true, formatter: "{b} : {c} ({d}%)" } },
                        data: alldata,
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            };

            // 使用刚指定的配置项和数据显示图表。
            myChart2.setOption(option);
        }
        //刷新主表
        function updateChart2() {
            $.ajax({
                type: 'post',
                url: '/Report.mvc/GetTotalProduceCube',
                dataType: 'json',
                data: {},
                success: function (result) {
                    initChart2(result);
                }
            });
        }
        updateChart2();

        // 主表
        var myChart3 = echarts.init(document.getElementById('ProductConstr'));
        // 主表初始化
        function initChart3(data) {
            var option = {
                title: {
                    text: '近四个月各强度生产排行',
                    subtext: '数据来自',
                    left: 'center'
                },
                xAxis: {
                    type: 'category',
                    data: data.ConStrength
                },
                yAxis: {
                    type: 'value',
                    axisLabel: {
                        formatter: '{value} 方'
                    },
                    axisPointer: {
                        snap: true
                    }
                },
                series: [{
                    data: data.Cube,
                    type: 'line'
                }]
            };


            // 使用刚指定的配置项和数据显示图表。
            myChart3.setOption(option);
        }
        //刷新主表
        function updateChart3() {
            $.ajax({
                type: 'post',
                url: '/Report.mvc/GetTop5ConStrength',
                dataType: 'json',
                data: {},
                success: function (result) {
                    initChart3(result);
                }
            });
        }


        updateChart3();

        // 主表
        var myChart4 = echarts.init(document.getElementById('ProductLine'));
        // 主表初始化
        function initChart4(data) {
            var option = {
                title: {
                    text: '各生产线近半年生产量',
                    subtext: ''
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: data.listLine
                },
                toolbox: {
                    show: true,
                    feature: {
                        dataZoom: {
                            yAxisIndex: 'none'
                        },
                        dataView: { readOnly: false },
                        magicType: { type: ['line', 'bar'] },
                        restore: {},
                        saveAsImage: {}
                    }
                },
                xAxis: {
                    type: 'category',
                    boundaryGap: false,
                    data: data.listMonth
                },
                yAxis: {
                    type: 'value',
                    axisLabel: {
                        formatter: '{value} 方'
                    }
                },
                series: functionNodname(data)
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart4.setOption(option);
        }
        //刷新主表
        function updateChart4() {
            $.ajax({
                type: 'post',
                url: '/Report.mvc/GetCubeBYLine',
                dataType: 'json',
                data: {},
                success: function (result) {
                    initChart4(result);
                }
            });
        }
        updateChart4();
        function functionNodname(data) {
            var series = [];
            var count = Object.keys(data.list).length;
            for (var i = 0; i < count; i++) {
                var item = {
                    name: data.list[i].ProductLineName,
                    type: 'line',
                    data: data.list[i].Cube,
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大值' },
                            { type: 'min', name: '最小值' }
                        ]
                    },
                    markLine: {
                        data: [
                            { type: 'average', name: '平均值' }
                        ]
                    }
                }
                series.push(item)
            }
            return series;
        }
        //点击也可改为阴影区域，参考https://www.cnblogs.com/zhenghengbin/p/7258378.html
        myChart.on('click', function (params) {
            //updateProjectChart(params.name);
        });
    </script>
</asp:Content>
