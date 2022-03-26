
using System.Collections.Generic;

namespace Assets.Scripts.IO
{
    /// <summary>
    ///     Stores data about a tutorial for saving and loading files.
    /// </summary>
    public struct TutorialData
    {
        /// <summary>
        ///     The current file version.
        /// </summary>
        public const int Version = 3;

        /// <summary>
        ///     Gets or sets the step information.
        /// </summary>
        /// <value>
        ///     The step information.
        /// </value>
        public StepInformation[] StepInformation { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="PartTimeline"/> for each part at each step.
        /// </summary>
        /// <value>
        ///     The states.
        /// </value>
        public PartTimeline[] States { get; set; }

        /// <summary>
        ///     Gets the total number of steps.
        /// </summary>
        /// <value>
        ///     The number of steps.
        /// </value>
        public int StepCount => StepInformation.Length;

        /// <summary>
        ///     Gets the part count.
        /// </summary>
        /// <value>
        ///     The part count.
        /// </value>
        public int PartCount => States.Length;

        /// <summary>
        ///     Generates the table of contents.
        /// </summary>
        /// <returns>An array of <see cref="TOCEntry"/> representing the table of contents.</returns>
        public TOCEntry[] GenerateTableOfContents()
        {
            List<TOCEntry> entries = new List<TOCEntry>();

            string lastTitle = "";
            string lastSubtitle = "";

            for (int i = 0; i < this.StepCount; i++)
            {
                if (this.StepInformation[i].Title != lastTitle)
                {
                    lastTitle = this.StepInformation[i].Title;
                    entries.Add(new TOCEntry(lastTitle, i));
                }
                if (this.StepInformation[i].Subtitle != string.Empty && this.StepInformation[i].Subtitle != lastSubtitle)
                {
                    lastSubtitle = this.StepInformation[i].Subtitle;
                    entries.Add(new TOCEntry(lastSubtitle, i, 1));
                }
            }

            return entries.ToArray();
        }
    }
}
