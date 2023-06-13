using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Client.Components.QuizCard;
using Client.Models;
using Client.Pages;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client;

public partial class MainWindow : Window
{
    private HubConnection connection;
    
    public MainWindow()
    {
        InitializeComponent();
        connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7251/game")
            .WithKeepAliveInterval(TimeSpan.FromMinutes(10))
            .WithAutomaticReconnect(new[] { TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20) })
            .WithServerTimeout(TimeSpan.FromMinutes(10))
            .Build();
        connection.HandshakeTimeout = TimeSpan.FromMinutes(10);
        connection.Closed += exception =>
        {
            Dispatcher.Invoke(() =>
            {
                if (exception is not null && exception.Message.Length > 0)
                    MessageBox.Show(exception.Message);
                Frame.Content = new ConnectionPage(Connect);
                CloseBtn.Visibility = Visibility.Collapsed;
            });
            return Task.CompletedTask;
        };
        
        connection.On<string>("Message", MessageRecievedHandler);
        connection.On<SessionViewModel>("EndGame", GameEndedHanlder);
        connection.On<SessionViewModel>("StepMaked", StepMakedHandler);
    }
    
    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        Frame.Content = new ConnectionPage(Connect);
    }
    
    private async void Connect(string name, string roomName)
    {
        await connection.StartAsync();
        Frame.Content = null;
        await connection.InvokeAsync("Connect", name, roomName);
        CloseBtn.Visibility = Visibility.Visible;
    }

    private async void GameEndedHanlder(SessionViewModel session)
    {
        if (session.CorrectAnswers1Player > session.CorrectAnswers2Player)
            MessageBox.Show(
                $"{session.Players[0]} победил!" +
                $"\n{session.Players[0]}: {session.CorrectAnswers1Player} очков" +
                $"\n{session.Players[1]}: {session.CorrectAnswers2Player} очков");
        else if (session.CorrectAnswers2Player > session.CorrectAnswers1Player)
            MessageBox.Show(
                $"{session.Players[1]} победил!." +
                $"\n{session.Players[0]}: {session.CorrectAnswers1Player} очков" +
                $"\n{session.Players[1]}: {session.CorrectAnswers2Player} очков");
        else
            MessageBox.Show(
                "Ничья!" +
                $"\n{session.Players[0]}: {session.CorrectAnswers1Player} очков" +
                $"\n{session.Players[1]}: {session.CorrectAnswers2Player} очков");
        await connection.StopAsync();
        
        Dispatcher.Invoke(() =>
        {
            Frame.Content = new ConnectionPage(Connect);
            CloseBtn.Visibility = Visibility.Collapsed;
        });
    }
    
    private void StepMakedHandler(SessionViewModel session)
    {
        Dispatcher.Invoke(() =>
        {
            QuizCard card = new(SendAnswerHandler);
            card.ViewModel.Answer1 = session.Answer1;
            card.ViewModel.Answer2 = session.Answer2;
            card.ViewModel.Answer3 = session.Answer3;
            card.ViewModel.Answer4 = session.Answer4;
            card.ViewModel.Text = session.Text;
            Frame.Content = card;
        });
    }

    private async void SendAnswerHandler(int answer)
    {
        Frame.Content = null;
        await connection.InvokeAsync("MakeStep", answer);
    }
    
    private void MessageRecievedHandler(string message) => MessageBox.Show(message);
    
    private async void CloseBtnClicked(object sender, RoutedEventArgs e)
    {
        await connection.SendAsync("ClientLost");
        await connection.StopAsync();
        Frame.Content = new ConnectionPage(Connect);
        CloseBtn.Visibility = Visibility.Collapsed;
    }
    
    private async void WindowClosing(object? sender, CancelEventArgs e)
    {
        await connection.SendAsync("ClientLost");
        await connection.StopAsync();
    }
}