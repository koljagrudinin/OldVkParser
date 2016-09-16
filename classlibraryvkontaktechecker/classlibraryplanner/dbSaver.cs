using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibraryVkontakteChecker.Results;
using System.Data.Objects.DataClasses;
using System.Data.Objects.SqlClient;

namespace ClassLibraryPlanner
{
	/// <summary>
	/// Класс, сохраняющий значения в БД
	/// </summary>
	class dbSaver
	{
		/// <summary>
		/// Добавить новых пользователей в БД
		/// </summary>
		private void addNewUsers( string[] arrOfUsersFromRequest )
		{
			using ( var cont = new Model.ModelVkontakteContainer() )
			{
				var newUsers = arrOfUsersFromRequest.Except( cont.UsersНабор.Select( j => j.UserId ) );

				foreach ( var user in newUsers )
				{
					cont.UsersНабор.AddObject( new Model.Users { UserId = user } );
				}
				cont.SaveChanges();
			}
		}

		/// <summary>
		/// Сохранить списки в БД
		/// </summary>
		/// <param name="groupId">ИЛ группы</param>
		/// <param name="result">Результат</param>
		/// <param name="data">Время получения данных</param>
		public void SaveListsResultToDB( string groupId , ClassLibraryVkontakteChecker.Results.ListsResult result , DateTime data )
		{
			//добавляем новых пользователей из группы
			addNewUsers( result.UsersInGroup.Select( i => i.UserId ).ToArray() );

			//цикл добавления пользователей в БД
			foreach ( var post in result.Wall.PostsOnWall )//проходимся по посту
			{
				addNewUsers( new string[] { post.Author.UserId } );//добавляем новых пользователей из поста
				addNewUsers( post.UsersThatLikedThisPost.Select( i => i.UserId ).ToArray() );//добаляем новых пользователей из людей, которым понравился данный пост
				foreach ( var comment in post.CommentsOfPost )//проходимся по комментарию
				{
					addNewUsers( new string[] { comment.CommentAuthor.UserId } );//добавляем автора комментария
					addNewUsers( comment.UsersThatLikedThisComment.Select( i => i.UserId ).ToArray() );//добавляем пользователей, которым понравился данный коммент
				}
			}
			using ( var cont = new Model.ModelVkontakteContainer() )
			{
				Model.ListsResults listsResult = new Model.ListsResults();//создаем новый результат

				foreach ( var post in result.Wall.PostsOnWall )//проходимся по постам
				{
					var PostFromEnt = new Model.Posts//создаем новый пост
					{
						PostId = post.PID ,
						UserAuthor = cont.UsersНабор.First( i => i.UserId == post.Author.UserId ) ,
						ListsResults = listsResult ,
					};
					cont.UsersНабор.First( i => i.UserId == post.Author.UserId ).AuthorOfPosts.Add( PostFromEnt );
					listsResult.Posts.Add( PostFromEnt );//добавляем пост в список постов результата
					foreach ( var user in post.UsersThatLikedThisPost.Select( i => i.UserId ) )//добавляем пользователей
					{
						PostFromEnt.UsersLike.Add( cont.UsersНабор.First( i => i.UserId == user ) );
						cont.UsersНабор.First( i => i.UserId == user ).LikedPosts.Add( PostFromEnt );
					}
					foreach ( var comment in post.CommentsOfPost )
					{
						var CommentForAdd = new Model.Comments//создаем новый комментарий
						{
							CommentId = comment.CID ,
							UserAuthor = cont.UsersНабор.First( i => i.UserId == comment.CommentAuthor.UserId ) ,
							Posts = PostFromEnt ,
						};
						PostFromEnt.Comments.Add( CommentForAdd );//добавляем комментарий в пост
						cont.UsersНабор.First( i => i.UserId == comment.CommentAuthor.UserId ).AuthorOfComments.Add( CommentForAdd );//добавляем данные к автору комментария
						foreach ( var user in comment.UsersThatLikedThisComment.Select( i => i.UserId ) )//добавляем пользователей
						{
							CommentForAdd.UsersLike.Add( cont.UsersНабор.Single( i => i.UserId == user ) );
							cont.UsersНабор.First( i => i.UserId == user ).LikedComments.Add( CommentForAdd );
						}
					}
				}

				cont.OperationTableEnt.AddObject( new Model.Operations
				{
					isSuccess = true ,
					GroupId = groupId ,
					BasicMetricksResult = null ,
					ErrorCodes = null ,
					Time = data ,
					ListsResults = listsResult ,
				} );
				cont.SaveChanges();
			}

		}


		///// <summary>
		///// Сохраняет новый результат списка
		///// </summary>
		//public void SaveListsResultToDB(string group_id, ClassLibraryVkontakteChecker.Results.ListsResult result, DateTime data)
		//{
		//    addNewUsers(result.UsersInGroup.Select(i => i.UserId).ToArray());//добавляем новых пользователей в БД
		//    using (var cont = new Model.ModelVkontakteContainer())
		//    {

