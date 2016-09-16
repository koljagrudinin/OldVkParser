using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using ClassLibraryVkontakteChecker.XmlData;

namespace ClassLibraryVkontakteChecker.WorkClasses
{
	/// <summary>
	/// Класс-получатель данных от одного пользователя и N групп
	/// </summary>
	class Wrapper
	{
		VkontakteWorker vkWorker = new VkontakteWorker();

		readonly string access_token;
		readonly string secret_code;
		/// <summary>
		/// Инициализация класса-робота для одного пользователя
		/// </summary>
		public Wrapper( string token , string secret )
		{
			this.access_token = token;
			this.secret_code = secret;
		}

		/// <summary>
		/// Получает список всех людей
		/// </summary>
		public Data.User[] getMembersFull( string groupId , bool needCheck = true )
		{
			int offset = 0 , count = 1000 , maxCount;
			if ( needCheck )
				groupId = getGroupId( groupId ).Substring( 0 );

			maxCount = int.Parse( getMembers( groupId , offset , 1 , false ).count );

			var arrOfUsers = new List<Data.User>();

			while ( offset < maxCount )
			{
				arrOfUsers.AddRange(
					getMembers( groupId , offset , count , false ).users.SelectMany( k => k.uid.Select( j => new Data.User { UserId = j.Value } ) ) );

				offset += count;
			}
			return arrOfUsers.ToArray();
		}

		/// <summary>
		/// Получает код ошибки контакта из полученных данных
		/// </summary>
		int getVkontakteCodeError( byte[] response )
		{
			var serializer = new XmlSerializer( typeof( XmlData.vkontakteError.error ) );
			var data = int.Parse( ( (XmlData.vkontakteError.error)serializer.Deserialize( new System.IO.MemoryStream( response ) ) ).error_code );
			return data;
		}

		/// <summary>
		/// Получает реальный ИД группы, а не название
		/// </summary>
		public string getGroupId( string groupId )
		{
			byte[] response = null;

			var serializer = new XmlSerializer( typeof( XmlData.groups_getById.response ) );

			var list = new List<string[]>();
			list.Add( new string[] { "gid" , groupId } );

			string url = VkontakteWorker.createUrl( "groups.getById.xml" , list , access_token , secret_code );

			response = vkWorker.getBytes( url );
			try
			{
				var data = (XmlData.groups_getById.response)serializer.Deserialize( new System.IO.MemoryStream( response ) );
				return data.group[0].gid;
			}
			catch
			{
				int vkontakteErrorId = getVkontakteCodeError( response );
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( true , vkontakteErrorId , false );
			}
		}

		/// <summary>
		/// Полностью получает содержимое стены
		/// </summary>
		public Data.Wall getWall( string group_id , bool needCheck = true )
		{
			// 1. получить посты
			// 2. получить комментарии к каждому посту
			// 3. получить лайки к каждому посту
			// 4. получить лайки к каждому комментарию

			if ( needCheck )
				group_id = getGroupId( group_id );

			// 1. получить посты
			var postIds = getWallFull( group_id , false ).SelectMany( i => i.post.Select( j => new ClassLibraryVkontakteChecker.Data.Post
			{
				PID = j.id ,
				Author = new ClassLibraryVkontakteChecker.Data.User
				{
					UserId = j.from_id ,
				} ,
				LikesCount = int.Parse( j.likes[0].count ) ,
				CommentsCount = int.Parse( j.comments[0].count )

			} ) ).ToArray();

			// 2. получить комментарии к каждому посту
			for ( int i = 0 ; i < postIds.Count() ; i++ )
			{
				if ( postIds[i].CommentsCount > 0 )
				{
					postIds[i].CommentsOfPost = getCommentsOfPostFull( group_id , postIds[i].PID , false ).SelectMany(
						n => n.comment.Select(
							j => new ClassLibraryVkontakteChecker.Data.Comment
							{
								CID = j.cid ,
								CommentAuthor = new ClassLibraryVkontakteChecker.Data.User
								{
									UserId = j.from_id
								} ,
								LikesCount = int.Parse( j.likes[0].count )

							} ) ).ToArray();
				}
				else
					postIds[i].CommentsOfPost = new Data.Comment[0];
			}

			// 3. получить лайки к каждому посту
			for ( int i = 0 ; i < postIds.Count() ; i++ )
			{
				if ( postIds[i].LikesCount > 0 )
				{
					postIds[i].UsersThatLikedThisPost = getLikesOfPostFull( group_id , postIds[i].PID , false ).SelectMany(
						q => q.users.SelectMany(
							w => w.uid.Select(
								e => new ClassLibraryVkontakteChecker.Data.User
								{
									UserId = e.Value
								} ) ) ).ToArray();
				}
				else
				{
					postIds[i].UsersThatLikedThisPost = new Data.User[0];
				}
			}

			// 4. получить лайки к каждому комментарию
			for ( int i = 0 ; i < postIds.Count() ; i++ )
			{
				for ( int j = 0 ; j < postIds[i].CommentsOfPost.Count() ; j++ )
				{
					if ( postIds[i].CommentsOfPost[j].LikesCount > 0 )
					{
						postIds[i].CommentsOfPost[j].UsersThatLikedThisComment =
						getLikesOfCommentFull( group_id , postIds[i].PID , postIds[i].CommentsOfPost[j].CID , false ).SelectMany(
							q => q.users.SelectMany(
								w => w.uid.Select(
									e => new ClassLibraryVkontakteChecker.Data.User
									{
										UserId = e.Value
									} ) ) ).ToArray();
					}
					else
					{
						postIds[i].CommentsOfPost[j].UsersThatLikedThisComment = new Data.User[0];
					}
				}
			}
			// 5. вернуть данные
			return new Data.Wall
			{
				GroupId = group_id ,
				PostsOnWall = postIds
			};
		}

