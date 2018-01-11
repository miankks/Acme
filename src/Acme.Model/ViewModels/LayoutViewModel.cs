namespace Acme.Model.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Acme.Model.Navigation;

    public class LayoutViewModel
    {
        public bool IsDebug { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public string RobotsText { get; set; }

        public string CssPath { get; set; }

        public string GoogleTagManagerId { get; set; }

        public Uri CanonicalUrl { get; set; }

        public OpenGraph OpenGraph { get; set; }

        public List<NavigationItem> Breadcrumbs { get; set; }

        public List<NavigationItem> Navigation { get; set; }

        public HierarchicalNavigationItem SubNavigation { get; set; }
    }
}
