namespace Server.Models;

public class GameState
{
    /// <summary>
    /// Иденификатор игрового состояния.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Шаг игры.
    /// </summary>
    public int Step { get; set; } = 1;

    /// <summary>
    /// Правильные ответы первого игрока.
    /// </summary>
    public int CorrectAnswers1Player { get; set; }
    
    /// <summary>
    /// Правильные ответы второго игрока.
    /// </summary>
    public int CorrectAnswers2Player { get; set; }

    /// <summary>
    /// Текущий вопрос.
    /// </summary>
    public Question? CurrentQuestion { get; set; }
    
    /// <summary>
    /// Идентификатор текущего вопроса.
    /// </summary>
    public long? CurrentQuestionId { get; set; }
}