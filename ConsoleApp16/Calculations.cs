using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
   public class Calculations
   {
            public string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime,
                TimeSpan endWorkingTime, int consultationTime)
            {
                List<string> result = new List<string>();
                if (endWorkingTime <= beginWorkingTime)
                    endWorkingTime += TimeSpan.FromHours(24);

                var consultationSlotDuration = TimeSpan.FromMinutes(consultationTime);
                var workStart = beginWorkingTime;
                var breaks = startTimes
                .Select(startTime => startTime <= beginWorkingTime ? startTime + TimeSpan.FromHours(24) : startTime)
                .Zip(durations, (start, duration) => (start, end: start + TimeSpan.FromMinutes(duration)))
                .Append((endWorkingTime, TimeSpan.Zero));

                foreach (var (breakStart, breakEnd) in breaks)
                {
                    var slotCount = (int)((breakStart - workStart) / consultationSlotDuration);
                    for (int j = 0; j < slotCount; j++)
                    {
                        var slotStart = workStart + consultationSlotDuration * j;
                        var slotEnd = workStart + consultationSlotDuration * (j + 1);
                        result.Add($"{slotStart:hh':'mm}-{slotEnd:hh':'mm}");
                    }
                    workStart = breakEnd;
                }
                return result.ToArray();
            }
        
   }
}
