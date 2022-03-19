﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.IO
{

    /// <summary>
    ///     Stores data about a tutorial for saving and loading files.
    /// </summary>
    public struct TutorialData
    {
        /// <summary>
        ///     The current file version
        /// </summary>
        public const int Version = 1;

        /// <summary>
        ///     Gets or sets the titles. Each title is the text displayed at the top of the screen<br />
        ///     for each step
        /// </summary>
        /// <value>
        ///     The titles.
        /// </value>
        public string[] Titles { get; set; }

        /// <summary>
        ///     Gets or sets the instructions. Each instruction is displayed at the bottom of the<br />
        ///     screen for each step. Instructions may contain clickable, linked text that perform<br />
        ///     actions during the tutorial when clicked.
        /// </summary>
        /// <value>
        ///     The instructions.
        /// </value>
        public string[] Instructions { get; set; }

        /// <summary>
        ///     Gets or sets the states for each parts at each step. Each row represents a part, while<br />
        ///     the indices in the row represent its state for a given step.
        /// </summary>
        /// <value>
        ///     The states.
        /// </value>
        public PartState[][] States { get; set; }

        /// <summary>
        ///     Gets the total number of steps.
        /// </summary>
        /// <value>
        ///     The number of steps.
        /// </value>
        public int StepCount => Titles.Length;

        /// <summary>
        ///     Gets the part count.
        /// </summary>
        /// <value>
        ///     The part count.
        /// </value>
        public int PartCount => States.Length;
    }
}
