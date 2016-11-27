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

        public static AnalysisResult Increment(Category category, string feature)
        {
            return null;
        } 
    }
}