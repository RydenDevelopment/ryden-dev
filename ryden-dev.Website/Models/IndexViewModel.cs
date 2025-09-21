namespace ryden_dev.Website.Models;

public class IndexViewModel
{
    public Dictionary<string, string> Translation { get; set; } = new Dictionary<string, string>();

    public IndexViewModel()
    {
        Translation.Add("Title", "Titel");
    }
}