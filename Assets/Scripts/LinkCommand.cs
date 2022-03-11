using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public struct LinkCommand
    {
        public LinkCommandType Type { get; set; }
        public string Data { get; set; }

        public LinkCommand(LinkCommandType type, string data)
        {
            Type = type;
            Data = data;
        }

        public LinkCommand(string type, string data)
        {
            Type = (LinkCommandType)Enum.Parse(typeof(LinkCommandType), type);
            Data = data;
        }
    }

    public enum LinkCommandType
    {
        JUMP,
        STUT,
    }
}
