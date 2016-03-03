using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JANR
{
    public class JanrSettingsClass
    {
        public string Server { get; set; }
        public JanrSettingsClass()
        {
            Server = "http://localhost/janr/";
        }
    }
}
