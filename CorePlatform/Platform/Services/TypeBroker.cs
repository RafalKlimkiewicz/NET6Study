namespace Platform.Services
{
    public static class TypeBroker
    {
        private static IResponseFormatter formatter = new HtmlResponseFormatter();
        //private static IResponseFormatter formatter = new TextResponseFormatter();

        public static IResponseFormatter Formatter => formatter;
    }
}