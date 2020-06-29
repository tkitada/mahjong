namespace Simple.Player.Console
{
    internal class View
    {
        private static ViewModel vm_;

        private static void Main()
        {
            vm_ = new ViewModel();
            while (true)
            {
                _ = System.Console.ReadLine();
            }
        }
    }
}