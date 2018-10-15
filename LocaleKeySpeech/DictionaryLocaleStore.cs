using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleKeySpeech
{
    public class DictionaryLocaleStore : ILocaleKeyStore
    {
        public string SkillId { get; }
        public Dictionary<string, IDictionary<string, object>> Languages { get; } = new Dictionary<string, IDictionary<string, object>>();

        public DictionaryLocaleStore()
        {
        }

        public DictionaryLocaleStore(string skillId)
        {
            SkillId = skillId ?? throw new ArgumentNullException(nameof(skillId));
        }

        public bool Supports(string skillId, string locale)
        {
            return (SkillId == null || SkillId == skillId) && Languages.ContainsKey(locale);
        }

        public Task<IOutputSpeech> GetSpeech(string skillId, string locale, string key, object[] parameters)
        {
            var value = Languages[locale][key];
            return Task.FromResult(ObjectToSpeech.Generate(value,parameters));
        }

        public void AddLanguage(string locale, IDictionary<string, object> speech)
        {
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
