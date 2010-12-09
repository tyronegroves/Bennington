using System.Collections.Generic;

namespace Deg.Alt.Mvc.Helpers
{
    public interface ITagReplacer
    {
        void AddReplaceTag(string tag, string value);
        string ProcessReplacementTags(string value);
    }

    public class TagReplacer : ITagReplacer
    {
        private readonly IDictionary<string, string> replaceTags;

        public TagReplacer()
        {
            replaceTags = new Dictionary<string, string>();
        }

        public void AddReplaceTag(string tag, string value)
        {
            if (replaceTags.ContainsKey(tag))
                replaceTags[tag] = value;
            else
                replaceTags.Add(tag, value);
        }

        public string ProcessReplacementTags(string value)
        {
            foreach (var key in replaceTags.Keys)
                value = value.Replace("[:" + key + ":]", replaceTags[key]);
            return value;
        }
    }
}