using UnityEngine;

public class ConsoleLog
{
    public static void Log(LogCategory category, string logMessage, LogPriority logPriority = LogPriority.Normal)
    {
        if (ConsoleConfiguration.LogCategories.TryGetValue(category, out LogDisplaySetting logDisplaySetting))
        {
            if (!CheckDisplayLog(logPriority, logDisplaySetting)) return;

            Debug.Log($"[ <color={ColourUtility.GetHexadecimalColour(ColourType.Empty)}>{category}</color>] {logMessage}");
        }
        else
        {
            new NotImplementedException("LogCategory", category.ToString());
        }
    }

    public static void Warning(LogCategory category, string logMessage, LogPriority logPriority = LogPriority.Normal)
    {
        if (ConsoleConfiguration.LogCategories.TryGetValue(category, out LogDisplaySetting logDisplaySetting))
        {
            if (!CheckDisplayLog(logPriority, logDisplaySetting)) return;

            Debug.LogWarning($"[ <color={ColourUtility.GetHexadecimalColour(ColourType.Empty)}>{category}</color>] {logMessage}");
        }
        else
        {
            new NotImplementedException("LogCategory", category.ToString());
        }
    }

    public static void Error(LogCategory category, string logMessage, LogPriority logPriority = LogPriority.High)
    {
        if (ConsoleConfiguration.LogCategories.TryGetValue(category, out LogDisplaySetting logDisplaySetting))
        {
            Debug.LogError($"[ <color={ColourUtility.GetHexadecimalColour(ColourType.Empty)}>{category}</color>] {logMessage}");
        }
        else
        {
            new NotImplementedException("LogCategory", category.ToString());
        }
    }

    private static bool CheckDisplayLog(LogPriority logPriority, LogDisplaySetting logDisplaySetting)
    {
        switch (logDisplaySetting)
        {
            case LogDisplaySetting.None:
                return false;
            case LogDisplaySetting.Few:
                if (logPriority == LogPriority.High) return true;
                return false;
            case LogDisplaySetting.Medium:
                if (logPriority == LogPriority.High ||
                    logPriority == LogPriority.Normal) return true;
                return false;
            case LogDisplaySetting.All:
                return true;
            default:
                new NotImplementedException("LogDisplaySetting", logDisplaySetting.ToString());
                break;
        }
        return false;
    }
}
