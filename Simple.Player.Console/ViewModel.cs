using Simple.Player.Application;

namespace Simple.Player.Console
{
    internal class ViewModel
    {
        private readonly PlayerApplicationService appService_;

        public ViewModel()
        {
            appService_ = new PlayerApplicationService("yamada");

            appService_.JoinEvent += (_, e) => System.Console.WriteLine($"id: {e.JoinRes.Id}");
            appService_.HandEvent += (_, e) => System.Console.WriteLine($"hand: {e.HandRes.Hand.ToOneLineString()}");
        }
    }
}