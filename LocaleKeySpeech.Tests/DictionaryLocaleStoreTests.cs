using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alexa.NET.LocaleSpeech;
using Alexa.NET.Response;
using Xunit;

namespace LocaleSpeech.Tests
{
    public class DictionaryLocaleStoreTests
    {
        [Fact]
        public void EmptyConstructorAlwaysReturnsTrueSupport()
        {
            var store = new DictionaryLocaleStore();
            store.AddLanguage("fr", new Dictionary<string, object> { });
            Assert.True(store.Supports("randomSkill", "fr"));
        }

        [Fact]
        public void NullSkillConstructorThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new DictionaryLocaleStore(null));
        }

        [Fact]
        public async Task MissingKeyThrowsException()
        {
            var store = new DictionaryLocaleStore();
            store.AddLanguage("fr", new Dictionary<string, object> { });
            await Assert.ThrowsAsync<KeyNotFoundException>(() => store.Get(null, "fr", "test", null));
        }

        [Fact]
        public async Task KeyWithNoParamsGeneratesSpeech()
        {
            var testText = "yes - a test";
            var store = new DictionaryLocaleStore();
            store.AddLanguage("fr", new Dictionary<string, object>
            {
                {"test",testText }
            });

            var result = await store.Get(null, "fr", "test", null);
            Assert.IsType<PlainTextOutputSpeech>(result);
            Assert.Equal(testText,((PlainTextOutputSpeech)result).Text);
        }

        [Fact]
        public async Task KeyWithParamsGeneratesSpeech()
        {
            var testText = "yes - a {0} test";
            var store = new DictionaryLocaleStore();
            store.AddLanguage("fr", new Dictionary<string, object>
            {
                {"test",testText }
            });

            var result = await store.Get(null, "fr", "test",new object[]{"simple"});
            Assert.IsType<PlainTextOutputSpeech>(result);
            Assert.Equal("yes - a simple test", ((PlainTextOutputSpeech)result).Text);
        }
    }
}
