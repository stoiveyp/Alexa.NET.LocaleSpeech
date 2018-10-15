using System;
using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleKeySpeech
{
    public class LocaleKeyClient : ILocaleKeyClient
    {
        public ILocaleKeyStore Store { get; }
        public string SkillId { get; }
        public string Locale { get; }

        public LocaleKeyClient(ILocaleKeyStore store, string skillId, string locale)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            SkillId = skillId ?? throw new ArgumentNullException(nameof(skillId));
            Locale = locale ?? throw new ArgumentNullException(nameof(locale));
        }

        public async Task<IOutputSpeech> GetSpeech(string key)
        {
            var result = await Store.GetSpeech(SkillId, Locale, key, null);
            if (result == null)
            {
                throw new ArgumentOutOfRangeException(nameof(key),$"No key \"{key}\" found in store");
            }

            return result;
        }

        public async Task<IOutputSpeech> GetSpeech(string key, params object[] arguments)
        {
            var result = await Store.GetSpeech(SkillId, Locale, key, arguments);
            if (result == null)
            {
                throw new ArgumentOutOfRangeException(nameof(key), $"No key \"{key}\" found in store");
            }

            return result;
        }
    }
}
