namespace Platform.Services.ChainDependency
{
    public interface ITimeStamper
    {
        string TimeStamp { get; }
    }

    public class DefaultTimeStamper : ITimeStamper
    {
        public string TimeStamp { get => DateTime.Now.ToString(); }
    }

    public class TimeResponseFormatter : IResponseFormatter
    {
        private ITimeStamper _timeStamper;

        public TimeResponseFormatter(ITimeStamper timeStamper)
        {
            _timeStamper = timeStamper;
        }

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{_timeStamper.TimeStamp}: {content}");
        }
    }
}
