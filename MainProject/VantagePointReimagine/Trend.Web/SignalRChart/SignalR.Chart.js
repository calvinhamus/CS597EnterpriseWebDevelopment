var running = false;
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
            // return signalrchart.server.startChartData();
        })
        .done(function (state) {
           
        });
    $("#liveMode").click(function () {
        if (!running) {
            signalrchart.server.startChartData();
            running = true;
        }
        running = false;


    });
});
$(function () {
    $('#datetimepicker1').datetimepicker();
    $('#datetimepicker2').datetimepicker({
        useCurrent: false //Important! See issue #1075
    });
    $("#datetimepicker1").on("dp.change", function (e) {
        $('#datetimepicker2').data("DateTimePicker").minDate(e.date);
        var x = $('#datetimepicker2').data("DateTimePicker").date()
        console.log(x._i );
    });
    $("#datetimepicker2").on("dp.change", function (e) {
        $('#datetimepicker1').data("DateTimePicker").maxDate(e.date);
        console.log(e.date);
    });
});
$("#getDataBtn").click(function () {
    var startDate = $('#datetimepicker1').data("DateTimePicker").date();
    var endDate = $('#datetimepicker2').data("DateTimePicker").date();
    $.ajax({
        type: 'post',
        dataType: 'json',
        cache: false,
        url: '/Trend/Home/GetChartData',
        data: { StartDate: startDate._i, EndDate: endDate._i },
        success: function (response, textStatus, jqXHR) {
            alert(response);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert('Error - ' + errorThrown);
        }
    });

});
