namespace Acme.Domain.ViewModelBuilders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Hosting;
    using Acme.Model.Navigation;
    using Acme.Model.ViewModels;
    using EPiServer.Web;
    using NetR.Base.Concurrency;

    // TODO: Fix user handling
    // TODO: Move hashing to own service
    public class LayoutViewModelBuilder<TViewModel>
       where TViewModel : LayoutViewModel, new()
    {
        public LayoutViewModelBuilder()
        {
            ////var context = new ApplicationDbContext<SiteApplicationUser>();
            ////var userStore = new UserStore<SiteApplicationUser>(context);

            ////this.UserManager = new ApplicationUserManager<SiteApplicationUser>(userStore);

            this.ViewModel = new TViewModel();
            this.ViewModel.Title = SiteDefinition.Current.Name;
            this.ViewModel.Language = "en";
            this.ViewModel.OpenGraph = new OpenGraph();
            this.ViewModel.Breadcrumbs = new List<NavigationItem>();
            this.ViewModel.Navigation = new List<NavigationItem>();
            this.ViewModel.SubNavigation = new HierarchicalNavigationItem();

            this.WithCssPath();
            ////this.WithCurrentUser();

            if (HttpContext.Current != null &&
                HttpContext.Current.Request["debug"] != null)
            {
                this.ViewModel.IsDebug = true;
            }

            ////context.Dispose();
        }

        protected TViewModel ViewModel { get; private set; }

        ////protected ApplicationUserManager<SiteApplicationUser> UserManager { get; private set; }

        public virtual TViewModel Build()
        {
            return this.ViewModel;
        }

        protected virtual LayoutViewModelBuilder<TViewModel> WithCssPath()
        {
            this.ViewModel.CssPath = this.GetHashedPathToStaticFile("/gui/css/all.min.css");

            return this;
        }

        ////protected virtual LayoutViewModelBuilder<TViewModel> WithCurrentUser()
        ////{
        ////    var userId = HttpContext.Current.User.Identity.GetUserId();

        ////    this.ViewModel.CurrentUser = this.UserManager.FindById(userId);

        ////    return this;
        ////}

        private string GetHashedPathToStaticFile(string path)
        {
            var cacheKey = $"hash-for-{path}";

            var result = HttpRuntime.Cache.Get(cacheKey) as string;

            if (result == null)
            {
                lock (StringSemaphore.GetOrCreate(cacheKey))
                {
                    result = HttpRuntime.Cache.Get(cacheKey) as string;

                    if (result == null)
                    {
                        var localPath = HostingEnvironment.MapPath(path);

                        if (File.Exists(localPath) == false)
                        {
                            return string.Empty;
                        }

                        using (var md5 = MD5.Create())
                        {
                            using (var stream = File.OpenRead(localPath))
                            {
                                var hash = md5.ComputeHash(stream);
                                var nicerHash = BitConverter.ToString(hash)
                                    .Replace("-", string.Empty)
                                    .ToLowerInvariant();

                                var directory = VirtualPathUtility.GetDirectory(path);
                                var extension = VirtualPathUtility.GetExtension(path);
                                var fileName = VirtualPathUtility.GetFileName(path);
                                var fileNameWithoutExtension = fileName.Substring(0, fileName.Length - extension.Length);

                                result = VirtualPathUtility.Combine(directory, $"{fileNameWithoutExtension}.{nicerHash}{extension}");

                                var dependency = new CacheDependency(localPath);

                                HttpRuntime.Cache.Insert(
                                    cacheKey,
                                    result,
                                    dependency);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
