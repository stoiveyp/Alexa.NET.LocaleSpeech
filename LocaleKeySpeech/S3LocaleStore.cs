using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleSpeech
{
    public class S3LocaleSpeechStore: ILocaleSpeechStore
    {
        public bool Supports(string locale)
        {
            throw new NotImplementedException();
        }

        public Task<IOutputSpeech> Get(string locale, string key, object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
