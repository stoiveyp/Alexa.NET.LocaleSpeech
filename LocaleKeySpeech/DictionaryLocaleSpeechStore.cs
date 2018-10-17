using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleSpeech
{
    public class DictionaryLocaleSpeechStore : ILocaleSpeechStore
    {
        public Dictionary<string, IDictionary<string, object>> Languages { get; } = new Dictionary<string, IDictionary<string, object>>();

        public DictionaryLocaleSpeechStore()
        {
        }

        public bool Supports(string locale)
        {
            return Languages.ContainsKey(locale.ToLower());
        }

        public Task<IOutputSpeech> Get(string locale, string key, object[] parameters)
        {
            var value = Languages[locale.ToLower()][key];
            return Task.FromResult(ObjectToSpeech.Generate(value,parameters));
        }

        public void AddLanguage(string locale, IDictionary<string, object> speech)
        {
            locale = locale.ToLower();
            if (Languages.ContainsKey(locale))
            {
                Languages[locale] = speech;
            }
            else
            {
                Languages.Add(locale, speech);
            }
        }
    }
}
