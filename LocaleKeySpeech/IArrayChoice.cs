using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.NET.LocaleKeySpeech
{
    public interface IArrayChoice
    {
        T From<T>(T[] choices);
    }
}
