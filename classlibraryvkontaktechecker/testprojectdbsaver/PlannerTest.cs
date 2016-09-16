using ClassLibraryPlanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProjectDbSaver
{
    
    
    /// <summary>
    ///Это класс теста для PlannerTest, в котором должны
    ///находиться все модульные тесты PlannerTest
    ///</summary>
	[TestClass()]
	public class PlannerTest
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


		/// <summary>
		///Тест для Start
		///</summary>
		[TestMethod()]
		public void StartTest( )
		{
			Planner target = new Planner(); // TODO: инициализация подходящего значения
			target.Start();
			Assert.Inconclusive( "Невозможно проверить метод, не возвращающий значение." );
		}

		/// <summary>
		///Тест для Конструктор Planner
		///</summary>
		[TestMethod()]
		public void PlannerConstructorTest( )
		{
			Planner target = new Planner();
			Assert.Inconclusive( "TODO: реализуйте код для проверки целевого объекта" );
		}

		/// <summary>
		///Тест для updateOptionsTimer
		///</summary>
		[TestMethod()]
		public void updateOptionsTimerTest( )
		{
			Planner target = new Planner(); // TODO: инициализация подходящего значения
			target.updateOptionsTimer();
			//Assert.Inconclusive( "Невозможно проверить метод, не возвращающий значение." );
		}
	}
}
