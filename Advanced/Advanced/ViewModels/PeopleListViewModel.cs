using Advanced.Models;

namespace Advanced.ViewModels
{
    public class PeopleListViewModel
    {
        public IEnumerable<Person> People { get; set; } = Enumerable.Empty<Person>();
        public IEnumerable<string> Cities { get; set; } = Enumerable.Empty<string>();
        public string SelectedCity { get; set; } = string.Empty;

        public string GetClass(string? city) => SelectedCity == city ? "--bs-table-bg: transparent; --bs-table-bg-state: transparent; background-color: #17a2b8 !important; color: #fff !important; font-weight: bold;" : "";
    }
}