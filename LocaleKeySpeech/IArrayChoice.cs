using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.NET.LocaleSpeech
{
    public interface IArrayChoice
    {
        T From<T>(T[] choices);
    }
}
