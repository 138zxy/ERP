function carIndexInit(storeUrl, dirverStoreUrl, carClassSelectUrl,uploadUrl, date) {
    var mixerCode = "CT1";
    var pumpCode = "CT2";
    function carTypeChanged() {
        if ($(this).val() == pumpCode) {
            $("select[name='Car.PumpTypeID']").addClass("requiredval");
            $("select[name='Car.PumpTypeID']").css("display", "");
        } else {
            // $('select[name='Car.PumpTypeID']').find('option[value]').hide();
            $("select[name='Car.PumpTypeID']").css("display", "none");
            $("select[name='Car.PumpTypeID']").val("");

        }
    }
    var beCode = "租用";
    function belongToChanged() {
        if ($(this).val() == beCode) {
            $("select[name='Car.RentCarName']").addClass("requiredval");
            $("select[name='Car.RentCarName']").css("display", "");
        } else {
            $("select[name='Car.RentCarName']").css("display", "none");
            $("select[name='Car.RentCarName']").val("");
        }
    }
    //car form提交
    function beforeCarFormSubmit(formId) {
        var carType = carGrid.getFormFieldValue("Car.CarTypeID");
        var pumpType = carGrid.getFormFieldValue("Car.PumpTypeID");

        if (carType == pumpCode && pumpType == "") {
            $("select[name='Car.PumpTypeID']").addClass("input-validation-error");
            showError("验证错误", "泵车必须指定泵车类型");
            return false;
        }

        var maxCube = carGrid.getFormFieldValue("Car.MaxCube");
        if (carType == mixerCode && maxCube == 0) {
            carGrid.getFormField("MaxCube").addClass("input-validation-error");
            showError("验证错误", "搅拌车的砼容量必须大于0");
            return false;
        }

        var carBelongTo = carGrid.getFormFieldValue("Car.BelongTo");
        var rentCarName = carGrid.getFormFieldValue("Car.RentCarName");

        if (carBelongTo == beCode && rentCarName == "") {
            $("select[name='Car.RentCarName']").addClass("input-validation-error");
            showError("验证错误", "外车必须指定外租车队名");
            return false;
        }

        $("select[name='Car.PumpTypeID']").removeClass("input-validation-error");
        $("select[name='Car.RentCarName']").removeClass("input-validation-error");
        return true;
    }


    function CarTypeFmt(cellvalue, options, rowObject) {
        var style = "color:green;";
        var txt = '混凝土';
        if (cellvalue == 1) {
            style = "color:red;";
            var txt = '湿拌';
        }

        if (cellvalue == 2) {
            style = "color:blue;";
            var txt = '干混';
        }

        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }
    function CarTypeUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }



    var filterCondition = "IsUsed= 1";
    var carGrid = new MyGrid({
        renderTo: "MyJqGrid",
        title: "车辆信息",
        autoWidth: true,
        buttons: buttons0,
        height: gGridWithTitleHeight - 100,
        storeURL: storeUrl,
        storeCondition: filterCondition,
        sortByField: "ID",
        sortOrder: "ASC",
        primaryKey: "ID"
        //, groupField: "UserType"
		,
        setGridPageSize: 30,
        dialogHeight: 519,
        showPageBar: true,
        groupField: "CarTypeID",
        groupingView: {
            groupText: ["{0}({1})"],
            groupDataSorted: true
        },
        singleSelect: false,
        rowNumbers: true,
        initArray: [{
            label: '车辆代号',
            name: 'ID',
            index: 'ID',
            width: 80
        },
		{
		    label: '车牌号码',
		    name: 'CarNo',
		    index: 'CarNo',
		    width: 80
		},
        {
            label: '车辆作业',
            name: 'DataType',
            index: 'DataType',
            sortable: false,
            width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt
            , formatoptions: {
                parentId: 'CarUse'
            }
            ,stype: 'select',
		    searchoptions: {
		        value: dicToolbarSearchValues('CarUse')
		    }
        },
        {
            label: '实时视频',
            name: 'RealTimeVideo',
            index: 'RealTimeVideo', 
            width: 90, 
            sortable: false, 
            align: "center",
            search: false, hidden: true 
        },
		{
		    label: '车辆类型',
		    name: 'CarTypeID',
		    index: 'CarTypeID',
		    sortable: false,
		    width: 80,
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: 'CarType'
		    },
		    stype: 'select',
		    searchoptions: {
		        value: dicToolbarSearchValues('CarType')
		    }
		},
		{
		    label: '砼容量',
		    name: 'MaxCube',
		    index: 'MaxCube',
		    align: 'right',
		    width: 60
		},
		{
		    label: '车辆状态',
		    name: 'CarStatus',
		    index: 'CarStatus',
		    width: 80,
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: 'CarStatus'
		    },
		    stype: 'select',
		    searchoptions: {
		        value: dicToolbarSearchValues('CarStatus')
		    }
		},
		{
		    label: '泵车类型',
		    name: 'PumpTypeID',
		    index: 'PumpTypeID',
		    width: 80,
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: 'PumpType'
		    },
		    stype: 'select',
		    searchoptions: {
		        value: dicToolbarSearchValues('PumpType')
		    }
		},
		{
		    name: 'CompanyID',
		    index: 'CompanyID',
		    hidden: true
		},
		{
		    label: '所属公司',
		    name: 'CompanyName',
		    index: 'Company.CompName',
		    width: 160
		},
		{
		    label: '车别',
		    name: 'BelongTo',
		    index: 'BelongTo',
		    width: 80,
		    formatter: dicCodeFmt,
		    unformat: dicCodeUnFmt,
		    formatoptions: {
		        parentId: 'CarBelong'
		    },
		    stype: 'select',
		    searchoptions: {
		        value: dicToolbarSearchValues('CarBelong')
		    }
		},
		{
		    label: '车种',
		    name: 'CarClassID',
		    index: 'CarClassID',
		    width: 80,
		    hidden: true
		},
		{
		    label: '车种',
		    name: 'CarClassName',
		    index: 'CarClass.CarClassName',
		    width: 80,
		    stype: 'select',
		    searchoptions: {
		        dataUrl: carClassSelectUrl
		    },
		    sortable: false
		},
		{
		    label: '车主',
		    name: 'Owner',
		    index: 'Owner'
		},
