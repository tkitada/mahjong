namespace Simple.Game.Domain
{
    internal class ClientManager
    {
        private string name_;

        public void Join(string name)
        {
            name_ = name;
        }
    }
}