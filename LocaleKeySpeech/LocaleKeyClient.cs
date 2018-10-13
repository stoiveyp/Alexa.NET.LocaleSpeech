using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.NET.LocaleKeySpeech
{
    public class LocaleKeyClient
    {
        public ILocaleKeyStore Store { get; }
        public string SkillId { get; }
        public string Locale { get; }

        public LocaleKeyClient(ILocaleKeyStore store, string skillId, string locale)
        {
            Store = store;
            SkillId = skillId;
            Locale = locale;
        }
    }
}
