using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    /// <summary>
    ///     Stores the type of command and the extra data needed to execute it.
    /// </summary>
    public struct LinkCommand
    {
        /// <summary>
        ///     Gets or sets the type of the command. <br />
        ///     This is what the command will do.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public LinkCommandType Type { get; set; }

        /// <summary>
        ///     Gets or sets the data of the command.<br />
        ///     This is what information the command needs to execute.
        /// </summary>
        /// <value>
        ///     The data.
        /// </value>
        public string Data { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LinkCommand"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        public LinkCommand(LinkCommandType type, string data)
        {
            this.Type = type;
            this.Data = data;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LinkCommand"/> struct.<br />
        ///     Attempts to convert the string type into a <see cref="LinkCommandType"/>.
        /// </summary>
        /// <param name="type">The name of the type as a string.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="System.ArgumentException">Unable to parse ${type} into a LinkCommandType.</exception>
        public LinkCommand(string type, string data)
        {
            if (Enum.TryParse(type, out LinkCommandType result))
            {
                this.Type = result;
                this.Data = data;
            }
            else
            {
                throw new ArgumentException($"Unable to parse ${type} into a LinkCommandType.");
            }
        }
    }

    /// <summary>
    ///     Represents the finite set of possible commands for links to execute.
    /// </summary>
    public enum LinkCommandType
    {
        /// <summary>
        ///     Jumps to a specified step.<br />
        ///     Usage: JUMP &lt;step:int&gt;
        /// </summary>
        JUMP,

        /// <summary>
        ///     Jumps immediately to a specified step.<br />
        ///     Usage: JUMP &lt;step:int&gt;
        /// </summary>
        IJMP,

        /// <summary>
        ///     Opens a specified subtutorial at a given step.<br />
        ///     Usage: STUT &lt;tutorial_index:int&gt;,&lt;step:int&gt;
        /// </summary>
        STUT,

        /// <summary>
        ///     Closes the current subtutorial and returns to the previous tutorial.<br />
        ///     Usage: "RTRN &lt;step:int&gt"<br />
        ///     A step of -1 will pick up where the previous tutorial left off.
        /// </summary>
        RTRN,

        /// <summary>
        ///     Opens a video with a specified filename.<br />
        ///     Usage: "VDEO &lt;filename:string&gt"<br />
        /// </summary>
        VDEO,
    }
}
