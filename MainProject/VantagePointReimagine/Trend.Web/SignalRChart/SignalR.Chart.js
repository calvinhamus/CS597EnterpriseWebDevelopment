var myClientId = "";
var startDate = "";
var endDate = "";
$(function () {
    var dataPoints = [];
    var dataLegend = [];
    var chartdata = {
        labels: ["null", "null", "null", "null", "null", "null", "null"],
        datasets: [
             {
                 label: "My First dataset",
                 fillColor: "rgba(220,220,220,0.2)",
                 strokeColor: "rgba(220,220,220,1)",
                 pointColor: "rgba(220,220,220,1)",
                 pointStrokeColor: "#fff",
                 pointHighlightFill: "#fff",
                 pointHighlightStroke: "rgba(220,220,220,1)",
                 data: [0, 0, 0, 0, 0, 0,0]
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


        responsive: true,
        maintainAspectRatio: false,

        //String - A legend template
        // legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].strokeColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"
        legendTemplate: '<ul id="legendUl">'
                      + '<% for (var i=0; i<datasets.length; i++) { %>'
                        + '<li>'
                        + '<span style=\"background-color:<%=datasets[i].lineColor%>\"></span>'
                        + '<% if (datasets[i].label) { %><%= datasets[i].label %><% } %>'
                      + '</li>'
                    + '<% } %>'
                  + '</ul>'
    };

    //// Get the context of the canvas element we want to select
    var ctx = document.getElementById("myChart").getContext("2d");

    myLineChart = new Chart(ctx).Line(chartdata, options);

  //  var legend = myLineChart.generateLegend();

    //and append it to your page somewhere
   // $('#legendDiv').append(legend);
    var signalrchart = $.connection.signalrchart // the generated client-side hub proxy
       
  
    function stopTicker() {
        $stockTickerUl.stop();
    }

    // Add client-side hub methods that the server will call
    $.extend(signalrchart.client, {
        updateChart: function (returndata) {
         //   var dataSet = [[returndata.Values, returndata.DateTime]]
           
          //  console.log(returndata.Values);
            myLineChart.removeData();
            myLineChart.addData(returndata.Values, returndata.DateTime);
           
            myLineChart.update();

           
        },
        addToLegend: function (data) {

            for (var i = 0; i < chartdata.datasets.length; i++) {
                var obj = chartdata.datasets[i];

                if (obj.label == "My First dataset") {
                    chartdata.datasets.splice(i, 1);
                }
            };
          //  console.log(data);
            dataLegend.push(data.Label);
            dataPoints.push(data.DataPointId);
            var a =
            {
                label: data.Label,
                fillColor: data.FillColor,
                strokeColor: data.StrokeColor,
                pointColor: data.PointColor,
                pointStrokeColor: data.PointStrokeColor,
                pointHighlightFill: data.PointHighlightFill,
                pointHighlightStroke: data.PointHighlightStroke,
                data: [0,0,0,0,0,0,0]
            }
            chartdata.datasets.push(a);
           // console.log(chartdata)
            ctx.clearRect(0, 0, myChart.width, myChart.height);
            myLineChart.destroy();
            myLineChart = new Chart(ctx).Line(chartdata, options);
            //datasets.push(a);
            //var x = window.myLineChart.datasets;
           // myLineChart.datasets.push(a);
           // myLineChart.update();

            var legend = myLineChart.generateLegend();
            $('#legendUl').remove();
            //and append it to your page somewhere
            $('#legendDiv').append(legend);
        },
        removeFromLegend: function (data) {

            for (var i = 0; i < chartdata.datasets.length; i++) {
                var obj = chartdata.datasets[i];

                if (data.Label == obj.label) {
                    chartdata.datasets.splice(i, 1);
                }
            };
           
          //  console.log(chartdata)
            ctx.clearRect(0, 0, myChart.width, myChart.height);
            myLineChart.destroy();
            myLineChart = new Chart(ctx).Line(chartdata, options);

            var legend = myLineChart.generateLegend();
            $('#legendUl').remove();
            //and append it to your page somewhere
            $('#legendDiv').append(legend);
        },
        chartSaved: function (data) {
            var chartName = $('#saveChartName').val();
            var holder = "";
            $.each(dataLegend, function (index, value) {
                holder = holder + " " + value;
            });
           
            $('#chartsList').append(' <a href="#" id="chart-' + data + '" data-toggle="modal" data-target="#exampleModal" data-whatever="' + chartName + '" data-points="'+holder+'" data-chartid="' + data + '">' + chartName + ' <i class="glyphicon glyphicon-list-alt"></i></a>')
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
      //  console.log(this.checked);
        if (this.checked) {
            signalrchart.server.startChartData(myClientId);
        } else {
            signalrchart.server.stopChartData(myClientId);
        }
    });

    $('.mdi-content-add').click(function (e) {
       
        signalrchart.server.addToChart($.connection.hub.id, e.currentTarget.id);
    });
    $('.mdi-content-remove').click(function (e) {
      //  console.log(e.currentTarget.id);
        var a = dataPoints.indexOf(e.currentTarget.id);
        dataPoints.splice(a, 1);
        signalrchart.server.removeFromChart($.connection.hub.id,e.currentTarget.id);

    });


    $('#date-end').bootstrapMaterialDatePicker({ format: 'MM/DD/YYYY HH:mm', weekStart: 0 }).on('change', function (e, date) {
      //  console.log(date._i);
        endDate = date._i;
    });
    $('#date-start').bootstrapMaterialDatePicker({ format: 'MM/DD/YYYY HH:mm', weekStart: 0 }).on('change', function(e, date) {
        $('#date-end').bootstrapMaterialDatePicker('setMinDate', date);
        startDate = date._i;
    });
    $('#saveChartBtn').click(function (e) {
        var chartName = $('#saveChartName').val();
        signalrchart.server.saveChart(myClientId, chartName);
       
    });
    $("#getDataBtn").click(function () {

        var startDate = $('#date-start').val();
        var endDate = $('#date-end').val();
        myLineChart.destroy();
     //   console.log(startDate + " " + endDate);
        if (startDate != "" && endDate != "") {
            $.ajax({
                type: 'post',
                dataType: 'json',
                cache: false,
                url: '/Trend/Home/GetChartData',
                data: { StartDate: startDate, EndDate: endDate, DataPointIds: dataPoints },
                success: function (response, textStatus, jqXHR) {
                   // alert(response);
                    for (i = 0; i < response.Values.length; i++)
                    {
                      //  console.log("Values: "+response.Values[i] + " Labels: "+ response.Labels[i])
                        myLineChart.addData(response.Values[i], response.Labels[i]);

                        myLineChart.update();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert('Error - ' + errorThrown);
                }
            });
        }
    });
    $('#exampleModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var chartName = button.data('whatever') // Extract info from data-* attributes
        var points = button.data('points')
        var chartId = button.data('chartid')
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this)
        modal.find('.modal-title').text(chartName)
        modal.find('#chartId-text').text(chartId)
        modal.find('#point-name').text(points)
    })
    $('#loadChartBtn').click(function (e) {
        var id = $('#chartId-text').text();
        signalrchart.server.loadChart(id);
    })
    $('#deleteChartBtn').click(function (e) {
        var id = $('#chartId-text').text();
        $('#chart-' + id).remove();
        signalrchart.server.deleteChart(id);
    })
});
