/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Model for messages
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SequencePeeker.Models
{
    public class MessageModel
    {
        public string Message { get; set; }

        //constructor
        public MessageModel(string message)
        {
            Message = message;
        }
    }
}