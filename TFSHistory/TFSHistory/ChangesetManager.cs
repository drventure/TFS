using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSHistory
{
    public class ChangesetManager
    {
        public static List<ChangesetViewModel> GetChangesetHistory(ChangesetHistoryRequest request)
        { 
            List<ChangesetViewModel> changesetList = new List<ChangesetViewModel>();

			using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(request.TFSUrl)))
			{
				tpc.EnsureAuthenticated();
				VersionControlServer vcs = tpc.GetService<VersionControlServer>();

				VersionSpec fromDateVersion = null;
				VersionSpec toDateVersion = null;
				if (request.FromDate != DateTime.MinValue || request.ToDate != DateTime.MinValue)
				{
					fromDateVersion = new DateVersionSpec(request.FromDate);
					toDateVersion = new DateVersionSpec(request.ToDate);
				}

				IEnumerable changesets = vcs.QueryHistory(request.ReleaseBranchUrl, VersionSpec.Latest,
					0, RecursionType.Full, null, fromDateVersion, toDateVersion, int.MaxValue, true, true);

				//try to resolve the branch url to an mapped TFS path
				//if this doesn't work, just leave the branch URL as is
				//When user uses the current dir for the path, this will convert to a branch URL
				try
				{
					var ws = vcs.TryGetWorkspace(request.ReleaseBranchUrl);
					if (ws != null)
					{
						request.ReleaseBranchUrl = ws.GetWorkingFolderForLocalItem(request.ReleaseBranchUrl).DisplayServerItem;
					}
				}
				catch { }//safe to ignore any exception here


				var ignoreUsers = request.IgnoreFromUsers;
				var includeUsers = request.IncludeFromUsers;
				foreach (Changeset changeset in changesets)
				{
					//default to include if there are no users in the include list
					var add = includeUsers.Length == 0;
					if (includeUsers.Any(n => changeset.Owner.ToLower().Contains(n) || changeset.OwnerDisplayName.ToLower().Contains(n)))
					{
						//if there are include users and this is one, include it
						add = true;
					}

					if (ignoreUsers.Any(n => changeset.Owner.ToLower().Contains(n) || changeset.OwnerDisplayName.ToLower().Contains(n)))
					{
						//don't include if there are IgnoreUsers and this is one of them
						add = false;
					}


					if (add)
					{
						changesetList.Add(new ChangesetViewModel() { Changeset = changeset.ChangesetId, Owner = changeset.OwnerDisplayName ?? changeset.Owner, Comment = changeset.Comment, CheckIn = changeset.CreationDate });
					}
				}
			}
            return changesetList;
        }

        private static void ValidateRequest(ChangesetHistoryRequest request)
        {
            bool result = Uri.TryCreate(request.TFSUrl, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
            {
                throw new ArgumentException("TFS URL is not valid");
            }

            if (string.IsNullOrEmpty(request.ReleaseBranchUrl))
            {
                throw new ArgumentNullException("Release Branch Url cannot be null");
            }
        }
    }
}
