namespace SingleConsoleApp.Model.Domain
{
    public class TileCursor
    {
        public int Position { get; private set; }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    MoveLeft();
                    break;

                case Direction.Right:
                    MoveRight();
                    break;

                default:
                    break;
            }
        }

        private void MoveLeft()
        {
            if (Position == 0)
                Position = 13;
            else
                Position--;
        }

        private void MoveRight()
        {
            if (Position == 13)
                Position = 0;
            else
                Position++;
        }
    }

    public enum Direction
    {
        Left,
        Right,
    }
}