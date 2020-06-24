using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Models
{
   public class DahaiRequest
    {
        public int Index { get; set; }
        public bool IsRiichi { get; set; }

        public DahaiRequest(int index, bool isRiichi)
        {
            Index = index;
            IsRiichi = isRiichi;
        }
    }
}
