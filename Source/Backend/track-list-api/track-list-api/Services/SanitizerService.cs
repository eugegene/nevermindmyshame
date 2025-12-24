using Ganss.Xss;

namespace api.Services;

public interface ISanitizerService
{
    string Sanitize(string input);
}

public class SanitizerService : ISanitizerService
{
    private readonly HtmlSanitizer _sanitizer;

    public SanitizerService()
    {
        _sanitizer = new HtmlSanitizer();
    }

    public string Sanitize(string input)
    {
        return _sanitizer.Sanitize(input);
    }
}