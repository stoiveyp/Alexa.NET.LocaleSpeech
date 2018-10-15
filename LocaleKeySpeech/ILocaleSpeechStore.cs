using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleSpeech
{
    public interface ILocaleSpeechStore
    {
        bool Supports(string skillId, string locale);

        Task<IOutputSpeech> Get(string skillId, string locale, string key, object[] parameters);
    }
}