		//        //выбираем пользователей
		//        EntityCollection<Model.Users> arrOfUsers = new EntityCollection<Model.Users>();
		//        foreach (var user in result.UsersInGroup)
		//            arrOfUsers.Add(cont.UsersНабор.Single(i => i.UserId.Equals(user)));

		//        var ListsResultForReturn = new Model.ListsResults
		//        {
		//            //Posts = createEntityCollectionFromPosts//создаем коллекцию из постов
		//            //(
		//            //    result.Wall.PostsOnWall.Select
		//            //    (
		//            //        i => createNewPost//создаем пост
		//            //        (
		//            //            i.PID ,
		//            //            i.Author.UserId ,
		//            //            i.UsersThatLikedThisPost.Select( u => u.UserId ).ToArray() ,
		//            //            i.CommentsOfPost.Select
		//            //            (
		//            //                c => createNewComment//создаем новые комментарии
		//            //                (
		//            //                    c.CID ,
		//            //                    c.CommentAuthor.UserId ,
		//            //                    c.UsersThatLikedThisComment.Select
		//            //                    (
		//            //                        j => j.UserId
		//            //                    ).ToArray()
		//            //                )
		//            //            ).ToArray()
		//            //        )
		//            //    )
		//            //) ,
		//            //GroupMembers = arrOfUsers
		//        };
		//        //addDataToDB( group_id , ListsResultForReturn , null );
		//        cont.OperationTableEnt.AddObject(new Model.Operations
		//        {
		//            isSuccess = true,
		//            GroupId = group_id,
		//            ListsResults = ListsResultForReturn,
		//            Time = data,
		//            BasicMetricksResult = null
		//        });
		//        cont.SaveChanges();
		//    }
		//}

		/// <summary>
		/// Сохраняет новый результат базовых метрик
		/// </summary>
		public void SaveBasicMetricksToDB( string groupId , BasicMetricsResult result , DateTime data )
		{
			using ( var cont = new Model.ModelVkontakteContainer() )
			{
				Model.BasicMetricksResults basicResult = new Model.BasicMetricksResults
				{
					AudioCount = result.AudioCount.ToString() ,
					MembersCount = result.UsersCount.ToString() ,
					PostCount = result.PostsCount.ToString() ,
					VideoCount = result.VideoCount.ToString()
				};
				//addDataToDB( group_id , null , basicResult );
				cont.OperationTableEnt.AddObject( new Model.Operations
				{
					isSuccess = true ,
					GroupId = groupId ,
					BasicMetricksResult = basicResult ,
					ErrorCodes = null ,
					Time = data ,
					ListsResults = null
				} );
				cont.SaveChanges();
			}

		}

		/// <summary>
		/// Функция проверки, есть ли такой код ошибка в БД, если нет - добавляет
		/// </summary>
		void checkErrorStatesInDB( int errorId )
		{
			using ( var cont = new Model.ModelVkontakteContainer() )
			{
				if ( cont.ErrorCodeStatesEnt.Any( i => i.ErrorId == errorId ) )
					return;
				cont.ErrorCodeStatesEnt.AddObject( new Model.ErrorCodeStates { Description = "" , ErrorId = errorId } );
				cont.SaveChanges();
			}
		}

		void checkErrorInDB( int operationId , int errorId )
		{
			checkErrorStatesInDB( errorId );
			using ( var cont = new Model.ModelVkontakteContainer() )
			{
				if ( cont.ErrorCodesEnt.Any( i => i.MethodErrorId.Equals( operationId ) && i.ErrorCodeState.ErrorId.Equals( errorId ) ) )
					return;
				cont.ErrorCodesEnt.AddObject( new Model.ErrorCodes
				{
					Description = "" ,
					ErrorCodeState = cont.ErrorCodeStatesEnt.First( i => i.ErrorId == errorId ) ,
					MethodErrorId = operationId
				} );
				cont.SaveChanges();
			}
		}

		/// <summary>
		/// Записываем ошибку в базу данных
		/// </summary>
		public void SaveErrorToDB( string groupId , int errorId , int operationId , DateTime data )
		{
			checkErrorInDB( operationId , errorId );
			using ( var cont = new Model.ModelVkontakteContainer() )
			{
				cont.OperationTableEnt.AddObject( new Model.Operations
				{
					BasicMetricksResult = null ,
					ListsResults = null ,
					ErrorCodes = cont.ErrorCodesEnt.First( j => j.MethodErrorId.Equals( operationId ) && j.ErrorCodeState.ErrorId.Equals( errorId ) ) ,
					isSuccess = false ,
					GroupId = groupId ,
					Time = data ,
				} );
				cont.SaveChanges();
			}
		}
	}
}
