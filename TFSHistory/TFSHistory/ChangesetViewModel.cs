using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSHistory
{
    public class ChangesetViewModel
    {
		public int Changeset { get; set; }
		public DateTime CheckIn { get; set; }
		public string Owner { get; set; }
		public string Comment { get; set; }
	}
}
