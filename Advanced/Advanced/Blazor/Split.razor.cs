using Advanced.Database;

using Microsoft.AspNetCore.Components;

namespace Advanced.Blazor
{
    public partial class Split
    {
        [Inject]
        public DataContext? Context { get; set; }

        public IEnumerable<string> Names => Context?.People.Select(p => p.FirstName) ?? Enumerable.Empty<string>();
    }
}