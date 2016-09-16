using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibraryVkontakteChecker.Results;

namespace ClassLibraryPlanner
{
	/// <summary>
	/// Класс-токен, в котором содержатся группы с их таймингами
	/// </summary>
	class tokenWithGroups
	{
		/// <summary>
		/// Класс, работающий с контактом
		/// </summary>
		ClassLibraryVkontakteChecker.WorkClasses.MainClass vkWorker;
		/// <summary>
		/// Список групп
		/// </summary>
		List<group> Groups = new List<group>();
		/// <summary>
		/// Сохраняльщик в БД
		/// </summary>
		dbSaver saver;

		/// <summary>
		/// ИД токена для обновления данных
		/// </summary>
		public int tokenId;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="token">токен доступа</param>
		/// <param name="secret">секретный ключ</param>
		/// <param name="tokenId">ид токена</param>
		/// <param name="saverToDb">класс, выполняющий сохранение в БД</param>
		/// <param name="arrOfGroups">массив групп</param>
		public tokenWithGroups(string token, string secret, int tokenId, dbSaver saverToDb, Model.Groups[] arrOfGroups)
		{
			saver = saverToDb;

			this.tokenId = tokenId;

			vkWorker = new ClassLibraryVkontakteChecker.WorkClasses.MainClass(token, secret);
			vkWorker.haveBasicResult_EventHandler += new ClassLibraryVkontakteChecker.WorkClasses.MainClass.haveBasickMetricks(vkWorker_haveBasicResult_EventHandler);
			vkWorker.haveLists_EventHandler += new ClassLibraryVkontakteChecker.WorkClasses.MainClass.haveLists(vkWorker_haveLists_EventHandler);
			vkWorker.haveError_EventHandler += new ClassLibraryVkontakteChecker.WorkClasses.MainClass.haveError(vkWorker_haveError_EventHandler);
			foreach (var group in arrOfGroups)
			{
				Groups.Add(new group(
					group.GroupId,
					group.ListsRegulerReading,
					group.BasicMetrickRegularReading,
					group.ListsTimeOut,
					group.BasicMetricksTimeOut,
					vkWorker));
			}
			foreach (var group in Groups)
			{
				group.Start();
			}
		}

		/// <summary>
		/// поочередный запуск всех групп
		/// </summary>
		public void Start()
		{
			foreach (var group in Groups)
			{
				group.Start();
			}
		}

		/// <summary>
		/// Поочередная остановка всех групп
		/// </summary>
		public void Stop()
		{
			foreach (var group in Groups)
			{
				group.Stop();
			}
		}

		/// <summary>
		/// Обновить настройки для данного токена
		/// </summary>
		/// <param name="token"></param>
		public void updateSettings(Model.TokenList token)
		{
			//находим те элементы, которых нет в существующей последовательности, 
			//их нужно добавить, так как их нету в существующем массиве
			var newGroups = (token.Groups.Select(i => i.GroupId).Except(Groups.Select(i => i.groupId))).Select(i => token.Groups.Single(j => j.GroupId == i));
			//находим элементы в существующем массиве, которые надо удалить, так как их нету в списках
			var groupdForDelete = (Groups.Select(i => i.groupId).Except(token.Groups.Select(i => i.GroupId)));

//существующие группы
			//var oldGroups = token.Groups.Select(i => i.GroupId).Union(Groups.Select(i => i.group_id)).Select(i => Groups.Single(j => j.group_id == i));


			foreach (var group in newGroups)
			{
				//добавляем новые группы для обхода
				Groups.Add(new group(group.GroupId, group.ListsRegulerReading, group.BasicMetrickRegularReading, group.ListsTimeOut, group.BasicMetricksTimeOut, vkWorker));
				//запускаем
				Groups.Single(i => i.groupId == group.GroupId).Start();
			}

			foreach (var group in groupdForDelete)
			{
				Groups.Single(i => i.groupId == group).Stop();
				Groups.Remove(Groups.Single(i => i.groupId == group));
			}

			foreach (var group in Groups)//обновляем тайминги
			{
				group.setTimings(token.Groups.Single(i => i.GroupId == group.groupId));
			}

			//Groups.AsParallel().ForAll(i => i.setTimings(token.Groups.Single(j => j.GroupId == i.group_id)));
		}


		void vkWorker_haveError_EventHandler(string groupId, bool isContactError, bool isConnectionError, int contactErrorId, int methodIdError)
		{
			int errorId = -1;
			if (isContactError)
			{
				errorId = contactErrorId;
			}
			if (isConnectionError)
			{
				errorId = 101;
			}
			saver.SaveErrorToDB(groupId, errorId, methodIdError, DateTime.Now);
		}

		void vkWorker_haveLists_EventHandler(string groupId, ListsResult result)
		{
			saver.SaveListsResultToDB(groupId, result, DateTime.Now);
		}

		void vkWorker_haveBasicResult_EventHandler(string groupId, BasicMetricsResult result)
		{
			saver.SaveBasicMetricksToDB(groupId, result, DateTime.Now);
		}
	}

}
