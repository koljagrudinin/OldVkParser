using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.GroupsEditMetricks
{
	class GroupData
	{
		public static string groupId;
		public static int basicMetricksRegular;
		public static int basicMetricksTimeOut;
		public static int listsRegular;
		public static int listsTimeOut;

		public static void NullFields( )
		{
			groupId = "";
			basicMetricksRegular = basicMetricksTimeOut = listsRegular = listsTimeOut = 0;
		}
	}
}
