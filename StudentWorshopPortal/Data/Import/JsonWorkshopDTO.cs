namespace StudentWorshopPortal.Data.Import;

public class JsonWorkshopDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Instructor { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string Location { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}