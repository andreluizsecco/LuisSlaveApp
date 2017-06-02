using System;
using System.Runtime.Serialization;

namespace WebAPI.Models
{
    [Serializable]
    [DataContract]
    public class Command
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Parameter { get; set; }

        public Command(string name, string parameter)
        {
            Name = name;
            Parameter = parameter;
        }
    }
}