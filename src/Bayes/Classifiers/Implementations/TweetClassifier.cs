using System;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Bayes.Utils;
using System.Linq;
using static Bayes.Utils.ProbabilityExtensions;

namespace Bayes.Classifiers.Implementations
{
    public class TweetClassifier : ITweetClassifier
    {
        public Score Classify(string parameter, LearnerState learnerState)
        {
            var words = parameter.Tokenize();
            int allElementsQuantity = learnerState.CategoryPerQuantity.Select(x => x.Value).Sum();
            var keys = learnerState.CategoryPerQuantity.Keys.ToList();
            var aprioriProbability = GetAprioriProbability(keys, allElementsQuantity, learnerState);
            var prioriProbability = GetPrioriProbability(keys, words, allElementsQuantity, learnerState);

            var res = aprioriProbability.Zip(prioriProbability, (apriori, priori) =>
            {
                if (apriori.Key == priori.Key)
                {
                    double probability = 0.0;
                    Probability aprioriProbabilityValue;
                    if (aprioriProbability.TryGetValue(apriori.Key, out aprioriProbabilityValue))
                    {
                        Probability prioriProbabilityValue;
                        if (prioriProbability.TryGetValue(apriori.Key, out prioriProbabilityValue))
                        {
                            probability = (aprioriProbabilityValue * prioriProbabilityValue).Value;
                        }
                    }
                    return Tuple.Create(apriori.Key, new Probability(probability));
                }
                return Tuple.Create(apriori.Key, new Probability(-1d));
            }).MaxBy(x => x.Item2.Value);
            return new Score(new Sentence(parameter, res.Item1), res.Item2);
        }
    }
}