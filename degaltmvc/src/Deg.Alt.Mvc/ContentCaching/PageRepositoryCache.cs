using System.Collections.Generic;
using Deg.Alt.ContentProvider;

namespace Deg.Alt.Mvc.ContentCaching
{
    public class PageRepositoryCache : IPageRepository
    {
        private readonly IPageRepository pageRepository;
        private readonly IContentCacheState contentCacheState;
        private IEnumerable<Page> pages;
        private string cacheId;

        private static readonly object lockObject = new object();

        public PageRepositoryCache(IPageRepository pageRepository, IContentCacheState contentCacheState)
        {
            this.pageRepository = pageRepository;
            this.contentCacheState = contentCacheState;
        }

        public IEnumerable<Page> GetPages()
        {
            if (RecachingIsNecessary())
                RefreshTheCacheWithALock();
            return pages;
        }

        private void RefreshTheCacheWithALock()
        {
            lock (lockObject)
                if (RecachingIsStillNecessary())
                    RefreshTheCache();
        }

        private bool RecachingIsStillNecessary()
        {
            return RecachingIsNecessary();
        }

        private void RefreshTheCache()
        {
            cacheId = contentCacheState.GetCacheId();
            pages = pageRepository.GetPages();
        }

        private bool RecachingIsNecessary()
        {
            return NoPagesHaveBeenLoaded() || TheCacheIdHasChanged();
        }

        private bool TheCacheIdHasChanged()
        {
            return cacheId != contentCacheState.GetCacheId();
        }

        private bool NoPagesHaveBeenLoaded()
        {
            return pages == null;
        }

        public IPageRepository PageRepository
        {
            get { return pageRepository; }
        }

        public IContentCacheState ContentCacheState
        {
            get { return contentCacheState; }
        }
    }
}