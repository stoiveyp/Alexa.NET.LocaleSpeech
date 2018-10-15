using System;
using System.Linq;
using Alexa.NET.Request;

namespace Alexa.NET.LocaleSpeech
{
    //https://developer.amazon.com/blogs/alexa/post/285a6778-0ed0-4467-a602-d9893eae34d7/how-to-localize-your-alexa-skills

    public class LocaleSpeechFactory : ILocaleSpeechFactory
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
                throw new ArgumentOutOfRangeException(nameof(stores), "No LocaleSpeech stores found");
            }

            Stores = stores;
        }

        public ILocaleSpeech Create(SkillRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return Create(request.Request.Locale);
        }

        public ILocaleSpeech Create(string locale)
        {
            if (string.IsNullOrWhiteSpace(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            var selectedStore = Stores.FirstOrDefault(s => s.Supports(locale));
            if (selectedStore != null)
            {
                return new LocaleSpeech(selectedStore, locale);
            }

            if (locale.Length == 5 && locale[2] == '-')
            {
                var generalLocale = locale.Substring(0, 2);
                selectedStore = Stores.FirstOrDefault(s => s.Supports(generalLocale));
                if (selectedStore != null)
                {
                    return new LocaleSpeech(selectedStore, generalLocale);
                }
            }

            throw new InvalidOperationException($"unable to find store that supports locale {locale}");
        }
    }
}
