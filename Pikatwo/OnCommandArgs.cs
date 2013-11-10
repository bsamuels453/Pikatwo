namespace Pikatwo{
    internal class OnCommandArgs{
        public readonly AuthLevel AuthLevel;
        public readonly string Host;
        public readonly string Ident;
        public readonly string Message;
        public readonly string Nick;
        public readonly string Source;

        public OnCommandArgs(AuthLevel authLevel, string source, string host, string ident, string message, string nick){
            AuthLevel = authLevel;
            Source = source;
            Host = host;
            Ident = ident;
            Message = message;
            Nick = nick;
        }
    }
}