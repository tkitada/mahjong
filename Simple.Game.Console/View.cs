namespace Simple.Game.Console
{
    internal class View
    {
        private static ViewModel vm_;

        private static void Main()
        {
            vm_ = new ViewModel();
            while (true)
            {
                var input = System.Console.ReadLine();
                if (input == "start")
                {
                    vm_.GameStart();
                }
            }
        }
    }
}