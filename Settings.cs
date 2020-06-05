using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Speedtest
{
    [Serializable]
    class Settings : ISerializable
    {
        public Int32 Interval { get; set; }
        public String SaveFolder { get; set; }
        public Boolean AutoTest { get; set; }
        public Boolean AutoStart { get; set; }

        public Settings()
        {
        }

        protected Settings(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            AutoStart = (Boolean)info.GetValue("AutoStart", typeof(Boolean));
            AutoTest = (Boolean)info.GetValue("AutoTest", typeof(Boolean));
            SaveFolder = (String) info.GetValue("SaveFolder", typeof(string));
            Interval = (Int32) info.GetValue("Interval", typeof(int));
        }
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            info.AddValue("Interval", Interval);
            info.AddValue("SaveFolder", SaveFolder);
            info.AddValue("AutoTest", AutoTest);
            info.AddValue("AutoStart", AutoStart);
        }
    }
}
