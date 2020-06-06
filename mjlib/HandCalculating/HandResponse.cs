using System.Collections.Generic;

namespace mjlib.HandCalculating
{
    internal class HandResponse
    {
        public Cost Cost { get; }
        public int Han { get; }
        public int Fu { get; }
        public IList<Yaku> YakuList { get; }
        public string Error { get; }
        public IList<FuDetail> FuDetailSet { get; }
    }
}