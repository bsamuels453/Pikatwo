using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RawParse {
    class Program {
        /// <summary>
        /// parses hexchat-generated log files into the pika-comaptible raw format
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            var sr = new StreamReader(args[0]);
            var sw = new StreamWriter("output.txt");
            sw.AutoFlush = true;

            string input;
            while ((input = sr.ReadLine()) != null){
                if (input.Count() < 5)
                    continue;
                if (input.Contains("**** "))//block hexchat logging stuff
                    continue;
                if(input.Contains("*	"))//block hexchat logging stuff
                    continue;
                if (input.Contains("-NickServ-"))//block nickserv stuff
                    continue;
                int openingBracketIdx = input.IndexOf('<');
                int closingBracketIdx = input.IndexOf('>');
                if (openingBracketIdx == -1 || closingBracketIdx == -1)
                    continue;
                if (openingBracketIdx > closingBracketIdx)
                    continue;
                string name = input.Substring(openingBracketIdx+1, closingBracketIdx - openingBracketIdx-1);
                string content = input.Substring(closingBracketIdx + 2);
                sw.WriteLine(name + ":" + content);
            }
            sr.Close();
            sw.Close();
        }
    }
}
