using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.Pages;

public partial class ConnectionPage : Page
{
    private readonly Action<string, string> hanlder;
    public string Name => NameTextBox.Text;
    public string RoomName => RoomNameTextBox.Text;
    
    public ConnectionPage(Action<string, string> hanlder)
    {
        this.hanlder = hanlder;
        InitializeComponent();
    }

    private void ConnectClicked(object sender, RoutedEventArgs e)
    {
        hanlder(Name, RoomName);
    }
}