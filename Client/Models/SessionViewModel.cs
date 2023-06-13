using System.Collections.Generic;

namespace Client.Models;

public class SessionViewModel
{ 
    public string Text { get; set; }
    public string Answer1 { get; set; }
    public string Answer2 { get; set; }
    public string Answer3 { get; set; }
    public string Answer4 { get; set; }
    public int CorrectAnswers1Player { get; set; }
    public int CorrectAnswers2Player { get; set; }
    public List<string> Players { get; set; }
}