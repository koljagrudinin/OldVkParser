using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryVkontakteChecker.Data
{
	/// <summary>
	/// Комментарий
	/// </summary>
	public class Comment
	{
		/// <summary>
		/// ИД комментария
		/// </summary>
		public string CID;
		/// <summary>
		/// Пользователь, написавший комментарий
		/// </summary>
		public User CommentAuthor;
		/// <summary>
		/// Массив пользователей, которым понравился данный комментарий
		/// </summary>
		public User[] UsersThatLikedThisComment { get; set; }

		/// <summary>
		/// Количество пользователей, которым понравился данный пост
		/// </summary>
		public int LikesCount { get; set; }
	}
}
