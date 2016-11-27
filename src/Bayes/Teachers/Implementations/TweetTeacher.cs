using System.Linq;
using Bayes.Data;
using Bayes.Teachers.Interfaces;

namespace Bayes.Teachers.Implementations
{
    public class TweetTeacher: ITeacher<Classification, AnalysisResult>
    {
        public AnalysisResult Learn(AnalysisResult oldResult, Classification source)
        {
            var result = source.Words.Aggregate(oldResult, (acc, x) => acc.IncrementFeature(source.Category, x));
            result = result.IncrementCategory(source.Category);
            return result;

        }
    }
}