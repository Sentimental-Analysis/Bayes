using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Bayes.Data
{
    public sealed class SerializableLearnerState
    {
        public Dictionary<WordCategory, int> CategoryPerQuantity { get; set; }
        public Dictionary<string, int> WordPerQuantity { get; set; }
        public Dictionary<WordCategory, Dictionary<string, int>> CategoryPerWords { get; set; }

        public static SerializableLearnerState FromImmutableLearnerState(LearnerState state)
        {
            return new SerializableLearnerState()
            {
                CategoryPerQuantity = state.CategoryPerQuantity.ToDictionary(x => x.Key, x => x.Value),
                WordPerQuantity = state.WordPerQuantity.ToDictionary(x => x.Key, x => x.Value),
                CategoryPerWords = state.CategoryPerWords.ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => y.Value))
            };
        }

        public static LearnerState ToImmutableLearnerState(SerializableLearnerState state)
        {
            var categoryPerQuantity = state.CategoryPerQuantity.ToImmutableDictionary();
            var wordPerQuantity = state.WordPerQuantity.ToImmutableDictionary();
            var categoryPerWords = state.CategoryPerWords.ToImmutableDictionary(x => x.Key,
                x => x.Value.ToImmutableDictionary());

            return new LearnerState(categoryPerQuantity, wordPerQuantity, categoryPerWords);
        }
    }
}