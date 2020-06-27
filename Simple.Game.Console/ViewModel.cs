using Simple.Game.Application;

namespace Simple.Game.Console
{
    internal class ViewModel
    {
        private GameApplicationService appService_;

        public ViewModel()
        {
            appService_ = new GameApplicationService();
        }

        public void GameStart()
        {
            appService_.Start();
        }
    }
}