using mjlib.Tiles;
using SingleConsoleApp.Model.Domain;
using System.Collections.Generic;

namespace SingleConsoleApp.Model.ApplicationService
{
    public class SingleConsoleApplicationService
    {
        private readonly GameManager gameManager_ = new GameManager();
        private readonly TileCursor tileCursor_ = new TileCursor();

        public (TileId, Hand, List<int>, TileCursor) Update()
        {
            return (gameManager_.DoraIndicate, gameManager_.Hand, gameManager_.Discards, tileCursor_);
        }

        public void MoveCursor(Direction dir)
        {
            tileCursor_.Move(dir);
        }

        public void Select()
        {
            gameManager_.Dahai(tileCursor_.Position);
        }
    }
}