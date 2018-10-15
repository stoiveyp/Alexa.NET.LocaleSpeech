using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;

namespace Alexa.NET.LocaleKeySpeech
{
    public static class ObjectToSpeech
    {
        public static IArrayChoice Pick { get; set; } = new DefaultArrayChoice();

        public static IOutputSpeech Generate(object text)
        {
            var result = Generate(text, null);
            if (result == null)
            {
                throw new InvalidOperationException("unable to generate IOutputSpeech");
            }

            return result;
        }

        public static IOutputSpeech GenerateSsml(object text)
        {
            var result = GenerateSsml(text, null);
            if (result == null)
            {
                throw new InvalidOperationException("unable to generate IOutputSpeech");
            }
            return result;
        }

        public static IOutputSpeech Generate(object text, object[] arguments)
        {
            switch (text)
            {
                case string raw:
                    return Generate(raw,arguments);
                case string[] rawArray:
                    return Generate(Pick.From(rawArray), arguments);
                case Speech speech:
                    return GenerateSsml(speech, arguments);
            }

            return null;
        }

        public static IOutputSpeech GenerateSsml(object text, object[] arguments)
        {
            switch (text)
            {
                case string s:
                    return GenerateSsml(s, arguments);
                case string[] rawArray:
                    return GenerateSsml(Pick.From(rawArray), arguments);
                case Speech speech:
                    return GenerateSsml(speech, arguments);
            }

            return null;
        }

        public static IOutputSpeech Generate(string text, object[] arguments)
        {
            return new PlainTextOutputSpeech {Text = (arguments?.Any() ?? false) ? string.Format(text,arguments) : text};
        }

        public static IOutputSpeech GenerateSsml(string ssml, object[] arguments)
        {
            return new SsmlOutputSpeech { Ssml = arguments?.Any() ?? false ? string.Format(ssml,arguments) : ssml };
        }

        public static IOutputSpeech GenerateSsml(Speech speech, object[] arguments)
        {
            return new SsmlOutputSpeech { Ssml = arguments?.Any() ?? false ? string.Format(speech.ToXml(), arguments) : speech.ToXml() };
        }
    }
}
