using Hangfire;

namespace Service.Core
{
    public interface IHangFireService
    {
        void StartBackgroundService();
    }

    public class HangFireService : IHangFireService
    {
        private readonly IRecurringJobManager _recurringJobManager;
        public HangFireService(IRecurringJobManager recurringJobManager, IBackgroundJobClient backgroundJobClient)
        {
            _recurringJobManager = recurringJobManager;
        }
        public void StartBackgroundService()
        {
            _recurringJobManager.AddOrUpdate(
              "CalculateRiceSettlementOfDay",
              () => CalculateRiceSettlementOfDay(),
              "30 8 * * *"
          );

        }
        [AutomaticRetry(Attempts = 0)]
        public async Task CalculateRiceSettlementOfDay()
        {
            var date = DateTime.Now.Date.AddDays(-1);
            //  await _riceService.RiceSettlementOfDay(date);
        }
    }
}
