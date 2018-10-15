using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.LocaleSpeech;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using NSubstitute;
using Xunit;

namespace LocaleSpeech.Tests
{
    public class ObjectToSpeechTests
    {
        private const string plainText = "this is a {0} test";
        private const string ssmlText = "<speak>this is a {0} test</speak>";

        [Fact]
        public void StringConvertsCorrectly()
        {
            var result = ObjectToSpeech.Generate(plainText);
            CorrectPlainText(result);
        }

        [Fact]
        public void StringSsmlConvertsCorrect()
        {
            var result = ObjectToSpeech.GenerateSsml(ssmlText);
            CorrectSsmlText(result);
        }

        [Fact]
        public void StringParamsConvertsCorrectly()
        {
            var result = ObjectToSpeech.Generate(plainText, new []{"simple"});
            CorrectPlainText(result,"this is a simple test");
        }

        [Fact]
        public void StringSsmlParamsConvertsCorrectly()
        {
            var result = ObjectToSpeech.GenerateSsml(plainText, new[] { "simple" });
            CorrectSsmlText(result, "this is a simple test");
        }

        [Fact]
        public void StringArrayConvertsAccordingToPicker()
        {
            var customPicker = Substitute.For<IArrayChoice>();
            customPicker.From(Arg.Any<string[]>()).Returns(c => c.Arg<string[]>()[1]);
            ObjectToSpeech.Pick = customPicker;
            var result = ObjectToSpeech.Generate(new[] {"test", "thing"});
            CorrectPlainText(result,"thing");
        }

        [Fact]
        public void SsmlStringArrayConvertsAccordingToPicker()
        {
            var customPicker = Substitute.For<IArrayChoice>();
            customPicker.From(Arg.Any<string[]>()).Returns(c => c.Arg<string[]>()[1]);
            ObjectToSpeech.Pick = customPicker;
            var result = ObjectToSpeech.GenerateSsml(new[] { "test", "thing" });
            CorrectSsmlText(result, "thing");
        }

        [Fact]
        public void SpeechGeneratesSsmlRegardless()
        {
            var speech = new Speech(new PlainText(plainText));
            var result = ObjectToSpeech.Generate(speech);
            var ssmlResult = ObjectToSpeech.Generate(speech);
            CorrectSsmlText(result);
            CorrectSsmlText(ssmlResult);
        }

        [Fact]
        public void SpeechGeneratesWithArguments()
        {
            var speech = new Speech(new PlainText(plainText));
            var result = ObjectToSpeech.Generate(speech,new object[]{"simple"});
            var ssmlResult = ObjectToSpeech.GenerateSsml(speech,new object[]{"simple"});
            CorrectSsmlText(result, string.Format(ssmlText,"simple"));
            CorrectSsmlText(ssmlResult, string.Format(ssmlText, "simple"));
        }

        private void CorrectPlainText(IOutputSpeech speech, string text = plainText)
        {
            Assert.IsType<PlainTextOutputSpeech>(speech);
            Assert.Equal(text, ((PlainTextOutputSpeech)speech).Text);
        }
        private void CorrectSsmlText(IOutputSpeech speech, string text = ssmlText)
        {
            Assert.IsType<SsmlOutputSpeech>(speech);
            Assert.Equal(text, ((SsmlOutputSpeech)speech).Ssml);
        }
    }
}
