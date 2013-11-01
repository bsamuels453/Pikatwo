namespace Pikatwo{
    internal interface IrcComponent{
        ClientInterface IrcClient { get; set; }
        void Update(long secsSinceStart);
    }
}