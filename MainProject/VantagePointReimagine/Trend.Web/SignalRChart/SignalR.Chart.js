$(function () {

    var signalrchart = $.connection.signalrchart // the generated client-side hub proxy
       
    function formatStock(stock) {
        return $.extend(stock, {
            Price: stock.Price.toFixed(2),
            PercentChange: (stock.PercentChange * 100).toFixed(2) + '%',
            Direction: stock.Change === 0 ? '' : stock.Change >= 0 ? up : down,
            DirectionClass: stock.Change === 0 ? 'even' : stock.Change >= 0 ? 'up' : 'down'
        });
    }

    function scrollTicker() {
        var w = $stockTickerUl.width();
        $stockTickerUl.css({ marginLeft: w });
        $stockTickerUl.animate({ marginLeft: -w }, 15000, 'linear', scrollTicker);
    }

    function stopTicker() {
        $stockTickerUl.stop();
    }

    //function init() {
    //    return chart.server.getAllStocks().done(function (stocks) {
    //        $stockTableBody.empty();
    //        $stockTickerUl.empty();
    //        $.each(stocks, function () {
    //            var stock = formatStock(this);
    //            $stockTableBody.append(rowTemplate.supplant(stock));
    //            $stockTickerUl.append(liTemplate.supplant(stock));
    //        });
    //    });
    //}

    // Add client-side hub methods that the server will call
    $.extend(signalrchart.client, {
        updateStockPrice: function (stock) {
            console.log(stock.Value);
            //var displayStock = formatStock(stock),
            //    $row = $(rowTemplate.supplant(displayStock)),
            //    $li = $(liTemplate.supplant(displayStock)),
            //    bg = stock.LastChange < 0
            //            ? '255,148,148' // red
            //            : '154,240,117'; // green

            //$stockTableBody.find('tr[data-symbol=' + stock.Symbol + ']')
            //    .replaceWith($row);
            //$stockTickerUl.find('li[data-symbol=' + stock.Symbol + ']')
            //    .replaceWith($li);

            //$row.flash(bg, 1000);
            //$li.flash(bg, 1000);
        },

    });

    // Start the connection
    $.connection.hub.start()
      //  .then(init)
        .then(function () {
            return signalrchart.server.getMarketState();
        })
        .done(function (state) {
           
        });
});