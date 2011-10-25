using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bennington.Core.List
{
    public class ListViewModelProviderDictionary : IDictionary<Type, IListViewModelProvider>
    {
        private IListViewModelProvider defaultProvider;
        private readonly IDictionary<Type, IListViewModelProvider> innerDictionary = new Dictionary<Type, IListViewModelProvider>();

        public IListViewModelProvider DefaultProvider
        {
            get { return defaultProvider ?? (defaultProvider = new DefaultListViewModelProvider()); }
            set { defaultProvider = value; }
        }

        public ListViewModel GetListViewModelForType(Type type, ControllerContext controllerContext)
        {
            return GetListViewModelForType(type, controllerContext, new ListViewModelOptions());
        }

        public ListViewModel GetListViewModelForType(Type type, ControllerContext controllerContext, ListViewModelOptions options)
        {
            IListViewModelProvider provider;

            if(innerDictionary.TryGetValue(type, out provider)) 
                return provider.GetListViewModelForType(type, controllerContext, options);
            
            return DefaultProvider.GetListViewModelForType(type, controllerContext, options);
        }

        #region Dictionary Implementaton

        public IEnumerator<KeyValuePair<Type, IListViewModelProvider>> GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<Type, IListViewModelProvider> item)
        {
            innerDictionary.Add(item);
        }

        public void Clear()
        {
            innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<Type, IListViewModelProvider> item)
        {
            return innerDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<Type, IListViewModelProvider>[] array, int arrayIndex)
        {
            innerDictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Type, IListViewModelProvider> item)
        {
            return innerDictionary.Remove(item);
        }

        public int Count
        {
            get { return innerDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return innerDictionary.IsReadOnly; }
        }

        public bool ContainsKey(Type key)
        {
            return innerDictionary.ContainsKey(key);
        }

        public void Add(Type key, IListViewModelProvider value)
        {
            innerDictionary.Add(key, value);
        }

        public bool Remove(Type key)
        {
            return innerDictionary.Remove(key);
        }

        public bool TryGetValue(Type key, out IListViewModelProvider value)
        {
            return innerDictionary.TryGetValue(key, out value);
        }

        public IListViewModelProvider this[Type key]
        {
            get
            {
                IListViewModelProvider provider;
                innerDictionary.TryGetValue(key, out provider);
                return provider;
            }
            set { innerDictionary[key] = value; }
        }

        public ICollection<Type> Keys
        {
            get { return innerDictionary.Keys; }
        }

        public ICollection<IListViewModelProvider> Values
        {
            get { return innerDictionary.Values; }
        }

        #endregion
    }
}