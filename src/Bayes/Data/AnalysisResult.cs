using System.Collections.Generic;
using System.Collections.Immutable;

namespace Bayes.Data
{
    public class AnalysisResult
    {
        public AnalysisResult(ImmutableDictionary<string, int> totalFeature, ImmutableDictionary<Category, int> totalCategory, ImmutableDictionary<Category, ImmutableDictionary<string, int>> featureByCategory)
        {
            TotalFeature = totalFeature;
            TotalCategory = totalCategory;
            FeatureByCategory = featureByCategory;
        }

        public ImmutableDictionary<string, int> TotalFeature { get; }
        public ImmutableDictionary<Category, int> TotalCategory { get; }
        public ImmutableDictionary<Category, ImmutableDictionary<string, int>> FeatureByCategory { get; }


        public static AnalysisResult Empty()
        {
            return new AnalysisResult(ImmutableDictionary<string, int>.Empty, ImmutableDictionary<Category, int>.Empty, ImmutableDictionary<Category, ImmutableDictionary<string, int>>.Empty);
        }

        public AnalysisResult IncrementFeature(Category category, string feature)
        {
            var totalFeature = TotalFeature;
            var featureByCategory = FeatureByCategory;
            ImmutableDictionary<string, int> features;
            if (featureByCategory.TryGetValue(category, out features))
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
                featureByCategory = featureByCategory.SetItem(category, features);
            }
            else
            {
                featureByCategory.Add(category, new Dictionary<string, int>() { {feature, 1} }.ToImmutableDictionary());
            }

            int totalCount;
            if (totalFeature.TryGetValue(feature, out totalCount))
            {
                totalFeature = totalFeature.SetItem(feature, totalCount + 1);
            }
            else
            {
                totalFeature = totalFeature.Add(feature, 1);
            }

            return new AnalysisResult(totalFeature, TotalCategory, featureByCategory);
        }

        public AnalysisResult IncrementCategory(Category category)
        {
            var totalCategory = TotalCategory;
            int count;
            if (totalCategory.TryGetValue(category, out count))
            {
                totalCategory = totalCategory.SetItem(category, count + 1);
            }
            else
            {
                totalCategory = totalCategory.Add(category, 1);
            }
            return new AnalysisResult(TotalFeature, totalCategory, FeatureByCategory);
        }
    }
}