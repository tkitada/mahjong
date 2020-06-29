using Simple.Game.Application;

namespace Simple.Game.Console
{
    internal class ViewModel
    {
        private readonly GameApplicationService appService_;

        public ViewModel()
        {
            appService_ = new GameApplicationService();

            appService_.JoinEvent += (_, e) => System.Console.WriteLine($"{e.JoinReq.Name} joined.");
        }

        public void GameStart()
        {
            appService_.Start();
        }
    }
}