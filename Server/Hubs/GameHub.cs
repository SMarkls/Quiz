using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Hubs;

public class GameHub : Hub
{
    private readonly ApplicationDbContext context;

    public GameHub(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task MakeStep(int answer)
    {
        var user = GetUserByConnectionId(Context.ConnectionId);
        var session = await context.Sessions
            .Include(i => i.Players)
            .Include(i => i.AskedQuestions)
            .Include(i => i.State)
            .FirstAsync(x => x == user.GameSession);
        
        var state = await context.States
            .Include(i => i.CurrentQuestion)
            .FirstAsync(x => x.Id == session.State.Id);

        if (state.CurrentQuestion.CorrectAnswer == answer)
        {
            int player = session.Players[1].Id > user.Id ? 1 : 2;
            if (player == 1)
                state.CorrectAnswers1Player++;
            else
                state.CorrectAnswers2Player++;
        }
        
        state.Step++;
        
        if (state.Step % 2 == 0)
        {
            await context.SaveChangesAsync();
            return;
        }
        
        session.AskedQuestions.Add(state.CurrentQuestion);
        await context.SaveChangesAsync();
        state.CurrentQuestion = await GetNewQuestion(session);
        await context.SaveChangesAsync();
        
        if (state.Step != 41) 
        {
            await Clients.Group(session.Name).SendAsync("StepMaked", GetVm(session));
            return;
        }
        
        await Clients.Group(session.Name).SendAsync("EndGame", GetVm(session));
        context.Users.RemoveRange(session.Players[0], session.Players[1]);
        context.Sessions.Remove(session);
        context.States.Remove(state);
        await context.SaveChangesAsync();
    }
    
    private async Task<Question?> GetNewQuestion(GameSession session) =>
        await context.Questions.FirstOrDefaultAsync(x => !session.AskedQuestions.Contains(x));

    private User GetUserByConnectionId(string connectionId) => context.Users
        .Include(i => i.GameSession) 
        .First(x => x.ConnectionId == connectionId);
    
    
    public async Task Connect(string userName, string roomName)
    {
        var user = new User { Name = userName, ConnectionId = Context.ConnectionId };
        var room = await context.Sessions
            .Include(i => i.Players)
            .Include(i => i.State)
            .FirstOrDefaultAsync(x => x.Name == roomName);
        if (room is null)
        {
            var state = new GameState();
            await context.States.AddAsync(state);
            room = new GameSession { State = state, Name = roomName };
            await context.Sessions.AddAsync(room);
            await context.SaveChangesAsync();
        }

        if (room.Players.Count < 1)
        {
            await context.Users.AddAsync(user);
            room.Players.Add(user);
            await context.SaveChangesAsync();
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Caller.SendAsync("Message", "Ожидание второго игрока.");
        }
        else if (room.Players.Count < 2)
        {
            await context.Users.AddAsync(user);
            room.Players.Add(user);
            room.State.CurrentQuestion = await GetNewQuestion(room);
            await context.SaveChangesAsync();
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Groups(roomName).SendAsync("StepMaked", GetVm(room));
        }
        else
            await Clients.Caller.SendAsync("Message", "Комната уже занята.");
    }
    
    public async Task ClientLost()
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.ConnectionId == Context.ConnectionId);
        if (user is null)
            return;
        var session = await context.Sessions
            .Include(i => i.Players)
            .Include(i => i.State)
            .Include(i => i.State.CurrentQuestion)
            .FirstAsync(x => x.Players.Contains(user));

        await Clients.GroupExcept(session.Name, new[] { Context.ConnectionId }).SendAsync("EndGame", GetVm(session));
        context.Users.RemoveRange(session.Players[0], session.Players[1]);
        context.States.Remove(session.State);
        context.Sessions.Remove(session);
        await context.SaveChangesAsync();
    }
    
    private SessionViewModel GetVm(GameSession session)
    {
        return new SessionViewModel
        {
            Players = session.Players.OrderBy(x => x.Id).Select(x => x.Name).ToList(),
            CorrectAnswers1Player = session.State.CorrectAnswers1Player,
            CorrectAnswers2Player = session.State.CorrectAnswers2Player,
            Answer1 = session.State.CurrentQuestion?.Answer1,
            Answer2 = session.State.CurrentQuestion?.Answer2,
            Answer3 = session.State.CurrentQuestion?.Answer3,
            Answer4 = session.State.CurrentQuestion?.Answer4,
            Text = session.State.CurrentQuestion?.Text
        };
    }
}