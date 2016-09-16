using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ClassLibraryVkontakteChecker.Exception;

namespace ClassLibraryVkontakteChecker.WorkClasses
{
	/// <summary>
	/// Класс для работы от имени одного пользователя
	/// </summary>
	public class MainClass
	{
		/// <summary>
		/// Класс, напрямую работающий с контактом
		/// </summary>
		Wrapper vkWorker;

		public delegate void haveBasickMetricks( string groupId , Results.BasicMetricsResult result );
		/// <summary>
		/// Событие, возникающее при получении базовых метрик
		/// </summary>
		public event haveBasickMetricks haveBasicResult_EventHandler;

		public delegate void haveLists( string groupId , Results.ListsResult result );
		/// <summary>
		/// Событие, возникающее при получении списков
		/// </summary>
		public event haveLists haveLists_EventHandler;

		public delegate void haveError( string groupId , bool isContactError , bool isConnectionError , int contactErrorId , int methodIdError );
		/// <summary>
		/// Событие, возникающее при какой-либо ошибке
		/// </summary>
		public event haveError haveError_EventHandler;

		//инициализация
		public MainClass( string accessToken , string secretKey )
		{
			vkWorker = new Wrapper( accessToken , secretKey );
		}

		/// <summary>
		/// Класс, создает ошибку таймера
		/// </summary>
		/// <param name="groupId"></param>
		/// <param name="methodId"></param>
		/// <param name="Time"></param>
		public void createTimeOutError( string groupId , int methodId , DateTime Time )
		{
			if ( haveError_EventHandler != null )
			{
				this.haveError_EventHandler( groupId , false , false , 102 , methodId );
			}
		}

		#region Lists
		/// <summary>
		/// Возвращает списки по группе
		/// </summary>
		public void getLists( string groupId )
		{
			try
			{
				groupId = vkWorker.getGroupId( groupId ).Substring( 0 );
				Results.ListsResult result = new Results.ListsResult
				{
					UsersInGroup = vkWorker.getMembersFull( groupId , false ) ,
					Wall = vkWorker.getWall( groupId , false )
				};

				if ( haveLists_EventHandler != null )
				{
					this.haveLists_EventHandler( groupId , result );
				}
			}
			catch ( ContactCheckerException e )
			{
				if ( haveError_EventHandler != null )
				{
					this.haveError_EventHandler( groupId , e.isContactError , e.isConnectionError , e.contactErrorId , 1 );
				}
			}
		}

		/// <summary>
		/// Получить всех пользователей группы
		/// </summary>
		/// <param name="groupId"></param>
		/// <returns></returns>
		public Data.User[] getUserOfGroup( string groupId )
		{
			return vkWorker.getMembersFull( groupId );
		}

		#endregion
		#region BasicMetricks

		/// <summary>
		/// Возвращает статистику по группе
		/// </summary>
		public void getBasicMetricks( string groupId )
		{
			try
			{
				groupId = vkWorker.getGroupId( groupId ).Substring( 0 );
				Results.BasicMetricsResult result = null;
				var PostsCount = int.Parse( vkWorker.getWall( groupId , 0 , 1 , false ).count );
				var UsersCount = int.Parse( vkWorker.getMembers( groupId , 0 , 1 , false ).count );
				var AudioCount = int.Parse( (string)vkWorker.getAudioCount( groupId , false ) );
				string videoCountString = vkWorker.getVideos( groupId , false ).count;
				var VideosCount = int.Parse( ( videoCountString == null ) ? "0" : videoCountString );
				result = new Results.BasicMetricsResult()
				{
					groupId = groupId ,
					AudioCount = AudioCount ,
					PostsCount = PostsCount ,
					UsersCount = UsersCount ,
					VideoCount = VideosCount
				};
				if ( haveBasicResult_EventHandler != null )
				{
					this.haveBasicResult_EventHandler( groupId , result );
				}
			}
			catch ( ContactCheckerException e )
			{
				if ( haveError_EventHandler != null )
				{
					this.haveError_EventHandler( groupId , e.isContactError , e.isConnectionError , e.contactErrorId , 2 );
				}
			}
		}

		#endregion
	}
}
