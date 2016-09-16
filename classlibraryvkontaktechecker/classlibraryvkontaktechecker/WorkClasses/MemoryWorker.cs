using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ClassLibraryVkontakteChecker.WorkClasses
{
	/// <summary>
	/// Класс, реализующий кеширование на диск
	/// </summary>
	class MemoryWorker
	{
		int i = 0;
		string fileName = "";

		public MemoryWorker( string fileName )
		{
			this.fileName = fileName;
		}
		/// <summary>
		/// Сохраняет данные из списка
		/// </summary>
		/// <param name="arrOfData"></param>
		/// <returns></returns>
		public void saveData( IEnumerable<object> arrOfData )
		{
			if ( i == 0 )
				i = 1;
			object[] arrOfObj = arrOfData.ToArray();
			string path = fileName + i + ".dat";
			using ( FileStream FS = new FileStream( path , FileMode.Create ) )
			{
				BinaryFormatter BF = new BinaryFormatter();
				BF.Serialize( FS , typeof( object[] ) );
				FS.Close();
			}
			i++;
		}

		/// <summary>
		/// Загружает данные
		/// </summary>
		/// <returns></returns>
		public object[] LoadData( )
		{
			if ( i == 0 )
			{
				return new List<object>().ToArray();
			}
			List<object> arrOfObjects = new List<object>();
			for ( int k = 1 ; k <= i ; k++ )
			{
				object[] tempList = null;
				string path = fileName + k + ".dat";
				using ( FileStream FS = new FileStream( path , FileMode.Open ) )
				{
					BinaryFormatter BF = new BinaryFormatter();
					tempList = (object[])BF.Deserialize( FS );
					FS.Close();
				}
				System.IO.File.Delete( path );
				arrOfObjects.AddRange( tempList.ToArray() );
			}
			return arrOfObjects.ToArray();
		}
	}
}
