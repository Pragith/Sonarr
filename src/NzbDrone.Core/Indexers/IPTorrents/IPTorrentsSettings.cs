using System;
using FluentValidation;
using FluentValidation.Results;
using NzbDrone.Core.Annotations;
using NzbDrone.Core.ThingiProvider;
using NzbDrone.Core.Validation;

namespace NzbDrone.Core.Indexers.IPTorrents
{
    public class IPTorrentsSettingsValidator : NzbDroneValidator<IPTorrentsSettings>
    {
        public IPTorrentsSettingsValidator()
        {
            RuleFor(c => c.Url).ValidRootUrl();
            RuleFor(c => c.Url).Matches(@"/rss\?.+;download$");
        }
    }

    public class IPTorrentsSettings : IProviderConfig
    {
        private static readonly IPTorrentsSettingsValidator validator = new IPTorrentsSettingsValidator();

        public IPTorrentsSettings()
        {
        }

        [FieldDefinition(0, Label = "Feed URL", HelpText = "The full RSS feed url generated by IPTorrents, using only the categories you selected (HD, SD, x264, etc ...)")]
        public String Url { get; set; }

        public ValidationResult Validate()
        {
            return validator.Validate(this);
        }
    }
}