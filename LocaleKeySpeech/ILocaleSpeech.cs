using System.Threading.Tasks;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleSpeech
{
    public interface ILocaleSpeech
    {
        Task<IOutputSpeech> Get(string key, object[] arguments);
    }
}