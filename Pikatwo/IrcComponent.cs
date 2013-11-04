namespace Pikatwo{
    internal interface IrcComponent{
        ClientInterface IrcInterface { get; set; }
        void Update(long secsSinceStart);
    }
}