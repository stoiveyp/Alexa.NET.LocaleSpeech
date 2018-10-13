using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleKeySpeech
{
    public interface ILocaleKeyStore
    {
        bool Supports(string skillId, string locale);

        Task<IOutputSpeech> GetSpeech(string skillId, string locale, string key, object[] parameters);
    }
}