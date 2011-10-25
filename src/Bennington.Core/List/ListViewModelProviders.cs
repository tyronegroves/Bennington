namespace Bennington.Core.List
{
    public static class ListViewModelProviders
    {
        private static readonly ListViewModelProviderDictionary providers = CreateDefaultBinderDictionary();

        private static ListViewModelProviderDictionary CreateDefaultBinderDictionary()
        {
            return new ListViewModelProviderDictionary();
        }

        public static ListViewModelProviderDictionary Providers
        {
            get { return providers; }
        }
    }
}