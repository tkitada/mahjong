using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Models
{
  public  class HandNotification
    {
        public TileIds Hand { get; set; }

        public HandNotification(TileIds hand)
        {
            Hand = hand;
        }
    }
}
