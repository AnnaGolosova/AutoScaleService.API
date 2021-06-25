namespace AutoScaleService.Models.Configuration
{
    public class ResourcesSettings
    {
        public int MaxCount { get; set; }

        public int ResourceTranslationRate { get; set; }

        public int RequiredRate => MaxCount * ResourceTranslationRate;
    }
}