		/// <summary>
		/// Получить количество музыки		
		/// </summary>
		public string getAudioCount( string group_id , bool needCheck = true )
		{
			byte[] response = null;
			if ( needCheck )
				group_id = getGroupId( group_id ).Substring( 0 );

			var list = new List<string[]>();
			list.Add( new string[] { "oid" , "-" + group_id } );

			string url = VkontakteWorker.createUrl( "audio.getCount.xml" , list , access_token , secret_code );

			response = vkWorker.getBytes( url );
			///нормально десериализовать не получилось, потому пришлось работать со строкой

			string dataString = Encoding.UTF8.GetString( response );
			dataString = System.Text.RegularExpressions.Regex.Match( dataString , "<response>[0-9]*</response>" ).Value;
			dataString = dataString.Replace( "<response>" , "" );
			dataString = dataString.Replace( "</response>" , "" );
			return dataString;
		}

		/// <summary>		
		/// Получить участников группы	
		/// </summary>
		public group_getMembers.response getMembers( string group_id , int offset , int count = 1000 , bool needCheck = true )
		{
			if ( needCheck )
				group_id = getGroupId( group_id ).Substring( 0 );

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( typeof( XmlData.group_getMembers.response ) );

			var list = new List<string[]>();
			list.Add( new string[] { "gid" , group_id } );
			list.Add( new string[] { "count" , count.ToString() } );
			list.Add( new string[] { "offset" , offset.ToString() } );

			string url = VkontakteWorker.createUrl( "groups.getMembers.xml" , list , access_token , secret_code );

			byte[] response = vkWorker.getBytes( url );

			try
			{
				var data = (XmlData.group_getMembers.response)serializer.Deserialize( new System.IO.MemoryStream( response ) );
				return data;
			}
			catch//ошибка распознавания
			{
				int contactErrorId = getVkontakteCodeError( response );
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( true , contactErrorId , false );
			}
		}

		/// <summary>		
		/// Получить первое видео		
		/// </summary>
		public video_get.response getVideos( string group_id , bool needCheck = true )
		{
			//http://api.vk.com/method/video.get.xml?count=1&gid=habr&access_token=9582336595c04d1195c04d114195ebe993895c095d56d0ff6feb34316669cdb&sig=7765616bae38734e6bff50bc299fd227
			if ( needCheck )
				group_id = getGroupId( group_id );


			var serializer = new XmlSerializer( typeof( XmlData.video_get.response ) );

			var list = new List<string[]>();
			list.Add( new string[] { "gid" , "-" + group_id } );
			list.Add( new string[] { "count" , "1" } );
			list.Add( new string[] { "offset" , "0" } );

			string url = VkontakteWorker.createUrl( "video.get.xml" , list , access_token , secret_code );

			System.IO.File.WriteAllText( "c:\\cmdfiles\\svn\\url.txt" , url );


			byte[] response = vkWorker.getBytes( url );
			try
			{
				var data = (XmlData.video_get.response)serializer.Deserialize( new System.IO.MemoryStream( response ) );
				return data;
			}
			catch//ошибка распознавания
			{
				int contactErrorId = getVkontakteCodeError( response );
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( true , contactErrorId , false );
			}
		}

