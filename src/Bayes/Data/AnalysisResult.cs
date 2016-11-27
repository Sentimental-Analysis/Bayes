using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

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
            var totalFeature = TotalFeature.ToBuilder();
            var totalCategory = TotalCategory.ToBuilder();
            var featureByCategory = FeatureByCategory.ToBuilder();
            ImmutableDictionary<string, int> features;
            if (!FeatureByCategory.TryGetValue(category, out features))
            {
                features = ImmutableDictionary<string, int>.Empty;
            }

            int count;
            if (!features.TryGetValue(feature, out count))
            {
                count = 0;
            }


            return null;
        }

        public AnalysisResult IncrementCategory(Category category)
        {
            return null;
        }
    }
}