using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFSChangeHistory
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
            if (args == null)
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

					const string OUTPUTFORMATHEADER = "{0,-9} {1,-20} {2,10}  {3}";
					Console.WriteLine(OUTPUTFORMATHEADER, "Changeset", "Author", "Check-In", "Comments");

					const string OUTPUTFORMAT = "{0,-9} {1,-20} {2:yyyy-MM-dd}  {3}";
					foreach (var changeset in response)
					{
						Console.WriteLine(OUTPUTFORMAT, changeset.ChangesetId, changeset.Owner, changeset.CheckInDateTime, changeset.Comment.Replace("\r", " ").Replace("\n", " "));
					}
				}
            }
            catch (ArgumentException aex)
            {
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
                        request.TFSUrl = args[index];
                        break;
                    case "-r":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -r is missing");
                        }
                        request.ReleaseBranchUrl = args[index];
                        break;
                    case "-from":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -from is missing");
                        }
						if (DateTime.TryParse(args[index], out DateTime fromDate))
						{
							request.FromDate = fromDate;
							if (request.ToDate == DateTime.MinValue) request.ToDate = new DateTime(2100, 1, 1); ;
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
                    case "-i":
                        index++;
                        if (args[index].Trim().StartsWith("-"))
                        {
                            throw new ArgumentException("Value for argument -i is missing");
                        }
                        request.IgnoreFromUsersString = args[index];
                        break;
					case "-?":
					case "-h":
						PrintUsage();
						request = null;
						break;
				}
			}

            return request;

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
BRANCH URL      the url of the branch to search
DATE            a date specification
                Providing both FROM and TO will locate all history between dates
                Providing a FROM will locate all history back to and including the date
                Providing a TO will locate all history up to and including the date
USERNAME        the TFS username (either short or displayname, or a partial name
                can be comma seperated for multiple names
                +u indicates users to include in search
                -u indicates users to exclude in search
");
		}
	}
}
