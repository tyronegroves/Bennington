using System.Collections.Generic;
using Deg.Alt.ContentProvider;

namespace Deg.Alt.Mvc.ContentCaching
{
    public class SectionRepositoryCache : ISectionRepository
    {
        private readonly ISectionRepository sectionRepository;
        private readonly IContentCacheState contentCacheState;
        private IEnumerable<Section> sections;
        private string cacheId;

        private static readonly object lockObject = new object();

        public SectionRepositoryCache(ISectionRepository sectionRepository, IContentCacheState contentCacheState)
        {
            this.sectionRepository = sectionRepository;
            this.contentCacheState = contentCacheState;
        }

        public IEnumerable<Section> GetSections()
        {
            if (RecachingIsNecessary())
                RefreshTheCacheWithALock();
            return sections;
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
            sections = sectionRepository.GetSections();
        }

        private bool RecachingIsNecessary()
        {
            return NoSectionsHaveBeenLoaded() || TheCacheIdHasChanged();
        }

        private bool TheCacheIdHasChanged()
        {
            return cacheId != contentCacheState.GetCacheId();
        }

        private bool NoSectionsHaveBeenLoaded()
        {
            return sections == null;
        }

        public ISectionRepository SectionRepository
        {
            get { return sectionRepository; }
        }

        public IContentCacheState ContentCacheState
        {
            get { return contentCacheState; }
        }
    }
}