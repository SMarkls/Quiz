namespace Server.Models;

public class GameSession
{
    /// <summary>
    /// Идентификтор игровой сессии.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Название игровой сессии (комнаты). 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список игроков в сесси.
    /// </summary>
    public List<User> Players { get; set; } = new();
    
    /// <summary>
    /// Состояние игры.
    /// </summary>
    public GameState State { get; set; }

    /// <summary>
    /// Список заданных вопросов.
    /// </summary>
    public List<Question> AskedQuestions { get; set; } = new();
}