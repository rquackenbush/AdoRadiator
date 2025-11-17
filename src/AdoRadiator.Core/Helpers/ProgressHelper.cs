using System;

namespace AdoRadiator.Core.Helpers;

public static class ProgressHelper
{
    public static double GetProgress(DateTime start, DateTime finish)
    {
        finish = finish.AddDays(1);

        var total = (finish - start).TotalDays;
        var completed = (DateTime.Now - start).TotalDays;

        return completed * 100 / total; 
    }

    public static string GetColorFromProgress(double progress)
    {
        if (progress < 40)
            return "Good";

        if (progress <= 80)
            return "Warning";

        return "Attention";
    }
}
