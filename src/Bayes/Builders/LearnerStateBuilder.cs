using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;

namespace Bayes.Builders
{
    public sealed class LearnerStateBuilder : IBuilder<LearnerState>
    {
        public Dictionary<WordCategory, int> CategoryPerQuantity { get; set; }
        public Dictionary<string, int> WordPerQuantity { get; set; }
        public Dictionary<WordCategory, Dictionary<string, int>> CategoryPerWords { get; set; }

        public LearnerStateBuilder()
        {

        }

        public LearnerStateBuilder(LearnerState state)
        {
            CategoryPerQuantity = state.CategoryPerQuantity.ToDictionary(x => x.Key, x => x.Value);
            WordPerQuantity = state.WordPerQuantity.ToDictionary(x => x.Key, x => x.Value);
            CategoryPerWords = state.CategoryPerWords.ToDictionary(x => x.Key,
                x => x.Value.ToDictionary(y => y.Key, y => y.Value));
        }

        public LearnerState Build()
        {
            var categoryPerQuantity = CategoryPerQuantity.ToImmutableDictionary();
            var wordPerQuantity = WordPerQuantity.ToImmutableDictionary();
            var categoryPerWords = CategoryPerWords.ToImmutableDictionary(x => x.Key,
                x => x.Value.ToImmutableDictionary());

            return new LearnerState(categoryPerQuantity, wordPerQuantity, categoryPerWords);
        }
    }
}