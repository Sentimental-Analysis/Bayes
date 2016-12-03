using System.Collections.Generic;
using System.Collections.Immutable;

namespace Bayes.Data
{
    public sealed class LearnerState
    {
        public ImmutableDictionary<WordCategory, int> CategoryPerQuantity { get; }
        public ImmutableDictionary<string, int> WordPerQuantity { get; }
        public ImmutableDictionary<WordCategory, ImmutableDictionary<string, int>> CategoryPerWords { get; }

        public LearnerState(ImmutableDictionary<WordCategory, int> categoryPerQuantity,
            ImmutableDictionary<string, int> wordPerQuantity,
            ImmutableDictionary<WordCategory, ImmutableDictionary<string, int>> categoryPerWords)
        {
            CategoryPerQuantity = categoryPerQuantity;
            WordPerQuantity = wordPerQuantity;
            CategoryPerWords = categoryPerWords;
        }

        public static LearnerState Empty => new LearnerState(ImmutableDictionary<WordCategory, int>.Empty, ImmutableDictionary<string, int>.Empty, ImmutableDictionary<WordCategory, ImmutableDictionary<string, int>>.Empty);

        public LearnerState IncrementFeature(WordCategory category, string feature)
        {
            var wordPerQuantity = WordPerQuantity;
            var categoryPerWords = CategoryPerWords;
            ImmutableDictionary<string, int> features;
            if (categoryPerWords.TryGetValue(category, out features))
            {
                int count;
                if (features.TryGetValue(feature, out count))
                {
                    features = features.SetItem(feature, count + 1);
                }
                else
                {
                    features = features.Add(feature, 1);
                }
                categoryPerWords = categoryPerWords.SetItem(category, features);
            }
            else
            {
                categoryPerWords = categoryPerWords.Add(category, new Dictionary<string, int>() { { feature, 1 } }.ToImmutableDictionary());
            }

            int totalCount;
            if (wordPerQuantity.TryGetValue(feature, out totalCount))
            {
                wordPerQuantity = wordPerQuantity.SetItem(feature, totalCount + 1);
            }
            else
            {
                wordPerQuantity = wordPerQuantity.Add(feature, 1);
            }

            return new LearnerState(CategoryPerQuantity, wordPerQuantity, categoryPerWords);
        }

        public LearnerState DecrementFeature(WordCategory category, string feature)
        {
            var wordPerQuantity = WordPerQuantity;
            var categoryPerWords = CategoryPerWords;
            ImmutableDictionary<string, int> features;
            if (categoryPerWords.TryGetValue(category, out features))
            {
                int count;
                if (features.TryGetValue(feature, out count))
                {
                    categoryPerWords = categoryPerWords.SetItem(category, count == 1 ? features.Remove(feature) : features.SetItem(feature, count - 1));
                }
                else
                {
                    return this;
                }
            }
            else
            {
                return this;
            }

            int totalCount;
            if (wordPerQuantity.TryGetValue(feature, out totalCount))
            {
                wordPerQuantity = totalCount == 1 ? wordPerQuantity.Remove(feature) : wordPerQuantity.SetItem(feature, totalCount - 1);
            }
            else
            {
                return this;
            }

            return new LearnerState(CategoryPerQuantity, wordPerQuantity, categoryPerWords);
        }

        public LearnerState IncrementCategory(WordCategory category)
        {
            var totalCategory = CategoryPerQuantity;
            int count;
            if (totalCategory.TryGetValue(category, out count))
            {
                totalCategory = totalCategory.SetItem(category, count + 1);
            }
            else
            {
                totalCategory = totalCategory.Add(category, 1);
            }
            return new LearnerState(totalCategory, WordPerQuantity, CategoryPerWords);
        }

        private bool Equals(LearnerState other)
        {
            return Equals(CategoryPerQuantity, other.CategoryPerQuantity) && Equals(WordPerQuantity, other.WordPerQuantity) && Equals(CategoryPerWords, other.CategoryPerWords);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LearnerState) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CategoryPerQuantity?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (WordPerQuantity?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CategoryPerWords?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}