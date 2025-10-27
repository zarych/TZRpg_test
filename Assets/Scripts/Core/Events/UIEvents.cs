using System;

namespace TZRpg.Core.Events
{
    public static class UIEvents
    {
        public static event Action GenerateCharacterRequested;
        public static event Action PlayButtonClicked;

        public static void RaiseGenerateCharacterRequested() => GenerateCharacterRequested?.Invoke();
        public static void RaisePlayButtonClicked() => PlayButtonClicked?.Invoke();
    }
}
