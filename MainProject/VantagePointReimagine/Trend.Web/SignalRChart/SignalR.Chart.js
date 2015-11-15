var datasets = [];

var data = {
    labels: ["January", "February", "March", "April", "May", "June", "July"],
    datasets: [
        {
            label: "My First dataset",
            fillColor: "#1a4356",
            strokeColor: "rgba(220,220,220,1)",
            pointColor: "rgba(220,220,220,1)",
            pointStrokeColor: "#fff",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(220,220,220,1)",
            data: [65, 59, 80, 81, 56, 55, 40]
        },
        {
            label: "My Second dataset",
            fillColor: "rgba(151,187,205,0.2)",
            strokeColor: "rgba(151,187,205,1)",
            pointColor: "rgba(151,187,205,1)",
            pointStrokeColor: "#fff",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(151,187,205,1)",
            data: [28, 48, 40, 19, 86, 27, 90]
        }
    ]
};

var options = {
    animation: true,
    ///Boolean - Whether grid lines are shown across the chart
    scaleShowGridLines: true,

    //String - Colour of the grid lines
    scaleGridLineColor: "rgba(0,0,0,.05)",

    //Number - Width of the grid lines
    scaleGridLineWidth: 1,

    //Boolean - Whether to show horizontal lines (except X axis)
    scaleShowHorizontalLines: true,

    //Boolean - Whether to show vertical lines (except Y axis)
    scaleShowVerticalLines: true,

    //Boolean - Whether the line is curved between points
    bezierCurve: true,

    //Number - Tension of the bezier curve between points
    bezierCurveTension: 0.4,

    //Boolean - Whether to show a dot for each point
    pointDot: true,

    //Number - Radius of each point dot in pixels
    pointDotRadius: 4,

    //Number - Pixel width of point dot stroke
    pointDotStrokeWidth: 1,

    //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    pointHitDetectionRadius: 20,

    //Boolean - Whether to show a stroke for datasets
    datasetStroke: true,

    //Number - Pixel width of dataset stroke
    datasetStrokeWidth: 2,

    //Boolean - Whether to fill the dataset with a colour
    datasetFill: true,

    //String - A legend template
   // legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].strokeColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"
    legendTemplate: '<ul>'
                  + '<% for (var i=0; i<datasets.length; i++) { %>'
                    + '<li>'
                    + '<span style=\"background-color:<%=datasets[i].lineColor%>\"></span>'
                    + '<% if (datasets[i].label) { %><%= datasets[i].label %><% } %>'
                  + '</li>'
                + '<% } %>'
              + '</ul>'
};



// Get the context of the canvas element we want to select
var ctx = document.getElementById("myChart").getContext("2d");

 window.myLineChart = new Chart(ctx).Line(data, options);

var legend = myLineChart.generateLegend();

//and append it to your page somewhere
$('#legendDiv').append(legend);


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
        updateChart: function (returndata) {
            var dataSet = [[returndata.Value, 0]]
           
            console.log(returndata.Value);
            window.myLineChart.removeData();
            window.myLineChart.addData([returndata.Value], returndata.DateTime);
           
            window.myLineChart.update();

           
        },
        addToLegend: function (data) {
            console.log(data);

            var a =
            {
                label: data.Label,
                fillColor: data.FillColor,
                strokeColor: data.StrokeColor,
                pointColor: data.PointColor,
                pointStrokeColor: data.PointStrokeColor,
                pointHighlightFill: data.PointHighlightFill,
                pointHighlightStroke: data.PointHighlightStroke,
                data: [0,0,0,0]
            }
            datasets.push(a);

        }

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
    });

    $('.mdi-content-add').click(function (e) {
        console.log(e.currentTarget.id);
        console.log(e.currentTarget.parentElement.parentElement.innerText)
        signalrchart.server.addToChart($.connection.hub.id, e.currentTarget.id);


    });
    $('.mdi-content-remove').click(function (e) {
        console.log(e.currentTarget.id);
        signalrchart.server.removeFromChart($.connection.hub.id,e.currentTarget.id);

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

    window.myLineChart.destroy();
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
    //var ctx = document.getElementById("myChart").getContext("2d");
    //// var myNewChart = new Chart(ctx).PolarArea(data);

    //var newData =
    //{
    //    labels: ["January", "February", "March", "April", "May", "June", "July"],
    //    datasets: datasets
    //}
   

    //window.myLineChart = new Chart(ctx).Line(newData, options);

    //var legend = window.myLineChart.generateLegend();

    ////and append it to your page somewhere
    //$('#legendDiv').append(legend);
    //var startDate = $('#date-end').bootstrapMaterialDatePicker().date;
    //console.log(startDate);
    //var endDate = $('#datetimepicker2').data("DateTimePicker").date();
    
   

});
