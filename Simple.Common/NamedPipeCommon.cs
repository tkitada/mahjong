namespace Simple.Common
{
    public abstract class NamedPipeCommon
    {
        public string GameServerPipeName => "mahjong.Game.Pipe";
        public string PlayerServerPipeName => "mahjong.Player.Pipe";
    }
}