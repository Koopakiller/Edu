namespace StringAnalyzer.Helper
{
    internal class ProgressUpdateNotification
    {
        public int ProgressStepMaximum { get; set; }
        public int ProgressStepValue { get; set; }

        public int ProgressStep { get; set; }
        public int ProgressStepCount { get; set; }

        public string Info { get; set; }
    }
}