		/// <summary>		
		/// Получить посты со стены		
		/// </summary>
		public wall_get.response getWall( string group_id , int offset , int count = 100 , bool needCheck = true )
		{
			//http://api.vk.com/method/wall.get.xml?owner_id=-20629724&access_token=9582336595c04d1195c04d114195ebe993895c095d56d0ff6feb34316669cdb&sig=4959c721a614aa493503525ab15f2cc8

			if ( needCheck )
				group_id = getGroupId( group_id );

			var serializer = new XmlSerializer( typeof( XmlData.wall_get.response ) );

			var list = new List<string[]>();
			list.Add( new string[] { "owner_id" , "-" + group_id } );
			list.Add( new string[] { "count" , count.ToString() } );
			list.Add( new string[] { "offset" , offset.ToString() } );

			string url = VkontakteWorker.createUrl( "wall.get.xml" , list , access_token , secret_code );

			byte[] response = vkWorker.getBytes( url );
			try
			{
				var data = (XmlData.wall_get.response)serializer.Deserialize( new System.IO.MemoryStream( response ) );
				return data;
			}
			catch//ошибка распознавания
			{
				int contactErrorId = getVkontakteCodeError( response );
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( true , contactErrorId , false );
			}
		}

		/// <summary>
		/// Получить полный список сообщений
		/// </summary>
		public wall_get.response[] getWallFull( string groupId , bool needCheck = true )
		{
			int offset = 0 , count = 100 , maxCount;
			if ( needCheck )
				groupId = getGroupId( groupId );

			maxCount = int.Parse( getWall( groupId , offset , 1 , false ).count );

			List<XmlData.wall_get.response> arrOfPosts = new List<XmlData.wall_get.response>();

			while ( offset < maxCount )
			{
				arrOfPosts.Add( getWall( groupId , offset , count , false ) );
				offset += count;
			}
			return arrOfPosts.ToArray();
		}

		/// <summary>		
		/// Получить лайки от комментария		
		/// </summary>
		public wall_getLikesOfComment.response getLikesOfComment( string group_id , string postId , string commentId , int offset , int count = 1000 , bool needCheck = true )
		{
			//* списки лайков к каждому из комментариев к (id пользователей оставивших их)	
			//    http://api.vk.com/method/likes.getList.xml?item_id=105812&owner_id=-20629724&post_id=105809&type=comment&access_token=9582336595c04d1195c04d114195ebe993895c095d56d0ff6feb34316669cdb&sig=29a3ed59d7609a278df8d61b9a32abf9

			if ( needCheck )
				group_id = getGroupId( group_id );

			string funcName = "likes.getList.xml";
			var arrOfParams = new List<string[]>();
			arrOfParams.Add( new string[] { "item_id" , commentId } );
			arrOfParams.Add( new string[] { "post_id" , postId } );
			arrOfParams.Add( new string[] { "owner_id" , "-" + group_id } );
			arrOfParams.Add( new string[] { "type" , "comment" } );
			arrOfParams.Add( new string[] { "count" , count.ToString() } );
			arrOfParams.Add( new string[] { "offset" , offset.ToString() } );

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( typeof( XmlData.wall_getLikesOfComment.response ) );

			string url = VkontakteWorker.createUrl( funcName , arrOfParams , access_token , secret_code );

			//System.IO.File.WriteAllText( "c:\\cmdfiles\\svn\\url.txt" , url );

			byte[] response = vkWorker.getBytes( url );
			try
			{
				var data = (XmlData.wall_getLikesOfComment.response)serializer.Deserialize( new System.IO.MemoryStream( response ) );

				return data;
			}
			catch//ошибка распознавания
			{
				int contactErrorId = getVkontakteCodeError( response );
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( true , contactErrorId , false );
			}
		}

		/// <summary>
		/// Получить полный список лайков к комментарию
		/// </summary>
		public wall_getLikesOfComment.response[] getLikesOfCommentFull( string groupId , string postId , string commentId , bool needCheck = true )
		{
			if ( needCheck )
				groupId = getGroupId( groupId );

			int offset = 0 , count = 100 , maxCount;
			maxCount = int.Parse( getLikesOfComment( groupId , postId , commentId , offset , 1 , false ).count );

			var arrOfPosts = new List<wall_getLikesOfComment.response>();

			while ( offset < maxCount )
			{
				arrOfPosts.Add( getLikesOfComment( groupId , postId , commentId , offset , count , false ) );

				offset += count;
			}
			return arrOfPosts.ToArray();
		}

