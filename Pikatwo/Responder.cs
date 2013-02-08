using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IrcClient;
using Newtonsoft.Json;

namespace Pikatwo{
    internal class Responder : IrcComponent{
        readonly string _triggerWord;
        readonly Quote[] _quotes;
        readonly Random _randGen;
        readonly string _channelName;

        public Responder(string triggerWord, string quoteFile, string channelName){
            _triggerWord = triggerWord;
            _channelName = channelName;
            var sr = new StreamReader(quoteFile);
            _quotes = JsonConvert.DeserializeObject<Quote[]>(sr.ReadToEnd());
            sr.Close();

            _randGen = new Random((int)DateTime.Now.ToFileTime());
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "PRIVMSG"){
                if (msg.CommandParams[0].Contains("#")){ //be certain we're recieving this from channel
                    if (msg.Trailing.Contains(_triggerWord)){
                        //now is the interesting part
                        //the issue at hand is that we have 200k quotes and have to choose one at random with weights
                        //the lazyman solution is to create a list of pointers where if quote N has twice the chance
                        //of a normal quote to be chosen, it gets two pointers. this will use up far too much memory
                        //with the amount of quotes we have.

                        //This solution didn't seem to appropriate at first glance, but after some thought it seems to be the best.
                        //What happens:
                        //1. A quote is chosen from list at random
                        //2. A 1-100 die is rolled, if the quote has probability of 30, a roll between 1 and 30 is required.
                        //   for the quote to be chosen.
                        //3. If the roll fails, another quote is chosen at random and die is rolled again.

                        int randIdx = _randGen.Next(0, _quotes.Length);

                        Quote quoteToUse;
                        while (true){
                            int chance = _randGen.Next(1, 100);
                            if (_quotes[randIdx].Score <= chance && _quotes[randIdx].InlinedNicks == 1){
                                quoteToUse = _quotes[randIdx];
                                break;
                            }
                            randIdx = _randGen.Next(0, _quotes.Length);
                        }
                        string parsedName = msg.Prefix.Substring(1);
                        int delimitierIdx = parsedName.IndexOf('!');
                        parsedName = parsedName.Substring(0, delimitierIdx);
                        string replacedQuote = quoteToUse.Text.Replace("$(0)",parsedName);

                        sendMethod.Invoke(IrcCommand.Message, _channelName, replacedQuote);
                    }
                }
            }

        }


        public void Dispose(){
        }

        public void Reset(){
        }
    }
}