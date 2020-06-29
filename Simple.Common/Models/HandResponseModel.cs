using System.Collections.Generic;

namespace Simple.Common.Models
{
    public class HandResponseModel
    {
        public CostModel Cost { get; set; }
        public int Han { get; set; }
        public int Fu { get; set; }
        public List<YakuModel> Yaku { get; set; }
        public string Error { get; set; }
        public List<FuDetailModel> FuDetailSet { get; set; }
    }

    public class CostModel
    {
        public int Main { get; set; }
        public int Additional { get; set; }
    }

    public class YakuModel
    {
        public int YakuId { get; set; }
        public int TenhouId { get; set; }
        public string Name { get; set; }
        public string Japanese { get; set; }
        public string English { get; set; }
        public int HanOpen { get; set; }
        public int HanClosed { get; set; }
        public bool IsYakuman { get; set; }
    }

    public class FuDetailModel
    {
        public int Fu { get; set; }
        public string Reason { get; set; }
    }
}