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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects.SqlClient;

namespace WpfApplication1
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow( )
		{
			InitializeComponent();
		}

		void updateUsers( )
		{
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				dataGridUsers.ItemsSource = cont.TokenListEnt.OrderBy( i => i.UserName );
			}
			updateGroups();
		}

		void updateGroups( )
		{
			dataGridGroups.ItemsSource = null;
			if ( dataGridUsers.SelectedIndex >= 0 && dataGridUsers.SelectedIndex >= 0 )
			{
				using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
				{
					var token = (ClassLibraryPlanner.Model.TokenList)dataGridUsers.SelectedItem;//cont.TokenListEnt.OrderBy( j => j.UserName ).ToArray()[dataGridUsers.SelectedIndex];

					dataGridGroups.ItemsSource = cont.TokenListEnt.Single( i => i.Id == ( token ).Id ).Groups.OrderBy( i => i.GroupId );
				}
			}
			updateList();
		}

		void updateList( )
		{
			listBox1.Items.Clear();
			if ( dataGridGroups.SelectedIndex >= 0 && dataGridUsers.SelectedIndex >= 0 )
			{
				//dataGridGroups.context
				using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
				{
					var token = (ClassLibraryPlanner.Model.TokenList)dataGridUsers.SelectedItem;//cont.TokenListEnt.OrderBy( j => j.UserName ).ToArray()[dataGridUsers.SelectedIndex];
					var selectedItemInDB = (ClassLibraryPlanner.Model.Groups)dataGridGroups.SelectedItem;//cont.TokenListEnt.Single( i => i.Id == ( token ).Id ).Groups.OrderBy( i => i.GroupId ).ToArray()[dataGridGroups.SelectedIndex];
					listBox1.Items.Add( selectedItemInDB.GroupId );
					listBox1.Items.Add( "selectedItemInDB.BasicMetrickRegularReading : " + selectedItemInDB.BasicMetrickRegularReading );
					listBox1.Items.Add( "selectedItemInDB.BasicMetricksTimeOut : " + selectedItemInDB.BasicMetricksTimeOut );
					listBox1.Items.Add( "selectedItemInDB.ListsRegulerReading : " + selectedItemInDB.ListsRegulerReading );
					listBox1.Items.Add( "selectedItemInDB.ListsTimeOut : " + selectedItemInDB.ListsTimeOut );
				}
			}
		}

		private void dataGridGroups_SelectionChanged( object sender , SelectionChangedEventArgs e )
		{
			updateList();
		}

		private void dataGridUsers_SelectionChanged( object sender , SelectionChangedEventArgs e )
		{
			updateGroups();
		}

		void addNewUserToDB( )
		{
			if ( UsersAddDeleteForms.UserData.token == "" || UsersAddDeleteForms.UserData.secret == "" )
				return;
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				cont.TokenListEnt.AddObject( new ClassLibraryPlanner.Model.TokenList
				{
					Token = UsersAddDeleteForms.UserData.token ,
					UserName = UsersAddDeleteForms.UserData.nameOfNewUser ,
					SecretKey = UsersAddDeleteForms.UserData.secret
				} );
				cont.SaveChanges();
			}
			UsersAddDeleteForms.UserData.NullData();
		}
		private void button1_Click( object sender , RoutedEventArgs e )
		{
			var formAddUser = new UsersAddDeleteForms.AddNewUser();
			this.Hide();
			formAddUser.ShowDialog();
			this.Show();
			addNewUserToDB();
			updateUsers();
		}

		void deleteUser( ClassLibraryPlanner.Model.TokenList token )
		{
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				cont.TokenListEnt.DeleteObject( cont.TokenListEnt.Single( i => i.Id == token.Id ) );
				cont.SaveChanges();
			}
		}

		private void button2_Click( object sender , RoutedEventArgs e )
		{
			if ( dataGridUsers.SelectedIndex >= 0 )
			{
				deleteUser( (ClassLibraryPlanner.Model.TokenList)dataGridUsers.SelectedItem );
				updateUsers();
			}
		}

		void deleteGroup( int groupId , int tokenId )
		{
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				cont.TokenListEnt.Single( i => i.Id == tokenId ).Groups.Remove( cont.GroupsEnt.Single( i => i.Id == groupId ) );
				cont.SaveChanges();
			}
		}

		private void button4_Click( object sender , RoutedEventArgs e )
		{
			if ( dataGridGroups.SelectedIndex >= 0 && dataGridUsers.SelectedIndex >= 0 )
			{
				using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
				{
					var token = (ClassLibraryPlanner.Model.TokenList)dataGridUsers.SelectedItem;//cont.TokenListEnt.OrderBy( j => j.UserName ).ToArray()[dataGridUsers.SelectedIndex];
					var selectedItemInDB = (ClassLibraryPlanner.Model.Groups)dataGridGroups.SelectedItem;//cont.TokenListEnt.Single( i => i.Id == ( token ).Id ).Groups.OrderBy( i => i.GroupId ).ToArray()[dataGridGroups.SelectedIndex];

					deleteGroup( selectedItemInDB.Id , token.Id );

				}
			}
		}

		void createNewGroup( )
		{
			if ( dataGridUsers.SelectedIndex < 0 )
				return;
			var formAddGroup = new GroupsEditMetricks.GroupdAddOrEdit();
			formAddGroup.ShowDialog();
			if ( GroupsEditMetricks.GroupData.groupId == "" ||
					GroupsEditMetricks.GroupData.basicMetricksRegular == 0 ||
					GroupsEditMetricks.GroupData.basicMetricksTimeOut == 0 ||
					GroupsEditMetricks.GroupData.listsRegular == 0 ||
					GroupsEditMetricks.GroupData.listsTimeOut == 0 )
				return;

			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				var group = new ClassLibraryPlanner.Model.Groups
				{
					GroupId = GroupsEditMetricks.GroupData.groupId ,
					BasicMetrickRegularReading = GroupsEditMetricks.GroupData.basicMetricksRegular ,
					BasicMetricksTimeOut = GroupsEditMetricks.GroupData.basicMetricksTimeOut ,
					ListsRegulerReading = GroupsEditMetricks.GroupData.listsRegular ,
					ListsTimeOut = GroupsEditMetricks.GroupData.listsTimeOut ,
				};
				var token = cont.TokenListEnt.Single( i => i.Id == ( (ClassLibraryPlanner.Model.TokenList)dataGridUsers.SelectedItem ).Id );//cont.TokenListEnt.OrderBy( j => j.UserName ).ToArray()[dataGridUsers.SelectedIndex];

				group.TokenList.Add( token );
				token.Groups.Add( group );
				cont.SaveChanges();
			}
			GroupsEditMetricks.GroupData.NullFields();
			updateGroups();
		}

		void selectGroupFromForm( )
		{
			SelectGroupForms.GroupData.tokenId = ( (ClassLibraryPlanner.Model.TokenList)dataGridUsers.SelectedItem ).Id;
			new SelectGroupForms.SelectGroupForm().ShowDialog();
			if ( SelectGroupForms.GroupData.tokenId < 0 || SelectGroupForms.GroupData.arrOfGroupdId.Count == 0 )
			{
				SelectGroupForms.GroupData.NullData();
				return;
			}
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				var token = cont.TokenListEnt.Single( i => i.Id == SelectGroupForms.GroupData.tokenId );
				foreach ( var idGroup in SelectGroupForms.GroupData.arrOfGroupdId )
					token.Groups.Add( cont.GroupsEnt.Single( i => i.Id == idGroup ) );
				cont.SaveChanges();
				SelectGroupForms.GroupData.NullData();
			}
		}

		private void buttonAddGroup_Click( object sender , RoutedEventArgs e )
		{
			var result = MessageBox.Show( "Создать новую группу?" , "Группы" , MessageBoxButton.YesNo , MessageBoxImage.Question , MessageBoxResult.Yes , MessageBoxOptions.DefaultDesktopOnly );
			if ( result == MessageBoxResult.Yes )
				createNewGroup();
			else
				selectGroupFromForm();
			updateGroups();
		}

		private void buttonEditGroup_Click( object sender , RoutedEventArgs e )
		{
			if ( dataGridGroups.SelectedIndex < 0 || dataGridUsers.SelectedIndex < 0 )
				return;
			using ( var cont = new ClassLibraryPlanner.Model.ModelVkontakteContainer() )
			{
				var group = cont.GroupsEnt.Single( i => i.Id == ( (ClassLibraryPlanner.Model.Groups)dataGridGroups.SelectedItem ).Id );//cont.GroupsEnt.Single( i => i.Id == ( (ClassLibraryPlanner.Model.Groups)dataGridGroups.SelectedItem ).Id );
				var formAddGroup = new GroupsEditMetricks.GroupdAddOrEdit( group.GroupId , group.BasicMetrickRegularReading , group.BasicMetricksTimeOut , group.ListsRegulerReading , group.ListsTimeOut );
				formAddGroup.ShowDialog();
				if ( GroupsEditMetricks.GroupData.groupId == "" ||
						GroupsEditMetricks.GroupData.basicMetricksRegular == 0 ||
						GroupsEditMetricks.GroupData.basicMetricksTimeOut == 0 ||
						GroupsEditMetricks.GroupData.listsRegular == 0 ||
						GroupsEditMetricks.GroupData.listsTimeOut == 0 )
					return;

				group.GroupId = GroupsEditMetricks.GroupData.groupId;
				group.BasicMetrickRegularReading = GroupsEditMetricks.GroupData.basicMetricksRegular;
				group.BasicMetricksTimeOut = GroupsEditMetricks.GroupData.basicMetricksTimeOut;
				group.ListsRegulerReading = GroupsEditMetricks.GroupData.listsRegular;
				group.ListsTimeOut = GroupsEditMetricks.GroupData.listsTimeOut;

				cont.SaveChanges();
			}
			GroupsEditMetricks.GroupData.NullFields();
			updateGroups();
		}

		private void Window_Loaded( object sender , RoutedEventArgs e )
		{
			updateUsers();
		}
	}
}