//		{
//		    label: '运输单位',
//		    name: 'FleetName',
//		    index: 'FleetName',
//		    width: 80
//		}
        {
        name: 'RentCarName',
        index: 'RentCarName',
		    hidden: true
		},
		{
		    label: '运输单位',
		    name: 'FleetName',
		    index: 'B_CarFleet.FleetName',
		    width: 160
		}
        //,{label:' 当班司机', name: 'Driver', index:'Driver'}   	 
		, {
		    label: '最近一次回厂时间',
		    name: 'LastBackTime',
		    index: 'LastBackTime',
		    formatter: 'datetime',
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datetimepicker();
		        },
		        sopt: ['ge']
		    }
		},
		{
		    label: '空重',
		    name: 'CarWeight',
		    index: 'CarWeight',
		    align: 'right',
		    width: 60
		},
		{
		    label: '原料容量',
		    name: 'StuffWeight',
		    index: 'StuffWeight',
		    align: 'right',
		    width: 60
		},
        {
            label: '品牌型号',
            name: 'Brand',
            index: 'Brand', 
        	width: 60
     },
         {
             label: '车辆颜色',
             name: 'CarColour',
             index: 'CarColour',
             width: 60
         },
         {
             label: '外廓尺寸',
             name: 'OverDimension',
             index: 'OverDimension',
             width: 60
         },
            {
                label: '燃料类型',
                name: 'OilTypeName',
                index: 'OilTypeName',
                width: 60
            },
             {
                 label: '保养间隔里程',
                 name: 'MaintainCycle',
                 index: 'MaintainCycle',
                 width: 60
             },
		{
		    label: '厂商',
		    name: 'Factory',
		    index: 'Factory'
		},
		{
		    label: '购买日期',
		    name: 'BuyDate',
		    index: 'BuyDate',
		    formatter: 'date',
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datepicker();
		        },
		        sopt: ['ge']
		    }
		},
		{
		    label: '出厂日期',
		    name: 'LeaveFacDate',
		    index: 'LeaveFacDate',
		    formatter: 'date',
		    searchoptions: {
		        dataInit: function (elem) {
		            $(elem).datepicker();
		        },
		        sopt: ['ge']
		    }
		},
		{
		    label: '厂内编号',
		    name: 'FacInnerID',
		    index: 'FacInnerID'
		},
		{
		    label: '发动机号',
		    name: 'EngineID',
		    index: 'EngineID'
		},
		{
		    label: 'GPS设备号',
		    name: 'TerminalID',
		    index: 'TerminalID'
		},
		{
		    label: 'SIM卡号',
		    name: 'SIM',
		    index: 'SIM'
		},
		{
		    label: '底盘号',
		    name: 'ChassisID',
		    index: 'ChassisID'
		},
		{
		    label: '耗油量',
		    name: 'OilConsume',
		    index: 'OilConsume',
		    align: 'right',
		    width: 60
		},
		{
		    label: '备注',
		    name: 'Remark',
		    index: 'Remark',
		    search: false
		},
		{
		    label: '排序',
		    name: 'OrderNum',
		    index: 'OrderNum',
		    search: false
		},
		{
		    label: '车架号',
		    name: 'CarVINo',
		    index: 'CarVINo'
		},
		{
		    label: '注册日期',
		    name: 'RegDate',
		    index: 'RegDate',
		    formatter: 'date'
		},
		{
		    label: '发证日期',
		    name: 'IssuingDate',
		    index: 'IssuingDate',
		    formatter: 'date'
		},
		{
		    label: '保险',
		    name: 'Insurance',
		    index: 'Insurance'
		},
		{
		    label: '运营证',
		    name: 'OperCertificate',
		    index: 'OperCertificate'
		},
       	{
		    label: '车辆照片',
		    name: 'PonthUrl',
		    index: 'PonthUrl',
		    hidden: true
		}, 
		{
		    label: '是否启用',
		    name: 'IsUsed',
		    index: 'IsUsed',
		    width: 50,
		    formatter: booleanFmt,
		    unformat: booleanUnFmt,
		    stype: 'select',
		    searchoptions: {
		        value: booleanSearchValues()
		    }
		},
        { label: "车辆文件", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false },
        { label: '视频设备编码', name: 'VideoDeviceGuid', index: 'VideoDeviceGuid' }
        ],
        autoLoad: true,
        functions: {
            handleReload: function (btn) {
                carGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                carGrid.refreshGrid(filterCondition);
            }, 
            handleReport: function (btn) {
                if (!carGrid.isSelectedOnlyOne()) {
                    showMessage('提示', '请选择一条记录进行操作！');
                    return;
                }
                var selectedRecord = carGrid.getSelectedRecord();
                window.open("/Reports/Finance/R020233.aspx?uid=" + selectedRecord.ID);
            },
            handleAdd: function (btn) {
                $('#car_img').attr('src', "");
                carGrid.handleAdd({                      
                    loadFrom: "CarFormDiv",
                    btn: btn,
                    width: 880,
                    height: 680,
                    afterFormLoaded: function () { 
                        $("select[name='Car.CarTypeID']").trigger("change");
                        $("select[name='Car.BelongTo']").trigger("change");

                        carGrid.setFormFieldReadOnly("Car.ID", false);
                        carGrid.setFormFieldDisabled("Car.CarStatus", false);
                        
                    },
                    beforeSubmit: beforeCarFormSubmit,
                    afterFormLoaded: function () {
                        $("#Attachments").empty(); 
                    },
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(response.Data);
                        }
                    }
                });
            },
            handleEdit: function (btn) {
                var Record = carGrid.getSelectedRecord();
                var id = Record.ID;
                carGrid.handleEdit({
                    btn: btn,
                    loadFrom: "CarFormDiv",
                    prefix: "Car", 
                    width: 880,
                    height: 680,
                    afterFormLoaded: function () {
                        //附件
                        var attDiv = $("#Attachments");
                        attDiv.empty();
                        attDiv.append(Record["Attachments"]);
                        $("a[att-id]").show(); 
                        $("select[name='Car.CarTypeID']").trigger("change");
                        $("select[name='Car.BelongTo']").trigger("change");

                        carGrid.setFormFieldReadOnly("Car.ID", true);
                        carGrid.setFormFieldDisabled("Car.CarStatus", true);
                        $('#car_img').attr('src', Record.PonthUrl); 
                    },
                    beforeSubmit: beforeCarFormSubmit,
                   
                    postCallBack: function (response) {
                        if (response.Result) {
                            attachmentUpload(Record.ID);
                        }
                    }
                });
            },
            handleDelete: function (btn) {
                carGrid.deleteRecord({
                    deleteUrl: btn.data.Url 
                });
            },
            handleAddDrive: function (btn) {
                carGrid.handleAdd({
                    title: "快捷增加司机信息",
                    loadFrom: "UserFormDiv",
                    btn: btn,
                    width: 330,
                    height: 200
                })
            }, 
            handleAddAvailableCar: function (btn) {
                carGrid.handleAdd({
                    title: "明天可用车辆",
                    loadFrom: "AvailableCarFormDiv",
                    btn: btn,
                    width: 500,
                    height: 300,
                    postData: {
                        availableCar: $("#availableCar").val(),
                        carNum: $("#carNum").val()
                    },
                    afterFormLoaded: function () {
                        var requestURL = "Dic.mvc/Get";
                        var postParams = {
                            id: date
                        };
                        var rData;
                        ajaxRequest(requestURL, postParams, false,
						function (response) {
						    if (response) {
						        rData = response.Data;
						        carGrid.getFormField("availableCar").val(response.Data == null ? "" : rData.Des);
						        carGrid.getFormField("carNum").val(response.Data == null ? "" : rData.Field1);
						    }
						});
                    }
                })
            },
            handleViewHidden: function (btn) {
                carGrid.refreshGrid("IsUsed = 0");
            },
            handleReUse: function (btn) {
                if (!carGrid.isSelectedOnlyOne()) {
                    showMessage("提示", "请选择一条记录进行操作！");
                    return;
                }
                var selectedRecord = carGrid.getSelectedRecord();
                var isUsedValue = selectedRecord.IsUsed;
                if (isUsedValue == "true") {
                    showMessage("提示", "您选择的记录已是启用状态！");
                    return;
                }
                ajaxRequest(btn.data.Url, {
                    "ID": selectedRecord.ID,
                    "IsUsed": true

                },
				true,
				function () {
				    carGrid.refreshGrid();
				});
            }
        }
    });
    
    carGrid.addListeners("rowclick",
	function (id, record, selBool) {
	    driverGrid.refreshGrid("CarID='" + id + "'");
	});
	carGrid.addListeners('rowdblclick', function (id, record, selBool) {
	    console.log(record);
	    carGrid.handleEdit({
	        title: '查看车辆详细',
	        loadFrom: "CarFormDiv",
	        prefix: "Car",
	        width: 880,
	        height: 680,
	        buttons: {
	            "关闭": function () {
	                $(this).dialog('close');
	            }
	        },
	        afterFormLoaded: function () {
	            console.log(record.PonthUrl);
	            $('#car_img').attr('src', record.PonthUrl);
	        }
	    });
	});

	carGrid.addListeners("gridComplete", function () {
	    var ids = carGrid.getJqGrid().jqGrid('getDataIDs');
	    for (var i = 0; i < ids.length; i++) {
	        var realTimeVideoButton = "<input class='identityButton'  type='button' value='实时视频' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleCarRealTimeVido('" + ids[i] + "');\"  /> ";
	        carGrid.getJqGrid().jqGrid('setRowData', ids[i], { RealTimeVideo: realTimeVideoButton });
	    }
	});


	window.handleCarRealTimeVido = function (carId) {
	    console.log("carId = " + carId);
	    var selectedCar = carGrid.getRecordByKeyValue(carId);
	    //console.log(selectedCar);
	    //console.log(selectedCar["TerminalID"]);
	    //console.log("gSysConfig.CarRealTimeVideoUrl = " + gSysConfig.CarRealTimeVideoUrl);
	    if (gSysConfig.CarRealTimeVideoUrl == undefined || gSysConfig.CarRealTimeVideoUrl == "") {
	        showError("未配置车辆实时视频外部地址，请在【系统全局配置】->【GPS配置】中设置 车辆实时视频URL");
	        return;
	    }


	    var realTimeVideoUrl = gSysConfig.CarRealTimeVideoUrl + "?deviceGuid=" + selectedCar["VideoDeviceGuid"];


	    window.open(realTimeVideoUrl, "_Blank");
	}



    //司机Grid
	var driverGrid = new MyGrid({
	    renderTo: "DriverGrid",
	    title: "司机列表",
	    autoWidth: true,
	    buttons: buttons1,
	    height: 100 //gGridWithTitleHeight
		,
	    storeURL: dirverStoreUrl,
	    sortByField: "ID",
	    primaryKey: "ID",
	    setGridPageSize: 30,
	    showPageBar: true,
	    autoLoad: false,
	    rowNumbers: true,
	    dialogWidth: 300,
	    dialogHeight: 300,
	    storeCondition: "1<>1",
	    initArray: [{
	        label: '子表编号',
	        name: 'ID',
	        index: 'ID',
	        hidden: true
	    },
		{
		    label: '司机姓名',
		    name: 'TrueName',
		    jsonmap: 'User.TrueName',
		    index: 'User.TrueName',
		    width: 80
		},
		{
		    label: '车辆代号',
		    name: 'CarID',
		    index: 'CarID',
		    width: 80
		},
		{
		    label: '司机ID',
		    name: 'UserID',
		    index: 'UserID',
		    hidden: true
		},
		{
		    label: '当班司机',
		    name: 'Driver',
		    index: 'Driver',
		    width: 60,
		    formatter: booleanFmt,
		    unformat: booleanUnFmt
		},
		{
		    label: '班次',
		    name: 'PlanClass',
		    index: 'PlanClass',
		    width: 80
		}],
	    functions: {
	        handleReload: function (btn) {
	            driverGrid.reloadGrid();
	        },
	        handleRefresh: function (btn) {
	            driverGrid.refreshGrid();
	        },
	        handleAdd: function (btn) {
	            if (!carGrid.isSelectedOnlyOne()) {
	                showError("提示", "请先选择要增加司机的车辆！");
	                return;
	            }
	            driverGrid.handleAdd({
	                btn: btn,
	                loadFrom: "DriverFormDiv",
	                afterFormLoaded: function () {
	                    driverGrid.setFormFieldValue("DriverForCar.CarID", carGrid.getSelectedKey());
	                    driverGrid.setFormFieldDisabled("DriverForCar.UserID", false);
	                    $("input[name='TrueName']").attr("disabled", false).next("button").attr("disabled", false);
	                },
	                beforeSubmit: function () {
	                    driverGrid.setFormFieldValue("DriverForCar.UserID", driverGrid.getFormFieldValue("TrueName"));
	                    return true;
	                }
	            });
	        },
	        handleEdit: function (btn) {
	            driverGrid.handleEdit({
	                btn: btn,
	                loadFrom: "DriverFormDiv",
	                prefix: "DriverForCar",
	                afterFormLoaded: function () {
	                    driverGrid.setFormFieldValue("TrueName", driverGrid.getRecordByKeyValue(driverGrid.getSelectedKey())["TrueName"]);
	                    driverGrid.setFormFieldDisabled("DriverForCar.UserID", true);
	                    $("input[name='TrueName']").attr("disabled", true).next("button").attr("disabled", true); 
	                }

	            });
	        },
	        handleDelete: function (btn) {
	            driverGrid.deleteRecord({
	                deleteUrl: btn.data.Url
	            });
	        }
	    }
	});

    $("select[name='Car.CarTypeID']").bind('change', carTypeChanged);
   // $("select[name='Car.BelongTo']").bind('change', belongToChanged);

    //上传附件
    function attachmentUpload(objectId) {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload({
            url: uploadUrl + "?objectType=Car&objectId=" + objectId,
            secureuri: false,
            fileElementId: "uploadFile",
            dataType: "json",
            beforeSend: function () {
                $("#loading").show();
            },
            complete: function () {
                $("#loading").hide();
            },
            success: function (data, status) {
                if (data.Result) {
                    showMessage("附件上传成功");
                    carGrid.reloadGrid();
                }
                else {
                    showError("附件上传失败", data.Message);
                }
            },
            error: function (data, status, e) {
                showError(e);
            }
        }
        );
        return false;

    }
}