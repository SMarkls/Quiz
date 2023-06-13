using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.Components.QuizCard;

public partial class QuizCard : UserControl
{
    private readonly Action<int> clickHandler;
    public QuizCard(Action<int> clickHandler)
    {
        this.clickHandler = clickHandler;
        InitializeComponent();
    }

    private void AnswerClicked(object sender, RoutedEventArgs e)
    {
        Button btn = sender as Button;
        if (btn is null)
            return;
        
        if (Grid.GetRow(btn) == 2)
            ViewModel.GivenAnswer = 3 + Grid.GetColumn(btn);
        else
            ViewModel.GivenAnswer = 1 + Grid.GetColumn(btn);
        
        clickHandler(ViewModel.GivenAnswer);
    }
}