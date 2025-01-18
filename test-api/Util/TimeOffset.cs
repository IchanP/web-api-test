
namespace test_api.Util
{
    public class Timer()
    {
        public static TimeSpan CreateTimeSpanOffset(int hours, int minutes)
        {
            DateTime timeNow = DateTime.Now;
            DateTime nextRunTime = timeNow.Date.AddHours(timeNow.Hour + hours).AddMinutes(minutes);
            return nextRunTime - timeNow;
        }
    }
}