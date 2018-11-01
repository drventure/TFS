using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSHistory
{
    public class ChangesetViewModel
    {
        public string Comment { get; set; }
        public int ChangesetId { get; set; }
        public string Owner { get; set; }
        public DateTime CheckInDateTime { get; set; }
    }
}
