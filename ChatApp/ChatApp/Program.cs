using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// !!!INFO!!!
		/// Ich habe bei der Hauptform bewusst auf MVC verzichtet, da ich an dieser Stelle durch den Einsatz diese Models
		/// keinen nennenswerten Vorteil erhalte und auf einige Probleme mit meinen Threads stoße. 
		/// Daher erfolgt der Programmaufbau auf der Form "ChatApp".
		/// MVC kommt bei den Clientfenstern allerdings zum Einsatz!
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ChatApp());
		}
	}
}
