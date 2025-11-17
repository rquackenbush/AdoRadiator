using System;

namespace AdoRadiator.Core.Factories
{
    public record class SprintDay(DateTime Date, int Index)
    {
        /// <summary>
        /// Gets the one based number of the day in the sprint.
        /// </summary>
        public int Number => Index + 1;
    }
}
