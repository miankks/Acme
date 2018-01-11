namespace Acme.Model.Navigation
{
    using System.Collections.Generic;

    public class HierarchicalNavigationItem : NavigationItem
    {
        public HierarchicalNavigationItem()
        {
            this.Items = new List<HierarchicalNavigationItem>();
        }

        public bool HasChildren { get; set; }

        public int Level { get; set; }

        public List<HierarchicalNavigationItem> Items { get; set; }
    }
}
