using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;

namespace Alexa.NET.LocaleKeySpeech
{
    public class LocaleKeyClientFactory:ILocaleKeyClientFactory
    {
        public ILocaleKeyStore[] Stores { get; set; }

        public LocaleKeyClientFactory(params ILocaleKeyStore[] stores)
        {
            if (stores == null)
            {
                throw new ArgumentNullException(nameof(stores));
            }

            if (!stores.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(stores),"No LocaleKey stores found");
            }

            Stores = stores;
        }

        public ILocaleKeyClient Create(SkillRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return Create(request.Context.System.Application.ApplicationId, request.Request.Locale);
        }

        public ILocaleKeyClient Create(string skillId, string locale)
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

            if (selectedStore == null)
            {
                throw new InvalidOperationException($"unable to find store that supports locale {locale} within skill id {skillId}");
            }

            return new LocaleKeyClient(selectedStore,skillId,locale);
        }
    }
}
