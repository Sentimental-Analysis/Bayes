﻿using Bayes.Classifiers.Implementations;
using Bayes.Data;
using Bayes.Learner.Implementations;
using FluentAssertions;
using Xunit;

namespace Bayes.Tests.Classifiers
{
    public class TweetClassifierTests
    {
        [Fact]
        public void Test_Classifier_When_Text_Is_Negative()
        {
            var positiveText = "I love F#";
            var negativeText = "I hate java";
            var learner = new TweetLearner();
            var learnLove = learner.Learn(LearnerState.Empty, new Sentence(positiveText, WordCategory.Positive));
            var testLearner = learner.Learn(learnLove, new Sentence(negativeText, WordCategory.Negative));
            var classifier = new TweetClassifier(testLearner);
            var testResult = classifier.Classify("My brother hate java");
            testResult.Category.Should().Be(WordCategory.Negative);
        }


        [Fact]
        public void Test_Classifier_When_Text_Is_Positive()
        {
            var positiveText = "I love F#";
            var negativeText = "I hate java";
            var learner = new TweetLearner();
            var learnLove = learner.Learn(LearnerState.Empty, new Sentence(positiveText, WordCategory.Positive));
            var testLearner = learner.Learn(learnLove, new Sentence(negativeText, WordCategory.Negative));
            var classifier = new TweetClassifier(testLearner);
            var testResult = classifier.Classify("My brother love F#");
            testResult.Category.Should().Be(WordCategory.Positive);
        }

        [Fact]
        public void Test_Classifier_When_Text_Is_Positive2()
        {
            var positiveText = "I love sunny days";
            var negativeText = "I hate rain";
            var learner = new TweetLearner();
            var learnLove = learner.Learn(LearnerState.Empty, new Sentence(positiveText, WordCategory.Positive));
            var testLearner = learner.Learn(learnLove, new Sentence(negativeText, WordCategory.Negative));
            var classifier = new TweetClassifier(testLearner);
            var testResult = classifier.Classify("today is a sunny day");
            testResult.Category.Should().Be(WordCategory.Positive);
        }

        [Fact]
        public void Test_Classifier_When_Text_Is_Negative2()
        {
            var positiveText = "I love sunny days";
            var negativeText = "I hate rain";
            var learner = new TweetLearner();
            var learnLove = learner.Learn(LearnerState.Empty, new Sentence(positiveText, WordCategory.Positive));
            var testLearner = learner.Learn(learnLove, new Sentence(negativeText, WordCategory.Negative));
            var classifier = new TweetClassifier(testLearner);
            var testResult = classifier.Classify("there will be rain");
            testResult.Category.Should().Be(WordCategory.Negative);
        }
    }
}