using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongBo
{
    public class Function 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public abstract class Identity
    {
        public int Id { get; set; }
    }
    public class PingPongEvent : Identity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string EventState { get; set; } 
    }

    public class PingPongDocumentFolder : PingPongDocumentResourceOrFolder
    {
        public List<PingPongDocumentResourceOrFolder> Children { get; set; }

    }

    public class PingPongDocumentResource : PingPongDocumentResourceOrFolder
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
    }

    public abstract class PingPongDocumentResourceOrFolder : Identity
    {
        public string Name { get; set; }
        public string Type { get; set; }

    }
    public class EventAndEnabledFunctions
    {
        public PingPongEvent PingPongEvent { get; set; }
        public List<Function> Functions { get; set; }
        public bool IsCourseTeacher{ get; set; } 
    }

    public class PingPongEventRole : Identity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string EvenstState   { get; set; }
        
    }

    public class PingPongGroup: Identity
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
