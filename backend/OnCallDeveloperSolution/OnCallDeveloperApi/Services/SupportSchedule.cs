namespace OnCallDeveloperApi.Services
{
    public class SupportSchedule : IProvideSupportSchedule
    {
        public bool InternalSupportAvailable
        {
            get
            {
                var now = DateTime.Now;
                return !(now.DayOfWeek == DayOfWeek.Sunday || now.DayOfWeek == DayOfWeek.Saturday);
            }
        }
    }
}
