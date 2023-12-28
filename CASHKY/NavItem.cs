namespace CASHKY
{
    public class NavItem
    {
        public NavItem(string title)
        {
            Title = title;
        }

        public NavItem(string title, Type? type) : this(title)
        {
            Type = type;
        }

        public string Title { get; set; }
        public Type? Type { get; set; }
        public Action? Action { get; set; }

        public List<NavItem> NavItems { get; set; } = new();
    }
}