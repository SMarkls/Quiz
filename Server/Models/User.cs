namespace Server.Models;

public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Идентификатор соединения пользователя.
    /// </summary>
    public string ConnectionId { get; set; }
    
    /// <summary>
    /// Имя пользователя. 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Игровая сессия, в которой состоит пользователь.
    /// </summary>
    public GameSession GameSession { get; set; }
}