using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleKeySpeech
{
    public interface ILocaleKeyClient
    {
        Task<IOutputSpeech> GetSpeech(string key);
        Task<IOutputSpeech> GetSpeech(string key, object[] arguments);
    }
}