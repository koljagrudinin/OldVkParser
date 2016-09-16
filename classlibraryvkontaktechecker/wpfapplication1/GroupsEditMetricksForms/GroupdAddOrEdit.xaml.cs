using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1.GroupsEditMetricks
{
	/// <summary>
	/// Логика взаимодействия для GroupdAddOrEdit.xaml
	/// </summary>
	public partial class GroupdAddOrEdit : Window
	{
		public GroupdAddOrEdit( )
		{
			InitializeComponent();
		}

		public GroupdAddOrEdit( string groupId , int basicRegular , int basicTimeOut , int listsRegular , int listsTimeOut )
		{
			InitializeComponent();
			textBoxName.Text = groupId;
			textBoxBasicRegular.Text = basicRegular.ToString();
			textBoxBasicTimeOut.Text = basicTimeOut.ToString();
			textBoxListsRegular.Text = listsRegular.ToString();
			textBoxListsTimeOut.Text = listsTimeOut.ToString();
		}

		private void button1_Click( object sender , RoutedEventArgs e )
		{
			GroupData.groupId = textBoxName.Text;
			GroupData.basicMetricksRegular = int.Parse( textBoxBasicRegular.Text );
			GroupData.basicMetricksTimeOut = int.Parse( textBoxBasicTimeOut.Text );
			GroupData.listsRegular = int.Parse( textBoxListsRegular.Text );
			GroupData.listsTimeOut = int.Parse( textBoxListsTimeOut.Text );
			this.Close();
		}
	}
}
