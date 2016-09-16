using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryVkontakteChecker.Results
{
	public class ListsResult
	{

		/// <summary>
		/// Идентификатор группы
		/// </summary>
		public string GroupId { get; set; }
		/// <summary>
		/// Список участников
		/// </summary>
		public Data.User[] UsersInGroup { get; set; }

		/// <summary>
		/// Список постов
		/// </summary>
		public Data.Wall Wall { get; set; }
	}
}
