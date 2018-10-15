using System;
using System.Linq;
using Alexa.NET.Request;

namespace Alexa.NET.LocaleSpeech
{
    public class LocaleSpeechFactory:ILocaleSpeechFactory
    {
        public ILocaleSpeechStore[] Stores { get; set; }

        public LocaleSpeechFactory(params ILocaleSpeechStore[] stores)
        {
            if (stores == null)
            {
                throw new ArgumentNullException(nameof(stores));
            }

            if (!stores.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(stores),"No LocaleSpeech stores found");
            }

            Stores = stores;
        }

        public ILocaleSpeech Create(SkillRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return Create(request.Context.System.Application.ApplicationId, request.Request.Locale);
        }

        public ILocaleSpeech Create(string skillId, string locale)
        {
            if (string.IsNullOrWhiteSpace(skillId))
            {
                throw new ArgumentNullException(nameof(skillId));
            }

            if (string.IsNullOrWhiteSpace(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            var selectedStore = Stores.FirstOrDefault(s => s.Supports(skillId, locale));

            if (selectedStore == null && locale.Length == 5 && locale[2] == '-')
            {
                selectedStore = Stores.FirstOrDefault(s => s.Supports(skillId, locale.Substring(0, 2)));
            }

            if (selectedStore == null)
            {
                throw new InvalidOperationException($"unable to find store that supports locale {locale} within skill id {skillId}");
            }

            return new LocaleSpeech(selectedStore,skillId,locale);
        }
    }
}
