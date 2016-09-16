using ClassLibraryVkontakteChecker.WorkClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibraryVkontakteChecker.XmlData;
using ClassLibraryVkontakteChecker.Data;

using System.Linq;

namespace TestProjectDbSaver
{


	/// <summary>
	///Это класс теста для WrapperTest, в котором должны
	///находиться все модульные тесты WrapperTest
	///</summary>
	[TestClass()]
	public class WrapperTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Получает или устанавливает контекст теста, в котором предоставляются
		///сведения о текущем тестовом запуске и обеспечивается его функциональность.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Дополнительные атрибуты теста
		// 
		//При написании тестов можно использовать следующие дополнительные атрибуты:
		//
		//ClassInitialize используется для выполнения кода до запуска первого теста в классе
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//TestInitialize используется для выполнения кода перед запуском каждого теста
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//TestCleanup используется для выполнения кода после завершения каждого теста
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		string token = "401d5399405f2ded405f2dedee4074896f5405f404a0df305b7e85b7a014e61";
		string secret = "5d52bc6277d6ccb029";

		//string group_id = "hotpsy";

		string group_id = "club24309268";

		int offset = 0; // TODO: инициализация подходящего значения
		int count = 1; // TODO: инициализация подходящего значения

		string postId = "5359";

		string commentId = "5362";

		/// <summary>
		///Тест для getLikesOfComment
		///</summary>
		[TestMethod()]//работает
		public void getLikesOfCommentTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_getLikesOfComment.response actual;
			actual = target.getLikesOfComment( group_id , postId , commentId , offset , 100 );
			Assert.Inconclusive( actual.count + " " + actual.users.SelectMany( i => i.uid.Select( j => j.Value ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getLikesOfCommentFull
		///</summary>
		[TestMethod()]//работает
		public void getLikesOfCommentFullTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_getLikesOfComment.response[] actual;
			actual = target.getLikesOfCommentFull( group_id , postId , commentId );
			Assert.Inconclusive( actual.SelectMany( i => i.users.SelectMany( j => j.uid.Select( k => k.Value ) ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getCommentsOfPost
		///</summary>
		[TestMethod()]//работает
		public void getCommentsOfPostTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_getPostComments.response actual;
			actual = target.getCommentsOfPost( group_id , postId , offset , 100 );
			Assert.Inconclusive( actual.count + actual.comment.Select( i => i.cid + ":" + i.likes.Select( d => d.count ).Aggregate( ( q , e ) => q + " " + e ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getCommentsOfPostFull
		///</summary>
		[TestMethod()]//работает
		public void getCommentsOfPostFullTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_getPostComments.response[] actual;
			actual = target.getCommentsOfPostFull( group_id , postId );
			Assert.Inconclusive( actual.SelectMany( i => i.comment.Select( j => j.cid + ":" + j.likes.Select( a => a.count ).Aggregate( ( a , q ) => a + " " + q ) ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getLikesOfPost
		///</summary>
		[TestMethod()]//работает
		public void getLikesOfPostTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_getLikesOfPost.response actual = target.getLikesOfPost( group_id , postId , offset , count );
			Assert.Inconclusive( actual.users.SelectMany( i => i.uid.Select( j => j.Value ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getLikesOfPostFull
		///</summary>
		[TestMethod()]//работает
		public void getLikesOfPostFullTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_getLikesOfPost.response[] actual;
			actual = target.getLikesOfPostFull( group_id , postId );
			Assert.Inconclusive( actual.SelectMany( i => i.users.SelectMany( j => j.uid.Select( q => q.Value ) ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getMembers
		///</summary>
		[TestMethod()]//работает
		public void getMembersTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			group_getMembers.response actual;
			actual = target.getMembers( group_id , offset , count );
			Assert.Inconclusive( actual.count.ToString() + " " + actual.users[0].uid.Select( i => i.Value ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getMembersFull
		///</summary>
		[TestMethod()]//работает
		public void getMembersFullTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			User[] actual;
			actual = target.getMembersFull( group_id );
			//	System.IO.File.WriteAllText( "C:\\cmdfiles\\SVN\\vkontakte-robot\\ClassLibraryVkontakteChecker\\TestProjectDbSaver\\bin\\members.txt" ,
			// actual.Select( j => j.UserId ).Aggregate( ( i , j ) => i + " " + j ) );
			Assert.Inconclusive( actual.Select( i => i.UserId ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getVideos
		///</summary>
		[TestMethod()]//работает
		public void getVideosTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			video_get.response actual;
			actual = target.getVideos( group_id );
			Assert.Inconclusive( actual.count );
		}

		/// <summary>
		///Тест для getWall
		///</summary>
		[TestMethod()]//работает
		public void getWallTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_get.response actual;
			actual = target.getWall( group_id , offset , 100 );
			Assert.Inconclusive( actual.count + " " + actual.post.Select( i => i.id + ":" + ( i.likes.Select( a => a.count ).Aggregate( ( a , q ) => a + " " + q ) ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}

		/// <summary>
		///Тест для getWallFull
		///</summary>
		[TestMethod()]//работает
		public void getWallFullTest( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			wall_get.response[] actual;
			actual = target.getWallFull( group_id );

			Assert.Inconclusive( actual.Select( i => i.post ).SelectMany( i => i.Select( h => h.id ) ).Aggregate( ( i , j ) => i + " " + j ) );
		}



		/// <summary>
		///Тест для getWall
		///</summary>
		[TestMethod()]
		public void getWallTest1( )
		{
			Wrapper target = new Wrapper( token , secret ); // TODO: инициализация подходящего значения
			Wall actual;
			actual = target.getWall( group_id );
			Assert.Inconclusive( actual.PostsOnWall.Count().ToString() );
		}
	}
}
