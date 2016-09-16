using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryVkontakteChecker.Exception
{
	public class ContactCheckerException : SystemException
	{
		public readonly bool isContactError;
		public readonly int contactErrorId = -1;
		public readonly bool isConnectionError;
		public ContactCheckerException( bool isContactError , int contactErrorId , bool isConnectionError  )
		{
			this.isConnectionError = isConnectionError;
			this.isContactError = isContactError;
			this.contactErrorId = contactErrorId;
		}
	}
}
