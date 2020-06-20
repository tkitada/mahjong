﻿using System.Collections.Generic;

namespace mjlib.HandCalculating
{
    public class HandResponse
    {
        public Cost Cost { get; }
        public int Han { get; }
        public int Fu { get; }
        public IList<Yaku> Yaku { get; }
        public string Error { get; }
        public IList<FuDetail> FuDetailSet { get; }

        public HandResponse(Cost cost = null,
            int han = 0,
            int fu = 0,
            IList<Yaku> yaku = null,
            string error = null,
            IList<FuDetail> fuDetailSet = null)
        {
            Cost = cost;
            Han = han;
            Fu = fu;
            Yaku = yaku;
            Error = error;
            FuDetailSet = fuDetailSet;
        }
    }
}