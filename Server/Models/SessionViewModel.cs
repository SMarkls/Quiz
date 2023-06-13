namespace Server.Models;

public class SessionViewModel
{
    /// <summary>
    /// Текст вопроса.
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// 1 Вариант ответа.
    /// </summary>
    public string Answer1 { get; set; }
    
    /// <summary>
    /// 2 Вариант ответа.
    /// </summary>
    public string Answer2 { get; set; }
    
    /// <summary>
    /// 3 Вариант ответа.
    /// </summary>
    public string Answer3 { get; set; }
    
    /// <summary>
    /// 4 Вариант ответа.
    /// </summary>
    public string Answer4 { get; set; }
    
    /// <summary>
    /// Правильные ответы первого игрока.
    /// </summary>
    public int CorrectAnswers1Player { get; set; }
    
    /// <summary>
    /// Правильные ответы второго игрока.
    /// </summary>
    public int CorrectAnswers2Player { get; set; }
    
    /// <summary>
    /// Список игроков.
    /// </summary>
    public List<string> Players { get; set; }
}