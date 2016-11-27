using Bayes.Data;
using Bayes.Teachers.Interfaces;

namespace Bayes.Teachers.Implementations
{
    public class TweetTeacher: ITeacher<Classification, AnalysisResult>
    {
        public AnalysisResult Learn(Classification source)
        {
            throw new System.NotImplementedException();
        }
    }
}