		/// <summary>		
		/// Получить пользователей, лайкнувших пост		
		/// </summary>
		public wall_getLikesOfPost.response getLikesOfPost( string group_id , string postId , int offset , int count = 1000 , bool needCheck = true )
		{
			//* списки лайков к каждому из постов (id пользователей оставивших их)	
			//	http://api.vk.com/method/likes.getList.xml?item_id=105809&owner_id=-20629724&post_id=105809&type=post&access_token=9582336595c04d1195c04d114195ebe993895c095d56d0ff6feb34316669cdb&sig=c55875bb75a3d49b6234080897485029

			if ( needCheck )
				group_id = getGroupId( group_id );

			string funcName = "likes.getList.xml";
			var arrOfParams = new List<string[]>();
			arrOfParams.Add( new string[] { "item_id" , postId } );
			arrOfParams.Add( new string[] { "post_id" , postId } );
			arrOfParams.Add( new string[] { "owner_id" , "-" + group_id } );
			arrOfParams.Add( new string[] { "type" , "post" } );
			arrOfParams.Add( new string[] { "count" , count.ToString() } );
			arrOfParams.Add( new string[] { "offset" , offset.ToString() } );

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( typeof( XmlData.wall_getLikesOfPost.response ) );

			string url = VkontakteWorker.createUrl( funcName , arrOfParams , access_token , secret_code );

			//			System.IO.File.WriteAllText( "c:\\cmdfiles\\svn\\url.txt" , url );

			byte[] response = vkWorker.getBytes( url );
			try
			{
				var data = (XmlData.wall_getLikesOfPost.response)serializer.Deserialize( new System.IO.MemoryStream( response ) );

				return data;
			}
			catch//ошибка распознавания
			{
				int contactErrorId = getVkontakteCodeError( response );
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( true , contactErrorId , false );
			}

		}

		/// <summary>
		/// Получить полный список пользователей, лайкнувших пост
		/// </summary>
		public wall_getLikesOfPost.response[] getLikesOfPostFull( string groupId , string postId , bool needCheck = true )
		{
			if ( needCheck )
				groupId = getGroupId( groupId );

			string type = "post";
			int offset = 0 , count = 1000 , maxCount;
			maxCount = int.Parse( getLikesOfPost( groupId , postId , offset , 1 , false ).count );

			List<wall_getLikesOfPost.response> arrOfPosts = new List<wall_getLikesOfPost.response>();

			while ( offset < maxCount )
			{
				arrOfPosts.Add( getLikesOfPost( groupId , postId , offset , count , false ) );
				offset += count;
			}
			return arrOfPosts.ToArray();
		}


		/// <summary>		
		/// Получить список людей, прокомментировавших данный пост		
		/// </summary>
		public wall_getPostComments.response getCommentsOfPost( string group_id , string postId , int offset , int count = 100 , bool needCheck = true )
		{
			if ( needCheck )
				group_id = getGroupId( group_id );

			//* списки комментариев к каждому из постов (id пользователей оставивших их)	
			//    http://api.vk.com/method/wall.getComments.xml?owner_id=-20629724&post_id=105809&access_token=9582336595c04d1195c04d114195ebe993895c095d56d0ff6feb34316669cdb&sig=aacb217ff3e3fac2a68c874a8b1172ff
			string funcName = "wall.getComments.xml";
			var arrOfParams = new List<string[]>();
			arrOfParams.Add( new string[] { "post_id" , postId } );
			arrOfParams.Add( new string[] { "owner_id" , "-" + group_id } );
			arrOfParams.Add( new string[] { "count" , count.ToString() } );
			arrOfParams.Add( new string[] { "offset" , offset.ToString() } );
			arrOfParams.Add( new string[] { "preview_length" , "1" } );
			arrOfParams.Add( new string[] { "need_likes" , "1" } );

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( typeof( XmlData.wall_getPostComments.response ) );

			string url = VkontakteWorker.createUrl( funcName , arrOfParams , access_token , secret_code );

			//System.IO.File.WriteAllText( "c:\\cmdfiles\\svn\\url.txt" , url );

			byte[] response = vkWorker.getBytes( url );
			//	System.IO.File.WriteAllText( "c:\\cmdfiles\\svn\\data.xml" , Encoding.UTF8.GetString( response ) );
			try
			{
				var data = (XmlData.wall_getPostComments.response)serializer.Deserialize( new System.IO.MemoryStream( response ) );
				return data;
			}
			catch//ошибка распознавания
			{
				int contactErrorId = getVkontakteCodeError( response );
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( true , contactErrorId , false );
			}
		}

		/// <summary>
		/// Получить полный список комментариев к посту
		/// </summary>
		public wall_getPostComments.response[] getCommentsOfPostFull( string groupId , string postId , bool needCheck = true )
		{
			if ( needCheck )
				groupId = getGroupId( groupId );

			int offset = 0 , count = 100 , maxCount;
			maxCount = int.Parse( getCommentsOfPost( groupId , postId , offset , 1 , false ).count );

			List<XmlData.wall_getPostComments.response> arrOfPosts = new List<wall_getPostComments.response>();

			while ( offset < maxCount )
			{
				arrOfPosts.Add( getCommentsOfPost( groupId , postId , offset , count , false ) );

				offset += count;
			}
			return arrOfPosts.ToArray();
		}


	}
}
