using System.Collections.Generic;

public static class ConsoleConfiguration
{
    public static Dictionary<LogCategory, LogDisplaySetting> LogCategories = new Dictionary<LogCategory, LogDisplaySetting>()
    {
        {  LogCategory.Data, LogDisplaySetting.Medium },
        {  LogCategory.General, LogDisplaySetting.Medium },
        {  LogCategory.Initialisation, LogDisplaySetting.Medium },
    };
}