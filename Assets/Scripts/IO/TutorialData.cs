
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
        public const int Version = 2;

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

            if (this.StepCount == 0)
            {
                return entries.ToArray();
            }

            entries.Add(new TOCEntry(StepInformation[0].Title, 0));

            for (int i = 1; i < this.StepCount; i++)
            {
                if (StepInformation[i].Title != entries[entries.Count - 1].Text)
                {
                    entries.Add(new TOCEntry(StepInformation[i].Title, i));
                }
            }

            return entries.ToArray();
        }
    }
}
