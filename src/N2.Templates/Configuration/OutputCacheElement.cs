﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace N2.Templates.Configuration
{
    public class OutputCacheElement : ConfigurationElement
    {
        [ConfigurationProperty("enabled", DefaultValue = false)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        [ConfigurationProperty("duration", DefaultValue = 0)]
        public int Duration
        {
            get { return (int)base["duration"]; }
            set { base["duration"] = value; }
        }

        [ConfigurationProperty("varyByParam", DefaultValue = "*")]
        public string VaryByParam
        {
            get { return (string)base["varyByParam"]; }
            set { base["varyByParam"] = value; }
        }

        [ConfigurationProperty("cacheProfile")]
        public string CacheProfile
        {
            get { return (string)base["cacheProfile"]; }
            set { base["cacheProfile"] = value; }
        }
    }
}