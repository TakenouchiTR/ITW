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
        ///     A <see cref="TutorialData" /> object containing the file's contents
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
                case 2:
                    return ReadVersion2(fileLocation);
                case 3:
                    return ReadVersion3(fileLocation);
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        ///     Reads files using version 1 of the file format.<br />
        ///     <br />
        ///     Version 1 only contains the titles, instructions, and positions for the part states.
        /// </summary>
        /// <param name="fileLocation">The file to read.</param>
        /// <returns>A <see cref="TutorialData"/> object containing the file's contents</returns>
        private static TutorialData ReadVersion1(string fileLocation)
        {
            TutorialData data;

            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileLocation)))
            {
                //Read and discard the version
                reader.ReadInt32();

                int stepCount = reader.ReadInt32();
                int partCount = reader.ReadInt32();

                StepInformation[] stepInformation = new StepInformation[stepCount];
                PartTimeline[] states = new PartTimeline[partCount];

                for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                {
                    string title = reader.ReadString();
                    string instructions = reader.ReadString();
                    stepInformation[stepIndex] = new StepInformation(title, "", instructions);
                }

                for (int partIndex = 0; partIndex < partCount; partIndex++)
                {
                    states[partIndex] = new PartTimeline(new List<PartState>());
                    for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                    {
                        float x = reader.ReadSingle();
                        float y = reader.ReadSingle();
                        float z = reader.ReadSingle();
                        states[partIndex].States.Add(new PartState()
                        {
                            Position = new Vector3(x, y, z)
                        });
                    }
                }

                data = new TutorialData()
                {
                    StepInformation = stepInformation,
                    States = states
                };
            }


            return data;
        }

        /// <summary>
        ///     Reads files using version 2 of the file format.<br />
        ///     <br />
        ///     Version 2 contains the titles, subtitles, instructions, part names, and positions for the part states.
        /// </summary>
        /// <param name="fileLocation">The file to read.</param>
        /// <returns>A <see cref="TutorialData"/> object containing the file's contents</returns>
        private static TutorialData ReadVersion2(string fileLocation)
        {
            TutorialData data;

            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileLocation)))
            {
                //Read and discard the version
                reader.ReadInt32();

                int stepCount = reader.ReadInt32();
                int partCount = reader.ReadInt32();

                StepInformation[] stepInformation = new StepInformation[stepCount];
                PartTimeline[] states = new PartTimeline[partCount];

                for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                {
                    string title = reader.ReadString();
                    string subtitle = reader.ReadString();
                    string instructions = reader.ReadString();
                    stepInformation[stepIndex] = new StepInformation(title, subtitle, instructions);
                }

                for (int partIndex = 0; partIndex < partCount; partIndex++)
                {
                    states[partIndex] = new PartTimeline(new List<PartState>());
                    states[partIndex].PartName = reader.ReadString();
                    for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                    {
                        float x = reader.ReadSingle();
                        float y = reader.ReadSingle();
                        float z = reader.ReadSingle();
                        states[partIndex].States.Add(new PartState()
                        {
                            Position = new Vector3(x, y, z)
                        });
                    }
                }

                data = new TutorialData()
                {
                    StepInformation = stepInformation,
                    States = states
                };
            }


            return data;
        }

        /// <summary>
        ///     Reads files using version 3 of the file format.<br />
        ///     <br />
        ///     Version 3 contains the titles, subtitles, instructions, audio clip, message & message type, part names, <br />
        ///     and positions for the part states.
        /// </summary>
        /// <param name="fileLocation">The file to read.</param>
        /// <returns>A <see cref="TutorialData"/> object containing the file's contents</returns>
        private static TutorialData ReadVersion3(string fileLocation)
        {
            TutorialData data;

            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileLocation)))
            {
                //Read and discard the version
                reader.ReadInt32();

                int stepCount = reader.ReadInt32();
                int partCount = reader.ReadInt32();

                StepInformation[] stepInformation = new StepInformation[stepCount];
                PartTimeline[] states = new PartTimeline[partCount];

                for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                {
                    string title = reader.ReadString();
                    string subtitle = reader.ReadString();
                    string audioFileName = reader.ReadString();
                    string message = reader.ReadString();
                    MessageType messageType = (MessageType)reader.ReadInt32();
                    string instructions = reader.ReadString();
                    stepInformation[stepIndex] = new StepInformation()
                    {
                        Title = title,
                        Subtitle = subtitle,
                        AudioFileName = audioFileName,
                        Message = new Message()
                        {
                            Text = message,
                            Type = messageType,
                        },
                        Instructions = instructions,
                    };
                }

                for (int partIndex = 0; partIndex < partCount; partIndex++)
                {
                    states[partIndex] = new PartTimeline(new List<PartState>());
                    states[partIndex].PartName = reader.ReadString();
                    for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                    {
                        float x = reader.ReadSingle();
                        float y = reader.ReadSingle();
                        float z = reader.ReadSingle();
                        states[partIndex].States.Add(new PartState()
                        {
                            Position = new Vector3(x, y, z)
                        });
                    }
                }

                data = new TutorialData()
                {
                    StepInformation = stepInformation,
                    States = states
                };
            }


            return data;
        }
    }
}
