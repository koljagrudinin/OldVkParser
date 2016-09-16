using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ClassLibraryVkontakteChecker.WorkClasses
{
	/// <summary>
	/// Класс, напрямую работающий с вконтактом
	/// </summary>
	class VkontakteWorker
	{
		private object lockObj = new object();

		/// <summary>
		/// получение битовых данных 
		/// </summary>
		/// <param name="url">Ссылка</param>
		/// <exception cref="System.NetWebExeption"></exception>
		/// <exception cref="System.Net.ProtocolViolationException"></exception>
		byte[] getBytesNative( string url )
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create( url );

			request.MaximumAutomaticRedirections = 200;
			request.MaximumResponseHeadersLength = 200;
			request.Credentials = CredentialCache.DefaultCredentials;
			List<byte> arrOfBytes = new List<byte>();
			lock ( lockObj )
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				using ( var receiveStream = response.GetResponseStream() )
				{
					int a = 0;
					while ( ( a = receiveStream.ReadByte() ) >= 0 )
						arrOfBytes.Add( (byte)a );
					receiveStream.Close();
				}
				response.Close();
			}

			return arrOfBytes.ToArray();
		}



		/// <summary>
		/// Получение данных с проверкой на 6ю ошибку (слишком много запросов)
		/// </summary>
		public byte[] getBytes( string url )
		{
			try
			{
				byte[] data;

				data = getBytesNative( url );
				string dataString = Encoding.UTF8.GetString( data );

				if ( dataString.IndexOf( "<error_code>6</error_code>" ) >= 0 )//6 ошибка - слишком много запросов
				{
					lock ( lockObj )
					{
						System.Threading.Thread.Sleep( 3000 );
					}
					data = getBytesNative( url );
				}

				return data;
			}
			catch
			{
				throw new ClassLibraryVkontakteChecker.Exception.ContactCheckerException( false , -1 , true );
			}
		}

		/// <summary>
		/// Функция создания запроса
		/// </summary>
		/// <param name="methodName">Имя метода</param>
		/// <param name="NamesOfParamsWithNames">Массив имен парамет</param>
		/// <param name="accessTokenWithoutName">Токен доступа</param>
		/// <param name="secretKey">Секретный ключ</param>
		/// <returns>Возвращает строку запроса</returns>
		public static string createUrl( string methodName , List<string[]> NamesOfParamsWithNames , string accessTokenWithoutName , string secretKey )
		{
			string baseUrl = "http://api.vk.com";
			string sortedData = "";
			if ( NamesOfParamsWithNames.Count != 0 )
				sortedData = NamesOfParamsWithNames.OrderBy( i => i[0] ).Select( i => i[0] + "=" + i[1] ).Aggregate( ( i , j ) => i + '&' + j );
			string baseMethod = "/method/" + methodName + '?' + sortedData + "&access_token=" + accessTokenWithoutName;
			string sid = MD5( baseMethod + secretKey );
			baseMethod += "&sig=" + sid;
			return baseUrl + baseMethod;
		}

		/// <summary>
		/// Возвращает MD5-хэш текста
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private static string MD5( string text )
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			return System.Text.RegularExpressions.Regex.Replace( BitConverter.ToString( md5.ComputeHash( ASCIIEncoding.Default.GetBytes( text ) ) ) , "-" , "" ).ToLowerInvariant();
		}
	}
}
