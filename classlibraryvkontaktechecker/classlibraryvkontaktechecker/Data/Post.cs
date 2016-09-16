using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryVkontakteChecker.Data
{
	/// <summary>
	/// Запись на стене
	/// </summary>
	public class Post
	{
		/// <summary>
		/// ИД записи
		/// </summary>
		public string PID { get; set; }
		/// <summary>
		/// Автор записи
		/// </summary>
		public User Author { get; set; }
		/// <summary>
		/// Массив комментариев записи
		/// </summary>
		public Comment[] CommentsOfPost { get; set; }
		/// <summary>
		/// Пользователи, которым понравился данный пост
		/// </summary>
		public User[] UsersThatLikedThisPost { get; set; }
		
		/// <summary>
		/// Количество пользователей, которым понравился данный пост
		/// </summary>
		public int LikesCount { get; set; }

		/// <summary>
		/// Количество комментариев
		/// </summary>
		public int CommentsCount { get; set; }
	}
}
