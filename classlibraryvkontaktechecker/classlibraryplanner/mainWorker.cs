using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibraryVkontakteChecker;
using System.Threading.Tasks;
using ClassLibraryVkontakteChecker.Results;
using System.Threading;

namespace ClassLibraryPlanner
{
	public class Planner
	{
		/*mkk
		 Планировщик работает с таблицой конфига и запускает робота. 
	Если операция превышает таймаут, обрывает процесс.
Результаты операции планировщик 
	сохраняет в БД (таблица опреаций: время, успех, время сканирования и т.п. 
	и таблицы результатов: конкретные значения).
		 
		 */
		//
		/*
			Конфиг - это список групп и настройки их сканирования.
		 * Каждому пользователю можно прописать настройки
		 *	список групп, которые надо проверять
		 *	* Регулярность считывания базовых метрик
				* Таймаут считывания базовых метрик
				* Регулярность считывания списков
				* Таймаут считывания списков
		 */
		/// <summary>
		/// Список токенов
		/// </summary>
		List<tokenWithGroups> listOfTokens = new List<tokenWithGroups>();

		/// <summary>
		/// Класс, работающий с БД
		/// </summary>
		dbSaver dbSaver = new dbSaver();

		/// <summary>
		/// поток обновления данных
		/// </summary>
		System.Threading.Thread updateOptionsThread;

		public Planner( )
		{
			/**
			 * получить токены и группы связанные с ними
			 * создать объект для каждого токена
			 * запустить  таймеры для каждой группы в токене
			 *  когда таймер тикает - он начинает проверку
			 * 6. когда проверка заканчивается до таймаута - она бросает событие
			 * 7. подписчики события агрегируют данные и сохраняют их в БД
			 * */

			using ( var cont = new Model.ModelVkontakteContainer() )
			{
				cont.TokenListEnt.ToArray();
				foreach ( var token in cont.TokenListEnt.ToArray() )
				{
					listOfTokens.Add( new tokenWithGroups( token.Token , token.SecretKey , token.Id , dbSaver , token.Groups.ToArray() ) );
				}
			}
			updateOptionsThread = new Thread( updateOptionsTimer );
		}

		/// <summary>
		/// Запуск работы потоков
		/// </summary>
		public void Start( )
		{
			foreach ( var token in listOfTokens )
			{
				token.Start();
			}
			//	updateOptionsThread.Start();
			if ( updateOptionsThread.ThreadState != ThreadState.Aborted || updateOptionsThread.ThreadState != ThreadState.AbortRequested )
				updateOptionsThread.Abort();
			updateOptionsThread = new Thread( updateOptionsTimer );
			updateOptionsThread.Start();
		}


		/// <summary>
		/// Остановить работу потоков
		/// </summary>
		public void Stop( )
		{
			foreach ( var token in listOfTokens )
			{
				token.Stop();
			}
			updateOptionsThread.Abort();
		}

		/// <summary>
		/// Функция обновления данных у токенов
		/// </summary>
		public void updateOptionsTimer( )
		{
			while ( true )
			{
				System.Threading.Thread.Sleep( 20 * 60 * 1000 );//спим 20 минут
				using ( var cont = new Model.ModelVkontakteContainer() )
				{
					var newTokens = cont.TokenListEnt.Select( i => i.Id ).Except( listOfTokens.Select( j => j.tokenId ) ).Select( i => cont.TokenListEnt.FirstOrDefault( j => j.Id == i ) );
					var tokensForDelete = listOfTokens.Select( j => j.tokenId ).Except( cont.TokenListEnt.Select( i => i.Id ) );
					foreach ( var token in tokensForDelete )//удалняем несуществующие токены 
					{
						listOfTokens.Single( i => i.tokenId == token ).Stop();
						listOfTokens.Remove( listOfTokens.Single( i => i.tokenId == token ) );
					}

					foreach ( var token in listOfTokens )//обновляем настройки для существующих токенов
					{
						token.updateSettings( cont.TokenListEnt.Single( i => i.Id == token.tokenId ) );
					}

					foreach ( var token in newTokens )//добавляем еще не добавленные токены
					{
						listOfTokens.Add( new tokenWithGroups( token.Token , token.SecretKey , token.Id , dbSaver , token.Groups.ToArray() ) );
						listOfTokens.First( i => i.tokenId == token.Id ).Start();
					}
				}

			}
		}


	}
}
