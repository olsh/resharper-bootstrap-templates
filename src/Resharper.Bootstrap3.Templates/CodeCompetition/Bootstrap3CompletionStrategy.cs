using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Settings;
using JetBrains.ReSharper.Feature.Services.Html.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.Html.CodeCompletion.Settings;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Html;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;

namespace Resharper.Bootstrap3.Templates.CodeCompetition
{
    [SolutionComponent]
    public class Bootstrap3CompletionStrategy : IAutomaticCodeCompletionStrategy
    {
        private readonly SettingsScalarEntry _settingsScalarEntry;

        public Bootstrap3CompletionStrategy(ISettingsStore settingsStore)
        {
            _settingsScalarEntry =
                settingsStore.Schema.GetScalarEntry<HtmlAutopopupEnabledSettingsKey, AutopopupType>(
                    key => key.OnIdentifiers);
        }

        public bool ForceHideCompletion => false;

        public PsiLanguageType Language => HtmlLanguage.Instance;

        public bool AcceptsFile(IFile file, ITextControl textControl)
        {
            return this.MatchHtmlToken(
                file,
                textControl,
                curToken => curToken.GetTokenType() == curToken.TokenTypes.TEXT);
        }

        public bool AcceptTyping(char c, ITextControl textControl, IContextBoundSettingsStore boundSettingsStore)
        {
            return (c == 's') && this.MatchText(textControl, 1, text => text == "b");
        }

        public AutopopupType IsEnabledInSettings(IContextBoundSettingsStore settingsStore, ITextControl textControl)
        {
            return (AutopopupType)settingsStore.GetValue(_settingsScalarEntry, null);
        }

        public bool ProcessSubsequentTyping(char c, ITextControl textControl)
        {
            return false;
        }
    }
}
