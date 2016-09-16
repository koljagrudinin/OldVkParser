using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ClassLibraryPlanner
{
	/// <summary>
	/// Группа, для которой производится работа с метриками
	/// </summary>
	class group
	{
		public string groupId;

		ClassLibraryVkontakteChecker.WorkClasses.MainClass classChecker;

		int listsRegularTime;
		int basicRegularTime;

		int listsTimeOut;
		int basicTimeOut;

		System.Timers.Timer checkLists;
		System.Timers.Timer checkBasic;

		System.Threading.Thread getListsThread = null;
		System.Threading.Thread getBasicThread = null;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="groupId">ИД группы вконтакте</param>
		/// <param name="listsRegularTime">регулярность проверки списков</param>
		/// <param name="basicRegularTime">регулярность проверки базовых метрик</param>
		/// <param name="listsTimeOut">Таймер проверки списков</param>
		/// <param name="basicTimeOut">Тамйер проверки базовых метрик</param>
		/// <param name="vkWorker">класс, работающий с данными</param>
		public group( string groupId , int listsRegularTime , int basicRegularTime , int listsTimeOut , int basicTimeOut , ClassLibraryVkontakteChecker.WorkClasses.MainClass vkWorker )
		{
			this.groupId = groupId;

			this.classChecker = vkWorker;

			this.listsRegularTime = listsRegularTime;
			this.basicRegularTime = basicRegularTime;

			this.listsTimeOut = listsTimeOut;
			this.basicTimeOut = basicTimeOut;

			getBasicThread = new Thread( checkBasicFunc );
			getListsThread = new Thread( checkListsFunc );
			InitializeTimers();
			//StartTimers();
		}

		/// <summary>
		/// запускает таймеры
		/// </summary>
		public void Start( )
		{
			StartTimers();
		}

		/// <summary>
		/// Останавливает таймеры и потоки получения данных
		/// </summary>
		public void Stop( )
		{
			StopTimers();
			getListsThread.Abort();
			getBasicThread.Abort();
		}

		/// <summary>
		/// Инициализация таймеров
		/// </summary>
		void InitializeTimers( )
		{
			checkLists = new System.Timers.Timer();
			checkLists.Interval = listsRegularTime;
			checkLists.Elapsed += new System.Timers.ElapsedEventHandler( checkLists_Elapsed );
			checkLists.AutoReset = true;

			checkBasic = new System.Timers.Timer();
			checkBasic.Interval = basicRegularTime;
			checkBasic.Elapsed += new System.Timers.ElapsedEventHandler( checkBasic_Elapsed );
			checkBasic.AutoReset = true;
		}

		/// <summary>
		/// Запуск таймеров
		/// </summary>
		void StartTimers( )
		{
			checkLists.Start();
			checkBasic.Start();
		}

		/// <summary>
		/// Остановка таймеров
		/// </summary>
		void StopTimers( )
		{
			checkBasic.Stop();
			checkLists.Stop();
		}

		/// <summary>
		/// Задать интервалы таймеров
		/// </summary>
		void setTimerTimeIntervals( )
		{
			checkBasic.Interval = this.basicRegularTime;
			checkLists.Interval = this.listsRegularTime;
		}

		/// <summary>
		/// Есть ли измененные тайминги
		/// </summary>
		bool haveNewTimings( int ListsRegular , int BasicRegular , int ListsTimeOut , int BasicTimeOut )
		{
			if ( ListsRegular != this.listsRegularTime )
				return true;
			if ( BasicRegular != this.basicRegularTime )
				return true;
			if ( ListsTimeOut != this.listsTimeOut )
				return true;
			if ( BasicTimeOut != this.basicTimeOut )
				return true;
			return false;
		}

		public void setTimings( Model.Groups groupFromModel )
		{
			if ( haveNewTimings( groupFromModel.ListsRegulerReading , groupFromModel.BasicMetrickRegularReading , groupFromModel.ListsTimeOut , groupFromModel.BasicMetricksTimeOut ) )
			{
				StopTimers();
				this.groupId = groupFromModel.GroupId;

				this.basicRegularTime = groupFromModel.BasicMetrickRegularReading;
				this.listsRegularTime = groupFromModel.ListsRegulerReading;

				this.basicTimeOut = groupFromModel.BasicMetricksTimeOut;
				this.listsTimeOut = groupFromModel.ListsTimeOut;

				setTimerTimeIntervals();
				StartTimers();
			}
		}

		void checkBasic_Elapsed( object sender , System.Timers.ElapsedEventArgs e )
		{
			//проверяем базовые метрики, поднимаем событие

			if ( getBasicThread.ThreadState != ThreadState.Running ||
				getBasicThread.ThreadState != ThreadState.WaitSleepJoin )
			{
				getBasicThread = new Thread( checkBasicFunc );

				getBasicThread.Start();
				System.Threading.Thread.Sleep( basicTimeOut );
				getBasicThread.Abort();
				if ( getBasicThread.ThreadState == System.Threading.ThreadState.Aborted || getBasicThread.ThreadState == System.Threading.ThreadState.AbortRequested )
					classChecker.createTimeOutError( groupId , 2 , DateTime.Now );
			}
		}

		/// <summary>
		/// Функция-обертка для проверки базовых метрик
		/// </summary>
		void checkBasicFunc( )
		{
			classChecker.getBasicMetricks( groupId );
		}

		void checkLists_Elapsed( object sender , System.Timers.ElapsedEventArgs e )
		{
			//проверяем списки, поднимаем событие
			if ( getListsThread.ThreadState != ThreadState.Running ||
				getListsThread.ThreadState != ThreadState.WaitSleepJoin )
			{
				getListsThread = new Thread( checkListsFunc );
				getListsThread.Start();
				System.Threading.Thread.Sleep( listsTimeOut );
				getListsThread.Abort();
				if ( getListsThread.ThreadState == System.Threading.ThreadState.Aborted || getListsThread.ThreadState == System.Threading.ThreadState.AbortRequested )
					classChecker.createTimeOutError( groupId , 1 , DateTime.Now );

			}
		}

		/// <summary>
		/// Функция-обертка для получения списко
		/// </summary>
		void checkListsFunc( )
		{
			classChecker.getLists( groupId );
		}
	}

}
