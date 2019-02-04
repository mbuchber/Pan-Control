using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace pan_control
{
	class Program
	{
		static void Main(string[] args)
		{
			if(args.Length!=4)
			{
				Console.WriteLine("Usage: pan-control IP_or_name user pass command");
				Console.WriteLine("where command is: powon, powoff");
				return;
			}

			using (var wb = new WebClient())
			{
				var data = new NameValueCollection();
				data["username"] = args[1];
				data["password"] = args[2];

				var url = "http://" + args[0] + "/cgi-bin";
				if (args[3].Equals("powon"))
					url += "/power_on.cgi";
				else if (args[3].Equals("powoff"))
					url += "/power_off.cgi";
				else
				{
					Console.WriteLine("Unknown command.");
					return;
				}
				wb.Credentials = new NetworkCredential(args[1],args[2]);
				var response = wb.DownloadData(url);
				string responseInString = Encoding.UTF8.GetString(response);
			}
		}
	}
}
