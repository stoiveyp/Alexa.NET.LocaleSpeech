# Alexa.NET.LocaleSpeech

A small library to allow locale speech to be maintained seperately to the skill logic using it

This is done by registering "stores", classes that can support one or more locales.

## Create a LocaleSpeech Store

The easiest kind of store to create is a DictionaryLocaleStore. 

You can create stores for specific locales (en-GB, fr-CA) or general language (en, fr)

```csharp
var store = new DictionaryLocaleSpeechStore();
    store.AddLanguage("en", new Dictionary<string,object>
    {
        { "key", "value" },
        { "ssmlKey", new Speech(new PlainText("ssml value {0}")) }
    }
```

## Register a store

LocaleSpeech will check for specific, then general, in that order.

```csharp
var factory = new LocaleSpeechFactory(store);
```

## Create locale speech for a skill request

This uses the request locale

```csharp
 var localeSpeech = factory.CreateClient(skillRequest);
```

## Set speech with ResponseBuilder

Generate IOutputSpeech objects based on store keys

```csharp
ResponseBuilder.Tell(localeSpeech.Get("key"));
ResponseBuilder.Ask(localeSpeech.Get("ssmlKey",specificValue);
```