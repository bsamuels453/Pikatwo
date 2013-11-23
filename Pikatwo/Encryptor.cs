#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Meebey.SmartIrc4net;

#endregion

namespace Pikatwo{
    internal class Encryptor : IrcComponent{
        ClientInterface _ircInterface;

        #region IrcComponent Members

        public ClientInterface IrcInterface{
            get { return _ircInterface; }
            set{
                _ircInterface = value;
                _ircInterface.OnIrcCommand += IrcInterfaceOnOnIrcCommand;
            }
        }

        public void Update(long secsSinceStart){
        }

        #endregion

        string AssembleMessage(List<string> msg){
            if (msg.Count == 0)
                return "";
            var next = msg[0];
            msg.RemoveAt(0);
            return next + " " + AssembleMessage(msg);
        }

        void IrcInterfaceOnOnIrcCommand(OnCommandArgs args){
            var split = args.Message.Split();

            if (split[0].Contains(".encrypt")){
                Encrypt(args, split);
            }
            if (split[0].Contains(".decrypt")){
                Decrypt(args, split);
            }
        }

        void Decrypt(OnCommandArgs args, string[] inMsg){
            if (inMsg.Length < 3){
                _ircInterface.Client.SendMessage(SendType.Message, args.Source, args.Nick + " the syntax for this command is .decrypt <key> <data>");
                return;
            }
            try{
                var key = AsciiToBase10(inMsg[1].ToCharArray());
                var msgByte = Convert.FromBase64CharArray(inMsg[2].ToCharArray(), 0, inMsg[2].Length);

                var aes = new AesManaged();
                aes.Mode = CipherMode.CBC;
                aes.IV = new byte[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
                aes.KeySize = 128;
                var keyPad = PKCS7(key, 16);
                aes.Key = keyPad;
                aes.Padding = PaddingMode.None;

                var memStrm = new MemoryStream(msgByte.Length);
                var cryptStrm = new CryptoStream(memStrm, aes.CreateDecryptor(), CryptoStreamMode.Write);
                cryptStrm.Write(msgByte, 0, msgByte.Length);

                var ret = memStrm.ToArray();
                aes.Dispose();
                cryptStrm.Dispose();
                memStrm.Dispose();
                ret = StripPKCS7(ret);
                var retStr = new string(Base10ToAscii(ret));
                _ircInterface.Client.SendMessage(SendType.Message, args.Source, args.Nick + ": " + retStr);
            }
            catch{
                _ircInterface.Client.SendMessage
                    (SendType.Message, args.Source, args.Nick + ": something went wrong during decryption. did you change the encrypted string?");
            }
        }

        void Encrypt(OnCommandArgs args, string[] inMsg){
            if (inMsg.Length < 3){
                _ircInterface.Client.SendMessage(SendType.Message, args.Source, args.Nick + " the syntax for this command is .encrypt <key> <data>");
                return;
            }
            try{
                var key = AsciiToBase10(inMsg[1].ToCharArray());
                var msg = AssembleMessage(inMsg.Skip(2).ToList());
                msg = msg.Remove(msg.Length - 1);
                var msgByte = AsciiToBase10(msg.ToCharArray());

                if (msgByte.Length%16 != 0){
                    var paddedLen = (msgByte.Length/16 + 1)*16;
                    msgByte = PKCS7(msgByte, paddedLen);
                }


                var aes = new AesManaged();
                aes.KeySize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Key = PKCS7(key, 16);
                aes.Padding = PaddingMode.None;
                aes.IV = new byte[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};

                var memStrm = new MemoryStream(msgByte.Length);
                var cryptStrm = new CryptoStream(memStrm, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptStrm.Write(msgByte, 0, msgByte.Length);

                var ret = memStrm.ToArray();
                aes.Dispose();
                cryptStrm.Dispose();
                memStrm.Dispose();

                var retb10 = new string(Convert.ToBase64String(ret).ToCharArray());
                _ircInterface.Client.SendMessage(SendType.Message, args.Source, args.Nick + ": " + retb10);
            }
            catch{
                _ircInterface.Client.SendMessage(SendType.Message, args.Source, args.Nick + ": something went wrong during encryption.");
            }
        }


        public static byte[] AsciiToBase10(char[] ascii){
            var ret = new byte[ascii.Length];
            for (int i = 0; i < ascii.Length; i++){
                ret[i] = Convert.ToByte(ascii[i]);
            }
            return ret;
        }

        public static char[] Base10ToAscii(byte[] base10){
            char[] ret = new char[base10.Length];
            for (int i = 0; i < base10.Length; i++){
                ret[i] = Convert.ToChar(base10[i]);
            }
            return ret;
        }

        public static byte[] PKCS7(byte[] str, int length){
            if (str.Length >= length){
                var cret = new byte[length];
                Array.Copy(str, cret, length);
                return cret;
            }
            var ret = new byte[length];
            str.CopyTo(ret, 0);
            byte filler = (byte) (length - str.Length);
            for (int i = str.Length; i < length; i++){
                ret[i] = filler;
            }

            return ret;
        }

        public static byte[] HexToBase10(char[] hex){
            Debug.Assert(hex.Length%2 == 0);
            var base10 = new byte[hex.Length/2];
            for (int i = 0; i < hex.Length; i += 2){
                string val = hex[i].ToString() + hex[i + 1].ToString();
                byte b10Char = Byte.Parse(val, NumberStyles.HexNumber);
                base10[i/2] = b10Char;
            }
            return base10;
        }

        public static byte[] StripPKCS7(byte[] str){
            if (!IsPKCS7Valid(str)){
                throw new Exception("invalid pkcs7");
            }

            byte padLen = str[str.Length - 1];

            var ret = new byte[str.Length - padLen];
            Buffer.BlockCopy(str, 0, ret, 0, ret.Length);
            return ret;
        }

        public static bool IsPKCS7Valid(byte[] str){
            byte padLen = str[str.Length - 1];
            if (padLen == 0 || padLen > str.Length){
                return false;
            }

            int estPadStart = str.Length - padLen;

            for (int i = estPadStart; i < str.Length; i++){
                if (str[i] != padLen){
                    return false;
                }
            }
            return true;
        }

        public static char[] Base10ToHex(byte[] base10){
            var ret = new List<char>();
            for (int i = 0; i < base10.Length; i += 1){
                byte b = base10[i];
                //string hexOutput = String.Format("{0:X}", b);
                string hexOutput = Convert.ToInt32(b).ToString("X").ToLower();
                if (hexOutput.Length == 1){
                    hexOutput = "0" + hexOutput;
                }
                ret.AddRange(hexOutput.ToLower());
            }
            return ret.ToArray();
        }
    }
}