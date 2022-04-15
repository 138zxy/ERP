function init(model)
{
        $("#HR_KQ_Set_ID").val(model.ID);
        $("#HR_KQ_Set_LeaveMinuteKG").val(model.LeaveMinuteKG);
        $("#HR_KQ_Set_LateMinuteKG").val(model.LateMinuteKG);
        $("#HR_KQ_Set_KGRate").val(model.KGRate);
        $("#HR_KQ_Set_MonthLateMinute").val(model.MonthLateMinute);
        $("#HR_KQ_Set_DeviceType").val(model.DeviceType);
        $("#HR_KQ_Set_Machine").val(model.Machine);
        $("#HR_KQ_Set_Dispatch").val(model.Dispatch);
        $("#HR_KQ_Set_IP").val(model.IP);
        $("#HR_KQ_Set_SerialPort").val(model.SerialPort);
        $("#HR_KQ_Set_BoteLV").val(model.BoteLV);
        $("#HR_KQ_Set_PortNo").val(model.PortNo);
        $("#HR_KQ_Set_DispatchPass").val(model.DispatchPass); 
}

function HR_KQ_SetIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' 
        , autoWidth: true
        , buttons: buttons0
        , height: 280
		, storeURL: options.itemUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30 
		, showPageBar: true  
		, initArray: [
              { label: 'ID', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '区间起(分钟)', name: 'StartMinute', index: 'StartMinute', editable: true, width: 80 }
            , { label: '区间止(分钟)', name: 'EndMinute', index: 'EndMinute', editable: true, width: 80 }
            , { label: '处罚金额(元)', name: 'PunishMoney', index: 'PunishMoney', editable: true, width: 120 } 
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDiv',
                    btn: btn
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    prefix: "HR_KQ_SetItem",
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    window.Save = function () {
        var ID = $("#HR_KQ_Set_ID").val();
        var LeaveMinuteKG = $("#HR_KQ_Set_LeaveMinuteKG").val();
        var LateMinuteKG = $("#HR_KQ_Set_LateMinuteKG").val();
        var KGRate = $("#HR_KQ_Set_KGRate").val();
        var MonthLateMinute = $("#HR_KQ_Set_MonthLateMinute").val();
        var DeviceType = $("#HR_KQ_Set_DeviceType").val();
        var Machine = $("#HR_KQ_Set_Machine").val();
        var Dispatch = $("#HR_KQ_Set_Dispatch").val();
        var IP = $("#HR_KQ_Set_IP").val();
        var SerialPort = $("#HR_KQ_Set_SerialPort").val();
        var BoteLV = $("#HR_KQ_Set_BoteLV").val();
        var PortNo = $("#HR_KQ_Set_PortNo").val();
        var DispatchPass = $("#HR_KQ_Set_DispatchPass").val(); 
        var requestURL = options.AddOrUpdateUrl;
        ajaxRequest(requestURL, {
            ID: ID,
            LeaveMinuteKG: LeaveMinuteKG,
            LateMinuteKG: LateMinuteKG,
            KGRate: KGRate,
            MonthLateMinute: MonthLateMinute,
            DeviceType: DeviceType,
            Machine: Machine,
            Dispatch: Dispatch,
            IP: IP,
            SerialPort: SerialPort,
            BoteLV: BoteLV,
            PortNo: PortNo,
            DispatchPass: DispatchPass
        },
		false,
		function (response) {
			if (!!response.Result) {
                if(response.Data!=null)
                {
                     $("#HR_KQ_Set_ID").val(response.Data); 
                 }
				showMessage('提示', '保存成功！'); 
			} else {
				showMessage('提示', response.Message);
			}
		});
    }

}