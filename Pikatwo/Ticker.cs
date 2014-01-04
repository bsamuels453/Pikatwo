#region

using System;
using System.Net;
using Meebey.SmartIrc4net;
using Newtonsoft.Json.Linq;

#endregion

namespace Pikatwo{
    internal class Ticker : IrcComponent{
        ClientInterface _client;

        #region IrcComponent Members

        public ClientInterface IrcInterface{
            get { return _client; }
            set{
                _client = value;
                _client.OnIrcCommand += ClientOnOnIrcCommand;
            }
        }

        public void Update(long secsSinceStart){
        }

        public string[] GetCmdDocs(){
            return new[]{
                ".bicker",
                ".dicker",
                ".licker"
            };
        }

        #endregion

        void ClientOnOnIrcCommand(OnCommandArgs args){
            try{
                if (args.Message == ".bicker"){
                    var tickerStr = GenerateBtceTicker("usd", "btc");
                    _client.Client.SendMessage(SendType.Message, args.Source, tickerStr);
                }
                if (args.Message == ".licker"){
                    var tickerStr = GenerateBtceTicker("usd", "ltc");
                    _client.Client.SendMessage(SendType.Message, args.Source, tickerStr);
                }
                if (args.Message == ".dicker"){
                    var tickerStr = GenerateVircurexTicker("btc", "doge");
                    _client.Client.SendMessage(SendType.Message, args.Source, tickerStr);
                }
            }
            catch{
                _client.Client.SendMessage(SendType.Message, args.Source, "Exchange API is non-responsive, try again later");
            }
        }

        string GenerateVircurexTicker(string refCurrency, string targCurrency){
            var wclient = new WebClient();
            var ticker = wclient.DownloadString("https://vircurex.com/api/get_info_for_1_currency.json?base=" + targCurrency + "&alt=" + refCurrency);
            var jticker = JObject.Parse(ticker);
            wclient.Dispose();

            var avgPrice = TruncateToSignificantDigits((jticker["highest_bid"].ToObject<double>() + jticker["lowest_ask"].ToObject<double>())/2d, 4);
            var volume = jticker["volume"].ToObject<double>();
            volume = (volume/1000000d);
            volume = TruncateToSignificantDigits(volume, 4);
            //var high = TruncateToSignificantDigits(jticker["high"].ToObject<double>(), 4);
            //var low = TruncateToSignificantDigits(jticker["low"].ToObject<double>(), 4);
            var pairName = (targCurrency + refCurrency).ToUpper();
            string ret = pairName + " ticker | Avg:" + avgPrice + refCurrency + " | 24h_volume:" + volume + "M" + targCurrency;
            return ret;
        }

        string GenerateBtceTicker(string refCurrency, string targCurrency){
            var wclient = new WebClient();
            var ticker = wclient.DownloadString("https://btc-e.com/api/2/" + targCurrency + "_" + refCurrency + "/ticker");
            var jticker = JObject.Parse(ticker)["ticker"];
            wclient.Dispose();

            var avgPrice = TruncateToSignificantDigits((jticker["sell"].ToObject<double>() + jticker["buy"].ToObject<double>())/2d, 4);
            var volume = jticker["vol"].ToObject<double>();
            volume = (volume/1000d);
            volume = TruncateToSignificantDigits(volume, 4);
            var high = TruncateToSignificantDigits(jticker["high"].ToObject<double>(), 4);
            var low = TruncateToSignificantDigits(jticker["low"].ToObject<double>(), 4);
            var pairName = (targCurrency + refCurrency).ToUpper();
            string ret = pairName + " ticker | Avg:" + avgPrice + refCurrency + " | high:" + high + refCurrency + " | low:" + low + refCurrency +
                " | 24h_volume:" + volume + "K" + targCurrency;
            return ret;
        }

        static double TruncateToSignificantDigits(double d, int digits){
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1 - digits);
            return scale*Math.Truncate(d/scale);
        }
    }
}