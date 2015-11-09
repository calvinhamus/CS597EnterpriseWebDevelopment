var myClientId = "";
var startDate = "";
var endDate = "";
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
    $("#liveMode").change(function () {
        console.log(this.checked);
        if (this.checked) {
            signalrchart.server.startChartData(myClientId);
        } else {
            signalrchart.server.stopChartData(myClientId);
        }
        //if (!running) {
        //    running = true;
        //    signalrchart.server.startChartData(myClientId);

        //} else {
        //    running = false;
        //    signalrchart.server.stopChartData(myClientId);
        //}


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
$('#date-end').bootstrapMaterialDatePicker({ format: 'MM/DD/YYYY HH:mm', weekStart: 0 }).on('change', function (e, date) {
    console.log(date._i);
    endDate = date._i;
});
$('#date-start').bootstrapMaterialDatePicker({ format: 'MM/DD/YYYY HH:mm', weekStart: 0 }).on('change', function(e, date) {
    $('#date-end').bootstrapMaterialDatePicker('setMinDate', date);
    startDate = date._i;
});

$("#getDataBtn").click(function () {
    console.log(startDate + " " + endDate);
    if (startDate != "" && endDate != "") {
        $.ajax({
            type: 'post',
            dataType: 'json',
            cache: false,
            url: '/Trend/Home/GetChartData',
            data: { StartDate: startDate, EndDate: endDate },
            success: function (response, textStatus, jqXHR) {
                alert(response);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Error - ' + errorThrown);
            }
        });
    }

    //var startDate = $('#date-end').bootstrapMaterialDatePicker().date;
    //console.log(startDate);
    //var endDate = $('#datetimepicker2').data("DateTimePicker").date();
    
   

});
