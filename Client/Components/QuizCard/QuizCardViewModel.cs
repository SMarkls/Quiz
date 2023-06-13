using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.Components.QuizCard;

public class QuizCardViewModel : INotifyPropertyChanged
{
    private string text;
    private string answer1;
    private string answer2;
    private string answer3;
    private string answer4;
    
    public string Text
    {
        get => text;
        set
        {
            text = value;
            OnPropertyChanged();
        }
    }
    
    // Четыре варианта ответа.
    public string Answer1
    {
        get => answer1;
        set
        {
            answer1 = value;
            OnPropertyChanged();
        }
    }

    public string Answer2
    {
        get => answer2;
        set
        {
            answer2 = value;
            OnPropertyChanged();
        }
    }

    public string Answer3
    {
        get => answer3;
        set
        {
            answer3 = value;
            OnPropertyChanged();
        }
    }

    public string Answer4
    {
        get => answer4;
        set
        {
            answer4 = value;
            OnPropertyChanged();
        }
    }
    public int GivenAnswer { get; set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}