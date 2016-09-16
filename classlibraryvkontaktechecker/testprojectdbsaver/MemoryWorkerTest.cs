using ClassLibraryVkontakteChecker.WorkClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectDbSaver
{


	/// <summary>
	///Это класс теста для MemoryWorkerTest, в котором должны
	///находиться все модульные тесты MemoryWorkerTest
	///</summary>
	[TestClass()]
	public class MemoryWorkerTest
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

		string fileName = "C:\\cmdfiles\\SVN\\vkontakte-robot\\ClassLibraryVkontakteChecker\\TestProjectDbSaver\\bin\\strings.dat";

		/// <summary>
		///Тест для LoadData
		///</summary>
		[TestMethod()]//работает
		public void LoadDataTest( )
		{
			MemoryWorker target = new MemoryWorker( fileName ); // TODO: инициализация подходящего значения
			var expected = new string[] { "123" , "234" , "345" }; // TODO: инициализация подходящего значения
			var actual = (object[])( target.LoadData() );

			for ( int i = 0 ; i < actual.Count() ; i++ )
			{
				for ( int j = 0 ; j < ( (string[])actual[i] ).Count() ; j++ )
				{
					if ( !expected[j].Equals( ( (string[])actual[i] )[j] ) )
						Assert.Fail( "Несоответствие значений" );
				}
			}
			//Assert.Inconclusive( "Проверьте правильность этого метода теста." );
		}

		/// <summary>
		///Тест для saveData
		///</summary>
		[TestMethod()]//рабочий
		public void saveDataTest( )
		{
			MemoryWorker target = new MemoryWorker( fileName ); // TODO: инициализация подходящего значения
			IEnumerable<object> arrOfData = new object[] { new string[] { "123" , "234" , "345" } }; // TODO: инициализация подходящего значения
			target.saveData( arrOfData );
			//Assert.Inconclusive("Невозможно проверить метод, не возвращающий значение.");
		}
	}
}
