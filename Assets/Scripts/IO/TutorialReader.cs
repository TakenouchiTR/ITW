using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.IO
{
    /// <summary>
    ///     Reads tutorial data from files.
    /// </summary>
    public class TutorialReader
    {
        /// <summary>
        ///     Reads tutorial data from a specified file.
        /// </summary>
        /// <param name="fileLocation">The file location.</param>
        /// <returns>
        ///     A TutorialData object containing the file's contents
        /// </returns>
        /// <exception cref="System.IO.FileNotFoundException">The file {fileLocation} does not exist.</exception>
        public static TutorialData ReadFile(string fileLocation)
        {
            TutorialData result = new TutorialData();
            if (!File.Exists(fileLocation))
            {
                throw new FileNotFoundException($"The file {fileLocation} does not exist.");
            }

            int version;
            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileLocation)))
            {
                version = reader.ReadInt32();
            }


            switch (version)
            {
                case 1:
                    return ReadVersion1(fileLocation);
            }

            return result;
        }

        /// <summary>
        ///     Reads files using version 1 of the file format.<br />
        ///     <br />
        ///     Version 1 only contains the titles, instructions, and positions for the part states.
        /// </summary>
        /// <param name="reader">An open.</param>
        /// <returns>A TutorialData object containing the file's contents</returns>
        private static TutorialData ReadVersion1(string fileLocation)
        {
            TutorialData data;

            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileLocation)))
            {
                //Read and discard the version
                reader.ReadInt32();

                int stepCount = reader.ReadInt32();
                int partCount = reader.ReadInt32();

                string[] titles = new string[stepCount];
                string[] instructions = new string[stepCount];
                PartState[][] states = new PartState[partCount][];

                for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                {
                    titles[stepIndex] = reader.ReadString();
                    instructions[stepIndex] = reader.ReadString();
                }

                for (int partIndex = 0; partIndex < partCount; partIndex++)
                {
                    states[partIndex] = new PartState[stepCount];
                    for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                    {
                        float x = reader.ReadSingle();
                        float y = reader.ReadSingle();
                        float z = reader.ReadSingle();
                        states[partIndex][stepIndex] = new PartState()
                        {
                            Position = new Vector3(x, y, z)
                        };
                    }
                }

                data = new TutorialData()
                {
                    Titles = titles,
                    Instructions = instructions,
                    States = states
                };
            }


            return data;
        }
    }
}
