namespace ASPNetExapp.Models;

public class Newspaper
{
    public int ID { get; set; }
    public string Thema { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public  DateTimeOffset Date { get; set; } = DateTime.Now;
    public List<string> Names { get; set; } = new List<string>();
    public List<string> Adreses { get; set; } = new List<string>();
}
