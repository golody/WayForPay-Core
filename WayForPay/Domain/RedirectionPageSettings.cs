namespace WayForPay.Domain;

public class RedirectionPageSettings
{
    public string Title { get; set; } = "Redirecting to WayForPay...";

    /// <summary>
    /// Raw segment of the page
    /// </summary>
    public string VisibleContent { get; set; } = "<p>Redirecting to WayForPay...</p>";
}