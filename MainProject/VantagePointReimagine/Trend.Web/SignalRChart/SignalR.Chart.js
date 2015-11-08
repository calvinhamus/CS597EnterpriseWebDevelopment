var running = false;
var myClientId = "";
$(function () {
  
    var signalrchart = $.connection.signalrchart // the generated client-side hub proxy
       

    function stopTicker() {
        $stockTickerUl.stop();
    }

    // Add client-side hub methods that the server will call
    $.extend(signalrchart.client, {
        updateChart: function (data) {
            console.log(data.Value);
          //  myLineChart.removeData();
         //   myLineChart.addData([data.Value], data.Id);
            
          //  myLineChart.update();

           
        },

    });

    // Start the connection
    $.connection.hub.start()
      //  .then(init)
        .then(function () {
            console.log("Connected");
            myClientId = $.connection.hub.id;
            console.log(myClientId);
            // return signalrchart.server.startChartData();
        })
        .done(function (state) {
           
        });
    $("#liveMode").click(function () {
        if (!running) {
            signalrchart.server.startChartData(myClientId);
            running = true;
        }
        running = false;


    });

    $('.mdi-content-add').click(function (e) {
        console.log(e.currentTarget.id);
        signalrchart.server.addToChart(e.currentTarget.id);

    });
    $('.mdi-content-remove').click(function (e) {
        console.log(e.currentTarget.id);
         signalrchart.server.removeFromChart(e.currentTarget.id);

    });
});
$('#date-end').bootstrapMaterialDatePicker({format:'DD/MM/YYYY HH:mm', weekStart: 0 });
$('#date-start').bootstrapMaterialDatePicker({ format: 'DD/MM/YYYY HH:mm', weekStart: 0 }).on('change', function (e, date) {
    $('#date-end').bootstrapMaterialDatePicker('setMinDate', date);
});

//$("#getDataBtn").click(function () {
//    var startDate = $('#datetimepicker1').data("DateTimePicker").date();
//    var endDate = $('#datetimepicker2').data("DateTimePicker").date();
//    $.ajax({
//        type: 'post',
//        dataType: 'json',
//        cache: false,
//        url: '/Trend/Home/GetChartData',
//        data: { StartDate: startDate._i, EndDate: endDate._i },
//        success: function (response, textStatus, jqXHR) {
//            alert(response);
//        },
//        error: function (jqXHR, textStatus, errorThrown) {
//            alert('Error - ' + errorThrown);
//        }
//    });

//});
