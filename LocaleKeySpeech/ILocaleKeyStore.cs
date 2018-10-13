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

        IOutputSpeech Translate(string skillId, string locale, string key, params object[] parameters);
    }
}