using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryVkontakteChecker.Data
{
	public class Wall
	{
		/// <summary>
		/// ИД группы
		/// </summary>
		public string GroupId { get; set; }

		/// <summary>
		/// Массив записей на стене
		/// </summary>
		public Post[] PostsOnWall { get; set; }
	}
}
