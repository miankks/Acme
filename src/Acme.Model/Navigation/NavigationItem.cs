namespace Acme.Model.Navigation
{
    public class NavigationItem
    {
        public bool IsSelected { get; set; }

        public bool IsExpanded { get; set; }

        public bool IsStartPage { get; set; }

        public object Reference { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }
    }
}
