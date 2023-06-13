namespace Server.Models;

public class Question
{
    /// <summary>
    /// Идентификатор вопроса.
    /// </summary>
    public long Id { get; set; }
    
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
    /// Номер правильного овтета. 
    /// </summary>
    public int CorrectAnswer { get; set; }
    
    /// <summary>
    /// Список сессий, где текущий вопрос уже задан.
    /// </summary>
    public List<GameSession> Sessions { get; set; } = new();
}