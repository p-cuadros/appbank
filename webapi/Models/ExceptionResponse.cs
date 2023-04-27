using System.Text.Json;

namespace apibanca.webapi.Models;
public class ExceptionResponse
{
    public string Message { get; set; } = String.Empty;
    public Dictionary<string, IEnumerable<string>> Errors { get; set; } = new();
    private int _errorCount = 0;
    public void AddError(string error)
    {
        var el = new List<string>();
        el.Add(error);
        Errors[_errorCount.ToString()] = el;
    }
    public void AddValidationError(string property, string error)
    {
        if ( Errors.ContainsKey(property) )
        {
            (Errors[property]).Append(error);
        }
        else
        {
            var el = new List<string>();
            el.Add(error);
            Errors.Add(property, el);
        }
    }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}