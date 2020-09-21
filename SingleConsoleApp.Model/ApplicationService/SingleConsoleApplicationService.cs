using mjlib.HandCalculating;
using mjlib.Tiles;
using SingleConsoleApp.Model.Domain;
using System.Collections.Generic;

namespace SingleConsoleApp.Model.ApplicationService
{
    public class SingleConsoleApplicationService
    {
        private GameManager gameManager_ = new GameManager();
        private TileCursor tileCursor_ = new TileCursor();

        private ModeType mode_ = ModeType.Normal;
        private ModeType preMode_ = ModeType.Normal;
        private HandResponse result_;

        public SingleConsoleApplicationService()
        {
            gameManager_.AgariEvent += (_, e) =>
            {
                preMode_ = mode_;
                mode_ = ModeType.ConfirmAgari;
                result_ = e.Result;
            };
            gameManager_.RyukyokuEvent += (_, __) =>
            {
                mode_ = ModeType.DisplayResult;
                result_ = null;
            };
        }

        public UpdateContainer Update()
        {
            if (mode_ == ModeType.Riichi)
            {
                gameManager_.Dahai(13, false);
            }
            return new UpdateContainer
            {
                DoraIndicators = gameManager_.DoraIndicators,
                Hand = gameManager_.Hand,
                Discards = gameManager_.Discards,
                Mode = mode_,
                Result = result_,
                TileCursor = tileCursor_
            };
        }

        public void MoveCursor(Direction dir)
        {
            tileCursor_.Move(dir);
        }

        public void Dahai(bool isRiichi)
        {
            gameManager_.Dahai(tileCursor_.Position, isRiichi);
            if (isRiichi)
            {
                mode_ = ModeType.Riichi;
            }
        }

        public void Agari()
        {
            mode_ = ModeType.DisplayResult;
        }

        public void CancelAgari()
        {
            mode_ = preMode_;
            result_ = null;
        }

        public void Reset()
        {
            gameManager_ = new GameManager();
            tileCursor_ = new TileCursor();
            mode_ = ModeType.Normal;
            preMode_ = ModeType.Normal;
            result_ = null;
        }
    }

    public class UpdateContainer
    {
        public TileIds DoraIndicators { get; internal set; }
        public Hand Hand { get; internal set; }
        public List<int> Discards { get; internal set; }
        public TileCursor TileCursor { get; internal set; }
        public HandResponse Result { get; internal set; }
        public ModeType Mode { get; internal set; }
    }
}