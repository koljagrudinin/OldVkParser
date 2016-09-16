using System;

public class Class1
{
	public Class1()
	{
		
		/*
			http://oauth.vk.com/authorize?client_id=2860162&scope=audio,video,wall,groups,offline,nohttps&redirect_uri=http://api.vk.com/blank.html&display=popup&response_type=token

access_token=58e5660358a7187758a71877c6588cbcf5458a758a2396efa1cda0a0a0711ac
&expires_in=0
&user_id=4357748
&secret=54cdb8668265a74cce

/method/{Название метода}?{GET параметры}{POST параметры}
/method/isAppUser?uid=4357748&access_token=58e5660358a7187758a71877c6588cbcf5458a758a2396efa1cda0a0a0711ac
+secret
	/method/isAppUser?uid=4357748&access_token=58e5660358a7187758a71877c6588cbcf5458a758a2396efa1cda0a0a0711ac54cdb8668265a74cce
	=	5f8a2e71bc30487bead8b6c91eed4c33
	
http://api.vk.com/method/isAppUser?uid=4357748&access_token=58e5660358a7187758a71877c6588cbcf5458a758a2396efa1cda0a0a0711ac&sig=5f8a2e71bc30487bead8b6c91eed4c33
	=	{"response":"1"}
	
	/method/isAppUser.xml?uid=4357748&access_token=58e5660358a7187758a71877c6588cbcf5458a758a2396efa1cda0a0a0711ac54cdb8668265a74cce
	=	d6a1115d3bae8af5c3ee7c8ac42abf0b
	
http://api.vk.com/method/isAppUser.xml?uid=4357748&access_token=58e5660358a7187758a71877c6588cbcf5458a758a2396efa1cda0a0a0711ac&sig=d6a1115d3bae8af5c3ee7c8ac42abf0b
	=	<response>1</response>
	
код md5() c#:
	public static string MD5(string text)
	{
		System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		return System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(md5.ComputeHash(ASCIIEncoding.Default.GetBytes(text))), "-", "").ToLowerInvariant();
	}
		 */
		

	}
}
