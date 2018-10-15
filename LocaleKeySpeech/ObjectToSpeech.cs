using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexa.NET.Response;

namespace Alexa.NET.LocaleKeySpeech
{
    public static class ObjectToSpeech
    {
        public static IArrayChoice Pick { get; set; } = new DefaultArrayChoice();

        public static IOutputSpeech Generate(object text)
        {
            return Generate(text, null);
        }

        public static IOutputSpeech GenerateSsml(object text)
        {
            return GenerateSsml(text, null);
        }

        public static IOutputSpeech Generate(object text, object[] arguments)
        {
            switch (text)
            {
                case string raw:
                    return Generate(raw,arguments);
                case string[] rawArray:
                    return Generate(Pick.From(rawArray), arguments);
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
    }
}
