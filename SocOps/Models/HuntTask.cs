namespace SocOps.Models;

public class HuntTask
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public bool IsChecked { get; set; }
}
