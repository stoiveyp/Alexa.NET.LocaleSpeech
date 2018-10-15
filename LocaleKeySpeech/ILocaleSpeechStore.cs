using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleSpeech
{
    public interface ILocaleSpeechStore
    {
        bool Supports(string locale);

        Task<IOutputSpeech> Get(string locale, string key, object[] parameters);
    }
}