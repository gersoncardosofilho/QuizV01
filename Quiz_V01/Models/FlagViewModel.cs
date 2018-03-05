using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz_V01.Models
{
    public class FlagViewModel
    {
        public Flag flag { get; set; }
        public List<Flag> AllFlags { get; set; }
        public List<string> FlagsToQuestion { get; set; }
        public string AnswerCheck { get; set; }

    }
}