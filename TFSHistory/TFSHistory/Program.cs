using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFSHistory
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
			Properties.Settings.Default.Upgrade();
			Properties.Settings.Default.Reload();

			if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());
            }
            else
            {
                AttachConsole(ATTACH_PARENT_PROCESS);
                HandleCLIMode(args);
            }

        }

        private static void HandleCLIMode(string[] args)
        {
            try
            {
                var request = ParseArgs(args);
				if (request != null)
				{
					var response = ChangesetManager.GetChangesetHistory(request);

					Console.WriteLine("");
					//don't write dates unless there's a range
					var dates = (request.FromDate.Year != 1970 && request.ToDate.Year != 2200) ?
						"from {request.FromDate.ToShortDateString()} to {request.ToDate.ToShortDateString()}" :
						"";
					//write header
					Console.WriteLine($"History for {request.ReleaseBranchUrl} {dates}");

					const string OUTPUTFORMATHEADER = "{0,-9} {1,-20} {2,-10}  {3}";
					Console.WriteLine(OUTPUTFORMATHEADER, "Changeset", "Author", "Check-In", "Comments");

					const string OUTPUTFORMAT = "{0,-9} {1,-20} {2:yyyy-MM-dd}  {3}";

					//write history
					foreach (var changeset in response)
					{
						Console.WriteLine(OUTPUTFORMAT, changeset.Changeset, changeset.Owner, changeset.CheckIn, changeset.Comment.Replace("\r", " ").Replace("\n", " "));
					}
				}
            }
            catch (ArgumentException aex)
            {
				Console.WriteLine($"Argument error: {aex.Message}");

                PrintUsage();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
			}
		}

        private static ChangesetHistoryRequest ParseArgs(string[] args)
        {
            ChangesetHistoryRequest request = new ChangesetHistoryRequest();

			//attempt to retrieve the TFS URL via the Workstation info, based on the "current Directory"
			//this is how TF.exe can connect to TFS without you having to specify the tfs url
			//user can still override this via the T parameter
			var wi = Workstation.Current.GetLocalWorkspaceInfo(Environment.CurrentDirectory);
			request.TFSUrl = wi?.ServerUri.AbsoluteUri;
			//set default branch to be based on the current directory
			//user can override with the -r switch
			request.ReleaseBranchUrl = "./";

			for (int index = 0; index < args.Length; index++)
            {
                switch (args[index])
                {
					case "-t":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -t is missing");
                        }
                        request.TFSUrl = StripQuotes(args[index]);
                        break;
                    case "-r":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -r is missing");
                        }
                        request.ReleaseBranchUrl = StripQuotes(args[index]);
                        break;
                    case "-from":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -from is missing");
                        }
						if (DateTime.TryParse(args[index], out DateTime fromDate))
						{
							request.FromDate = fromDate.AddDays(-1);
                        }
                        else
                        {
                            throw new ArgumentException("Value for argument -from is invalid");
                        }
                        break;
                    case "-to":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -to is missing");
                        }
                        if (DateTime.TryParse(args[index], out DateTime toDate))
                        {
                            request.ToDate = toDate;
						}
						else
                        {
                            throw new ArgumentException("Value for argument -from is invalid");
                        }
                        break;
                    case "-u":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -u is missing");
                        }
                        request.IgnoreFromUsersString = args[index];
                        break;
					case "+u":
						index++;
						if (args[index].Trim().StartsWith("+"))
						{
							throw new ArgumentException("Value for argument +u is missing");
						}
						request.IncludeFromUsersString = args[index];
						break;
					case "-?":
					case "-h":
						PrintUsage();
						request = null;
						break;
					default:
						break;
				}
			}

            return request;

        }


		private static string StripQuotes(string v)
		{
			v = v.Trim();
			v = v.Trim('"');
			v = v.Trim('\'');
			return v;
		}


		private static void PrintUsage()
        {
			Console.WriteLine(
@"

TFSChangeHistory (a TFS History search tool)

Usage:
UI:	 TFSChangeHistory.exe
CLI: TFSChangeHistory.exe [-t <TFS URL>] [-r <branch URL>] [-from <date>] [-to <date>] [-u <username>] [+u <username>]

where:

TFS URL         the url of the TFS collection to open
                (NOTE if you run this utility from a folder that's currently part of a TFS
                workspace, you do not need to specify this URL, it will be obtained 
                automatically)
BRANCH URL      the url of the branch to search
DATE            a date specification
                Providing both FROM and TO will locate all history between dates
                Providing a FROM will locate all history back to and including the date
                Providing a TO will locate all history up to and including the date
USERNAME        the TFS username (either short or displayname, or a partial name
                can be semicolon seperated for multiple names
                +u indicates users to include in search
                -u indicates users to exclude in search
");
		}
	}
}
