using System;
using System.Threading.Tasks;
using Alexa.NET.LocaleSpeech;
using Alexa.NET.Request;
using Alexa.NET.Response;
using NSubstitute;
using Xunit;

namespace LocaleSpeech.Tests
{
    public class LocaleSpeechTests
    {
        [Fact]
        public void ArgumentThrownWithNullStores()
        {
            Assert.Throws<ArgumentNullException>(() => new LocaleSpeechFactory(null));
        }

        [Fact]
        public void ArgumentThrowsWithEmptyStores()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocaleSpeechFactory(new ILocaleSpeechStore[]{}));
        }

        [Fact]
        public void ArgumentThrowsClientWithNullSkillRequest()
        {
            var store = Substitute.For<ILocaleSpeechStore>();
            var factory = new LocaleSpeechFactory(store);
            Assert.Throws<ArgumentNullException>(() => { factory.Create((SkillRequest)null);});
        }

        [Fact]
        public void ArgumentThrowsWithNullLocale()
        {
            var store = Substitute.For<ILocaleSpeechStore>();
            var factory = new LocaleSpeechFactory(store);
            Assert.Throws<ArgumentNullException>(() => factory.Create((string)null));
        }

        [Fact]
        public void ArgumentThrowsWithNoMatchingStore()
        {
            var store = Substitute.For<ILocaleSpeechStore>();
            store.Supports(Arg.Any<string>()).Returns(false);
            var factory = new LocaleSpeechFactory(store);
            Assert.Throws<InvalidOperationException>(() => factory.Create("en-GB"));
        }

        [Fact]
        public void SupportChecksForGeneralIfSpecificFound()
        {
            var store = Substitute.For<ILocaleSpeechStore>();
            store.Supports("en-GB").Returns(false);
            store.Supports("en").Returns(true);
            var factory = new LocaleSpeechFactory(store);
            var result = factory.Create("en-GB");
            Assert.NotNull(result);
        }

        [Fact]
        public void CreatesClientWithMatchingStore()
        {
            var store = Substitute.For<ILocaleSpeechStore>();
            store.Supports(Arg.Any<string>()).Returns(true);
            var factory = new LocaleSpeechFactory(store);
            var client = (Alexa.NET.LocaleSpeech.LocaleSpeech)factory.Create("en-GB");
            Assert.Equal(store, client.Store);
            Assert.Equal("en-GB",client.Locale);
        }

        [Fact]
        public async Task TranslateWithNoKeyThrows()
        {
            var store = Substitute.For<ILocaleSpeechStore>();
            store.Get(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<object[]>()).Returns((IOutputSpeech)null);
            var client = new Alexa.NET.LocaleSpeech.LocaleSpeech(store,"locale");
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Get("test"));
        }

        [Fact]
        public async Task TranslateWithKeyReturnsResult()
        {
            var speech = new PlainTextOutputSpeech {Text = "this is a test"};
            var store = Substitute.For<ILocaleSpeechStore>();
            store.Get(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<object[]>()).Returns(speech);
            var client = new Alexa.NET.LocaleSpeech.LocaleSpeech(store, "locale");
            var result = await client.Get("test");
            Assert.Equal(speech,result);
        }


    }
}
