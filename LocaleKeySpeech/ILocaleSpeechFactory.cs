using Alexa.NET.Request;

namespace Alexa.NET.LocaleSpeech
{
    public interface ILocaleSpeechFactory
    {
        ILocaleSpeech Create(SkillRequest request);
        ILocaleSpeech Create(string locale);
    }
}