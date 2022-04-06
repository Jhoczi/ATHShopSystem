using System.ComponentModel;

namespace ath_server.Services;

public enum ServiceResultStatus
{
    [Description("Error")]
    Error = 0,
    [Description("Success")]
    Success = 1,
    [Description("Warning")]
    Warning,
    [Description("Info")]
    Info
}

public class ServiceResult
{
    public ServiceResultStatus Result { get; set; }
    public ICollection<string> Messages { get; set; }

    public ServiceResult()
    {
        Result = ServiceResultStatus.Success;
        Messages = new List<string>();
    }

    public static Dictionary<string, ServiceResult> CommonResults { get; set; } = new()
    {
        {
            "NotFound", new ServiceResult
            {
                Result = ServiceResultStatus.Error,
                Messages = new List<string>(new string[]
                {
                    "Object not found"
                })
            }
        },
        {
            "Ok", new ServiceResult
            {
                Result = ServiceResultStatus.Success,
                Messages = new List<string>()
            }
        }
    };
}