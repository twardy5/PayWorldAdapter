﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace PayWorldAdapter
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
