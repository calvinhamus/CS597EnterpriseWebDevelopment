using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Trend.Core.Data;
using Trend.Web.Models;
using Microsoft.AspNet.Identity;

namespace Trend.Web.SignalRChart
{
    public class ChartService 
    {
        private readonly static Lazy<ChartService> _instance = new Lazy<ChartService>(
            () => new ChartService(GlobalHost.ConnectionManager.GetHubContext<ChartHub>().Clients));

        private readonly object _marketStateLock = new object();
        private readonly object _updateStockPricesLock = new object();

        private readonly ConcurrentDictionary<int, T_DataValue> _stocks = new ConcurrentDictionary<int, T_DataValue>();

        // Stock can go up or down by a percentage of this factor on each change
        private readonly double _rangePercent = 0.002;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(500);
        private readonly Random _updateOrNotRandom = new Random();

        private Timer _timer;
        private volatile bool _updatingStockPrices;

        private ChartService(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
           // _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
            // LoadDefaultStocks();
        }

        public static ChartService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        //    public MarketState MarketState
        //    {
        //        get { return _marketState; }
        //        private set { _marketState = value; }
        //    }

        //    public IEnumerable<Stock> GetAllStocks()
        //    {
        //        return _stocks.Values;
        //    }

        public void Ready()
        {
            _stocks.Clear();

            var stocks = new List<T_DataValue>
                    {
                        new T_DataValue {Id = 1, Value = 41.68m },
                        new T_DataValue { Id = 2, Value = 92.08m },
                        new T_DataValue { Id = 3, Value = 543.01m }
                    };

            stocks.ForEach(stock => _stocks.TryAdd(stock.Id, stock));

            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

              
        }

        internal void SaveChart(HubClient client)
        {
            //var user = User.Identity.GetUserId();
            //var chart = new T_SavedChart
            //{
            //    T_UserId = ,

            //}

            throw new NotImplementedException();
        }

       

        //    public void CloseMarket()
        //    {
        //        lock (_marketStateLock)
        //        {
        //            if (MarketState == MarketState.Open)
        //            {
        //                if (_timer != null)
        //                {
        //                    _timer.Dispose();
        //                }

        //                MarketState = MarketState.Closed;

        //                BroadcastMarketStateChange(MarketState.Closed);
        //            }
        //        }
        //    }

        //    public void Reset()
        //    {
        //        lock (_marketStateLock)
        //        {
        //            if (MarketState != MarketState.Closed)
        //            {
        //                throw new InvalidOperationException("Market must be closed before it can be reset.");
        //            }

        //            LoadDefaultStocks();
        //            BroadcastMarketReset();
        //        }
        //    }

        //    private void LoadDefaultStocks()
        //    {
        //        _stocks.Clear();

        //        var stocks = new List<Stock>
        //        {
        //            new Stock { Symbol = "MSFT", Price = 41.68m },
        //            new Stock { Symbol = "AAPL", Price = 92.08m },
        //            new Stock { Symbol = "GOOG", Price = 543.01m }
        //        };

        //        stocks.ForEach(stock => _stocks.TryAdd(stock.Symbol, stock));
        //    }

        private void UpdateStockPrices(object state)
        {
            // This function must be re-entrant as it's running as a timer interval handler
            lock (_updateStockPricesLock)
            {
                if (!_updatingStockPrices)
                {
                    _updatingStockPrices = true;

                    foreach (var stock in _stocks.Values)
                    {
                        if (TryUpdateStockPrice(stock))
                        {
                            BroadcastStockPrice(stock);
                        }
                    }

                    _updatingStockPrices = false;
                }
            }
        }

        private bool TryUpdateStockPrice(T_DataValue stock)
        {
            // Randomly choose whether to udpate this stock or not
            var r = _updateOrNotRandom.NextDouble();
            if (r > 0.1)
            {
                return false;
            }

            // Update the stock price by a random factor of the range percent
            var random = new Random((int)Math.Floor(stock.Value));
            var percentChange = random.NextDouble() * _rangePercent;
            var pos = random.NextDouble() > 0.51;
            var change = Math.Round(stock.Value * (decimal)percentChange, 2);
            change = pos ? change : -change;

            stock.Value += change;
            return true;
        }

        //    private void BroadcastMarketStateChange(MarketState marketState)
        //    {
        //        switch (marketState)
        //        {
        //            case MarketState.Open:
        //                Clients.All.marketOpened();
        //                break;
        //            case MarketState.Closed:
        //                Clients.All.marketClosed();
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    private void BroadcastMarketReset()
        //    {
        //        Clients.All.marketReset();
        //    }

        private void BroadcastStockPrice(T_DataValue chartData)
        {
            Clients.All.updateChart(chartData);
        }
        //}

        //public enum MarketState
        //{
        //    Closed,
        //    Open
        //}
    }
}