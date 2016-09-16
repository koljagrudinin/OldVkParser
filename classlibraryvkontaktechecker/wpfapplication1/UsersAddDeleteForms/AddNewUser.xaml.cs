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

namespace WpfApplication1.UsersAddDeleteForms
{
	/// <summary>
	/// Логика взаимодействия для AddNewUser.xaml
	/// </summary>
	public partial class AddNewUser : Window
	{
		public AddNewUser( )
		{
			InitializeComponent();
		}

		private void webBrowser1_Navigating( object sender , System.Windows.Navigation.NavigatingCancelEventArgs e )
		{
			//	var en = Environment.NewLine;
			//MessageBox.Show( e.Uri.AbsoluteUri );
			string text = e.Uri.AbsoluteUri;//.AbsolutePath;
			if ( text.IndexOf( "http://api.vk.com/blank.html" ) >= 0 && text.IndexOf( "#" ) >= 0 )
			{
				text = text.Substring( text.IndexOf( "#" ) + 1 );
				foreach ( var txt in text.Split( '&' ) )
				{
					if ( txt.IndexOf( "access_token=" ) >= 0 )
					{
						UsersAddDeleteForms.UserData.token = txt.Replace( "access_token=" , "" );
					}
					if ( txt.IndexOf( "secret=" ) >= 0 )
					{
						UsersAddDeleteForms.UserData.secret = txt.Replace( "secret=" , "" );
					}
				}
				UsersAddDeleteForms.UserData.nameOfNewUser = textBox1.Text.Substring( 0 );
				this.Close();
			}
		}

		private void button2_Click( object sender , RoutedEventArgs e )
		{
			this.label1.Content = "Добавьте приложение";
			this.webBrowser1.Navigate( Properties.Settings.Default.UriToRegistration );//переходим
		}

		private void webBrowser1_Navigated( object sender , System.Windows.Navigation.NavigationEventArgs e )
		{
			//	MessageBox.Show( e.Uri.AbsolutePath + en + e.Uri.AbsoluteUri + en + e.Uri.Fragment + en + e.Uri.LocalPath + en + e.Uri.OriginalString + en + e.Uri.PathAndQuery + en + e.Uri.Query + en + e.Uri.Scheme + en + e.Uri.Segments.Aggregate( ( i , j ) => i + " " + j ) + en + e.Uri.UserInfo );
			//string text = e.Uri.AbsolutePath;
			//if ( text.IndexOf( "http://api.vk.com/blank.html" ) >= 0 )
			//{
			//    text = text.Substring( text.IndexOf( "#" ) );
			//    foreach ( var txt in text.Split( '&' ) )
			//    {
			//        if ( txt.IndexOf( "access_token=" ) >= 0 )
			//        {
			//            UsersAddDeleteForms.UserData.token = txt.Substring( txt.IndexOf( "access_token=" ) );
			//        }
			//        if ( txt.IndexOf( "secret=" ) >= 0 )
			//        {
			//            UsersAddDeleteForms.UserData.secret = txt.Substring( txt.IndexOf( "secret=" ) );
			//        }
			//    }
			//    UsersAddDeleteForms.UserData.nameOfNewUser = textBox1.Text.Substring( 0 );
			//    this.Close();
			//}
		}
	}
}
