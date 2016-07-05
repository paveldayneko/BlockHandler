using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BlocksHandler.Domain
{
    [Serializable()]
    [XmlRoot("Block")]
    public class ContentBlock
    {
        [XmlAttribute("ID")]
        public string Id { get; set; }

        [XmlAttribute("BOX")]
        public string Box { get; set; }

        private double[] _coordinates;

        public double[] Coordinates
        {
            get
            {
                if (_coordinates != null)
                    return _coordinates;
                _coordinates = Array.ConvertAll(Box.Split(' '), s => double.Parse(s));
                return _coordinates;
            }
        }
    }
}