namespace Alexa.NET.LocaleSpeech
{
    public interface IArrayChoice
    {
        T From<T>(T[] choices);
    }
}
