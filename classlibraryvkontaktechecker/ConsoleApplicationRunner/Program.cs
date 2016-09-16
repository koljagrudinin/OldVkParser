using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ClassLibraryVkontakteChecker.WorkClasses;

namespace ConsoleApplicationRunner
{
	class Program
	{
		static void func( )
		{
			var worker = new ClassLibraryPlanner.Planner();
			Console.WriteLine( "Планировщик создан" );
			worker.Start();
			Console.WriteLine( "Планировщик запущен" );
			Console.WriteLine( "Нажмите энтер для выхода" );
			Console.ReadLine();
			worker.Stop();
		}
		static void Main( string[] args )
		{
			//func();
			//2	955222e895105c9c95105c9c49953bf81e8951095057c8235d8dbf93e43d953	927aaaddb614f2f3f3	kaljan	System.Data.Objects.DataClasses.EntityCollection`1[ClassLibraryPlanner.Model.Groups]	Unchanged	System.Data.EntityKey

			string token = "955222e895105c9c95105c9c49953bf81e8951095057c8235d8dbf93e43d953";
			string secret = "927aaaddb614f2f3f3";

			ClassLibraryVkontakteChecker.WorkClasses.MainClass a = new MainClass( token , secret );
			//var data = a.getUserOfGroup( "club132265" );

			//using ( System.IO.StreamWriter writer = new System.IO.StreamWriter( "..\\data.txt" ) )
			//{
			//    foreach ( var str in data )
			//    {
			//        writer.WriteLine( str.UserId );
			//    }
			//    writer.Close();
			//}
			List<string> arrOfGroupsId = new List<string>();


			using ( var input = new System.IO.StreamReader( "..\\groups.txt" ) )
			{
				while ( !input.EndOfStream )
				{
					string groupId = input.ReadLine();
					arrOfGroupsId.Add( groupId );
				}
			}
			foreach ( var groupId in arrOfGroupsId )
			{
				//Console.WriteLine( groupId );
				//var data = a.getUserOfGroup( groupId );

				new System.Threading.Thread( runGetDataOfGroup ).Start( new ddd { vkWorker = a , groupId = groupId } );

				//runGetDataOfGroup( new ddd { vkWorker = a , groupId = groupId } );
				//using ( System.IO.StreamWriter writer = new System.IO.StreamWriter( groupId + ".txt" ) )
				//{
				//    foreach ( var str in data )
				//    {
				//        writer.WriteLine( str.UserId );
				//    }
				//    writer.Close();
				//}
			}

			
			
			
		}

		class ddd
		{
			public ClassLibraryVkontakteChecker.WorkClasses.MainClass vkWorker;
			public string groupId;
		}



		static void runGetDataOfGroup( object groupIdObject )
		{
			ClassLibraryVkontakteChecker.WorkClasses.MainClass a = ( (ddd)groupIdObject ).vkWorker;
			string groupId = ( (ddd)groupIdObject ).groupId;
			Console.WriteLine( groupId );
			try
			{
				var data = a.getUserOfGroup( groupId );
				using ( System.IO.StreamWriter writer = new System.IO.StreamWriter( groupId + ".txt" ) )
				{
					foreach ( var str in data )
					{
						writer.WriteLine( str.UserId );
					}
					writer.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine( "Системная ошибка обработки " + groupId +Environment.NewLine+ex.Message);
				return;
			}

		}


		
	}
}

