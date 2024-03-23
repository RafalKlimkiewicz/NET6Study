namespace PartyInvites.Models
{
    public static class Repository
    {
        private static List<GuestReponse> responses = new();

        public static IEnumerable<GuestReponse> Responses => responses;

        public static void AddResponse(GuestReponse response)
        {
            Console.WriteLine(response);
            responses.Add(response);
        }
    }
}