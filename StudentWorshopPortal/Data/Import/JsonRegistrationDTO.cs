namespace StudentWorshopPortal.Data.Import;

public class JsonRegistrationDTO
{
    public int Id { get; set; }

    public string StudentFullName { get; set; } = string.Empty;

    public string StudentNumber { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string WorkshopTitle { get; set; } = string.Empty;

    public string? Comments { get; set; }
}