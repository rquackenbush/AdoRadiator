using System;
using System.Collections.Generic;

namespace AdoRadiator.Core.Factories
{
    public static class SprintDayFactory
    {
        public static IEnumerable<SprintDay> GetSprintDays(DateTime start, DateTime end)
        {
            var index = 0;

            for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    yield return new SprintDay(date, index);

                    index++;
                }
            }
        }
    }
}
