namespace Pikatwo{
    internal class OnCommandArgs{
        public readonly AuthLevel AuthLevel;
        public readonly string From;
        public readonly string Host;
        public readonly string Ident;
        public readonly string Message;
        public readonly string Nick;

        public OnCommandArgs(AuthLevel authLevel, string @from, string host, string ident, string message, string nick){
            AuthLevel = authLevel;
            From = @from;
            Host = host;
            Ident = ident;
            Message = message;
            Nick = nick;
        }
    }
}