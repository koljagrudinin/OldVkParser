using ClassLibraryPlanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibraryVkontakteChecker.Results;
using ClassLibraryPlanner.Model;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;

namespace TestProjectDbSaver
{


	/// <summary>
	///Это класс теста для dbSaverTest, в котором должны
	///находиться все модульные тесты dbSaverTest
	///</summary>
	[TestClass()]
	public class dbSaverTest
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


		string groupId = "habr";

		DateTime data = DateTime.Now;

		/// <summary>
		///Тест для addNewUsers
		///</summary>
		[TestMethod()]//работает
		[DeploymentItem("ClassLibraryPlanner.dll")]
		public void addNewUsersTest()
		{
			dbSaver_Accessor target = new dbSaver_Accessor(); // TODO: инициализация подходящего значения
			string[] arrOfUsersFromRequest = new string[] { "01010101", "2323232323", "23345446577", "koljagrudinin" }; // TODO: инициализация подходящего значения
			target.addNewUsers(arrOfUsersFromRequest);
			//Assert.Inconclusive( "Невозможно проверить метод, не возвращающий значение." );
		}

		/// <summary>
		///Тест для SaveErrorToDB
		///</summary>
		[TestMethod()]//работает
		public void SaveErrorToDBTest()
		{
			dbSaver target = new dbSaver(); // TODO: инициализация подходящего значения
			int errorId = 5; // TODO: инициализация подходящего значения
			int operationId = 1; // TODO: инициализация подходящего значения
			target.SaveErrorToDB(groupId, errorId, operationId, data);
			//	Assert.Inconclusive( "Невозможно проверить метод, не возвращающий значение." );
		}

		/// <summary>
		///Тест для SaveListsResultToDBNew
		///</summary>
		[TestMethod()]//работает
		public void SaveListsResultToDBTest()
		{
			dbSaver target = new dbSaver(); // TODO: инициализация подходящего значения
			ListsResult result = new ListsResult
			{
				GroupId = groupId,
				UsersInGroup = new ClassLibraryVkontakteChecker.Data.User[] { new ClassLibraryVkontakteChecker.Data.User { UserId = "1231232342" } },
				Wall = new ClassLibraryVkontakteChecker.Data.Wall
				{
					GroupId = groupId,
					PostsOnWall = new ClassLibraryVkontakteChecker.Data.Post[] 
					{
						new ClassLibraryVkontakteChecker.Data.Post 
						{
							PID = "123123",
							Author = new ClassLibraryVkontakteChecker.Data.User 
							{
								UserId = "234234" 
							},
							UsersThatLikedThisPost = new ClassLibraryVkontakteChecker.Data.User[] { },
					CommentsOfPost = new ClassLibraryVkontakteChecker.Data.Comment[] {new ClassLibraryVkontakteChecker.Data.Comment{CID="234234",CommentAuthor=new ClassLibraryVkontakteChecker.Data.User{UserId="345345"},UsersThatLikedThisComment=new ClassLibraryVkontakteChecker.Data.User[]{new ClassLibraryVkontakteChecker.Data.User{UserId="345345"}} } } }}
				}
			}; // TODO: инициализация подходящего значения
			target.SaveListsResultToDB(groupId, result, data);
			//Assert.Inconclusive("Невозможно проверить метод, не возвращающий значение.");
		}

		/// <summary>
		///Тест для SaveBasicMetricksToDB
		///</summary>
		[TestMethod()]//работает
		public void SaveBasicMetricksToDBTest()
		{
			dbSaver target = new dbSaver(); // TODO: инициализация подходящего значения
			string groupId = "123123123"; // TODO: инициализация подходящего значения
			BasicMetricsResult result = new BasicMetricsResult
			{
				AudioCount = 1,
				groupId = groupId,
				PostsCount = 123,
				UsersCount = 123,
				VideoCount = 123
			}; // TODO: инициализация подходящего значения
			target.SaveBasicMetricksToDB(groupId, result, data);
			//Assert.Inconclusive("Невозможно проверить метод, не возвращающий значение.");
		}
	}
}
