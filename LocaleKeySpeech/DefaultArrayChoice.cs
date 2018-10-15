using System;

namespace Alexa.NET.LocaleKeySpeech
{
    public class DefaultArrayChoice : IArrayChoice
    {
        private readonly Random Rnd = new Random(DateTime.Now.Millisecond);

        T IArrayChoice.From<T>(T[] choices)
        {
            if (choices.Length == 1)
            {
                return choices[0];
            }

            return choices[Rnd.Next(0, choices.Length - 1)];
        }
    }
}