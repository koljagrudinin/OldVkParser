using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.SelectGroupForms
{
	class GroupData
	{
		public static int tokenId = -1;
		public static List<int> arrOfGroupdId = new List<int>();
		public static void NullData( )
		{
			tokenId = -1;
			arrOfGroupdId.Clear();
		}
	}
}
