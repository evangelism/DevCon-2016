using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.ReactionToCloud
{
    public class ReactionData
    {
        public string Nick { get; set; }
        public int Age { get; set; }
        public long Reaction { get; set; }
        public int Dizziness { get; set; }
        public string Sex { get; set; }

        public string Id { get; set; }

        public int DeviceType { get; set; }

        public int NoExperiment { get; set; }

        public int PreWait { get; set; }

        public ReactionData(string Id,string Nick, int Age, string Sex, 
            int Dizz, long React, int type, int noexp, int PreWait)
        {
            this.Id = Id;
            this.Nick = Nick;
            this.Age = Age;
            this.Sex = Sex;
            this.Dizziness = Dizz;
            this.Reaction = React;
            DeviceType = type;
            NoExperiment = noexp;
            this.PreWait = PreWait;
        }
    }

}
