namespace IrcClient{
    public class IrcMsg{
        /// <summary>
        /// stuff like PRIVMSG and VERSION
        /// </summary>
        public string Command;

        /// <summary>
        /// parameters for the command, includes stuff like the sender for PRIVMSG
        /// </summary>
        public string[] CommandParams;

        /// <summary>
        /// kind of a second set of parameters for a command, in the case of recv PRIVMSG from a channel, this is the name of the sender
        /// </summary>
        public string Prefix;

        /// <summary>
        /// the trailing text to the IrcMsg. usually contains long sentences like the motd
        /// </summary>
        public string Trailing;
    }
}