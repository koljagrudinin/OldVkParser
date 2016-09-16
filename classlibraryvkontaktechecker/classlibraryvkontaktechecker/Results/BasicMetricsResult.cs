using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryVkontakteChecker.Results
{
	public class BasicMetricsResult
	{
		/// <summary>
		/// Идентификатор группы
		/// </summary>
		public string groupId { get; set; }

		/// <summary>
		/// Количество пользователей в группе
		/// </summary>
		public int UsersCount { get; set; }
		/// <summary>
		/// Количество записей на стене
		/// </summary>
		public int PostsCount { get; set; }
		/// <summary>
		/// Количество видео в группе
		/// </summary>
		public int VideoCount { get; set; }
		/// <summary>
		/// Количество аудио в группе
		/// </summary>
		public int AudioCount { get; set; }
	}
}
