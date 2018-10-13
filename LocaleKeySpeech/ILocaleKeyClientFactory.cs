using Alexa.NET.Request;

namespace Alexa.NET.LocaleKeySpeech
{
    public interface ILocaleKeyClientFactory
    {
        ILocaleKeyClient Create(SkillRequest request);
        ILocaleKeyClient Create(string skillId, string locale);
    }
}