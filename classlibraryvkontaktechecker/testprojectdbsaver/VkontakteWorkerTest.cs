using ClassLibraryVkontakteChecker.WorkClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectDbSaver
{


	/// <summary>
	///Это класс теста для VkontakteWorkerTest, в котором должны
	///находиться все модульные тесты VkontakteWorkerTest
	///</summary>
	[TestClass()]
	public class VkontakteWorkerTest
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

		string url = "http://api.vk.com/method/groups.getById.xml?gid=habr&access_token=1556d0161514ae621514ae6251153f0ae00151415018e7c319fc853cde869de&sig=42b7ee90705d081cd5290a363d5cb8ce";

		/// <summary>
		///Тест для getBytesNative
		///</summary>
		[TestMethod()]//работает
		[DeploymentItem("ClassLibraryVkontakteChecker.dll")]
		public void getBytesNativeTest()
		{
			VkontakteWorker_Accessor target = new VkontakteWorker_Accessor(); // TODO: инициализация подходящего значения
			byte[] expected = null; // TODO: инициализация подходящего значения
			byte[] actual;
			actual = target.getBytesNative(url);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Проверьте правильность этого метода теста.");
		}

		/// <summary>
		///Тест для getBytes
		///</summary>
		[TestMethod()]//работает
		public void getBytesTest()
		{
			VkontakteWorker target = new VkontakteWorker(); // TODO: инициализация подходящего значения
			byte[] actual = target.getBytes(url);
			System.IO.File.WriteAllText("d:\\cmdfiles\\svn\\request.txt",Encoding.UTF8.GetString(actual));
			//Assert.Inconclusive(Encoding.UTF8.GetString(actual));
		}

		/// <summary>
		///Тест для createUrl
		///</summary>
		[TestMethod()]//работает
		public void createUrlTest()
		{
			string methodName = "groups.getById.xml"; // TODO: инициализация подходящего значения
			List<string[]> NamesOfParamsWithNames = new List<string[]>(); // TODO: инициализация подходящего значения
			NamesOfParamsWithNames.Add(new string[] { "gid", "habr" });

			string accessTokenWithoutName = "1556d0161514ae621514ae6251153f0ae00151415018e7c319fc853cde869de"; // TODO: инициализация подходящего значения
			string secretKey = "9c5ea43c0b38712d6e"; // TODO: инициализация подходящего значения
			string actual = VkontakteWorker.createUrl(methodName, NamesOfParamsWithNames, accessTokenWithoutName, secretKey);
			
			Assert.Inconclusive(actual);
		}
	}
}
