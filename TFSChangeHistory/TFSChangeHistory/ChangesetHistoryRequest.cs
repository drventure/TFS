using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSChangeHistory
{
    public class ChangesetHistoryRequest
    {
        public string TFSUrl { get; set; }
        public string ReleaseBranchUrl { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string IgnoreFromUsersString { get; set; }

        public string[] IgnoreFromUsers
        {
            get
            {
                return (IgnoreFromUsersString ?? "wsbuilduser").ToLower().Split(';');
            }
        }


		public string IncludeFromUsersString { get; set; }
		public string[] IncludeFromUsers
		{
			get
			{
				if (string.IsNullOrWhiteSpace(IncludeFromUsersString))
				{
					return new string[] { };
				}
				return IncludeFromUsersString.ToLower().Split(';');
			}
		}
	}
}
