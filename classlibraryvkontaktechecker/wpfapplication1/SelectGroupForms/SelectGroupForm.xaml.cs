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

namespace WpfApplication1.SelectGroupForms
{
	/// <summary>
	/// Логика взаимодействия для SelectGroupForm.xaml
	/// </summary>
	public partial class SelectGroupForm : Window
	{
		public SelectGroupForm( )
		{
			InitializeComponent();
		}

		void updateGroups( )
		{
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				listBox1.Items.Clear();
				foreach ( var group in cont.GroupsEnt.OrderBy( i => i.GroupId ) )
				{
					listBox1.Items.Add( group.Id + ":" + group.GroupId );
				}
			}
		}

		private void Window_Loaded( object sender , RoutedEventArgs e )
		{
			updateGroups();
		}

		private void button1_Click( object sender , RoutedEventArgs e )
		{
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				var groups = cont.GroupsEnt.OrderBy( i => i.GroupId );

				foreach ( string str in listBox1.SelectedItems )
				{
					var strings = str.Split( ':' );
					foreach ( var group in groups )
					{
						if ( group.Id.ToString() == strings[0] )
						{
							if ( group.GroupId == strings[1] )
							{
								SelectGroupForms.GroupData.arrOfGroupdId.Add( group.Id );
								break;
							}
						}
					}
				}
			}
			this.Close();
		}

		private void listBox1_SelectionChanged( object sender , SelectionChangedEventArgs e )
		{

		}
	}
}
