using System;

namespace GameBox
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				GBInfo.WriteLine("Welcome to GameBox");
				GBInfo.WriteLine("Reading properties...");

				GBSystem.Init();
			} catch (Exception e)
			{
				Console.WriteLine(e);
			}

            GBDebug.Execute(Console.ReadKey());

		}
	}
}
